using System;
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
        #region OldAlert
        //public DataSet GetListofAlerts(string type)
        //{
        //    DataSet dsAlertList = new DataSet();
        //    Database db;
        //    DbCommand getListOfAlertsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getListOfAlertsCmd = db.GetStoredProcCommand("SP_GetListofAlerts");
        //        db.AddInParameter(getListOfAlertsCmd, "@Type", DbType.String, type);
        //        dsAlertList = db.ExecuteDataSet(getListOfAlertsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetListofAlerts()");

        //        //object[] objects = new object[1];
        //        //objects[0] = rmId;

        //        //FunctionInfo = exBase.AddObject(FunctionInfo, null);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsAlertList;
        //}

        //public List<AlertsSetupVo> GetListofEventsSubscribedByCustomer(int CustomerID, int EventID)
        //{
        //    List<AlertsSetupVo> alertSetupList = null;
        //    AlertsSetupVo alertVo;// = new CustomerCashSavingsPortfolioVo();
        //    Database db;
        //    DbCommand getCustSubscribedEventsCmd;
        //    DataSet dsGetCustSubscribedEvents;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustSubscribedEventsCmd = db.GetStoredProcCommand("SP_GetCustomerSubscribedEvents");
        //        db.AddInParameter(getCustSubscribedEventsCmd, "@C_CustomerId", DbType.Int32, CustomerID);
        //        db.AddInParameter(getCustSubscribedEventsCmd, "@EventID", DbType.Int32, EventID);

        //        dsGetCustSubscribedEvents = db.ExecuteDataSet(getCustSubscribedEventsCmd);
        //        if (dsGetCustSubscribedEvents.Tables[0].Rows.Count > 0)
        //        {
        //            alertSetupList = new List<AlertsSetupVo>();
        //            foreach (DataRow dr in dsGetCustSubscribedEvents.Tables[0].Rows)
        //            {
        //                alertVo = new AlertsSetupVo();

        //                alertVo.EventSetupID = Int64.Parse(dr["AES_EventSetupID"].ToString());
        //                alertVo.EventID = Int32.Parse(dr["AEL_EventID"].ToString());
        //                alertVo.CustomerID = Int32.Parse(dr["AES_TargetID"].ToString());
        //                alertVo.DeliveryMode = dr["AES_DeliveryMode"].ToString();

        //                if (dr["AES_EndDate"].ToString() != "")
        //                {
        //                    alertVo.EndDate = DateTime.Parse(dr[""].ToString());
        //                }

        //                if (dr["AES_EventMessage"].ToString() != "")
        //                {
        //                    alertVo.EventMessage = dr["AES_EventMessage"].ToString();
        //                }

        //                if (dr["CL_CycleID"].ToString() != "")
        //                {
        //                    alertVo.FrequencyID = Int16.Parse(dr["CL_CycleID"].ToString());
        //                }

        //                if (dr["AEL_EventCode"].ToString() != "")
        //                {
        //                    alertVo.EventName = dr["AEL_EventCode"].ToString();
        //                }

        //                alertVo.Reminder = dr["AEL_Reminder"].ToString();
        //                alertVo.EventSubscriptionDate = DateTime.Parse(dr["AES_EventSubscriptionDate"].ToString());
        //                if (dr["AES_NextOccurence"].ToString() != "")
        //                    alertVo.NextOccurence = DateTime.Parse(dr["AES_NextOccurence"].ToString());
        //                if (dr["AES_LastOccurence"].ToString() != "")
        //                    alertVo.LastOccurence = DateTime.Parse(dr["AES_LastOccurence"].ToString());
        //                if (dr["AES_SchemeID"].ToString() != "")
        //                {
        //                    alertVo.SchemeID = Int32.Parse(dr["AES_SchemeID"].ToString());
        //                }
        //                alertVo.SentToQueue = dr["AES_SentToQueue"].ToString();
        //                alertVo.AllFieldNames = dr["AllFieldNames"].ToString();

        //                alertSetupList.Add(alertVo);
        //            }
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetListofEventsSubscribedByCustomer()");

        //        object[] objects = new object[2];
        //        objects[0] = CustomerID;
        //        objects[1] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return alertSetupList;
        //}

        //public DataSet GetTableFieldNames(int EventID)
        //{
        //    Database db;
        //    DbCommand getTableFieldsCmd;
        //    DataSet dsGetTableFields;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getTableFieldsCmd = db.GetStoredProcCommand("SP_GetTableAndFieldNamesFromEventLookup");
        //        db.AddInParameter(getTableFieldsCmd, "@EventID", DbType.Int32, EventID);
        //        dsGetTableFields = db.ExecuteDataSet(getTableFieldsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetTableFieldNames()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetTableFields;
        //}

        //public DataSet GetCustomerSchemeList(string TableName, string CustomerIDField, int CustomerID, string EventCode, string SchemeIDFieldName, string TypeCodeField, string StartDateField, string EndDateField, string SystematicDateField, string FrequencyField)
        //{
        //    Database db;
        //    DbCommand getCustomerSchemeListCmd;
        //    DataSet dsGetCustomerSchemeList;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerSchemeListCmd = db.GetStoredProcCommand("SP_GetCustomerSchemeList");
        //        db.AddInParameter(getCustomerSchemeListCmd, "@TableName", DbType.String, TableName);
        //        db.AddInParameter(getCustomerSchemeListCmd, "@CustomerFieldName", DbType.String, CustomerIDField);
        //        db.AddInParameter(getCustomerSchemeListCmd, "@TypeCodeFieldName", DbType.String, TypeCodeField);
        //        db.AddInParameter(getCustomerSchemeListCmd, "@CustomerId", DbType.Int32, CustomerID);
        //        db.AddInParameter(getCustomerSchemeListCmd, "@TypeCode", DbType.String, EventCode);
        //        db.AddInParameter(getCustomerSchemeListCmd, "@SchemeIDFieldName", DbType.String, SchemeIDFieldName);

        //        if (StartDateField != "")
        //            db.AddInParameter(getCustomerSchemeListCmd, "@StartDateField", DbType.String, StartDateField);
        //        else
        //            db.AddInParameter(getCustomerSchemeListCmd, "@StartDateField", DbType.String, DBNull.Value);

        //        if (EndDateField != "")
        //            db.AddInParameter(getCustomerSchemeListCmd, "@EndDateField", DbType.String, EndDateField);
        //        else
        //            db.AddInParameter(getCustomerSchemeListCmd, "@EndDateField", DbType.String, DBNull.Value);

        //        if (SystematicDateField != "")
        //            db.AddInParameter(getCustomerSchemeListCmd, "@SystematicDateField", DbType.String, SystematicDateField);
        //        else
        //            db.AddInParameter(getCustomerSchemeListCmd, "@SystematicDateField", DbType.String, DBNull.Value);

        //        if (FrequencyField != "")
        //            db.AddInParameter(getCustomerSchemeListCmd, "@FrequencyField", DbType.String, FrequencyField);
        //        else
        //            db.AddInParameter(getCustomerSchemeListCmd, "@FrequencyField", DbType.String, DBNull.Value);

        //        dsGetCustomerSchemeList = db.ExecuteDataSet(getCustomerSchemeListCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerSchemeList()");

        //        object[] objects = new object[3];
        //        objects[0] = TableName;
        //        objects[1] = CustomerIDField;
        //        objects[2] = CustomerID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerSchemeList;
        //}

        //public bool SaveAlert(int EventID, int CustomerId, DateTime NextOccurence, int Frequency)
        //{
        //    bool blResult = false;

        //    Database db;
        //    DbCommand addEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        addEventCmd = db.GetStoredProcCommand("SP_AddCustomerEvent");

        //        db.AddInParameter(addEventCmd, "@C_CustomerId", DbType.Int32, CustomerId);
        //        db.AddInParameter(addEventCmd, "@AEL_EventID", DbType.Int32, EventID);
        //        db.AddInParameter(addEventCmd, "@AES_SchemeID", DbType.String, DBNull.Value);
        //        db.AddInParameter(addEventCmd, "@AES_NextOccurence", DbType.DateTime, NextOccurence);
        //        db.AddInParameter(addEventCmd, "@CL_CycleID", DbType.Int32, Frequency);
        //        // The Below code needs to be changed... The Right UserId has to be sent
        //        db.AddInParameter(addEventCmd, "@AES_CreatedFor", DbType.Int32, CustomerId);
        //        db.AddInParameter(addEventCmd, "@CCSP_CreatedBy", DbType.Int32, CustomerId);
        //        db.AddInParameter(addEventCmd, "@CCSP_ModifiedBy", DbType.Int32, CustomerId);

        //        db.ExecuteNonQuery(addEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:SaveAlert()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool SaveAlert(int EventID, string eventMessage, int targetID, int custId, int SchemeID, DateTime NextOccurence, int Frequency, int userId)
        //{
        //    bool blResult = false;

        //    Database db;
        //    DbCommand addEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        addEventCmd = db.GetStoredProcCommand("SP_AddCustomerEvent");

        //        db.AddInParameter(addEventCmd, "@C_CustomerId", DbType.Int32, targetID);
        //        db.AddInParameter(addEventCmd, "@AES_TargetId", DbType.Int32, targetID);
        //        db.AddInParameter(addEventCmd, "@EventMessage", DbType.String, eventMessage);
        //        db.AddInParameter(addEventCmd, "@AEL_EventID", DbType.Int32, EventID);
        //        db.AddInParameter(addEventCmd, "@AES_SchemeID", DbType.Int32, SchemeID);
        //        db.AddInParameter(addEventCmd, "@AES_NextOccurence", DbType.DateTime, NextOccurence);
        //        if (Frequency != 0)
        //            db.AddInParameter(addEventCmd, "@CL_CycleID", DbType.Int32, Frequency);
        //        else
        //            db.AddInParameter(addEventCmd, "@CL_CycleID", DbType.Int32, DBNull.Value);
        //        // The Below code needs to be changed... The Right UserId has to be sent
        //        db.AddInParameter(addEventCmd, "@AES_CreatedFor", DbType.Int32, custId);
        //        db.AddInParameter(addEventCmd, "@CCSP_CreatedBy", DbType.Int32, userId);
        //        db.AddInParameter(addEventCmd, "@CCSP_ModifiedBy", DbType.Int32, userId);

        //        db.ExecuteNonQuery(addEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:SaveAlert()");

        //        object[] objects = new object[3];
        //        objects[0] = EventID;
        //        objects[1] = custId;
        //        objects[2] = SchemeID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool SaveAlert(int customerId, string eventMessage, int targetId, int SchemeID, string Condition, double PresetValue, int EventID, int userId)
        //{
        //    bool blResult = false;

        //    Database db;
        //    DbCommand addEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        addEventCmd = db.GetStoredProcCommand("SP_AddCustomerConditionalEvent");

        //        db.AddInParameter(addEventCmd, "@C_CustomerId", DbType.Int32, customerId);
        //        db.AddInParameter(addEventCmd, "@EventMessage", DbType.String, eventMessage);
        //        db.AddInParameter(addEventCmd, "@AES_TargetId", DbType.Int32, targetId);
        //        db.AddInParameter(addEventCmd, "@AEL_EventID", DbType.Int32, EventID);
        //        db.AddInParameter(addEventCmd, "@AES_SchemeID", DbType.Int32, SchemeID);
        //        db.AddInParameter(addEventCmd, "@Condition", DbType.String, Condition);
        //        db.AddInParameter(addEventCmd, "@PresetValue", DbType.Double, PresetValue);
        //        db.AddInParameter(addEventCmd, "@UserId", DbType.Int32, userId);

        //        db.ExecuteNonQuery(addEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:SaveAlert()");

        //        object[] objects = new object[5];
        //        objects[0] = EventID;
        //        objects[1] = SchemeID;
        //        objects[2] = Condition;
        //        objects[3] = customerId;
        //        objects[4] = PresetValue;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool SaveAlert(int customerId,string eventMessage,int targetId, int SchemeID, int EventID, int userId)
        //{
        //    bool blResult = false;

        //    Database db;
        //    DbCommand addEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        addEventCmd = db.GetStoredProcCommand("SP_AddCustomerTranxEvent");

        //        db.AddInParameter(addEventCmd, "@CustomerID", DbType.Int32, customerId);
        //        db.AddInParameter(addEventCmd, "@EventMessage", DbType.String, eventMessage);
        //        db.AddInParameter(addEventCmd, "@TargetId", DbType.Int32, targetId);
        //        db.AddInParameter(addEventCmd, "@EventID", DbType.Int32, EventID);
        //        db.AddInParameter(addEventCmd, "@SchemeID", DbType.Int32, SchemeID);
        //        // The Below code needs to be changed... The Right UserId has to be sent
        //        db.AddInParameter(addEventCmd, "@CreatedFor", DbType.Int32, customerId);
        //        db.AddInParameter(addEventCmd, "@CCSP_CreatedBy", DbType.Int32, userId);
        //        db.AddInParameter(addEventCmd, "@CCSP_ModifiedBy", DbType.Int32, userId);

        //        db.ExecuteNonQuery(addEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:SaveAlert()");

        //        object[] objects = new object[3];
        //        objects[0] = EventID;
        //        objects[1] = customerId;
        //        objects[2] = SchemeID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public DataSet CheckDOBSubscribed(int EventID, int CustomerId)
        //{
        //    DataSet dsDOB = new DataSet();
        //    Database db;
        //    DbCommand getCheckDOBCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCheckDOBCmd = db.GetStoredProcCommand("SP_CheckDOBSubscribed");
        //        db.AddInParameter(getCheckDOBCmd, "@EventID", DbType.Int32, EventID);
        //        db.AddInParameter(getCheckDOBCmd, "@CustomerID", DbType.Int32, CustomerId);
        //        dsDOB = db.ExecuteDataSet(getCheckDOBCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:CheckDOBSubscribed()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsDOB;
        //}

        //public DataSet GetSubscribedSchemeList(int EventID, int CustomerId)
        //{
        //    DataSet dsSubscribedSchemes;
        //    Database db;
        //    DbCommand getSubscribedSchemeCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSubscribedSchemeCmd = db.GetStoredProcCommand("SP_GetSubscribedSchemeList");
        //        db.AddInParameter(getSubscribedSchemeCmd, "@EventID", DbType.Int32, EventID);
        //        db.AddInParameter(getSubscribedSchemeCmd, "@CustomerID", DbType.Int32, CustomerId);
        //        dsSubscribedSchemes = db.ExecuteDataSet(getSubscribedSchemeCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSubscribedSchemeList()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsSubscribedSchemes;
        //}

        //public string GetEventCode(int EventID)
        //{
        //    string EventCode = string.Empty;
        //    DataSet dsEventCode;
        //    Database db;
        //    DbCommand getEventCodeCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getEventCodeCmd = db.GetStoredProcCommand("SP_GetEventCodeFromID");
        //        db.AddInParameter(getEventCodeCmd, "@EventID", DbType.Int32, EventID);
        //        dsEventCode = db.ExecuteDataSet(getEventCodeCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSubscribedSchemeList()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    if (dsEventCode.Tables[0].Rows.Count > 0)
        //    {
        //        EventCode = dsEventCode.Tables[0].Rows[0]["AEL_EventCode"].ToString();
        //    }

        //    return EventCode;
        //}

        //public bool DeleteEvent(long EventSetupID)
        //{
        //    bool blResult = false;
        //    Database db;
        //    DbCommand deleteDateEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        deleteDateEventCmd = db.GetStoredProcCommand("SP_DeleteEvent");
        //        db.AddInParameter(deleteDateEventCmd, "@EventSetupID", DbType.Int64, EventSetupID);
        //        db.ExecuteNonQuery(deleteDateEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:DeleteDateEvent()");

        //        object[] objects = new object[1];
        //        objects[0] = EventSetupID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool DeleteConditionalEvent(long EventSetupID, int EventID, int CustomerID, int SchemeID)
        //{
        //    bool blResult = false;
        //    Database db;
        //    DbCommand deleteConditionalEventCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        deleteConditionalEventCmd = db.GetStoredProcCommand("SP_DeleteConditionalEvent");
        //        db.AddInParameter(deleteConditionalEventCmd, "@EventSetupID", DbType.Int64, EventSetupID);
        //        db.AddInParameter(deleteConditionalEventCmd, "@EventID", DbType.Int32, EventID);
        //        db.AddInParameter(deleteConditionalEventCmd, "@CustomerID", DbType.Int32, CustomerID);
        //        db.AddInParameter(deleteConditionalEventCmd, "@SchemeID", DbType.Int32, SchemeID);
        //        db.ExecuteNonQuery(deleteConditionalEventCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:DeleteConditionalEvent()");

        //        object[] objects = new object[1];
        //        objects[0] = EventSetupID;
        //        objects[1] = EventSetupID;
        //        objects[2] = EventSetupID;
        //        objects[3] = EventSetupID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public DataSet GetFrequencyList()
        //{
        //    DataSet ds;
        //    Database db;
        //    DbCommand getFrequencyListCmd;

        //    try 
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getFrequencyListCmd = db.GetStoredProcCommand("SP_GetFrequencyList");
        //        ds = db.ExecuteDataSet(getFrequencyListCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetFrequencyList()");

        //        //object[] objects = new object[1];
        //        //objects[0] = EventSetupID;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return ds;
        //}

        //public bool IsReminder(int EventID)
        //{
        //    bool blResult = false;
        //    DataSet ds;
        //    Database db;
        //    DbCommand isReminderCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        isReminderCmd = db.GetStoredProcCommand("SP_CheckEventReminder");
        //        db.AddInParameter(isReminderCmd, "@EventID", DbType.Int32, EventID);
        //        ds = db.ExecuteDataSet(isReminderCmd);

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["AEL_Reminder"].ToString().ToLower() == "true")
        //                blResult = true;
        //            else if (ds.Tables[0].Rows[0]["AEL_Reminder"].ToString().ToLower() == "false")
        //                blResult = false;
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:IsReminder()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool CustEventExists(int customerId, int EventlookupID, int schemeId, bool Reminder)
        //{
        //    bool blResult = false;
        //    DataSet ds;
        //    Database db;
        //    DbCommand custEventExistsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        custEventExistsCmd = db.GetStoredProcCommand("SP_CheckCustEventExists");
        //        db.AddInParameter(custEventExistsCmd, "@C_CustomerId", DbType.Int32, customerId);
        //        db.AddInParameter(custEventExistsCmd, "@AEL_EventID", DbType.Int32, EventlookupID);
        //        db.AddInParameter(custEventExistsCmd, "@AES_SchemeID", DbType.Int32, schemeId);
        //        if (Reminder)
        //            db.AddInParameter(custEventExistsCmd, "@AES_Reminder", DbType.Int32, 1);
        //        else
        //            db.AddInParameter(custEventExistsCmd, "@AEL_Reminder", DbType.Int32, 0);

        //        ds = db.ExecuteDataSet(custEventExistsCmd);

        //        if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
        //            blResult = true;
        //        else
        //            blResult = false;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:CustEventExists()");

        //        object[] objects = new object[4];
        //        objects[0] = customerId;
        //        objects[1] = EventlookupID;
        //        objects[2] = schemeId;
        //        objects[3] = Reminder;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public DateTime GetEventNextOcurrence(int customerId, string EventCode, int SchemeID, string Reminder)
        //{
        //    DateTime dtNxtOccr = new DateTime();
        //    DataSet ds;
        //    Database db;
        //    DbCommand getEventNextOcurrenceCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getEventNextOcurrenceCmd = db.GetStoredProcCommand("SP_GetEventNextOcurrence");
        //        db.AddInParameter(getEventNextOcurrenceCmd, "@C_CustomerId", DbType.Int32, customerId);
        //        db.AddInParameter(getEventNextOcurrenceCmd, "@AEL_EventCode", DbType.String, EventCode);
        //        db.AddInParameter(getEventNextOcurrenceCmd, "@AES_SchemeID", DbType.Int32, SchemeID);
        //        if (Reminder == "false")
        //            db.AddInParameter(getEventNextOcurrenceCmd, "@AEL_Reminder", DbType.Int32, 0);
        //        else
        //            db.AddInParameter(getEventNextOcurrenceCmd, "@AEL_Reminder", DbType.Int32, 1);

        //        ds = db.ExecuteDataSet(getEventNextOcurrenceCmd);

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtNxtOccr = DateTime.Parse(ds.Tables[0].Rows[0]["AES_NextOccurence"].ToString());
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetEventNextOcurrence()");

        //        object[] objects = new object[4];
        //        objects[0] = customerId;
        //        objects[1] = EventCode;
        //        objects[2] = SchemeID;
        //        objects[3] = Reminder;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dtNxtOccr;
        //}

        //public DataSet GetSchemeDetailsFromMasterTable(string Table1, string CustomerIDField, string SchemeIDField, int customerId)
        //{
        //    DataSet dsSchemeDetails;
        //    Database db;
        //    DbCommand getSchemeDetailsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSchemeDetailsCmd = db.GetStoredProcCommand("SP_GetDOBDetailsFromMasterTable");
        //        db.AddInParameter(getSchemeDetailsCmd, "@TableName", DbType.String, Table1);
        //        db.AddInParameter(getSchemeDetailsCmd, "@CustomerIDField", DbType.String, CustomerIDField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@SchemeIDField", DbType.String, SchemeIDField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@C_CustomerId", DbType.Int32, customerId);

        //        dsSchemeDetails = db.ExecuteDataSet(getSchemeDetailsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSchemeDetailsFromMasterTable()");

        //        object[] objects = new object[4];
        //        objects[0] = customerId;
        //        objects[1] = Table1;
        //        objects[2] = CustomerIDField;
        //        objects[3] = SchemeIDField;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsSchemeDetails;
        //}

        //public DataSet GetSchemeDetailsFromMasterTable(string Table1, string CustomerIDField, string SchemeIDField, string SchemeDateField, string StartDateField, string EndDateField, string SystematicDateField, string FrequencyField, int customerId, int SchemeID)
        //{
        //    DataSet dsSchemeDetails;
        //    Database db;
        //    DbCommand getSchemeDetailsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSchemeDetailsCmd = db.GetStoredProcCommand("SP_GetSchemeDetailsFromMasterTable");
        //        db.AddInParameter(getSchemeDetailsCmd, "@TableName", DbType.String, Table1);
        //        db.AddInParameter(getSchemeDetailsCmd, "@CustomerIDField", DbType.String, CustomerIDField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@SchemeIDField", DbType.String, SchemeIDField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@SchemeDateField", DbType.String, SchemeDateField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@StartDateField", DbType.String, StartDateField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@EndDateField", DbType.String, EndDateField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@SystematicDateField", DbType.String, SystematicDateField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@FrequencyField", DbType.String, FrequencyField);
        //        db.AddInParameter(getSchemeDetailsCmd, "@C_CustomerId", DbType.Int32, customerId);
        //        db.AddInParameter(getSchemeDetailsCmd, "@SchemeID", DbType.Int32, SchemeID);

        //        dsSchemeDetails = db.ExecuteDataSet(getSchemeDetailsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSchemeDetailsFromMasterTable()");

        //        object[] objects = new object[10];
        //        objects[0] = customerId;
        //        objects[1] = Table1;
        //        objects[2] = CustomerIDField;
        //        objects[3] = SchemeIDField;
        //        objects[4] = SchemeDateField;
        //        objects[5] = StartDateField;
        //        objects[6] = EndDateField;
        //        objects[7] = SystematicDateField;
        //        objects[8] = FrequencyField;
        //        objects[9] = SchemeID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsSchemeDetails;
        //}

        //public bool UpdateGridEventDetails(long EventSetupID, DateTime NextOccurMod)
        //{
        //    bool blResult = false;
        //    Database db;
        //    DbCommand updateEventDetailsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        updateEventDetailsCmd = db.GetStoredProcCommand("SP_UpdateGridEventDetails");
        //        db.AddInParameter(updateEventDetailsCmd, "@AES_EventSetupID", DbType.Int64, EventSetupID);
        //        db.AddInParameter(updateEventDetailsCmd, "@AES_NextOccurence", DbType.DateTime, NextOccurMod);

        //        db.ExecuteNonQuery(updateEventDetailsCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:UpdateGridEventDetails()");

        //        object[] objects = new object[2];
        //        objects[0] = EventSetupID;
        //        objects[1] = NextOccurMod;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool UpdateGridEventDataConditions(int customerId, int SchemeID, int EventID, string Condition, double PresetValue)
        //{
        //    bool blResult = false;
        //    Database db;
        //    DbCommand updateEventDataConditionsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        updateEventDataConditionsCmd = db.GetStoredProcCommand("SP_UpdateGridEventDataConditions");
        //        db.AddInParameter(updateEventDataConditionsCmd, "@CustomerID", DbType.Int32, customerId);
        //        db.AddInParameter(updateEventDataConditionsCmd, "@SchemeID", DbType.Int32, SchemeID);
        //        db.AddInParameter(updateEventDataConditionsCmd, "@EventID", DbType.Int32, EventID);
        //        db.AddInParameter(updateEventDataConditionsCmd, "@Condition", DbType.String, Condition);
        //        db.AddInParameter(updateEventDataConditionsCmd, "@PresetValue", DbType.Double, PresetValue);

        //        db.ExecuteNonQuery(updateEventDataConditionsCmd);
        //        blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:UpdateGridEventDataConditions()");

        //        object[] objects = new object[5];
        //        objects[0] = customerId;
        //        objects[1] = SchemeID;
        //        objects[2] = EventID;
        //        objects[3] = Condition;
        //        objects[4] = PresetValue;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public DataSet GetEventDetails(long EventSetupID)
        //{
        //    DataSet ds;
        //    Database db;
        //    DbCommand getEventDetailsCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getEventDetailsCmd = db.GetStoredProcCommand("SP_GetEventDetailsFromID");
        //        db.AddInParameter(getEventDetailsCmd, "@AES_EventSetupID", DbType.Int64, EventSetupID);
        //        ds = db.ExecuteDataSet(getEventDetailsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetEventDetails()");

        //        object[] objects = new object[1];
        //        objects[0] = EventSetupID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return ds;
        //}

        //public List<AlertsNotificationVo> GetCustomerNotifications(int CustomerId)
        //{
        //    List<AlertsNotificationVo> alertNotificationList = null;
        //    AlertsNotificationVo alertNotificationVo;
        //    Database db;
        //    DbCommand getCustomerNotificationsCmd;
        //    DataSet dsGetCustomerNotifications;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerNotificationsCmd = db.GetStoredProcCommand("SP_GetCustomerNotifications");
        //        db.AddInParameter(getCustomerNotificationsCmd, "@C_CustomerId", DbType.Int32, CustomerId);

        //        dsGetCustomerNotifications = db.ExecuteDataSet(getCustomerNotificationsCmd);
        //        if (dsGetCustomerNotifications.Tables[0].Rows.Count > 0)
        //        {
        //            alertNotificationList = new List<AlertsNotificationVo>();
        //            foreach (DataRow dr in dsGetCustomerNotifications.Tables[0].Rows)
        //            {
        //                alertNotificationVo = new AlertsNotificationVo();

        //                alertNotificationVo.NotificationID = Int64.Parse(dr["AEN_EventQueueID"].ToString());
        //                alertNotificationVo.Category = dr["AEL_EventType"].ToString();
        //                alertNotificationVo.EventSetupID = Int64.Parse(dr["AES_EventSetupID"].ToString());
        //                alertNotificationVo.EventID = Int32.Parse(dr["AEL_EventID"].ToString());
        //                alertNotificationVo.CustomerID = Int32.Parse(dr["AEN_TargetID"].ToString());
        //                alertNotificationVo.EventCode = dr["AEL_EventCode"].ToString();
        //                alertNotificationVo.EventMessage = dr["AEN_EventMessage"].ToString();
        //                if (dr["AEN_SchemeID"].ToString() != "")
        //                {
        //                    alertNotificationVo.SchemeID = Int32.Parse(dr["AEN_SchemeID"].ToString());
        //                }
        //                alertNotificationVo.PopulatedDate = DateTime.Parse(dr["AEN_PopulatedDate"].ToString());
        //                alertNotificationVo.ModeId = Int16.Parse(dr["ADML_ModeId"].ToString());
        //                alertNotificationVo.IsAlerted = dr["AEN_IsAlerted"].ToString();
        //                alertNotificationVo.Reminder = dr["AEL_Reminder"].ToString();
        //                //alertNotificationVo.IsDeleted = Int16.Parse(dr["ADML_ModeId"].ToString());

        //                alertNotificationList.Add(alertNotificationVo);
        //            }
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerNotifications()");

        //        object[] objects = new object[1];
        //        objects[0] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return alertNotificationList;
        //}

        //public DataSet GetCustomerConditionalSchemeList(string TableName, string CustomerIDField, string SchemeIDFieldName, string CurrentValueFieldName, int CustomerId, string EventCode, string SchemeNameFieldName)
        //{
        //    Database db;
        //    DbCommand getCustomerConditionalSchemeListCmd;
        //    DataSet dsGetCustomerConditionalSchemeList;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerConditionalSchemeListCmd = db.GetStoredProcCommand("SP_GetCustomerConditionalSchemeList");
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@TableName", DbType.String, TableName);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@CustomerFieldName", DbType.String, CustomerIDField);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@SchemeIDFieldName", DbType.String, SchemeIDFieldName);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@CurrentValueFieldName", DbType.String, CurrentValueFieldName);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@CustomerId", DbType.Int32, CustomerId);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@EventCode", DbType.String, EventCode);
        //        db.AddInParameter(getCustomerConditionalSchemeListCmd, "@SchemeNameFieldName", DbType.String, SchemeNameFieldName);

        //        dsGetCustomerConditionalSchemeList = db.ExecuteDataSet(getCustomerConditionalSchemeListCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerSchemeList()");

        //        object[] objects = new object[6];
        //        objects[0] = TableName;
        //        objects[1] = CustomerIDField;
        //        objects[2] = SchemeIDFieldName;
        //        objects[3] = CurrentValueFieldName;
        //        objects[4] = CustomerId;
        //        objects[5] = EventCode;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerConditionalSchemeList;
        //}

        //public DataSet GetCustomerDataConditions(int customerId, int SchemeID, int EventID)
        //{
        //    Database db;
        //    DbCommand getCustomerDataConditionsCmd;
        //    DataSet dsGetCustomerDataConditions;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerDataConditionsCmd = db.GetStoredProcCommand("SP_GetCustomerDataConditions");
        //        db.AddInParameter(getCustomerDataConditionsCmd, "@CustomerID", DbType.Int32, customerId);
        //        db.AddInParameter(getCustomerDataConditionsCmd, "@SchemeID", DbType.Int32, SchemeID);
        //        db.AddInParameter(getCustomerDataConditionsCmd, "@EventID", DbType.String, EventID);

        //        dsGetCustomerDataConditions = db.ExecuteDataSet(getCustomerDataConditionsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerDataConditions()");

        //        object[] objects = new object[3];
        //        objects[0] = customerId;
        //        objects[1] = SchemeID;
        //        objects[2] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerDataConditions;
        //}

        //public DataSet GetSchemeCurrentValue(string SchemeTableName, string CustomerIDFieldName, string SchemeIDFieldName, string CurrentValueFieldName, int customerId, int SchemeID)
        //{
        //    Database db;
        //    DbCommand getSchemeCurrentValueCmd;
        //    DataSet dsGetSchemeCurrentValue;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSchemeCurrentValueCmd = db.GetStoredProcCommand("SP_GetSchemeCurrentValue");
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@SchemeTableName", DbType.String, SchemeTableName);
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@CustomerIDFieldName", DbType.String, CustomerIDFieldName);
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@SchemeIDFieldName", DbType.String, SchemeIDFieldName);
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@CurrentValueFieldName", DbType.String, CurrentValueFieldName);
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@CustomerID", DbType.Int32, customerId);
        //        db.AddInParameter(getSchemeCurrentValueCmd, "@SchemeID", DbType.Int32, SchemeID);

        //        dsGetSchemeCurrentValue = db.ExecuteDataSet(getSchemeCurrentValueCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSchemeCurrentValue()");

        //        object[] objects = new object[6];
        //        objects[0] = SchemeTableName;
        //        objects[1] = CustomerIDFieldName;
        //        objects[2] = SchemeIDFieldName;
        //        objects[3] = CurrentValueFieldName;
        //        objects[4] = customerId;
        //        objects[5] = SchemeID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetSchemeCurrentValue;
        //}

        //public DataSet GetCustomerDashboard(int CustomerID)
        //{
        //    Database db;
        //    DbCommand getCustomerDashboardCmd;
        //    DataSet dsGetCustomerDashboard;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerDashboardCmd = db.GetStoredProcCommand("SP_GetCustomerAlertDashboard");
        //        db.AddInParameter(getCustomerDashboardCmd, "@CustomerID", DbType.Int32, CustomerID);

        //        dsGetCustomerDashboard = db.ExecuteDataSet(getCustomerDashboardCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerDashboard()");

        //        object[] objects = new object[1];
        //        objects[0] = CustomerID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerDashboard;
        //}

        //public DataSet GetMetatableDetails(string primaryKey)
        //{
        //    Database db;
        //    DbCommand getMetatableCmd;
        //    DataSet dsGetMetatableDetails;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getMetatableCmd = db.GetStoredProcCommand("SP_GetMetatableDetails");
        //        db.AddInParameter(getMetatableCmd, "@WM_PrimaryKey", DbType.String, primaryKey);

        //        dsGetMetatableDetails = db.ExecuteDataSet(getMetatableCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetMetatableDetails()");

        //        object[] objects = new object[1];
        //        objects[0] = primaryKey;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetMetatableDetails;
        //}

        //public DataSet GetSchemeDescription(string description, string tableName, string metatablePrimaryKey, int SchemeId)
        //{
        //    Database db;
        //    DbCommand getSchemeDescriptionCmd;
        //    DataSet dsGetSchemeDescription;
        //    string query;

        //    try
        //    {
        //        query = "select " + description + " from " + tableName + " where " + metatablePrimaryKey + " = " + SchemeId.ToString();
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getSchemeDescriptionCmd = db.GetSqlStringCommand(query);


        //        dsGetSchemeDescription = db.ExecuteDataSet(getSchemeDescriptionCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetSchemeDescription()");

        //        object[] objects = new object[4];
        //        objects[0] = description;
        //        objects[1] = tableName;
        //        objects[2] = metatablePrimaryKey;
        //        objects[3] = SchemeId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetSchemeDescription;
        //}

        //public DataSet GetCustomerPortfolioSchemeList(int customerId)
        //{
        //    Database db;
        //    DbCommand getCustomerPortfolioSchemeListCmd;
        //    DataSet dsGetCustomerPortfolioSchemeList;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerPortfolioSchemeListCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioSchemeList");
        //        db.AddInParameter(getCustomerPortfolioSchemeListCmd, "@CustomerID", DbType.Int32, customerId);

        //        dsGetCustomerPortfolioSchemeList = db.ExecuteDataSet(getCustomerPortfolioSchemeListCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerPortfolioSchemeList()");

        //        object[] objects = new object[1];
        //        objects[0] = customerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerPortfolioSchemeList;
        //}

        //public bool DeleteAlertNotification(long notificationId)
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand deleteAlertNotificationCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        deleteAlertNotificationCmd = db.GetStoredProcCommand("SP_DeleteAlertNotification");
        //        db.AddInParameter(deleteAlertNotificationCmd, "@NotificationID", DbType.Int64, notificationId);
        //        db.ExecuteNonQuery(deleteAlertNotificationCmd);
        //        bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:DeleteAlertNotification()");

        //        object[] objects = new object[1];
        //        objects[0] = notificationId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return bResult;

        //}

        //public bool ExecuteDateAlert()
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand executeDateAlertCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        executeDateAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForDate");

        //        db.ExecuteNonQuery(executeDateAlertCmd);
        //        bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteDateAlert()");

        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return bResult;
        //}

        //public bool ExecuteDataAlert()
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand executeDataAlertCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        executeDataAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForData");

        //        db.ExecuteNonQuery(executeDataAlertCmd);
        //        bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteDataAlert()");

        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return bResult;
        //}

        //public bool ExecuteTransactionAlert()
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand executeTransactionAlertCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        executeTransactionAlertCmd = db.GetStoredProcCommand("sproc_AlertDiscoveryForTrans");

        //        db.ExecuteNonQuery(executeTransactionAlertCmd);
        //        bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:ExecuteTransactionAlert()");

        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return bResult;
        //}

        //public DataSet GetCustomerDashboardAlerts(int customerId)
        //{
        //    Database db;
        //    DbCommand getCustomerAlertsCmd;
        //    DataSet dsGetCustomerAlertsList;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        getCustomerAlertsCmd = db.GetStoredProcCommand("SP_GetCustomerDashboardAlerts");
        //        db.AddInParameter(getCustomerAlertsCmd, "@CustomerID", DbType.Int32, customerId);

        //        dsGetCustomerAlertsList = db.ExecuteDataSet(getCustomerAlertsCmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsDao.cs:GetCustomerDashboardAlerts()");

        //        object[] objects = new object[1];
        //        objects[0] = customerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsGetCustomerAlertsList;
        //}
        //
        #endregion

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
        public bool SaveAdviserSIPReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSIPReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSIPReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSIPReminderAlert");
                db.AddInParameter(saveSIPReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserSWPReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSWPReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSWPReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSWPReminderAlert");
                db.AddInParameter(saveSWPReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserAnniversaryReminderAlert(int rmId, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveAnniversaryReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveAnniversaryReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserAnniversaryReminderAlert");
                db.AddInParameter(saveAnniversaryReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserDOBReminderAlert(int rmId, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveDOBReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveDOBReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserDOBReminderAlert");
                db.AddInParameter(saveDOBReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserELSSMaturityReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveELSSMaturityReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveELSSMaturityReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserELSSMaturityReminderAlert");
                db.AddInParameter(saveELSSMaturityReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserFDMaturityReminderAlert(int rmId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveFDMaturityReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveFDMaturityReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserFDMaturityReminderAlert");
                db.AddInParameter(saveFDMaturityReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserFDRecurringDepositReminderAlert(int rmId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveFDRecurringDepositReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveFDRecurringDepositReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserFDRecurringDepositReminderAlert");
                db.AddInParameter(saveFDRecurringDepositReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserInsuranceReminderAlert(int rmId, int customerId, int accountId, int insuranceId, int isBulk, int reminderDays, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveInsuranceReminderAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveInsuranceReminderAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserInsurancePremiumReminderAlert");
                db.AddInParameter(saveInsuranceReminderAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserSIPConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSIPConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSIPConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSIPConfirmationAlert");
                db.AddInParameter(saveSIPConfirmationAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserSWPConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveSWPConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveSWPConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserSWPConfirmationAlert");
                db.AddInParameter(saveSWPConfirmationAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserMFDividendConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFDividendConfirmationAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFDividendConfirmationAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFDividendConfirmationAlert");
                db.AddInParameter(saveMFDividendConfirmationAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserPersonalOccurrenceAlert(int rmId, int customerId, int accountId, int personalNpId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand savePersonalOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                savePersonalOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserPersonalOccurrenceAlert");
                db.AddInParameter(savePersonalOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserPropertyOccurrenceAlert(int rmId, int customerId, int accountId, int propertyNpId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand savePropertyOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                savePropertyOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserPropertyOccurrenceAlert");
                db.AddInParameter(savePropertyOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserMFStopLossOccurrenceAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFStopLossOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFStopLossOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFStopLossOccurrenceAlert");
                db.AddInParameter(saveMFStopLossOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserMFProfitBookingOccurrenceAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveMFProfitBookingOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveMFProfitBookingOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserMFProfitBookingOccurrenceAlert");
                db.AddInParameter(saveMFProfitBookingOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserEQStopLossOccurrenceAlert(int rmId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveEQStopLossOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveEQStopLossOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserEQStopLossOccurrenceAlert");
                db.AddInParameter(saveEQStopLossOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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
        public bool SaveAdviserEQProfitBookingOccurrenceAlert(int rmId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            Database db;
            bool bResult = false;
            DbCommand saveEQProfitBookingOccurrenceAlertCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveEQProfitBookingOccurrenceAlertCmd = db.GetStoredProcCommand("SP_SaveAdviserEQProfitBookingOccurrenceAlert");
                db.AddInParameter(saveEQProfitBookingOccurrenceAlertCmd, "@RMId", DbType.Int32, rmId);
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
                objects[0] = rmId;
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

        public DataSet GetAdviserCustomerSMSAlerts(int adviserId)
        {
            
            Database db;
            DbCommand getAdviserSMSAlertsCmd;
            DataSet dsAdviserSMSAlerts;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserSMSAlertsCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerSMSAlerts");
                db.AddInParameter(getAdviserSMSAlertsCmd, "@A_AdviserId", DbType.Int32, adviserId);

                dsAdviserSMSAlerts = db.ExecuteDataSet(getAdviserSMSAlertsCmd);

                
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
                objects[0] = adviserId;

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

    }

}
