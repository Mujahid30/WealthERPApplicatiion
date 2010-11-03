using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoEmailSMS;
using VoUser;
using BoAdvisorProfiling;
using BoAlerts;
using BoCommon;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Advisor
{
    public partial class AdviserCustomerSMSAlerts : System.Web.UI.UserControl
    {
        DataSet dsAdviserCustomerAlerts;
        AlertsBo alertsBo = new AlertsBo();
        AdvisorVo advisorVo = new AdvisorVo();
        //DataSet dsTest = new DataSet();
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
                FunctionInfo.Add("Method", "AdviserCustomerSMSAlert.ascx.cs:OnInit()");
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
                this.GetAdviserCustomerAlerts(out dsAdviserCustomerAlerts);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomerSMSAlert.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];

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
                if (hdnCount.Value != "")
                    rowCount = Convert.ToInt32(hdnCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "AdviserCustomerSMSAlert.ascx.cs:GetPageCount()");
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
            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;                
                GetAdviserCustomerAlerts(out dsAdviserCustomerAlerts);
            }
            setAdviserSMSLicenseInfo();
            SuccessMessage.Visible = false;
            advisorVo = (AdvisorVo)Session["AdvisorVo"];
        }
        public void GetAdviserCustomerAlerts(out DataSet dsAdviserCustomerAlerts)
        {
            int adviserId = 0;
            int id = 0;
            int Count = 0;
            string usertype = null;
            hdnCustomerIdWithoutMobileNumber.Value = "";
            hdnCustomerNameWithoutMobileNumber.Value = "";
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }
            DataRow drAdviserCustomerAlert = null;
            if (Session["UserType"] == "rm")
            {
                if (Session["CurrentrmVo"] != null)
                {
                    id = ((RMVo)Session["CurrentrmVo"]).RMId;
                    usertype = "rm";
                }
                else if (Session["rmVo"] != null)
                {
                    id = ((RMVo)Session["rmVo"]).RMId;
                    usertype = "rm";
                    //Session["UserType"] = null;
                }
            }
            else
            {
                if (Session["advisorVo"] != null)
                {
                    id = ((AdvisorVo)Session["advisorVo"]).advisorId;
                    usertype = "adviser";
                }
            }
            AlertsBo alertsBo = new AlertsBo();

            dsAdviserCustomerAlerts = alertsBo.GetAdviserCustomerSMSAlerts(id, usertype, mypager.CurrentPage,hdnNameFilter.Value.Trim(), out Count);
            ViewState["vsDsAdviserCustomerAlert"] = dsAdviserCustomerAlerts;
            lblTotalRows.Text = hdnCount.Value = Count.ToString();
            if (dsAdviserCustomerAlerts.Tables[0].Rows.Count > 0)
            {
                DataTable dtAdviserCustomerAlerts = new DataTable();
                dtAdviserCustomerAlerts.Columns.Add("CustomerId");
                dtAdviserCustomerAlerts.Columns.Add("AlertId");
                dtAdviserCustomerAlerts.Columns.Add("CustomerName");
                dtAdviserCustomerAlerts.Columns.Add("Name");
                dtAdviserCustomerAlerts.Columns.Add("AlertMessage");
                dtAdviserCustomerAlerts.Columns.Add("TimesSMSSent");
                dtAdviserCustomerAlerts.Columns.Add("LastSMSDate");
                dtAdviserCustomerAlerts.Columns.Add("AlertDate");
                dtAdviserCustomerAlerts.Columns.Add("Mobile");
                for (int i = 0; i < dsAdviserCustomerAlerts.Tables[0].Rows.Count; i++)
                {
                    drAdviserCustomerAlert = dtAdviserCustomerAlerts.NewRow();
                    if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerId"] != null && dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertId"]!=null)
                    {
                        drAdviserCustomerAlert["CustomerId"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerId"].ToString();
                        drAdviserCustomerAlert["AlertId"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertId"].ToString();
                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerName"] != null)
                        {
                            drAdviserCustomerAlert["CustomerName"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerName"].ToString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["CustomerName"] = "";
                        }
                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["Name"].ToString() != null)
                        {
                            drAdviserCustomerAlert["Name"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["Name"].ToString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["Name"] = "";
                        }
                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertMessage"].ToString() != null)
                        {
                            drAdviserCustomerAlert["AlertMessage"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertMessage"].ToString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["AlertMessage"] = "";
                        }
                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["TimesSMSSent"].ToString() != null && dsAdviserCustomerAlerts.Tables[0].Rows[i]["TimesSMSSent"].ToString() != "")
                        {
                            drAdviserCustomerAlert["TimesSMSSent"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["TimesSMSSent"].ToString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["TimesSMSSent"] = "";
                        }

                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["LastSMSDate"].ToString() != null && dsAdviserCustomerAlerts.Tables[0].Rows[i]["LastSMSDate"].ToString() !="")
                        {
                            DateTime lastSMSDate=DateTime.Parse(dsAdviserCustomerAlerts.Tables[0].Rows[i]["LastSMSDate"].ToString());
                            drAdviserCustomerAlert["LastSMSDate"] = lastSMSDate.ToShortDateString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["LastSMSDate"] = "";
                        }
                        if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertDate"].ToString() != null && dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertDate"].ToString() != "")
                        {
                            DateTime alertDate = DateTime.Parse(dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertDate"].ToString());
                            drAdviserCustomerAlert["AlertDate"] = alertDate.ToShortDateString();
                        }
                        else
                        {
                            drAdviserCustomerAlert["AlertDate"] = "";
                        }
                        drAdviserCustomerAlert["Mobile"] = dsAdviserCustomerAlerts.Tables[0].Rows[i]["Mobile"].ToString();
                        if (Int64.Parse(dsAdviserCustomerAlerts.Tables[0].Rows[i]["Mobile"].ToString()) != 0)
                        {
                            hdnCustomerIdWithoutMobileNumber.Value += dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerId"].ToString() + ",";
                        }
                        if (drAdviserCustomerAlert != null)
                        {
                            dtAdviserCustomerAlerts.Rows.Add(drAdviserCustomerAlert);
                        }
                        
                        
                    }                   
                    
                }
                gvCustomerSMSAlerts.DataSource = dtAdviserCustomerAlerts;
                gvCustomerSMSAlerts.DataBind();
                gvCustomerSMSAlerts.Visible = true;
                pnlCustomerSMSAlerts.Visible = true;
                //lblNoRecords.Visible = false;
                divNoRecords.Visible = false;
                btnSend.Visible = true;
                this.GetPageCount();

                TextBox txtName = GetCustNameTextBox();
                if (txtName != null)
                {
                    if (hdnNameFilter.Value != "")
                    {
                        txtName.Text = hdnNameFilter.Value.ToString();
                    }
                }
            }
            else
            {
                //lblNoRecords.Visible = true;
                divNoRecords.Visible = true;
                lblDisclaimer.Visible = false;
                gvCustomerSMSAlerts.Visible = false;
                pnlCustomerSMSAlerts.Visible = false;
                btnSend.Visible = false;
                DivPager.Visible = false;
                trPageCount.Visible = false;
            }

        }
        public void setAdviserSMSLicenseInfo()
        {
            AdvisorBo adviserBo = new AdvisorBo();
            DataSet dsAdviserSubscriptionDetails;
            int smsLicense = 0;
            int adviserId=0;
            if(Session["advisorVo"]!=null)
                adviserId=((AdvisorVo)Session["advisorVo"]).advisorId;
            dsAdviserSubscriptionDetails=adviserBo.GetAdviserSubscriptionDetails(adviserId);
            if (dsAdviserSubscriptionDetails != null)
            {
                if (dsAdviserSubscriptionDetails.Tables[0].Rows.Count > 0)
                {
                    DataRow drAdviserSubscriptionDetails = dsAdviserSubscriptionDetails.Tables[0].Rows[0];
                    smsLicense = int.Parse(drAdviserSubscriptionDetails["AS_SMSLicenece"].ToString());

                }
            }
            if (smsLicense == 0)
            {
                lblLincenceValue.Text = "No SMS Licence Left!!";
            }
            else
            {
                lblLincenceValue.Text=smsLicense.ToString();
            }
        }
        protected void gvCustomerSMSAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "0")
                {
                    ((CheckBox)e.Row.FindControl("chkCustomerSMSAlert")).Visible = false;
                    e.Row.Cells[2].Text = "";
                }
                else
                {
                    ((CheckBox)e.Row.FindControl("chkCustomerSMSAlert")).Visible = true;                    
                  
                }                

            }
           
        }
        protected void btnUpdateMobileNo_Click(object sender, EventArgs e)
        {
            //List<TextBox> txtMobileNoList=(TextBox)gvCustomerSMSAlerts.FindControl("txtMobileNo");
            hdnCustomerIdWithoutMobileNumber.Value = "";
            AlertsBo alertsBo = new AlertsBo();
            string customerId = "";            
            foreach (GridViewRow gvr in gvCustomerSMSAlerts.Rows)
            {
                TextBox txtMobileNo = (TextBox)gvr.FindControl("txtMobileNo");
                if (txtMobileNo.Text != "0" && txtMobileNo.Text != null)
                {
                    customerId = gvCustomerSMSAlerts.DataKeys[gvr.RowIndex].Value.ToString();
                    alertsBo.UpdateCustomerMobileNumber(int.Parse(customerId),Int64.Parse(txtMobileNo.Text));
                    hdnCustomerIdWithoutMobileNumber.Value += "1,";
                }
            }
            
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);
            int smsCount = 0;
            int smsLicence = 0;
            SMSVo smsVo = new SMSVo();
            List<int> alertIdList = new List<int>();
            EmailSMSBo emailSMSBo = new EmailSMSBo();
            List<SMSVo> smsVoList = new List<SMSVo>();
            AdvisorBo advisorBo = new AdvisorBo();
            AlertsBo alertsBo = new AlertsBo();
            string mobileNo;
            int adviserId = 0;
            
            dsAdviserCustomerAlerts = (DataSet)ViewState["vsDsAdviserCustomerAlert"];
            if (Session["advisorVo"] != null)
                adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            if (lblLincenceValue.Text != "No SMS Licence Left!!")
                smsLicence = int.Parse(lblLincenceValue.Text.ToString());
            int i = 0;
            if (dsAdviserCustomerAlerts != null)
            {
                foreach (GridViewRow gvRow in gvCustomerSMSAlerts.Rows)
                {
                    if (gvRow.RowType == DataControlRowType.DataRow)
                    {
                        if (((CheckBox)gvRow.FindControl("chkCustomerSMSAlert")).Checked)
                        {
                            smsVo = new SMSVo();
                            if (dsAdviserCustomerAlerts != null)
                            {
                                mobileNo = dsAdviserCustomerAlerts.Tables[0].Rows[i]["Mobile"].ToString();
                                if (mobileNo != "0")
                                {
                                    if (mobileNo.Length <= 10)
                                        smsVo.Mobile = Int64.Parse("91" + dsAdviserCustomerAlerts.Tables[0].Rows[i]["Mobile"].ToString());
                                    else
                                        smsVo.Mobile = Int64.Parse(mobileNo);
                                }
                            }

                            smsVo.Message = CreateSMSMessage(dsAdviserCustomerAlerts.Tables[0].Rows[i]["CustomerName"].ToString(), dsAdviserCustomerAlerts.Tables[0].Rows[i]["Name"].ToString(),
                                                             dsAdviserCustomerAlerts.Tables[0].Rows[i]["AEL_EventCode"].ToString(), dsAdviserCustomerAlerts.Tables[0].Rows[i]["AEN_EventMessage"].ToString(),
                                                             advisorVo.OrganizationName);
                            smsVo.CustomerId = int.Parse(gvCustomerSMSAlerts.DataKeys[gvRow.RowIndex].Values["CustomerId"].ToString());
                            smsVo.IsSent = 0;
                            if (dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertId"].ToString() != null)
                            {
                                smsVo.AlertId = int.Parse(dsAdviserCustomerAlerts.Tables[0].Rows[i]["AlertId"].ToString());
                            }
                            else
                            {
                                smsVo.AlertId = 0;
                            }
                            smsVoList.Add(smsVo);
                            alertIdList.Add(int.Parse(gvCustomerSMSAlerts.DataKeys[gvRow.RowIndex].Values["AlertId"].ToString()));
                            smsCount++;

                        }
                    }
                    i++;
                }
                if (smsCount <= smsLicence && smsCount != 0)
                {
                    smsVoList = emailSMSBo.AddToSMSQueue(smsVoList);

                    smsLicence = smsLicence - smsCount;
                    advisorBo.UpdateAdviserSMSLicence(adviserId, smsLicence);
                    advisorBo.AddToAdviserSMSLog(smsVoList, adviserId, "Alert");
                    alertsBo.UpdateAlertStatus(alertIdList, 1);
                    if (smsLicence == 0)
                    {
                        lblLincenceValue.Text = "No SMS Licence Left!!";
                    }
                    else
                    {
                        lblLincenceValue.Text = smsLicence.ToString();
                        GetAdviserCustomerAlerts(out dsAdviserCustomerAlerts);
                        SuccessMessage.Visible = true;
                    }
                    
                }
                else if (smsCount == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select an Alert!!');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough SMS Credits to process this request');", true);

                }
            }
        
        }

        protected void gvCustomerSMSAlerts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerSMSAlerts.PageIndex = e.NewPageIndex;
            gvCustomerSMSAlerts.DataBind();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow dr in gvCustomerSMSAlerts.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkCustomerSMSAlert");
                if (checkBox.Checked)
                {
                    long alertId = Convert.ToInt64(gvCustomerSMSAlerts.DataKeys[dr.RowIndex].Values[1]);
                    alertsBo.DeleteAlertNotification(alertId);
                }
            }
            GetAdviserCustomerAlerts(out dsAdviserCustomerAlerts);
        }

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                //if (Session["Customer"].ToString() == "Customer")
                //{
                this.GetAdviserCustomerAlerts(out dsAdviserCustomerAlerts);
                //}
                //else
                //{
                //    this.BindCustomer(mypager.CurrentPage);
                //}
            }
        }

        //returns the text in the customer name filter textbox in the grid
        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomerSMSAlerts.HeaderRow != null)
            {
                if ((TextBox)gvCustomerSMSAlerts.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomerSMSAlerts.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private string CreateSMSMessage(string customerName,string schemeName, string eventCode, string message, string adviserName)
        {
            string smsMessage = null;
            string identifierType = null;
            if (eventCode == "SIP" || eventCode == "SWP" || eventCode == "MF Dividend" || eventCode == "ELSS Maturity" || eventCode == "MF Absolute Stop Loss" || eventCode == "MF Absolute Profit booking")
                identifierType = "Scheme Name : ";
            else if (eventCode == "Insurance Premium payment")
                identifierType = "Insurance Plan : ";
            else if (eventCode == "FD/Recurring Deposit" || eventCode == "Bank FD Maturity")
                identifierType = "Account : ";
            else if (eventCode == "Equity Absolute stop Loss" || eventCode == "Equity Absolute Profit booking")
                identifierType = "Scrip Name : ";
            else
                identifierType = "";
            
            smsMessage = identifierType + schemeName + "\n" + "Message : " + message + "\n - " + adviserName;

            smsMessage = smsMessage.Replace("%", "%25");
            smsMessage= smsMessage.Replace("\n", "%0A");

            return smsMessage;
        }

    }
}