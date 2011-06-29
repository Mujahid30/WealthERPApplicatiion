using System;
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

namespace WealthERP.SuperAdmin
{
    public partial class IFFUserManagement : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        List<RMVo> IFFUserList = null;
        int userId;
        UserBo userBo = new UserBo();

        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            try
            {
                mypager.CurrentPage = 1;
                if (!IsPostBack)
                    this.BindGrid();
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
                    ErrorMessage.Visible = false;
                }
                else
                {
                    gvIFFUsers.DataSource = null;
                    gvIFFUsers.Visible = false;
                    ErrorMessage.Visible = true;
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

                            userVo = userBo.GetUserDetails(userId);
                            string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                            email.GetAdviserRMAccountMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), userName);
                            email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                            email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                            email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                            email.To.Add(userVo.Email);

                            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                            //int rmId = Convert.ToInt32(ViewState["rmId"]);

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
                            bool isMailSent = emailer.SendMail(email);

                            if (isMailSent)
                            {
                                statusMessage = "Credentials have been sent to selected Adviser";
                                tblMessage.Visible = true;
                                ErrorMessage.Visible = false;
                                SuccessMsg.InnerText = statusMessage;
                                SuccessMsg.Visible = true;
                            }
                            else
                            {
                                statusMessage = "An error occurred while sending mail to selected Adviser";
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

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetRMNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.ShowIFFs();
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
                    userVo.Password = r.Next(20000, 100000).ToString();
                    userVo.IsTempPassword = 1;
                    userVo.Password = Encryption.Encrypt(userVo.Password);
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
        }
    }
}