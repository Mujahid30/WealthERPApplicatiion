using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;
using System.Data;
using Telerik.Web.UI;
using BoOnlineOrderManagement;
using System.Configuration;
using System.IO;
using BoCommon;
using BoProductMaster;
using System.Web.UI.HtmlControls;

namespace WealthERP
{
    public partial class InvestorOnline : System.Web.UI.Page
    {
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        OnlineCommonBackOfficeBo OnlineCommonBackOfficeBo = new OnlineCommonBackOfficeBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["setupId"]))
                {
                    DataSet ds = onlineOrderBackOfficeBo.GetNotificationParameterwithEmailSMSDetails(Convert.ToInt32(Request.QueryString["setupId"].Trim()));
                    if (Request.QueryString["FormatType"] == "Email")
                    {
                        divEmail.Visible = true;
                        divSMS.Visible = false;
                        hdnCurrentTextEditor.Value = "txtEmailSubject";
                    }
                    else if (Request.QueryString["FormatType"] == "SMS")
                    {
                        divEmail.Visible = false;
                        divSMS.Visible = true;
                        hdnCurrentTextEditor.Value = "txtSMSBody";
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Cache["NotificationParameter" + userVo.UserId.ToString()] == null)
                        {
                            Cache.Insert("NotificationParameter" + userVo.UserId.ToString(), ds.Tables[0]);
                        }
                        else
                        {
                            Cache.Remove("NotificationParameter" + userVo.UserId.ToString());
                            Cache.Insert("NotificationParameter" + userVo.UserId.ToString(), ds.Tables[0]);
                        }
                        //BindLiteral(ds.Tables[0]);

                    }
                    else
                    {
                        Cache.Remove("NotificationParameter" + userVo.UserId.ToString());
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (Cache["EmailDetails" + userVo.UserId.ToString()] == null)
                        {
                            Cache.Insert("EmailDetails" + userVo.UserId.ToString(), ds.Tables[1]);
                        }
                        else
                        {
                            Cache.Remove("EmailDetails" + userVo.UserId.ToString());
                            Cache.Insert("EmailDetails" + userVo.UserId.ToString(), ds.Tables[1]);
                        }

                        txtEmailSubject.Text = ds.Tables[1].Rows[0]["CTNEF_EmailSubjectFormat"].ToString();
                        txtEmailBody.Text = ds.Tables[1].Rows[0]["CTNEF_EmailBodyFormat"].ToString();
                        hdnEmailBody.Value = ds.Tables[1].Rows[0]["CTNEF_EmailBodyFormat"].ToString();
                        hdnEmailSubject.Value = ds.Tables[1].Rows[0]["CTNEF_EmailSubjectFormat"].ToString();
                    }
                    else
                    {
                        Cache.Remove("EmailDetails" + userVo.UserId.ToString());
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        if (Cache["SMSDetails" + userVo.UserId.ToString()] == null)
                        {
                            Cache.Insert("SMSDetails" + userVo.UserId.ToString(), ds.Tables[2]);
                        }
                        else
                        {
                            Cache.Remove("SMSDetails" + userVo.UserId.ToString());
                            Cache.Insert("SMSDetails" + userVo.UserId.ToString(), ds.Tables[2]);
                        }
                        txtSMSBody.Text = ds.Tables[2].Rows[0]["CTNSF_SMSBodyFormat"].ToString();
                        hdnSMSBody.Value = ds.Tables[2].Rows[0]["CTNSF_SMSBodyFormat"].ToString();
                    }
                    else
                    {
                        Cache.Remove("SMSDetails" + userVo.UserId.ToString());
                    }

                }
            }
           
            BindLiteral();
        }
        protected void BindLiteral()
        {
            PlaceHolder1.Controls.Clear();
            DataTable parameter = (DataTable)Cache["NotificationParameter" + userVo.UserId.ToString()];
            PlaceHolder1.Controls.Add(new LiteralControl(@"<table ID=divparameter width=100" + "%"));
            if (parameter.Rows.Count > 0)
            {
                foreach (DataRow dr in parameter.Rows)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<tr class=" + "trparameter>"));
                    PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"15\">"));
                    LinkButton lbl = new LinkButton();
                    lbl.ID = dr["NP_ParameterCode"].ToString();
                    lbl.Text = dr["NP_Parameter"].ToString();
                    lbl.CssClass = "lblparameter";
                    lbl.OnClientClick = "SetParameter('" + dr["NP_ParameterCode"].ToString() + "')";

                    PlaceHolder1.Controls.Add(lbl);
                    PlaceHolder1.Controls.Add(new LiteralControl("</td></tr>"));

                }
            }
            PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
        }
        protected string SampleText(string formatText)
        {
            DataTable dt;
            dt = (DataTable)Cache["NotificationParameter" + userVo.UserId.ToString()];
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                 formatText=   formatText.Replace(dr["NP_ParameterCode"].ToString(), dr["NP_SampleText"].ToString());
                    
                }
            }
            return formatText;
        }
        protected string GetParameters(string formatText)
        {
            DataTable dt;
            dt = (DataTable)Cache["NotificationParameter" + userVo.UserId.ToString()];
            string parameterCodes = string.Empty;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (formatText.Contains(dr["NP_ParameterCode"].ToString()))
                    {
                        parameterCodes = parameterCodes + dr["NP_ParameterCode"].ToString() + ",";
                    }
                }
            }
            return parameterCodes;

        }
        protected void previewButton_Onclick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.ID == "btnEmailSubjectPreview")
            {
                lblSampleEmailSubject.Text = SampleText(txtEmailSubject.Text);
            }
            else if (btn.ID == "btnEmailBodyPreview")
            {
                lblSampleEmailBody.Text = SampleText(txtEmailBody.Text);
            }
            else if (btn.ID == "btnSMSBodyPreview")
            {
                lblSampleSMSBody.Text = SampleText(txtSMSBody.Text);
            }
        }
        protected void SaveButton_Onclick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataTable dtEmail;
            DataTable dtSms;
            dtEmail = (DataTable)Cache["EmailDetails" + userVo.UserId.ToString()];
            dtSms = (DataTable)Cache["SMSDetails" + userVo.UserId.ToString()];
            int emailId = 0;
            int smsId = 0;
            int notificationId = Convert.ToInt32(Request.QueryString["setupId"]);
            if (dtEmail != null && dtEmail.Rows.Count > 0)
            {

                emailId = Convert.ToInt32(dtEmail.Rows[0]["CTNEF_Id"].ToString());
            }
            if (dtSms!=null && dtSms.Rows.Count > 0)
            {

                smsId = Convert.ToInt32(dtSms.Rows[0]["CTNSF_Id"].ToString());
            }
            if (btn.ID == "btnEmailSubjectSave")
            {
                onlineOrderBackOfficeBo.InsertUpdateNotificationFormat(userVo.UserId, notificationId, "EmailSubject", GetParameters(txtEmailSubject.Text), txtEmailSubject.Text, emailId);
            }
            else if (btn.ID == "btnEmailBodySave")
            {
                onlineOrderBackOfficeBo.InsertUpdateNotificationFormat(userVo.UserId, notificationId, "EmailBody", GetParameters(txtEmailBody.Text), txtEmailBody.Text, emailId);
            }
            else if (btn.ID == "btnSMSBodySave")
            {
                onlineOrderBackOfficeBo.InsertUpdateNotificationFormat(userVo.UserId, notificationId, "SMS", GetParameters(txtSMSBody.Text), txtSMSBody.Text, smsId);

            }

        }
        protected void RadioBtn_OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            if (rbtn.ID == "Subject")
            {
                divEmailBody.Visible = false;
                divEmailSubject.Visible = true;
                hdnCurrentTextEditor.Value = "txtEmailSubject";
            }
            else if (rbtn.ID == "Body")
            {
                divEmailBody.Visible = true;
                divEmailSubject.Visible = false;
                hdnCurrentTextEditor.Value = "txtEmailBody";
            }

        }
    }
}
