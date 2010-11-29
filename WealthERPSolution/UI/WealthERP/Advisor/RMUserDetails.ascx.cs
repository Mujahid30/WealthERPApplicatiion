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

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
           
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
                FunctionInfo.Add("Method", "RMUserDeatils.ascx.cs:OnInit()");
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
                this.showRMUserDetails();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMUserDeatils.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];

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
                if (hdnRecordCount.Value != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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

        public void showRMUserDetails()
        {
            try
            {
                advisorVo = (AdvisorVo)Session["AdvisorVo"];

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

                rmUserList = advisorStaffBo.GetRMList(advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value.Trim(), out count,hdnNameFilter.Value.Trim());

                lblTotalRows.Text = hdnRecordCount.Value = count.ToString();

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
                        
                        drRMUsers[2] = userVo.LoginId.ToString();
                        
                        drRMUsers[3] = userVo.Email;
                        drRMUsers[4] = userVo.UserId;

                        dtRMUsers.Rows.Add(drRMUsers);
                    }

                    gvRMUsers.DataSource = dtRMUsers;
                   
                    gvRMUsers.DataBind();

                    if (trPagger.Visible == false)
                        trPagger.Visible = true;

                    this.GetPageCount();

                    if (btnGenerate.Visible == false)
                        btnGenerate.Visible = true;
                    if (mypager.Visible == false)
                        mypager.Visible = true;
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
                    mypager.Visible = false;
                   
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

        protected void gvRMUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortGridViewRMUserDetails(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortGridViewRMUserDetails(sortExpression, ASCENDING);
            }

        }

        private void sortGridViewRMUserDetails(string sortExpression, string direction)
        {
            try
            {
                advisorVo = (AdvisorVo)Session["AdvisorVo"];


                rmUserList = advisorStaffBo.GetRMList(advisorVo.advisorId);
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
                    drRMUsers[2] = userVo.LoginId.ToString();
                    drRMUsers[3] = userVo.Email;
                    drRMUsers[4] = userVo.UserId;
                    dtRMUsers.Rows.Add(drRMUsers);
                }
                DataView dv = new DataView(dtRMUsers);
                dv.Sort = sortExpression + direction;
                gvRMUsers.DataSource = dv;
                gvRMUsers.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMUserDetails.ascx:sortGridViewRMUserDetails()");


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

        protected void gvRMUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Random r = new Random();
            
            bool isSuccess = false;

            if (e.CommandName == "resetPassword")
            {

                userVo = userBo.GetUserDetails(Convert.ToInt32(gvRMUsers.DataKeys[int.Parse(e.CommandArgument.ToString())].Value));
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
                    ErrorMessage.InnerText= "An error occurred while reseting password.";

                }
            }
            else if (e.CommandName == "ViewDetails")
            {
                string userId = e.CommandArgument.ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int selectedRecords = 0;
            string statusMessage = string.Empty;
            advisorVo=(AdvisorVo)Session["advisorVo"];
            
            if (Page.IsValid)
            {
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "$.colorbox({width: '700px', overlayClose: false, inline: true, href: '#LoadImage'});", true);
                try
                {
                    foreach (GridViewRow gvr in gvRMUsers.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkBoxChild")).Checked == true)
                        {
                            selectedRecords++;

                            userId = int.Parse(gvRMUsers.DataKeys[gvr.RowIndex].Value.ToString());

                            Emailer emailer = new Emailer();
                            EmailMessage email = new EmailMessage();

                            userVo = userBo.GetUserDetails(userId);
                            string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                            email.GetAdviserRMAccountMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), userName);
                            //email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
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
                                    email.From = new MailAddress(emailer.smtpUserName, "WealthERP");
                                }
                            }
                            bool isMailSent = emailer.SendMail(email);

                            if (isMailSent)
                            {
                                statusMessage = "Credentials have been sent to selected user" ;
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

        protected void btnNameSearch_Click(object sender, EventArgs e)
        {
            TextBox txtName = GetRMNameTextBox();

            if (txtName != null)
            {
                hdnNameFilter.Value = txtName.Text.Trim();
                this.showRMUserDetails();
            }
        }

        private TextBox GetRMNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvRMUsers.HeaderRow != null)
            {
                if ((TextBox)gvRMUsers.HeaderRow.FindControl("txtRMNameSearch") != null)
                {
                    txt = (TextBox)gvRMUsers.HeaderRow.FindControl("txtRMNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }
    }
}
