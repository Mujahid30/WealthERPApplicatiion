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
using BoCommon;

namespace WealthERP.Alerts
{
    public partial class CustomerAlertManagement : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AlertsBo alertsBo = new AlertsBo();
        DataSet dsSchemes = new DataSet();
        AlertsSetupVo alertVo;
        List<AlertsSetupVo> AlertSetupList = null;
        int count;
        int userId;
        int customerId;
        int rmId;
        string customerName;
        int eventID;
        int frequencyPeriod;
        string eventCode;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        #region OldAlert
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // Retrieve Cust and User Ids from Session
        //        userVo = (UserVo)Session[SessionContents.UserVo];
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //        rmVo = (RMVo)Session[SessionContents.RmVo];
        //        rmId = rmVo.RMId;
        //        userId = userVo.UserId;
        //        customerId = customerVo.CustomerId;
        //        CustomerName = customerVo.FirstName + " " + customerVo.LastName;

        //        lblCustomerName.Text = CustomerName;

        //        EventID = Int32.Parse(Session[SessionContents.EventID].ToString());
        //        ddlDateAlertTypes.Visible=false;

        //        /** Guess this is not required!
        //        EventCode = (string)Session[SessionContents.EventCode];
        //        **/

        //        // Bind Date Related Data
        //        //BindAlertsDropDown("date", EventID);
        //        BindGridView("date", customerId, EventID);//, EventCode
        //    }
        //}

        //private void BindGridView(string type, int CustomerID, int EventID)//, string EventCode
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvDateAlerts.DataSource = null;
        //    gvDateAlerts.DataBind();

        //    if (type == "date")
        //    {
        //       // AlertSetupList = alertsBo.GetListofEventsSubscribedByCustomer(CustomerID, EventID);

        //        if (AlertSetupList != null)
        //        {
        //            DataTable dtAlertSetup = new DataTable();

        //            dtAlertSetup.Columns.Add("EventSetupID");
        //            dtAlertSetup.Columns.Add("Details");
        //            dtAlertSetup.Columns.Add("EventDate");
        //            dtAlertSetup.Columns.Add("NextOccur");
        //            dtAlertSetup.Columns.Add("Message");

        //            DataRow drAlertSetup;
        //            for (int i = 0; i < AlertSetupList.Count; i++)
        //            {
        //                drAlertSetup = dtAlertSetup.NewRow();
        //                alertVo = new AlertsSetupVo();
        //                alertVo = AlertSetupList[i];
        //                drAlertSetup[0] = alertVo.EventSetupID.ToString().Trim();
        //                //drAlertSetup[1] = alertVo.EventName.ToString().Trim();
        //                if (alertVo.SchemeID.ToString() == "" || alertVo.SchemeID.ToString() == "0")
        //                {
        //                    //drAlertSetup[2] = alertVo.SchemeID.ToString().Trim();
        //                    drAlertSetup[1] = "DOB";
        //                }
        //                else
        //                {
        //                    drAlertSetup[1] = alertVo.EventName.ToString().Trim() + GetSchemeName(alertVo.AllFieldNames, alertVo.SchemeID);
        //                }

        //                drAlertSetup[2] = alertVo.EventName.ToString().Trim(); // Has to b changed to the Event Date

        //                DateTime dt = DateTime.Parse(alertVo.NextOccurence.ToString());
        //                drAlertSetup[3] = dt.ToShortDateString();

        //                //if (alertVo.EventMessage.ToString() != "")
        //                //    drAlertSetup[4] = alertVo.EventMessage.ToString();
        //                //else
        //                drAlertSetup[4] = "";
        //                //if (alertVo.Reminder.ToString().ToLower() == "false")
        //                //{
        //                //    //drAlertSetup[4] = alertVo.Reminder.ToString().ToLower();
        //                //    drAlertSetup[4] = "0";
        //                //}
        //                //else if (alertVo.Reminder.ToString().ToLower() == "true")
        //                //{

        //                //    DateTime ReminderNextOccur = DateTime.Parse(alertVo.NextOccurence.ToString());
        //                //    int EventlookupID = Int32.Parse(alertVo.EventID.ToString());
        //                //    int numberofDays = 0;

        //                //    numberofDays = GetReminderDays(ReminderNextOccur, EventlookupID, alertVo.EventName.ToString().Trim(), Int32.Parse(alertVo.SchemeID.ToString()), type, alertVo.Reminder.ToString().ToLower());

        //                //    drAlertSetup[4] = numberofDays.ToString();
        //                //}

        //                //if (alertVo.FrequencyID != 0)
        //                //{
        //                //    switch (alertVo.FrequencyID.ToString())
        //                //    {
        //                //        case "1":
        //                //            alertVo.Frequency = "One Time";
        //                //            break;
        //                //        case "2":
        //                //            alertVo.Frequency = "Daily";
        //                //            break;
        //                //        case "3":
        //                //            alertVo.Frequency = "Fortnightly";
        //                //            break;
        //                //        case "4":
        //                //            alertVo.Frequency = "Half Yearly";
        //                //            break;
        //                //        case "5":
        //                //            alertVo.Frequency = "Monthly";
        //                //            break;
        //                //        case "6":
        //                //            alertVo.Frequency = "Quarterly";
        //                //            break;
        //                //        case "7":
        //                //            alertVo.Frequency = "Weekly";
        //                //            break;
        //                //        case "8":
        //                //            alertVo.Frequency = "Yearly";
        //                //            break;
        //                //        default:
        //                //            alertVo.Frequency = "N/A";
        //                //            break;
        //                //    }
        //                //    drAlertSetup[5] = alertVo.Frequency.ToString().Trim();
        //                //}
        //                //else
        //                //    drAlertSetup[5] = "N/A";

        //                dtAlertSetup.Rows.Add(drAlertSetup);
        //            }

        //            gvDateAlerts.DataSource = dtAlertSetup;
        //            gvDateAlerts.DataBind();
        //            gvDateAlerts.Visible = true;
        //            lblMessage0.Visible = false;
        //        }
        //        else
        //        {
        //            lblMessage0.Visible = true;
        //        }
        //    }
        //}

        ////private int GetReminderDays(DateTime ReminderNextOccur, int EventlookupID, string EventCode, int SchemeID, string type, string Reminder)
        ////{
        ////    int NumberofDays = 0;
        ////    // First check::: 
        ////    // * if the alert exists in the Setup table for CustID, 
        ////    // EventLookupID, SchemeID and "Not Reminder"
        ////    // Get the Actual Next Occurence and Calculate Diff b/w Actual and Reminder
        ////    // The above is number of days.
        ////    // * else get the Next Occurence(calculated) from master table
        ////    // Calculate the date diff.. This is number of days
        ////    if (Reminder == "true")
        ////    {
        ////        if (alertsBo.CustEventExists(customerId, EventlookupID, SchemeID, false))
        ////        {
        ////            DateTime ActualNextOccurence = alertsBo.GetEventNextOcurrence(customerId, EventCode, SchemeID, "false");
        ////            TimeSpan ts = ActualNextOccurence.Subtract(ReminderNextOccur);
        ////            NumberofDays = Int32.Parse(ts.TotalDays.ToString());
        ////        }
        ////        else
        ////        {
        ////            DataSet ds = alertsBo.GetSchemeDetailsFromMasterTable(customerId, EventlookupID, SchemeID, type, EventCode);

        ////            if (ds == null)
        ////            {
        ////                // This is a meeting! The Logic for that goes here.
        ////                NumberofDays = 0;
        ////            }
        ////            else
        ////            {
        ////                if (EventCode == "DOB")
        ////                {
        ////                    DateTime dtDOB = DateTime.Parse(ds.Tables[0].Rows[0]["DOB"].ToString());
        ////                    //dtDOB = DateTime.Parse(String.Format("{0:MM/dd/yyyy}", dtDOB));
        ////                    DateTime dtReminder = DateTime.Parse(ReminderNextOccur.ToString());
        ////                    DateTime dtCurrent = DateTime.Now;
        ////                    int yearDiff = dtCurrent.Year - dtDOB.Year;
        ////                    dtDOB = dtDOB.AddYears(yearDiff);
        ////                    if (ReminderNextOccur.Ticks < dtDOB.Ticks)
        ////                    {
        ////                        DateTime ActualNextOccurence = dtDOB;
        ////                        //double diff = ActualNextOccurence.Date.ToOADate() - ReminderNextOccur.Date.DateToOADate();
        ////                        TimeSpan ts = ActualNextOccurence.Subtract(ReminderNextOccur);
        ////                        NumberofDays = Int32.Parse(ts.TotalDays.ToString());
        ////                    }
        ////                    else
        ////                    {
        ////                        dtDOB = dtDOB.AddYears(1);
        ////                        DateTime ActualNextOccurence = dtDOB;
        ////                        TimeSpan ts = ActualNextOccurence.Subtract(ReminderNextOccur);
        ////                        NumberofDays = Int32.Parse(ts.TotalDays.ToString());
        ////                    }
        ////                    // after setting the next occurence check if the reminder
        ////                    // it shud be less than the next occurence
        ////                    // if not then add one year to next occurence
        ////                    // then take the difference in the date values
        ////                }
        ////                else
        ////                {
        ////                    DateTime StartDate = DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
        ////                    int SystematicDate = Int32.Parse(ds.Tables[0].Rows[0]["SystematicDate"].ToString());
        ////                    string FrequencyCode = ds.Tables[0].Rows[0]["FrequencyCode"].ToString().Trim();
        ////                    int Frequency;
        ////                    switch (FrequencyCode)
        ////                    {
        ////                        case "DA":
        ////                            Frequency = 2;
        ////                            break;
        ////                        case "FN":
        ////                            Frequency = 3;
        ////                            break;
        ////                        case "HY":
        ////                            Frequency = 4;
        ////                            break;
        ////                        case "MN":
        ////                            Frequency = 5;
        ////                            break;
        ////                        case "QT":
        ////                            Frequency = 6;
        ////                            break;
        ////                        case "WK":
        ////                            Frequency = 7;
        ////                            break;
        ////                        case "YR":
        ////                            Frequency = 8;
        ////                            break;
        ////                        default:
        ////                            Frequency = 0;
        ////                            break;
        ////                    }
        ////                    //string[] dateArray = StartDate.ToString().Split('-');
        ////                    //dateArray[1] = SystematicDate.ToString();
        ////                    //DateTime ActualNextOccurence = DateTime.Parse(dateArray[0] + "/" + dateArray[1] + "/" + dateArray[2]);
        ////                    DateTime ActualNextOccurence = GetSystematicNextOccurance(StartDate, SystematicDate, Frequency);

        ////                    TimeSpan ts = ActualNextOccurence.Subtract(ReminderNextOccur);
        ////                    NumberofDays = Int32.Parse(ts.TotalDays.ToString());
        ////                }
        ////            }
        ////        }
        ////    }
        ////    else
        ////    {

        ////    }

        ////    return NumberofDays;
        ////}

        ////private void BindAlertsDropDown(string type, int EventID)
        ////{
        ////    AlertsBo alertsBo = new AlertsBo();
        ////    DataSet dsAlerts;

        ////    try
        ////    {
        ////        //dsAlerts = alertsBo.GetListofAlerts(type);
        ////    }
        ////    catch (BaseApplicationException Ex)
        ////    {
        ////        throw Ex;
        ////    }

        ////    catch (Exception Ex)
        ////    {
        ////        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        ////        NameValueCollection FunctionInfo = new NameValueCollection();

        ////        FunctionInfo.Add("Method", "CustomerAlertManagement.ascx:BindAlertsDropDown()");

        ////        object[] objects = new object[1];
        ////        objects[0] = type;

        ////        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        ////        exBase.AdditionalInformation = FunctionInfo;
        ////        ExceptionManager.Publish(exBase);
        ////        throw exBase;
        ////    }

        ////    if (dsAlerts.Tables[0].Rows.Count > 0)
        ////    {
        ////        if (type == "date")
        ////        {
        ////            ddlDateAlertTypes.DataSource = dsAlerts.Tables[0];
        ////            ddlDateAlertTypes.DataTextField = "EventName";
        ////            ddlDateAlertTypes.DataValueField = "AEL_EventID";
        ////            ddlDateAlertTypes.DataBind();
        ////            ddlDateAlertTypes.Items.Insert(0, "Please select an Alert Type");
        ////            ddlDateAlertTypes.SelectedValue = EventID.ToString();
        ////        }
        ////        else if (type == "data")
        ////        {

        ////        }
        ////        else if (type == "transactional")
        ////        {

        ////        }
        ////        else
        ////        {
        ////            // Display some sort of Javascript Error!
        ////        }
        ////    }
        ////}

        //protected void btnAddNewDateEvent_Click(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;


        //    if (IsReminder(Int32.Parse(ddlDateAlertTypes.SelectedValue)))
        //    {
        //        Session[SessionContents.Reminder] = "true";
        //        trReminder.Visible = true;

        //        if (ddlDateAlertTypes.SelectedItem.Text.Contains("Meeting"))
        //        {
        //            DataSet ds;
        //            //ds = alertsBo.GetFrequencyList();

        //            //ddlFrequency.DataSource = ds.Tables[0];
        //            ddlFrequency.DataTextField = "CL_CycleDesc";
        //            ddlFrequency.DataValueField = "CL_CycleID";
        //            ddlFrequency.DataBind();
        //            ddlFrequency.Items.Insert(0, "Select a Frequency");

        //            trFrequency.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        Session[SessionContents.Reminder] = "false";
        //        trReminder.Visible = false;
        //    }

        //    if (ddlDateAlertTypes.SelectedItem.Text.Contains("DOB") || ddlDateAlertTypes.SelectedItem.Text.Contains("Meeting"))
        //    {
        //        trAddNewDirectDate.Visible = true;
        //        trDirectDateSave.Visible = true;
        //        trAddNewSchemeDate.Visible = false;
        //        trSchemeSave.Visible = false;

        //        if (ddlDateAlertTypes.SelectedItem.Text.Contains("DOB"))
        //        {
        //            if (CheckDOBSubscribed(Int32.Parse(ddlDateAlertTypes.SelectedValue), customerId))
        //            {   // DOB Already Subscribed
        //                DateTime dtDOB = (DateTime)Session["DOB"];
        //                txtDateEntry.Text = dtDOB.ToShortDateString();
        //                txtDateEntry.Enabled = false;
        //                txtDateEntry_CalendarExtender.Enabled = false;
        //                btnSaveDirect.Enabled = false;
        //                // Show message that the user has already subscribed for the event
        //            }
        //            else
        //            {
        //                DateTime dtDOB = (DateTime)Session["DOB"];
        //                DateTime dtCurrent = DateTime.Now;
        //                int yearDiff = dtCurrent.Year - dtDOB.Year;
        //                dtDOB = dtDOB.AddYears(yearDiff);
        //                txtDateEntry.Text = dtDOB.ToShortDateString();
        //                txtDateEntry.Enabled = false;
        //                txtDateEntry_CalendarExtender.Enabled = false;
        //                btnSaveDirect.Enabled = true;
        //            }
        //        }
        //        else if (ddlDateAlertTypes.SelectedItem.Text.Contains("Meeting"))
        //        {
        //            txtDateEntry.Enabled = true;
        //            txtDateEntry_CalendarExtender.Enabled = true;
        //            txtDateEntry.Text = DateTime.Now.ToShortDateString();
        //        }
        //    }
        //    else
        //    {
        //        trAddNewDirectDate.Visible = false;
        //        trDirectDateSave.Visible = false;
        //        trAddNewSchemeDate.Visible = true;
        //        trSchemeSave.Visible = true;
        //        trFrequency.Visible = false;

        //        string EventCode = string.Empty;
        //        // Get EventCode
        //        //EventCode = alertsBo.GetEventCode(Int32.Parse(ddlDateAlertTypes.SelectedValue));
        //        BindSchemeDropDown(Int32.Parse(ddlDateAlertTypes.SelectedValue), customerId, EventCode);
        //    }
        //}

        //private bool IsReminder(int EventID)
        //{
        //    bool blResult = false;

        //    try
        //    {
        //        //if (alertsBo.IsReminder(EventID))
        //        //    blResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerAlertManagement.ascx:IsReminder()");

        //        object[] objects = new object[1];
        //        objects[0] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    return blResult;
        //}

        //private bool CheckDOBSubscribed(int EventID, int CustomerId)
        //{
        //    bool blResult = false;
        //    DataSet dsDOB;

        //    try
        //    {
        //       // dsDOB = alertsBo.CheckDOBSubscribed(EventID, CustomerId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerAlertManagement.ascx:CheckDOBSubscribed()");

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustomerId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    //if (dsDOB.Tables[0].Rows.Count > 0)
        //    //{
        //    //    Session["DOB"] = dsDOB.Tables[0].Rows[0]["AES_NextOccurence"];
        //    //    blResult = true;
        //    //}
        //    //else if (dsDOB.Tables[1].Rows.Count > 0)
        //    //{
        //    //    Session["DOB"] = dsDOB.Tables[1].Rows[0]["C_DOB"];
        //    //}

        //    return blResult;
        //}

        //private void BindSchemeDropDown(int EventID, int CustomerID, string EventCode)
        //{
        //    //DataTable dtSchemes = new DataTable();
        //    DataSet dsSchemes = new DataSet();
        //    DataSet dsSchemeName = new DataSet();

        //    try
        //    {
        //        //dsSchemes = alertsBo.GetUnsubscribedSchemesList(EventID, CustomerID, "date", EventCode);
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

        //        object[] objects = new object[2];
        //        objects[0] = EventID;
        //        objects[1] = CustomerID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    if (dsSchemes != null)
        //    {
        //        Session["DataSetSchemes"] = dsSchemes;

        //        ddlScheme.DataSource = dsSchemes.Tables[0];
        //        ddlScheme.DataTextField = "TextField"; // "SchemeName" - Give this once the Scheme Name Doubt is clarified
        //        ddlScheme.DataValueField = "ValueField";
        //        ddlScheme.DataBind();
        //        ddlScheme.Items.Insert(0, "Select a Scheme");
        //        btnSaveDateScheme.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlScheme.Items.Clear();
        //        ddlScheme.Items.Insert(0, "No Schemes!");
        //        btnSaveDateScheme.Enabled = false;
        //    }
        //}

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    int EventID = Int32.Parse(ddlDateAlertTypes.SelectedValue);
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    foreach (GridViewRow dr in gvDateAlerts.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        if (checkBox.Checked)
        //        {
        //            int EventSetupID = Convert.ToInt32(gvDateAlerts.DataKeys[dr.RowIndex].Value);
        //            if (alertsBo.DeleteEvent(EventSetupID))
        //            {
        //                // Display Success Messages
        //            }
        //        }
        //    }

        //    // Success Message
        //    BindGridView("date", customerId, EventID);
        //    trAddNewDirectDate.Visible = false;
        //    trDirectDateSave.Visible = false;
        //    trAddNewSchemeDate.Visible = false;
        //    trSchemeSave.Visible = false;
        //    trReminder.Visible = false;
        //    trFrequency.Visible = false;
        //}

        //protected void btnSaveDirect_Click(object sender, EventArgs e)
        //{
        //    int EventID = Int32.Parse(ddlDateAlertTypes.SelectedValue);
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    DateTime NextOccurence = DateTime.Parse(txtDateEntry.Text);

        //    string Reminder = Session[SessionContents.Reminder].ToString();

        //    if (Reminder == "true")
        //    {
        //        int Frequency = 0;

        //        if (ddlDateAlertTypes.SelectedItem.Text.Contains("DOB"))
        //        {
        //            Frequency = 8;
        //        }
        //        else if (ddlDateAlertTypes.SelectedItem.Text.Contains("Meeting"))
        //        {
        //            // Set By the User
        //            if (ddlFrequency.SelectedIndex != 0)
        //                Frequency = Int32.Parse(ddlFrequency.SelectedValue);
        //            else
        //                Frequency = 1;
        //        }

        //        double ReminderDays = Double.Parse(txtReminder.Text.Trim());
        //        DateTime dtReminder = NextOccurence.AddDays(-ReminderDays);

        //        //if (alertsBo.SaveAlert(EventID, customerId, dtReminder, Frequency))
        //        //{
        //        //    // Success Message
        //        //    BindGridView("date", customerId, EventID);
        //        //    trAddNewDirectDate.Visible = false;
        //        //    trDirectDateSave.Visible = false;
        //        //    trAddNewSchemeDate.Visible = false;
        //        //    trSchemeSave.Visible = false;
        //        //    trReminder.Visible = false;
        //        //    trFrequency.Visible = false;
        //        //}
        //        //else
        //        //{
        //        //    // Failure Message
        //        //}
        //    }
        //    else
        //    {
        //        int Frequency = 0;

        //        if (ddlDateAlertTypes.SelectedItem.Text.Contains("DOB"))
        //        {
        //            Frequency = 8;
        //        }
        //        else if (ddlDateAlertTypes.SelectedItem.Text.Contains("Meeting"))
        //        {
        //            Frequency = 1;
        //        }


        //        //if (alertsBo.SaveAlert(EventID, customerId, NextOccurence, Frequency))
        //        //{
        //        //    // Success Message
        //        //    BindGridView("date", customerId, EventID);
        //        //    trAddNewDirectDate.Visible = false;
        //        //    trDirectDateSave.Visible = false;
        //        //    trAddNewSchemeDate.Visible = false;
        //        //    trSchemeSave.Visible = false;
        //        //    trReminder.Visible = false;
        //        //    trFrequency.Visible = false;
        //        //}
        //        //else
        //        //{
        //        //    // Failure Message
        //        //}
        //    }
        //}

        //protected void btnSaveDateScheme_Click(object sender, EventArgs e)
        //{
        //    DataSet dsSchemesList;
        //    int EventID = Int32.Parse(ddlDateAlertTypes.SelectedValue);
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    int targetId=0;
        //    int userId = userVo.UserId;
        //    int Frequency = 0;
        //    string eventMessage;
        //    string FrequencyCode = "";
        //    int schemeId = 0;
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;
        //    string schemeDetails = ddlScheme.SelectedValue.ToString();
        //    string[] accIdSchemeId = schemeDetails.Split(':');
        //    targetId = int.Parse(accIdSchemeId[0]);
        //    schemeId = int.Parse(accIdSchemeId[1]);
        //    DateTime NextOccurence = new DateTime();

        //    // Calculate the Next Occurence here
        //    dsSchemesList = (DataSet)Session["DataSetSchemes"];

        //    foreach (DataRow dr in dsSchemesList.Tables[0].Rows)
        //    {
        //        if (ddlScheme.SelectedValue == dr["ValueField"].ToString())
        //        {
        //            FrequencyCode = dr["FrequencyCode"].ToString().Trim();
        //            DateTime StartDate = DateTime.Parse(dr["StartDate"].ToString());
        //            DateTime EndDate = DateTime.Parse(dr["EndDate"].ToString());
        //            int SystematicDate = Int32.Parse(dr["SystematicDate"].ToString());

        //            switch (FrequencyCode)
        //            {
        //                case "DA":
        //                    Frequency = 2;
        //                    break;
        //                case "FN":
        //                    Frequency = 3;
        //                    break;
        //                case "HY":
        //                    Frequency = 4;
        //                    break;
        //                case "MN":
        //                    Frequency = 5;
        //                    break;
        //                case "QT":
        //                    Frequency = 6;
        //                    break;
        //                case "WK":
        //                    Frequency = 7;
        //                    break;
        //                case "YR":
        //                    Frequency = 8;
        //                    break;
        //                default:
        //                    Frequency = 0;
        //                    break;
        //            }

        //            //string[] dateArray = StartDate.ToString().Split('-');
        //            //dateArray[1] = SystematicDate.ToString();
        //            //NextOccurence = DateTime.Parse(dateArray[0] + "/" + dateArray[1] + "/" + dateArray[2]);
        //            NextOccurence = GetSystematicNextOccurance(StartDate, SystematicDate, Frequency);

        //        }
        //    }

        //    string Reminder = Session[SessionContents.Reminder].ToString();

        //    if (Reminder == "true")
        //    {
        //        double ReminderDays = Double.Parse(txtReminder.Text.Trim());
        //        DateTime dtReminder = NextOccurence.AddDays(-ReminderDays);
        //        eventMessage = "Event occured on " + NextOccurence.ToShortDateString();
        //        //if (alertsBo.SaveAlert(EventID, eventMessage, targetId, customerId, schemeId, dtReminder, Frequency, userId))
        //        //{
        //        //    // Success Message
        //        //    // ReBind The Grid View with Updated New Values
        //        //    BindGridView("date", customerId, EventID);
        //        //    trAddNewDirectDate.Visible = false;
        //        //    trDirectDateSave.Visible = false;
        //        //    trAddNewSchemeDate.Visible = false;
        //        //    trSchemeSave.Visible = false;
        //        //    trReminder.Visible = false;
        //        //    trFrequency.Visible = false;
        //        //}
        //        //else
        //        //{
        //        //    // Failure Message
        //        //}
        //    }
        //    else
        //    {
        //        eventMessage = "Event occured on " + NextOccurence.ToShortDateString();
        //        //if (alertsBo.SaveAlert(EventID, eventMessage, targetId, customerId, schemeId, NextOccurence, Frequency, userId))
        //        //{
        //        //    // Success Message
        //        //    // ReBind The Grid View with Updated New Values
        //        //    BindGridView("date", customerId, EventID);
        //        //    trAddNewDirectDate.Visible = false;
        //        //    trDirectDateSave.Visible = false;
        //        //    trAddNewSchemeDate.Visible = false;
        //        //    trSchemeSave.Visible = false;
        //        //    trReminder.Visible = false;
        //        //    trFrequency.Visible = false;
        //        //}
        //        //else
        //        //{
        //        //    // Failure Message
        //        //}
        //    }
        //}

        //protected void ddlDateAlertTypes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;
        //    string type = "date";

        //    trAddNewDirectDate.Visible = false;
        //    trDirectDateSave.Visible = false;
        //    trAddNewSchemeDate.Visible = false;
        //    trSchemeSave.Visible = false;
        //    trReminder.Visible = false;
        //    trFrequency.Visible = false;

        //    if (ddlDateAlertTypes.SelectedIndex != 0)
        //    {
        //        gvDateAlerts.DataSource = null;
        //        gvDateAlerts.DataBind();
        //        BindGridView(type, customerId, Int32.Parse(ddlDateAlertTypes.SelectedValue));
        //    }
        //    else if (ddlDateAlertTypes.SelectedIndex == 0)
        //    {
        //        gvDateAlerts.DataSource = null;
        //        gvDateAlerts.DataBind();
        //    }
        //}

        //protected void gvDateAlerts_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvDateAlerts.EditIndex = e.NewEditIndex;

        //    Label lbl = (Label)gvDateAlerts.Rows[e.NewEditIndex].FindControl("lblReminder1");
        //    Session["ReminderOrg"] = lbl.Text;

        //    BindGridView("date", customerId, Int32.Parse(ddlDateAlertTypes.SelectedValue));
        //}

        //protected void gvDateAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    // Check if Reminder, if yes then 
        //    DataSet dsFreq;
        //    DataSet ds;
        //    TextBox txtReminder1 = new TextBox();

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlFreq;

        //        ddlFreq = e.Row.FindControl("ddlFrequency") as DropDownList;
        //        HiddenField hdnFreq = e.Row.FindControl("hdnFrequency") as HiddenField;

        //        if (hdnFreq != null)
        //        {
        //            if (ddlFreq != null)
        //            {
        //                //dsFreq = alertsBo.GetFrequencyList();

        //                //ddlFreq.DataSource = dsFreq.Tables[0];
        //                ddlFreq.DataTextField = "CL_CycleDesc";
        //                ddlFreq.DataValueField = "CL_CycleID";
        //                ddlFreq.DataBind();

        //                //foreach (DataRow dr in dsFreq.Tables[0].Rows)
        //                //{
        //                //    if (dr["CL_CycleDesc"].ToString().Trim() == hdnFreq.Value.ToString())
        //                //    {
        //                //        ddlFreq.SelectedValue = dr["CL_CycleID"].ToString();
        //                //    }
        //                //}

        //                ddlFreq.Enabled = true;
        //            }
        //        }

        //        long EventSetupID = Int64.Parse(gvDateAlerts.DataKeys[e.Row.RowIndex].Value.ToString());

        //        txtReminder1 = e.Row.FindControl("txtReminder1") as TextBox;
        //        if (txtReminder1 != null)
        //        {
        //            //ds = alertsBo.GetEventDetails(EventSetupID);
        //            //if (ds.Tables[0].Rows.Count > 0)
        //            //{
        //            //    if (ds.Tables[0].Rows[0]["AEL_Reminder"].ToString().ToLower() == "true")
        //            //    {
        //            //        txtReminder1.Enabled = true;
        //            //    }
        //            //}
        //        }
        //    }
        //}

        //protected void gvDateAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //}

        //protected void gvDateAlerts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    DataSet ds;
        //    DataSet EventDetails;

        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    DateTime NextOccurOrg = new DateTime();
        //    int SchemeID = 0;
        //    int Frequency = 0;
        //    int EventID = 0;
        //    string Reminder = "";
        //    string type = "";
        //    int Diff = 0;
        //    DateTime NextOccurMod = new DateTime();

        //    TextBox txtNextOccur = (TextBox)gvDateAlerts.Rows[e.RowIndex].FindControl("txtNxtOccur");
        //    TextBox txtReminder1 = (TextBox)gvDateAlerts.Rows[e.RowIndex].FindControl("txtReminder1");
        //    DropDownList ddlFreq = (DropDownList)gvDateAlerts.Rows[e.RowIndex].FindControl("ddlFrequency");

        //    // Save Data Here!
        //    long EventSetupID = Int64.Parse(gvDateAlerts.DataKeys[e.RowIndex].Value.ToString());

        //    //EventDetails = alertsBo.GetEventDetails(EventSetupID);

        //    //if (EventDetails.Tables[0].Rows.Count > 0)
        //    //{
        //    //    NextOccurOrg = DateTime.Parse(EventDetails.Tables[0].Rows[0]["AES_NextOccurence"].ToString());
        //    //    if (EventDetails.Tables[0].Rows[0]["AES_SchemeID"].ToString() != "")
        //    //    {
        //    //        SchemeID = Int32.Parse(EventDetails.Tables[0].Rows[0]["AES_SchemeID"].ToString());
        //    //    }
        //    //    else
        //    //    {
        //    //        SchemeID = 0;
        //    //    }
        //    //    EventID = Int32.Parse(EventDetails.Tables[0].Rows[0]["AEL_EventID"].ToString());
        //    //    Frequency = Int32.Parse(EventDetails.Tables[0].Rows[0]["CL_CycleID"].ToString());
        //    //    type = EventDetails.Tables[0].Rows[0]["AEL_EventType"].ToString().ToLower().Trim();
        //    //    Reminder = EventDetails.Tables[0].Rows[0]["AEL_Reminder"].ToString().ToLower().Trim();
        //    //}

        //    int ReminderOrg = Int32.Parse(Session["ReminderOrg"].ToString());
        //    int ReminderMod = Int32.Parse(txtReminder1.Text);

        //    if (ReminderMod > ReminderOrg)
        //    {
        //        Diff = ReminderMod - ReminderOrg;
        //        NextOccurMod = NextOccurOrg - new TimeSpan(Diff, 0, 0, 0);
        //    }
        //    else if (ReminderMod < ReminderOrg)
        //    {
        //        Diff = ReminderOrg - ReminderMod;
        //        NextOccurMod = NextOccurOrg + new TimeSpan(Diff, 0, 0, 0);
        //    }
        //    else
        //    {
        //        NextOccurMod = NextOccurOrg;
        //    }
        //    //if (alertsBo.UpdateGridEventDetails(EventSetupID, NextOccurMod))
        //    //{
        //    //    // Display Success
        //    //}
        //    gvDateAlerts.EditIndex = -1;
        //    BindGridView("date", customerId, Int32.Parse(ddlDateAlertTypes.SelectedValue));
        //}

        //protected void gvDateAlerts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvDateAlerts.EditIndex = -1;
        //    BindGridView("date", customerId, Int32.Parse(ddlDateAlertTypes.SelectedValue));
        //}

        //public void LoadDDL(DropDownList ddlFreq)
        //{
        //    DataSet ds;
        //    //ds = alertsBo.GetFrequencyList();

        //    //ddlFreq.DataSource = ds.Tables[0];
        //    ddlFreq.DataTextField = "CL_CycleDesc";
        //    ddlFreq.DataValueField = "CL_CycleID";
        //    ddlFreq.DataBind();
        //}

        //public DateTime GetSystematicNextOccurance(DateTime StartDate, int SystematicDate, int Frequency)
        //{
        //    DateTime nextOccurance = new DateTime();
        //    int day;
        //    int month;
        //    int year;
        //    int frequencyMonths;

        //    day = SystematicDate;
        //    year = StartDate.Year;
        //    switch (Frequency)
        //    {
        //        case 4:
        //            frequencyMonths = 6;
        //            break;
        //        case 5:
        //            frequencyMonths = 1;
        //            break;
        //        case 6:
        //            frequencyMonths = 4;
        //            break;
        //        case 8:
        //            frequencyMonths = 12;
        //            break;
        //        default:
        //            frequencyMonths = 0;
        //            break;
        //    }
        //    if (StartDate.Day > SystematicDate)
        //    {
        //        month = StartDate.Month + frequencyMonths + 1 ;
        //    }
        //    else
        //    {
        //        month = StartDate.Month + frequencyMonths;
        //    }
        //    if (month > 12)
        //    {
        //        month = month - 12;
        //        year = year + 1;
        //    }

        //    nextOccurance = DateTime.Parse(month.ToString() + "/" + day.ToString() + "/" + year.ToString());

        //    return nextOccurance;
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
                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx.cs:OnInit()");
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
                eventID = Int32.Parse(Session[SessionContents.EventID].ToString());
                customerId = customerVo.CustomerId;
                BindSubscribedEventsGridView(customerId, eventID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[1];
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
                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx.cs:GetPageCount()");
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
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                // Retrieve Cust and User Ids from Session
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                eventCode = Session[SessionContents.EventCode].ToString();
                rmId = rmVo.RMId;
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;
                customerName = customerVo.FirstName + " " + customerVo.LastName;

                lblCustomerName.Text = customerName;

                if (eventCode.ToString().Trim() != "")
                {
                    lblHeader.Text = eventCode + " Reminder Setup";
                }

                eventID = Int32.Parse(Session[SessionContents.EventID].ToString());

                BindSubscribedEventsGridView(customerId, eventID);

                trAddNewEvent.Visible = false;
                trSchemeDetails.Visible = false;
                trReminderFive.Visible = false;
                trReminderTen.Visible = false;
                trReminderFifteen.Visible = false;
                trSubscribeButton.Visible = false;
                trDOB.Visible = false;

            }
        }

        private void BindSubscribedEventsGridView(int customerId, int eventID)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                gvDateAlerts.DataSource = null;
                gvDateAlerts.DataBind();

                //function that retrieves the customer subscribed alerts from the DB
                AlertSetupList = alertsBo.GetCustomerSubscribedReminderAlerts(customerId, eventID, mypager.CurrentPage,out count);
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
                    dtAlertSetup.Columns.Add("EventDate");
                    dtAlertSetup.Columns.Add("NextOccur");
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
                        if (alertVo.EventDate.ToString() != "")
                        {
                            drAlertSetup[2] = DateTime.Parse(alertVo.EventDate.ToString()).ToShortDateString();
                        }
                        if (alertVo.NextOccurence.ToString() != "")
                        {
                            drAlertSetup[3] = DateTime.Parse(alertVo.NextOccurence.ToString()).ToShortDateString();
                        }
                        if (alertVo.EventMessage.ToString().Trim() != "" || alertVo.EventMessage.ToString().Trim() != null)
                        {
                            //Message that gets displayed when the alert is triggered
                            drAlertSetup[4] = alertVo.EventMessage.ToString().Trim();
                        }


                        dtAlertSetup.Rows.Add(drAlertSetup);
                    }

                    // binding the datatable to the gridview and making the grid visible
                    gvDateAlerts.DataSource = dtAlertSetup;
                    gvDateAlerts.DataBind();
                    gvDateAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:BindSubscribedEventsGridView()");

                object[] objects = new object[0];
                objects[0] = customerId;
                objects[1] = eventID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnAddNewDateEvent_Click(object sender, EventArgs e)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                eventCode = Session[SessionContents.EventCode].ToString();
                eventID = int.Parse(Session[SessionContents.EventID].ToString());
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                //string EventCode = string.Empty;
                // Get EventCode
                //EventCode = alertsBo.GetEventCode(Int32.Parse(ddlTranxAlertTypes.SelectedValue));
                //BindSchemeDropDown(Int32.Parse(ddlTranxAlertTypes.SelectedValue), customerId, EventCode);
                chkReminderFive.Checked = false;
                chkReminderTen.Checked = false;
                chkReminderFifteen.Checked = false;

                trReminderFive.Visible = true;
                trReminderTen.Visible = true;
                trReminderFifteen.Visible = true;
                trSubscribeButton.Visible = true;
                trAddNewEvent.Visible = true;

                if (eventCode == "SIP")
                {
                    BindSchemeDropDown(customerId, eventCode, eventID);

                    trSchemeDetails.Visible = true;

                }
                if (eventCode == "DOB")
                {
                    if (customerVo.Dob != null)
                    {
                        txtDOB.Text = customerVo.Dob.ToShortDateString();
                    }

                    trDOB.Visible = true;

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

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:btnAddNewDateEvent_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindSchemeDropDown(int customerId, string eventCode, int eventId)
        {
            //DataTable dtSchemes = new DataTable();

            try
            {
                dsSchemes = alertsBo.GetCustomerUnsubscribedSystematicSchemes(customerId, eventCode, eventId);
                Session["UnsubscribedSchemes"] = dsSchemes;

                if (dsSchemes != null)
                {
                    Session["DataSetSchemes"] = dsSchemes;

                    ddlScheme.DataSource = dsSchemes.Tables[0];
                    ddlScheme.DataTextField = "PASP_SchemePlanName"; // "SchemeName" - Give this once the Scheme Name Doubt is clarified
                    ddlScheme.DataValueField = "PASP_SchemePlanCode";//"ValueField"
                    ddlScheme.DataBind();
                    ddlScheme.Items.Insert(0, "Select a Scheme");
                    //btnSaveTranxAlert.Enabled = true;
                }
                else
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Insert(0, "No Schemes!");
                    //btnSaveTranxAlert.Enabled = false;
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

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:BindSchemeDropDown()");

                object[] objects = new object[3];
                objects[0] = eventID;
                objects[1] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnSaveReminderAlert_Click(object sender, EventArgs e)
        {
            int reminderDays = 0;
            try
            {
                dsSchemes = (DataSet)Session["UnsubscribedSchemes"];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                eventID = int.Parse(Session[SessionContents.EventID].ToString());
                eventCode = Session[SessionContents.EventCode].ToString();
                customerId = customerVo.CustomerId;
                userId = userVo.UserId;

                if (eventCode == "SIP")
                {
                    if (dsSchemes.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsSchemes.Tables[0].Rows)
                        {
                            if (dr["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedItem.Value.ToString())
                            {

                                alertVo = new AlertsSetupVo();

                                alertVo.CustomerId = customerId;
                                alertVo.EventID = eventID;
                                alertVo.SchemeID = int.Parse(ddlScheme.SelectedItem.Value.ToString());
                                alertVo.TargetID = int.Parse(dr["CMFA_AccountId"].ToString());
                                alertVo.EndDate = DateTime.Parse(dr["CMFSS_EndDate"].ToString());
                                alertVo.Reminder = "N";
                                alertVo.FrequencyCode = GetAlertFrequencyCode(dr["XF_FrequencyCode"].ToString());
                                alertVo.NextOccurence = GetNextOccurrence(dr["CMFSS_StartDate"].ToString(), dr["CMFSS_EndDate"].ToString(), dr["CMFSS_SystematicDate"].ToString(), reminderDays, alertVo.FrequencyCode);
                                alertVo.EventMessage = "Transaction to be Captured Today";

                                alertVo.EventSetupID = alertsBo.SaveReminderAlert(alertVo, userId);

                                if (chkReminderFive.Checked)
                                {
                                    reminderDays = 5;
                                    alertVo.Reminder = "Y";
                                    alertVo.EventMessage = "Transaction to be Captured in 5 Days";
                                    alertVo.NextOccurence = GetNextOccurrence(dr["CMFSS_StartDate"].ToString(), dr["CMFSS_EndDate"].ToString(), dr["CMFSS_SystematicDate"].ToString(), reminderDays, alertVo.FrequencyCode);

                                    alertsBo.SaveReminderAlert(alertVo, userId);
                                }

                                if (chkReminderTen.Checked)
                                {
                                    reminderDays = 10;
                                    alertVo.Reminder = "Y";
                                    alertVo.EventMessage = "Transaction to be Captured in 10 Days";
                                    alertVo.NextOccurence = GetNextOccurrence(dr["CMFSS_StartDate"].ToString(), dr["CMFSS_EndDate"].ToString(), dr["CMFSS_SystematicDate"].ToString(), reminderDays, alertVo.FrequencyCode);

                                    alertsBo.SaveReminderAlert(alertVo, userId);
                                }

                                if (chkReminderFifteen.Checked)
                                {
                                    reminderDays = 15;
                                    alertVo.Reminder = "Y";
                                    alertVo.EventMessage = "Transaction to be Captured in 15 Days";
                                    alertVo.NextOccurence = GetNextOccurrence(dr["CMFSS_StartDate"].ToString(), dr["CMFSS_EndDate"].ToString(), dr["CMFSS_SystematicDate"].ToString(), reminderDays, alertVo.FrequencyCode);

                                    alertsBo.SaveReminderAlert(alertVo, userId);
                                }

                            }
                        }
                    }
                }
                if (eventCode == "DOB")
                {
                    alertVo = new AlertsSetupVo();

                    alertVo.CustomerId = customerId;
                    alertVo.EventID = eventID;
                    alertVo.TargetID = customerId;
                    alertVo.Reminder = "N";
                    alertVo.FrequencyCode = 9;
                    alertVo.NextOccurence = GetNextOccurrence(txtDOB.Text.ToString(), null, null, reminderDays, alertVo.FrequencyCode);
                    alertVo.EventMessage = "Happy Birthday";

                    alertVo.EventSetupID = alertsBo.SaveReminderAlert(alertVo, userId);

                    if (chkReminderFive.Checked)
                    {
                        reminderDays = 5;
                        alertVo.Reminder = "Y";
                        alertVo.EventMessage = "Birthday Coming up in 5 Days";
                        alertVo.NextOccurence = GetNextOccurrence(txtDOB.Text.ToString(), null, null, reminderDays, alertVo.FrequencyCode);

                        alertsBo.SaveReminderAlert(alertVo, userId);
                    }

                    if (chkReminderTen.Checked)
                    {
                        reminderDays = 10;
                        alertVo.Reminder = "Y";
                        alertVo.EventMessage = "Birthday Coming up in 10 Days";
                        alertVo.NextOccurence = GetNextOccurrence(txtDOB.Text.ToString(), null, null, reminderDays, alertVo.FrequencyCode);

                        alertsBo.SaveReminderAlert(alertVo, userId);
                    }

                    if (chkReminderFifteen.Checked)
                    {
                        reminderDays = 15;
                        alertVo.Reminder = "Y";
                        alertVo.EventMessage = "Birthday Coming up in 15 Days";
                        alertVo.NextOccurence = GetNextOccurrence(txtDOB.Text.ToString(), null, null, reminderDays, alertVo.FrequencyCode);

                        alertsBo.SaveReminderAlert(alertVo, userId);
                    }
                }
                BindSubscribedEventsGridView(customerId, eventID);

                trAddNewEvent.Visible = false;
                trSchemeDetails.Visible = false;
                trReminderFive.Visible = false;
                trReminderTen.Visible = false;
                trReminderFifteen.Visible = false;
                trSubscribeButton.Visible = false;
                trDOB.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:btnSaveReminderAlert_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private int GetAlertFrequencyCode(string frequency)
        {
            int alertFreqCode = 0;

            try
            {
                if (frequency != "")
                {
                    switch (frequency)
                    {
                        case "DA":
                            alertFreqCode = 2;
                            break;
                        case "FN":
                            alertFreqCode = 6;
                            break;
                        case "HY":
                            alertFreqCode = 8;
                            break;
                        case "MN":
                            alertFreqCode = 5;
                            break;
                        case "QT":
                            alertFreqCode = 7;
                            break;
                        case "WK":
                            alertFreqCode = 3;
                            break;
                        case "YR":
                            alertFreqCode = 9;
                            break;
                        default:
                            alertFreqCode = 0;
                            break;
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

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:GetAlertFrequencyCode()");

                object[] objects = new object[1];
                objects[0] = frequency;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return alertFreqCode;
        }

        private DateTime GetNextOccurrence(string startDate, string endDate, string systematicDate, int reminderDays, int alertFreqCode)
        {
            DateTime nextOccurrence = new DateTime();
            DateTime startDt = new DateTime();
            DateTime currentDate = new DateTime();
            DateTime endDt = new DateTime();
            eventCode = Session[SessionContents.EventCode].ToString();
            int day = 0;
            int year;
            int month;
            int frequencyMonths;
            int frequencyDays;

            try
            {
                currentDate = DateTime.Now;
                startDt = DateTime.Parse(startDate);
                if (endDate != null)
                {
                    endDt = DateTime.Parse(endDate);
                }

                if (eventCode == "SIP")
                {
                    day = int.Parse(systematicDate);
                }
                if (eventCode == "DOB")
                {
                    day = startDt.Day;
                }
                year = startDt.Year;
                month = startDt.Month;

                switch (alertFreqCode)
                {
                    case 5:
                        frequencyMonths = 1;
                        break;
                    case 7:
                        frequencyMonths = 4;
                        break;
                    case 8:
                        frequencyMonths = 6;
                        break;
                    case 9:
                        frequencyMonths = 12;
                        break;
                    default:
                        frequencyMonths = 0;
                        break;
                }
                switch (alertFreqCode)
                {
                    case 1:
                        frequencyDays = 0;
                        break;
                    case 2:
                        frequencyDays = 1;
                        break;
                    case 3:
                        frequencyDays = 7;
                        break;
                    case 6:
                        frequencyDays = 14;
                        break;
                    default:
                        frequencyDays = 0;
                        break;
                }
                nextOccurrence = DateTime.Parse(month + "/" + day + "/" + year);

                //if (nextOccurrence < currentDate)
                //{
                //    year = currentDate.Year;
                //    if (eventCode == "SIP")
                //    {
                //        month = currentDate.Month;
                //    }

                //    nextOccurrence = DateTime.Parse(month + "/" + day + "/" + year);

                while (nextOccurrence < currentDate)
                {
                    month += frequencyMonths;
                    if (month > 12)
                    {
                        month -= 12;
                        year += 1;
                    }
                    //day += frequencyDays;

                    nextOccurrence = DateTime.Parse(month + "/" + day + "/" + year);
                }
                //}
                if (reminderDays > 0)
                {
                    nextOccurrence = nextOccurrence.AddDays(-reminderDays);
                    //nextOccurrence.Subtract(ts);
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

                FunctionInfo.Add("Method", "CustomerDateAlertManagement.ascx:GetNextOccurrence()");

                object[] objects = new object[5];
                objects[0] = startDate;
                objects[1] = endDate;
                objects[2] = systematicDate;
                objects[3] = alertFreqCode;
                objects[4] = reminderDays;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return nextOccurrence;
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            int EventID = int.Parse(Session[SessionContents.EventID].ToString());
            //eventCode = Session[SessionContents.EventCode].ToString();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            userId = userVo.UserId;
            customerId = customerVo.CustomerId;

            foreach (GridViewRow dr in gvDateAlerts.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                if (checkBox.Checked)
                {
                    int EventSetupID = Convert.ToInt32(gvDateAlerts.DataKeys[dr.RowIndex].Value);
                    if (alertsBo.DeleteReminderEvent(EventSetupID))
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