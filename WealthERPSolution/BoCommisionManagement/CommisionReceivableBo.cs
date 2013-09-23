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

        public void CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId,out int structureId)
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

        public void CreateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.CreateCommissionStructureRule(commissionStructureRuleVo, userId);
                
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

        public void UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                commisionReceivableDao.UpdateCommissionStructureRule(commissionStructureRuleVo, userId);

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

        public DataSet GetStructureScheme(int adviserId)
        {
            CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            try
            {
                return commisionReceivableDao.GetStructureScheme(adviserId);
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
    }
}
