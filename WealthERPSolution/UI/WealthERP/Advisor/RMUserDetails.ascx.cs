using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoAdvisorProfiling;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using PCGMailLib;
using System.Net.Mail;
using BoCommon;
using WealthERP.Base;
using VoHostConfig;
using System.Configuration;
using System.IO;
using Telerik.Web.UI;

namespace WealthERP.Advisor
{

    public partial class RMUserDetails : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserBo userBo = new UserBo();
        List<RMVo> rmUserList = null;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int userId;
        int PageSize;
        OneWayEncryption encryption;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["AdvisorVo"];
            tblMessage.Visible = false;
            SuccessMsg.Visible = false;
            ErrorMessage.Visible = false;
            if (!IsPostBack)
            {
                ViewState["rmId"] = 0;
                //trNoRecords.Visible = false;

                showRMUserDetails();
            }
        }



        public void showRMUserDetails()
        {
            try
            {


                UserVo uservo = (UserVo)Session["userVo"];
                AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();
                RMVo advrm = new RMVo();
                advrm = adviserstaffbo.GetAdvisorStaff(uservo.UserId);
                ViewState["rmId"] = advrm.RMId;

                //if (hdnCurrentPage.Value.ToString() != "")
                //{
                //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                //    hdnCurrentPage.Value = "";
                //}

                int count = 0;

                rmUserList = advisorStaffBo.GetRMList(advisorVo.advisorId, hdnSort.Value.Trim(), hdnNameFilter.Value.Trim());

                //lblTotalRows.Text = hdnRecordCount.Value = count.ToString();

                if (rmUserList != null)
                {

                    DataTable dtRMUsers = new DataTable();

                    dtRMUsers.Columns.Add("S.No");
                    dtRMUsers.Columns.Add("RMName");
                    dtRMUsers.Columns.Add("LoginId");
                    dtRMUsers.Columns.Add("EmailId");
                    dtRMUsers.Columns.Add("UserId");
                    DataRow drRMUsers;

                    for (int i = 0; i < rmUserList.Count; i++)
                    {
                        drRMUsers = dtRMUsers.NewRow();
                        rmVo = new RMVo();
                        rmVo = rmUserList[i];
                        userId = rmVo.UserId;
                        userVo = new UserVo();
                        userVo = userBo.GetUserDetails(userId);

                        drRMUsers[0] = (i + 1).ToString();
                        drRMUsers[1] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();

                        if (userVo != null)
                        {
                            drRMUsers[2] = userVo.LoginId.ToString();

                            drRMUsers[3] = userVo.Email;
                            drRMUsers[4] = userVo.UserId;
                        }

                        dtRMUsers.Rows.Add(drRMUsers);
                    }
                    if (Cache["customerList" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("customerList" + advisorVo.advisorId, dtRMUsers);
                    }
                    else
                    {
                        Cache.Remove("customerList" + advisorVo.advisorId);
                        Cache.Insert("customerList" + advisorVo.advisorId, dtRMUsers);
                    }
                    gvUserMgt.DataSource = dtRMUsers;
                    gvUserMgt.DataBind();
                    //gvRMUsers.PageSize = PageSize;

                    //if (trPagger.Visible == false)
                    //    trPagger.Visible = true;

                    //this.GetPageCount();

                    //if (btnGenerate.Visible == false)
                    //    btnGenerate.Visible = true;
                    //if (mypager.Visible == false)
                    //    mypager.Visible = true;
                }
                else
                {
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    tblPager.Visible = false;
                    //trNoRecords.Visible = true;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    SuccessMsg.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
                    trPagger.Visible = false;
                    btnGenerate.Visible = false;


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

                FunctionInfo.Add("Method", "RMUserDetails.ascx:showRMUserDetails()");

                object[] objects = new object[5];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                objects[3] = rmUserList;
                objects[4] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

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
            int userId = int.Parse((gvUserMgt.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));

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


        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();

            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session["AdvisorPreferenceVo"] != null)
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];

            int selectedRecords = 0;
            string statusMessage = string.Empty;
            advisorVo=(AdvisorVo)Session["advisorVo"];
            string logoPath = string.Empty;
            if (Page.IsValid)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "$.colorbox({width: '700px', overlayClose: false, inline: true, href: '#LoadImage'});", true);
                try
                {
                    foreach (GridDataItem gvr in gvUserMgt.Items)
                    {
                        if (((CheckBox)gvr.FindControl("cbRecons")).Checked == true)
                        {
                            selectedRecords++;
                            int selectedRow = gvr.ItemIndex + 1;
                            string Phone1Details = string.Empty, phone2Details = string.Empty, phone3Details = string.Empty, PhoneNumber = string.Empty;
                            userId = int.Parse((gvUserMgt.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));
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
                            if (!string.IsNullOrEmpty(advisorVo.Phone1Number.ToString()) || !string.IsNullOrEmpty(advisorVo.Phone2Number.ToString()) || !string.IsNullOrEmpty(advisorVo.Phone3Number.ToString()))
                            {
                                if (!string.IsNullOrEmpty(advisorVo.Phone1Isd) && !string.IsNullOrEmpty(advisorVo.Phone1Std) && advisorVo.Phone1Number > 1)
                                    Phone1Details = advisorVo.Phone1Isd.ToString() + "-" + advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString();
                                else if (!string.IsNullOrEmpty(advisorVo.Phone1Isd) && !string.IsNullOrEmpty(advisorVo.Phone1Std) && advisorVo.Phone1Number > 1)
                                    Phone1Details = advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString();
                                else if (!string.IsNullOrEmpty(advisorVo.Phone1Isd) && !string.IsNullOrEmpty(advisorVo.Phone1Std) && advisorVo.Phone1Number > 1)
                                    Phone1Details = advisorVo.Phone1Isd.ToString() + "-" + advisorVo.Phone1Number.ToString();
                                else if (string.IsNullOrEmpty(advisorVo.Phone1Isd) && string.IsNullOrEmpty(advisorVo.Phone1Std) && advisorVo.Phone1Number > 1)
                                    Phone1Details = advisorVo.Phone1Number.ToString();


                                if (!string.IsNullOrEmpty(advisorVo.Phone2Isd) && !string.IsNullOrEmpty(advisorVo.Phone2Std) && advisorVo.Phone2Number > 1)
                                    phone2Details = advisorVo.Phone2Isd.ToString() + "-" + advisorVo.Phone2Std.ToString() + "-" + advisorVo.Phone2Number.ToString();
                                else if (string.IsNullOrEmpty(advisorVo.Phone2Isd) && !string.IsNullOrEmpty(advisorVo.Phone2Std) && advisorVo.Phone2Number > 1)
                                    phone2Details = advisorVo.Phone2Std.ToString() + "-" + advisorVo.Phone2Number.ToString();
                                else if (!string.IsNullOrEmpty(advisorVo.Phone2Isd) && string.IsNullOrEmpty(advisorVo.Phone2Std) && advisorVo.Phone2Number > 1)
                                    phone2Details = advisorVo.Phone2Isd.ToString() + "-" + advisorVo.Phone2Number.ToString();
                                else if (string.IsNullOrEmpty(advisorVo.Phone2Isd) && string.IsNullOrEmpty(advisorVo.Phone2Std) && advisorVo.Phone2Number > 1)
                                    phone2Details = advisorVo.Phone2Number.ToString();

                                if (!string.IsNullOrEmpty(advisorVo.Phone3Isd) && !string.IsNullOrEmpty(advisorVo.Phone3Std) && advisorVo.Phone3Number > 1)
                                    phone3Details = advisorVo.Phone3Isd.ToString() + "-" + advisorVo.Phone3Std.ToString() + "-" + advisorVo.Phone3Number.ToString();
                                else if (string.IsNullOrEmpty(advisorVo.Phone3Isd) && !string.IsNullOrEmpty(advisorVo.Phone3Std) && advisorVo.Phone3Number > 1)
                                    phone3Details = advisorVo.Phone3Std.ToString() + "-" + advisorVo.Phone3Number.ToString();
                                else if (!string.IsNullOrEmpty(advisorVo.Phone3Isd) && string.IsNullOrEmpty(advisorVo.Phone3Std) && advisorVo.Phone3Number > 1)
                                    phone3Details = advisorVo.Phone3Isd.ToString() + "-" + advisorVo.Phone3Number.ToString();
                                else if (string.IsNullOrEmpty(advisorVo.Phone3Isd) && string.IsNullOrEmpty(advisorVo.Phone3Std) && advisorVo.Phone3Number > 1)
                                    phone3Details = advisorVo.Phone3Number.ToString();
                                if(!string.IsNullOrEmpty(Phone1Details) && !string.IsNullOrEmpty(phone2Details) && !string.IsNullOrEmpty(phone3Details))
                                {
                                  PhoneNumber =Phone1Details + "/"+  phone2Details+ "/" + phone3Details;
                                }
                                else if (!string.IsNullOrEmpty(Phone1Details) && !string.IsNullOrEmpty(phone2Details) && string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = Phone1Details + "/" + phone2Details;
                                }
                                else if (string.IsNullOrEmpty(Phone1Details) && !string.IsNullOrEmpty(phone2Details) && !string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = phone2Details + "/" + phone3Details;
                                }
                                else if (!string.IsNullOrEmpty(Phone1Details) && string.IsNullOrEmpty(phone2Details) && !string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = Phone1Details + "/" + phone3Details;
                                }
                                else if (!string.IsNullOrEmpty(Phone1Details) && string.IsNullOrEmpty(phone2Details) && string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = Phone1Details;
                                }
                                else if (string.IsNullOrEmpty(Phone1Details) && !string.IsNullOrEmpty(phone2Details) && string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = phone2Details;
                                }
                                else if (string.IsNullOrEmpty(Phone1Details) && string.IsNullOrEmpty(phone2Details) && !string.IsNullOrEmpty(phone3Details))
                                {
                                    PhoneNumber = phone3Details;
                                }

                                email.Body = email.Body.Replace("[PHONE]", PhoneNumber);
                            }
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
                            //Add two views to message.
                            email.AlternateViews.Add(plainTextView);
                            email.AlternateViews.Add(htmlView);
                            
                            email.To.Add(userVo.Email);

                            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                            int rmId = Convert.ToInt32(ViewState["rmId"]);
                            AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(rmId);
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
                                statusMessage = "Credentials have been reset and sent to selected user" ;
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
                    //if (selectedRecords == 0)
                    //statusMessage = "Please select RM to send Password";
                   
                    
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

        protected void gvAssoMgt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["customerList" + advisorVo.advisorId];
            gvUserMgt.DataSource = dtCustomer;

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvUserMgt.ExportSettings.OpenInNewWindow = true;
            gvUserMgt.ExportSettings.IgnorePaging = true;
            gvUserMgt.ExportSettings.HideStructureColumns = true;
            gvUserMgt.ExportSettings.ExportOnlyData = true;
            gvUserMgt.ExportSettings.FileName = "StaffUserDetails";
            gvUserMgt.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvUserMgt.MasterTableView.ExportToExcel();
        }

    }
}
