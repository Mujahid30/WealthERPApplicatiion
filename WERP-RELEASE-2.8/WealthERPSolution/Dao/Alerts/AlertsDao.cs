﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VoAlerts;

namespace DaoAlerts
{
    public class AlertsDao
    {    

        #region NewAlertSetup

        /// <summary>
        /// Shows all the different types of Alerts available in the 
        /// system to select from for the User to subscribe
        /// </summary>
        /// <returns> Returns a Dataset of the Necessary Alert Details to show onscreen</returns>
        public DataSet GetSystemAlerts(int CurrentPage, out int count)
        {
            DataSet dsAlertList = new DataSet();
            Database db;
            DbCommand getListOfAlertsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getListOfAlertsCmd = db.GetStoredProcCommand("SP_GetSystemAlerts");
                db.AddInParameter(getListOfAlertsCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                dsAlertList = db.ExecuteDataSet(getListOfAlertsCmd);

                if (dsAlertList.Tables[1] != null && dsAlertList.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsAlertList.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetSystemAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAlertList;
        }

        /// <summary>
        /// Gets all the details of the already subscribed Alerts of a particular type
        /// for a particular Customer
        /// </summary>
        /// <param name="customerId"> The customer identifier for whom the Alerts have to be fetched </param>
        /// <param name="eventId">Determines the subtype of Alert that has to be fetched</param>
        /// <param name="alertType">Determines the Fields that need to be fetched</param>
        /// <returns>Returns a List of Subscribed alerts</returns>
        public List<AlertsSetupVo> GetCustomerSubscribedConfirmationAlerts(int customerId, int eventId, string alertType, int CurrentPage, out int count)
        {
            DataSet dsSubscribedEventList = new DataSet();
            List<AlertsSetupVo> customerSubscribedList = null;
            AlertsSetupVo alertVo;
            Database db;
            DbCommand getSubscribedEventListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSubscribedEventListCmd = db.GetStoredProcCommand("SP_GetCustomerSubscribedConfirmationAlerts");
                db.AddInParameter(getSubscribedEventListCmd, "@AES_CreatedFor", DbType.Int32, customerId);
                db.AddInParameter(getSubscribedEventListCmd, "@AEL_EventID", DbType.Int16, eventId);
                db.AddInParameter(getSubscribedEventListCmd, "@AlertType", DbType.String, alertType);
                db.AddInParameter(getSubscribedEventListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                //db.AddInParameter(getSubscribedEventListCmd, "@SortOrder", DbType.String, sortOrder);

                dsSubscribedEventList = db.ExecuteDataSet(getSubscribedEventListCmd);
                if (dsSubscribedEventList.Tables[1] != null && dsSubscribedEventList.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsSubscribedEventList.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsSubscribedEventList.Tables[0].Rows.Count > 0)
                {
                    customerSubscribedList = new List<AlertsSetupVo>();

                    foreach (DataRow dr in dsSubscribedEventList.Tables[0].Rows)
                    {

                        alertVo = new AlertsSetupVo();

                        alertVo.EventSetupID = Int64.Parse(dr["AES_EventSetupID"].ToString());
                        alertVo.EventID = Int32.Parse(dr["AEL_EventID"].ToString());
                        alertVo.CustomerId = Int32.Parse(dr["AES_TargetID"].ToString());
                        //alertVo.DeliveryMode = dr["AES_DeliveryMode"].ToString();

                        //if (dr["AES_EndDate"].ToString() != "")
                        //{
                        //    alertVo.EndDate = DateTime.Parse(dr[""].ToString());
                        //}

                        if (dr["AES_EventMessage"].ToString() != "")
                        {
                            alertVo.EventMessage = dr["AES_EventMessage"].ToString();
                        }

                        //if (dr["CL_CycleID"].ToString() != "")
                        //{
                        //    alertVo.FrequencyID = Int16.Parse(dr["CL_CycleID"].ToString());
                        //}

                        if (dr["AEL_EventCode"].ToString() != "")
                        {
                            alertVo.EventName = dr["AEL_EventCode"].ToString();
                        }

                        //alertVo.Reminder = dr["AEL_Reminder"].ToString();
                        //alertVo.EventSubscriptionDate = DateTime.Parse(dr["AES_EventSubscriptionDate"].ToString());
                        //if (dr["AES_NextOccurence"].ToString() != "")
                        //    alertVo.NextOccurence = DateTime.Parse(dr["AES_NextOccurence"].ToString());
                        //if (dr["AES_LastOccurence"].ToString() != "")
                        //    alertVo.LastOccurence = DateTime.Parse(dr["AES_LastOccurence"].ToString());
                        if (dr["AES_SchemeID"].ToString() != "")
                        {
                            alertVo.SchemeID = Int32.Parse(dr["AES_SchemeID"].ToString());
                        }
                        if (dr["PASP_SchemePlanName"].ToString() != "")
                        {
                            alertVo.SchemeName = dr["PASP_SchemePlanName"].ToString();
                        }
                        //if (dr["AES_SentToQueue"].ToString() != "")
                        //{
                        //    alertVo.SentToQueue = dr["AES_SentToQueue"].ToString();
                        //}
                        //if (dr["AllFieldNames"].ToString() != "")
                        //{
                        //    alertVo.AllFieldNames = dr["AllFieldNames"].ToString();
                        //}

                        customerSubscribedList.Add(alertVo);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerSubscribedAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerSubscribedList;
        }

        /// <summary>
        /// Gets all the details of the already subscribed Alerts of a particular type
        /// for a particular Customer
        /// </summary>
        /// <param name="customerId">The customer identifier for whom the Alerts have to be fetched</param>
        /// <param name="eventId">Determines the subtype of Alert that has to be fetched</param>
        /// <returns>Returns a List of Subscribed alerts</returns>
        public List<AlertsSetupVo> GetCustomerSubscribedOccurrenceAlerts(int customerId, int eventId, int CurrentPage, out int count)
        {
            DataSet dsSubscribedEventList = new DataSet();
            List<AlertsSetupVo> customerSubscribedList = new List<AlertsSetupVo>();
            AlertsSetupVo alertVo;
            Database db;
            DbCommand getSubscribedEventListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSubscribedEventListCmd = db.GetStoredProcCommand("SP_GetCustomerSubscribedOccurrenceAlerts");
                db.AddInParameter(getSubscribedEventListCmd, "@AES_CreatedFor", DbType.Int32, customerId);
                db.AddInParameter(getSubscribedEventListCmd, "@AEL_EventID", DbType.Int16, eventId);
                db.AddInParameter(getSubscribedEventListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                //db.AddInParameter(getSubscribedEventListCmd, "@SortOrder", DbType.String, sortOrder);

                dsSubscribedEventList = db.ExecuteDataSet(getSubscribedEventListCmd);

                if (dsSubscribedEventList.Tables[1] != null && dsSubscribedEventList.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsSubscribedEventList.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsSubscribedEventList.Tables[0].Rows.Count > 0)
                {
                    customerSubscribedList = new List<AlertsSetupVo>();
                    foreach (DataRow dr in dsSubscribedEventList.Tables[0].Rows)
                    {
                        alertVo = new AlertsSetupVo();

                        alertVo.EventSetupID = Int64.Parse(dr["AES_EventSetupID"].ToString());
                        alertVo.EventID = Int32.Parse(dr["AEL_EventID"].ToString());

                        if (dr["AES_EventMessage"].ToString() != "")
                        {
                            alertVo.EventMessage = dr["AES_EventMessage"].ToString();
                        }

                        if (dr["AEL_EventCode"].ToString() != "")
                        {
                            alertVo.EventName = dr["AEL_EventCode"].ToString();
                        }

                        if (dr["AES_SchemeID"].ToString() != "")
                        {
                            alertVo.SchemeID = Int32.Parse(dr["AES_SchemeID"].ToString());
                        }
                        if (dr["Name"].ToString() != "")
                        {
                            alertVo.SchemeName = dr["Name"].ToString();
                        }
                        if (dr["ADCS_PresetValue"].ToString() != "")
                        {
                            alertVo.PresetValue = float.Parse(dr["ADCS_PresetValue"].ToString());
                        }
                        if (dr["CurrentValue"].ToString() != "")
                        {
                            alertVo.CurrentValue = float.Parse(dr["CurrentValue"].ToString());
                        }
                        if (dr["ADCS_Condition"].ToString() != "")
                        {
                            alertVo.Condition = dr["ADCS_Condition"].ToString();
                        }

                        customerSubscribedList.Add(alertVo);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerSubscribedOccurrenceAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerSubscribedList;
        }


        public List<AlertsSetupVo> GetCustomerSubscribedReminderAlerts(int customerId, int eventId, int CurrentPage, out int count)
        {
            DataSet dsSubscribedEventList = new DataSet();
            List<AlertsSetupVo> customerSubscribedList = new List<AlertsSetupVo>();
            AlertsSetupVo alertVo;
            Database db;
            DbCommand getSubscribedEventListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSubscribedEventListCmd = db.GetStoredProcCommand("SP_GetCustomerSubscribedReminderAlerts");
                db.AddInParameter(getSubscribedEventListCmd, "@AES_CreatedFor", DbType.Int32, customerId);
                db.AddInParameter(getSubscribedEventListCmd, "@AEL_EventID", DbType.Int16, eventId);
                db.AddInParameter(getSubscribedEventListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                //db.AddInParameter(getSubscribedEventListCmd, "@SortOrder", DbType.String, sortOrder);

                dsSubscribedEventList = db.ExecuteDataSet(getSubscribedEventListCmd);

                if (dsSubscribedEventList.Tables[1] != null && dsSubscribedEventList.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsSubscribedEventList.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsSubscribedEventList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSubscribedEventList.Tables[0].Rows)
                    {
                        alertVo = new AlertsSetupVo();

                        alertVo.EventSetupID = Int64.Parse(dr["ParentSetupId"].ToString());
                        alertVo.EventID = Int32.Parse(dr["EventID"].ToString());

                        if (dr["EventMessage"].ToString() != "")
                        {
                            alertVo.EventMessage = dr["EventMessage"].ToString();
                        }

                        if (dr["EventCode"].ToString() != "")
                        {
                            alertVo.EventName = dr["EventCode"].ToString();
                        }

                        if (dr["SchemeID"].ToString() != "")
                        {
                            alertVo.SchemeID = Int32.Parse(dr["SchemeID"].ToString());
                        }
                        if (dr["Name"].ToString() != "")
                        {
                            alertVo.SchemeName = dr["Name"].ToString();
                        }
                        if (dr["EventDate"].ToString() != "")
                        {
                            alertVo.EventDate = DateTime.Parse(dr["EventDate"].ToString());
                        }
                        if (dr["NextOccurence"].ToString() != "")
                        {
                            alertVo.NextOccurence = DateTime.Parse(dr["NextOccurence"].ToString());
                        }

                        customerSubscribedList.Add(alertVo);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerSubscribedReminderAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerSubscribedList;
        }

        /// <summary>
        /// Gets all the Unsubscribed schemes for a particular customer to pick from for subscribing 
        /// </summary>
        /// <param name="customerId">The customer identifier for whom the Schemes have to be fetched</param>
        /// <param name="eventCode">Determines which type of unsubscribed schemes have to be picked eg:SIP or SWP etc.</param>
        /// <returns>Dataset having all the necessary details of the unsubscribed schemes</returns>
        public DataSet GetCustomerUnsubscribedSystematicSchemes(int customerId, string eventCode, int eventId)
        {
            DataSet dsCustomerSystematicSchemes = new DataSet();
            Database db;
            DbCommand getCustomerSystematicSchemesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerSystematicSchemesCmd = db.GetStoredProcCommand("SP_GetCustomerUnsubscribedSystematicSchemes");
                db.AddInParameter(getCustomerSystematicSchemesCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerSystematicSchemesCmd, "@EventCode", DbType.String, eventCode);
                db.AddInParameter(getCustomerSystematicSchemesCmd, "@AEL_EventId", DbType.Int16, eventId);
                dsCustomerSystematicSchemes = db.ExecuteDataSet(getCustomerSystematicSchemesCmd);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerUnsubscribedSystematicSchemes()");

                object[] objects = new object[0];
                objects[0] = customerId;
                objects[1] = eventCode;
                objects[2] = eventId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsCustomerSystematicSchemes;
        }

        /// <summary>
        /// Gets all the Assets for a particular customer and event code that have not already been subscribed.
        /// </summary>
        /// <param name="customerId">The customer identifier for whom the Assets have to be fetched</param>
        /// <param name="eventCode">Determines which type of Assets have to be picked eg:Property or Personal</param>
        /// <returns>Dataset having all the necessary details of the Assets</returns>
        public DataSet GetCustomerUnsubscribedAssets(int customerId, string eventCode)
        {
            DataSet dsCustomerUnsubscribedAssets = new DataSet();
            Database db;
            DbCommand getCustomerUnsubscribedAssetsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerUnsubscribedAssetsCmd = db.GetStoredProcCommand("SP_GetCustomerUnsubscribedAssets");
                db.AddInParameter(getCustomerUnsubscribedAssetsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerUnsubscribedAssetsCmd, "@EventCode", DbType.String, eventCode);
                dsCustomerUnsubscribedAssets = db.ExecuteDataSet(getCustomerUnsubscribedAssetsCmd);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerUnsubscribedAssets()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eventCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsCustomerUnsubscribedAssets;
        }


        public DataSet GetCustomerReminderEventAssets(int customerId, string eventCode)
        {
            DataSet dsCustomerReminderEventAssets = new DataSet();
            Database db;
            DbCommand getCustomerReminderEventAssetsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerReminderEventAssetsCmd = db.GetStoredProcCommand("SP_GetCustomerReminderEventAssets");
                db.AddInParameter(getCustomerReminderEventAssetsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerReminderEventAssetsCmd, "@EventCode", DbType.String, eventCode);
                dsCustomerReminderEventAssets = db.ExecuteDataSet(getCustomerReminderEventAssetsCmd);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerReminderEventAssets()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eventCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsCustomerReminderEventAssets;
        }

        /// <summary>
        /// Function to save Confirmation type Alert
        /// </summary>
        /// <param name="alertSetupVo">Object that holds all the details required for setting up the alert </param>
        /// <param name="userId">UserId of the subscriber</param>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool SaveConfirmationAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveConfirmationAlert");
                db.AddInParameter(saveConfirmationAlertCmd, "@AEL_EventID", DbType.Int32, alertSetupVo.EventID);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_EventMessage", DbType.String, alertSetupVo.EventMessage);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_SchemeID", DbType.Int32, alertSetupVo.SchemeID);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_TargetID", DbType.Int32, alertSetupVo.TargetID);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_CreatedFor", DbType.Int32, alertSetupVo.CustomerId);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(saveConfirmationAlertCmd, "@AES_ModifiedBy", DbType.Int32, userId);

                db.ExecuteNonQuery(saveConfirmationAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save Occurrence type Alert
        /// </summary>
        /// <param name="alertSetupVo">Object that holds all the details required for setting up the alert</param>
        /// <param name="userId">UserId of the subscriber</param>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool SaveOccurrenceAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveOccurrenceAlert");
                db.AddInParameter(saveOccurrenceAlertCmd, "@AEL_EventID", DbType.Int32, alertSetupVo.EventID);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_EventMessage", DbType.String, alertSetupVo.EventMessage);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_SchemeID", DbType.Int32, alertSetupVo.SchemeID);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_TargetID", DbType.Int32, alertSetupVo.TargetID);
                db.AddInParameter(saveOccurrenceAlertCmd, "@ADCS_PresetValue", DbType.Int32, alertSetupVo.PresetValue);
                db.AddInParameter(saveOccurrenceAlertCmd, "@ADCS_Condition", DbType.String, alertSetupVo.Condition);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_CreatedFor", DbType.Int32, alertSetupVo.CustomerId);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(saveOccurrenceAlertCmd, "@AES_ModifiedBy", DbType.Int32, userId);

                db.ExecuteNonQuery(saveOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveOccurrenceAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public long SaveReminderAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            Database db;
            //bool bResult = false;
            long eventSetupId;
            DbCommand saveReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveReminderAlertCmd = db.GetStoredProcCommand("SP_SaveReminderAlert");

                db.AddInParameter(saveReminderAlertCmd, "@AEL_EventID", DbType.Int32, alertSetupVo.EventID);
                db.AddInParameter(saveReminderAlertCmd, "@AES_EventMessage", DbType.String, alertSetupVo.EventMessage);
                db.AddInParameter(saveReminderAlertCmd, "@AES_SchemeID", DbType.Int32, alertSetupVo.SchemeID);
                db.AddInParameter(saveReminderAlertCmd, "@AES_TargetID", DbType.Int32, alertSetupVo.TargetID);
                db.AddInParameter(saveReminderAlertCmd, "@AES_NextOccurence", DbType.DateTime, alertSetupVo.NextOccurence);
                db.AddInParameter(saveReminderAlertCmd, "@AES_EndDate", DbType.DateTime, alertSetupVo.EndDate);
                db.AddInParameter(saveReminderAlertCmd, "@AES_ParentEventSetupId", DbType.Int32, alertSetupVo.EventSetupID);
                db.AddInParameter(saveReminderAlertCmd, "@CL_CycleId", DbType.Int32, alertSetupVo.FrequencyCode);
                db.AddInParameter(saveReminderAlertCmd, "@AES_CreatedFor", DbType.Int32, alertSetupVo.CustomerId);
                db.AddInParameter(saveReminderAlertCmd, "@IsReminder", DbType.String, alertSetupVo.Reminder);
                db.AddInParameter(saveReminderAlertCmd, "@AES_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(saveReminderAlertCmd, "@AES_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(saveReminderAlertCmd, "EventSetupId", DbType.Int64, 20);

                db.ExecuteNonQuery(saveReminderAlertCmd);

                eventSetupId = long.Parse(db.GetParameterValue(saveReminderAlertCmd, "EventSetupId").ToString());

                //bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveReminderAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eventSetupId;
        }

        /// <summary>
        /// Function to delete the subscribed Event
        /// </summary>
        /// <param name="EventSetupID">Id of the Event to be Deleted</param>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool DeleteEvent(long EventSetupID)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteDateEventCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteDateEventCmd = db.GetStoredProcCommand("SP_DeleteEvent");
                db.AddInParameter(deleteDateEventCmd, "@EventSetupID", DbType.Int64, EventSetupID);
                db.ExecuteNonQuery(deleteDateEventCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteEvent()");

                object[] objects = new object[1];
                objects[0] = EventSetupID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool DeleteReminderEvent(long eventSetupId)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteDateEventCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteDateEventCmd = db.GetStoredProcCommand("SP_DeleteReminderEvent");
                db.AddInParameter(deleteDateEventCmd, "@ParentEventSetupID", DbType.Int64, eventSetupId);
                db.ExecuteNonQuery(deleteDateEventCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteReminderEvent()");

                object[] objects = new object[1];
                objects[0] = eventSetupId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }
        /// <summary>
        /// Function to delete the subscribed Conditional Event
        /// </summary>
        /// <param name="EventSetupID">Id of the Event to be Deleted</param>
        /// <param name="EventID">The below 3 ids used to delete the data from dataconditionalsetuptable</param>
        /// <param name="CustomerID"></param>
        /// <param name="SchemeID"></param>
        /// <returns></returns>
        public bool DeleteConditionalEvent(long EventSetupID)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteConditionalEventCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteConditionalEventCmd = db.GetStoredProcCommand("SP_DeleteConditionalEvent");
                db.AddInParameter(deleteConditionalEventCmd, "@EventSetupID", DbType.Int64, EventSetupID);

                db.ExecuteNonQuery(deleteConditionalEventCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteConditionalEvent()");

                object[] objects = new object[1];
                objects[0] = EventSetupID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }



        public DataSet GetMetatableDetails(string primaryKey)
        {
            Database db;
            DbCommand getMetatableCmd;
            DataSet dsGetMetatableDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMetatableCmd = db.GetStoredProcCommand("SP_GetMetatableDetails");
                db.AddInParameter(getMetatableCmd, "@WM_PrimaryKey", DbType.String, primaryKey);

                dsGetMetatableDetails = db.ExecuteDataSet(getMetatableCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetMetatableDetails()");

                object[] objects = new object[1];
                objects[0] = primaryKey;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetMetatableDetails;
        }

        public DataSet GetSchemeDescription(string description, string tableName, string metatablePrimaryKey, int SchemeId)
        {
            Database db;
            DbCommand getSchemeDescriptionCmd;
            DataSet dsGetSchemeDescription;
            string query;

            try
            {
                query = "select " + description + " from " + tableName + " where " + metatablePrimaryKey + " = " + SchemeId.ToString();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeDescriptionCmd = db.GetSqlStringCommand(query);


                dsGetSchemeDescription = db.ExecuteDataSet(getSchemeDescriptionCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetSchemeDescription()");

                object[] objects = new object[4];
                objects[0] = description;
                objects[1] = tableName;
                objects[2] = metatablePrimaryKey;
                objects[3] = SchemeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSchemeDescription;
        }


        #endregion

        #region NewAlertNotification

        /// <summary>
        /// Function the gets a List of notifications for a particular customer for displaying
        /// </summary>
        /// <param name="CustomerId">Id of the customer for whom the notifications have to be fetched</param>
        /// <returns>Returns a List Data Type having all the Notification</returns>
        public List<AlertsNotificationVo> GetCustomerNotifications(int CustomerId, int CurrentPage, string sortOrder, out int count)
        {
            List<AlertsNotificationVo> alertNotificationList = null;
            AlertsNotificationVo alertNotificationVo;
            Database db;
            DbCommand getCustomerNotificationsCmd;
            DataSet dsGetCustomerNotifications;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNotificationsCmd = db.GetStoredProcCommand("SP_GetCustomerNotifications");
                db.AddInParameter(getCustomerNotificationsCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getCustomerNotificationsCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(getCustomerNotificationsCmd, "@SortOrder", DbType.String, sortOrder);

                dsGetCustomerNotifications = db.ExecuteDataSet(getCustomerNotificationsCmd);

                if (dsGetCustomerNotifications.Tables[1] != null && dsGetCustomerNotifications.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsGetCustomerNotifications.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsGetCustomerNotifications.Tables[0].Rows.Count > 0)
                {
                    alertNotificationList = new List<AlertsNotificationVo>();

                    foreach (DataRow dr in dsGetCustomerNotifications.Tables[0].Rows)
                    {

                        alertNotificationVo = new AlertsNotificationVo();

                        alertNotificationVo.NotificationID = Int64.Parse(dr["AEN_EventQueueID"].ToString());
                        alertNotificationVo.Category = dr["AEL_EventType"].ToString();
                        alertNotificationVo.EventSetupID = Int64.Parse(dr["AES_EventSetupID"].ToString());
                        alertNotificationVo.EventID = Int32.Parse(dr["AEL_EventID"].ToString());
                        alertNotificationVo.CustomerID = Int32.Parse(dr["AEN_TargetID"].ToString());
                        alertNotificationVo.EventCode = dr["AEL_EventCode"].ToString();
                        alertNotificationVo.EventMessage = dr["AEN_EventMessage"].ToString();
                        if (dr["AEN_SchemeID"].ToString() != "")
                        {
                            alertNotificationVo.SchemeID = Int32.Parse(dr["AEN_SchemeID"].ToString());
                        }
                        alertNotificationVo.PopulatedDate = DateTime.Parse(dr["AEN_PopulatedDate"].ToString());
                        alertNotificationVo.ModeId = Int16.Parse(dr["ADML_ModeId"].ToString());
                        alertNotificationVo.Reminder = dr["AEL_Reminder"].ToString();
                        alertNotificationVo.Name = dr["Name"].ToString();

                        alertNotificationList.Add(alertNotificationVo);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerNotifications()");

                object[] objects = new object[1];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return alertNotificationList;
        }
        /// <summary>
        /// Function to Detele a particular notification
        /// </summary>
        /// <param name="notificationId">Id of the notification</param>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool DeleteAlertNotification(long notificationId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteAlertNotificationCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAlertNotificationCmd = db.GetStoredProcCommand("SP_DeleteAlertNotification");
                db.AddInParameter(deleteAlertNotificationCmd, "@NotificationID", DbType.Int64, notificationId);
                db.ExecuteNonQuery(deleteAlertNotificationCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteAlertNotification()");

                object[] objects = new object[1];
                objects[0] = notificationId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerDashboardAlerts(int customerId)
        {
            Database db;
            DbCommand getCustomerAlertsCmd;
            DataSet dsGetCustomerAlertsList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerDashboardAlerts");
                db.AddInParameter(getCustomerAlertsCmd, "@C_CustomerID", DbType.Int32, customerId);

                dsGetCustomerAlertsList = db.ExecuteDataSet(getCustomerAlertsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerDashboardAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetCustomerAlertsList;
        }

        public DataSet GetCustomerGrpDashboardAlerts(int customerId)
        {
            Database db;
            DbCommand getCustomerGrpAlertsCmd;
            DataSet dsGetCustomerGrpAlertsList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerGrpAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerGrpDashboardAlerts");
                db.AddInParameter(getCustomerGrpAlertsCmd, "@C_CustomerID", DbType.Int32, customerId);

                dsGetCustomerGrpAlertsList = db.ExecuteDataSet(getCustomerGrpAlertsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerGrpDashboardAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetCustomerGrpAlertsList;
        }

        public DataSet GetRMCustomerDashboardAlerts(int rmId)
        {
            Database db;
            DbCommand getCustomerAlertsCmd;
            DataSet dsGetCustomerAlertsList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAlertsCmd = db.GetStoredProcCommand("SP_GetRMCustomerDashboardAlerts");
                db.AddInParameter(getCustomerAlertsCmd, "@RMId", DbType.Int32, rmId);

                dsGetCustomerAlertsList = db.ExecuteDataSet(getCustomerAlertsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetRMCustomerDashboardAlerts()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetCustomerAlertsList;
        }

        #endregion

        #region NewAlertServicesExecution

        /// <summary>
        /// Function to call the Reminder alert Service
        /// </summary>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool ExecuteReminderAlert()
        {
            bool bResult = false;
            Database db;
            DbCommand executeReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                executeReminderAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForDate");
                executeReminderAlertCmd.CommandTimeout = 60 * 60; //in Seconds
                db.ExecuteNonQuery(executeReminderAlertCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteReminderAlert()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }


        public bool ExecuteProcessAlertstoEmailQueue()
        {
            bool bResult = false;
            Database db;
            DbCommand executeProcessAlertstoEmailQueueCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                executeProcessAlertstoEmailQueueCmd = db.GetStoredProcCommand("sproc_Job_ProcessAlerts");
                executeProcessAlertstoEmailQueueCmd.CommandTimeout = 60 * 60; //in Seconds
                db.ExecuteNonQuery(executeProcessAlertstoEmailQueueCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteReminderAlert()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        /// <summary>
        /// Function to call the Occurrence alert service
        /// </summary>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool ExecuteOccurrenceAlert()
        {
            bool bResult = false;
            Database db;
            DbCommand executeOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                executeOccurrenceAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForData");
                executeOccurrenceAlertCmd.CommandTimeout = 60 * 60; //in Seconds
                db.ExecuteNonQuery(executeOccurrenceAlertCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteOccurrenceAlert()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        /// <summary>
        /// Function to call the Confimation alert service
        /// </summary>
        /// <returns>Returns a boolean variable to determine if the process was successful</returns>
        public bool ExecuteConfirmationAlert()
        {
            bool bResult = false;
            Database db;
            DbCommand executeConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                executeConfirmationAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForTrans");
                executeConfirmationAlertCmd.CommandTimeout = 60 * 60; //in Seconds

                db.ExecuteNonQuery(executeConfirmationAlertCmd);
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

                FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteConfirmationAlert()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        #endregion

        #region NewNewAlertSetup

        /// <summary>
        /// Function to save SIP Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserSIPReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSIPReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSIPReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSIPReminderAlert");
                db.AddInParameter(saveSIPReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveSIPReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveSIPReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveSIPReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveSIPReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveSIPReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveSIPReminderAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveSIPReminderAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveSIPReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveSIPReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveSIPReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserSIPReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save SWP Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserSWPReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSWPReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSWPReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSWPReminderAlert");
                db.AddInParameter(saveSWPReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveSWPReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveSWPReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveSWPReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveSWPReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveSWPReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveSWPReminderAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveSWPReminderAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveSWPReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveSWPReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveSWPReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserSWPReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save Anniversary Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserAnniversaryReminderAlert(int advisorId, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveAnniversaryReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveAnniversaryReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserAnniversaryReminderAlert");
                db.AddInParameter(saveAnniversaryReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveAnniversaryReminderAlertCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(saveAnniversaryReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveAnniversaryReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserAnniversaryReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save DOB Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserDOBReminderAlert(int advisorId, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveDOBReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveDOBReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserDOBReminderAlert");
                db.AddInParameter(saveDOBReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveDOBReminderAlertCmd, "@UserId", DbType.String, userId);
                db.AddInParameter(saveDOBReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveDOBReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserDOBReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save ELSSMaturity Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserELSSMaturityReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveELSSMaturityReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveELSSMaturityReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserELSSMaturityReminderAlert");
                db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveELSSMaturityReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserELSSMaturityReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save FDMaturity Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserFDMaturityReminderAlert(int advisorId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveFDMaturityReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveFDMaturityReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserFDMaturityReminderAlert");
                db.AddInParameter(saveFDMaturityReminderAlertCmd, "@adviserid", DbType.Int32, advisorId);
                db.AddInParameter(saveFDMaturityReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (fdId != 0)
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@FixedIncomeNPId", DbType.Int32, fdId);
                else
                    db.AddInParameter(saveFDMaturityReminderAlertCmd, "@FixedIncomeNPId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveFDMaturityReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveFDMaturityReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveFDMaturityReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserFDMaturityReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save FDRecurringDeposit Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserFDRecurringDepositReminderAlert(int advisorId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveFDRecurringDepositReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveFDRecurringDepositReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserFDRecurringDepositReminderAlert");
                db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (fdId != 0)
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@FixedIncomeNPId", DbType.Int32, fdId);
                else
                    db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@FixedIncomeNPId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveFDRecurringDepositReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserFDRecurringDepositReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save Insurance Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="reminderDays">The parameter for calculating the next occurrence. Indicates the number of days before the actual event</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserInsuranceReminderAlert(int advisorId, int customerId, int accountId, int insuranceId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveInsuranceReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveInsuranceReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserInsurancePremiumReminderAlert");
                db.AddInParameter(saveInsuranceReminderAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveInsuranceReminderAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (insuranceId != 0)
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@InsuranceNPId", DbType.Int32, insuranceId);
                else
                    db.AddInParameter(saveInsuranceReminderAlertCmd, "@InsuranceNPId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveInsuranceReminderAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveInsuranceReminderAlertCmd, "@reminderDays", DbType.Int32, reminderDays);

                db.ExecuteNonQuery(saveInsuranceReminderAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserInsuranceReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save SIP Confirmation Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserSIPConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSIPConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSIPConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSIPConfirmationAlert");
                db.AddInParameter(saveSIPConfirmationAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveSIPConfirmationAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveSIPConfirmationAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveSIPConfirmationAlertCmd, "@IsBulk", DbType.Int32, isBulk);

                db.ExecuteNonQuery(saveSIPConfirmationAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserSIPConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save SWP Confirmation Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserSWPConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSWPConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSWPConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSWPConfirmationAlert");
                db.AddInParameter(saveSWPConfirmationAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveSWPConfirmationAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveSWPConfirmationAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveSWPConfirmationAlertCmd, "@IsBulk", DbType.Int32, isBulk);

                db.ExecuteNonQuery(saveSWPConfirmationAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserSWPConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save SWP Confirmation Reminder Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserMFDividendConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFDividendConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFDividendConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFDividendConfirmationAlert");
                db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@IsBulk", DbType.Int32, isBulk);

                db.ExecuteNonQuery(saveMFDividendConfirmationAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserMFDividendConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save Personal Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserPersonalOccurrenceAlert(int advisorId, int customerId, int accountId, int personalNpId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand savePersonalOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                savePersonalOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserPersonalOccurrenceAlert");
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (personalNpId != 0)
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@PersonalNpId", DbType.Int32, personalNpId);
                else
                    db.AddInParameter(savePersonalOccurrenceAlertCmd, "@PersonalNpId", DbType.Int32, DBNull.Value);
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(savePersonalOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserPersonalOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save Property Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserPropertyOccurrenceAlert(int advisorId, int customerId, int accountId, int propertyNpId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand savePropertyOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                savePropertyOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserPropertyOccurrenceAlert");
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (propertyNpId != 0)
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@PropertyNpId", DbType.Int32, propertyNpId);
                else
                    db.AddInParameter(savePropertyOccurrenceAlertCmd, "@PropertyNpId", DbType.Int32, DBNull.Value);
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(savePropertyOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserPropertyOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save MF Stop Loos Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserMFStopLossOccurrenceAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFStopLossOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFStopLossOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFStopLossOccurrenceAlert");
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(saveMFStopLossOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserMFStopLossOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save MF Stop Loos Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserMFProfitBookingOccurrenceAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFProfitBookingOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFProfitBookingOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFProfitBookingOccurrenceAlert");
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (schemeId != 0)
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@SchemeId", DbType.Int32, schemeId);
                else
                    db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@SchemeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(saveMFProfitBookingOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserMFProfitBookingOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save MF Stop Loos Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserEQStopLossOccurrenceAlert(int advisorId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveEQStopLossOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveEQStopLossOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserEQStopLossOccurrenceAlert");
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (scripId != 0)
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@ScripId", DbType.Int32, scripId);
                else
                    db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@ScripId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(saveEQStopLossOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserEQStopLossOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to save MF Stop Loos Occurrence Type Alert
        /// </summary>
        /// <param name="rmId">the rmId of the person for whom the setup needs to be done</param>
        /// <param name="userId">UserId of the person subscribing</param>
        /// <returns>Boolean Value tochk if the process was successful</returns>
        public bool SaveAdviserEQProfitBookingOccurrenceAlert(int advisorId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveEQProfitBookingOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveEQProfitBookingOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserEQProfitBookingOccurrenceAlert");
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@UserId", DbType.Int32, userId);
                if (customerId != 0)
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@CustomerId", DbType.Int32, customerId);
                else
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                if (accountId != 0)
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@AccountId", DbType.Int32, accountId);
                else
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@AccountId", DbType.Int32, DBNull.Value);
                if (scripId != 0)
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@ScripId", DbType.Int32, scripId);
                else
                    db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@ScripId", DbType.Int32, DBNull.Value);
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@IsBulk", DbType.Int32, isBulk);
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@Condition", DbType.String, condition);
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@presetValue", DbType.Int32, presetValue);

                db.ExecuteNonQuery(saveEQProfitBookingOccurrenceAlertCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:SaveAdviserEQProfitBookingOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataSet GetCustomerMFAlerts(int customerId)
        {
            Database db;
            DataSet dsGetCustomerMFAlerts;
            DbCommand getCustomerMFAlertsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerMFAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerMFAlerts");
                db.AddInParameter(getCustomerMFAlertsCmd, "@CustomerId", DbType.Int32, customerId);
                dsGetCustomerMFAlerts = db.ExecuteDataSet(getCustomerMFAlertsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerMFAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerMFAlerts;
        }

        public DataSet GetCustomerFIAlerts(int customerId)
        {
            Database db;
            DataSet dsGetCustomerFIAlerts;
            DbCommand getCustomerFIAlertsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFIAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerFIAlerts");
                db.AddInParameter(getCustomerFIAlertsCmd, "@CustomerId", DbType.Int32, customerId);
                dsGetCustomerFIAlerts = db.ExecuteDataSet(getCustomerFIAlertsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerFIAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerFIAlerts;
        }

        public DataSet GetCustomerInsuranceAlerts(int customerId)
        {
            Database db;
            DataSet dsGetCustomerInsuranceAlerts;
            DbCommand getCustomerInsuranceAlertsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerInsuranceAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerInsuranceAlerts");
                db.AddInParameter(getCustomerInsuranceAlertsCmd, "@CustomerId", DbType.Int32, customerId);
                dsGetCustomerInsuranceAlerts = db.ExecuteDataSet(getCustomerInsuranceAlertsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerInsuranceAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerInsuranceAlerts;
        }

        public DataSet GetCustomerEQAlerts(int customerId)
        {
            Database db;
            DataSet dsGetCustomerEQAlerts;
            DbCommand getCustomerEQAlertsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerEQAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerEQAlerts");
                db.AddInParameter(getCustomerEQAlertsCmd, "@CustomerId", DbType.Int32, customerId);
                dsGetCustomerEQAlerts = db.ExecuteDataSet(getCustomerEQAlertsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerEQAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerEQAlerts;
        }

        public bool DeleteAdviserAlertSetup(int rmUserId, int eventId)
        {
            Database db;
            bool bResult = false;
            DbCommand deleteAlertSetupCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAlertSetupCmd = db.GetStoredProcCommand("SP_DeleteAdviserAlertSetup");
                db.AddInParameter(deleteAlertSetupCmd, "@RMUserId", DbType.Int32, rmUserId);
                db.AddInParameter(deleteAlertSetupCmd, "@EventId", DbType.Int32, eventId);
                db.ExecuteNonQuery(deleteAlertSetupCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteAdviserAlertSetup()");

                object[] objects = new object[2];
                objects[0] = rmUserId;
                objects[0] = eventId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public int ChkAdviserAlertSetup(int rmUserId, int eventId)
        {
            Database db;
            int count;
            DataSet ds;
            DbCommand chkAlertSetupCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAlertSetupCmd = db.GetStoredProcCommand("SP_ChkAdviserAlertSetup");
                db.AddInParameter(chkAlertSetupCmd, "@RMUserId", DbType.Int32, rmUserId);
                db.AddInParameter(chkAlertSetupCmd, "@EventId", DbType.Int32, eventId);
                ds = db.ExecuteDataSet(chkAlertSetupCmd);

                count = Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:ChkAdviserAlertSetup()");

                object[] objects = new object[2];
                objects[0] = rmUserId;
                objects[0] = eventId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public bool DeleteCustomerAlertSetup(int customerId, int accountId, int schemeId)
        {
            Database db;
            bool bResult = false;
            DbCommand deleteAlertSetupCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAlertSetupCmd = db.GetStoredProcCommand("SP_DeleteCustomerAlertSetup");
                db.AddInParameter(deleteAlertSetupCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(deleteAlertSetupCmd, "@AccountId", DbType.Int32, accountId);
                db.AddInParameter(deleteAlertSetupCmd, "@SchemeId", DbType.Int32, schemeId);
                db.ExecuteNonQuery(deleteAlertSetupCmd);

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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteCustomerAlertSetup()");

                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = accountId;
                objects[2] = schemeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }



        #endregion
        
        /// <summary>
        /// Modified the function to add a name filter in the Alert notifications gridview.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usertype"></param>
        /// <param name="currentpage"></param>
        /// <param name="nameFilter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetAdviserCustomerSMSAlerts(int id,string usertype, int currentpage,string nameFilter, out int count)
        {
            
            Database db;
            DbCommand getAdviserSMSAlertsCmd;
            DataSet dsAdviserSMSAlerts;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserSMSAlertsCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerSMSAlerts");
                db.AddInParameter(getAdviserSMSAlertsCmd, "@Id", DbType.Int32, id);
                db.AddInParameter(getAdviserSMSAlertsCmd, "@CurrentPage", DbType.Int32, currentpage);
                db.AddInParameter(getAdviserSMSAlertsCmd, "@Usertype", DbType.String, usertype);
                db.AddInParameter(getAdviserSMSAlertsCmd, "@nameFilter", DbType.String, nameFilter);
                //db.AddInParameter(getAdviserSMSAlertsCmd, "@CurrentPage", DbType.String, sortorder);
                //db.AddInParameter(getAdviserSMSAlertsCmd, "@SortOrder", DbType.Int32, currentpage);

                dsAdviserSMSAlerts = db.ExecuteDataSet(getAdviserSMSAlertsCmd);
                count = Int32.Parse(dsAdviserSMSAlerts.Tables[1].Rows[0][0].ToString());
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:GetAdviserCustomerSMSAlerts(int adviserId)");

                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdviserSMSAlerts;
        }

        public bool UpdateAlertStatus(int alertId, int alertStatus)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAlertStatusCmd;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAlertStatusCmd = db.GetStoredProcCommand("SP_UpdateAlertStatus");
                db.AddInParameter(updateAlertStatusCmd, "@AlertId", DbType.Int32, alertId);
                db.AddInParameter(updateAlertStatusCmd, "@AlertStatus", DbType.Int16, alertStatus);

                db.ExecuteNonQuery(updateAlertStatusCmd);
                bResult=true;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:UpdateAlertStatus(int alertId, int alertStatus)");

                object[] objects = new object[1];
                objects[0] = alertId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }

        public void UpdateCustomerMobileNumber(int customerId, Int64 mobileNo)
        {

            Database db;
            DbCommand updateCustomerMobileNumber;  
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustomerMobileNumber = db.GetStoredProcCommand("SP_UpdateMobileNumberForAlertCustomer");
                db.AddInParameter(updateCustomerMobileNumber, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(updateCustomerMobileNumber, "@MobileNo", DbType.Int64, mobileNo);
                db.ExecuteNonQuery(updateCustomerMobileNumber);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsDao.cs:UpdateCustomerMobileNumber(int customerId, int mobileNo)");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = mobileNo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
       
        /// <summary>
        /// Function to delete the notifications from the notifications grid view (MP)
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        public bool DeleteAdviserCustomerSMSAlerts(int alertId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteSMSAlertsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteSMSAlertsCmd = db.GetStoredProcCommand("SP_DeleteAdviserCustomerSMSAlerts");
                db.AddInParameter(deleteSMSAlertsCmd, "@AlertId", DbType.Int32, alertId);

                if(db.ExecuteNonQuery(deleteSMSAlertsCmd)  != 0)
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

                FunctionInfo.Add("Method", "AlertsDao.cs:DeleteAdviserCustomerSMSAlerts(int alertId)");

                object[] objects = new object[1];
                objects[0] = alertId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
    }

}
