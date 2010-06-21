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
    public partial class CustomerMFAlert : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        DataSet dsCustomerMFAlerts = new DataSet();
        int rmId;
        int userId;
        int EventID;
        string eventType;
        string eventCode;
        int schemeId;
        int accountId;

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
                //BindSystemAlertsGrid();
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

                    BindCustomerMFAlertGrid();
                    tblPager.Visible = false;
                    tblReminderEdit.Visible = false;
                    tblOccurrenceEdit.Visible = false;
                    tblMFAlertGrid.Visible = true;

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

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindCustomerMFAlertGrid()
        {
            
            DataTable dtCustomerMFAlerts = new DataTable();
            DataRow drMFAlerts;
            //int count;
            try
            {
                dsCustomerMFAlerts = alertsBo.GetCustomerMFAlerts(customerVo.CustomerId);
                //Session["SystemAlerts"] = dsSystemAlerts;
                //if (count > 0)
                //{
                //    lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                //    tblPager.Visible = true;
                //}
                if (dsCustomerMFAlerts.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Visible = false;

                    dtCustomerMFAlerts.Columns.Add("SchemeId");
                    dtCustomerMFAlerts.Columns.Add("AccountId");
                    dtCustomerMFAlerts.Columns.Add("Scheme");
                    dtCustomerMFAlerts.Columns.Add("SIPReminder");
                    dtCustomerMFAlerts.Columns.Add("SIPConfirmation");
                    dtCustomerMFAlerts.Columns.Add("MFAbsoluteStopLoss");
                    dtCustomerMFAlerts.Columns.Add("MFAbsoluteProfitBooking");
                    dtCustomerMFAlerts.Columns.Add("SWPReminder");
                    dtCustomerMFAlerts.Columns.Add("SWPConfirmation");
                    dtCustomerMFAlerts.Columns.Add("ELSSMaturity");
                    dtCustomerMFAlerts.Columns.Add("DivTranxOccur");

                    foreach (DataRow dr in dsCustomerMFAlerts.Tables[0].Rows)
                    {
                        drMFAlerts = dtCustomerMFAlerts.NewRow();

                        drMFAlerts[0] = dr["MFALT_SchemeId"].ToString();
                        drMFAlerts[1] = dr["MFALT_AccountId"].ToString();
                        drMFAlerts[2] = dr["MFALT_SchemeName"].ToString().Trim();
                        if (dr["MFALT_SIPReminder"].ToString() != null)
                            drMFAlerts[3] = dr["MFALT_SIPReminder"].ToString();
                        if (dr["MFALT_SIPConfirmation"].ToString() != null)
                            drMFAlerts[4] = dr["MFALT_SIPConfirmation"].ToString();
                        if (dr["MFALT_MFAbsoluteStopLoss"].ToString() != null)
                            drMFAlerts[5] = dr["MFALT_MFAbsoluteStopLoss"].ToString();
                        if (dr["MFALT_MFAbsoluteProfitBooking"].ToString() != null)
                            drMFAlerts[6] = dr["MFALT_MFAbsoluteProfitBooking"].ToString();
                        if (dr["MFALT_SWPReminder"].ToString() != null)
                            drMFAlerts[7] = dr["MFALT_SWPReminder"].ToString().Trim();
                        if (dr["MFALT_SWPConfirmation"].ToString() != null)
                            drMFAlerts[8] = dr["MFALT_SWPConfirmation"].ToString();
                        if (dr["MFALT_ELSSMaturity"].ToString() != null)
                            drMFAlerts[9] = dr["MFALT_ELSSMaturity"].ToString();
                        if (dr["MFALT_DivTranx"].ToString() != null)
                            drMFAlerts[10] = dr["MFALT_DivTranx"].ToString().Trim();

                        dtCustomerMFAlerts.Rows.Add(drMFAlerts);
                    }

                    gvMFAlerts.DataSource = dtCustomerMFAlerts;
                    gvMFAlerts.DataBind();
                    gvMFAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:BindCustomerMFAlertGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvMFAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            Button btn;
            Label lbl;

            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILL THIS WILL ALL THE CONTROLS IN THE GRID
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_SIPReminder"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnSIPReminder") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblSIPReminder") as Label;
                    lbl.Visible = false;

                }
                else
                {
                    btn = e.Row.FindControl("btnSIPReminder") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblSIPReminder") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_SIPConfirmation"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnSIPConfirmation") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblSIPConfirmation") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnSIPConfirmation") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblSIPConfirmation") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_SWPReminder"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnSWPReminder") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblSWPReminder") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnSWPReminder") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblSWPReminder") as Label;
                    lbl.Visible = true;

                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_SWPConfirmation"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnSWPConfirmation") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblSWPConfirmation") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnSWPConfirmation") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblSWPConfirmation") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_ELSSMaturity"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnELSSMaturity") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblELSSMaturity") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnELSSMaturity") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblELSSMaturity") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_DivTranx"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnDivTranxOccur") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblDivTranxOccur") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnDivTranxOccur") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblDivTranxOccur") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_MFAbsoluteStopLoss"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnMFAbsoluteStopLoss") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblMFAbsoluteStopLoss") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnMFAbsoluteStopLoss") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblMFAbsoluteStopLoss") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerMFAlerts.Tables[0].Rows[gvMFAlerts.Rows.Count]["MFALT_MFAbsoluteProfitBooking"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnMFAbsoluteProfitBooking") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblMFAbsoluteProfitBooking") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnMFAbsoluteProfitBooking") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblMFAbsoluteProfitBooking") as Label;
                    lbl.Visible = true;
                }
            }
        }

        protected void btnSIPReminder_Click(object sender, EventArgs e)
        {
            Button btnSIPReminder;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnSIPReminder = (Button)sender;
                gvr = (GridViewRow)btnSIPReminder.NamingContainer;
                index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["SchemeId"] = schemeId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "SIPReminder";

                tblReminderEdit.Visible = true;
                tblMFAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSIPReminder_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSWPReminder_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSWPReminder = (Button)sender;
                GridViewRow gvr = (GridViewRow)btnSWPReminder.NamingContainer;
                int index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["SchemeId"] = schemeId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "SWPReminder";

                tblReminderEdit.Visible = true;
                tblMFAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSWPReminder_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnELSSMaturity_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnELSSMaturityReminder = (Button)sender;
                GridViewRow gvr = (GridViewRow)btnELSSMaturityReminder.NamingContainer;
                int index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["SchemeId"] = schemeId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "ELSSMaturity";

                tblReminderEdit.Visible = true;
                tblMFAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnELSSMaturity_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSIPConfirmation_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                Button btnSIPConfirmation = (Button)sender;
                GridViewRow gvr = (GridViewRow)btnSIPConfirmation.NamingContainer;
                int index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());

                alertsBo.SaveAdviserSIPConfirmationAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, userVo.UserId);

                BindCustomerMFAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSIPConfirmation_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSWPConfirmation_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                Button btnSWPConfirmation = (Button)sender;
                GridViewRow gvr = (GridViewRow)btnSWPConfirmation.NamingContainer;
                int index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());

                alertsBo.SaveAdviserSWPConfirmationAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, userVo.UserId);

                BindCustomerMFAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSWPConfirmation_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnDivTranxOccur_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                Button btnDivTranxConfirmation = (Button)sender;
                GridViewRow gvr = (GridViewRow)btnDivTranxConfirmation.NamingContainer;
                int index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());

                alertsBo.SaveAdviserMFDividendConfirmationAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, userVo.UserId);

                BindCustomerMFAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnDivTranxOccur_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnMFAbsoluteProfitBooking_Click(object sender, EventArgs e)
        {
            Button btnMFAbsoluteProfitBooking;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnMFAbsoluteProfitBooking = (Button)sender;
                gvr = (GridViewRow)btnMFAbsoluteProfitBooking.NamingContainer;
                index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["SchemeId"] = schemeId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "MFAbsoluteProfitBooking";

                ddlOccurrenceCondition.Items.Clear();
                ddlOccurrenceCondition.Items.Insert(0, ">");
                ddlOccurrenceCondition.Items.Insert(1, ">=");
                tblOccurrenceEdit.Visible = true;
                tblMFAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnMFAbsoluteProfitBooking_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnMFAbsoluteStopLoss_Click(object sender, EventArgs e)
        {
            Button btnMFAbsoluteStopLoss;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnMFAbsoluteStopLoss = (Button)sender;
                gvr = (GridViewRow)btnMFAbsoluteStopLoss.NamingContainer;
                index = gvr.RowIndex;
                schemeId = int.Parse(gvMFAlerts.DataKeys[index].Values["SchemeId"].ToString());
                accountId = int.Parse(gvMFAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["SchemeId"] = schemeId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "MFAbsoluteStopLoss";

                ddlOccurrenceCondition.Items.Clear();
                ddlOccurrenceCondition.Items.Insert(0, "<");
                ddlOccurrenceCondition.Items.Insert(1, "<=");
                tblOccurrenceEdit.Visible = true;
                tblMFAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnMFAbsoluteStopLoss_Click()");

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
                schemeId = int.Parse(Session["SchemeId"].ToString());
                eventType = Session["AlertType"].ToString();

                if (eventType == "SIPReminder")
                {
                    alertsBo.SaveAdviserSIPReminderAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, reminderDays, userVo.UserId);
                }
                if (eventType == "SWPReminder")
                {
                    alertsBo.SaveAdviserSWPReminderAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, reminderDays, userVo.UserId);
                }
                if (eventType == "ELSSMaturity")
                {
                    alertsBo.SaveAdviserELSSMaturityReminderAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, reminderDays, userVo.UserId);
                }

                BindCustomerMFAlertGrid();
                tblReminderEdit.Visible = false;
                tblMFAlertGrid.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSIPReminder_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSubmitOccurrence_Click(object sender, EventArgs e)
        {
            string condition;
            int preset;
            try
            {
                condition = ddlOccurrenceCondition.SelectedItem.Value.ToString();
                preset = int.Parse(txtOccurrencePreset.Text.ToString());
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
                accountId = int.Parse(Session["AccountId"].ToString());
                schemeId = int.Parse(Session["SchemeId"].ToString());
                eventType = Session["AlertType"].ToString();

                if (eventType == "MFAbsoluteProfitBooking")
                {
                    alertsBo.SaveAdviserMFProfitBookingOccurrenceAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, userVo.UserId, condition, preset);
                }
                if (eventType == "MFAbsoluteStopLoss")
                {
                    alertsBo.SaveAdviserMFStopLossOccurrenceAlert(rmVo.RMId, customerVo.CustomerId, accountId, schemeId, 0, userVo.UserId, condition, preset);
                }

                BindCustomerMFAlertGrid();
                tblOccurrenceEdit.Visible = false;
                tblMFAlertGrid.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlert.ascx:btnSubmitOccurrence_Click()");

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

                foreach (GridViewRow gvr in this.gvMFAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        schemeId = int.Parse(gvMFAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        accountId = int.Parse(gvMFAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim());

                        alertsBo.DeleteCustomerAlertSetup(customerVo.CustomerId, accountId, schemeId);
                    }
                }
                BindCustomerMFAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerMFAlerts.ascx:btnDeleteSetup_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}