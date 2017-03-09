using System;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoAdvisorProfiling;
using VoAdvisorProfiling;

namespace BoAdvisorProfiling
{
    /// <summary>
    /// 
    /// </summary>
    public class AdviserAssociateCategorySetupBo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserAssociateCategory(int AdviserId)
        {
            DataSet ds = null;

            try
            {
                
                AdviserAssociateCategorySetupDao AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupDao();
                ds = AdviserAssociateCategorySetup.GetAdviserAssociateCategory(AdviserId);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupBo.cs:GetAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssociateCategory"></param>
        /// <returns></returns>
        public bool UpdateAdviserAssociateCategory(AssociateCategoryVo AssociateCategory )
        {
            bool result  = false;

            try
            {
                
                AdviserAssociateCategorySetupDao AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupDao();
                result = AdviserAssociateCategorySetup.UpdateAdviserAssociateCategory(AssociateCategory);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupBo.cs:UpdateAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AssociateCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssociateCategory"></param>
        /// <returns></returns>

        public bool InsertAdviserAssociateCategory(AssociateCategoryVo AssociateCategory )
        {
            bool result  = false;

            try
            {
                
                AdviserAssociateCategorySetupDao AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupDao();
                result = AdviserAssociateCategorySetup.InsertAdviserAssociateCategory(AssociateCategory);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupBo.cs:InsertAdviserAssociateCategory()");
                object[] objects = new object[1];
                objects[0] = AssociateCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssociateCategoryId"></param>
        /// <returns></returns>
        public bool DeleteAdviserAssociateCategory(int AssociateCategoryId)
        {
            bool result = false;

            try
            {

                AdviserAssociateCategorySetupDao AdviserAssociateCategorySetup = new AdviserAssociateCategorySetupDao();
                result = AdviserAssociateCategorySetup.DeleteAdviserAssociateCategory(AssociateCategoryId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAssociateCategorySetupBo.cs:DeleteAdviserAssociateCategory()");
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
