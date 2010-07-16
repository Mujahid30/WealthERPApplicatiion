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
        #region OldAlert

        //public DataSet GetListofAlerts(string type)
        //{
        //    DataSet dsAlertList = new DataSet();
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        dsAlertList = alertsDao.GetListofAlerts(type);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetListofAlerts()");

        //        object[] objects = new object[1];
        //        objects[0] = type;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsAlertList;
        //}

        //public List<AlertsSetupVo> GetListofEventsSubscribedByCustomer(int CustomerID, int EventID)
        //{
        //    List<AlertsSetupVo> alertSetupList = new List<AlertsSetupVo>();
        //    AlertsDao alertsDao = new AlertsDao();

        //    /********************************
        //    AlertsSetupVo alertVo = null;
        //    DataSet dsTableFields;
        //    DataSet dsSchemeList;

        //    string[] tables;
        //    string Table1;
        //    string Table2;

        //    string[] fieldArray;
        //    string Table1Fields;
        //    string Table2Fields;
        //    string[] customerField;
        //    string CustomerIDField;
        //    string SchemeIDField;
        //    ***********************************/
        //    try
        //    {
        //        // We have the EventID and TargetID
        //        // Get a DataSet of all Events Setup for that Customer
        //        alertSetupList = alertsDao.GetListofEventsSubscribedByCustomer(CustomerID, EventID);

        //        /***********************************************************
        //        dsTableFields = alertsDao.GetTableFieldNames(EventID);

        //        string TableName = dsTableFields.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //        string FieldNames = dsTableFields.Tables[0].Rows[0]["AEL_FieldName"].ToString();

        //        // We now split the Table Names
        //        if (TableName.Contains(','))
        //        {
        //            tables = TableName.Split(',');
        //            Table1 = tables[0].ToString();
        //            Table2 = tables[1].ToString();
        //        }
        //        else
        //        {
        //            Table1 = TableName;
        //        }


        //        // We now split Table1 and Table2 Fields
        //        if (FieldNames.Contains('|'))
        //        {
        //            fieldArray = FieldNames.Split('|');
        //            Table1Fields = fieldArray[0].ToString();
        //            Table2Fields = fieldArray[1].ToString();

        //            customerField = Table1Fields.Split(',');
        //            CustomerIDField = customerField[0].ToString();
        //            SchemeIDField = customerField[1].ToString();
        //        }
        //        else
        //        {
        //            customerField = FieldNames.Split(',');
        //            CustomerIDField = customerField[0].ToString();
        //            SchemeIDField = customerField[1].ToString();
        //        }

        //        dsSchemeList = alertsDao.GetCustomerSchemeList(Table1, CustomerIDField, CustomerID);

        //        //if (alertSetupList != null)
        //        //{
        //        //    if (dsSchemeList.Tables[0].Rows.Count > 0)
        //        //    {
        //        //        // Iterate through the list of Alerts Subscribed!
        //        //        for (int i = 0; i < alertSetupList.Count; i++)
        //        //        {
        //        //            // Contains the details of the particular alert!
        //        //            alertVo = alertSetupList[i];

        //        //            // Iterate through the list of schemes the customer has
        //        //            foreach (DataRow dr in dsSchemeList.Tables[0].Rows)
        //        //            {
        //        //                if (alertVo.SchemeID == Int32.Parse(dr[SchemeIDField].ToString()))
        //        //                {

        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        // Using using EventID retrieve the dependent Table and Fields 
        //        // from the lookup.

        //        // Return a Dataset containing the Scheme List for this Particular
        //        *************************************************************/
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetListofEventsSubscribedByCustomer()");

        //        object[] objects = new object[2];
        //        //objects[0] = type;
        //        objects[0] = CustomerID;
        //        objects[1] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    return alertSetupList;
        //}

        //public bool SaveAlert(int EventID, int CustID, DateTime NextOccurence, int Frequency)
        //{
        //    bool blResult = false;
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        if (alertsDao.SaveAlert(EventID, CustID, NextOccurence, Frequency))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:SaveAlert()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    return blResult;
        //}

        //public bool SaveAlert(int EventID, string eventMessage, int targetId, int CustID, int SchemeID, DateTime NextOccurence, int Frequency, int userId)
        //{
        //    bool blResult = false;

        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        if (alertsDao.SaveAlert(EventID,eventMessage, targetId, CustID, SchemeID, NextOccurence, Frequency, userId))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:SaveAlert()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustID;
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

        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        if (alertsDao.SaveAlert(customerId,eventMessage, targetId, SchemeID, Condition, PresetValue, EventID, userId))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:SaveAlert()");

        //        object[] objects = new object[5];
        //        objects[0] = customerId;
        //        objects[1] = SchemeID;
        //        objects[2] = Condition;
        //        objects[3] = PresetValue;
        //        objects[4] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    return blResult;
        //}

        //public bool SaveAlert(int customerId,string eventMessage, int targetId, int SchemeID, int EventID, int userId)
        //{
        //    bool blResult = false;

        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        if (alertsDao.SaveAlert(customerId,eventMessage, targetId, SchemeID, EventID, userId))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:SaveAlert()");

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
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        dsDOB = alertsDao.CheckDOBSubscribed(EventID, CustomerId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:CheckDOBSubscribed()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;
        //        objects[1] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsDOB;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="EventID">Event Look up ID</param>
        ///// <param name="CustomerId">Customer ID from Session</param>
        ///// <param name="type">Can accept values 'date', 'data', 'transactional'</param>
        ///// <returns>DataSet containing Unsubscribed Scheme IDs for the EventID input</returns>
        //public DataSet GetUnsubscribedSchemesList(int EventID, int CustomerId, string type, string EventCode)
        //{
        //    DataSet dsEntireSchemeList = null;
        //    DataSet dsTableAndFields = null;
        //    DataSet dsSubscribedSchemeList = null;
        //    DataSet dsUnSubSchemeList = new DataSet();
        //    DataSet dsSchemeDetails = new DataSet();

        //    AlertsDao alertsDao = new AlertsDao();
        //    DataTable dtUnSubs = new DataTable();

        //    string TableName = string.Empty;
        //    string FieldNames = string.Empty;
        //    string[] tables;
        //    string Table1 = string.Empty;
        //    string Table2 = string.Empty;

        //    string[] fieldArray;
        //    string Table1Fields = string.Empty;
        //    string Table2Fields = string.Empty;
        //    string[] customerField;
        //    string CustomerIDField = string.Empty;
        //    string SchemeIDField = string.Empty;
        //    string TypeCodeField = string.Empty;
        //    string SchemeDateField = string.Empty;
        //    string StartDateField = string.Empty;
        //    string EndDateField = string.Empty;
        //    string SystematicDateField = string.Empty;
        //    string FrequencyField = string.Empty;
        //    string CurrentValueFieldName = string.Empty;
        //    string ConditionCustomerIDField = string.Empty;
        //    string ConditionSchemeIDField = string.Empty;
        //    string ConditionEventIDField = string.Empty;
        //    string ConditionField = string.Empty;
        //    string ConditionPresetValueField = string.Empty;
        //    string schemeNameFieldName=string.Empty;
        //    int j = 0;

        //    try
        //    {
        //        // Get Table and Field Names
        //        dtUnSubs.Columns.Add("SchemePlanCode");
        //        dtUnSubs.Columns.Add("SchemePlan");

        //        dsTableAndFields = alertsDao.GetTableFieldNames(EventID);

        //        if (dsTableAndFields.Tables[0].Rows.Count > 0)
        //        {

        //            if (type == "date")
        //            {
        //                TableName = dsTableAndFields.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //                FieldNames = dsTableAndFields.Tables[0].Rows[0]["AEL_FieldName"].ToString();

        //                // We now split the Table Names
        //                if (TableName.Contains(','))
        //                {
        //                    tables = TableName.Split(',');
        //                    Table1 = tables[0].ToString();
        //                    Table2 = tables[1].ToString();
        //                }
        //                else
        //                {
        //                    Table1 = TableName;
        //                }

        //                // We now split Table1 and Table2 Fields
        //                if (FieldNames.Contains('|'))
        //                {
        //                    fieldArray = FieldNames.Split('|');
        //                    Table1Fields = fieldArray[0].ToString();
        //                    Table2Fields = fieldArray[1].ToString();

        //                    customerField = Table1Fields.Split(',');
        //                    CustomerIDField = customerField[0].ToString();
        //                    SchemeIDField = customerField[1].ToString();
        //                }
        //                else
        //                {
        //                    customerField = FieldNames.Split(',');
        //                    int i = customerField.Length;

        //                    CustomerIDField = "Customer.C_CustomerId";
        //                    SchemeIDField = customerField[1].ToString();
        //                    TypeCodeField = customerField[2].ToString();
        //                    if (i > 3)
        //                    {
        //                        SchemeDateField = customerField[3].ToString();
        //                        StartDateField = "CustomerMutualFundSystematicSetup.CMFSS_StartDate";
        //                        EndDateField = "CustomerMutualFundSystematicSetup.CMFSS_EndDate";
        //                        SystematicDateField = "CustomerMutualFundSystematicSetup.CMFSS_SystematicDate";
        //                        FrequencyField = "CustomerMutualFundSystematicSetup.XF_FrequencyCode";
        //                    }
        //                }

        //                // Get Schemes from the table for this Customer ID
        //                dsEntireSchemeList = alertsDao.GetCustomerSchemeList(Table1, CustomerIDField, CustomerId, EventCode, SchemeIDField, TypeCodeField, StartDateField, EndDateField, SystematicDateField, FrequencyField);
        //                dsSubscribedSchemeList = alertsDao.GetSubscribedSchemeList(EventID, CustomerId);

        //                // Filter the Unsubscribed Events and place it into the respective dataset
        //                foreach (DataRow drSubscribedSchemeList in dsSubscribedSchemeList.Tables[0].Rows)
        //                {
        //                    foreach (DataRow drEntireSchemeList in dsEntireSchemeList.Tables[0].Rows)
        //                    {
        //                        if (drEntireSchemeList["SchemeID"].ToString() == drSubscribedSchemeList["AES_SchemeID"].ToString())
        //                        {
        //                            drEntireSchemeList.Delete();
        //                        }

        //                    }

        //                    dsEntireSchemeList.AcceptChanges();
        //                }
        //            }
        //            else if (type == "data")
        //            {
        //                TableName = dsTableAndFields.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //                FieldNames = dsTableAndFields.Tables[0].Rows[0]["AEL_FieldName"].ToString();
        //                CurrentValueFieldName = dsTableAndFields.Tables[0].Rows[0]["AEL_DataConditionField"].ToString();
        //                //CurrentValueFieldName = "Customer.C_CustomerId";



        //                // We now split the Table Names
        //                if (TableName.Contains(','))
        //                {
        //                    tables = TableName.Split(',');
        //                    Table1 = tables[0].ToString();
        //                    Table2 = tables[1].ToString();
        //                }
        //                else
        //                {
        //                    Table1 = TableName;
        //                }

        //                if (Table1 == "CustomerPropertyNetPosition")
        //                {
        //                    schemeNameFieldName = "CustomerPropertyNetPosition.CPNP_Name";
        //                }
        //                else if (Table1 == "CustomerPersonalNetPosition")
        //                {
        //                    schemeNameFieldName = "CustomerPersonalNetPosition.CPNP_Name";
        //                }

        //                // We now split Table1 and Table2 Fields, have not entered Table 2 Fields in the database
        //                // Since its a constant Table, for data conditions, I am hard coding them here
        //                //
        //                if (FieldNames.Contains('|'))
        //                {
        //                    //fieldArray = FieldNames.Split('|');
        //                    //Table1Fields = fieldArray[0].ToString();
        //                    //Table2Fields = fieldArray[1].ToString();
        //                    //customerField = Table1Fields.Split(',');
        //                    //CustomerIDField = customerField[0].ToString();
        //                    //SchemeIDField = customerField[1].ToString();
        //                }
        //                else
        //                {
        //                    customerField = FieldNames.Split(',');
        //                    int i = customerField.Length;
        //                    //CustomerIDField = customerField[0].ToString();
        //                    CustomerIDField = "Customer.C_CustomerId";
        //                    SchemeIDField = customerField[1].ToString();
        //                }
        //                /**/
        //                ConditionCustomerIDField = "ADCS_UserID";
        //                ConditionSchemeIDField = "ADCS_SchemeID";
        //                ConditionEventIDField = "AEL_EventID";
        //                ConditionField = "ADCS_Condition";
        //                ConditionPresetValueField = "ADCS_PresetValue";
        //                /**/

        //                // Get Schemes from the table for this Customer ID
        //                dsEntireSchemeList = alertsDao.GetCustomerConditionalSchemeList(Table1, CustomerIDField, SchemeIDField, CurrentValueFieldName, CustomerId, EventCode, schemeNameFieldName);
        //                dsSubscribedSchemeList = alertsDao.GetSubscribedSchemeList(EventID, CustomerId);

        //                // Filter the Unsubscribed Events and place it into the respective dataset
        //                foreach (DataRow drSubscribedSchemeList in dsSubscribedSchemeList.Tables[0].Rows)
        //                {
        //                    foreach (DataRow drEntireSchemeList in dsEntireSchemeList.Tables[0].Rows)
        //                    {
        //                        if (drEntireSchemeList["SchemeID"].ToString() == drSubscribedSchemeList["AES_SchemeID"].ToString())
        //                        {
        //                            drEntireSchemeList.Delete();
        //                        }
        //                    }
        //                    dsEntireSchemeList.AcceptChanges();
        //                }

        //            }
        //            else if (type == "transactional")
        //            {
        //                TableName = dsTableAndFields.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //                FieldNames = dsTableAndFields.Tables[0].Rows[0]["AEL_FieldName"].ToString();

        //                // We now split the Table Names
        //                if (TableName.Contains(','))
        //                {
        //                    tables = TableName.Split(',');
        //                    Table1 = tables[0].ToString();
        //                    Table2 = tables[1].ToString();
        //                }
        //                else
        //                {
        //                    Table1 = "CustomerMutualFundSystematicSetup";
        //                }

        //                // We now split Table1 and Table2 Fields
        //                if (FieldNames.Contains('|'))
        //                {
        //                    fieldArray = FieldNames.Split('|');
        //                    Table1Fields = fieldArray[0].ToString();
        //                    Table2Fields = fieldArray[1].ToString();

        //                    customerField = Table1Fields.Split(',');
        //                    CustomerIDField = customerField[0].ToString();
        //                    SchemeIDField = customerField[1].ToString();
        //                }
        //                else
        //                {
        //                    customerField = FieldNames.Split(',');
        //                    int i = customerField.Length;

        //                    //CustomerIDField = "CustomerMutualFundSystematicSetup.CMFA_AccountId";
        //                    CustomerIDField = "Customer.C_CustomerId";
        //                    SchemeIDField = "CustomerMutualFundSystematicSetup.PASP_SchemePlanCode";
        //                    TypeCodeField = "CustomerMutualFundSystematicSetup.XSTT_SystematicTypeCode";

        //                    StartDateField = "CustomerMutualFundSystematicSetup.CMFSS_StartDate";
        //                    EndDateField = "CustomerMutualFundSystematicSetup.CMFSS_EndDate";
        //                    SystematicDateField = "CustomerMutualFundSystematicSetup.CMFSS_SystematicDate";
        //                    FrequencyField = "CustomerMutualFundSystematicSetup.XF_FrequencyCode";


        //                    dsEntireSchemeList = alertsDao.GetCustomerSchemeList(Table1, CustomerIDField, CustomerId, EventCode, SchemeIDField, TypeCodeField, StartDateField, EndDateField, SystematicDateField, FrequencyField);
        //                    dsSubscribedSchemeList = alertsDao.GetSubscribedSchemeList(EventID, CustomerId);

        //                    foreach (DataRow drSubscribedSchemeList in dsSubscribedSchemeList.Tables[0].Rows)
        //                    {
        //                        foreach (DataRow drEntireSchemeList in dsEntireSchemeList.Tables[0].Rows)
        //                        {
        //                            if (drEntireSchemeList["SchemeID"].ToString() == drSubscribedSchemeList["AES_SchemeID"].ToString())
        //                            {
        //                                drEntireSchemeList.Delete();
        //                            }

        //                        }

        //                        dsEntireSchemeList.AcceptChanges();
        //                    }
        //                }
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

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetUnsubscribedSchemesList()");

        //        object[] objects = new object[3];
        //        objects[0] = CustomerId;
        //        objects[1] = EventID;
        //        objects[2] = type;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsEntireSchemeList;

        //}



        //public string GetEventCode(int EventID)
        //{
        //    string EventCode = string.Empty;
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        EventCode = alertsDao.GetEventCode(EventID);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetEventCode()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return EventCode;
        //}

        //public bool DeleteEvent(long EventSetupID)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool blResult = false;

        //    try
        //    {
        //        if (alertsDao.DeleteEvent(EventSetupID))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteEvent()");

        //        object[] objects = new object[1];
        //        objects[0] = EventSetupID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public bool DeleteConditionalEvent(long EventSetupID, int EventID)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool blResult = false;
        //    int SchemeID;
        //    int CustomerID;

        //    try
        //    {
        //        DataSet dsEventDetails = alertsDao.GetEventDetails(EventSetupID);
        //        SchemeID = Int32.Parse(dsEventDetails.Tables[0].Rows[0]["AES_SchemeID"].ToString());
        //        CustomerID = Int32.Parse(dsEventDetails.Tables[0].Rows[0]["AES_TargetID"].ToString());

        //        if (alertsDao.DeleteConditionalEvent(EventSetupID, EventID, CustomerID, SchemeID))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteConditionalEvent()");

        //        object[] objects = new object[2];
        //        objects[0] = EventSetupID;
        //        objects[1] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //public DataSet GetFrequencyList()
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet ds;

        //    try
        //    {
        //        ds = alertsDao.GetFrequencyList();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetFrequencyList()");

        //        //object[] objects = new object[1];
        //        //objects[0] = EventID;

        //        //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return ds;
        //}

        //public bool IsReminder(int EventID)
        //{
        //    bool blResult = false;
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        if (alertsDao.IsReminder(EventID))
        //            blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:IsReminder()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool blResult = false;

        //    try
        //    {
        //        blResult = alertsDao.CustEventExists(customerId, EventlookupID, schemeId, Reminder);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:CustEventExists()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    DateTime dt = new DateTime();

        //    try
        //    {
        //        dt = alertsDao.GetEventNextOcurrence(customerId, EventCode, SchemeID, Reminder);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetEventNextOcurrence()");

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

        //    return dt;
        //}

        //public DataSet GetSchemeDetailsFromMasterTable(int customerId, int EventlookupID, int SchemeID, string type, string EventCode)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsSchemeDetails = new DataSet();
        //    DataSet dsTableAndFields = null;

        //    string TableName = string.Empty;
        //    string FieldNames = string.Empty;
        //    string[] tables;
        //    string Table1 = string.Empty;
        //    string Table2 = string.Empty;

        //    string[] fieldArray;
        //    string Table1Fields = string.Empty;
        //    string Table2Fields = string.Empty;
        //    string[] customerField;
        //    string CustomerIDField = string.Empty;
        //    string SchemeIDField = string.Empty;
        //    string TypeCodeField = string.Empty;
        //    string SchemeDateField = string.Empty;
        //    string StartDateField = string.Empty;
        //    string EndDateField = string.Empty;
        //    string SystematicDateField = string.Empty;
        //    string FrequencyField = string.Empty;

        //    try
        //    {
        //        // Get Table and Field Names
        //        dsTableAndFields = alertsDao.GetTableFieldNames(EventlookupID);

        //        if (dsTableAndFields.Tables[0].Rows.Count > 0)
        //        {
        //            TableName = dsTableAndFields.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //            FieldNames = dsTableAndFields.Tables[0].Rows[0]["AEL_FieldName"].ToString();

        //            if (type == "date")
        //            {

        //            }
        //            else if (type == "data")
        //            {

        //            }
        //            else if (type == "transactional")
        //            {

        //            }

        //            // We now split the Table Names
        //            if (TableName.Contains(','))
        //            {
        //                tables = TableName.Split(',');
        //                Table1 = tables[0].ToString();
        //                Table2 = tables[1].ToString();
        //            }
        //            else
        //            {
        //                Table1 = TableName;
        //            }


        //            // We now split Table1 and Table2 Fields
        //            if (FieldNames.Contains('|'))
        //            {
        //                fieldArray = FieldNames.Split('|');
        //                Table1Fields = fieldArray[0].ToString();
        //                Table2Fields = fieldArray[1].ToString();

        //                customerField = Table1Fields.Split(',');
        //                CustomerIDField = customerField[0].ToString();
        //                SchemeIDField = customerField[1].ToString();
        //            }
        //            else
        //            {
        //                customerField = FieldNames.Split(',');
        //                int i = customerField.Length;

        //                CustomerIDField = customerField[0].ToString();
        //                SchemeIDField = customerField[1].ToString();
        //                TypeCodeField = customerField[2].ToString();
        //                if (i > 3)
        //                {
        //                    SchemeDateField = customerField[3].ToString();
        //                    StartDateField = "CMFSS_StartDate";
        //                    EndDateField = "CMFSS_EndDate";
        //                    SystematicDateField = "CMFSS_SystematicDate";
        //                    FrequencyField = "XF_Frequency";
        //                }
        //            }
        //        }

        //        /**************************/
        //        if (EventCode == "DOB")
        //        {
        //            dsSchemeDetails = alertsDao.GetSchemeDetailsFromMasterTable(Table1, CustomerIDField, SchemeIDField, customerId);
        //        }
        //        else if (EventCode == "Meeting")
        //        {
        //            dsSchemeDetails = null;
        //        }
        //        else
        //        {
        //            dsSchemeDetails = alertsDao.GetSchemeDetailsFromMasterTable(Table1, CustomerIDField, SchemeIDField, SchemeDateField, StartDateField, EndDateField, SystematicDateField, FrequencyField, customerId, SchemeID);
        //        }

        //        /**************************/

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetSchemeDetailsFromMasterTable()");

        //        object[] objects = new object[3];
        //        objects[0] = customerId;
        //        objects[1] = EventlookupID;
        //        objects[2] = SchemeID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsSchemeDetails;
        //}

        //public bool UpdateGridEventDetails(long EventSetupID, DateTime NextOccurMod)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool blResult = false;

        //    try
        //    {
        //        blResult = alertsDao.UpdateGridEventDetails(EventSetupID, NextOccurMod);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:UpdateGridEventDetails()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool blResult = false;

        //    try
        //    {
        //        blResult = alertsDao.UpdateGridEventDataConditions(customerId, SchemeID, EventID, Condition, PresetValue);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:UpdateGridEventDataConditions()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet ds;

        //    try
        //    {
        //        ds = alertsDao.GetEventDetails(EventSetupID);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetEventDetails()");

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
        //    List<AlertsNotificationVo> alertNotificationList = new List<AlertsNotificationVo>();
        //    AlertsDao alertsDao = new AlertsDao();

        //    try
        //    {
        //        alertNotificationList = alertsDao.GetCustomerNotifications(CustomerId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerNotifications()");

        //        object[] objects = new object[1];
        //        //objects[0] = type;
        //        objects[0] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return alertNotificationList;
        //}

        //public DataSet GetCustomerDataConditions(int customerId, int SchemeID, int EventID)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsDataConditions;

        //    try
        //    {
        //        dsDataConditions = alertsDao.GetCustomerDataConditions(customerId, SchemeID, EventID);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerDataConditions()");

        //        object[] objects = new object[3];
        //        objects[0] = customerId;
        //        objects[1] = SchemeID;
        //        objects[2] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsDataConditions;
        //}

        //public DataSet GetSchemeCurrentValue(int customerId, int SchemeID, int EventID)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsTableAndFieldNames;
        //    DataSet dsSchemeCurrentValue = null;
        //    string[] tables;
        //    string[] schemeTableFields;
        //    string TableName = string.Empty;
        //    string FieldNames = string.Empty;
        //    string SchemeTableName = string.Empty;
        //    string CustomerIDFieldName = string.Empty;
        //    string SchemeIDFieldName = string.Empty;
        //    string CurrentValueFieldName = string.Empty;

        //    try
        //    {
        //        dsTableAndFieldNames = alertsDao.GetTableFieldNames(EventID);

        //        if (dsTableAndFieldNames.Tables[0].Rows.Count > 0)
        //        {
        //            TableName = dsTableAndFieldNames.Tables[0].Rows[0]["AEL_TableName"].ToString();
        //            FieldNames = dsTableAndFieldNames.Tables[0].Rows[0]["AEL_FieldName"].ToString();
        //            CurrentValueFieldName = dsTableAndFieldNames.Tables[0].Rows[0]["AEL_DataConditionField"].ToString();

        //            // We now split the Table Names
        //            if (TableName.Contains(','))
        //            {
        //                tables = TableName.Split(',');
        //                SchemeTableName = tables[0].ToString();
        //            }
        //            else
        //            {
        //                SchemeTableName = TableName;
        //            }

        //            // We now split Table1 and Table2 Fields, have not entered Table 2 Fields in the database
        //            // Since its a constant Table, for data conditions, I am hard coding them here
        //            //
        //            if (FieldNames.Contains('|'))
        //            {
        //                //fieldArray = FieldNames.Split('|');
        //                //Table1Fields = fieldArray[0].ToString();
        //                //Table2Fields = fieldArray[1].ToString();
        //                //customerField = Table1Fields.Split(',');
        //                //CustomerIDField = customerField[0].ToString();
        //                //SchemeIDField = customerField[1].ToString();
        //            }
        //            else
        //            {
        //                schemeTableFields = FieldNames.Split(',');
        //                int i = schemeTableFields.Length;
        //                CustomerIDFieldName = schemeTableFields[0].ToString();
        //                SchemeIDFieldName = schemeTableFields[1].ToString();
        //            }
        //            dsSchemeCurrentValue = alertsDao.GetSchemeCurrentValue(SchemeTableName, CustomerIDFieldName, SchemeIDFieldName, CurrentValueFieldName, customerId, SchemeID);
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

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerDataConditions()");

        //        object[] objects = new object[3];
        //        objects[0] = customerId;
        //        objects[1] = SchemeID;
        //        objects[2] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsSchemeCurrentValue;
        //}

        //public DataSet GetCustomerDashboard(int CustomerID)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsGetCustomerDashboard;

        //    try
        //    {
        //        dsGetCustomerDashboard = alertsDao.GetCustomerDashboard(CustomerID);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerDataConditions()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsGetMetatableDetails;

        //    try
        //    {
        //        dsGetMetatableDetails = alertsDao.GetMetatableDetails(primaryKey);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetMetatableDetails()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsSchemeDescription;

        //    try
        //    {
        //        dsSchemeDescription = alertsDao.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetSchemeDescription()");

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

        //    return dsSchemeDescription;
        //}

        //public bool DeleteAlertNotification(long notificationId)
        //{
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool bResult = false;

        //    try
        //    {
        //        if (alertsDao.DeleteAlertNotification(notificationId))
        //            bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAlertNotification()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool bResult = false;

        //    try
        //    {
        //        bResult = alertsDao.ExecuteDateAlert();

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAlertNotification()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool bResult = false;

        //    try
        //    {
        //        bResult = alertsDao.ExecuteDataAlert();

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAlertNotification()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    bool bResult = false;

        //    try
        //    {
        //        bResult = alertsDao.ExecuteTransactionAlert();

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:DeleteAlertNotification()");

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
        //    AlertsDao alertsDao = new AlertsDao();
        //    DataSet dsCustomerAlerts;

        //    try
        //    {
        //        dsCustomerAlerts = alertsDao.GetCustomerDashboardAlerts(customerId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertsBo.cs:GetCustomerDashboardAlerts()");

        //        object[] objects = new object[4];
        //        objects[0] = customerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return dsCustomerAlerts;
        //}
        #region Code To Use To Get Unique Values From DataTables
        //private bool ColumnEqual(object A, object B)
        //{
        //    if (A == DBNull.Value && B == DBNull.Value)
        //        return true;
        //    if (A == DBNull.Value || B == DBNull.Value)
        //        return false;
        //    return (A.Equals(B));
        //}

        //public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        //{
        //    DataTable dt = new DataTable(TableName);
        //    dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

        //    object LastValue = null;
        //    foreach (DataRow dr in SourceTable.Select("", FieldName))
        //    {
        //        if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
        //        {
        //            LastValue = dr[FieldName];
        //            dt.Rows.Add(new object[] { LastValue });
        //        }
        //    }
        //    //if (ds != null)
        //    //    ds.Tables.Add(dt);
        //    return dt;
        //}
        #endregion
        #endregion

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

        public bool SaveAdviserSIPReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSIPReminderAlert(rmId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSWPReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSWPReminderAlert(rmId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserDOBReminderAlert(int rmId, int userId, int reminderDays)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserDOBReminderAlert(rmId, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserAnniversaryReminderAlert(int rmId, int userId, int reminderDays)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserAnniversaryReminderAlert(rmId, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserELSSMaturityReminderAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserELSSMaturityReminderAlert(rmId, customerId, accountId, schemeId, isBulk, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserFDMaturityReminderAlert(int rmId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserFDMaturityReminderAlert(rmId, customerId, accountId, fdId, isBulk, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserFDRecurringDepositReminderAlert(int rmId, int customerId, int accountId, int fdId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserFDRecurringDepositReminderAlert(rmId, customerId, accountId, fdId, isBulk, reminderDays, userId); 
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserInsuranceReminderAlert(int rmId, int customerId, int accountId, int insuranceId, int isBulk, int reminderDays, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserInsuranceReminderAlert(rmId, customerId, accountId, insuranceId, isBulk, reminderDays, userId);
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
                objects[0] = rmId;
                objects[1] = reminderDays;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSIPConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSIPConfirmationAlert(rmId, customerId, accountId, schemeId, isBulk, userId);
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
                objects[0] = rmId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserSWPConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserSWPConfirmationAlert(rmId, customerId, accountId, schemeId, isBulk, userId);
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
                objects[0] = rmId;
                objects[1] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserMFDividendConfirmationAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFDividendConfirmationAlert(rmId, customerId, accountId, schemeId, isBulk, userId);
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
                objects[0] = rmId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool SaveAdviserPersonalOccurrenceAlert(int rmId, int customerId, int accountId, int personalNpId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserPersonalOccurrenceAlert(rmId, customerId, accountId, personalNpId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public bool SaveAdviserPropertyOccurrenceAlert(int rmId, int customerId, int accountId, int propertyNpId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserPropertyOccurrenceAlert(rmId, customerId, accountId, propertyNpId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public bool SaveAdviserMFStopLossOccurrenceAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFStopLossOccurrenceAlert(rmId, customerId, accountId, schemeId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public bool SaveAdviserMFProfitBookingOccurrenceAlert(int rmId, int customerId, int accountId, int schemeId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserMFProfitBookingOccurrenceAlert(rmId, customerId, accountId, schemeId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public bool SaveAdviserEQStopLossOccurrenceAlert(int rmId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserEQStopLossOccurrenceAlert(rmId, customerId, accountId, scripId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public bool SaveAdviserEQProfitBookingOccurrenceAlert(int rmId, int customerId, int accountId, int scripId, int isBulk, int userId, string condition, int presetValue)
        {
            bool bResult = false;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                bResult = alertsDao.SaveAdviserEQProfitBookingOccurrenceAlert(rmId, customerId, accountId, scripId, isBulk, userId, condition, presetValue);
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
                objects[0] = rmId;
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

        public DataSet GetAdviserCustomerSMSAlerts(int adviserId)
        {
            DataSet dsAdviserCustomerSMSAlerts;
            AlertsDao alertsDao = new AlertsDao();
            try
            {
                dsAdviserCustomerSMSAlerts = alertsDao.GetAdviserCustomerSMSAlerts(adviserId);
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
                objects[0] = adviserId;

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
                    bResult=UpdateAlertStatus(alertIdList[i], alertStatus);
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
    }
}
