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

namespace WealthERP.Advisor
{
    public partial class AdviserStaffSMTP : System.Web.UI.UserControl
    {


        UserVo uservo = new UserVo();
        AdvisorVo advisevo = new AdvisorVo();
        AdvisorBo adviserbo = new AdvisorBo();
        RMVo advrm = new RMVo();

        AdviserStaffSMTPVo adviserstaffsmtpvo = new AdviserStaffSMTPVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            uservo = (UserVo)Session["userVo"];
            advisevo = (AdvisorVo)Session["advisorVo"];

            AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();
            advrm = adviserstaffbo.GetAdvisorStaff(uservo.UserId);
            adviserstaffsmtpvo.RMId = advrm.RMId;

            if (!IsPostBack)
            {
                //if (trInsertMessage.Visible == true)
                //    trInsertMessage.Visible = false;

                AdviserStaffSMTPBo advstaffsmtpbo = new AdviserStaffSMTPBo();
                adviserstaffsmtpvo = advstaffsmtpbo.GetSMTPCredentials(advrm.RMId);
                txtEmail.Text = adviserstaffsmtpvo.Email;
                if (!String.IsNullOrEmpty(adviserstaffsmtpvo.Password))
                    txtPassword.Attributes.Add("value", Encryption.Decrypt(adviserstaffsmtpvo.Password));
                txtSMTPHost.Text = adviserstaffsmtpvo.HostServer;
                txtSMTPPort.Text = adviserstaffsmtpvo.Port;
                chkAthenticationRequired.Checked = Convert.ToBoolean(adviserstaffsmtpvo.IsAuthenticationRequired);
            }

            string a = txtPassword.Text;
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
            adviserstaffsmtpvo.Password =Encryption.Encrypt(txtPassword.Text.Trim());
            adviserstaffsmtpvo.Port = txtSMTPPort.Text.Trim();
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
                email.From = new MailAddress(txtEmail.Text.Trim(), "Test Email from MoneyTouch360");
            else
                email.From = new MailAddress("admin@wealthERP.com", "Test Email from MoneyTouch360");

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
                lblInsertMessage.Text = "Not able to send mail using the SMTP credentials.\nError:" + statusMessage;
            }
        }
    }
}
