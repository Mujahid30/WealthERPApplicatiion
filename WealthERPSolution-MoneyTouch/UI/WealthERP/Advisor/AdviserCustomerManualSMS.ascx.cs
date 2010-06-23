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
    public partial class AdviserCustomerManualSMS : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setAdviserSMSLicenseInfo();
                GetAdviserCustomerForSMS();
            }
        }
        public void setAdviserSMSLicenseInfo()
        {
            AdvisorBo adviserBo = new AdvisorBo();
            DataSet dsAdviserSubscriptionDetails;
            int smsLicense = 0;
            int adviserId = 0;
            if (Session["advisorVo"] != null)
                adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            dsAdviserSubscriptionDetails = adviserBo.GetAdviserSubscriptionDetails(adviserId);
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
                lblLincenceValue.Text = smsLicense.ToString();
            }
        }
        public void GetAdviserCustomerForSMS()
        {
            int adviserId = 0;
            string namesearch = "";
            List<CustomerVo> customerList=new List<CustomerVo>();
            AdvisorBo adviserBo=new AdvisorBo();
            DataTable dtAdviserCustomerAlerts=new DataTable();
            dtAdviserCustomerAlerts.Columns.Add("CustomerId");
            dtAdviserCustomerAlerts.Columns.Add("CustomerName");
            dtAdviserCustomerAlerts.Columns.Add("Mobile");
            if (gvCustomerSMSAlerts.HeaderRow != null)
            {
                namesearch = ((TextBox)gvCustomerSMSAlerts.HeaderRow.FindControl("txtCustomerSearch")).Text;
            }
            DataRow dr;
            if (Session["advisorVo"] != null)
                adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            
            customerList=adviserBo.GetAdviserCustomersForSMS(adviserId,namesearch);
            if(customerList!=null)
            {
                for(int i=0;i<customerList.Count;i++)
                {
                    dr = dtAdviserCustomerAlerts.NewRow();
                    dr[0] = customerList[i].CustomerId;
                    dr[1] = customerList[i].FirstName;
                    dr[2] = customerList[i].Mobile1;

                    dtAdviserCustomerAlerts.Rows.Add(dr);
                }
                if (dtAdviserCustomerAlerts.Rows.Count == 0)
                {
                    dr = dtAdviserCustomerAlerts.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "";

                    dtAdviserCustomerAlerts.Rows.Add(dr);
                }

                gvCustomerSMSAlerts.DataSource = dtAdviserCustomerAlerts;
                gvCustomerSMSAlerts.DataBind();
                gvCustomerSMSAlerts.Visible = true;
                pnlCustomerSMSAlerts.Visible = true;
                lblNoRecords.Visible = false;
            }
            else
            {
                lblNoRecords.Visible = true;
                gvCustomerSMSAlerts.Visible = false;
            }
            BindGridSearchBoxes(namesearch);
        }
        private void BindGridSearchBoxes(string customerSearch)
        {
            
            if ((TextBox)gvCustomerSMSAlerts.HeaderRow.FindControl("txtCustomerSearch") != null)
            {
                ((TextBox)gvCustomerSMSAlerts.HeaderRow.FindControl("txtCustomerSearch")).Text = customerSearch;
            }

            

        }
        protected void gvCustomerSMSAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "0" || e.Row.Cells[2].Text == "" ||  e.Row.Cells[2].Text =="&nbsp;")
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetAdviserCustomerForSMS();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int smsCount = 0;
            int smsLicence = 0;
            SMSVo smsVo = new SMSVo();
            
            EmailSMSBo emailSMSBo = new EmailSMSBo();
            List<SMSVo> smsVoList = new List<SMSVo>();
            AdvisorBo advisorBo = new AdvisorBo();
           
            int adviserId = 0;
            if (Session["advisorVo"] != null)
                adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            if (lblLincenceValue.Text != "No SMS Licence Left!!")
                smsLicence = int.Parse(lblLincenceValue.Text.ToString());

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
                        smsVo.Message = txtMessage.Text.ToString();
                        smsVo.CustomerId = int.Parse(gvCustomerSMSAlerts.DataKeys[gvRow.RowIndex].Values["CustomerId"].ToString());
                        smsVo.IsSent = 0;
                        smsVoList.Add(smsVo);
                        
                        smsCount++;

                    }
                }
            }
            if (smsCount <= smsLicence && smsCount != 0)
            {
                smsVoList = emailSMSBo.AddToSMSQueue(smsVoList);

                smsLicence = smsLicence - smsCount;
                advisorBo.UpdateAdviserSMSLicence(adviserId, smsLicence);
                advisorBo.AddToAdviserSMSLog(smsVoList, adviserId, "Manual Message");

                if (smsLicence == 0)
                {
                    lblLincenceValue.Text = "No SMS Licence Left!!";
                }
                else
                {
                    lblLincenceValue.Text = smsLicence.ToString();
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('SMS has been Sent to the Selected Customers!!');", true);

            }
            else if (smsCount == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a Customer!!');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough SMS Credits to process this request');", true);

            }
        }
    }
}