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
    public partial class CustomerFIAlerts : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        DataSet dsCustomerFIAlerts = new DataSet();
        int rmId;
        int userId;
        int EventID;
        string eventType;
        string eventCode;
        int fiNPId;
        int accountId;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    userVo = (UserVo)Session[SessionContents.UserVo];
                    if (userVo.UserType == "Customer")
                    {
                        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    }
                    else
                    {
                        rmVo = (RMVo)Session[SessionContents.RmVo];
                        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                        rmId = rmVo.RMId;
                    }
                    userId = userVo.UserId;

                    BindCustomerFIAlertGrid();
                    tblReminderEdit.Visible = false;
                    tblFIAlertGrid.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerFIAlert.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindCustomerFIAlertGrid()
        {

            DataTable dtCustomerFIAlerts = new DataTable();
            DataRow drFIAlerts;
            //int count;
            try
            {

                dsCustomerFIAlerts = alertsBo.GetCustomerFIAlerts(customerVo.CustomerId);

                if (dsCustomerFIAlerts.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Visible = false;

                    dtCustomerFIAlerts.Columns.Add("FINPId");
                    dtCustomerFIAlerts.Columns.Add("AccountId");
                    dtCustomerFIAlerts.Columns.Add("FIInvestment");
                    dtCustomerFIAlerts.Columns.Add("ReccurringDepositReminder");
                    dtCustomerFIAlerts.Columns.Add("FDMaturityReminder");

                    foreach (DataRow dr in dsCustomerFIAlerts.Tables[0].Rows)
                    {
                        drFIAlerts = dtCustomerFIAlerts.NewRow();

                        drFIAlerts[0] = dr["FIALT_FINPId"].ToString();
                        drFIAlerts[1] = dr["FIALT_AccountId"].ToString();
                        drFIAlerts[2] = dr["FIALT_FIInvestment"].ToString().Trim();
                        if (dr["FIALT_RDReminder"].ToString() != null)
                            drFIAlerts[3] = dr["FIALT_RDReminder"].ToString();
                        if (dr["FIALT_FDMaturityReminder"].ToString() != null)
                            drFIAlerts[4] = dr["FIALT_FDMaturityReminder"].ToString();

                        dtCustomerFIAlerts.Rows.Add(drFIAlerts);
                    }

                    gvFIAlerts.DataSource = dtCustomerFIAlerts;
                    gvFIAlerts.DataBind();
                    gvFIAlerts.Visible = true;
                    //this.GetPageCount();

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

                FunctionInfo.Add("Method", "CustomerFIAlert.ascx:BindCustomerFIAlertGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvFIAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            Button btn;
            Label lbl;

            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILL THIS WILL ALL THE CONTROLS IN THE GRID
                if (dsCustomerFIAlerts.Tables[0].Rows[gvFIAlerts.Rows.Count]["FIALT_RDReminder"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnReccurringDepositReminder") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblReccurringDepositReminder") as Label;
                    lbl.Visible = false;

                }
                else
                {
                    btn = e.Row.FindControl("btnReccurringDepositReminder") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblReccurringDepositReminder") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerFIAlerts.Tables[0].Rows[gvFIAlerts.Rows.Count]["FIALT_FDMaturityReminder"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnFDMaturityReminder") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblFDMaturityReminder") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnFDMaturityReminder") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblFDMaturityReminder") as Label;
                    lbl.Visible = true;
                }
            }
        }

        protected void btnReccurringDepositReminder_Click(object sender, EventArgs e)
        {
            Button btnReccurringDepositReminder;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnReccurringDepositReminder = (Button)sender;
                gvr = (GridViewRow)btnReccurringDepositReminder.NamingContainer;
                index = gvr.RowIndex;
                fiNPId = int.Parse(gvFIAlerts.DataKeys[index].Values["FINPId"].ToString());
                accountId = int.Parse(gvFIAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["FINPId"] = fiNPId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "RDReminder";

                tblReminderEdit.Visible = true;
                tblFIAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFIAlert.ascx:btnReccurringDepositReminder_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnFDMaturityReminder_Click(object sender, EventArgs e)
        {
            Button btnFDMaturityReminder;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnFDMaturityReminder = (Button)sender;
                gvr = (GridViewRow)btnFDMaturityReminder.NamingContainer;
                index = gvr.RowIndex;
                fiNPId = int.Parse(gvFIAlerts.DataKeys[index].Values["FINPId"].ToString());
                accountId = int.Parse(gvFIAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["FINPId"] = fiNPId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "FDMaturityReminder";

                tblReminderEdit.Visible = true;
                tblFIAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFIAlert.ascx:btnFDMaturityReminder_Click()");

                object[] objects = new object[0];

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
                int reminderDays = int.Parse(txtReminderDays.Text.ToString());
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                accountId = int.Parse(Session["AccountId"].ToString());
                fiNPId = int.Parse(Session["FINPId"].ToString());
                eventType = Session["AlertType"].ToString();

                if (eventType == "RDReminder")
                {
                    alertsBo.SaveAdviserFDRecurringDepositReminderAlert(rmVo.RMId, customerVo.CustomerId, accountId, fiNPId, 0, reminderDays, userVo.UserId);
                }
                if (eventType == "FDMaturityReminder")
                {
                    alertsBo.SaveAdviserFDMaturityReminderAlert(rmVo.RMId, customerVo.CustomerId, accountId, fiNPId, 0, reminderDays, userVo.UserId);
                }

                BindCustomerFIAlertGrid();
                tblReminderEdit.Visible = false;
                tblFIAlertGrid.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFIAlert.ascx:btnSubmitReminder_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnDeleteSetup_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

                foreach (GridViewRow gvr in this.gvFIAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        fiNPId = int.Parse(gvFIAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        accountId = int.Parse(gvFIAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim());

                        alertsBo.DeleteCustomerAlertSetup(customerVo.CustomerId, accountId, fiNPId);
                    }
                }
                BindCustomerFIAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFIAlerts.ascx:btnDeleteSetup_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


    }
}