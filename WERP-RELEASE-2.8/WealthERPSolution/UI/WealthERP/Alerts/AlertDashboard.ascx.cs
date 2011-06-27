using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using VoUser;
using System.Text;
using System.Data;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAlerts;
using BoCustomerProfiling;
using BoCommon;

namespace WealthERP.Alerts
{
    public partial class AlertDashboard : UserControl
    {

        #region OldAlert

        //RMVo rmVo = new RMVo();
        //CustomerVo customerVo = new CustomerVo();
        //AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        //CustomerBo customerBo = new CustomerBo();
        //UserVo userVo = new UserVo();
        //AlertsBo alertsBo = new AlertsBo();
        //int rmId;
        //int userId;
        //int EventID;

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    if (!IsPostBack)
        //    {
        //        userVo = (UserVo)Session[SessionContents.UserVo];
        //        rmVo = (RMVo)Session[SessionContents.RmVo];
        //        rmId = rmVo.RMId;
        //        userId = userVo.UserId;

        //        if (Request.QueryString["Clear"] != null)
        //        {
        //            Session[SessionContents.CustomerVo] = null;
        //        }

        //        if (Session[SessionContents.CustomerVo] != null)
        //        {
        //            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //            BindCustomerDashboardGrid(customerVo.CustomerId);
        //            pnlDashboardGrid.Visible = true;
        //            //ddlRMCustList.Visible = false;
        //            trChooseCustomer.Visible = false;
        //        }
        //        else
        //        {
        //            BindCustomerListDropDown(rmId);
        //            pnlDashboardGrid.Visible = false;
        //            //ddlRMCustList.Visible = true;
        //            trChooseCustomer.Visible = true;
        //        }
        //    }
        //}

        //private void BindCustomerListDropDown(int rmId)
        //{
        //    DataSet dsCustomerList = new DataSet();

        //    try
        //    {
        //        dsCustomerList = advStaffBo.GetRMCustomerList(rmId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AlertDashboard.ascx:BindCustomerListDropDown()");

        //        object[] objects = new object[1];
        //        objects[0] = rmId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //    if (dsCustomerList.Tables[0].Rows.Count > 0)
        //    {
        //        ddlRMCustList.DataSource = dsCustomerList.Tables[0];
        //        ddlRMCustList.DataTextField = "CustomerName";
        //        ddlRMCustList.DataValueField = "CustomerID";
        //        ddlRMCustList.DataBind();
        //        ddlRMCustList.Items.Insert(0, "Select your Customer");
        //    }
        //}

        //private void BindCustomerSpecificAlerts(string CustomerID)
        //{
        //    int CustID = Int32.Parse(CustomerID);
        //    AlertsBo alertsBo = new AlertsBo();
        //    DataSet dsAlerts;

        //    try
        //    {
        //        dsAlerts = alertsBo.GetListofAlerts("all");

        //        if (dsAlerts.Tables[0].Rows.Count > 0)
        //        {
        //            ddlAlertTypes.DataSource = dsAlerts.Tables[0];
        //            ddlAlertTypes.DataTextField = "EventName";
        //            ddlAlertTypes.DataValueField = "AEL_EventID";
        //            ddlAlertTypes.DataBind();
        //            ddlAlertTypes.Items.Insert(0, "Select an Alert Type");

        //            Session["AlertDS"] = dsAlerts;
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

        //        FunctionInfo.Add("Method", "AlertDashboard.ascx:BindCustomerSpecificAlerts()");

        //        object[] objects = new object[1];
        //        objects[0] = rmId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void ddlRMCustList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (ddlRMCustList.SelectedIndex != 0)
        //    //{
        //    //    BindCustomerSpecificAlerts(ddlRMCustList.SelectedValue);
        //    //}

        //    if (ddlRMCustList.SelectedIndex != 0)
        //    {
        //        customerVo = customerBo.GetCustomer(Int32.Parse(ddlRMCustList.SelectedValue));
        //        Session[SessionContents.CustomerVo] = customerVo;
        //        BindCustomerDashboardGrid(Int32.Parse(ddlRMCustList.SelectedValue));
        //        pnlDashboardGrid.Visible = true;
        //    }
        //    else
        //    {
        //        pnlDashboardGrid.Visible = false;
        //    }
        //}

        //private void BindCustomerDashboardGrid(int CustomerID)
        //{
        //    DataSet customerDashBoard;

        //    gvAlertDashboard.DataSource = null;
        //    gvAlertDashboard.DataBind();

        //    customerDashBoard = alertsBo.GetCustomerDashboard(CustomerID);

        //    if (customerDashBoard.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable dtAlertDashboard = new DataTable();

        //        dtAlertDashboard.Columns.Add("EventID");
        //        //dtAlertDashboard.Columns.Add("Subscribed");
        //        dtAlertDashboard.Columns.Add("Alert");
        //        dtAlertDashboard.Columns.Add("Type");
        //        //dtAlertDashboard.Columns.Add("Reminder");

        //        DataRow drAlertDash;
        //        foreach (DataRow dr in customerDashBoard.Tables[0].Rows)
        //        {
        //            drAlertDash = dtAlertDashboard.NewRow();

        //            drAlertDash[0] = dr["AEL_EventID"].ToString();
        //            //if (dr["SubscriptionStatus"].ToString() == "0")
        //            //    drAlertDash[1] = "false";
        //            //else
        //            //    drAlertDash[1] = "true";

        //            drAlertDash[1] = dr["AEL_EventCode"].ToString().Trim();

        //            if (dr["AEL_EventType"].ToString().Trim() == "Date")
        //            {
        //                drAlertDash[2] = "Reminder";
        //            }
        //            else if (dr["AEL_EventType"].ToString().Trim() == "Data")
        //            {
        //                drAlertDash[2] = "Occurence (Of Condition)";
        //            }
        //            else if (dr["AEL_EventType"].ToString().Trim() == "Transactional")
        //            {
        //                drAlertDash[2] = "Confirmation";
        //            }
        //            //if (dr["AEL_Reminder"].ToString().ToLower() == "false")
        //            //    drAlertDash[4] = "No";
        //            //else
        //            //    drAlertDash[4] = "Yes";

        //            dtAlertDashboard.Rows.Add(drAlertDash);
        //        }

        //        gvAlertDashboard.DataSource = dtAlertDashboard;
        //        gvAlertDashboard.DataBind();
        //        gvAlertDashboard.Visible = true;
        //    }
        //}

        //protected void ddlAlertTypes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlAlertTypes.SelectedIndex != 0)
        //    {   //A selection is made
        //        DataSet dsAlerts = (DataSet)Session["AlertDS"];
        //        customerVo = customerBo.GetCustomer(Int32.Parse(ddlRMCustList.SelectedValue));

        //        foreach (DataRow dr in dsAlerts.Tables[0].Rows)
        //        {
        //            if (ddlAlertTypes.SelectedValue == dr["AEL_EventID"].ToString())
        //            {
        //                if (dr["AEL_EventType"].ToString().Trim().ToLower() == "date")
        //                {
        //                    Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                    //Session[SessionContents.EventCode] = dr["AEL_EventCode"];
        //                    Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                    Session[SessionContents.CustomerVo] = customerVo;

        //                    // Redirect to the Customer Alert Management Screen
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerDateAlertManage','none');", true);
        //                }
        //                else if (dr["AEL_EventType"].ToString().Trim().ToLower() == "data")
        //                {
        //                    Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                    //Session[SessionContents.EventCode] = dr["AEL_EventCode"];
        //                    Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                    Session[SessionContents.CustomerVo] = customerVo;

        //                    // Redirect to the Customer Alert Management Screen
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerConditionalAlertManage','none');", true);
        //                }
        //                else if (dr["AEL_EventType"].ToString().Trim().ToLower() == "transactional")
        //                {
        //                    Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                    //Session[SessionContents.EventCode] = dr["AEL_EventCode"];
        //                    Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                    Session[SessionContents.CustomerVo] = customerVo;

        //                    // Redirect to the Customer Alert Management Screen
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerTransactionalAlertManage','none');", true);
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void gvAlertDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = 0;

        //    try
        //    {
        //        if (e.CommandName == "subscribe")
        //        {
        //            index = Convert.ToInt32(e.CommandArgument);
        //            EventID = int.Parse(gvAlertDashboard.DataKeys[index].Value.ToString());
        //            DataSet dsAlerts = alertsBo.GetListofAlerts("all");
        //            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //            //customerVo = customerBo.GetCustomer(Int32.Parse(ddlRMCustList.SelectedValue));

        //            foreach (DataRow dr in dsAlerts.Tables[0].Rows)
        //            {
        //                if (EventID.ToString() == dr["AEL_EventID"].ToString())
        //                {
        //                    if (dr["AEL_EventType"].ToString().Trim().ToLower() == "date")
        //                    {
        //                        Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                        Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                        Session[SessionContents.CustomerVo] = customerVo;

        //                        // Redirect to the Customer Alert Management Screen
        //                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerDateAlertManage','none');", true);
        //                    }
        //                    else if (dr["AEL_EventType"].ToString().Trim().ToLower() == "data")
        //                    {
        //                        Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                        Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                        Session[SessionContents.CustomerVo] = customerVo;

        //                        // Redirect to the Customer Alert Management Screen
        //                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerConditionalAlertManage','none');", true);
        //                    }
        //                    else if (dr["AEL_EventType"].ToString().Trim().ToLower() == "transactional")
        //                    {
        //                        Session[SessionContents.EventID] = dr["AEL_EventID"];
        //                        Session[SessionContents.Reminder] = dr["AEL_Reminder"];
        //                        Session[SessionContents.CustomerVo] = customerVo;

        //                        // Redirect to the Customer Alert Management Screen
        //                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerTransactionalAlertManage','none');", true);
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

        //        FunctionInfo.Add("Method", "ViewBranches.ascx:gvBranchlist_RowCommand()");

        //        object[] objects = new object[1];
        //        objects[0] = index;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void gvAlertDashboard_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //}

        //protected void gvAlertDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddlCond;
        //        ddlCond = e.Row.FindControl("ddlCondition") as DropDownList;
        //        HiddenField hdnCond = e.Row.FindControl("hdnCondition") as HiddenField;

        //        if (hdnCond != null)
        //        {

        //        }
        //    }
        //}
        //}
        #endregion

        #region NewAlert

        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        AdvisorVo advisorVo = new AdvisorVo();
        int advisorId;
        int rmId;
        int userId;
        int EventID;
        string eventType;
        string eventCode;
        int count = 0;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        private static int sipReminder = 2;
        private static int swpReminder = 2;
        private static int dobReminder = 2;
        private static int anniversaryReminder = 2;
        private static int elssReminder = 2;
        private static int fdMaturityReminder = 2;
        private static int fdRecurringReminder = 2;
        private static int insuranceReminder = 2;
        private static int propertyOccurrence = 10;
        private static int personalOccurrence = 10;
        private static string propertyCondition = ">";
        private static string personalCondition = "<";
        private static string mfProfitBookingCondition = ">";
        private static string mfStopLossCondition = "<";
        private static int mfProfitBookingPreset = 10;
        private static int mfStopLossPreset = 10;
        private static string eqProfitBookingCondition = ">";
        private static string eqStopLossCondition = "<";
        private static int eqProfitBookingPreset = 10;
        private static int eqStopLossPreset = 10;

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
                FunctionInfo.Add("Method", "AlertDashboard.ascx.cs:OnInit()");
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
                BindSystemAlertsGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AlertDashboard.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "AlertDasboard.ascx.cs:GetPageCount()");
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
                    advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                    advisorId = advisorVo.advisorId;
                    //rmVo = (RMVo)Session[SessionContents.RmVo];
                    //if (Session[SessionContents.RmVo] != null)
                    //{
                    //    rmId = rmVo.RMId;
                    //}
                    userId = userVo.UserId;
                    tblOccurrenceEdit.Visible = false;
                    tblReminderEdit.Visible = false;
                    lblError.Visible = false;
                    lblSolution.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    tblPager.Visible = false;
                    //if (Request.QueryString["Clear"] != null)
                    //{
                    //    Session[SessionContents.CustomerVo] = null;
                    //}

                    if (Session[SessionContents.CustomerVo] != null)
                    {
                        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                        pnlDashboardGrid.Visible = true;
                        //ddlRMCustList.Visible = false;
                        //trChooseCustomer.Visible = false;
                    }
                    if (!IsPostBack)
                    {
                        sipReminder = 2;
                        swpReminder = 2;
                        dobReminder = 2;
                        anniversaryReminder = 2;
                        elssReminder = 2;
                        fdMaturityReminder = 2;
                        fdRecurringReminder = 2;
                        insuranceReminder = 2;
                        propertyOccurrence = 10;
                        personalOccurrence = 10;
                        propertyCondition = ">";
                        personalCondition = "<";
                        mfProfitBookingCondition = ">";
                        mfStopLossCondition = "<";
                        mfProfitBookingPreset = 10;
                        mfStopLossPreset = 10;
                        eqProfitBookingCondition = ">";
                        eqStopLossCondition = "<";
                        eqProfitBookingPreset = 10;
                        eqStopLossPreset = 10;
                    }
                    BindSystemAlertsGrid();
                    //else
                    //{
                    //    BindCustomerListDropDown(rmId);
                    //    pnlDashboardGrid.Visible = false;
                    //    //ddlRMCustList.Visible = true;
                    //    trChooseCustomer.Visible = true;
                    //}
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

                FunctionInfo.Add("Method", "AlertDashboard.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindSystemAlertsGrid()
        {
            DataSet dsSystemAlerts = new DataSet();
            DataTable dtSystemAlerts = new DataTable();
            DataRow drAlertType;
            int count;
            try
            {
                dsSystemAlerts = alertsBo.GetSystemAlerts(mypager.CurrentPage, out count);
                //Session["SystemAlerts"] = dsSystemAlerts;
                if (count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                    tblPager.Visible = false;
                }
                if (dsSystemAlerts.Tables[0].Rows.Count > 0)
                {
                    dtSystemAlerts.Columns.Add("EventId");
                    dtSystemAlerts.Columns.Add("EventType");
                    dtSystemAlerts.Columns.Add("EventCode");
                    dtSystemAlerts.Columns.Add("Alert");
                    dtSystemAlerts.Columns.Add("Default");

                    foreach (DataRow dr in dsSystemAlerts.Tables[0].Rows)
                    {
                        drAlertType = dtSystemAlerts.NewRow();

                        drAlertType[0] = dr["AEL_EventID"].ToString();
                        drAlertType[1] = dr["AEL_EventType"].ToString();
                        drAlertType[2] = dr["AEL_EventCode"].ToString().Trim();

                        if (dr["AEL_EventType"].ToString().Trim() == "Date")
                        {
                            drAlertType[3] = dr["AEL_EventCode"].ToString().Trim() + " Reminder";
                        }
                        else if (dr["AEL_EventType"].ToString().Trim() == "Data")
                        {
                            drAlertType[3] = dr["AEL_EventCode"].ToString().Trim() + " Occurence (Of Condition)";
                        }
                        else if (dr["AEL_EventType"].ToString().Trim() == "Transactional")
                        {
                            drAlertType[3] = dr["AEL_EventCode"].ToString().Trim() + " Confirmation";
                        }

                        switch (dr["AEL_EventCode"].ToString().Trim())
                        {
                            case "SIP":
                                if (dr["AEL_EventType"].ToString().Trim() == "Date")
                                    drAlertType[4] = sipReminder + " days before";
                                break;
                            case "SWP":
                                if (dr["AEL_EventType"].ToString().Trim() == "Date")
                                    drAlertType[4] = swpReminder + " days before";
                                break;
                            case "Anniversary":
                                drAlertType[4] = anniversaryReminder + " days before";
                                break;
                            case "DOB":
                                drAlertType[4] = dobReminder + " days before";
                                break;
                            case "ELSS Maturity":
                                drAlertType[4] = elssReminder + " days before";
                                break;
                            case "Insurance Premium payment":
                                drAlertType[4] = insuranceReminder + " days before";
                                break;
                            case "FD/Recurring Deposit":
                                drAlertType[4] = fdRecurringReminder + " days before";
                                break;
                            case "Bank FD Maturity":
                                drAlertType[4] = fdMaturityReminder + " days before";
                                break;
                            case "Personal":
                                drAlertType[4] = propertyCondition + propertyOccurrence;
                                break;
                            case "Property":
                                drAlertType[4] = propertyCondition + personalOccurrence;
                                break;
                            case "MF Absolute Stop Loss":
                                drAlertType[4] = mfStopLossCondition + mfStopLossPreset;
                                break;
                            case "MF Absolute Profit booking":
                                drAlertType[4] = mfProfitBookingCondition + mfProfitBookingPreset;
                                break;
                            case "Equity Absolute stop Loss":
                                drAlertType[4] = eqStopLossCondition + eqStopLossPreset;
                                break;
                            case "Equity Absolute Profit booking":
                                drAlertType[4] = eqProfitBookingCondition + eqProfitBookingPreset;
                                break;
                            default:
                                drAlertType[4] = "-";
                                break;
                        }

                        dtSystemAlerts.Rows.Add(drAlertType);
                    }
                    pnlDashboardGrid.Visible = true;
                    gvSystemAlerts.DataSource = dtSystemAlerts;
                    gvSystemAlerts.DataBind();
                    gvSystemAlerts.Visible = true;
                    this.GetPageCount();
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

                FunctionInfo.Add("Method", "AlertDashboard.ascx:BindSystemAlertsGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvSystemAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            DataSet dsSystemAlerts = new DataSet();
            try
            {
                if (e.CommandName == "subscribe")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    EventID = int.Parse(gvSystemAlerts.DataKeys[index].Values["EventID"].ToString());
                    eventType = gvSystemAlerts.DataKeys[index].Values["EventType"].ToString().Trim();
                    eventCode = gvSystemAlerts.DataKeys[index].Values["EventCode"].ToString().Trim();
                    //DataSet dsAlerts = alertsBo.GetListofAlerts("all");
                    //customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    //customerVo = customerBo.GetCustomer(Int32.Parse(ddlRMCustList.SelectedValue));

                    //foreach (DataRow dr in dsAlerts.Tables[0].Rows)
                    //{
                    //if (EventID.ToString() == dr["AEL_EventID"].ToString())
                    //{
                    Session[SessionContents.EventID] = EventID;
                    Session[SessionContents.EventCode] = eventCode;

                    if (eventType == "Date")
                    {
                        // Redirect to the Customer Alert Management Screen
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerDateAlertManage','none');", true);
                    }
                    else if (eventType == "Data")
                    {
                        // Redirect to the Customer Alert Management Screen
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerConditionalAlertManage','none');", true);
                    }
                    else if (eventType == "Transactional")
                    {
                        // Redirect to the Customer Alert Management Screen
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerTransactionalAlertManage','none');", true);
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

                FunctionInfo.Add("Method", "AlertDashboard.ascx:gvSystemAlerts_RowCommand()");

                object[] objects = new object[1];
                objects[0] = index;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }//NOT USED

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                advisorId = advisorVo.advisorId;
                rmVo = (RMVo)Session[SessionContents.RmVo];
                rmId = rmVo.RMId;
                userId = userVo.UserId;

                foreach (GridViewRow gvr in this.gvSystemAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        EventID = int.Parse(gvSystemAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        eventType = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim();
                        eventCode = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[2].ToString().Trim();
                        count = alertsBo.ChkAdviserAlertSetup(userId, EventID);

                        if (eventCode == "SIP" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserSIPReminderAlert(advisorId, 0, 0, 0, 1, sipReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }

                        }
                        if (eventCode == "SWP" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserSWPReminderAlert(advisorId, 0, 0, 0, 1, swpReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "SIP" && eventType == "Transactional")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserSIPConfirmationAlert(advisorId, 0, 0, 0, 1, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Confirmation";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "SWP" && eventType == "Transactional")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserSWPConfirmationAlert(advisorId, 0, 0, 0, 1, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Confirmation";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Anniversary" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserAnniversaryReminderAlert(advisorId, userId, anniversaryReminder);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "DOB" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserDOBReminderAlert(advisorId, userId, dobReminder);
                            else
                            {
                                lblError.Text = lblError.Text + "<br />" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "ELSS Maturity" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserELSSMaturityReminderAlert(advisorId, 0, 0, 0, 1, elssReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Insurance Premium payment" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserInsuranceReminderAlert(advisorId, 0, 0, 0, 1, insuranceReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "FD/Recurring Deposit" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserFDRecurringDepositReminderAlert(advisorId, 0, 0, 0, 1, fdRecurringReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Bank FD Maturity" && eventType == "Date")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserFDMaturityReminderAlert(advisorId, 0, 0, 0, 1, fdMaturityReminder, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Reminder";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "MF Dividend" && eventType == "Transactional")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserMFDividendConfirmationAlert(advisorId, 0, 0, 0, 1, userId);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Confirmation";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Property" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserPropertyOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, propertyCondition, propertyOccurrence);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Personal" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserPersonalOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, personalCondition, personalOccurrence);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "MF Absolute Stop Loss" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserMFStopLossOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, mfStopLossCondition, mfStopLossPreset);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "MF Absolute Profit booking" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserMFProfitBookingOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, mfProfitBookingCondition, mfProfitBookingPreset);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Equity Absolute stop Loss" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserEQStopLossOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, mfStopLossCondition, mfStopLossPreset);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }
                        if (eventCode == "Equity Absolute Profit booking" && eventType == "Data")
                        {
                            if (count == 0)
                                alertsBo.SaveAdviserEQProfitBookingOccurrenceAlert(advisorId, 0, 0, 0, 1, userId, mfProfitBookingCondition, mfProfitBookingPreset);
                            else
                            {
                                lblError.Text = lblError.Text + "<br>" + eventCode.ToString() + " Occurrence";
                                lblError.Visible = true;
                                lblSolution.Visible = true;
                            }
                        }

                    }
                }
                BindSystemAlertsGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AlertDashboard.ascx:btnSubmit_Click()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int reminderSelected = 0;
                int occurrenceSelected = 0;
                foreach (GridViewRow gvr in this.gvSystemAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        eventType = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim();
                        if (eventType == "Date")
                            reminderSelected += 1;
                        if (eventType == "Data")
                            occurrenceSelected += 1;
                    }
                }
                if (reminderSelected > 0)
                    tblReminderEdit.Visible = true;
                if (occurrenceSelected > 0)
                    tblOccurrenceEdit.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AlertDashboard.ascx:btnEdit_Click()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnSubmitReminder_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in this.gvSystemAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        EventID = int.Parse(gvSystemAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        eventType = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim();
                        eventCode = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[2].ToString().Trim();

                        if (eventCode == "SIP" && eventType == "Date")
                        {
                            sipReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "SWP" && eventType == "Date")
                        {
                            swpReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "Anniversary" && eventType == "Date")
                        {
                            anniversaryReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "DOB" && eventType == "Date")
                        {
                            dobReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "ELSS Maturity" && eventType == "Date")
                        {
                            elssReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "Insurance Premium payment" && eventType == "Date")
                        {
                            insuranceReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "FD/Recurring Deposit" && eventType == "Date")
                        {
                            fdRecurringReminder = int.Parse(txtReminderDays.Text.ToString());
                        }
                        if (eventCode == "Bank FD Maturity" && eventType == "Date")
                        {
                            fdMaturityReminder = int.Parse(txtReminderDays.Text.ToString());
                        }

                        tblReminderEdit.Visible = false;
                        BindSystemAlertsGrid();
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
                FunctionInfo.Add("Method", "AlertDashboard.ascx:btnSubmitReminder_Click()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnSubmitOccurrence_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in this.gvSystemAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        EventID = int.Parse(gvSystemAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        eventType = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim();
                        eventCode = gvSystemAlerts.DataKeys[gvr.RowIndex].Values[2].ToString().Trim();

                        if (eventCode == "Property" && eventType == "Data")
                        {
                            propertyCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            propertyOccurrence = int.Parse(txtOccurrencePreset.Text.ToString());
                        }
                        if (eventCode == "Personal" && eventType == "Data")
                        {
                            personalCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            personalOccurrence = int.Parse(txtOccurrencePreset.Text.ToString());
                        }
                        if (eventCode == "MF Absolute Stop Loss" && eventType == "Data")
                        {
                            //mfStopLossCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            mfStopLossPreset = int.Parse(txtOccurrencePreset.Text.ToString());
                        }
                        if (eventCode == "MF Absolute Profit booking" && eventType == "Data")
                        {
                            //mfProfitBookingCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            mfProfitBookingPreset = int.Parse(txtOccurrencePreset.Text.ToString());
                        }
                        if (eventCode == "Equity Absolute stop Loss" && eventType == "Data")
                        {
                            //eqStopLossCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            eqStopLossPreset = int.Parse(txtOccurrencePreset.Text.ToString());
                        }
                        if (eventCode == "Equity Absolute Profit booking" && eventType == "Data")
                        {
                            //eqProfitBookingCondition = ddlOccurrenceCondition.SelectedItem.Value;
                            eqProfitBookingPreset = int.Parse(txtOccurrencePreset.Text.ToString());
                        }

                        tblOccurrenceEdit.Visible = false;
                        BindSystemAlertsGrid();
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
                FunctionInfo.Add("Method", "AlertDashboard.ascx:btnSubmitOccurrence_Click()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblSolution.Visible = false;
                userVo = (UserVo)Session[SessionContents.UserVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                rmId = rmVo.RMId;
                userId = userVo.UserId;
                foreach (GridViewRow gvr in this.gvSystemAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        EventID = int.Parse(gvSystemAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());

                        alertsBo.DeleteAdviserAlertSetup(userId, EventID);

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
                FunctionInfo.Add("Method", "AlertDashboard.ascx:btnReset_Click()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        #endregion





    }

}