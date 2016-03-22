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

        public DataSet GetLookupDataForReceivableSetUP(int adviserId, string product)
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_GetLookupDataForReceivableSetUP");
                db.AddInParameter(cmdGetLookupDataForReceivable, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetLookupDataForReceivable, "@Product", DbType.String, product);

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


        public DataSet GetProduct(int adviserId)
        {
            Database db;
            DbCommand cmdProduct;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdProduct = db.GetStoredProcCommand("SPROC_GetProduct");
                db.AddInParameter(cmdProduct, "@A_AdviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(cmdProduct);
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


        public DataSet GetCommisionTypes()
        {
            Database db;
            DbCommand cmdIssueMap;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdIssueMap = db.GetStoredProcCommand("SPROC_GetCommisionTypes");
                ds = db.ExecuteDataSet(cmdIssueMap);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return ds;
        }

        public DataSet GetIssuesStructureMapings(int adviserId, string mappedType, string issueType, string product, int IsOnlineIssue, int structureId, string SubCategoryCode)
        {
            Database db;
            DbCommand cmdIssueMap;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdIssueMap = db.GetStoredProcCommand("SPROC_GetMappedIssues");
                db.AddInParameter(cmdIssueMap, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdIssueMap, "@mappedType", DbType.String, mappedType);
                db.AddInParameter(cmdIssueMap, "@issueType", DbType.String, issueType);
                db.AddInParameter(cmdIssueMap, "@product", DbType.String, product);
                db.AddInParameter(cmdIssueMap, "@structureId", DbType.Int32, structureId);
                db.AddInParameter(cmdIssueMap, "@IsOnlineIssue", DbType.Int16, IsOnlineIssue);
                db.AddInParameter(cmdIssueMap, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, SubCategoryCode);
               
                ds = db.ExecuteDataSet(cmdIssueMap);
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


        public int IssueMappingDuplicateChecks(int issueId, DateTime validityStart, DateTime validityEnd, int structureId )
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;
            int result=0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_IssueMappingDuplicateChecks");
                db.AddInParameter(cmdCreateCommissionStructure, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdCreateCommissionStructure, "@validityStart", DbType.DateTime, validityStart);
                db.AddInParameter(cmdCreateCommissionStructure, "@validityEnd", DbType.DateTime, validityEnd);
                db.AddInParameter(cmdCreateCommissionStructure, "@structureId", DbType.Int32, structureId);
               // db.AddOutParameter(cmdCreateCommissionStructure, "@structureId", DbType.Int32, result);

                if(!string.IsNullOrEmpty(db.ExecuteScalar(cmdCreateCommissionStructure).ToString()))
                {
                  result = Convert.ToInt32(db.ExecuteScalar(cmdCreateCommissionStructure));
                }


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;

        }

        public int  DeleteMapping(  int ruleDetailId)
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_DeleteMapping");
                db.AddInParameter(cmdCreateCommissionStructure, "@ruleDetailId", DbType.Int32, ruleDetailId);
               int i= db.ExecuteNonQuery(cmdCreateCommissionStructure);
               if (i > 0)
                   return 1;
               else
                   return 0;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }


        public int DeleteStaffAndAssociateMapping(int ruleDetailId,int agentId,string category)
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_DeleteStaffAndAssociateMapping");
                db.AddInParameter(cmdCreateCommissionStructure, "@ruleDetailId", DbType.Int32, ruleDetailId);
                db.AddInParameter(cmdCreateCommissionStructure, "@agentId", DbType.Int32, agentId);
                db.AddInParameter(cmdCreateCommissionStructure, "@category", DbType.String, category);

                int i = db.ExecuteNonQuery(cmdCreateCommissionStructure);
                if (i > 0)
                    return 1;
                else
                    return 0;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }


        public void CreateIssuesStructureMapings(CommissionStructureRuleVo commissionStructureRuleVo, out  int instructureId)
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_CreateIssueStructureMapping");
                db.AddInParameter(cmdCreateCommissionStructure, "@issueId", DbType.Int32, commissionStructureRuleVo.IssueId);
                db.AddInParameter(cmdCreateCommissionStructure, "@structureId", DbType.Int32, commissionStructureRuleVo.CommissionStructureId);
                db.AddInParameter(cmdCreateCommissionStructure, "@ValidityStartDate", DbType.Date, DateTime.Today);
                db.AddInParameter(cmdCreateCommissionStructure, "@ValidityEndDate", DbType.Date, DateTime.Today);
                db.AddOutParameter(cmdCreateCommissionStructure, "@mappingStructureId", DbType.Int64, 1000000);

                db.ExecuteNonQuery(cmdCreateCommissionStructure);
                Object objCommissionStructureId = db.GetParameterValue(cmdCreateCommissionStructure, "@mappingStructureId");
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

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void CreateAdviserPayableRuleToAgentCategoryMapping(int StructureId, string userType, string Category,DataTable dtRuleMapping,string ruleId,int arnNo, out Int32 mappingId)
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;
            DataSet dsCreateAdviserPayableRuleToAgentCategoryMapping = new DataSet(); 
            try
            {
                dsCreateAdviserPayableRuleToAgentCategoryMapping.Tables.Add(dtRuleMapping.Copy());
                dsCreateAdviserPayableRuleToAgentCategoryMapping.DataSetName = "agentIdDS";
                dsCreateAdviserPayableRuleToAgentCategoryMapping.Tables[0].TableName = "agentIdDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_CreateAdviserPayableRuleToAgentCategoryMapping");
                db.AddInParameter(cmdCreateCommissionStructure, "@CommissionStructureId", DbType.Int64, StructureId);
                db.AddInParameter(cmdCreateCommissionStructure, "@UserType", DbType.String, userType);
                if (!string.IsNullOrEmpty(Category))
                {
                    db.AddInParameter(cmdCreateCommissionStructure, "@StaffCategory", DbType.String, Category);
                }
                else
                {
                    db.AddInParameter(cmdCreateCommissionStructure, "@StaffCategory", DbType.String, DBNull.Value);

                }
                db.AddInParameter(cmdCreateCommissionStructure, "@XMLagentId", DbType.Xml, dsCreateAdviserPayableRuleToAgentCategoryMapping.GetXml().ToString());
                //if (!string.IsNullOrEmpty(agentId))
                //{
                //    db.AddInParameter(cmdCreateCommissionStructure, "@AgentId", DbType.String, agentId);
                //}
                //else
                //{
                //    db.AddInParameter(cmdCreateCommissionStructure, "@AgentId", DbType.String, DBNull.Value);

                //}
                db.AddOutParameter(cmdCreateCommissionStructure, "@id", DbType.Int64, 1000000);
                db.AddInParameter(cmdCreateCommissionStructure, "@ruleDetailID", DbType.String, ruleId);
                db.AddInParameter(cmdCreateCommissionStructure, "@arnNo", DbType.Int16, arnNo);
                db.ExecuteNonQuery(cmdCreateCommissionStructure);
                Object objCommissionStructureId = db.GetParameterValue(cmdCreateCommissionStructure, "@id");
                if (objCommissionStructureId != DBNull.Value)
                    mappingId = Convert.ToInt32(objCommissionStructureId);
                else
                    mappingId = 1;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void CreatePayableAgentCodeMapping(int StructureId, string userType, string Category, string agentId, out Int32 mappingId )
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_CreateAdviserPayableStructureAgentCategoryMapping");
                db.AddInParameter(cmdCreateCommissionStructure, "@CommissionStructureId", DbType.Int64, StructureId);
                db.AddInParameter(cmdCreateCommissionStructure, "@UserType", DbType.String, userType);
                if (!string.IsNullOrEmpty(Category))
                {
                    db.AddInParameter(cmdCreateCommissionStructure, "@StaffCategory", DbType.Int64, Category);
                }
                else
                {
                    db.AddInParameter(cmdCreateCommissionStructure, "@StaffCategory", DbType.Int64, 0);

                }
                db.AddInParameter(cmdCreateCommissionStructure, "@AgentId", DbType.String, agentId);

                db.AddOutParameter(cmdCreateCommissionStructure, "@id", DbType.Int64, 1000000);
                db.ExecuteNonQuery(cmdCreateCommissionStructure);
                Object objCommissionStructureId = db.GetParameterValue(cmdCreateCommissionStructure, "@id");
                if (objCommissionStructureId != DBNull.Value)
                    mappingId = Convert.ToInt32(objCommissionStructureId);
                else
                    mappingId = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId, int commissionLookUpId, out Int32 instructureId)
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
                if (!string.IsNullOrEmpty(commissionStructureMasterVo.Issuer))
                    db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_Issuer", DbType.Int32, Convert.ToUInt32(commissionStructureMasterVo.Issuer.ToString()));
                else
                    db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_Issuer", DbType.Int32, 0);

                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_ValidityStartDate", DbType.Date, commissionStructureMasterVo.ValidityStartDate);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_ValidityEndDate", DbType.Date, commissionStructureMasterVo.ValidityEndDate);

                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsNonMonetaryReward", DbType.Int16, commissionStructureMasterVo.IsNonMonetaryReward);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsClawBackApplicable", DbType.Int16, commissionStructureMasterVo.IsClawBackApplicable);

                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_Note", DbType.String, commissionStructureMasterVo.StructureNote);
                db.AddInParameter(cmdCreateCommissionStructure, "@AssetSubGroupCode", DbType.String, Convert.ToString(commissionStructureMasterVo.AssetSubCategory));

                db.AddInParameter(cmdCreateCommissionStructure, "@UserId", DbType.String, userId);
                db.AddInParameter(cmdCreateCommissionStructure, "@commissionLookUpId", DbType.Int32, commissionLookUpId);

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


        public DataSet GetAdviserAgentCodes(int adviserId, string mappingType, int ARN, string MappedruleId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SP_GetAdviserWiseAgentCodes");
                db.AddInParameter(cmdGetCommissionStructureRules, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@MappingType", DbType.String, mappingType);
                db.AddInParameter(cmdGetCommissionStructureRules, "@ARN", DbType.Int32, ARN);
                db.AddInParameter(cmdGetCommissionStructureRules, "@MappedruleId", DbType.String, MappedruleId);

                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
        public DataSet GetAdviserAgentMappedCodes(int adviserId, string mappingType, int ARN, string MappedruleId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SP_GetAdviserWiseAgentMappedCodes");
                db.AddInParameter(cmdGetCommissionStructureRules, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@MappingType", DbType.String, mappingType);
                db.AddInParameter(cmdGetCommissionStructureRules, "@ARN", DbType.Int32, ARN);
                db.AddInParameter(cmdGetCommissionStructureRules, "@MappedruleId", DbType.String, MappedruleId);

                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
        //Gets all the structures under adviser
        public DataSet GetAdviserCommissionStructureRules(int adviserId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SP_GetAllStructList");
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

        public int CreateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId, string ruleHash)
        {
            Database db;
            DbCommand cmdCreateCommissionStructureRule;
            int CommissionRuleId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructureRule = db.GetStoredProcCommand("SPROC_CreateCommissionStructureRule");
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_CommissionStructureId", DbType.Int64, commissionStructureRuleVo.CommissionStructureId);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_CommissionStructureName", DbType.String, commissionStructureRuleVo.CommissionStructureRuleName);
               
                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCT_CommissionTypeCode", DbType.String, commissionStructureRuleVo.CommissionType);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@XCC_CustomerCategoryCode", DbType.String, commissionStructureRuleVo.CustomerType);

                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACG_CityGroupID", DbType.String, commissionStructureRuleVo.AdviserCityGroupCode);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCAL_ApplicableLevelCode", DbType.String, commissionStructureRuleVo.ApplicableLevelCode);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_ReceivableRuleFrequency", DbType.String, commissionStructureRuleVo.ReceivableFrequency);

                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_IsServiceTaxReduced", DbType.Int16, commissionStructureRuleVo.IsServiceTaxReduced);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_IsTDSReduced", DbType.Int16, commissionStructureRuleVo.IsTDSReduced);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_IsOtherTaxReduced", DbType.Int16, commissionStructureRuleVo.IsOtherTaxReduced);

                if (commissionStructureRuleVo.MinInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MinInvestmentAmount);
                }
                if (commissionStructureRuleVo.MaxInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MaxInvestmentAmount);
                }

                if (commissionStructureRuleVo.TenureMin != 0)
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinTenure", DbType.Int32, commissionStructureRuleVo.TenureMin);
                else
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinTenure", DbType.Int32, DBNull.Value);
                if (commissionStructureRuleVo.TenureMin > 0 && commissionStructureRuleVo.TenureMax != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, commissionStructureRuleVo.TenureMax);
                  
                }
                else
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, DBNull.Value);
                }


                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_TenureUnit", DbType.String, commissionStructureRuleVo.TenureUnit);




                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinInvestmentAge", DbType.Int32, commissionStructureRuleVo.MinInvestmentAge);

                if (commissionStructureRuleVo.MinInvestmentAge > 0 && commissionStructureRuleVo.MaxInvestmentAge == 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, commissionStructureRuleVo.MaxInvestmentAge);
                }


                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_InvestmentAgeUnit", DbType.String, commissionStructureRuleVo.InvestmentAgeUnit);


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
                if (commissionStructureRuleVo.MaxNumberofApplications != 0)
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxNumberOfApplications", DbType.Int32, commissionStructureRuleVo.MaxNumberofApplications);
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.StructureRuleComment))
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_Comment", DbType.String, commissionStructureRuleVo.StructureRuleComment);

                db.AddInParameter(cmdCreateCommissionStructureRule, "@UsetId", DbType.Int32, userId);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_CommissionRuleHash", DbType.String, ruleHash);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_ReducedValue", DbType.Decimal,commissionStructureRuleVo.TDSValue );
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_ServiceTaxValue", DbType.Decimal, commissionStructureRuleVo.TaxValue);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@CommissionSubType", DbType.String, commissionStructureRuleVo.CommissionSubType);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@CO_ApplicationNo", DbType.String, commissionStructureRuleVo.applicationNo);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@RuleValidateFrom", DbType.DateTime, commissionStructureRuleVo.RuleValidateFrom);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@RuleValidateTo", DbType.DateTime, commissionStructureRuleVo.RuleValidateTo);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_Mode", DbType.String, commissionStructureRuleVo.mode);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@AID_IssueDetailId", DbType.Int32, commissionStructureRuleVo.series);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@AIIC_InvestorCatgeoryId", DbType.Int32, commissionStructureRuleVo.Category);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_EForm", DbType.Int32, commissionStructureRuleVo.eForm);
                db.AddOutParameter(cmdCreateCommissionStructureRule, "@CommissionRuleId", DbType.Int32, 0);
                ////db.ExecuteNonQuery(cmdCreateCommissionStructureRule);
                if (db.ExecuteNonQuery(cmdCreateCommissionStructureRule) != 0)
                {
                    CommissionRuleId = Convert.ToInt32(db.GetParameterValue(cmdCreateCommissionStructureRule, "CommissionRuleId").ToString());
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
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:CreateCommissionStructureRule(CommissionStructureMasterVo commissionStructureMasterVo)");
                object[] objects = new object[2];
                objects[0] = commissionStructureRuleVo.CommissionStructureId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return CommissionRuleId;

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
        public DataSet GetPayableCommissionTypeBrokerage(int structureId)
        {
            Database db;
            DbCommand cmdGetStructureCommissionRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructureCommissionRules = db.GetStoredProcCommand("SPROC_GetPayableCommissionTypeBrokerage");
                db.AddInParameter(cmdGetStructureCommissionRules, "@structureId", DbType.String, structureId);
               
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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public int CreateUpdateDeleteCommissionTypeBrokerage(int ruleId, int commissionType, string brokerageUnit, decimal brokeragageValue, string ruleName, string commandType, int RuleDetailsId, int structureRuleDetailsId, string BrokerIdentifier)
            //string issuerName, string commandType)
        {
           
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateUpdateDeleteCommissionTypeBrokerage");
                db.AddInParameter(createCmd, "@ruleId", DbType.String, ruleId);
                db.AddInParameter(createCmd, "@commissionType", DbType.Int32, commissionType);
                db.AddInParameter(createCmd, "@brokerageUnit", DbType.String, brokerageUnit);
                db.AddInParameter(createCmd, "@brokeragageValue", DbType.Decimal, brokeragageValue);
                db.AddInParameter(createCmd, "@commandType", DbType.String, commandType);
                db.AddInParameter(createCmd, "@RuleDetailsId", DbType.String, RuleDetailsId);
                db.AddOutParameter(createCmd, "@structureRuleDetailsId", DbType.Int32, structureRuleDetailsId);
                db.AddInParameter(createCmd, "@CSRD_RateName", DbType.String, ruleName);
                db.AddInParameter(createCmd, "@XB_BrokerIdentifier", DbType.String, BrokerIdentifier);
                
                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    return structureRuleDetailsId;
                }
                else
                {
                    return 0;

                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
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

        public DataSet GetCommissionTypeBrokerage(int ruleId)
        {
            Database db;
            DbCommand cmdGetCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCatCm = db.GetStoredProcCommand("SPROC_GetCommissionTypeBrokerage");
                db.AddInParameter(cmdGetCatCm, "@ruleId", DbType.Int32, ruleId);

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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }

        public DataSet GetCommissionTypeAndBrokerage(int ruleId)
        {
            Database db;
            DbCommand cmdGetCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCatCm = db.GetStoredProcCommand("SP_GetCommissionTypeAndBrokerage");
                db.AddInParameter(cmdGetCatCm, "@ruleId", DbType.Int32, ruleId);

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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
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
                db.AddInParameter(cmdGetCatCm, "@PAG_AssetGroupCode", DbType.String, prodType);
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
                db.AddInParameter(cmdGetSubCatCm, "@PAIC_AssetInstrumentCategoryCode", DbType.String, cat);
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

        public DataSet GetProdAmc(int amccode)
        {
            Database db;
            DbCommand cmdGetProdAmc;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProdAmc = db.GetStoredProcCommand("SP_GetProductAmc");
                if (amccode > 0) db.AddInParameter(cmdGetProdAmc, "@PA_AMCCode", DbType.Int32, amccode);
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

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_GetAdviserCommissionStructureRulesCM");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                if (product.ToLower().Equals("all") == false) db.AddInParameter(cmdGetCommissionStructureRules, "@Product", DbType.String, product);
                if (cat.ToLower().Equals("all") == false) db.AddInParameter(cmdGetCommissionStructureRules, "@Category", DbType.String, cat);
                if (subcat.ToLower().Equals("all") == false) db.AddInParameter(cmdGetCommissionStructureRules, "@SubCategory", DbType.String, subcat);
                if (issuer > 0) db.AddInParameter(cmdGetCommissionStructureRules, "@Issuer", DbType.Int32, issuer);
                if (validity.ToLower().Equals("all") == false) db.AddInParameter(cmdGetCommissionStructureRules, "@Valid", DbType.String, validity);
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

                    commissionStructureMasterVo.ValidityStartDate = Convert.ToDateTime(dr["ACSM_ValidityStartDate"].ToString());
                    commissionStructureMasterVo.ValidityEndDate = Convert.ToDateTime(dr["ACSM_ValidityEndDate"].ToString());
                    commissionStructureMasterVo.CommissionLookUpId = Convert.ToInt32(dr["WCMV_CommissionType"].ToString());

                    commissionStructureMasterVo.IsNonMonetaryReward = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsNonMonetaryReward"].ToString()));
                    commissionStructureMasterVo.IsClawBackApplicable = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsClawBackApplicable"].ToString()));

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
                if (!string.IsNullOrEmpty(commissionStructureMasterVo.Issuer))
                    db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_Issuer", DbType.Int32, Convert.ToUInt32(commissionStructureMasterVo.Issuer.ToString()));
                else
                    db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_Issuer", DbType.Int32, 0);

                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_ValidityStartDate", DbType.Date, commissionStructureMasterVo.ValidityStartDate);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_ValidityEndDate", DbType.Date, commissionStructureMasterVo.ValidityEndDate);

                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsNonMonetaryReward", DbType.Int16, commissionStructureMasterVo.IsNonMonetaryReward);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsClawBackApplicable", DbType.Int16, commissionStructureMasterVo.IsClawBackApplicable);

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

        public void UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId, string strRuleHash)
        {
            Database db;
            DbCommand cmdUpdateCommissionStructureRule;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCommissionStructureRule = db.GetStoredProcCommand("SPROC_UpdateCommissionStructureRule");
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSM_CommissionStructureRuleId", DbType.Int64, commissionStructureRuleVo.CommissionStructureRuleId);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSM_CommissionStructureName", DbType.String, commissionStructureRuleVo.CommissionStructureRuleName);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@WCT_CommissionTypeCode", DbType.String, commissionStructureRuleVo.CommissionType);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@XCC_CustomerCategoryCode", DbType.String, commissionStructureRuleVo.CustomerType);

                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACG_CityGroupID", DbType.String, commissionStructureRuleVo.AdviserCityGroupCode);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@WCAL_ApplicableLevelCode", DbType.String, commissionStructureRuleVo.ApplicableLevelCode);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_ReceivableRuleFrequency", DbType.String, commissionStructureRuleVo.ReceivableFrequency);

                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_IsServiceTaxReduced", DbType.Int16, commissionStructureRuleVo.IsServiceTaxReduced);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_IsTDSReduced", DbType.Int16, commissionStructureRuleVo.IsTDSReduced);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_IsOtherTaxReduced", DbType.Int16, commissionStructureRuleVo.IsOtherTaxReduced);

                if (commissionStructureRuleVo.MinInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MinInvestmentAmount);

                }
                if (commissionStructureRuleVo.MaxInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MaxInvestmentAmount);
                }


                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinTenure", DbType.Int32, commissionStructureRuleVo.TenureMin);

                if (commissionStructureRuleVo.TenureMin > 0 && commissionStructureRuleVo.TenureMax == 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, commissionStructureRuleVo.TenureMax);
                }

                if ((commissionStructureRuleVo.TenureMin == 0 && commissionStructureRuleVo.TenureMax == 0))
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_TenureUnit", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_TenureUnit", DbType.String, commissionStructureRuleVo.TenureUnit);



                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinInvestmentAge", DbType.Int32, commissionStructureRuleVo.MinInvestmentAge);

                if (commissionStructureRuleVo.MinInvestmentAge > 0 && commissionStructureRuleVo.MaxInvestmentAge == 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, commissionStructureRuleVo.MaxInvestmentAge);
                }


                if (!string.IsNullOrEmpty(commissionStructureRuleVo.InvestmentAgeUnit))
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_InvestmentAgeUnit", DbType.String, commissionStructureRuleVo.InvestmentAgeUnit);


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
                if (commissionStructureRuleVo.MaxNumberofApplications != 0)
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxNumberOfApplications", DbType.Int32, commissionStructureRuleVo.MaxNumberofApplications);
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.StructureRuleComment))
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_Comment", DbType.String, commissionStructureRuleVo.StructureRuleComment);

                db.AddInParameter(cmdUpdateCommissionStructureRule, "@UsetId", DbType.Int32, userId);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_CommissionRuleHash", DbType.String, strRuleHash);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_ServiceTaxValue", DbType.Decimal, commissionStructureRuleVo.TaxValue);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "ACSR_ReducedValue", DbType.Decimal, commissionStructureRuleVo.TDSValue);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@CommissionSubType", DbType.String, commissionStructureRuleVo.CommissionSubType);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@CO_ApplicationNo", DbType.String, commissionStructureRuleVo.applicationNo);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@RuleValidateFrom", DbType.DateTime, commissionStructureRuleVo.RuleValidateFrom);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@RuleValidateTo", DbType.DateTime, commissionStructureRuleVo.RuleValidateTo);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_Mode", DbType.String, commissionStructureRuleVo.mode);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@AID_IssueDetailId", DbType.Int32, commissionStructureRuleVo.series);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@AIIC_InvestorCatgeoryId", DbType.Int32, commissionStructureRuleVo.Category);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_EForm", DbType.Int32, commissionStructureRuleVo.eForm);
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
                if (cmStructId >= 1)
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
                if (cmStructId >= 1)
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

        public DataSet GetAvailSchemes(int adviserId, int structId, int issuer, string prodType, string cat, string subCat, DateTime validFrom, DateTime validTill)
        {
            Database db;
            DbCommand cmdGetAvailSchemes;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAvailSchemes = db.GetStoredProcCommand("SPROC_GetAvailableSchemes");
                db.AddInParameter(cmdGetAvailSchemes, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetAvailSchemes, "@StructId", DbType.Int32, structId);
                db.AddInParameter(cmdGetAvailSchemes, "@Issuer", DbType.Int32, issuer);
                db.AddInParameter(cmdGetAvailSchemes, "@Product", DbType.String, prodType);
                db.AddInParameter(cmdGetAvailSchemes, "@Category", DbType.String, cat);
                db.AddInParameter(cmdGetAvailSchemes, "@Subcategory", DbType.String, subCat);
                db.AddInParameter(cmdGetAvailSchemes, "@ValidFrom", DbType.DateTime, validFrom);
                db.AddInParameter(cmdGetAvailSchemes, "@ValidTill", DbType.DateTime, validTill);
                ds = db.ExecuteDataSet(cmdGetAvailSchemes);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetAvailSchemes(int adviserId, int structId, int issuer, string prodType, string cat, string subCat, DateTime validFrom, DateTime validTill)");
                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = issuer;
                objects[2] = prodType;
                objects[3] = cat;
                objects[4] = subCat;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /**
         * CommissionStructureToSchemeMapping - 
         */
        public void DeleteIssueMapping(int setUpId)
        {

            Database db;
            DbCommand cmdGetStructDet;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructDet = db.GetStoredProcCommand("SPROC_DeleteIssueMapping");
                db.AddInParameter(cmdGetStructDet, "@setUpId", DbType.Int32, setUpId);
                ds = db.ExecuteDataSet(cmdGetStructDet);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataSet  GetPayableMappings(int ruleDetID)
        {
            Database db;
            DbCommand cmdGetStructDet;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructDet = db.GetStoredProcCommand("SPROC_GetPayableMappings");
                db.AddInParameter(cmdGetStructDet, "@RuleDetId", DbType.Int32, ruleDetID);                
                ds = db.ExecuteDataSet(cmdGetStructDet);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        public DataSet GetStructureDetails(int adviserId, int structureId)
        {
            Database db;
            DbCommand cmdGetStructDet;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructDet = db.GetStoredProcCommand("SPROC_GetCommissionStructureDetails");
                db.AddInParameter(cmdGetStructDet, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetStructDet, "@StructureId", DbType.Int32, structureId);
                ds = db.ExecuteDataSet(cmdGetStructDet);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetStructureDetails(int adviserId, int structureId)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = structureId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetSubcategories(int adviserId, int structureId)
        {
            Database db;
            DbCommand cmdGetStructDet;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructDet = db.GetStoredProcCommand("SPROC_GetSubcategoriesOfStructure");
                db.AddInParameter(cmdGetStructDet, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetStructDet, "@StructureId", DbType.Int32, structureId);
                ds = db.ExecuteDataSet(cmdGetStructDet);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetSubcategories(int adviserId, int structureId)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = structureId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public void MapSchemesToStructres(int structureId, int schemeId, DateTime validFrom, DateTime validTill)
        {
            Database db;
            DbCommand cmdMapScheme;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMapScheme = db.GetStoredProcCommand("SPROC_MapSchemesToStructure");
                db.AddInParameter(cmdMapScheme, "@StructId", DbType.Int32, structureId);
                db.AddInParameter(cmdMapScheme, "@SchemeId", DbType.Int32, schemeId);
                db.AddInParameter(cmdMapScheme, "@ValidFrom", DbType.DateTime, validFrom);
                db.AddInParameter(cmdMapScheme, "@ValidTill", DbType.DateTime, validTill);
                db.ExecuteDataSet(cmdMapScheme);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:MapSchemesToStructres(int structId, int schemeId, DateTime validFrom, DateTime validTill)");
                object[] objects = new object[4];
                objects[0] = structureId;
                objects[1] = schemeId;
                objects[2] = validFrom;
                objects[3] = validTill;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public int checkSchemeAssociationExists(int schemeId, int structId, DateTime validFrom, DateTime validTo,string category)
        {
            Database db;
            DbCommand cmdUpdateSetup;
            int retVal = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateSetup = db.GetStoredProcCommand("SPROC_CheckSchemeAssociation");
                db.AddInParameter(cmdUpdateSetup, "@SchemeId", DbType.Int32, schemeId);
                db.AddInParameter(cmdUpdateSetup, "@StructId", DbType.Int32, structId);
                db.AddInParameter(cmdUpdateSetup, "@ValidFrom", DbType.DateTime, validFrom);
                db.AddInParameter(cmdUpdateSetup, "@ValidTill", DbType.DateTime, validTo);
                db.AddOutParameter(cmdUpdateSetup, "@RowUpdate", DbType.Int32, retVal);
                db.AddInParameter(cmdUpdateSetup, "@category", DbType.String, category);
                
                db.ExecuteDataSet(cmdUpdateSetup);
                retVal = (int)cmdUpdateSetup.Parameters["@RowUpdate"].Value;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:updateStructureToSchemeMapping(int setupId, DateTime validTill)");
                object[] objects = new object[4];
                objects[0] = schemeId;
                objects[1] = structId;
                objects[2] = validFrom;
                objects[3] = validTo;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return retVal;
        }

        public int checkSchemeAssociationExists(int setupId, DateTime validFrom, DateTime validTo)
        {
            Database db;
            DbCommand cmdUpdateSetup;
            int retVal = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateSetup = db.GetStoredProcCommand("SPROC_CheckSchemeAssociationBySetup");
                db.AddInParameter(cmdUpdateSetup, "@SetupId", DbType.Int32, setupId);
                db.AddInParameter(cmdUpdateSetup, "@ValidFrom", DbType.DateTime, validFrom);
                db.AddInParameter(cmdUpdateSetup, "@ValidTill", DbType.DateTime, validTo);
                db.AddOutParameter(cmdUpdateSetup, "@RowUpdate", DbType.Int32, retVal);
                db.ExecuteDataSet(cmdUpdateSetup);
                retVal = (int)cmdUpdateSetup.Parameters["@RowUpdate"].Value;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:updateStructureToSchemeMapping(int setupId, DateTime validTill)");
                object[] objects = new object[3];
                objects[0] = setupId;
                objects[1] = validFrom;
                objects[2] = validTo;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return retVal;
        }

        public int updateStructureToSchemeMapping(int setupId, DateTime validTill)
        {
            Database db;
            DbCommand cmdUpdateSetup;
            int rowsUpdate = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateSetup = db.GetStoredProcCommand("SPROC_UpdateStructureToSchemeMapping");
                db.AddInParameter(cmdUpdateSetup, "@SetupId", DbType.Int32, setupId);
                db.AddInParameter(cmdUpdateSetup, "@ValidTill", DbType.DateTime, validTill);
                db.AddOutParameter(cmdUpdateSetup, "@RowUpdate", DbType.Int32, rowsUpdate);
                db.ExecuteDataSet(cmdUpdateSetup);
                return (int)cmdUpdateSetup.Parameters["@RowUpdate"].Value;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:updateStructureToSchemeMapping(int setupId, DateTime validTill)");
                object[] objects = new object[2];
                objects[0] = setupId;
                objects[1] = validTill;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet GetStructureScheme(int adviserId, string product)
        {
            DataSet dsStructureScheme = new DataSet();
            Database db;
            DbCommand cmdStructureScheme;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdStructureScheme = db.GetStoredProcCommand("SP_GetStuctSchemeAssoc");
                db.AddInParameter(cmdStructureScheme, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdStructureScheme, "@product", DbType.String, product);

                dsStructureScheme = db.ExecuteDataSet(cmdStructureScheme);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetStructureScheme(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructureScheme;
        }


        public DataSet GetCommissionSchemeStructureRuleList(int adviserId, string product)
        {
            Database db;
            DbCommand cmdGetCommissionSchemeStructureRuleList;
            DataSet dsSchemeStructureRule = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionSchemeStructureRuleList = db.GetStoredProcCommand("SPROC_GetCommissionSchemeStructureRule");
                db.AddInParameter(cmdGetCommissionSchemeStructureRuleList, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionSchemeStructureRuleList, "@Product", DbType.String, product);

                dsSchemeStructureRule = db.ExecuteDataSet(cmdGetCommissionSchemeStructureRuleList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetCommissionSchemeStructureRuleList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeStructureRule;
        }

        public void deleteStructureToSchemeMapping(int setupId)
        {
            Database db;
            DbCommand cmdUpdateSetup;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateSetup = db.GetStoredProcCommand("SPROC_DeleteStructureToSchemeMapping");
                db.AddInParameter(cmdUpdateSetup, "@SetupId", DbType.Int32, setupId);
                db.ExecuteDataSet(cmdUpdateSetup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:deleteStructureToSchemeMapping(int setupId)");
                object[] objects = new object[1];
                objects[0] = setupId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool hasRule(int adviserId, string ruleHash)
        {
            Database db;
            DbCommand cmdUpdateSetup;
            DataSet dsRules;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateSetup = db.GetStoredProcCommand("SP_RuleExists");
                db.AddInParameter(cmdUpdateSetup, "@ACSR_CommissionRuleHash", DbType.String, ruleHash);
                db.AddInParameter(cmdUpdateSetup, "@A_AdviserId", DbType.Int32, adviserId);
                dsRules = db.ExecuteDataSet(cmdUpdateSetup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:bool hasRule(int adviserId, string ruleHash)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = ruleHash;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (dsRules.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public DataSet PaybleStructureViewWithAssociateDetails(int adviserId, int isCategory, int categoryId)
        {
            Database db;
            DbCommand cmdPaybleStructureViewWithAssociateDetails;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdPaybleStructureViewWithAssociateDetails = db.GetStoredProcCommand("SP_PaybleStructureViewWithAssociateDetails");
                db.AddInParameter(cmdPaybleStructureViewWithAssociateDetails, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdPaybleStructureViewWithAssociateDetails, "@CategoryId", DbType.Int32, categoryId);
                db.AddInParameter(cmdPaybleStructureViewWithAssociateDetails, "@IsCategoryWise", DbType.Int32, isCategory);

                ds = db.ExecuteDataSet(cmdPaybleStructureViewWithAssociateDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetCommissionSchemeStructureRuleList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public int RuleAssociate(string ruleid)
        {
            Database db;
            DataSet ds;
            DbCommand cmdRuleAssociate;
            int ruleids = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdRuleAssociate = db.GetStoredProcCommand("SPROC_GetRuleAssociated");
                db.AddInParameter(cmdRuleAssociate, "@structureId", DbType.Int32, ruleid);
                db.AddOutParameter(cmdRuleAssociate, "@ruleids", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdRuleAssociate);
                if (db.ExecuteNonQuery(cmdRuleAssociate) != 0)
                {
                    ruleids = Convert.ToInt32(db.GetParameterValue(cmdRuleAssociate, "ruleids").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ruleids;
        }
        public DataTable GetMappedStructure(int ruleid)
        {
            Database db;
            DbCommand cmdGetMappedStructure;
            DataSet ds = null;
            DataTable dt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetMappedStructure = db.GetStoredProcCommand("SPROC_GetMAppedStructure");
                db.AddInParameter(cmdGetMappedStructure, "@structureId", DbType.Int32,ruleid );
                ds = db.ExecuteDataSet(cmdGetMappedStructure);
                dt = ds.Tables[0];
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
            return dt;
        }
        public DataTable GetIncentiveType()
        {
            Database db;
            DbCommand cmdGetIncentiveType;
            DataSet ds = null;
            DataTable dt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIncentiveType = db.GetStoredProcCommand("SPROC_GetIncentiveType");
                ds = db.ExecuteDataSet(cmdGetIncentiveType);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataTable GetCategory(int IssueId)
        {
            Database db;
            DbCommand cmdGetCategory;
            DataSet ds = null;
            DataTable dt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCategory = db.GetStoredProcCommand("SPROC_GetCategory");
                db.AddInParameter(cmdGetCategory, "@issueId", DbType.Int32, IssueId);
                ds = db.ExecuteDataSet(cmdGetCategory);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataTable GetSeriese(int issueId,int categoryId)
        {
            Database db;
            DbCommand cmdGetSeriese;
            DataSet ds = null;
            DataTable dt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSeriese = db.GetStoredProcCommand("SPROC_GetSerieseName");
                db.AddInParameter(cmdGetSeriese, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdGetSeriese, "@InvestorCatgeoryId", DbType.Int32, categoryId);
                ds = db.ExecuteDataSet(cmdGetSeriese);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataSet BindNcdCategory(string type, string catCode)
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetNcdCategorysbrokrage");
                db.AddInParameter(dbCommand, "@Type", DbType.String, type);
                db.AddInParameter(dbCommand, "@catCode", DbType.String, catCode);

                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindNcdCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }
        public DataTable GetAssociateCommissionPayout(int adviserId, string agentCode,DateTime toDate,DateTime fromDate,Boolean IsDummyAgent)
        {
            DataSet dsGetAssociateCommissionPayout;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAssociateCommissionPayOuts");
                db.AddInParameter(dbCommand, "@AdviserId", DbType.String, adviserId);
                if (!string.IsNullOrEmpty(agentCode))
                    db.AddInParameter(dbCommand, "@AgentCode", DbType.String, agentCode);                
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@IsDummyAgent", DbType.Boolean, IsDummyAgent);
                dsGetAssociateCommissionPayout = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetAssociateCommissionPayout(int adviserId, string agentCode,DateTime toDate,DateTime fromDate)");
                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = agentCode;
                objects[2] = toDate;
                objects[3] = fromDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAssociateCommissionPayout.Tables[0];
        }
        public DataTable GetAgentProductWiseCommissionDetails(string agentCode, string product, string subCategory, int issueId, int adviserId, DateTime Date, string CommissionType)
        {
            Database db;
            DbCommand cmdGetAgentProductWiseCommissionDetails;
            DataTable dtAgentCommissionDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAgentProductWiseCommissionDetails = db.GetStoredProcCommand("SPROC_GetAgentProductCommissionDetails");
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@agentCode",DbType.String, agentCode);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@product", DbType.String, product);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@subCategory", DbType.String, subCategory);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@Date", DbType.Date, Date);
                db.AddInParameter(cmdGetAgentProductWiseCommissionDetails, "@CommissionType", DbType.String, CommissionType);
                dtAgentCommissionDetails = db.ExecuteDataSet(cmdGetAgentProductWiseCommissionDetails).Tables[0];
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionReceivable.cs:GetAgentProductWiseCommissionDetails(string agentCode,string product,string subCategory,int issueId,int adviserId)");
                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = agentCode;
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAgentCommissionDetails;
        }
        public DataTable getProductCommissionReceivable(string product,string productCategory,int amcCode,int schemeId,int adviserId,int issueId,int from,int to)
        {
            Database db;
            DbCommand cmdGetProductCommissionDetails;
            DataTable dtProductiontCommissionDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProductCommissionDetails = db.GetStoredProcCommand("SPROC_GetProductReceivableCommissionDetails");
                db.AddInParameter(cmdGetProductCommissionDetails, "@product", DbType.String, product);
                db.AddInParameter(cmdGetProductCommissionDetails, "@productCategory", DbType.String, productCategory);
                db.AddInParameter(cmdGetProductCommissionDetails, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(cmdGetProductCommissionDetails, "@schemeId", DbType.Int32, schemeId);
                db.AddInParameter(cmdGetProductCommissionDetails, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetProductCommissionDetails, "@Month", DbType.Int32, from);
                db.AddInParameter(cmdGetProductCommissionDetails, "@Year", DbType.Int32, to);
                db.AddInParameter(cmdGetProductCommissionDetails, "@issueId", DbType.Int32, issueId);
                dtProductiontCommissionDetails = db.ExecuteDataSet(cmdGetProductCommissionDetails).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionReceivable.cs:getProductCommissionReceivable(string product,string productCategory,int amcCode,int schemeId,int adviserId,int issueId,DateTime fromDate,DateTime toDate)");
                object[] objects = new object[9];
                objects[0] = product;
                objects[1] = productCategory;
                objects[2] = amcCode;
                objects[3] = schemeId;
                objects[4] = issueId;
                objects[5] = adviserId;
                objects[6] = schemeId;
                objects[7] = from;
                objects[8] = to;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtProductiontCommissionDetails;
        }
        public bool DeleteMappedIssue(int structureId)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand DeleteMappedIssueCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteMappedIssueCmd = db.GetStoredProcCommand("SPROC_DeleteStructureMappedAssociates");
                db.AddInParameter(DeleteMappedIssueCmd, "@CommissionStructureId", DbType.Int32, structureId);

                if (db.ExecuteNonQuery(DeleteMappedIssueCmd) != 0)
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:DeleteIssueinvestor(int investorcategoryid)");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }
        public bool CreateNewRuleAndRate(DataTable dtBrokerRuleRate, int structureId, int userId)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCreateNewRuleAndRate;
            DataSet dsCreateNewRuleAndRate = new DataSet();

            try
            {
                dsCreateNewRuleAndRate.Tables.Add(dtBrokerRuleRate.Copy());
                dsCreateNewRuleAndRate.DataSetName = "BrokerRuleRateDS";
                dsCreateNewRuleAndRate.Tables[0].TableName = "BrokerRuleRateDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCreateNewRuleAndRate = db.GetStoredProcCommand("SPROC_CreateMFNCDIPOBrokrageRuleRate");
                db.AddInParameter(dbCreateNewRuleAndRate, "@StructureId", DbType.Int32, structureId);
                db.AddInParameter(dbCreateNewRuleAndRate, "@BrokrageTable", DbType.Xml, dsCreateNewRuleAndRate.GetXml().ToString());
                db.AddInParameter(dbCreateNewRuleAndRate, "@userId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(dbCreateNewRuleAndRate) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public int RuleIsCreated(int structureId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdRuleIsCreated;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdRuleIsCreated = db.GetStoredProcCommand("SPROC_GetRuleAndRateCount");
                db.AddInParameter(cmdRuleIsCreated, "@StructureId", DbType.Int32, structureId);
                db.AddOutParameter(cmdRuleIsCreated, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdRuleIsCreated);
                if (db.ExecuteNonQuery(cmdRuleIsCreated) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdRuleIsCreated, "count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return count;
        }
         public bool UpdateRuleRateandTax(int ruleid,decimal serviceTax,decimal TDSValue,decimal recivablerate,decimal payablerate,string brokrageunit, int userId)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCreateNewRuleAndRate;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCreateNewRuleAndRate = db.GetStoredProcCommand("SPROC_UpdateRuleForBrokrageStructure");
                db.AddInParameter(dbCreateNewRuleAndRate, "@ruleId", DbType.Int32, ruleid);
                db.AddInParameter(dbCreateNewRuleAndRate, "@reciviablerate", DbType.Decimal, recivablerate);
                db.AddInParameter(dbCreateNewRuleAndRate, "@payablerate", DbType.Decimal, payablerate);
                db.AddInParameter(dbCreateNewRuleAndRate, "@brokrageUnit", DbType.String, brokrageunit);
                db.AddInParameter(dbCreateNewRuleAndRate, "@ServiceTaxValue", DbType.Decimal, serviceTax);
                db.AddInParameter(dbCreateNewRuleAndRate, "@reducetax", DbType.Decimal, TDSValue);
                db.AddInParameter(dbCreateNewRuleAndRate, "@userId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(dbCreateNewRuleAndRate) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        
    }
}
