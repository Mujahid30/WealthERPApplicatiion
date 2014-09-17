using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using System.Configuration;
using VOAssociates;
using BOAssociates;
using BoUser;
using BoCommon;
using VoAdvisorProfiling;
using PCGMailLib;
using BoAdvisorProfiling;
using System.IO;
using System.Net.Mail;

namespace WealthERP.UserManagement
{
    public partial class AgentManagement : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        RMVo rmVo = new RMVo();
        OneWayEncryption encryption;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            if (!IsPostBack)
            {
                BindAssociateUserMangemnetGrid(advisorVo.advisorId);
            }
        }

        private void BindAssociateUserMangemnetGrid(int adviserId)
        {
            DataSet dsGetAssociateList;
            DataTable dtGetAssociateList;
            dsGetAssociateList = associatesBo.AssociateUserMangemnetList(advisorVo.advisorId);
            dtGetAssociateList = dsGetAssociateList.Tables[0];
            if (dtGetAssociateList == null)
            {
                gvAssoMgt.DataSource = null;
                gvAssoMgt.DataBind();
            }
            else
            {
                gvAssoMgt.DataSource = dtGetAssociateList;
                gvAssoMgt.DataBind();
                if (Cache["gvAssoMgt" + userVo.UserId] == null)
                {
                    Cache.Insert("gvAssoMgt" + userVo.UserId, dtGetAssociateList);
                }
                else
                {
                    Cache.Remove("gvAssoMgt" + userVo.UserId);
                    Cache.Insert("gvAssoMgt" + userVo.UserId, dtGetAssociateList);
                }
            }
        }
        protected void lnkReset_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            encryption = new OneWayEncryption();
            bool isSuccess = false;
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int userId = int.Parse((gvAssoMgt.MasterTableView.DataKeyValues[selectedRow - 1]["U_userId"].ToString()));

            userVo = userBo.GetUserDetails(userId);
            if (userVo != null)
            {
                string hassedPassword = string.Empty;
                string saltValue = string.Empty;
                string password = r.Next(20000, 100000).ToString();

                //userVo = userBo.GetUserDetails(userId);
                string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                userVo.Password = hassedPassword;
                userVo.PasswordSaltValue = saltValue;
                userVo.OriginalPassword = password;
                userVo.IsTempPassword = 1;
                isSuccess = userBo.UpdateUser(userVo);
            }

            if (isSuccess)
            {
                tblMessage.Visible = true;
                SuccessMsg.Visible = true;
                ErrorMessage.Visible = false;
                SuccessMsg.InnerText = "Password has been reset successfully...";

            }
            else
            {
                tblMessage.Visible = true;
                SuccessMsg.Visible = false;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "An error occurred while reseting password.";

            }
        }
        protected void gvAssoMgt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetAssociateList = new DataTable();
            dtGetAssociateList = (DataTable)Cache["gvAssoMgt" + userVo.UserId];
            gvAssoMgt.DataSource = dtGetAssociateList;
            gvAssoMgt.Visible = true;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();

            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session["AdvisorPreferenceVo"] != null)
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];

            int selectedRecords = 0;
            string statusMessage = string.Empty;
            advisorVo = (AdvisorVo)Session["advisorVo"];
            string logoPath = string.Empty;
            int count=0;
            foreach (GridDataItem gvRow in gvAssoMgt.Items)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    count++;
                }
                if (count > 1)
                    chk.Checked = false;

            }
            
            try
            {
                foreach (GridDataItem gdi in gvAssoMgt.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbRecons")).Checked == true)
                    {
                        selectedRecords++;
                        int selectedRow = gdi.ItemIndex + 1;
                        int userId = int.Parse((gvAssoMgt.MasterTableView.DataKeyValues[selectedRow - 1]["U_userId"].ToString()));

                        Emailer emailer = new Emailer();
                        EmailMessage email = new EmailMessage();
                        string hassedPassword = string.Empty;
                        string saltValue = string.Empty;
                        encryption = new OneWayEncryption();
                        Random r = new Random();

                        userVo = userBo.GetUserDetails(userId);
                        string password = r.Next(20000, 100000).ToString();
                        encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                        userVo.Password = hassedPassword;
                        userVo.PasswordSaltValue = saltValue;
                        userVo.OriginalPassword = password;
                        userVo.IsTempPassword = 1;
                        userBo.UpdateUser(userVo);

                        string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;

                        email.GetResetPasswordMail(userVo.LoginId, password, userName);
                        email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                        email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                        //email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                        email.Body = email.Body.Replace("[CUSTOMER_NAME]", userVo.FirstName);
                        if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                        {
                            email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                        }
                        else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Citrus")
                        {
                            email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                        }

                        email.Body = email.Body.Replace("[CONTACTPERSON]", advisorVo.ContactPersonFirstName + " " + advisorVo.ContactPersonMiddleName + " " + advisorVo.ContactPersonLastName);
                        if (!string.IsNullOrEmpty(advisorVo.Designation))
                            email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation.Trim());
                        else
                            email.Body = email.Body.Replace("[DESIGNATION]", string.Empty);
                        if (!string.IsNullOrEmpty(advisorVo.Phone1Number.ToString()))
                            email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString().Trim() + "-" + advisorVo.Phone1Number.ToString().Trim());
                        else
                            email.Body = email.Body.Replace("[PHONE]", string.Empty);

                        if (!string.IsNullOrEmpty(advisorVo.Email))
                            email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email.Trim());
                        else
                            email.Body = email.Body.Replace("[EMAIL]", string.Empty);

                        email.Body = email.Body.Replace("[LOGO]", "<img src='cid:HDIImage' alt='Logo'>");

                        System.Net.Mail.AlternateView htmlView;
                        System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                        //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                        htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + email.Body + "</p></body></html>", null, "text/html");
                        //Add image to HTML version
                        if (advisorVo != null)
                            logoPath = "~/Images/" + advisorVo.LogoPath;
                        if (!File.Exists(Server.MapPath(logoPath)))
                            logoPath = "~/Images/spacer.png";
                        //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                        System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
                        imageResource.ContentId = "HDIImage";
                        htmlView.LinkedResources.Add(imageResource);
                        email.AlternateViews.Add(plainTextView);
                        email.AlternateViews.Add(htmlView);

                        email.To.Add(userVo.Email);

                        AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();                         
                        AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(rmVo.RMId);
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
                                    email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                                }
                                else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                                {
                                    email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                                }

                            }
                        }
                        bool isMailSent = false;

                        if (userBo.UpdateUser(userVo))
                        {
                            isMailSent = emailer.SendMail(email);
                        }

                        if (isMailSent)
                        {
                            statusMessage = "Credentials have been reset and sent to selected user";
                            tblMessage.Visible = true;
                            ErrorMessage.Visible = false;
                            SuccessMsg.InnerText = statusMessage;
                            SuccessMsg.Visible = true;
                        }
                        else
                        {
                            statusMessage = "An error occurred while sending mail to selected user";
                            tblMessage.Visible = true;
                            ErrorMessage.Visible = true;
                            ErrorMessage.InnerText = statusMessage;
                            SuccessMsg.Visible = false;

                        }
                    }
                }
                ErrorMessage.Visible = false;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerUserDetails.ascx:btnGenerate_Click()");

                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}