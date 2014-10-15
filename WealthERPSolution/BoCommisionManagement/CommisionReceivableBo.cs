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
using DaoCommisionManagement;

namespace BoCommisionManagement
{
    public class CommisionReceivableBo
    {

        public DataSet GetLookupDataForReceivableSetUP(int adviserId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetLookupDataForReceivableSetUP(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetProduct(int adviserId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetProduct(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetIssuesStructureMapings(int adviserId, string mappedType, string issueType, string product)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetIssuesStructureMapings(adviserId, mappedType, issueType, product);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }


        public void CreateIssuesStructureMapings(CommissionStructureRuleVo commissionStructureRuleVo, out  int instructureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();

            try
            {
                commisionReceivableDao.CreateIssuesStructureMapings(commissionStructureRuleVo, out instructureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:CreateCommissionStructureMastter(int adviserId)");
                object[] objects = new object[2];              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId, out int structureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();

            try
            {
                commisionReceivableDao.CreateCommissionStructureMastter(commissionStructureMasterVo, userId, out structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:CreateCommissionStructureMastter(int adviserId)");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = commissionStructureMasterVo.AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsCommissionStructureRules;
            try
            {
                dsCommissionStructureRules = commisionReceivableDao.GetAdviserCommissionStructureRules(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, int structureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsCommissionStructureRules;
            try
            {
                dsCommissionStructureRules = commisionReceivableDao.GetAdviserCommissionStructureRules(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }

        public void CreateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId, string ruleHash)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.CreateCommissionStructureRule(commissionStructureRuleVo, userId, ruleHash);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:CreateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo)");
                object[] objects = new object[2];
                objects[0] = commissionStructureRuleVo.CommissionStructureId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetStructureCommissionAllRules(int structureId, string commissionType)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsCommissionRules;
            try
            {
                dsCommissionRules = commisionReceivableDao.GetStructureCommissionAllRules(structureId, commissionType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetStructureCommissionAllRules(int structureId, string commissionType)");
                object[] objects = new object[1];
                objects[0] = structureId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionRules;
        }


        public DataSet GetProductType()
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetProductType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementBo.cs:GetProductType()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetCategories(String prodType)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetCategories(prodType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementBo.cs:GetCategories(string prodType)");
                object[] objects = new object[1];
                objects[0] = prodType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetSubCategories(String cat)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetSubCategories(cat);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementBo.cs:GetSubCategories(string cat)");
                object[] objects = new object[1];
                objects[0] = cat;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetProdAmc()
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetProdAmc();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementBo.cs:GetProdAmc()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetProdAmc(int amcCode)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = commisionReceivableDao.GetProdAmc(amcCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementBo.cs:GetProdAmc()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, string product, string cat, string subcat, int issuer, string validity)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsCommissionStructureRules;
            try
            {
                dsCommissionStructureRules = commisionReceivableDao.GetAdviserCommissionStructureRules(adviserId, product, cat, subcat, issuer, validity);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }

        public CommissionStructureMasterVo GetCommissionStructureMaster(int structureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            CommissionStructureMasterVo commissionStructureMasterVo = new CommissionStructureMasterVo();
            try
            {
                commissionStructureMasterVo = commisionReceivableDao.GetCommissionStructureMaster(structureId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetCommissionStructureMaster(int structureId)");
                object[] objects = new object[1];
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
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();

            try
            {
                commisionReceivableDao.UpdateCommissionStructureMastter(commissionStructureMasterVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:UpdateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId)");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = commissionStructureMasterVo.CommissionStructureId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId, string strRuleHash)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.UpdateCommissionStructureRule(commissionStructureRuleVo, userId, strRuleHash);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo)");
                object[] objects = new object[2];
                objects[0] = commissionStructureRuleVo.CommissionStructureRuleId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public void DeleteIssueMapping(int issueId )
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.DeleteIssueMapping(issueId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void DeleteCommissionStructureRule(int id, bool isAllRuleDelete)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.DeleteCommissionStructureRule(id, isAllRuleDelete);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:DeleteCommissionStructureRule(int id, bool isAllRuleDelete)");
                object[] objects = new object[2];
                objects[0] = id;
                objects[1] = isAllRuleDelete;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetCMStructNames(int advId, int cmStructId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsStructList;
            try
            {
                dsStructList = commisionReceivableDao.GetCMStructNames(advId, cmStructId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetCMStructNames(int cmStructId)");
                object[] objects = new object[1];
                objects[0] = cmStructId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructList;
        }

        public DataSet GetMappedSchemes(int cmStructId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsStructList;
            try
            {
                dsStructList = commisionReceivableDao.GetMappedSchemes(cmStructId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetMappedSchemes(int cmStructId)");
                object[] objects = new object[1];
                objects[0] = cmStructId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructList;
        }

        //DataSet GetAvailSchemes(int structId, int issuer, string prodType, string cat, string subCat)

        public DataSet GetAvailSchemes(int adviserId, int structid, int issuer, string prodType, string cat, string subCat, DateTime from, DateTime till)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsStructList;
            try
            {
                dsStructList = commisionReceivableDao.GetAvailSchemes(adviserId, structid, issuer, prodType, cat, subCat, from, till);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetAvailSchemes(int issuer, string prodType, string cat, string subCat)");
                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = issuer;
                objects[2] = prodType;
                objects[3] = cat;
                objects[4] = subCat;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructList;
        }

        /**
         * CommissionStructureToSchemeMapping - 
         */

        public DataSet GetStructureDetails(int adviserId, int structureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsStructList;
            try
            {
                dsStructList = commisionReceivableDao.GetStructureDetails(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetStructureDetails(int adviserId, int structureId)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = structureId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructList;
        }

        public DataSet GetSubcategories(int adviserId, int structureId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsStructList;
            try
            {
                dsStructList = commisionReceivableDao.GetSubcategories(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetSubcategories(int adviserId, int structureId)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = structureId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStructList;
        }

        public void MapSchemesToStructres(int structId, int schemeId, DateTime validFrom, DateTime validTill)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            //DataSet dsStructList;
            try
            {
                commisionReceivableDao.MapSchemesToStructres(structId, schemeId, validFrom, validTill);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:MapSchemesToStructres(int structId, int schemeId, DateTime validFrom, DateTime validTill)");
                object[] objects = new object[4];
                objects[0] = structId;
                objects[1] = schemeId;
                objects[2] = validFrom;
                objects[3] = validTill;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //return dsStructList;
        }

        public bool checkSchemeAssociationExists(int schemeId, int structId, DateTime validFrom, DateTime validTo)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                int nRows = commisionReceivableDao.checkSchemeAssociationExists(schemeId, structId, validFrom, validTo);
                if (nRows > 0) { return true; }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:checkSchemeAssociationExists(int schemeId, int structId, DateTime validFrom, DateTime validTo)");
                object[] objects = new object[4];
                objects[0] = schemeId;
                objects[1] = structId;
                objects[2] = validFrom;
                objects[3] = validTo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return false;
        }

        public bool checkSchemeAssociationExists(int setupId, DateTime validFrom, DateTime validTo)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                int nRows = commisionReceivableDao.checkSchemeAssociationExists(setupId, validFrom, validTo);
                if (nRows > 0) { return true; }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:checkSchemeAssociationExists(int setupId, DateTime validFrom, DateTime validTo)");
                object[] objects = new object[3];
                objects[0] = setupId;
                objects[1] = validFrom;
                objects[2] = validTo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return false;
        }

        public int updateStructureToSchemeMapping(int setupId, DateTime validTill)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                return commisionReceivableDao.updateStructureToSchemeMapping(setupId, validTill);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:updateStructureToSchemeMapping(int setupId, DateTime validTill)");
                object[] objects = new object[2];
                objects[0] = setupId;
                objects[1] = validTill;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet GetStructureScheme(int adviserId,string product)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                return commisionReceivableDao.GetStructureScheme(adviserId, product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetStructureScheme(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet GetCommissionSchemeStructureRuleList(int adviserId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsSchemeStructureRule;
            try
            {
                dsSchemeStructureRule = commisionReceivableDao.GetCommissionSchemeStructureRuleList(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:GetCommissionSchemeStructureRuleList(int adviserId)");
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
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.deleteStructureToSchemeMapping(setupId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableBo.cs:deleteStructureToSchemeMapping(int setupId)");
                object[] objects = new object[1];
                objects[0] = setupId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public string GetHash(CommissionStructureRuleVo voComStrRule)
        {
            string strRule = "";

            if (voComStrRule.AUMFrequency != null) strRule += voComStrRule.AUMFrequency.ToString();
            if (voComStrRule.AUMMonth != null) strRule += voComStrRule.AUMMonth.ToString();
            if (voComStrRule.AdviserCityGroupCode != null) strRule += voComStrRule.AdviserCityGroupCode.ToString();
            if (voComStrRule.AdviserId != null) strRule += voComStrRule.AdviserId.ToString();
            if (voComStrRule.ApplicableLevelCode != null) strRule += voComStrRule.ApplicableLevelCode.ToString();
            if (voComStrRule.ArchivedOn != null) strRule += voComStrRule.ArchivedOn.ToString();
            if (voComStrRule.AssetCategory != null) strRule += voComStrRule.AssetCategory.ToString();
            if (voComStrRule.AssetSubCategory != null) strRule += voComStrRule.AssetSubCategory.ToString();
            if (voComStrRule.BrokerageUnitCode != null) strRule += voComStrRule.BrokerageUnitCode.ToString();
            if (voComStrRule.BrokerageValue != null) strRule += voComStrRule.BrokerageValue.ToString();
            if (voComStrRule.CalculatedOnCode != null) strRule += voComStrRule.CalculatedOnCode.ToString();
            if (voComStrRule.CommissionStructureId != null) strRule += voComStrRule.CommissionStructureId.ToString();
            if (voComStrRule.CommissionStructureName != null) strRule += voComStrRule.CommissionStructureName.ToString();
            if (voComStrRule.CommissionStructureRuleId != null) strRule += voComStrRule.CommissionStructureRuleId.ToString();
            if (voComStrRule.CommissionType != null) strRule += voComStrRule.CommissionType.ToString();
            if (voComStrRule.CustomerType != null) strRule += voComStrRule.CustomerType.ToString();
            if (voComStrRule.InvestmentAgeUnit != null) strRule += voComStrRule.InvestmentAgeUnit.ToString();
            if (voComStrRule.IsArchived != null) strRule += voComStrRule.IsArchived.ToString();
            if (voComStrRule.IsClawBackApplicable != null) strRule += voComStrRule.IsClawBackApplicable.ToString();
            if (voComStrRule.IsNonMonetaryReward != null) strRule += voComStrRule.IsNonMonetaryReward.ToString();
            if (voComStrRule.IsOtherTaxReduced != null) strRule += voComStrRule.IsOtherTaxReduced.ToString();
            if (voComStrRule.IsServiceTaxReduced != null) strRule += voComStrRule.IsServiceTaxReduced.ToString();
            if (voComStrRule.IsTDSReduced != null) strRule += voComStrRule.IsTDSReduced.ToString();
            if (voComStrRule.Issuer != null) strRule += voComStrRule.Issuer.ToString();
            if (voComStrRule.MaxInvestmentAge != null) strRule += voComStrRule.MaxInvestmentAge.ToString();
            if (voComStrRule.MaxInvestmentAmount != null) strRule += voComStrRule.MaxInvestmentAmount.ToString();
            if (voComStrRule.MinInvestmentAge != null) strRule += voComStrRule.MinInvestmentAge.ToString();
            if (voComStrRule.MinInvestmentAmount != null) strRule += voComStrRule.MinInvestmentAmount.ToString();
            if (voComStrRule.MinNumberofApplications != null) strRule += voComStrRule.MinNumberofApplications.ToString();
            if (voComStrRule.ProductType != null) strRule += voComStrRule.ProductType.ToString();
            if (voComStrRule.ReceivableFrequency != null) strRule += voComStrRule.ReceivableFrequency.ToString();
            if (voComStrRule.RuleCreatedBy != null) strRule += voComStrRule.RuleCreatedBy.ToString();
            if (voComStrRule.RuleCreatedOn != null) strRule += voComStrRule.RuleCreatedOn.ToString();
            if (voComStrRule.RuleModifiedBy != null) strRule += voComStrRule.RuleModifiedBy.ToString();
            if (voComStrRule.RuleModifiedOn != null) strRule += voComStrRule.RuleModifiedOn.ToString();
            if (voComStrRule.SIPFrequency != null) strRule += voComStrRule.SIPFrequency.ToString();
            if (voComStrRule.StructureMasterCreatedBy != null) strRule += voComStrRule.StructureMasterCreatedBy.ToString();
            if (voComStrRule.StructureMasterCreatedOn != null) strRule += voComStrRule.StructureMasterCreatedOn.ToString();
            if (voComStrRule.StructureMasterModifiedBy != null) strRule += voComStrRule.StructureMasterModifiedBy.ToString();
            if (voComStrRule.StructureMasterModifiedOn != null) strRule += voComStrRule.StructureMasterModifiedOn.ToString();
            if (voComStrRule.StructureNote != null) strRule += voComStrRule.StructureNote.ToString();
            if (voComStrRule.StructureRuleComment != null) strRule += voComStrRule.StructureRuleComment.ToString();
            if (voComStrRule.TenureMax != null) strRule += voComStrRule.TenureMax.ToString();
            if (voComStrRule.TenureMin != null) strRule += voComStrRule.TenureMin.ToString();
            if (voComStrRule.TenureUnit != null) strRule += voComStrRule.TenureUnit.ToString();
            if (voComStrRule.TransactionType != null) strRule += voComStrRule.TransactionType.ToString();
            if (voComStrRule.ValidityEndDate != null) strRule += voComStrRule.ValidityEndDate.ToString();
            if (voComStrRule.ValidityStartDate != null) strRule += voComStrRule.ValidityStartDate.ToString();

            char[] chData = strRule.ToCharArray();
            byte[] byData = new byte[chData.Length];

            for (int i = 0; i < chData.Length; i++) byData[i] = (byte)chData[i];

            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            return HexStringFromBytes(sha1.ComputeHash(byData));
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        private string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public bool hasRule(CommissionStructureRuleVo commRule, string ruleHash)
        {
            CommisionReceivableDao daoCommRec = new CommisionReceivableDao();
            return daoCommRec.hasRule(commRule.AdviserId, ruleHash);
        }
    }
}
