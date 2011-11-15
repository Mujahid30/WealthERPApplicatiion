using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using VoUser;
using VoAlerts;
using BoUser;
using BoAlerts;
using DaoAlerts;
using DaoProductMaster;
using System.Collections.Specialized;

namespace BoAlerts
{
    public class AlertsBo
    {
        #region NewAlertSetup

        public DataSet GetSystemAlerts(int CurrentPage, out int count)
        {
            DataSet dsGetSystemAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetSystemAlerts = alertsDao.GetSystemAlerts(CurrentPage, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetSystemAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetSystemAlerts;
        }

        public List<AlertsSetupVo> GetCustomerSubscribedConfirmationAlerts(int customerId, int eventId, string alertType, int CurrentPage, out int count)
        {
            List<AlertsSetupVo> customerSubscribedList = new List<AlertsSetupVo>();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                customerSubscribedList = alertsDao.GetCustomerSubscribedConfirmationAlerts(customerId, eventId, alertType, CurrentPage, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerSubscribedConfirmationAlerts()");

                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = eventId;
                objects[2] = alertType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerSubscribedList;
        }

        public List<AlertsSetupVo> GetCustomerSubscribedOccurrenceAlerts(int customerId, int eventId, int CurrentPage, out int count)
        {
            List<AlertsSetupVo> customerSubscribedList = new List<AlertsSetupVo>();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                customerSubscribedList = alertsDao.GetCustomerSubscribedOccurrenceAlerts(customerId, eventId, CurrentPage, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerSubscribedOccurrenceAlerts()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eventId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerSubscribedList;
        }

        public List<AlertsSetupVo> GetCustomerSubscribedReminderAlerts(int customerId, int eventId, int CurrentPage, out int count)
        {
            List<AlertsSetupVo> customerSubscribedList = new List<AlertsSetupVo>();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                customerSubscribedList = alertsDao.GetCustomerSubscribedReminderAlerts(customerId, eventId, CurrentPage, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerSubscribedReminderAlerts()");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eventId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return customerSubscribedList;
        }

        public DataSet GetCustomerUnsubscribedSystematicSchemes(int customerId, string eventCode, int eventId)
        {
            DataSet dsCustomerUnsubscribedSchemesList = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsCustomerUnsubscribedSchemesList = alertsDao.GetCustomerUnsubscribedSystematicSchemes(customerId, eventCode, eventId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerUnsubscribedSystematicSchemes()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerUnsubscribedSchemesList;
        }

        public DataSet GetCustomerUnsubscribedAssets(int customerId, string eventCode)
        {
            DataSet dsCustomerUnsubscribedAssetsList = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsCustomerUnsubscribedAssetsList = alertsDao.GetCustomerUnsubscribedAssets(customerId, eventCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerUnsubscribedAssets()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerUnsubscribedAssetsList;
        }

        public DataSet GetCustomerReminderEventAssets(int customerId, string eventCode)
        {
            DataSet dsCustomerUnsubscribedAssetsList = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsCustomerUnsubscribedAssetsList = alertsDao.GetCustomerReminderEventAssets(customerId, eventCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerReminderEventAssets()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerUnsubscribedAssetsList;
        }

        public bool SaveConfirmationAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                bResult = alertsDao.SaveConfirmationAlert(alertSetupVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool SaveOccurrenceAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                bResult = alertsDao.SaveOccurrenceAlert(alertSetupVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveOccurrenceAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public long SaveReminderAlert(AlertsSetupVo alertSetupVo, int userId)
        {
            //bool bResult = false;
            long eventSetupId;
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                eventSetupId = alertsDao.SaveReminderAlert(alertSetupVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveReminderAlert()");

                object[] objects = new object[2];
                objects[0] = alertSetupVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return eventSetupId;
        }

        public bool DeleteEvent(long EventSetupID)
        {
            AlertsDao alertsDao = new AlertsDao();
            bool blResult = false;

            try
            {
                if (alertsDao.DeleteEvent(EventSetupID))
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

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteEvent()");

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
            AlertsDao alertsDao = new AlertsDao();
            bool blResult = false;

            try
            {
                if (alertsDao.DeleteReminderEvent(eventSetupId))
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

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteReminderEvent()");

                object[] objects = new object[1];
                objects[0] = eventSetupId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool DeleteConditionalEvent(long EventSetupID)
        {
            AlertsDao alertsDao = new AlertsDao();
            bool blResult = false;

            try
            {

                if (alertsDao.DeleteConditionalEvent(EventSetupID))
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

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteConditionalEvent()");

                object[] objects = new object[2];
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
            AlertsDao alertsDao = new AlertsDao();
            DataSet dsGetMetatableDetails;

            try
            {
                dsGetMetatableDetails = alertsDao.GetMetatableDetails(primaryKey);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetMetatableDetails()");

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
            AlertsDao alertsDao = new AlertsDao();
            DataSet dsSchemeDescription;

            try
            {
                dsSchemeDescription = alertsDao.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetSchemeDescription()");

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

            return dsSchemeDescription;
        }


        #endregion

        #region NewAlertNotification

        public List<AlertsNotificationVo> GetCustomerNotifications(int CustomerId, int CurrentPage, string sortOrder, out int count)
        {
            List<AlertsNotificationVo> alertNotificationList = new List<AlertsNotificationVo>();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                alertNotificationList = alertsDao.GetCustomerNotifications(CustomerId, CurrentPage, sortOrder, out count);//
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerNotifications()");

                object[] objects = new object[1];
                //objects[0] = type;
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return alertNotificationList;
        }//

        public bool DeleteAlertNotification(long notificationId)
        {
            AlertsDao alertsDao = new AlertsDao();
            bool bResult = false;

            try
            {
                if (alertsDao.DeleteAlertNotification(notificationId))
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

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAlertNotification()");

                object[] objects = new object[1];
                objects[0] = notificationId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }

        public DataSet GetCustomerDashboardAlerts(int customerId)
        {
            AlertsDao alertsDao = new AlertsDao();
            DataSet dsCustomerAlerts;

            try
            {
                dsCustomerAlerts = alertsDao.GetCustomerDashboardAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerDashboardAlerts()");

                object[] objects = new object[4];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerAlerts;
        }

        public DataSet GetCustomerGrpDashboardAlerts(int customerId)
        {
            AlertsDao alertsDao = new AlertsDao();
            DataSet dsCustomerGrpAlerts;

            try
            {
                dsCustomerGrpAlerts = alertsDao.GetCustomerGrpDashboardAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerGrpDashboardAlerts()");

                object[] objects = new object[4];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerGrpAlerts;
        }

        public DataSet GetRMCustomerDashboardAlerts(int rmId)
        {
            AlertsDao alertsDao = new AlertsDao();
            DataSet dsCustomerAlerts;

            try
            {
                dsCustomerAlerts = alertsDao.GetRMCustomerDashboardAlerts(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetRMCustomerDashboardAlerts()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCustomerAlerts;
        }


        #endregion

        #region NewAlertServicesExecution

        public bool ExecuteReminderAlert()
        {
            AlertsDao alertsDao = new AlertsDao();
            bool bResult = false;

            try
            {
                bResult = alertsDao.ExecuteReminderAlert();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:ExecuteReminderAlert()");

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
            AlertsDao alertsDao = new AlertsDao();
            bool bResult = false;

            try
            {
                bResult = alertsDao.ExecuteProcessAlertstoEmailQueue();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:ExecuteProcessAlertstoEmailQueue()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public bool ExecuteOccurrenceAlert()
        {
            AlertsDao alertsDao = new AlertsDao();
            bool bResult = false;

            try
            {
                bResult = alertsDao.ExecuteOccurrenceAlert();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:ExecuteOccurrenceAlert()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool ExecuteConfirmationAlert()
        {
            AlertsDao alertsDao = new AlertsDao();
            bool bResult = false;

            try
            {
                bResult = alertsDao.ExecuteConfirmationAlert();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:ExecuteTransactionAlert()");

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

        public bool SaveAdviserSIPReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSIPReminderAlert(advisorId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserSIPReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSWPReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSWPReminderAlert(advisorId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserSWPReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserDOBReminderAlert(int advisorId, int userId, int reminderDays)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserDOBReminderAlert(advisorId, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserDOBReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserAnniversaryReminderAlert(int advisorId, int userId, int reminderDays)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserAnniversaryReminderAlert(advisorId, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserAnniversaryReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserELSSMaturityReminderAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserELSSMaturityReminderAlert(advisorId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserELSSMaturityReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserFDMaturityReminderAlert(int advisorId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserFDMaturityReminderAlert(advisorId, customerId, accountId, fdId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserFDMaturityReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserFDRecurringDepositReminderAlert(int advisorId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserFDRecurringDepositReminderAlert(advisorId, customerId, accountId, fdId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserFDRecurringDepositReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserInsuranceReminderAlert(int advisorId, int customerId, int accountId, int insuranceId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserInsuranceReminderAlert(advisorId, customerId, accountId, insuranceId, isBulk, reminderDays, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserInsuranceReminderAlert()");

                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = reminderDays;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSIPConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSIPConfirmationAlert(advisorId, customerId, accountId, schemeId, isBulk, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserSIPConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSWPConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSWPConfirmationAlert(advisorId, customerId, accountId, schemeId, isBulk, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserSWPConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserMFDividendConfirmationAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFDividendConfirmationAlert(advisorId, customerId, accountId, schemeId, isBulk, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserMFDividendConfirmationAlert()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserPersonalOccurrenceAlert(int advisorId, int customerId, int accountId, int personalNpId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserPersonalOccurrenceAlert(advisorId, customerId, accountId, personalNpId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserPersonalOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserPropertyOccurrenceAlert(int advisorId, int customerId, int accountId, int propertyNpId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserPropertyOccurrenceAlert(advisorId, customerId, accountId, propertyNpId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserPropertyOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserMFStopLossOccurrenceAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFStopLossOccurrenceAlert(advisorId, customerId, accountId, schemeId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserMFStopLossOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserMFProfitBookingOccurrenceAlert(int advisorId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFProfitBookingOccurrenceAlert(advisorId, customerId, accountId, schemeId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserMFProfitBookingOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserEQStopLossOccurrenceAlert(int advisorId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserEQStopLossOccurrenceAlert(advisorId, customerId, accountId, scripId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserEQStopLossOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserEQProfitBookingOccurrenceAlert(int advisorId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserEQProfitBookingOccurrenceAlert(advisorId, customerId, accountId, scripId, isBulk, userId, condition, presetValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:SaveAdviserEQProfitBookingOccurrenceAlert()");

                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = userId;
                objects[2] = condition;
                objects[3] = presetValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataSet GetCustomerMFAlerts(int customerId)
        {
            DataSet dsGetCustomerMFAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetCustomerMFAlerts = alertsDao.GetCustomerMFAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerMFAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerMFAlerts;
        }

        public DataSet GetCustomerEQAlerts(int customerId)
        {
            DataSet dsGetCustomerEQAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetCustomerEQAlerts = alertsDao.GetCustomerEQAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerEQAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerEQAlerts;
        }

        public DataSet GetCustomerFIAlerts(int customerId)
        {
            DataSet dsGetCustomerFIAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetCustomerFIAlerts = alertsDao.GetCustomerFIAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerFIAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerFIAlerts;
        }

        public DataSet GetCustomerInsuranceAlerts(int customerId)
        {
            DataSet dsGetCustomerInsuranceAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetCustomerInsuranceAlerts = alertsDao.GetCustomerInsuranceAlerts(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerInsuranceAlerts()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerInsuranceAlerts;
        }

        public bool DeleteAdviserAlertSetup(int rmUserId, int eventId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.DeleteAdviserAlertSetup(rmUserId, eventId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAdviserAlertSetup()");

                object[] objects = new object[4];
                objects[0] = rmUserId;
                objects[1] = eventId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public int ChkAdviserAlertSetup(int rmUserId, int eventId)
        {
            int count = 0;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                count = alertsDao.ChkAdviserAlertSetup(rmUserId, eventId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:ChkAdviserAlertSetup()");

                object[] objects = new object[4];
                objects[0] = rmUserId;
                objects[1] = eventId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }

        public bool DeleteCustomerAlertSetup(int customerId, int accountId, int schemeId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.DeleteCustomerAlertSetup(customerId, accountId, schemeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAdviserAlertSetup()");

                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = accountId;
                objects[2] = schemeId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
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
        public DataSet GetAdviserCustomerSMSAlerts(int id, string usertype, int currentpage, string nameFilter, out int count)
        {
            DataSet dsAdviserCustomerSMSAlerts;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                dsAdviserCustomerSMSAlerts = alertsDao.GetAdviserCustomerSMSAlerts(id, usertype, currentpage, nameFilter, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetAdviserCustomerSMSAlerts(int adviserId))");

                object[] objects = new object[4];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserCustomerSMSAlerts;

        }

        public bool UpdateAlertStatus(List<int> alertIdList, int alertStatus)
        {
            bool bResult = false;

            try
            {
                for (int i = 0; i < alertIdList.Count; i++)
                {
                    bResult = UpdateAlertStatus(alertIdList[i], alertStatus);
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

                FunctionInfo.Add("Method", "AlertsBo.cs:UpdateAlertStatus(List<int> alertIdList, int alertStatus)");

                object[] objects = new object[4];
                objects[0] = alertIdList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool UpdateAlertStatus(int alertId, int alertStatus)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.UpdateAlertStatus(alertId, alertStatus);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:UpdateAlertStatus(int alertId, int alertStatus)");

                object[] objects = new object[4];
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
            AlertsDao alertsDao = new AlertsDao();
            alertsDao.UpdateCustomerMobileNumber(customerId, mobileNo);
        }

        /// <summary>
        /// Function to delete the notifications from the notifications grid view(MP)
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        public bool DeleteAdviserCustomerSMSAlerts(int alertId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.DeleteAdviserCustomerSMSAlerts(alertId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAdviserCustomerSMSAlerts(int alertId)");

                object[] objects = new object[1];
                objects[0] = alertId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Get adviser user alerts..
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Remindercount"></param>
        /// <param name="ConditionsCount"></param>
        /// <returns></returns>
        
        public DataSet GetAdviserUserAlerts(int userId, out int Remindercount, out int ConditionsCount)
        {
            DataSet dsGetAdviserUserAlerts = new DataSet();
            AlertsDao alertsDao = new AlertsDao();

            try
            {
                dsGetAdviserUserAlerts = alertsDao.GetAdviserUserAlerts(userId, out Remindercount, out ConditionsCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AlertsBo.cs:GetSystemAlerts()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetAdviserUserAlerts;
        }
    }
}
