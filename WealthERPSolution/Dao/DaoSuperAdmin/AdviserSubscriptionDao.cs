using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoSuperAdmin;


namespace DaoSuperAdmin
{
    public class AdviserSubscriptionDao
    {

        /// <summary>
        /// Function to retrieve the WERP plans 
        /// </summary>
        /// <param name="XMLFileTypeId"></param>
        /// <returns></returns>
        public DataSet GetWerpPlans()
        {
            DataSet getWerpPlansDs = new DataSet();
            Database db;
            DbCommand getWerpPlansCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWerpPlansCmd = db.GetStoredProcCommand("SP_GetWerpPlans");
                getWerpPlansDs = db.ExecuteDataSet(getWerpPlansCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserSubscriptionDao.cs:GetWerpPlans()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getWerpPlansDs;
        }

        /// <summary>
        /// Function to retrieve the flavours(the modules associated) for a plan
        /// </summary>
        /// <returns></returns>
        public DataSet GetWerpPlanFlavours(int planId)
        {
            DataSet getWerpPlanFlavoursDs = new DataSet();
            Database db;
            DbCommand getWerpPlanFlavoursCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWerpPlanFlavoursCmd = db.GetStoredProcCommand("SP_GetWerpPlanFlavours");
                db.AddInParameter(getWerpPlanFlavoursCmd, "@WP_PlanId", DbType.Int16, planId);
                getWerpPlanFlavoursDs = db.ExecuteDataSet(getWerpPlanFlavoursCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserSubscriptionDao.cs:GetWerpPlanFlavours()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getWerpPlanFlavoursDs;
        }

        /// <summary>
        /// Function to create a new Adviser Subscription
        /// </summary>
        /// <param name="adviserSubscriptionVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CreateAdviserSubscription(AdviserSubscriptionVo adviserSubscriptionVo, int userId)
        {

            int subscriptionId = 0;
            Database db;
            DbCommand createAdviserSubscriptionCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserSubscriptionCmd = db.GetStoredProcCommand("SP_CreateAdviserSubscription");

                db.AddInParameter(createAdviserSubscriptionCmd, "@A_AdviserId", DbType.Int32, adviserSubscriptionVo.AdviserId);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_TrialStartDate", DbType.DateTime, adviserSubscriptionVo.TrialStartDate);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_TrialEndDate", DbType.DateTime, adviserSubscriptionVo.TrialEndDate);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_SubscriptionStartDate", DbType.DateTime, adviserSubscriptionVo.StartDate);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_SubscriptionEndDate", DbType.DateTime, adviserSubscriptionVo.EndDate);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_SMSLicences", DbType.Int32, adviserSubscriptionVo.SmsBought);
                //db.AddInParameter(createAdviserSubscriptionCmd, "@XST_SubscriptionTypeId", DbType.Int32, rmVo.OfficePhoneDirectStd);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_NoOfStaffWebLogins", DbType.Int32, adviserSubscriptionVo.NoOfStaffLogins);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_NoOfBranches", DbType.Int32, adviserSubscriptionVo.NoOfBranches);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_NoOfCustomerWebLogins", DbType.Int32, adviserSubscriptionVo.NoOfCustomerLogins);
                //db.AddInParameter(createAdviserSubscriptionCmd, "@AS_IsDeactivated", DbType.Int32, rmVo.OfficePhoneExtStd);
                //db.AddInParameter(createAdviserSubscriptionCmd, "@AS_DeactivationDate", DbType.Int32, rmVo.OfficePhoneExtNumber);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_Comments", DbType.String, adviserSubscriptionVo.Comments);
                db.AddInParameter(createAdviserSubscriptionCmd, "@WP_PlanId", DbType.String, 3);
                db.AddInParameter(createAdviserSubscriptionCmd, "@WFC_FlavourCategoryCode", DbType.String, adviserSubscriptionVo.FlavourCategory);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_StorageSize", DbType.Double, adviserSubscriptionVo.StorageSize);
                db.AddInParameter(createAdviserSubscriptionCmd, "@AS_StorageBalance", DbType.Double, adviserSubscriptionVo.StorageBalance);


                db.AddOutParameter(createAdviserSubscriptionCmd, "@AS_AdviserSubscriptionId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createAdviserSubscriptionCmd) != 0)

                    subscriptionId = int.Parse(db.GetParameterValue(createAdviserSubscriptionCmd, "AS_AdviserSubscriptionId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionDao.cs:CreateAdviserSubscription()");


                object[] objects = new object[2];
                objects[0] = adviserSubscriptionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return subscriptionId;
        }

        /// <summary>
        /// Function to enter Adviser Subscription Plan details into the AdviserPlanDetails table
        /// </summary>
        /// <param name="adviserSubscriptionVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CreateAdviserSubscriptionPlans(AdviserSubscriptionVo adviserSubscriptionVo, int userId)
        {

            Database db;
            DbCommand createAdviserSubscriptionPlansCmd;
            bool bResult = false;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserSubscriptionPlansCmd = db.GetStoredProcCommand("SP_CreateAdviserSubscriptionPlans");

                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@AS_AdviserSubscriptionId", DbType.Int32, adviserSubscriptionVo.SubscriptionId);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@WP_PlanId", DbType.Int32, adviserSubscriptionVo.PlanId);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@APD_PlanStartDate", DbType.DateTime, adviserSubscriptionVo.StartDate);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@APD_PlanEndDate", DbType.DateTime, adviserSubscriptionVo.EndDate);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@APD_IsActive", DbType.Int16, adviserSubscriptionVo.IsActive);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@APD_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdviserSubscriptionPlansCmd, "@APD_ModifiedBy", DbType.Int32, userId);

                if (db.ExecuteNonQuery(createAdviserSubscriptionPlansCmd) != 0)
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

                FunctionInfo.Add("Method", "AdviserSubscriptionDao.cs:CreateAdviserSubscriptionPlans()");


                object[] objects = new object[2];
                objects[0] = adviserSubscriptionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to enter Adviser Subscription Flavour details into the AdviserFlavourDetails table
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="allModulesSelected"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CreateAdviserSubscriptionFlavours(int adviserId,string allModulesSelected, int userId)
        {

            Database db;
            DbCommand createAdviserSubscriptionFlavoursCmd;
            bool bResult = false;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserSubscriptionFlavoursCmd = db.GetStoredProcCommand("SP_CreateAdviserSubscriptionFlavours");

                db.AddInParameter(createAdviserSubscriptionFlavoursCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(createAdviserSubscriptionFlavoursCmd, "@allFlavoursSelected", DbType.String, allModulesSelected);
                db.AddInParameter(createAdviserSubscriptionFlavoursCmd, "@AFD_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdviserSubscriptionFlavoursCmd, "@AFD_ModifiedBy", DbType.Int32, userId);


                if (db.ExecuteNonQuery(createAdviserSubscriptionFlavoursCmd) != 0)
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

                FunctionInfo.Add("Method", "AdviserSubscriptionDao.cs:CreateAdviserSubscriptionFlavours()");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = userId;
                objects[2] = allModulesSelected;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to retrieve all the Adviser subscription details and Plan
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserSubscriptionPlanDetails(int adviserId)
        {
            Database db;
            DbCommand SubscriptionPlanCmd;
            DataSet dsSubscriptionPlan = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SubscriptionPlanCmd = db.GetStoredProcCommand("SP_GetAdviserSubscriptionPlanDetails");
                db.AddInParameter(SubscriptionPlanCmd, "@A_AdviserId", DbType.Int32, adviserId);
                dsSubscriptionPlan = db.ExecuteDataSet(SubscriptionPlanCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorSubscriptionDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSubscriptionPlan;

        }

        /// <summary>
        /// Function to retrieve the Adviser flavour details
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserSubscriptionFlavourDetails(int adviserId)
        {
            Database db;
            DbCommand SubscriptionFlavourCmd;
            DataSet dsSubscriptionFlavour = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SubscriptionFlavourCmd = db.GetStoredProcCommand("SP_GetAdviserSubscriptionFlavourDetails");
                db.AddInParameter(SubscriptionFlavourCmd, "@A_AdviserId", DbType.Int32, adviserId);
                dsSubscriptionFlavour = db.ExecuteDataSet(SubscriptionFlavourCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorSubscriptionDao.cs:GetAdviserSubscriptionFlavourDetails()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSubscriptionFlavour;

        }

        public void SetFlavoursToAdviser(string flavourIds, int adviserId)
        {
            DataSet dsSetFlovoursToAdviser;
            Database db;
            DbCommand FlovoursToAdviserCmd= null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                FlovoursToAdviserCmd = db.GetStoredProcCommand("SP_SetFlavourToAdviser");
                db.AddInParameter(FlovoursToAdviserCmd, "@FlavourIds", DbType.String, flavourIds);
                db.AddInParameter(FlovoursToAdviserCmd, "@AdviserId", DbType.Int32, adviserId);
                dsSetFlovoursToAdviser = db.ExecuteDataSet(FlovoursToAdviserCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorSubscriptionDao.cs:SetFlavoursToAdviser()");
                object[] objects = new object[2];
                objects[0] = flavourIds;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}
