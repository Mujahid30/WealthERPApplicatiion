using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAlerts;
using VoAlerts;
using System.Data;
using WealthERP.Base;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Alerts
{
    public partial class NotificationDashboard : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        AlertsNotificationVo alertNotificationVo;
        List<AlertsNotificationVo> AlertNotificationList = null;

        int rmId;
        int userId;
        int customerId;
        string metatablePrimaryKey;
        int count;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

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
                FunctionInfo.Add("Method", "NoficationDashboard.ascx.cs:OnInit()");
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
                BindCustomerSpecificNotifications(customerVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NoficationDashboard.ascx.cs:HandlePagerEvent()");
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
                FunctionInfo.Add("Method", "NotificationDashboard.ascx.cs:GetPageCount()");
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

                //BindCustomerListDropDown(rmId);
                
                pnlCustomerSelection.Visible = true;
                pnlNoEntries.Visible = false;
                pnlNotifications.Visible = false;
                trSelectCustomer.Visible = false;
                BindCustomerSpecificNotifications(customerVo);
            }
        }

        private void BindCustomerListDropDown(int rmId)
        {
            DataSet dsCustomerList = new DataSet();

            try
            {
                dsCustomerList = advStaffBo.GetRMCustomerList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "NotificationDashboard.ascx:BindCustomerListDropDown()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (dsCustomerList.Tables[0].Rows.Count > 0)
            {
                ddlRMCustList.DataSource = dsCustomerList.Tables[0];
                ddlRMCustList.DataTextField = "CustomerName";
                ddlRMCustList.DataValueField = "CustomerID";
                ddlRMCustList.DataBind();
                ddlRMCustList.Items.Insert(0, "Select your Customer");
            }
        }

        protected void ddlRMCustList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRMCustList.SelectedIndex != 0)
            {
                customerVo = customerBo.GetCustomer(Int32.Parse(ddlRMCustList.SelectedValue));
                Session[SessionContents.CustomerVo] = customerVo;

                // Bind the Notification Grid View
                BindCustomerSpecificNotifications(customerVo);
            }
            else
            {
                Session[SessionContents.CustomerVo] = null;
            }
        }

        private void BindCustomerSpecificNotifications(CustomerVo CustVO)
        {
            BindNotificationsGridView(CustVO);
            pnlCustomerSelection.Visible = false;
            pnlNotifications.Visible = true;
        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
        //    userId = userVo.UserId;
        //    customerId = customerVo.CustomerId;

        //    foreach (GridViewRow dr in gvNotification.Rows)
        //    {
        //        CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //        if (checkBox.Checked)
        //        {
        //            long NotificationID = Convert.ToInt64(gvNotification.DataKeys[dr.RowIndex].Value);
                    
        //             //Need an Extra Column in the Notification Table to mark a row as deleted

        //            if (alertsBo.DeleteAlertNotification(NotificationID))
        //            {
                        
        //            }
        //        }
        //    }
        //    BindCustomerSpecificNotifications(customerVo);

        //    // Success Message
        //    BindNotificationsGridView(customerVo);
        //}

        private void BindNotificationsGridView(CustomerVo CustVO)
        {
            lblCustomerName.Text = CustVO.FirstName + " " + CustVO.LastName;

            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            userId = userVo.UserId;
            customerId = customerVo.CustomerId;

            gvNotification.DataSource = null;
            gvNotification.DataBind();

            AlertNotificationList = alertsBo.GetCustomerNotifications(customerId, mypager.CurrentPage, hdnSort.Value, out count);
            if (count > 0)
            {
                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                tblPager.Visible = true;
            }
            if (AlertNotificationList != null)
            {
                DataTable dtAlertNotifications = new DataTable();

                dtAlertNotifications.Columns.Add("EventQueueID");
                dtAlertNotifications.Columns.Add("Category");
                dtAlertNotifications.Columns.Add("Alert");
                dtAlertNotifications.Columns.Add("Scheme");
                dtAlertNotifications.Columns.Add("Message");
                dtAlertNotifications.Columns.Add("NotifiedDate");

                DataRow drAlertNotifications;
                for (int i = 0; i < AlertNotificationList.Count; i++)
                {
                    drAlertNotifications = dtAlertNotifications.NewRow();
                    alertNotificationVo = new AlertsNotificationVo();
                    alertNotificationVo = AlertNotificationList[i];

                    drAlertNotifications[0] = alertNotificationVo.NotificationID.ToString().Trim();
                    if (alertNotificationVo.Category.ToString().Trim() == "Transactional")
                    {
                        drAlertNotifications[1] = "Confirmation";
                    }
                    else if (alertNotificationVo.Category.ToString().Trim() == "Date")
                    {
                        drAlertNotifications[1] = "Reminder";
                    }
                    else if (alertNotificationVo.Category.ToString().Trim() == "Data")
                    {
                        drAlertNotifications[1] = "Occurence";
                    }
                    drAlertNotifications[2] = alertNotificationVo.EventCode.ToString().Trim();
                    if (alertNotificationVo.SchemeID.ToString() == "" || alertNotificationVo.SchemeID.ToString() == "0")
                    {
                        drAlertNotifications[3] = alertNotificationVo.SchemeID.ToString().Trim();
                    }
                    else
                    {
                        drAlertNotifications[3] = alertNotificationVo.Name.ToString().Trim();
                    }
                    //if (alertNotificationVo.Reminder.ToString().ToLower() == "false")
                    //    drAlertNotifications[3] = "No";
                    //else
                    //    drAlertNotifications[3] = "Yes";

                    drAlertNotifications[4] = alertNotificationVo.EventMessage.ToString();
                    drAlertNotifications[5] = alertNotificationVo.PopulatedDate.ToShortDateString();

                    dtAlertNotifications.Rows.Add(drAlertNotifications);
                }

                gvNotification.DataSource = dtAlertNotifications;
                gvNotification.DataBind();
                gvNotification.Visible = true;
                this.GetPageCount();
            }
            else
            {
                pnlNoEntries.Visible = true;
                pnlCustomerSelection.Visible = false;
            }
        }

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            pnlNotifications.Visible = false;
            pnlNoEntries.Visible = false;
            gvNotification.DataSource = null;
            gvNotification.DataBind();
            ddlRMCustList.SelectedIndex = 0;
            pnlCustomerSelection.Visible = true;
        }

        protected string GetSchemeName(string alertType, int SchemeID)
        {
            string schemeName;
            
            DataSet dsmetatableDetails;
            DataSet dsSchemeName;
            string tableName;
            string description;

            if (alertType == "Property")
            {
                metatablePrimaryKey = "CPNP_PropertyNPId";
            }
            else if (alertType == "SIP" || alertType == "SWP" || alertType == "STP")
            {
                metatablePrimaryKey = "PASP_SchemePlanCode";
            }
            else if (alertType == "Personal")
            {
                metatablePrimaryKey = "CPNP_PersonalNPId";
            }
            dsmetatableDetails = alertsBo.GetMetatableDetails(metatablePrimaryKey);
            tableName = dsmetatableDetails.Tables[0].Rows[0][2].ToString();
            description = dsmetatableDetails.Tables[0].Rows[0][1].ToString();

            dsSchemeName = alertsBo.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeID);

            schemeName = dsSchemeName.Tables[0].Rows[0][0].ToString();

            return schemeName;

        }

    }
}