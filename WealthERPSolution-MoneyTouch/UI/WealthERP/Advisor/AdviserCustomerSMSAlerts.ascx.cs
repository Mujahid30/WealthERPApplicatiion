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

namespace WealthERP.Advisor
{
    public partial class AdviserCustomerSMSAlerts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setAdviserSMSLicenseInfo();
            GetAdviserCustomerAlerts();
        }
        public void GetAdviserCustomerAlerts()
        {
            int adviserId = 0;
            DataSet dsAdviserCustomerAlerts;
            DataTable dtAdviserCustomerAlerts;
            
            if(Session["advisorVo"]!=null)
                adviserId=((AdvisorVo)Session["advisorVo"]).advisorId;
            AlertsBo alertsBo = new AlertsBo();
            dsAdviserCustomerAlerts = alertsBo.GetAdviserCustomerSMSAlerts(adviserId);
            if (dsAdviserCustomerAlerts != null)
            {
                dtAdviserCustomerAlerts = dsAdviserCustomerAlerts.Tables[0];
                if (dtAdviserCustomerAlerts.Rows.Count != 0)
                {
                    gvCustomerSMSAlerts.DataSource = dtAdviserCustomerAlerts;
                    gvCustomerSMSAlerts.DataBind();
                    gvCustomerSMSAlerts.Visible = true;
                    pnlCustomerSMSAlerts.Visible = true;
                    lblNoRecords.Visible = false;
                    btnSend.Visible = true;
                }
                else
                {
                    lblNoRecords.Visible = true;
                    pnlCustomerSMSAlerts.Visible = true;
                    gvCustomerSMSAlerts.Visible = false;
                    btnSend.Visible = false;
                   
                }
            }
            else
            {
                lblNoRecords.Visible = true;
                gvCustomerSMSAlerts.Visible = false;
                pnlCustomerSMSAlerts.Visible = true;
                btnSend.Visible = false;
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
            if (dsAdviserSubscriptionDetails != null && dsAdviserSubscriptionDetails.Tables[0].Rows.Count!=0)
            {
                DataRow drAdviserSubscriptionDetails = dsAdviserSubscriptionDetails.Tables[0].Rows[0];
                smsLicense = int.Parse(drAdviserSubscriptionDetails["AS_SMSLicenece"].ToString());
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int smsCount=0;
            int smsLicence=0;
            SMSVo smsVo = new SMSVo();
            List<int> alertIdList = new List<int>();
            EmailSMSBo emailSMSBo = new EmailSMSBo();
            List<SMSVo> smsVoList = new List<SMSVo>();
            AdvisorBo advisorBo = new AdvisorBo();
            AlertsBo alertsBo=new AlertsBo();
            int adviserId=0;
            if(Session["advisorVo"]!=null)
                adviserId=((AdvisorVo)Session["advisorVo"]).advisorId;
            if(lblLincenceValue.Text!="No SMS Licence Left!!")
                smsLicence=int.Parse(lblLincenceValue.Text.ToString());

            foreach (GridViewRow gvRow in gvCustomerSMSAlerts.Rows)
            {
                if (gvRow.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)gvRow.FindControl("chkCustomerSMSAlert")).Checked)
                    {
                        smsVo = new SMSVo();
                        if (gvRow.Cells[2].Text.Trim().Length <= 10)
                            smsVo.Mobile = Int64.Parse("91" + gvRow.Cells[2].Text.Trim());
                        else
                            smsVo.Mobile = int.Parse(gvRow.Cells[2].Text.Trim());
                        smsVo.Message = gvRow.Cells[3].Text.ToString();
                        smsVo.CustomerId = int.Parse(gvCustomerSMSAlerts.DataKeys[gvRow.RowIndex].Values["CustomerId"].ToString());
                        smsVo.IsSent = 0;
                        smsVoList.Add(smsVo);
                        alertIdList.Add(int.Parse(gvCustomerSMSAlerts.DataKeys[gvRow.RowIndex].Values["AlertId"].ToString()));
                        smsCount++;

                    }
                }
            }
            if (smsCount <= smsLicence && smsCount!=0)
            {
                smsVoList = emailSMSBo.AddToSMSQueue(smsVoList);
                
                smsLicence=smsLicence-smsCount;
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
                }
                GetAdviserCustomerAlerts();
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
}