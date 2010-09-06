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
    public partial class CustomerInsuranceAlerts : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        DataSet dsCustomerINAlerts = new DataSet();
        int rmId;
        int userId;
        int EventID;
        string eventType;
        string eventCode;
        int inNPId;
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

                    BindCustomerINAlertGrid();
                    tblReminderEdit.Visible = false;
                    tblINAlertGrid.Visible = true;

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

                FunctionInfo.Add("Method", "CustomerInsuranceAlerts.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindCustomerINAlertGrid()
        {

            DataTable dtCustomerINAlerts = new DataTable();
            DataRow drINAlerts;
            //int count;
            try
            {
                dsCustomerINAlerts = alertsBo.GetCustomerInsuranceAlerts(customerVo.CustomerId);

                if (dsCustomerINAlerts.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Visible = false;

                    dtCustomerINAlerts.Columns.Add("INNPId");
                    dtCustomerINAlerts.Columns.Add("AccountId");
                    dtCustomerINAlerts.Columns.Add("InsurancePolicy");
                    dtCustomerINAlerts.Columns.Add("PremiumPaymentReminder");

                    foreach (DataRow dr in dsCustomerINAlerts.Tables[0].Rows)
                    {
                        drINAlerts = dtCustomerINAlerts.NewRow();

                        drINAlerts[0] = dr["INALT_INNPId"].ToString();
                        drINAlerts[1] = dr["INALT_AccountId"].ToString();
                        drINAlerts[2] = dr["INALT_InsurancePolicy"].ToString().Trim();
                        if (dr["INALT_PremiumPaymentReminder"].ToString() != null)
                            drINAlerts[3] = dr["INALT_PremiumPaymentReminder"].ToString();

                        dtCustomerINAlerts.Rows.Add(drINAlerts);
                    }

                    gvINAlerts.DataSource = dtCustomerINAlerts;
                    gvINAlerts.DataBind();
                    gvINAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerInsuranceAlert.ascx:BindCustomerINAlertGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvINAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            Button btn;
            Label lbl;

            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dsCustomerINAlerts.Tables[0].Rows[gvINAlerts.Rows.Count]["INALT_PremiumPaymentReminder"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnPremiumPaymentReminder") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblPremiumPaymentReminder") as Label;
                    lbl.Visible = false;

                }
                else
                {
                    btn = e.Row.FindControl("btnPremiumPaymentReminder") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblPremiumPaymentReminder") as Label;
                    lbl.Visible = true;
                }
            }
        }

        protected void btnPremiumPaymentReminder_Click(object sender, EventArgs e)
        {
            Button btnPremiumPaymentReminder;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnPremiumPaymentReminder = (Button)sender;
                gvr = (GridViewRow)btnPremiumPaymentReminder.NamingContainer;
                index = gvr.RowIndex;
                inNPId = int.Parse(gvINAlerts.DataKeys[index].Values["INNPId"].ToString());
                accountId = int.Parse(gvINAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["INNPId"] = inNPId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "PremiumReminder";

                tblReminderEdit.Visible = true;
                tblINAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerInsuranceAlerts.ascx:btnPremiumPaymentReminder_Click()");

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
                inNPId = int.Parse(Session["INNPId"].ToString());
                eventType = Session["AlertType"].ToString();
                
                
                if (eventType == "PremiumReminder")
                {
                    alertsBo.SaveAdviserInsuranceReminderAlert(customerVo.RmId, customerVo.CustomerId, accountId, inNPId, 0, reminderDays, userVo.UserId);
                }

                BindCustomerINAlertGrid();
                tblReminderEdit.Visible = false;
                tblINAlertGrid.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerInsuranceAlerts.ascx:btnSubmitReminder_Click()");

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

                foreach (GridViewRow gvr in this.gvINAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        inNPId = int.Parse(gvINAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        accountId = int.Parse(gvINAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim());

                        alertsBo.DeleteCustomerAlertSetup(customerVo.CustomerId, accountId, inNPId);
                    }
                }
                BindCustomerINAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerInsuranceAlerts.ascx:btnDeleteSetup_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


    }
}