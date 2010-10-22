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
    public partial class CustomerEQAlerts : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AlertsBo alertsBo = new AlertsBo();
        DataSet dsCustomerEQAlerts = new DataSet();
        int rmId;
        int userId;
        int EventID;
        string eventType;
        string eventCode;
        int scripId;
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

                    BindCustomerEQAlertGrid();
                    tblOccurrenceEdit.Visible = false;
                    tblEQAlertGrid.Visible = true;

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

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:Page_Load()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindCustomerEQAlertGrid()
        {

            DataTable dtCustomerEQAlerts = new DataTable();
            DataRow drEQAlerts;
            //int count;
            try
            {
                dsCustomerEQAlerts = alertsBo.GetCustomerEQAlerts(customerVo.CustomerId);

                if (dsCustomerEQAlerts.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Visible = false;

                    dtCustomerEQAlerts.Columns.Add("ScripId");
                    dtCustomerEQAlerts.Columns.Add("AccountId");
                    dtCustomerEQAlerts.Columns.Add("Scrip");
                    dtCustomerEQAlerts.Columns.Add("AbsoluteStopLoss");
                    dtCustomerEQAlerts.Columns.Add("AbsoluteProfitBooking");

                    foreach (DataRow dr in dsCustomerEQAlerts.Tables[0].Rows)
                    {
                        drEQAlerts = dtCustomerEQAlerts.NewRow();

                        drEQAlerts[0] = dr["EQALT_ScripId"].ToString();
                        drEQAlerts[1] = dr["EQALT_AccountId"].ToString();
                        drEQAlerts[2] = dr["EQALT_ScripName"].ToString().Trim();
                        if (dr["EQALT_AbsoluteStopLoss"].ToString() != null)
                            drEQAlerts[3] = dr["EQALT_AbsoluteStopLoss"].ToString();
                        if (dr["EQALT_AbsoluteProfitBooking"].ToString() != null)
                            drEQAlerts[4] = dr["EQALT_AbsoluteProfitBooking"].ToString();

                        dtCustomerEQAlerts.Rows.Add(drEQAlerts);
                    }

                    gvEQAlerts.DataSource = dtCustomerEQAlerts;
                    gvEQAlerts.DataBind();
                    gvEQAlerts.Visible = true;
                    //this.GetPageCount();

                }
                else
                {
                    lblMessage.Visible = true;
                    lblDisclaimer.Visible = false;
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

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:BindCustomerEQAlertGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvEQAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            Button btn;
            Label lbl;

            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILL THIS WILL ALL THE CONTROLS IN THE GRID
                if (dsCustomerEQAlerts.Tables[0].Rows[gvEQAlerts.Rows.Count]["EQALT_AbsoluteStopLoss"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnAbsoluteStopLoss") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblAbsoluteStopLoss") as Label;
                    lbl.Visible = false;

                }
                else
                {
                    btn = e.Row.FindControl("btnAbsoluteStopLoss") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblAbsoluteStopLoss") as Label;
                    lbl.Visible = true;
                }
                if (dsCustomerEQAlerts.Tables[0].Rows[gvEQAlerts.Rows.Count]["EQALT_AbsoluteProfitBooking"].ToString() == "")
                {

                    btn = e.Row.FindControl("btnAbsoluteProfitBooking") as Button;
                    btn.Visible = true;
                    lbl = e.Row.FindControl("lblAbsoluteProfitBooking") as Label;
                    lbl.Visible = false;
                }
                else
                {
                    btn = e.Row.FindControl("btnAbsoluteProfitBooking") as Button;
                    btn.Visible = false;
                    lbl = e.Row.FindControl("lblAbsoluteProfitBooking") as Label;
                    lbl.Visible = true;
                }
                
            }
        }

        protected void btnAbsoluteStopLoss_Click(object sender, EventArgs e)
        {
            Button btnEQAbsoluteStopLoss;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnEQAbsoluteStopLoss = (Button)sender;
                gvr = (GridViewRow)btnEQAbsoluteStopLoss.NamingContainer;
                index = gvr.RowIndex;
                scripId = int.Parse(gvEQAlerts.DataKeys[index].Values["ScripId"].ToString());
                accountId = int.Parse(gvEQAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["ScripId"] = scripId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "EQAbsoluteStopLoss";

                ddlOccurrenceCondition.Items.Clear();
                ddlOccurrenceCondition.Items.Insert(0, "<");
                ddlOccurrenceCondition.Items.Insert(1, "<=");
                tblOccurrenceEdit.Visible = true;
                tblEQAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:btnAbsoluteStopLoss_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnAbsoluteProfitBooking_Click(object sender, EventArgs e)
        {
            Button btnEQAbsoluteProfitBooking;
            int index;
            GridViewRow gvr = null;

            try
            {
                btnEQAbsoluteProfitBooking = (Button)sender;
                gvr = (GridViewRow)btnEQAbsoluteProfitBooking.NamingContainer;
                index = gvr.RowIndex;
                scripId = int.Parse(gvEQAlerts.DataKeys[index].Values["ScripId"].ToString());
                accountId = int.Parse(gvEQAlerts.DataKeys[index].Values["AccountId"].ToString());
                Session["ScripId"] = scripId;
                Session["AccountId"] = accountId;
                Session["AlertType"] = "EQAbsoluteProfitBooking";

                ddlOccurrenceCondition.Items.Clear();
                ddlOccurrenceCondition.Items.Insert(0, ">");
                ddlOccurrenceCondition.Items.Insert(1, ">=");
                tblOccurrenceEdit.Visible = true;
                tblEQAlertGrid.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:btnAbsoluteProfitBooking_Click()");

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
                scripId = int.Parse(Session["ScripId"].ToString());
                eventType = Session["AlertType"].ToString();

                if (eventType == "EQAbsoluteStopLoss")
                {
                    alertsBo.SaveAdviserEQStopLossOccurrenceAlert(rmVo.RMId, customerVo.CustomerId, accountId, scripId, 0, userVo.UserId, condition, preset);
                }
                if (eventType == "EQAbsoluteProfitBooking")
                {
                    alertsBo.SaveAdviserEQProfitBookingOccurrenceAlert(rmVo.RMId, customerVo.CustomerId, accountId, scripId, 0, userVo.UserId, condition, preset);
                }

                BindCustomerEQAlertGrid();
                tblOccurrenceEdit.Visible = false;
                tblEQAlertGrid.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:btnSubmitOccurrence_Click()");

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

                foreach (GridViewRow gvr in this.gvEQAlerts.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        scripId = int.Parse(gvEQAlerts.DataKeys[gvr.RowIndex].Values[0].ToString());
                        accountId = int.Parse(gvEQAlerts.DataKeys[gvr.RowIndex].Values[1].ToString().Trim());

                        alertsBo.DeleteCustomerAlertSetup(customerVo.CustomerId, accountId, scripId);
                    }
                }
                BindCustomerEQAlertGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerEQAlerts.ascx:btnDeleteSetup_Click()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


    }
}