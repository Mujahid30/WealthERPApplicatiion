using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using PCGMailLib;
using System.Net.Mail;
using System.Configuration;
using BoCommon;
using System.Data;
using System.IO;


namespace WealthERP.Advisor
{
    public partial class AdviserStaffSMTP : System.Web.UI.UserControl
    {


        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo adviserbo = new AdvisorBo();
        RMVo advrm = new RMVo();

        AdviserStaffSMTPVo adviserstaffsmtpvo = new AdviserStaffSMTPVo();
        AdviserStaffSMTPBo advstaffsmtpbo = new AdviserStaffSMTPBo();
        AdviserPreferenceBo adviserPreferenceBo = new AdviserPreferenceBo();
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];

            AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();
            advrm = adviserstaffbo.GetAdvisorStaff(userVo.UserId);
            adviserstaffsmtpvo.RMId = advrm.RMId;
            BindSMSProvider();
            BindAPIProvider();
            if (!IsPostBack)
            {
                //if (trInsertMessage.Visible == true)
                //    trInsertMessage.Visible = false;
                adviserstaffsmtpvo = advstaffsmtpbo.GetSMTPCredentials(advrm.RMId);
                txtEmail.Text = adviserstaffsmtpvo.Email;
                if (!String.IsNullOrEmpty(adviserstaffsmtpvo.Password))
                    txtPassword.Attributes.Add("value", Encryption.Decrypt(adviserstaffsmtpvo.Password));
                txtSMTPHost.Text = adviserstaffsmtpvo.HostServer;
                txtSMTPPort.Text = adviserstaffsmtpvo.Port;
                txtSenderEmailAlias.Text = adviserstaffsmtpvo.SenderEmailAlias;
                chkAthenticationRequired.Checked = Convert.ToBoolean(adviserstaffsmtpvo.IsAuthenticationRequired);
                ddlSMSProvider.SelectedValue = adviserstaffsmtpvo.SmsProviderId.ToString();
                txtUserName.Text = adviserstaffsmtpvo.SmsUserName;
                txtPassword1.Text = adviserstaffsmtpvo.Apipassword;
                txtSenderID.Text = adviserstaffsmtpvo.SmsSenderId;
                if (!String.IsNullOrEmpty(adviserstaffsmtpvo.Smspassword))
                    txtPwd.Attributes.Add("value", adviserstaffsmtpvo.Smspassword);
                txtsmsCredit.Text = adviserstaffsmtpvo.SmsInitialcredit.ToString();
                ddlAPIProvider.SelectedValue = adviserstaffsmtpvo.ApiProviderId.ToString();
                  txtUname.Text = adviserstaffsmtpvo.ApiUserName;
                txtMemberId.Text = Convert.ToString(adviserstaffsmtpvo.ApiMemberId);
                if (!String.IsNullOrEmpty(adviserstaffsmtpvo.Apipassword))
                    txtPassword1.Attributes.Add("value", adviserstaffsmtpvo.Apipassword);
                SetAdviserPreference();
              

            }

            string a = txtPassword.Text;
        }

        private void BindSMSProvider()
        {
            try
            {
                DataSet dsSMSProvider;
                dsSMSProvider = advstaffsmtpbo.GetSMSProvider();
                DataTable dtSMSProvider = dsSMSProvider.Tables[0];
                if (dtSMSProvider != null)
                {
                    ddlSMSProvider.DataSource = dtSMSProvider;
                    ddlSMSProvider.DataValueField = dtSMSProvider.Columns["WERPSMSPM_ID"].ToString();
                    ddlSMSProvider.DataTextField = dtSMSProvider.Columns["WERPSMSPM_Name"].ToString();
                    ddlSMSProvider.DataBind();
                }
                ddlSMSProvider.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindAPIProvider()
        {
            try
            {
                DataSet dsAPIProvider;
                dsAPIProvider = advstaffsmtpbo.GetAPIProvider();
                DataTable dtAPIProvider = dsAPIProvider.Tables[0];
                if (dtAPIProvider != null)
                {
                    ddlAPIProvider.DataSource = dtAPIProvider;
                    ddlAPIProvider.DataValueField = dtAPIProvider.Columns["WEAM_ID"].ToString();
                    ddlAPIProvider.DataTextField = dtAPIProvider.Columns["WEAM_Name"].ToString();
                    ddlAPIProvider.DataBind();
                }
                ddlAPIProvider.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            adviserstaffsmtpvo.Email = txtEmail.Text.Trim();
            adviserstaffsmtpvo.HostServer = txtSMTPHost.Text.Trim();
            if (chkAthenticationRequired.Checked)
            {
                adviserstaffsmtpvo.IsAuthenticationRequired = 1;
            }
            else
            {
                adviserstaffsmtpvo.IsAuthenticationRequired = 0;
            }
            adviserstaffsmtpvo.CreatedBy = userVo.UserId;
            adviserstaffsmtpvo.ModifiedBy = userVo.UserId;
            //if (!String.IsNullOrEmpty(txtPassword.Text))
            adviserstaffsmtpvo.Password = Encryption.Encrypt(txtPassword.Text.Trim());
            adviserstaffsmtpvo.Port = txtSMTPPort.Text.Trim();
            adviserstaffsmtpvo.SenderEmailAlias = txtSenderEmailAlias.Text;
            txtPassword.Attributes.Add("value", txtPassword.Text.Trim());



            AdviserStaffSMTPBo advstaffsmtpbo = new AdviserStaffSMTPBo();
            try
            {
                result = advstaffsmtpbo.InsertAdviserStaffSMTP(adviserstaffsmtpvo);
            }
            catch (Exception ex)
            {
                lblInsertMessage.Text = "There was an error in inserting the values";
            }

            if (result)
            {
                trInsertMessage.Visible = true;
                lblInsertMessage.Text = "Values inserted Successfully";
            }
            else
            {
                lblInsertMessage.Text = "There was an error in inserting the values";
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {



            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();

            //Get the test EmailId(s) from web.config. 
            //If no email Id is specified in web.config then send it to a sample email Id.
            string toMailIdString = ConfigurationManager.AppSettings["TestEmailId"].ToString();
            if (toMailIdString == null || toMailIdString == string.Empty)
                toMailIdString = "rthomas@principalconsulting.net";
            string[] toMailIds = toMailIdString.Split(',');
            foreach (string mailId in toMailIds)
            {
                email.To.Add(mailId);
            }

            emailer.smtpServer = txtSMTPHost.Text.Trim();
            emailer.isDefaultCredentials = !chkAthenticationRequired.Checked;
            if (txtSMTPPort.Text != string.Empty)
                emailer.smtpPort = Convert.ToInt32(txtSMTPPort.Text.Trim());
            emailer.smtpUserName = txtEmail.Text.Trim();

            //if (txtPassword.Text.Trim() != string.Empty)
            emailer.smtpPassword = txtPassword.Text.Trim();



            if (txtEmail.Text.Trim() != string.Empty && txtEmail.Text.Contains("@"))
                email.From = new MailAddress(txtEmail.Text.Trim(), "Test Email from WERP");
            else
                email.From = new MailAddress("admin@wealthERP.com", "Test Email from WERP");

            email.Body = "Test Email Content :" + txtEmail.Text;
            email.Subject = "Test Email subject";

            txtPassword.Attributes.Add("value", txtPassword.Text);
            string statusMessage = string.Empty;
            bool isMailSent = emailer.SendMail(email, out statusMessage);
            if (isMailSent)
            {
                lblInsertMessage.Text = "SMTP Credentials are valid.";
            }
            else
            {
                lblInsertMessage.Text = "Not able to send mail using the SMTP credentials";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = false;
            adviserstaffsmtpvo.SmsProviderId = int.Parse(ddlSMSProvider.SelectedValue);
            adviserstaffsmtpvo.AdvisorId = adviserVo.advisorId;
            adviserstaffsmtpvo.SmsUserName = txtUserName.Text;
            adviserstaffsmtpvo.Smspassword = txtPwd.Text;
            adviserstaffsmtpvo.SmsInitialcredit = int.Parse(txtsmsCredit.Text);
            adviserstaffsmtpvo.SmsCreditLeft = int.Parse(txtsmsCredit.Text);
            adviserstaffsmtpvo.SmsCreatedBy = userVo.UserId;
            adviserstaffsmtpvo.SmsModifiedBy = userVo.UserId;
            adviserstaffsmtpvo.SmsSenderId = txtSenderID.Text;
            txtPwd.Attributes.Add("value", txtPwd.Text.Trim());

            result = advstaffsmtpbo.CreateSMSProviderDetails(adviserstaffsmtpvo);

            if (result)
            {
                trBtnSaveMsg.Visible = true;
                lblbtnSaveMsg.Text = "Values inserted Successfully";
            }
            else
            {
                lblbtnSaveMsg.Text = "There was an error in inserting the values";
            }

        }

        protected void btnSendLoginWidGet_Click(object sender, EventArgs e)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
            AdviserStaffSMTPVo adviserStaffSMTPVo = new AdviserStaffSMTPVo();
            string logoPath = string.Empty;
            bool isMailSent=false;

            email.GetAdviserLoginWidgetMail(Encryption.Encrypt(adviserVo.advisorId.ToString()), adviserVo.OrganizationName.Trim());
            email.To.Add(txtLoginWidGetEmail.Text.Trim());

            adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(1000);

            if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
            {
                emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);
                if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                    emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                emailer.smtpUserName = adviserStaffSMTPVo.Email;

                email.Body = email.Body.Replace("[ORGANIZATION]", "WealthERP Team");
                if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                {
                    email.Body = email.Body.Replace("[WEBSITE]", "https://app.wealtherp.com/");
                }

                email.Body = email.Body.Replace("[CONTACTPERSON]", "Mr Vijay Shenoy");

                email.Body = email.Body.Replace("[DESIGNATION]", "Customer Care");

                email.Body = email.Body.Replace("[PHONE]", "080 - 32429514");

                email.Body = email.Body.Replace("[EMAIL]", "custcare@ampsys.in");


                email.Body = email.Body.Replace("[LOGO]", "<img src='cid:HDIImage' alt='Logo'>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + email.Body + "</p></body></html>", null, "text/html");
                //Add image to HTML version

                logoPath = "~/Images/WealthERP.jpf";
                if (!File.Exists(Server.MapPath(logoPath)))
                    logoPath = "~/Images/spacer.png";
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);
                //Send message
                //System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();

                //Assign SMTP Credentials if configured.
                if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
                {
                    emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                    if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                        emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                    emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                    emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                    emailer.smtpUserName = adviserStaffSMTPVo.Email;

                    if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                    {
                        if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                        {
                            email.From = new MailAddress(emailer.smtpUserName, "WealthERP Team");
                        }

                    }
                }

                 isMailSent = emailer.SendMail(email);

                 if (isMailSent)
                 {
                     trSuccessMsg.Visible = true;
                     //tblErrorMassage.Visible = false;                     
                     divSuccessMsg.InnerText = "Login Widget send to this email Id";
                    
                 }
                 else
                 {
                     trSuccessMsg.Visible = false;
                     //tblErrorMassage.Visible = true;                    
                     divSuccessMsg.InnerText = "An error occurred while sending Login Widget";
                    

                 }

            }
        }

        protected void rbLoginWidGetYes_CheckedChanged(object sender, EventArgs e)
        {
            txtWebSiteDomainName.ReadOnly = false;
            txtLogOutPageUrl.ReadOnly = false;
        }

        protected void rbLoginWidGetNo_CheckedChanged(object sender, EventArgs e)
        {
            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(1000);
            txtLogOutPageUrl.Text = advisorPreferenceVo.LoginWidgetLogOutPageURL;
            txtWebSiteDomainName.Text = advisorPreferenceVo.WebSiteDomainName;
            txtWebSiteDomainName.ReadOnly = true;
            txtLogOutPageUrl.ReadOnly = true;
        }

        protected void SetAdviserPreference()
        {
            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(adviserVo.advisorId);
            if (advisorPreferenceVo != null)
            {
                txtWebSiteDomainName.Text = advisorPreferenceVo.WebSiteDomainName;
                txtLogOutPageUrl.Text = advisorPreferenceVo.LoginWidgetLogOutPageURL;
                txtBrowserTitleBarName.Text = advisorPreferenceVo.BrowserTitleBarName;
                txtGridPageSize.Text = advisorPreferenceVo.GridPageSize.ToString();
               
                if (advisorPreferenceVo.IsLoginWidgetEnable)
                {
                    rbLoginWidGetYes.Checked = true;
                    txtLoginWidGetEmail.Text = adviserVo.Email.Trim();
                    txtLoginWidGetEmail.ReadOnly = false;
                }
                else
                {
                    rbLoginWidGetNo.Checked = true;
                    txtWebSiteDomainName.ReadOnly = true;
                    txtLogOutPageUrl.ReadOnly = true;
                    txtLoginWidGetEmail.ReadOnly = true;
                }
            }
        }

        protected void btnSubmitPreference_Click(object sender, EventArgs e)
        {
            string strCommand = "cmdPreference";
            bool isSuccess = false;
            if (rbLoginWidGetYes.Checked)
                advisorPreferenceVo.IsLoginWidgetEnable = true;
            else
                advisorPreferenceVo.IsLoginWidgetEnable = false;
            advisorPreferenceVo.WebSiteDomainName= txtWebSiteDomainName.Text;
            advisorPreferenceVo.LoginWidgetLogOutPageURL=txtLogOutPageUrl.Text;
            advisorPreferenceVo.BrowserTitleBarName = txtBrowserTitleBarName.Text;
            advisorPreferenceVo.GridPageSize =  int.Parse(txtGridPageSize.Text);
            isSuccess = adviserPreferenceBo.AdviserPreferenceSetUp(advisorPreferenceVo, adviserVo.advisorId, userVo.UserId,strCommand); 
            if (isSuccess)
            {
                trSuccessMsg.Visible = true;
                divSuccessMsg.InnerText = "Preference Updated Successfully";
            }
            SetAdviserPreference();
            Session["AdvisorPreferenceVo"] = advisorPreferenceVo;


        }
        protected void SetAdviserGridPageSize()
        {           
        }
        protected void btnSubmitPageSize_Click(object sender, EventArgs e)
        {
            string strCommand = "cmdGridSize";
            bool isSuccess = false;
            if(!string.IsNullOrEmpty(txtGridPageSize.Text))
            advisorPreferenceVo.GridPageSize =int.Parse(txtGridPageSize.Text);
            isSuccess = adviserPreferenceBo.AdviserPreferenceSetUp(advisorPreferenceVo, adviserVo.advisorId, userVo.UserId,strCommand);
            if (isSuccess)
            {
                trMsg.Visible = true;
                divMsgSuccess.InnerText = "Preference Updated Successfully";
            }
            SetAdviserPreference();
            Session["AdvisorPreferenceVo"] = advisorPreferenceVo;
        
        }


        protected void btnSubmit1_Click(object sender, EventArgs e)
        {

           bool result = false;
           string message;
            adviserstaffsmtpvo.ApiProviderId = int.Parse(ddlAPIProvider.SelectedValue);
            adviserstaffsmtpvo.AdvisorId = adviserVo.advisorId;
            adviserstaffsmtpvo.ApiUserName = txtUname.Text;
            adviserstaffsmtpvo.Apipassword = txtPassword1.Text;
            adviserstaffsmtpvo.ConfirmPassword = txtConfirmPassword.Text;
            adviserstaffsmtpvo.NewPassword = txtNewPassword.Text;
            adviserstaffsmtpvo.ApiCreatedBy = userVo.UserId;
            adviserstaffsmtpvo.ApiModifiedBy = userVo.UserId;
            adviserstaffsmtpvo.ApiMemberId = txtMemberId.Text; 
            txtPwd.Attributes.Add("value", txtPwd.Text.Trim());

            result = advstaffsmtpbo.CreateAPIProviderDetails(adviserstaffsmtpvo, out message);

            if (result)
            {
                trBtnSaveMsg.Visible = true;
                //lblbtnSaveMsg.Text = message;
                Response.Write("<script>alert('" + (message) + "')</script>");
            }
            else
            {
               // lblbtnSaveMsg.Text = message; 
                Response.Write("<script>alert('" + (message) + "')</script>");

            }

        }
        
        
    }
}
