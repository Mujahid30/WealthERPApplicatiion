using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using DaoSuperAdmin;
using VoSuperAdmin;

namespace BoSuperAdmin
{
    public class AdviserSubscriptionBo
    {
        /// <summary>
        /// Function to retrieve the WERP plans
        /// </summary>
        /// <returns></returns>
        public DataSet GetWerpPlans()
        {
            DataSet ds = new DataSet();
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                ds = adviserSubscriptionDao.GetWerpPlans();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:GetWerpPlans()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /// <summary>
        /// Function to retrieve the flavours(the modules associated) for a plan
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public DataSet GetWerpPlanFlavours(int planId)
        {
            DataSet ds = new DataSet();
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                ds = adviserSubscriptionDao.GetWerpPlanFlavours(planId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:GetWerpPlanFlavours()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        
        /// <summary>
        /// Function to create a new Adviser Subscription
        /// </summary>
        /// <param name="adviserSubscriptionVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CreateAdviserSubscription(AdviserSubscriptionVo adviserSubscriptionVo,int userId)
        {
            int subscriptionId = 0;
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                subscriptionId = adviserSubscriptionDao.CreateAdviserSubscription(adviserSubscriptionVo,userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:GetWerpPlanFlavours()");

                object[] objects = new object[0];

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
            bool bResult = false;
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                bResult = adviserSubscriptionDao.CreateAdviserSubscriptionPlans(adviserSubscriptionVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:CreateAdviserSubscriptionPlans()");

                object[] objects = new object[0];

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
        public bool CreateAdviserSubscriptionFlavours(int adviserId, string allModulesSelected, int userId)
        {
            bool bResult = false;
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                bResult = adviserSubscriptionDao.CreateAdviserSubscriptionFlavours(adviserId,allModulesSelected, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:CreateAdviserSubscriptionFlavours()");

                object[] objects = new object[0];

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
            DataSet ds = new DataSet();
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                ds = adviserSubscriptionDao.GetAdviserSubscriptionPlanDetails(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:GetAdviserSubscriptionPlanDetails()");


                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;


        }

        /// <summary>
        /// Function to retrieve the Adviser flavour details
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserSubscriptionFlavourDetails(int adviserId)
        {
            DataSet ds = new DataSet();
            AdviserSubscriptionDao adviserSubscriptionDao = new AdviserSubscriptionDao();
            try
            {
                ds = adviserSubscriptionDao.GetAdviserSubscriptionFlavourDetails(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserSubscriptionBo.cs:GetAdviserSubscriptionFlavourDetails()");


                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;


        }

    }
}
