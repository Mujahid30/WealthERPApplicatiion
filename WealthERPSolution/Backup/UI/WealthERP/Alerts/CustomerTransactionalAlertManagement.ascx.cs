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
    public partial class CustomerTransactionalAlertManagement : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        AlertsSetupVo alertVo;
        List<AlertsSetupVo> AlertSetupList = null;
        DataSet dsSchemes = new DataSet();

        int userId;
        int customerId;
        string CustomerName;
        int EventID;
        string eventCode;
        int count;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        #region OldAlert

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    trAddNewEvent.Visible = false;
        //    if (!IsPostBack)
        //    {
        //        userVo = (UserVo)Session[SessionContents.UserVo];
        //        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //        userId = userVo.UserId;
        //        customerId = customerVo.CustomerId;
        //        CustomerName = customerVo.FirstName + " " + customerVo.LastName;

        //        lblCustomerName.Text = CustomerName;

        //        EventID = Int32.Parse(Session[SessionContents.EventID].ToString());

        //        BindAlertsDropDown("transactional", EventID);
        //        BindGridView(customerId, EventID);

        //        trAddNewTranxAlert.Visible = false;
        //        trTranxSave.Visible = false;
        //    }
        //}

        //private void BindGridView(int customerId, int EventID)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    gvTransactionalAlerts.DataSource = null;
        //    gvTransactionalAlerts.DataBind();

        //    AlertSetupList = alertsBo.GetListofEventsSubscribedByCustomer(customerId, EventID);

        //    if (AlertSetupList != null)
        //    {
        //        DataTable dtAlertSetup = new DataTable();

        //        // The following comes from Event Setup
        //        dtAlertSetup.Columns.Add("EventSetupID");
        //        dtAlertSetup.Columns.Add("Alert");
        //        dtAlertSetup.Columns.Add("Scheme");
        //        dtAlertSetup.Columns.Add("Message");

        //        DataRow drAlertSetup;
        //        for (int i = 0; i < AlertSetupList.Count; i++)
        //        {
        //            drAlertSetup = dtAlertSetup.NewRow();
        //            alertVo = new AlertsSetupVo();
        //            alertVo = AlertSetupList[i];

        //            drAlertSetup[0] = alertVo.EventSetupID.ToString().Trim();
        //            drAlertSetup[1] = alertVo.EventName.ToString().Trim();

        //            drAlertSetup[2] = GetSchemeName(alertVo.AllFieldNames, alertVo.SchemeID);
        //            if (alertVo.EventMessage.ToString().Trim() != "" || alertVo.EventMessage.ToString().Trim() !=null)
        //            {
        //                drAlertSetup[3] = alertVo.EventMessage.ToString().Trim();
        //            }
        //            else
        //            {
        //                drAlertSetup[3] = "";
        //            }


        //            dtAlertSetup.Rows.Add(drAlertSetup);
        //        }


        //        gvTransactionalAlerts.DataSource = dtAlertSetup;
        //        gvTransactionalAlerts.DataBind();
        //        gvTransactionalAlerts.Visible = true;
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
        //        dsAlerts = alertsBo.GetListofAlerts(type);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:BindAlertsDropDown()");

        //        object[] objects = new object[2];
        //        objects[0] = type;
        //        objects[1] = EventID;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    if (dsAlerts.Tables[0].Rows.Count > 0)
        //    {
        //        ddlTranxAlertTypes.DataSource = dsAlerts.Tables[0];
        //        ddlTranxAlertTypes.DataTextField = "EventName";
        //        ddlTranxAlertTypes.DataValueField = "AEL_EventID";
        //        ddlTranxAlertTypes.DataBind();
        //        ddlTranxAlertTypes.Items.Insert(0, "Please select an Alert Type");
        //        ddlTranxAlertTypes.SelectedValue = EventID.ToString();
        //    }
        //}

        //protected void gvTransactionalAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //}

        //protected void ddlTranxAlertTypes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    trAddNewTranxAlert.Visible = false;
        //    trTranxSave.Visible = false;

        //    if (ddlTranxAlertTypes.SelectedIndex != 0)
        //    {
        //        gvTransactionalAlerts.DataSource = null;
        //        gvTransactionalAlerts.DataBind();
        //        BindGridView(customerId, Int32.Parse(ddlTranxAlertTypes.SelectedValue));
        //    }
        //    else if (ddlTranxAlertTypes.SelectedIndex == 0)
        //    {
        //        gvTransactionalAlerts.DataSource = null;
        //        gvTransactionalAlerts.DataBind();
        //    }
        //}

        //protected void btnAddNewTranxEvent_Click(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;


        //    if (ddlTranxAlertTypes.SelectedIndex != 0)
        //    {
        //        trAddNewTranxAlert.Visible = true;
        //        trTranxSave.Visible = true;
        //        trAddNewEvent.Visible = true;

        //        string EventCode = string.Empty;
        //        // Get EventCode
        //        EventCode = alertsBo.GetEventCode(Int32.Parse(ddlTranxAlertTypes.SelectedValue));
        //        BindSchemeDropDown(Int32.Parse(ddlTranxAlertTypes.SelectedValue), customerId, EventCode);
        //    }
        //    trAddNewEvent.Visible = true;
        //}

        //private void BindSchemeDropDown(int EventID, int CustomerID, string EventCode)
        //{
        //    //DataTable dtSchemes = new DataTable();
        //    DataSet dsSchemes = new DataSet();

        //    try
        //    {
        //        dsSchemes = alertsBo.GetUnsubscribedSchemesList(EventID, CustomerID, "transactional", EventCode);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:BindSchemeDropDown()");

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
        //        ddlScheme.DataTextField = "TextField"; // "SchemeName" - Give this once the Scheme Name Doubt is clarified
        //        ddlScheme.DataValueField = "ValueField";
        //        ddlScheme.DataBind();
        //        ddlScheme.Items.Insert(0, "Select a Scheme");
        //        btnSaveTranxAlert.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlScheme.Items.Clear();
        //        ddlScheme.Items.Insert(0, "No Schemes!");
        //        btnSaveTranxAlert.Enabled = false;
        //    }

        //}

        //protected void btnSaveTranxAlert_Click(object sender, EventArgs e)
        //{
        //    int SchemeID;
        //    int targetId;
        //    string[] accIdSchemeId;
        //    string eventMessage;

        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;
        //    accIdSchemeId = ddlScheme.SelectedValue.Split(':');
        //    targetId = Int32.Parse(accIdSchemeId[0]);
        //    SchemeID = Int32.Parse(accIdSchemeId[1]);
        //    EventID = Int32.Parse(ddlTranxAlertTypes.SelectedValue);
        //    eventMessage = "Transaction has been Captured";

        //    if (alertsBo.SaveAlert(customerId, eventMessage, targetId, SchemeID, EventID, userId))
        //    {
        //        // Success Message
        //        BindGridView(customerId, EventID);
        //        trAddNewTranxAlert.Visible = false;
        //        trTranxSave.Visible = false;
        //    }
        //    else
        //    {
        //        // Display Failure Message
        //    }

        //}

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    int EventID = Int32.Parse(ddlTranxAlertTypes.SelectedValue);
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    foreach (GridViewRow dr in gvTransactionalAlerts.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        if (checkBox.Checked)
        //        {
        //            int EventSetupID = Convert.ToInt32(gvTransactionalAlerts.DataKeys[dr.RowIndex].Value);
        //            if (alertsBo.DeleteEvent(EventSetupID))
        //            {
        //                // Display Success Messages
        //            }
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
                FunctionInfo.Add("Method", "CustomerTransactionAlertManagement.ascx.cs:OnInit()");
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
                FunctionInfo.Add("Method", "CustomerTransactionAlertManagement.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "CustomerTransationAlertManagement.ascx.cs:GetPageCount()");
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

        /// <summary>
        /// Page load function retrives the necessary session variable and calls the function to bind
        /// customer alerts to the grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                trAddNewEvent.Visible = false;
                if (!IsPostBack)
                {
                    userVo = (UserVo)Session[SessionContents.UserVo];
                    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    eventCode = Session[SessionContents.EventCode].ToString();
                    userId = userVo.UserId;
                    customerId = customerVo.CustomerId;
                    CustomerName = customerVo.FirstName + " " + customerVo.LastName;

                    if (eventCode.ToString().Trim() != "")
                    {
                        lblHeader.Text = eventCode + " Confirmation Setup";
                    }

                    lblCustomerName.Text = CustomerName;

                    //retrieving the eventId of the event for which management is done in this page.
                    //this was placed in a session when the pick for this alert was done
                    EventID = Int32.Parse(Session[SessionContents.EventID].ToString());

                    //function call that binds the customer's subscribed alerts to the gridview
                    BindSubscribedEventsGridView(customerId, EventID);

                    //makes the controls required for adding new alert invisible.
                    trAddNewTranxAlt.Visible = false;
                    trTranxSave.Visible = false;
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

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// Function that binds the customer subscribed events to the grid view
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="EventID"></param>
        private void BindSubscribedEventsGridView(int customerId, int EventID)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                gvTransactionalAlerts.DataSource = null;
                gvTransactionalAlerts.DataBind();

                //function that retrieves the customer subscribed alerts from the DB
                AlertSetupList = alertsBo.GetCustomerSubscribedConfirmationAlerts(customerId, EventID, "Transactional",mypager.CurrentPage, out count);
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = true;
                }
                //if alerts are subscribed runs the piece of code below else displays that events dont exist
                if (AlertSetupList != null)
                {
                    DataTable dtAlertSetup = new DataTable();

                    // creating a datatable with all the necessary details to bind to the gridview
                    dtAlertSetup.Columns.Add("EventSetupID");
                    dtAlertSetup.Columns.Add("Details");
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

                        if (alertVo.EventMessage.ToString().Trim() != "" || alertVo.EventMessage.ToString().Trim() != null)
                        {
                            //Message that gets displayed when the alert is triggered
                            drAlertSetup[2] = alertVo.EventMessage.ToString().Trim();
                        }
                        else
                        {
                            drAlertSetup[2] = "";
                        }


                        dtAlertSetup.Rows.Add(drAlertSetup);
                    }

                    // binding the datatable to the gridview and making the grid visible
                    gvTransactionalAlerts.DataSource = dtAlertSetup;
                    gvTransactionalAlerts.DataBind();
                    gvTransactionalAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:BindSubscribedEventsGridView()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNewTranxEvent_Click(object sender, EventArgs e)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                eventCode = Session[SessionContents.EventCode].ToString();
                EventID = int.Parse(Session[SessionContents.EventID].ToString());
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                trAddNewTranxAlt.Visible = true;
                trTranxSave.Visible = true;
                trAddNewEvent.Visible = true;

                //string EventCode = string.Empty;
                // Get EventCode
                //EventCode = alertsBo.GetEventCode(Int32.Parse(ddlTranxAlertTypes.SelectedValue));
                //BindSchemeDropDown(Int32.Parse(ddlTranxAlertTypes.SelectedValue), customerId, EventCode);
                BindSchemeDropDown(customerId, eventCode, EventID);

                trAddNewEvent.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:btnAddNewTranxEvent_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="eventCode"></param>
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
                    btnSaveTranxAlert.Enabled = true;
                }
                else
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Insert(0, "No Schemes!");
                    btnSaveTranxAlert.Enabled = false;
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

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:BindSchemeDropDown()");

                object[] objects = new object[3];
                objects[0] = EventID;
                objects[1] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveTranxAlert_Click(object sender, EventArgs e)
        {
            try
            {
                dsSchemes = (DataSet)Session["UnsubscribedSchemes"];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                EventID = int.Parse(Session[SessionContents.EventID].ToString());
                customerId = customerVo.CustomerId;
                userId = userVo.UserId;
                

                if (dsSchemes.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSchemes.Tables[0].Rows)
                    {
                        if (dr["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedItem.Value.ToString())
                        {
                            alertVo = new AlertsSetupVo();

                            alertVo.CustomerId = customerId;
                            alertVo.EventID = int.Parse(Session[SessionContents.EventID].ToString());
                            alertVo.SchemeID = int.Parse(ddlScheme.SelectedItem.Value.ToString());
                            alertVo.TargetID = int.Parse(dr["CMFA_AccountId"].ToString());
                            alertVo.EventMessage = "Transaction has been Captured";

                            alertsBo.SaveConfirmationAlert(alertVo, userId);

                        }
                    }
                }
                BindSubscribedEventsGridView(customerId, EventID);
                trAddNewTranxAlt.Visible = false;
                trTranxSave.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:btnSaveTranxAlert_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected string GetSchemeName(string AllFieldNames, int SchemeID)
        {
            string schemeName;
            string[] allFieldsArray;
            string metatablePrimaryKey;
            DataSet dsmetatableDetails;
            DataSet dsSchemeName;
            string tableName;
            string description;

            try
            {
                allFieldsArray = AllFieldNames.Split(',');
                metatablePrimaryKey = allFieldsArray[1].Split('.')[1];
                dsmetatableDetails = alertsBo.GetMetatableDetails(metatablePrimaryKey);
                tableName = dsmetatableDetails.Tables[0].Rows[0][2].ToString();
                description = dsmetatableDetails.Tables[0].Rows[0][1].ToString();

                dsSchemeName = alertsBo.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeID);

                schemeName = dsSchemeName.Tables[0].Rows[0][0].ToString();


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionalAlertManagement.ascx:GetSchemeName()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return schemeName;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            int EventID = int.Parse(Session[SessionContents.EventID].ToString());
            //eventCode = Session[SessionContents.EventCode].ToString();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            userId = userVo.UserId;
            customerId = customerVo.CustomerId;

            foreach (GridViewRow dr in gvTransactionalAlerts.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                if (checkBox.Checked)
                {
                    int EventSetupID = Convert.ToInt32(gvTransactionalAlerts.DataKeys[dr.RowIndex].Value);
                    if (alertsBo.DeleteEvent(EventSetupID))
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