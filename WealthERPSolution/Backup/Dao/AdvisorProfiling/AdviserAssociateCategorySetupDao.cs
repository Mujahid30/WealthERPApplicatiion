using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;

namespace DaoAdvisorProfiling
{
    public class AdviserAssociateCategorySetupDao
    {
        public DataSet GetAdviserAssociateCategory(int AdviserId)
        {
            DataSet ds = null;

            Database db;
            DbCommand getAdvAssocCategory;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvAssocCategory = db.GetStoredProcCommand("SP_GetAdviserAssociateCategory");
                db.AddInParameter(getAdvAssocCategory, "@A_AdviserId", DbType.Int32, AdviserId);

                ds = db.ExecuteDataSet(getAdvAssocCategory);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupDao.cs:GetAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;


        }

        //Update the Associate Category table

        public bool UpdateAdviserAssociateCategory(AssociateCategoryVo AssociateCategory)
        {
            bool result = false;

            Database db;
            DbCommand cmdUpdateAdvAssocCat;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateAdvAssocCat = db.GetStoredProcCommand("SP_UpdateAdviserAssociateCategory");

                if (AssociateCategory.AssociateCategoryId == 0)
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_AssociateCategoryId", DbType.Int32, DBNull.Value);   
                else
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_AssociateCategoryId", DbType.Int32, AssociateCategory.AssociateCategoryId);

                db.AddInParameter(cmdUpdateAdvAssocCat, "@A_AdviserId ", DbType.Int32, AssociateCategory.AdviserId);
                db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_AssociateCategoryCode", DbType.String, AssociateCategory.AssociateCategoryCode);
                db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_AssociateCategoryName", DbType.String, AssociateCategory.AssociateCategoryName);

                if (AssociateCategory.CreatedOn == DateTime.MinValue)
                {
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_CreatedBy", DbType.Int32, DBNull.Value);
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_CreatedOn", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_CreatedBy", DbType.Int32, AssociateCategory.CreatedBy);
                    db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_CreatedOn", DbType.DateTime, AssociateCategory.CreatedOn);
                }
                db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_ModifiedBy", DbType.Int32, AssociateCategory.ModifiedBy);
                db.AddInParameter(cmdUpdateAdvAssocCat, "@AAC_ModifiedOn", DbType.DateTime, AssociateCategory.Modifiedon);

                db.ExecuteNonQuery(cmdUpdateAdvAssocCat);
                result = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupDao.cs:UpdateAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AssociateCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }

        public bool InsertAdviserAssociateCategory(AssociateCategoryVo AssociateCategory)
        {
            bool result = false;

            Database db;
            DbCommand cmdInsertAdvAssocCat;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertAdvAssocCat = db.GetStoredProcCommand("SP_CreateAdviserAssociateCategory");


                db.AddInParameter(cmdInsertAdvAssocCat, "@A_AdviserId ", DbType.Int32, AssociateCategory.AdviserId);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_AssociateCategoryCode", DbType.String, AssociateCategory.AssociateCategoryCode);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_AssociateCategoryName", DbType.String, AssociateCategory.AssociateCategoryName);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_CreatedBy", DbType.Int32, AssociateCategory.CreatedBy);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_CreatedOn", DbType.DateTime, AssociateCategory.CreatedOn);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_ModifiedBy", DbType.Int32, AssociateCategory.ModifiedBy);
                db.AddInParameter(cmdInsertAdvAssocCat, "@AAC_ModifiedOn", DbType.DateTime, AssociateCategory.Modifiedon);

                db.ExecuteNonQuery(cmdInsertAdvAssocCat);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupDao.cs:InsertAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AssociateCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }

        public bool DeleteAdviserAssociateCategory(int AssociateCategoryId)
        {
            bool result = false;

            Database db;
            DbCommand cmdDelete;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDelete = db.GetStoredProcCommand("SP_DeleteAdviserAssociateCategoryDelete");
                db.AddInParameter(cmdDelete, "@AAC_AssociateCategoryId", DbType.Int32, AssociateCategoryId);

                db.ExecuteNonQuery(cmdDelete);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupDao.cs:SP_DeleteAdviserAssociateCategoryDelete()");
                object[] objects = new object[1];
                objects[0] = AssociateCategoryId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
    }

}
