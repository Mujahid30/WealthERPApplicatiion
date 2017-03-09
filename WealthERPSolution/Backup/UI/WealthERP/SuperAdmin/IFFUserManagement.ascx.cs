﻿using System;
using System.Web;
using System.Web.UI;
using VoUser;
using BoWerpAdmin;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Mail;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using PCGMailLib;
using BoUser;
using System.IO;

namespace WealthERP.SuperAdmin
{
    public partial class IFFUserManagement : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        List<RMVo> IFFUserList = null;
        int userId;
        UserBo userBo = new UserBo();
        OneWayEncryption encryption = new OneWayEncryption();
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();

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
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            try
            {
                mypager.CurrentPage = 1;
                if (!IsPostBack)
                {
                    this.BindGrid();
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:Page_Load()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindGrid()
        {
            try
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }
                ShowIFFs();
                GetPageCount();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRMD.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ShowIFFs()
        {
            List<AdvisorVo> IFvolist = new List<AdvisorVo>();
            int count = 0;

            try
            {
                IFvolist = adviserMaintenanceBo.GetIFFListForUserManagement(mypager.CurrentPage, out count, hdnSort.Value, hdnNameFilter.Value);
                if (IFvolist.Count != 0)
                {
                    DataTable dtIFFList = new DataTable();

                    dtIFFList.Columns.Add("UserId");
                    dtIFFList.Columns.Add("IFFName");
                    dtIFFList.Columns.Add("LoginId");
                    dtIFFList.Columns.Add("EmailId");

                    foreach (AdvisorVo advisorVo in IFvolist)
                    {
                        DataRow drIFFlist = dtIFFList.NewRow();
                        drIFFlist["UserId"] = advisorVo.UserId;
                        drIFFlist["IFFName"] = advisorVo.OrganizationName;
                        drIFFlist["LoginId"] = advisorVo.LoginId;
                        drIFFlist["EmailId"] = advisorVo.Email;
                        dtIFFList.Rows.Add(drIFFlist);
                    }
                    gvIFFUsers.Visible = true;
                    gvIFFUsers.DataSource = dtIFFList;
                    gvIFFUsers.DataBind();
                    hdnRecordCount.Value = count.ToString();
                    tblErrorMassage.Visible = false;
                    //ErrorMessage.Visible = false;
                }
                else
                {
                    tblErrorMassage.Visible = true;
                    //ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records found..";
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
                FunctionInfo.Add("Method", "SuperAdminMessageBroadcast.ascx.cs:ShowIFFs(string filterexpression)");
                object[] objects = new object[3];
                objects[0] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSendEmailToIFF_Click(object sender, EventArgs e)
        {
            int selectedRecords = 0;
            string statusMessage = string.Empty;
            advisorVo = (AdvisorVo)Session["advisorVo"];
            AdvisorBo advisorBo = new AdvisorBo();
            RMVo rmVo = new RMVo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            Random r = new Random();
            string logoPath = string.Empty;

            if (Page.IsValid)
            {
                try
                {
                    foreach (GridViewRow gvr in gvIFFUsers.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkBoxChild")).Checked == true)
                        {
                            selectedRecords++;

                            userId = int.Parse(gvIFFUsers.DataKeys[gvr.RowIndex].Value.ToString());
                            advisorVo = advisorBo.GetAdvisorUser(userId);


                            rmVo = advisorStaffBo.GetAdvisorStaff(userId);

                            Emailer emailer = new Emailer();
                            EmailMessage email = new EmailMessage();
                            string hassedPassword = string.Empty;
                            string saltValue = string.Empty;
                            string password = r.Next(20000, 100000).ToString();

                            userVo = userBo.GetUserDetails(userId);
                            string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;

                            encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                            userVo.Password = hassedPassword;
                            userVo.PasswordSaltValue = saltValue;
                            userVo.OriginalPassword = password;
                            userVo.IsTempPassword = 1;

                            email.GetResetPasswordMail(userVo.LoginId, password, userName);

                            //email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                            //email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                            //email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                            email.To.Add(userVo.Email);

                            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                            //int rmId = Convert.ToInt32(ViewState["rmId"]);

                            AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(1000);
                            //adviserStaffSMTPVo.HostServer = "";
                            if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
                            {
                                emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                                if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                                    emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                                emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                                emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                                emailer.smtpUserName = adviserStaffSMTPVo.Email;

                                //email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                                //email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                                email.Body = email.Body.Replace("[ORGANIZATION]", "WealthERP Team");
                            
                                if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                                {
                                    email.Body = email.Body.Replace("[WEBSITE]", "https://app.wealtherp.com/");
                                }

                                email.Body = email.Body.Replace("[CONTACTPERSON]", "Customer Care");

                                email.Body = email.Body.Replace("[DESIGNATION]", "Team WealthERP");

                                email.Body = email.Body.Replace("[PHONE]", "+91 9663305249 <br/>Skype: custcare.ampsys");

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




                                //if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                                //{
                                //    if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                                //    {
                                //        email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                                //    }
                                //    else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                                //    {
                                //        email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                                //    }

                                //}
                            }

                            bool isMailSent=false;
                            if (userBo.UpdateUser(userVo))
                            {
                                isMailSent=emailer.SendMail(email); 
                            }

                            if (isMailSent)
                            {
                                statusMessage = "Credentials have been reset & sent to selected Adviser";
                                tblMessage.Visible = true;
                                tblErrorMassage.Visible = false;
                                //ErrorMessage.Visible = false;
                                SuccessMsg.InnerText = statusMessage;
                                //SuccessMsg.Visible = true;
                            }
                            else
                            {
                                statusMessage = "An error occurred while sending mail to selected Adviser";
                                tblMessage.Visible = false;
                                tblErrorMassage.Visible = true;
                                //ErrorMessage.Visible = true;
                                ErrorMessage.InnerText = statusMessage;
                                //SuccessMsg.Visible = false;

                            }
                        }
                    }
                    tblErrorMassage.Visible = false;
                    //ErrorMessage.Visible = false;

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

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetRMNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.ShowIFFs();
                //tblErrorMassage.Visible = false;
                tblMessage.Visible = false;
            }
        }

        private TextBox GetRMNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvIFFUsers.HeaderRow != null)
            {
                if ((TextBox)gvIFFUsers.HeaderRow.FindControl("txtIFFNameSearch") != null)
                {
                    txt = (TextBox)gvIFFUsers.HeaderRow.FindControl("txtIFFNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void gvIFFUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Random r = new Random();

            bool isSuccess = false;

            if (e.CommandName == "resetPassword")
            {

                userVo = userBo.GetUserDetails(Convert.ToInt32(gvIFFUsers.DataKeys[int.Parse(e.CommandArgument.ToString())].Value));
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
                    //SuccessMsg.Visible = true;
                    tblErrorMassage.Visible = false;
                    //ErrorMessage.Visible = false;
                    SuccessMsg.InnerText = "Password has been reset successfully...";
                }
                else
                {
                    //tblMessage.Visible = true;
                    SuccessMsg.Visible = false;
                    tblErrorMassage.Visible = true;
                    //ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "An error occurred while reseting password.";

                }
            }
        }

        private void GetPageCount()
        {
            string upperlimit = string.Empty;
            string lowerlimit = string.Empty;
            int rowCount = 0;
            try
            {
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {

                    int ratio = rowCount / 20;
                    mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
                    upperlimit = (mypager.CurrentPage * 20).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    lblTotalRows.Text = hdnRecordCount.Value.ToString();
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
                }
                if(hdnRecordCount.Value == "")
                {
                    trPagger.Visible = false;
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
                FunctionInfo.Add("Method", "RejectedMFFolio.ascx.cs:GetPageCount()");
                object[] objects = new object[3];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = lowerlimit;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}