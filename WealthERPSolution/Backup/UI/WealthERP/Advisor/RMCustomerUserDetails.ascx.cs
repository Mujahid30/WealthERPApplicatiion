using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using BoUser;
using System.Collections.Specialized;
using WealthERP.Base;
using PCGMailLib;
using System.Net.Mail;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoCommon;
using VoAdvisorProfiling;
using System.IO;
using Telerik.Web.UI;


namespace WealthERP.Advisor
{
    public partial class RMCustomerUserDeatils : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBo advisorBo = new AdvisorBo();
        //AdvisorVo advisorVo = new AdvisorVo();
        int customerId;
        int userId;
        UserVo userVo = new UserVo();
        UserVo advisorUserVo = new UserVo();
        UserBo userBo = new UserBo();
        Random r = new Random();
        PcgMailMessage email = new PcgMailMessage();
        string statusMessage = string.Empty;
        AdvisorVo advisorVo = new AdvisorVo();
        OneWayEncryption encryption;

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                tblMessage.Visible = false;
                SuccessMsg.Visible = false;
                ErrorMessage.Visible = false;
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    this.BindGrid();
                }

                advisorUserVo = (UserVo)Session[SessionContents.UserVo];
                advisorVo = (AdvisorVo)Session["AdvisorVo"];
                //**************************lblStatusMsg.Text = string.Empty;
                //lblMailSent.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerUserDetails.ascx.cs:Page_Load()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

      

       

       

        protected void BindGrid()
        {
            List<CustomerVo> customerUserList = null;
            DataTable dtRMCustomer = new DataTable();
            Dictionary<string, string> genDictParent = new Dictionary<string, string>();
            Dictionary<string, string> genDictRM = new Dictionary<string, string>();
            Dictionary<string, string> genDicReassigntRM = new Dictionary<string, string>();


            try
            {
                // rmVo = (RMVo)Session["rmVo"];              
                
                advisorVo = (AdvisorVo)Session["advisorVo"];

                int Count = 0;
                dtRMCustomer = advisorBo.GetOfflineCustomerList(advisorVo.advisorId);

                if (dtRMCustomer.Rows.Count !=0)
                {
                  
                  if (Cache["Customerdetails" + advisorVo.advisorId]!= null)
                    {
                        Cache.Remove("Customerdetails" + advisorVo.advisorId);
                    }
                    Cache.Insert("Customerdetails" + advisorVo.advisorId, dtRMCustomer);
                    gvCustomer.DataSource = dtRMCustomer;
                    gvCustomer.DataBind();
                }
                else
                {
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    SuccessMsg.Visible = false;
                    trPagger.Visible = false;
                    btnGenerate.Visible = false;
                    ErrorMessage.InnerText = "No Records Found...!";
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
                FunctionInfo.Add("Method", "RMCustomerUserDetails.ascx:BindGrid()");
                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = customerVo;
                objects[2] = userVo;
                objects[3] = userId;
                objects[4] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void gvCustomers_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindGrid();
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
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

                FunctionInfo.Add("Method", "RMCustomerUserDetails.ascx.cs:gvCustomers_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int Count = 0;
            bool loginReset = false;
            encryption = new OneWayEncryption();
            try
            {
                foreach (GridDataItem gvr in this.gvCustomer.Items)
                {
                    if (((CheckBox)gvr.FindControl("cbRecons")).Checked == true)
                    {
                        Count = Count + 1;
                    }
                }
                if (Count == 0)
                {
                    //lblMailSent.Text = "Please select the Customer..!";
                    //********************* lblStatusMsg.Text = "Please select the Customer";
                    //lblMailSent.Visible = true;
                }
                else
                {
                    foreach (GridDataItem gvr in this.gvCustomer.Items)
                    {
                        if (((CheckBox)gvr.FindControl("cbRecons")).Checked == true)
                        {
                            string password = r.Next(20000, 100000).ToString();
                            string hassedPassword;
                            string saltValue;
                            int selectedRow = gvr.ItemIndex + 1;
                            int userId = int.Parse((gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));
                            //userId = int.Parse(gvCustomers.DataKeys[gvr.RowIndex].Value.ToString());
                            userVo = new UserVo();
                            userVo = userBo.GetUserDetails(userId);

                            if (string.IsNullOrEmpty(userVo.LoginId))
                            {
                                loginReset = true;
                                userVo.LoginId = r.Next(10000000, 99999999).ToString();
                                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                                userVo.Password = hassedPassword;
                                userVo.PasswordSaltValue = saltValue;
                                userVo.OriginalPassword = password;
                                userVo.IsTempPassword = 1;
                                userBo.UpdateUser(userVo);

                            }
                            else
                            {
                                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                                userVo.Password = hassedPassword;
                                userVo.PasswordSaltValue = saltValue;
                                userVo.OriginalPassword = password;
                                userVo.IsTempPassword = 1;
                                userBo.UpdateUser(userVo);
                            }
                            //Send email to customer
                            //
                            //string EmailPath = Server.MapPath(ConfigurationManager.AppSettings["EmailPath"].ToString());
                            //email.SendCustomerLoginPassMail(advisorUserVo.Email, userVo.Email, advisorUserVo.LastName, userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName, userVo.LoginId, password, EmailPath);
                            SendMail(userVo, false);


                        }
                    }

                    //*********************** lblStatusMsg.Text = statusMessage;
                    //lblMailSent.Visible = true;
                    //tblMessage.Visible = true;
                    //SuccessMsg.Visible = true;
                    //SuccessMsg.InnerText = statusMessage;
                    if (loginReset == true)
                    {
                        //mypager.CurrentPage = int.Parse(hdnCurrentPage.Value.ToString());
                        BindGrid();
                    }
                }

                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
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
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = customerId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private bool SendMail(UserVo userVo, bool isNewLogin)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();

            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session["AdvisorPreferenceVo"] != null)
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            string logoPath = string.Empty;
            bool isMailSent = false;
            bool isEmailIdBlank = false;
            try
            {
                UserVo uservo = (UserVo)Session["userVo"];
                AdvisorStaffBo adviserstaffbo = new AdvisorStaffBo();

                //Get SMTP settings of admin if configured.
                RMVo advrm = new RMVo();
                advrm = adviserstaffbo.GetAdvisorStaff(uservo.UserId);
                AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(advrm.RMId);

                //Get the mail contents
                if (userVo.Email.Trim() != string.Empty)
                {
                    email.To.Add(userVo.Email);
                    string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                    if (isNewLogin)
                    {
                        email.GetCustomerAccountMail(userVo.LoginId, userVo.OriginalPassword, name);
                    }
                    else
                    {
                        email.GetResetPasswordMail(userVo.LoginId, userVo.OriginalPassword, name);
                    }
                    email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                    email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                    email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
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
                    email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation);
                    else
                      email.Body = email.Body.Replace("[DESIGNATION]", string.Empty);
                    if (!string.IsNullOrEmpty(advisorVo.Phone1Number.ToString()))
                     email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString());
                    else
                        email.Body = email.Body.Replace("[PHONE]", string.Empty);

                    if (!string.IsNullOrEmpty(advisorVo.Email))
                       email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email);
                    else
                        email.Body = email.Body.Replace("[EMAIL]",string.Empty);

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
                                email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                            }
                            else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                            {
                                email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                            }
                        }
                    }
                    //Sending mail...
                    isMailSent = emailer.SendMail(email);
                }
                else
                    isEmailIdBlank = true;
                if (isEmailIdBlank)
                {
                    if (string.IsNullOrEmpty(statusMessage))
                    {
                        statusMessage = "No email Id specified for selected User";
                        tblMessage.Visible = true;
                        ErrorMessage.Visible = true;
                        ErrorMessage.InnerText = statusMessage;
                        SuccessMsg.Visible = false;

                    }
                    else
                    {
                        statusMessage = statusMessage + " and some selected User don't have E-mail id";
                        tblMessage.Visible = true;
                        ErrorMessage.Visible = true;
                        ErrorMessage.InnerText = statusMessage;
                        SuccessMsg.Visible = false;
                    }

                }

                else if (isMailSent)
                {
                    if (string.IsNullOrEmpty(statusMessage))
                    {
                        statusMessage = "Credentials have been sent to selected User";
                        tblMessage.Visible = true;
                        ErrorMessage.Visible = false;
                        SuccessMsg.InnerText = statusMessage;
                        SuccessMsg.Visible = true;
                    }
                    else if (statusMessage == "No email Id specified for slected User")
                    {
                        statusMessage = "some selected User don't have E-mail id and Credentials have been sent sucessfully to rest of User";
                        statusMessage = "Credentials have been sent to selected User";
                        tblMessage.Visible = true;
                        ErrorMessage.Visible = false;
                        SuccessMsg.InnerText = statusMessage;
                        SuccessMsg.Visible = true;
                    }
                    else
                    {
                        tblMessage.Visible = true;
                        ErrorMessage.Visible = false;
                        SuccessMsg.InnerText = statusMessage;
                        SuccessMsg.Visible = true;

                    }
                }
                else
                {
                    statusMessage = "An error occurred while sending mail .. ";
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = statusMessage;
                    SuccessMsg.Visible = false;

                }

            }
            catch (Exception ex)
            {

            }
            return isMailSent;
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
            int userId = int.Parse((gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));

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

        protected void lnkGenerate_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            encryption = new OneWayEncryption();
            bool isSuccess = false;
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int userId = int.Parse((gvCustomer.MasterTableView.DataKeyValues[selectedRow - 1]["UserId"].ToString()));

            userVo = userBo.GetUserDetails(userId);
            string password = r.Next(20000, 100000).ToString();
            string hassedPassword;
            string saltValue;
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);
            userVo = userBo.GetUserDetails(userId);

            if (string.IsNullOrEmpty(userVo.LoginId))
            {
                userVo.LoginId = "Cu" + r.Next(100000, 999999).ToString();
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                userVo.Password = hassedPassword;
                userVo.PasswordSaltValue = saltValue;
                userVo.OriginalPassword = password;
                userVo.IsTempPassword = 1;
                userBo.UpdateUser(userVo);

            }
            SendMail(userVo, true);
            BindGrid();


        
        }

        //protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    Random r = new Random();
        //    encryption = new OneWayEncryption();
        //    bool isSuccess = false;

        //    if (e.CommandName == "resetPassword")
        //    {
        //        int userId = int.Parse(e.CommandArgument.ToString());
        //        userVo = userBo.GetUserDetails(userId);
        //        if (userVo != null)
        //        {
        //            string hassedPassword = string.Empty;
        //            string saltValue = string.Empty;
        //            string password = r.Next(20000, 100000).ToString();

        //            userVo = userBo.GetUserDetails(userId);
        //            string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
        //            encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
        //            userVo.Password = hassedPassword;
        //            userVo.PasswordSaltValue = saltValue;
        //            userVo.OriginalPassword = password;
        //            userVo.IsTempPassword = 1;
        //            isSuccess = userBo.UpdateUser(userVo);
        //        }
        //        if (isSuccess)
        //        {
        //            //********************lblStatusMsg.Text = "Password has been reset.";
        //            tblMessage.Visible = true;
        //            SuccessMsg.Visible = true;
        //            SuccessMsg.InnerText = "Password has been reset.";
        //        }
        //        else
        //        {
        //            //************************lblStatusMsg.Text = "An error occurred while reseting password.";
        //            tblMessage.Visible = true;
        //            ErrorMessage.Visible = true;
        //            ErrorMessage.InnerText = "An error occurred while reseting password.";

        //        }
        //    }
        //    else if (e.CommandName == "GenerateLogin")
        //    {
        //        int userId = int.Parse(e.CommandArgument.ToString());
        //        string password = r.Next(20000, 100000).ToString();
        //        string hassedPassword;
        //        string saltValue;
        //        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);
        //        userVo = userBo.GetUserDetails(userId);

        //        if (string.IsNullOrEmpty(userVo.LoginId))
        //        {
        //            userVo.LoginId = "Cu" + r.Next(100000, 999999).ToString();
        //            encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
        //            userVo.Password = hassedPassword;
        //            userVo.PasswordSaltValue = saltValue;
        //            userVo.OriginalPassword = password;
        //            userVo.IsTempPassword = 1;
        //            userBo.UpdateUser(userVo);

        //        }
        //        SendMail(userVo, true);

        //        mypager.CurrentPage = int.Parse(hdnCurrentPage.Value.ToString());
        //        BindGrid();

        //    }
        //}

        //protected void btnNameSearch_Click(object sender, EventArgs e)
        //{
        //    TextBox txtName = GetCustNameTextBox();

        //    if (txtName != null)
        //    {
        //        hdnNameFilter.Value = txtName.Text.Trim();
        //        this.BindGrid();
        //    }
        //}

        //private TextBox GetCustNameTextBox()
        //{
        //    TextBox txt = new TextBox();
        //    if (gvCustomers.HeaderRow != null)
        //    {
        //        if ((TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch") != null)
        //        {
        //            txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch");
        //        }
        //    }
        //    else
        //        txt = null;

        //    return txt;
        //}

        protected void ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblLoginId = e.Row.FindControl("lblLoginId") as Label;
            //    LinkButton lblGenerateLogin = e.Row.FindControl("lnkGenerateLogin") as LinkButton;
            //    LinkButton lnkResetPassword = e.Row.FindControl("lnkResetPassword") as LinkButton;
            //    CheckBox chkBox = e.Row.FindControl("chkId") as CheckBox;
            //    if (!string.IsNullOrEmpty(lblLoginId.Text.Trim()))
            //    {
            //        lblGenerateLogin.Visible = false;

            //    }
            //    else
            //    {
            //        lblLoginId.Visible = false;
            //        chkBox.Enabled = false;
            //        lnkResetPassword.Visible = false;
            //    }
            if(e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Label lblLoginId = (Label)item.FindControl("lblLoginId");
                LinkButton lblGenerateLogin = (LinkButton)item.FindControl("lnkGenerateLogin");
                //LinkButton lblGenerateLogin=e.Item.FindControl("lnkGenerateLogin") as LinkButton;
                //LinkButton lnkResetPassword =e.Item.FindControl("lnkResetPassword") as LinkButton;
                LinkButton lnkResetPassword = (LinkButton)item.FindControl("lnkResetPassword");
                CheckBox chkBox =e.Item.FindControl("cbRecons") as CheckBox;
                if (!string.IsNullOrEmpty(lblLoginId.Text.Trim()))
                {
                    lblGenerateLogin.Visible = false;

                }
                else
                {
                    lblLoginId.Visible = false;
                    chkBox.Enabled = false;
                    lnkResetPassword.Visible = false;
                }
            }
            }
        protected void gvCustomer_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtCustomer = new DataTable();
            dtCustomer = (DataTable)Cache["Customerdetails" + advisorVo.advisorId];
            gvCustomer.DataSource = dtCustomer;

        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCustomer.ExportSettings.OpenInNewWindow = true;
            gvCustomer.ExportSettings.IgnorePaging = true;
            gvCustomer.ExportSettings.HideStructureColumns = true;
            gvCustomer.ExportSettings.ExportOnlyData = true;
            gvCustomer.ExportSettings.FileName = "CustomerUserDetails";
            gvCustomer.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCustomer.MasterTableView.ExportToExcel();
        }

        }
    }

