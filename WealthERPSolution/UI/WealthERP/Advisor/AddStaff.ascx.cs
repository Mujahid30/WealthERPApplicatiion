using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using VoAdvisorProfiling;
using BoUser;
using BoCommon;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using System.Net.Mail;
using PCGMailLib;
using System.IO;
using WealthERP.Base;

namespace WealthERP.Advisor
{
    public partial class AddStaff : System.Web.UI.UserControl
    {
        AdvisorBranchVo advisorBranchVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo tempUserVo = new UserVo();
        UserVo rmUserVo;
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        RMVo rmStaffVo;
        OneWayEncryption encryption = new OneWayEncryption();

        List<AdvisorBranchVo> advisorBranchList = null;
        List<int> rmIds;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["RMVo"];
            string userType = string.Empty;
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "admin";
            if (!Page.IsPostBack)
            {
               
                BindTeamDropList();
                BindBranchDropList(userType);
                ControlInitialState();
            }

        }

        private RMVo CollectAdviserStaffData()
        {
            rmStaffVo = new RMVo();
            rmStaffVo.FirstName = txtFirstName.Text;
            if (!string.IsNullOrEmpty(txtMiddleName.Text.Trim()))
                rmStaffVo.MiddleName = txtMiddleName.Text;
            if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                rmStaffVo.LastName = txtLastName.Text;

            if (!string.IsNullOrEmpty(txtStaffcode.Text.Trim()))
                rmStaffVo.StaffCode = txtStaffcode.Text;
            if (ddlBranch.SelectedIndex != 0 || ddlBranch.SelectedIndex != -1)
                rmStaffVo.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);

            rmStaffVo.HierarchyRoleId = Convert.ToInt32(ddlRportingRole.SelectedValue);
            rmStaffVo.ReportingManagerId = Convert.ToInt32(ddlReportingMgr.SelectedValue);
            rmVo.MiddleName = txtMiddleName.Text.ToString();
            rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
            if (!string.IsNullOrEmpty(txtPhDirectISD.Text.Trim()))
            rmVo.OfficePhoneDirectIsd = int.Parse(txtPhDirectISD.Text.ToString());
            if (!string.IsNullOrEmpty(txtPhDirectPhoneNumber.Text.Trim()))
            rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

            if (!string.IsNullOrEmpty(txtPhExtISD.Text.Trim()))
                rmStaffVo.OfficePhoneExtIsd = int.Parse(txtPhExtISD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhExtPhoneNumber.Text.Trim()))
                rmStaffVo.OfficePhoneExtNumber = int.Parse(txtPhExtPhoneNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtExtSTD.Text.Trim()))
                rmStaffVo.OfficePhoneExtStd = int.Parse(txtExtSTD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhResiISD.Text.Trim()))
                rmStaffVo.ResPhoneIsd = int.Parse(txtPhResiISD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhResiPhoneNumber.Text.Trim()))
                rmStaffVo.ResPhoneNumber = int.Parse(txtPhResiPhoneNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtResiSTD.Text.Trim()))
                rmStaffVo.ResPhoneStd = int.Parse(txtResiSTD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhDirectSTD.Text.Trim()))
                rmStaffVo.OfficePhoneDirectStd = int.Parse(txtPhDirectSTD.Text.Trim());
            if (!string.IsNullOrEmpty(txtFaxNumber.Text.Trim()))
                rmStaffVo.Fax = int.Parse(txtFaxNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtFaxNumber.Text.Trim()))
                rmStaffVo.Fax = int.Parse(txtFaxNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtFaxISD.Text.Trim()))
                rmStaffVo.FaxIsd = int.Parse(txtFaxISD.Text.Trim());
            if (!string.IsNullOrEmpty(txtExtSTD.Text.Trim()))
                rmStaffVo.FaxStd = int.Parse(txtExtSTD.Text.Trim());

            rmStaffVo.AdviserId = advisorVo.advisorId;
            rmStaffVo.IsAssociateUser = true;

            return rmStaffVo;

        }

        private UserVo CollectAdviserStaffUserData()
        {
            rmUserVo = new UserVo();
            Random id = new Random();
            AdvisorBo advisorBo = new AdvisorBo();

            string password = id.Next(10000, 99999).ToString();

            //rmUserVo.UserType = ddlRMRole.SelectedItem.Text.ToString().Trim();
            rmUserVo.Password = password;
            rmUserVo.MiddleName = txtMiddleName.Text.ToString();
            rmUserVo.LoginId = txtEmail.Text.ToString();
            rmUserVo.LastName = txtLastName.Text.ToString();
            rmUserVo.FirstName = txtFirstName.Text.ToString();
            rmUserVo.Email = txtEmail.Text.ToString();

            rmVo.Email = txtEmail.Text.ToString();


            rmVo.FirstName = txtFirstName.Text.ToString();
            rmVo.LastName = txtLastName.Text.ToString();

            return rmUserVo;

        }

        private void BindBranchDropList(string userRole)
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = advisorStaffBo.GetAdviserBranchList(advisorVo.advisorId, userRole);
            ddlBranch.DataSource = dsAdviserBranchList;
            ddlBranch.DataValueField = dsAdviserBranchList.Tables[0].Columns["AB_BranchId"].ToString();
            ddlBranch.DataTextField = dsAdviserBranchList.Tables[0].Columns["AB_BranchName"].ToString();
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindTeamDropList()
        {
            DataTable dtAdviserTeamList = new DataTable();
            dtAdviserTeamList = advisorStaffBo.GetAdviserTeamList();
            ddlTeamList.DataSource = dtAdviserTeamList;
            ddlTeamList.DataValueField = dtAdviserTeamList.Columns["WHLM_Id"].ToString();
            ddlTeamList.DataTextField = dtAdviserTeamList.Columns["WHLM_Name"].ToString();
            ddlTeamList.DataBind();
            ddlTeamList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        private void BindTeamTitleDropList(int teamId)
        {
            DataTable dtAdviserTeamTitleList = new DataTable();
            if (teamId != 0)
            {
                dtAdviserTeamTitleList = advisorStaffBo.GetAdviserTeamTitleList(teamId, advisorVo.advisorId);
                ddlTitleList.DataSource = dtAdviserTeamTitleList;
                ddlTitleList.DataValueField = dtAdviserTeamTitleList.Columns["AH_TitleId"].ToString();
                ddlTitleList.DataTextField = dtAdviserTeamTitleList.Columns["AH_HierarchyName"].ToString();
                ddlTitleList.DataBind();
            }
            else
            {
                ClearDropList(ddlTitleList);
                ClearDropList(ddlRportingRole);
                ClearDropList(ddlReportingMgr);
            }

            ddlTitleList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void BindTitleApplicableLevelAndChannel(int titleId)
        {

            DataSet dsAdviserTitleChannelRole = new DataSet();
            if (titleId != 0)
            {
                dsAdviserTitleChannelRole = advisorStaffBo.GetAdviserTitleReportingLevel(titleId, advisorVo.advisorId);
                ddlRportingRole.DataSource = dsAdviserTitleChannelRole.Tables[1];
                ddlRportingRole.DataValueField = dsAdviserTitleChannelRole.Tables[1].Columns["AH_Id"].ToString();
                ddlRportingRole.DataTextField = dsAdviserTitleChannelRole.Tables[1].Columns["AH_HierarchyName"].ToString();
                ddlRportingRole.DataBind();
            }
            else
            {
                ClearDropList(ddlRportingRole);
                ClearDropList(ddlReportingMgr);
            }
            ddlRportingRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            if (titleId != 0)
            {
                ddlChannel.Enabled = true;
                ddlChannel.DataSource = dsAdviserTitleChannelRole.Tables[0];
                ddlChannel.DataValueField = dsAdviserTitleChannelRole.Tables[0].Columns["AH_ChannelId"].ToString();
                ddlChannel.DataTextField = dsAdviserTitleChannelRole.Tables[0].Columns["AH_ChannelName"].ToString();
                ddlChannel.DataBind();
                ddlChannel.Enabled = false;
            }
            else
            {
                ClearDropList(ddlChannel);
            }

        }

        private void BindReportingManager(int roleId)
        {
            DataTable dtReportingManagerList = new DataTable();
            if (roleId != 0)
            {
                dtReportingManagerList = advisorStaffBo.GetAdviserReportingManagerList(roleId, advisorVo.advisorId);
                ddlReportingMgr.DataSource = dtReportingManagerList;
                ddlReportingMgr.DataValueField = dtReportingManagerList.Columns["AR_RMId"].ToString();
                ddlReportingMgr.DataTextField = dtReportingManagerList.Columns["AR_RMName"].ToString();
                ddlReportingMgr.DataBind();
            }
            else
            {
                ClearDropList(ddlReportingMgr);
            }

            ddlReportingMgr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        private void ClearDropList(DropDownList ddlList)
        {
            ddlList.DataSource = null;
            ddlList.Items.Clear();
            ddlList.DataBind();
        }

        protected void ddlTitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTitleList.SelectedIndex != -1)
            {
                BindTitleApplicableLevelAndChannel(Convert.ToInt32(ddlTitleList.SelectedValue));
            }

        }

        protected void ddlTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTeamList.SelectedIndex != -1)
            {
                BindTeamTitleDropList(Convert.ToInt32(ddlTeamList.SelectedValue));
            }

        }

        protected void ddlRportingRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRportingRole.SelectedIndex != -1)
            {
                BindReportingManager(Convert.ToInt32(ddlRportingRole.SelectedValue));
            }

        }

        private void ControlViewEditMode(bool isViewMode)
        {
            if (isViewMode)
            {
                txtFirstName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;
                txtStaffcode.Enabled = false;

                trTeamTitle.Visible = false;

                ddlBranch.Enabled = false;
                ddlChannel.Enabled = false;
                ddlRportingRole.Enabled = false;
                ddlReportingMgr.Enabled = false;

                txtMobileNumber.Enabled = false;
                txtMobileNumber.Enabled = false;

                txtPhDirectISD.Enabled = false;
                txtPhDirectPhoneNumber.Enabled = false;
                txtPhExtISD.Enabled = false;
                txtPhExtPhoneNumber.Enabled = false;
                txtExtSTD.Enabled = false;
                txtPhResiISD.Enabled = false;
                txtPhResiPhoneNumber.Enabled = false;
                txtResiSTD.Enabled = false;
                txtPhDirectSTD.Enabled = false;
                txtFaxNumber.Enabled = false;
                txtFaxNumber.Enabled = false;
                txtFaxISD.Enabled = false;
                txtExtSTD.Enabled = false;

                btnSubmit.Visible = false;
                btnUpdate.Visible = false;

                lnkAddNewStaff.Visible = false;
                lnkEditStaff.Visible = true;
            }
            else
            {
                txtFirstName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Enabled = true;
                txtStaffcode.Enabled = true;

                trTeamTitle.Visible = true;

                ddlBranch.Enabled = true;
                ddlChannel.Enabled = false;
                ddlRportingRole.Enabled = true;
                ddlReportingMgr.Enabled = true;

                txtMobileNumber.Enabled = true;
                txtMobileNumber.Enabled = true;

                txtPhDirectISD.Enabled = true;
                txtPhDirectPhoneNumber.Enabled = true;
                txtPhExtISD.Enabled = true;
                txtPhExtPhoneNumber.Enabled = true;
                txtExtSTD.Enabled = true;
                txtPhResiISD.Enabled = true;
                txtPhResiPhoneNumber.Enabled = true;
                txtResiSTD.Enabled = true;
                txtPhDirectSTD.Enabled = true;
                txtFaxNumber.Enabled = true;
                txtFaxNumber.Enabled = true;
                txtFaxISD.Enabled = true;
                txtExtSTD.Enabled = true;

                btnSubmit.Visible = false;
                btnUpdate.Visible = true;

                lnkAddNewStaff.Visible = true;
                lnkEditStaff.Visible = false;

            }

        }

        protected void lnkEditStaff_Click(object sender, EventArgs e)
        {
            ControlViewEditMode(false);
            trSuccessMsg.Visible = false;
        }

        protected void lnkAddNewStaff_Click(object sender, EventArgs e)
        {
            ControlViewEditMode(false);
            ControlInitialState();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            rmStaffVo = CollectAdviserStaffData();
            rmUserVo = CollectAdviserStaffUserData();
            rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmStaffVo, userVo.UserId, false, false);
            hidRMid.Value = rmIds[0].ToString();
            userBo.CreateRoleAssociation(rmIds[1], 1009);
            ControlViewEditMode(true);
            divMsgSuccess.InnerText = " Staff Added Sucessfully";
            trSuccessMsg.Visible = true;
           
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            rmStaffVo = CollectAdviserStaffData();
            rmUserVo = CollectAdviserStaffUserData();
            rmStaffVo.RMId = Convert.ToInt32(hidRMid.Value);
            userBo.UpdateUser(rmUserVo);
            advisorStaffBo.UpdateStaff(rmStaffVo);
            ControlViewEditMode(true);
            divMsgSuccess.InnerText = "Staff updated Sucessfully";
            trSuccessMsg.Visible = true;
        }

        protected void ControlInitialState()
        {
            lnkAddNewStaff.Visible = true;
            lnkEditStaff.Visible = false;
            btnUpdate.Visible = false;
            btnSubmit.Visible = true;

            ClearDropList(ddlTitleList);
            ddlTitleList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            ClearDropList(ddlRportingRole);
            ddlRportingRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            ClearDropList(ddlReportingMgr);
            ddlReportingMgr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            ClearDropList(ddlChannel);

            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtStaffcode.Text = string.Empty;

            trTeamTitle.Visible = true;

            ddlBranch.SelectedIndex = 0;
            ddlTeamList.SelectedIndex = 0;
            //ddlChannel.SelectedIndex = 0;
            //ddlRportingRole.SelectedIndex = 0;
            //ddlReportingMgr.SelectedIndex = 0;

            txtMobileNumber.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;

            txtPhDirectISD.Text = string.Empty;
            txtPhDirectPhoneNumber.Text = string.Empty;
            txtPhExtISD.Text = string.Empty;
            txtPhExtPhoneNumber.Text = string.Empty;
            txtExtSTD.Text = string.Empty;
            txtPhResiISD.Text = string.Empty;
            txtPhResiPhoneNumber.Text = string.Empty;
            txtResiSTD.Text = string.Empty;
            txtPhDirectSTD.Text = string.Empty;
            txtFaxNumber.Text = string.Empty;
            txtFaxNumber.Text = string.Empty;
            txtFaxISD.Text = string.Empty;
            txtExtSTD.Text = string.Empty;
            ddlChannel.Enabled = false;

        }
    }
}