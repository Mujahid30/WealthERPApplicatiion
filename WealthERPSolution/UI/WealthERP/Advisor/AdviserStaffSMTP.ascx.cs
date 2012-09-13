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


        UserVo uservo = new UserVo();
        AdvisorVo advisevo = new AdvisorVo();
        AdvisorBo adviserbo = new AdvisorBo();
        RMVo advrm = new RMVo();

        AdviserStaffSMTPVo adviserstaffsmtpvo = new AdviserStaffSMTPVo();
        AdviserStaffSMTPBo advstaffsmtpbo = new AdviserStaffSMTPBo();


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            uservo = (UserVo)Session["userVo"];
            advisevo = (AdvisorVo)Session["advisorVo"];

            AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();
            advrm = adviserstaffbo.GetAdvisorStaff(uservo.UserId);
            adviserstaffsmtpvo.RMId = advrm.RMId;
            BindSMSProvider();
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
                // txtPwd.Text = adviserstaffsmtpvo.Smspassword;
                if (!String.IsNullOrEmpty(adviserstaffsmtpvo.Smspassword))
                    txtPwd.Attributes.Add("value", adviserstaffsmtpvo.Smspassword);
                txtsmsCredit.Text = adviserstaffsmtpvo.SmsInitialcredit.ToString();

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
            adviserstaffsmtpvo.CreatedBy = uservo.UserId;
            adviserstaffsmtpvo.ModifiedBy = uservo.UserId;
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
            adviserstaffsmtpvo.AdvisorId = advisevo.advisorId;
            adviserstaffsmtpvo.SmsUserName = txtUserName.Text;
            adviserstaffsmtpvo.Smspassword = txtPwd.Text;
            adviserstaffsmtpvo.SmsInitialcredit = int.Parse(txtsmsCredit.Text);
            adviserstaffsmtpvo.SmsCreditLeft = int.Parse(txtsmsCredit.Text);
            adviserstaffsmtpvo.SmsCreatedBy = uservo.UserId;
            adviserstaffsmtpvo.SmsModifiedBy = uservo.UserId;
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

            email.GetAdviserLoginWidgetMail(Encryption.Encrypt(advisevo.advisorId.ToString()), advisevo.OrganizationName.Trim());
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
                     tblMessage.Visible = true;
                     tblErrorMassage.Visible = false;                     
                     SuccessMsg.InnerText = "Login Widget send to this email Id";
                    
                 }
                 else
                 {
                     tblMessage.Visible = false;
                     tblErrorMassage.Visible = true;                    
                     ErrorMessage.InnerText = "An error occurred while sending Login Widget";
                    

                 }

            }
        }


    }
}
