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

namespace WealthERP.Advisor
{
    public partial class RMCustomerUserDeatils : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBo advisorBo = new AdvisorBo();
        int customerId;
        int userId;
        UserVo userVo = new UserVo();
        UserVo advisorUserVo = new UserVo();
        UserBo userBo = new UserBo();
        Random r = new Random();
        PcgMailMessage email = new PcgMailMessage();
        string statusMessage = string.Empty;

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
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    this.BindGrid();
                }
                advisorUserVo = (UserVo)Session[SessionContents.UserVo];
                lblStatusMsg.Text = string.Empty;
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
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerUserDeatils.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerUserDeatils.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[1];
                objects[0] = mypager.CurrentPage;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
                ratio = rowCount / 20;
                mypager.PageCount = rowCount % 20 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 20) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 20).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMCustomerUserDetails.ascx.cs:GetPageCount()");

                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[0] = rowCount;
                objects[0] = ratio;
                objects[0] = lowerlimit;
                objects[0] = PageRecords;
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
                AdvisorVo advisorVo = new AdvisorVo();
                advisorVo = (AdvisorVo)Session["advisorVo"];

                int Count = 0;

                customerUserList = advisorBo.GetAdviserCustomerList(advisorVo.advisorId, mypager.CurrentPage, out Count, "", hdnNameFilter.Value.Trim(), "", "", "", "", out genDictParent, out genDictRM, out genDicReassigntRM);
                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                if (customerUserList != null)
                {
                    lblMsg.Visible = false;
                    dtRMCustomer.Columns.Add("UserId");
                    dtRMCustomer.Columns.Add("CustomerName");
                    dtRMCustomer.Columns.Add("Login Id");
                    dtRMCustomer.Columns.Add("Email Id");
                    dtRMCustomer.Columns.Add("Password");

                    DataRow drRMCustomerUser;

                    for (int i = 0; i < customerUserList.Count; i++)
                    {
                        drRMCustomerUser = dtRMCustomer.NewRow();
                        customerVo = new CustomerVo();
                        customerVo = customerUserList[i];
                        userId = customerVo.UserId;
                        userVo = new UserVo();
                        userVo = userBo.GetUserDetails(userId);

                        drRMCustomerUser[0] = userVo.UserId.ToString();
                        drRMCustomerUser[1] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        drRMCustomerUser[2] = userVo.LoginId.ToString();
                        drRMCustomerUser[3] = userVo.Email.ToString();
                        drRMCustomerUser[4] = userVo.Password.ToString();
                        dtRMCustomer.Rows.Add(drRMCustomerUser);
                    }
                    gvCustomers.DataSource = dtRMCustomer;
                    gvCustomers.DataBind();

                    //TextBox txtName = new TextBox();
                    //if (gvCustomers.HeaderRow != null)
                    //{
                    //    if ((TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch") != null)
                    //    {
                    //        txtName = (TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch");
                    //        txtName.Text = hdnNameFilter.Value.Trim();
                    //    }
                    //}
                    //else
                    //    txtName = null;
                    
                    this.GetPageCount();
                }
                else
                {
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    //tblPager.Visible = false;
                    lblMsg.Visible = true;
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
            try
            {
                foreach (GridViewRow gvr in this.gvCustomers.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        Count = Count + 1;
                    }
                }
                if (Count == 0)
                {
                    //lblMailSent.Text = "Please select the Customer..!";
                    lblStatusMsg.Text = "Please select the Customer";
                    //lblMailSent.Visible = true;
                }
                else
                {
                    foreach (GridViewRow gvr in this.gvCustomers.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {
                            string password = r.Next(20000, 100000).ToString();
                            userId = int.Parse(gvCustomers.DataKeys[gvr.RowIndex].Value.ToString());
                            userVo = new UserVo();
                            userVo = userBo.GetUserDetails(userId);

                            userVo.LoginId = r.Next(10000000, 99999999).ToString();
                            userVo.Password = Encryption.Encrypt(password);
                            userVo.IsTempPassword = 1;

                            userBo.UpdateUser(userVo);

                            //Send email to customer
                            //
                            //string EmailPath = Server.MapPath(ConfigurationManager.AppSettings["EmailPath"].ToString());
                            //email.SendCustomerLoginPassMail(advisorUserVo.Email, userVo.Email, advisorUserVo.LastName, userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName, userVo.LoginId, password, EmailPath);
                            SendMail(userVo);

                        }
                    }
                    lblStatusMsg.Text = statusMessage;
                    //lblMailSent.Visible = true;
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

        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();

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
                if (userVo.Email != string.Empty)
                {
                    email.To.Add(userVo.Email);
                    string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                    email.GetCustomerAccountMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), name);

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
                            email.From = new MailAddress(emailer.smtpUserName, "MoneyTouch");
                        }
                    }
                    //Sending mail...
                    isMailSent = emailer.SendMail(email);
                }
                else
                    isEmailIdBlank = true;
                if (isEmailIdBlank)
                    statusMessage += "<br/>No email Id specified for " + userVo.FirstName + " " + userVo.LastName;
                else if (isMailSent)
                {
                    statusMessage += "<br/>Credentials have been sent to " + userVo.Email;
                }
                else
                {
                    statusMessage += "<br/>An error occurred while sending mail to " + userVo.Email;

                }

            }
            catch (Exception ex)
            {

            }
            return isMailSent;
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Random r = new Random();

            bool isSuccess = false;

            if (e.CommandName == "resetPassword")
            {
                int userId = int.Parse(gvCustomers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
                userVo = userBo.GetUserDetails(userId);
                if (userVo != null)
                {
                    userVo.Password = r.Next(20000, 100000).ToString();
                    userVo.IsTempPassword = 1;
                    userVo.Password = Encryption.Encrypt(userVo.Password);
                    isSuccess = userBo.UpdateUser(userVo);
                }
                if (isSuccess)
                {
                    lblStatusMsg.Text = "Password has been reset.";
                }
                else
                {
                    lblStatusMsg.Text = "An error occurred while reseting password.";

                }
            }
            else if (e.CommandName == "ViewDetails")
            {
                string userId = e.CommandArgument.ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);
            }
        }

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetCustNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.BindGrid();
            }
        }

        private TextBox GetCustNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvCustomers.HeaderRow != null)
            {
                if ((TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch") != null)
                {
                    txt = (TextBox)gvCustomers.HeaderRow.FindControl("txtCustNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }
    }
}