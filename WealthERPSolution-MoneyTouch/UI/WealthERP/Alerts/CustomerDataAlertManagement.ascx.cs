using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using WealthERP.Base;
using BoAlerts;
using VoAlerts;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using BoCommon;

namespace WealthERP.Alerts
{
    public partial class CustomerDataAlertManagement : System.Web.UI.UserControl
    {
        /*Initialise CallBack Variable*/
        private string _callbackResult = null;

        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        AlertsSetupVo alertVo;
        List<AlertsSetupVo> AlertSetupList = null;
        DataSet dsAssets;

        int userId;
        int customerId;
        int count;
        string CustomerName;
        string eventCode;
        int EventID;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        #region OldAlert

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        userVo = (UserVo)Session[SessionContents.UserVo];
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //        userId = userVo.UserId;
        //        customerId = customerVo.CustomerId;
        //        CustomerName = customerVo.FirstName + " " + customerVo.LastName;
        //        trAlertType.Visible = false;

        //        lblCustomerName.Text = CustomerName;

        //        EventID = Int32.Parse(Session[SessionContents.EventID].ToString());

        //        BindAlertsDropDown("data", EventID);
        //        BindGridView(customerId, EventID);//, EventCode

        //        trAddNewCondition.Visible = false;
        //        trConditionSave.Visible = false;
        //        trAddNewEvent.Visible = false;
        //    }

        //    #region Initialise Call Back Scripts for Scheme DropDownList

        //    string CBddlReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "PopulateCurrentValue", "context");
        //    StringBuilder ddlCallBackScript = new StringBuilder();
        //    ddlCallBackScript.Append("function UseCallBackDropDown(arg, context)");
        //    ddlCallBackScript.Append("{");
        //    ddlCallBackScript.Append(CBddlReference);
        //    ddlCallBackScript.Append("};");
        //    string ddlCBScript = ddlCallBackScript.ToString();
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ddlSchemeCallback", ddlCBScript, true);

        //    #endregion

        //}

        //private void BindGridView(int customerId, int EventID)
        //{
        //    DataSet dsCustomerConditions;
        //    DataSet dsSchemeCurrentValue;
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvConditionalAlerts.DataSource = null;
        //    gvConditionalAlerts.DataBind();

        //    //AlertSetupList = alertsBo.GetListofEventsSubscribedByCustomer(customerId, EventID);


        //    if (AlertSetupList != null)
        //    {
        //        DataTable dtAlertSetup = new DataTable();

        //        // The following comes from Event Setup
        //        dtAlertSetup.Columns.Add("EventSetupID");
        //        dtAlertSetup.Columns.Add("Details");
        //        //dtAlertSetup.Columns.Add("Scheme");
        //        // This comes from the Master Table
        //        dtAlertSetup.Columns.Add("CurrentValue");
        //        // The following comes from Data Conditions Table
        //        dtAlertSetup.Columns.Add("Condition");
        //        dtAlertSetup.Columns.Add("PresetValue");
        //        dtAlertSetup.Columns.Add("Message");

        //        DataRow drAlertSetup;
        //        for (int i = 0; i < AlertSetupList.Count; i++)
        //        {
        //            drAlertSetup = dtAlertSetup.NewRow();
        //            alertVo = new AlertsSetupVo();
        //            alertVo = AlertSetupList[i];

        //            // Get Data Conditions for the specific Scheme
        //            //dsCustomerConditions = alertsBo.GetCustomerDataConditions(customerId, alertVo.SchemeID, EventID);

        //            // Get Current Value for the Specific Scheme
        //            // dsSchemeCurrentValue = alertsBo.GetSchemeCurrentValue(customerId, alertVo.SchemeID, EventID);

        //            drAlertSetup[0] = alertVo.EventSetupID.ToString().Trim();
        //            if (alertVo.SchemeID.ToString() == "" || alertVo.SchemeID.ToString() == "0")
        //            {
        //                drAlertSetup[1] = alertVo.EventName.ToString().Trim() + " : " + alertVo.SchemeID.ToString().Trim();
        //            }
        //            else
        //            {
        //                drAlertSetup[1] = alertVo.EventName.ToString().Trim() + " : " + GetSchemeName(alertVo.AllFieldNames, alertVo.SchemeID);
        //            }
        //            //drAlertSetup[2] = dsSchemeCurrentValue.Tables[0].Rows[0]["CurrentValue"].ToString();
        //            //drAlertSetup[3] = dsCustomerConditions.Tables[0].Rows[0]["Condition"].ToString();
        //            //drAlertSetup[4] = dsCustomerConditions.Tables[0].Rows[0]["PresetValue"].ToString();
        //            drAlertSetup[5] = alertVo.EventMessage.ToString().Trim();

        //            dtAlertSetup.Rows.Add(drAlertSetup);
        //        }

        //        gvConditionalAlerts.DataSource = dtAlertSetup;
        //        gvConditionalAlerts.DataBind();
        //        gvConditionalAlerts.Visible = true;
        //        lblMessage.Visible = false;
        //    }
        //    else
        //    {
        //        lblMessage.Visible = true;
        //    }
        //}

        //private void BindAlertsDropDown(string type, int EventID)
        //{
        //    AlertsBo alertsBo = new AlertsBo();
        //    DataSet dsAlerts;

        //    try
        //    {
        //        //dsAlerts = alertsBo.GetListofAlerts(type);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerAlertManagement.ascx:BindAlertsDropDown()");

        //        object[] objects = new object[1];
        //        objects[0] = type;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    //if (dsAlerts.Tables[0].Rows.Count > 0)
        //    //{
        //    //    ddlConditionalAlertTypes.DataSource = dsAlerts.Tables[0];
        //    //    ddlConditionalAlertTypes.DataTextField = "EventName";
        //    //    ddlConditionalAlertTypes.DataValueField = "AEL_EventID";
        //    //    ddlConditionalAlertTypes.DataBind();
        //    //    ddlConditionalAlertTypes.Items.Insert(0, "Please select an Alert Type");
        //    //    ddlConditionalAlertTypes.SelectedValue = EventID.ToString();
        //    //}
        //}

        //protected void gvConditionalAlerts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvConditionalAlerts.EditIndex = -1;
        //    BindGridView(customerId, Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //}

        //protected void gvConditionalAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //}

        //protected void gvConditionalAlerts_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvConditionalAlerts.EditIndex = e.NewEditIndex;

        //    //Label lbl = (Label)gvConditionalAlerts.Rows[e.NewEditIndex].FindControl("lblReminder1");
        //    //Session["ReminderOrg"] = lbl.Text;

        //    BindGridView(customerId, Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //}

        //protected void gvConditionalAlerts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    DataSet EventDetails;

        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    int SchemeID = 0;

        //    TextBox txtPreset = (TextBox)gvConditionalAlerts.Rows[e.RowIndex].FindControl("txtPresetValue");
        //    DropDownList ddlCond = (DropDownList)gvConditionalAlerts.Rows[e.RowIndex].FindControl("ddlCondition");

        //    // Save Data Here!
        //    long EventSetupID = Int64.Parse(gvConditionalAlerts.DataKeys[e.RowIndex].Value.ToString());

        //    //EventDetails = alertsBo.GetEventDetails(EventSetupID);

        //    //if (EventDetails.Tables[0].Rows.Count > 0)
        //    //{
        //    //    if (EventDetails.Tables[0].Rows[0]["AES_SchemeID"].ToString() != "")
        //    //    {
        //    //        SchemeID = Int32.Parse(EventDetails.Tables[0].Rows[0]["AES_SchemeID"].ToString());
        //    //    }
        //    //    else
        //    //    {
        //    //        SchemeID = 0;
        //    //    }
        //    //    EventID = Int32.Parse(EventDetails.Tables[0].Rows[0]["AEL_EventID"].ToString());

        //    //}
        //    string Condition = ddlCond.SelectedValue;
        //    double Preset = Double.Parse(txtPreset.Text);

        //    //if (alertsBo.UpdateGridEventDataConditions(customerId, SchemeID, EventID, Condition, Preset))
        //    //{
        //    //    // Display Success
        //    //}
        //    gvConditionalAlerts.EditIndex = -1;
        //    BindGridView(customerId, Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //}

        //protected void gvConditionalAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlCond;

        //        ddlCond = e.Row.FindControl("ddlCondition") as DropDownList;
        //        HiddenField hdnCond = e.Row.FindControl("hdnCondition") as HiddenField;

        //        if (hdnCond != null)
        //        {
        //            if (ddlCond != null)
        //            {
        //                Hashtable hshCondition = GetConditionsHashtable();

        //                ddlCond.DataSource = hshCondition;
        //                ddlCond.DataTextField = "Key";
        //                ddlCond.DataValueField = "Value";
        //                ddlCond.DataBind();

        //                /*Bind the Selected Value to the Drop Down*/
        //                IDictionaryEnumerator en = hshCondition.GetEnumerator();
        //                while (en.MoveNext())
        //                {
        //                    if (en.Key.ToString() == hdnCond.Value.ToString())
        //                    {
        //                        ddlCond.SelectedValue = en.Value.ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void btnSaveConditionAlert_Click(object sender, EventArgs e)
        //{
        //    int SchemeID;
        //    int targetId = 0;
        //    string Condition = string.Empty;
        //    double PresetValue = 0.0;
        //    string[] accIdSchemeId;
        //    string propertyDetails;
        //    string eventMessage;

        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;
        //    propertyDetails = ddlScheme.SelectedValue;
        //    accIdSchemeId = propertyDetails.Split(':');
        //    targetId = int.Parse(accIdSchemeId[0]);
        //    SchemeID = int.Parse(accIdSchemeId[1]);
        //    Condition = ddlCondition.SelectedValue;
        //    PresetValue = Double.Parse(txtPresetValue.Text.Trim());
        //    EventID = Int32.Parse(ddlConditionalAlertTypes.SelectedValue);
        //    eventMessage = "Property Value " + Condition + PresetValue.ToString();

        //    //if (alertsBo.SaveAlert(customerId,eventMessage, targetId, SchemeID, Condition, PresetValue, EventID, userId))
        //    //{
        //    //    // Success Message
        //    //    BindGridView(customerId, EventID);
        //    //    trAddNewCondition.Visible = false;
        //    //    trConditionSave.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    // Display Failure Message
        //    //}

        //}

        //protected void btnAddNewConditionalEvent_Click(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    if (ddlConditionalAlertTypes.SelectedIndex != 0)
        //    {
        //        trAddNewCondition.Visible = true;
        //        trConditionSave.Visible = true;

        //        string EventCode = string.Empty;
        //        // Get EventCode
        //        //EventCode = alertsBo.GetEventCode(Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //        BindSchemeDropDown(Int32.Parse(ddlConditionalAlertTypes.SelectedValue), customerId, EventCode);

        //        BindConditionDropDown();
        //    }
        //    trAddNewEvent.Visible = true;
        //}

        //private void BindConditionDropDown()
        //{
        //    Hashtable hshConditions = GetConditionsHashtable();

        //    ddlCondition.DataSource = hshConditions;
        //    ddlCondition.DataTextField = "Key";
        //    ddlCondition.DataValueField = "Value";
        //    ddlCondition.DataBind();
        //    ddlCondition.Items.Insert(0, "Select a Condition");


        //}

        //private static Hashtable GetConditionsHashtable()
        //{
        //    Hashtable hshConditions = new Hashtable();

        //    hshConditions.Add(">", ">");
        //    hshConditions.Add(">=", ">=");
        //    hshConditions.Add("<", "<");
        //    hshConditions.Add("<=", "<=");
        //    hshConditions.Add("=", "=");

        //    return hshConditions;

        //    /* FYI::
        //     * 
        //     * Retrieving values from Hashtable =>
        //     * string condition = hshConditions[">"].ToString();
        //     * 
        //     * Removing Items from Hashtable =>
        //     * hshConditions.Remove(">");
        //     * 
        //     * Looping through a Hashtable
        //     * IDictionaryEnumerator en = hshConditions.GetEnumerator();
        //     * while (en.MoveNext())
        //     * {
        //     *      string str = en.Value.ToString();
        //     * }
        //     * 
        //     */
        //}

        //private void BindSchemeDropDown(int EventID, int CustomerID, string EventCode)
        //{
        //    //DataTable dtSchemes = new DataTable();
        //    DataSet dsSchemes = new DataSet();

        //    try
        //    {
        //        //dsSchemes = alertsBo.GetUnsubscribedSchemesList(EventID, CustomerID, "data", EventCode);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerAlertManagement.ascx:BindSchemeDropDown()");

        //        object[] objects = new object[3];
        //        objects[0] = EventID;
        //        objects[1] = CustomerID;
        //        objects[2] = EventCode;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    if (dsSchemes != null)
        //    {
        //        Session["DataSetSchemes"] = dsSchemes;

        //        ddlScheme.DataSource = dsSchemes.Tables[0];
        //        ddlScheme.DataTextField = "SchemeName"; // "SchemeName" - Give this once the Scheme Name Doubt is clarified
        //        ddlScheme.DataValueField = "ValueField";
        //        ddlScheme.DataBind();
        //        ddlScheme.Items.Insert(0, "Select");
        //        btnSaveConditionAlert.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlScheme.Items.Clear();
        //        ddlScheme.Items.Insert(0, "No Schemes!");
        //        btnSaveConditionAlert.Enabled = false;
        //    }

        //    //Session["SchemeDataSet"] = dsSchemes;
        //}

        //protected void ddlConditionalAlertTypes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    trAddNewCondition.Visible = false;
        //    trConditionSave.Visible = false;

        //    if (ddlConditionalAlertTypes.SelectedIndex != 0)
        //    {
        //        gvConditionalAlerts.DataSource = null;
        //        gvConditionalAlerts.DataBind();
        //        BindGridView(customerId, Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //    }
        //    else if (ddlConditionalAlertTypes.SelectedIndex == 0)
        //    {
        //        gvConditionalAlerts.DataSource = null;
        //        gvConditionalAlerts.DataBind();
        //    }
        //}

        //#region Implemented the below code using callback
        ////protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    DataSet dsSchemeCurrentValue;
        ////    userVo = (UserVo)Session[SessionContents.UserVo];
        ////    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        ////    userId = userVo.UserId;
        ////    customerId = customerVo.CustomerId;

        ////    // Get Current Value for the Specific Scheme
        ////    dsSchemeCurrentValue = alertsBo.GetSchemeCurrentValue(customerId, Int32.Parse(ddlScheme.SelectedValue), Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        ////    if (dsSchemeCurrentValue.Tables[0].Rows.Count > 0)
        ////    {
        ////        txtCurrentValue.Text = dsSchemeCurrentValue.Tables[0].Rows[0]["CurrentValue"].ToString();
        ////    }
        ////    // Try implementing callback if possible for this - This will be useful for calculators
        ////}
        //#endregion

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    int EventID = Int32.Parse(ddlConditionalAlertTypes.SelectedValue);
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    foreach (GridViewRow dr in gvConditionalAlerts.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        if (checkBox.Checked)
        //        {
        //            int EventSetupID = Convert.ToInt32(gvConditionalAlerts.DataKeys[dr.RowIndex].Value);
        //            //if (alertsBo.DeleteConditionalEvent(EventSetupID, EventID))
        //            //{
        //            //    // Display Success Messages
        //            //}
        //        }
        //    }

        //    // Success Message
        //    BindGridView(customerId, EventID);
        //}

        //protected string GetSchemeName(string AllFieldNames, int SchemeID)
        //{
        //    string schemeName;
        //    string[] allFieldsArray;
        //    string metatablePrimaryKey;
        //    DataSet dsmetatableDetails;
        //    DataSet dsSchemeName;
        //    string tableName;
        //    string description;


        //    allFieldsArray = AllFieldNames.Split(',');
        //    metatablePrimaryKey = allFieldsArray[1].Split('.')[1];
        //    dsmetatableDetails = alertsBo.GetMetatableDetails(metatablePrimaryKey);
        //    tableName = dsmetatableDetails.Tables[0].Rows[0][2].ToString();
        //    description = dsmetatableDetails.Tables[0].Rows[0][1].ToString();

        //    dsSchemeName = alertsBo.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeID);

        //    schemeName = dsSchemeName.Tables[0].Rows[0][0].ToString();

        //    return schemeName;

        //}

        //#region CallBack Functions

        //public void RaiseCallbackEvent(string eventArg)
        //{
        //    string CurrentValue = string.Empty;
        //    string[] accIdSchemeId;
        //    int SchemeID;
        //    int targetId;

        //    if (eventArg != "0")
        //    {

        //        DataSet dsSchemeCurrentValue;
        //        userVo = (UserVo)Session[SessionContents.UserVo];
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //        userId = userVo.UserId;
        //        customerId = customerVo.CustomerId;
        //        //propertyDetails = ddlScheme.SelectedValue;
        //        accIdSchemeId = eventArg.Split(':');
        //        targetId = int.Parse(accIdSchemeId[0]);
        //        SchemeID = int.Parse(accIdSchemeId[1]);

        //        // Get Current Value for the Specific Scheme
        //        //dsSchemeCurrentValue = alertsBo.GetSchemeCurrentValue(customerId, SchemeID, Int32.Parse(ddlConditionalAlertTypes.SelectedValue));
        //        //if (dsSchemeCurrentValue.Tables[0].Rows.Count > 0)
        //        //{
        //        //    CurrentValue = dsSchemeCurrentValue.Tables[0].Rows[0]["CurrentValue"].ToString();
        //        //}
        //    }

        //    _callbackResult = CurrentValue;
        //}

        //public string GetCallbackResult()
        //{
        //    return _callbackResult;
        //}

        //#endregion

        #endregion

        #region NewAlert

        protected override void OnInit(EventArgs e)
        {

            try
            {
                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                customerVo = (CustomerVo)Session["customerVo"];
                EventID = Int32.Parse(Session[SessionContents.EventID].ToString());
                customerId = customerVo.CustomerId;
                BindSubscribedEventsGridView(customerId, EventID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            try
            {
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
                ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioSystematicView.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    userVo = (UserVo)Session[SessionContents.UserVo];
                    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    eventCode = Session[SessionContents.EventCode].ToString();
                    userId = userVo.UserId;
                    customerId = customerVo.CustomerId;
                    CustomerName = customerVo.FirstName + " " + customerVo.LastName;

                    lblCustomerName.Text = CustomerName;


                    if (eventCode.ToString().Trim() != "")
                    {
                        lblHeader.Text = eventCode + " Occurrence Setup";
                    }
                    //retrieving the eventId of the event for which management is done in this page.
                    //this was placed in a session when the pick for this alert was done
                    EventID = Int32.Parse(Session[SessionContents.EventID].ToString());

                    BindSubscribedEventsGridView(customerId, EventID);

                    
                    trAddNewCondition.Visible = false;
                    trAddNewEvent.Visible = false;
                    trConditionSave.Visible = false;
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

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindSubscribedEventsGridView(int customerId, int EventID)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;
                gvConditionalAlerts.DataSource = null;
                gvConditionalAlerts.DataBind();

                //function that retrieves the customer subscribed alerts from the DB
                AlertSetupList = alertsBo.GetCustomerSubscribedOccurrenceAlerts(customerId, EventID, mypager.CurrentPage, out count);
                //setting the pager
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }
                //if alerts are subscribed runs the piece of code below else displays that events dont exist
                if (AlertSetupList.Count > 0)
                {
                    DataTable dtAlertSetup = new DataTable();

                    // creating a datatable with all the necessary details to bind to the gridview
                    dtAlertSetup.Columns.Add("EventSetupID");
                    dtAlertSetup.Columns.Add("Details");
                    dtAlertSetup.Columns.Add("CurrentValue");
                    dtAlertSetup.Columns.Add("Condition");
                    dtAlertSetup.Columns.Add("PresetValue");
                    dtAlertSetup.Columns.Add("Message");

                    DataRow drAlertSetup;
                    for (int i = 0; i < AlertSetupList.Count; i++)
                    {
                        drAlertSetup = dtAlertSetup.NewRow();
                        alertVo = new AlertsSetupVo();
                        alertVo = AlertSetupList[i];
                        //assigns the setupid to each row as a Datakey
                        drAlertSetup[0] = alertVo.EventSetupID.ToString().Trim();
                        //Details is a combination of the event name and scheme name 
                        drAlertSetup[1] = alertVo.EventName.ToString().Trim() + " : " + alertVo.SchemeName.ToString();

                        if (alertVo.CurrentValue.ToString() != "" || alertVo.CurrentValue.ToString() != null)
                            drAlertSetup[2] = alertVo.CurrentValue.ToString();

                        if (alertVo.Condition.ToString().Trim() != "")
                            drAlertSetup[3] = alertVo.Condition.ToString().Trim();

                        if (alertVo.PresetValue.ToString() != null || alertVo.PresetValue.ToString() != "")
                            drAlertSetup[4] = alertVo.PresetValue.ToString();

                        if (alertVo.EventMessage.ToString().Trim() != "" || alertVo.EventMessage.ToString().Trim() != null)
                        {
                            //Message that gets displayed when the alert is triggered
                            drAlertSetup[5] = alertVo.EventMessage.ToString().Trim();
                        }
                        else
                        {
                            drAlertSetup[5] = "";
                        }


                        dtAlertSetup.Rows.Add(drAlertSetup);
                    }

                    // binding the datatable to the gridview and making the grid visible
                    gvConditionalAlerts.DataSource = dtAlertSetup;
                    gvConditionalAlerts.DataBind();
                    gvConditionalAlerts.Visible = true;
                    lblMessage.Visible = false;
                    this.GetPageCount();
                }
                else
                {
                    lblMessage.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:BindSubscribedEventsGridView()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnAddNewConditionalEvent_Click(object sender, EventArgs e)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                eventCode = Session[SessionContents.EventCode].ToString();
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                trAddNewCondition.Visible = true;
                trConditionSave.Visible = true;
                trAddNewEvent.Visible = true;
                txtCurrentValue.Text = "";
                txtPresetValue.Text = "";
                //string EventCode = string.Empty;
                // Get EventCode
                //EventCode = alertsBo.GetEventCode(Int32.Parse(ddlTranxAlertTypes.SelectedValue));
                //BindSchemeDropDown(Int32.Parse(ddlTranxAlertTypes.SelectedValue), customerId, EventCode);
                BindAssetDropDown(customerId, eventCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:btnAddNewConditionalEvent_Click()");

                object[] objects = new object[0];
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindAssetDropDown(int customerId, string eventCode)
        {
            //DataTable dtSchemes = new DataTable();

            try
            {
                dsAssets = alertsBo.GetCustomerUnsubscribedAssets(customerId, eventCode);
                Session["UnsubscribedAssets"] = dsAssets;

                if (dsAssets.Tables.Count > 0)
                {

                    ddlScheme.DataSource = dsAssets.Tables[0];
                    ddlScheme.DataTextField = "Name"; // "Name" - Give this once the Scheme Name Doubt is clarified
                    ddlScheme.DataValueField = "AssetId";//"ValueField"
                    ddlScheme.DataBind();
                    ddlScheme.Items.Insert(0, "Select a Scheme");
                    btnSaveConditionAlert.Enabled = true;
                }
                else
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Insert(0, "No Schemes!");
                    btnSaveConditionAlert.Enabled = false;
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

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:BindAssetDropDown()");

                object[] objects = new object[3];
                objects[0] = EventID;
                objects[1] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnSaveConditionAlert_Click(object sender, EventArgs e)
        {
            try
            {
                dsAssets = (DataSet)Session["UnsubscribedAssets"];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                EventID = int.Parse(Session[SessionContents.EventID].ToString());
                customerId = customerVo.CustomerId;
                userId = userVo.UserId;


                if (dsAssets.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsAssets.Tables[0].Rows)
                    {
                        if (dr["AssetId"].ToString() == ddlScheme.SelectedItem.Value.ToString())
                        {
                            alertVo = new AlertsSetupVo();

                            alertVo.CustomerId = customerId;
                            alertVo.EventID = int.Parse(Session[SessionContents.EventID].ToString());
                            alertVo.SchemeID = int.Parse(ddlScheme.SelectedItem.Value.ToString());
                            alertVo.TargetID = int.Parse(dr["TargetId"].ToString());
                            alertVo.Condition = ddlCondition.SelectedItem.Value.ToString();
                            alertVo.PresetValue = float.Parse(txtPresetValue.Text.ToString());

                            if(alertVo.Condition==">")
                                alertVo.EventMessage = "Current Value has crossed " + alertVo.PresetValue;
                            if (alertVo.Condition == "<")
                                alertVo.EventMessage = "Current Value has fallen below " + alertVo.PresetValue;
                            if (alertVo.Condition == ">=")
                                alertVo.EventMessage = "Current Value has gone up to " + alertVo.PresetValue;
                            if (alertVo.Condition == "<=")
                                alertVo.EventMessage = "Current Value has dropped to " + alertVo.PresetValue;
                            if (alertVo.Condition == "=")
                                alertVo.EventMessage = "Current Value is equal to " + alertVo.PresetValue;

                            alertsBo.SaveOccurrenceAlert(alertVo, userId);

                        }
                    }
                }
                BindSubscribedEventsGridView(customerId, EventID);
                trAddNewCondition.Visible = false;
                trAddNewEvent.Visible = false;
                trConditionSave.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:btnSaveConditionAlert_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dsAssets = (DataSet)Session["UnsubscribedAssets"];

                foreach (DataRow dr in dsAssets.Tables[0].Rows)
                {
                    if (ddlScheme.SelectedItem.Value.ToString() == dr["AssetId"].ToString())
                        txtCurrentValue.Text = dr["CurrentValue"].ToString();
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

                FunctionInfo.Add("Method", "CustomerDataAlertManagement.ascx:ddlScheme_SelectedIndexChanged()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            int EventID = int.Parse(Session[SessionContents.EventID].ToString());
            //eventCode = Session[SessionContents.EventCode].ToString();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            userId = userVo.UserId;
            customerId = customerVo.CustomerId;

            foreach (GridViewRow dr in gvConditionalAlerts.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                if (checkBox.Checked)
                {
                    int EventSetupID = Convert.ToInt32(gvConditionalAlerts.DataKeys[dr.RowIndex].Value);
                    if (alertsBo.DeleteConditionalEvent(EventSetupID))
                    {
                        // Display Success Messages
                    }
                }
            }

            // Success Message
            BindSubscribedEventsGridView(customerId, EventID);
        }

        #endregion

        

    }
}