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
using VOAssociates;
using BOAssociates;
using WealthERP.Base;
using DanLudwig;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using BoCustomerProfiling;

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
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        List<AdvisorBranchVo> advisorBranchList = null;
        List<int> rmIds;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();
        String userType;
        string agentCode = string.Empty;
        int agentId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["RMVo"];
            string userType = string.Empty;
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "admin";
            else
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                    userType = "associates";
            //int rmId=0;
            associatesVo = (AssociatesVO)Session["associatesVo"];
            HdnAdviserId.Value = advisorVo.advisorId.ToString();
            string action = string.Empty;

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["RmId"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["action"]))
                    {
                        hidRMid.Value = Request.QueryString["RmId"];
                        action = Request.QueryString["action"].ToString();
                    }
                    else
                    {
                        hidRMid.Value = Request.QueryString["RmId"];

                    }
                }
                BindTeamDropList();
                BinddepartDropList(advisorVo.advisorId);
                BindBranchDropList(userType);
                BindStaffBranchDrop(userType);
                if (!string.IsNullOrEmpty(hidRMid.Value.ToString()) && !string.IsNullOrEmpty(action))
                {
                    ShowRMDetails(Convert.ToInt32(hidRMid.Value));
                    if (action == "View")
                    {
                        ControlViewEditMode(true);
                    }
                    else if (action == "Edit")
                    {
                        ControlViewEditMode(false);
                    }
                }
                else
                {
                    ControlInitialState();
                    if (Request.QueryString["RmId"] != null)
                    {
                        ddlTeamList.SelectedValue = "13";
                        ddlTeamList.Enabled = false;
                        BindTeamTitleDropList(Convert.ToInt32(ddlTeamList.SelectedValue));
                        BindTitleApplicableLevelAndChannel(Convert.ToInt32(ddlTitleList.SelectedValue));
                        PLCustomer.Visible = true;


                    }
                }
            }

            if (userVo.UserType != "Advisor") lnkEditStaff.Visible = false;
        }

        private RMVo CollectAdviserStaffData()
        {

            rmStaffVo = new RMVo();
            string AllBranchId = string.Empty;
            rmStaffVo.FirstName = txtFirstName.Text;
            if (!string.IsNullOrEmpty(txtMiddleName.Text.Trim()))
                rmStaffVo.MiddleName = txtMiddleName.Text;
            if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                rmStaffVo.LastName = txtLastName.Text;

            if (!string.IsNullOrEmpty(txtStaffcode.Text.Trim()))
                rmStaffVo.StaffCode = txtStaffcode.Text;
            if (ddlBranch.SelectedIndex != 0 || ddlBranch.SelectedIndex != -1)
                rmStaffVo.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);

            rmStaffVo.HierarchyRoleId = Convert.ToInt32(ddlTitleList.SelectedValue);
            rmStaffVo.ReportingManagerId = Convert.ToInt32(ddlReportingMgr.SelectedValue);
            rmStaffVo.MiddleName = txtMiddleName.Text.ToString();
            rmStaffVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
            rmStaffVo.Email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(txtPhDirectISD.Text.Trim()))
                rmStaffVo.OfficePhoneDirectIsd = txtPhDirectISD.Text.ToString();
            if (!string.IsNullOrEmpty(txtPhDirectPhoneNumber.Text.Trim()))
                rmStaffVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

            if (!string.IsNullOrEmpty(txtPhExtISD.Text.Trim()))
                rmStaffVo.OfficePhoneExtIsd = txtPhExtISD.Text.Trim();
            if (!string.IsNullOrEmpty(txtPhExtPhoneNumber.Text.Trim()))
                rmStaffVo.OfficePhoneExtNumber = int.Parse(txtPhExtPhoneNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtExtSTD.Text.Trim()))
                rmStaffVo.OfficePhoneExtStd = txtExtSTD.Text.Trim();
            if (!string.IsNullOrEmpty(txtPhResiISD.Text.Trim()))
                rmStaffVo.ResPhoneIsd = int.Parse(txtPhResiISD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhResiPhoneNumber.Text.Trim()))
                rmStaffVo.ResPhoneNumber = int.Parse(txtPhResiPhoneNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtResiSTD.Text.Trim()))
                rmStaffVo.ResPhoneStd = int.Parse(txtResiSTD.Text.Trim());
            if (!string.IsNullOrEmpty(txtPhDirectSTD.Text.Trim()))
                rmStaffVo.OfficePhoneDirectStd = txtPhDirectSTD.Text.Trim();
            if (!string.IsNullOrEmpty(txtFaxNumber.Text.Trim()))
                rmStaffVo.Fax = int.Parse(txtFaxNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtFaxNumber.Text.Trim()))
                rmStaffVo.Fax = int.Parse(txtFaxNumber.Text.Trim());
            if (!string.IsNullOrEmpty(txtFaxISD.Text.Trim()))
                rmStaffVo.FaxIsd = int.Parse(txtFaxISD.Text.Trim());
            if (!string.IsNullOrEmpty(txtExtSTD.Text.Trim()))
                rmStaffVo.FaxStd = int.Parse(txtExtSTD.Text.Trim());
            if (!string.IsNullOrEmpty(txtEUIN.Text.Trim()))
                rmStaffVo.EUIN = (txtEUIN.Text.Trim()).ToString();
            rmStaffVo.AdviserId = advisorVo.advisorId;

            if (!string.IsNullOrEmpty(hdnAgentCode.Value))
                rmStaffVo.AAC_AgentCode = hdnAgentCode.Value;
            else
                rmStaffVo.AAC_AgentCode = null;
            if (ddlTitleList.SelectedItem.Text.Trim().ToUpper() == "OPS")
                rmStaffVo.IsAssociateUser = false;
            else
                rmStaffVo.IsAssociateUser = true;

            if (ddlTeamList.SelectedItem.Text.Trim().ToUpper() == "SALES")
                rmStaffVo.IsBranchOps = Convert.ToInt16(chkIsBranchOps.Checked);
            else
                rmStaffVo.IsBranchOps = 0;

            if (ddlTeamList.SelectedValue.ToUpper() == "OPS")
            {
                AllBranchId = ddlBranch.SelectedValue;
            }
            else
            {
                foreach (RadListBoxItem ListItem in this.RadListBoxDestination.Items)
                {
                    AllBranchId = AllBranchId + ListItem.Value.ToString() + ",";
                }
            }
            rmStaffVo.StaffBranchAssociation = AllBranchId.TrimEnd(',');
            rmStaffVo.roleIds = GetDepartmentRoleIds();
            rmStaffVo.roleIds = rmStaffVo.roleIds.Remove(rmStaffVo.roleIds.Length - 1);
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
            rmUserVo.LoginId = txtStaffcode.Text.ToString();
            rmUserVo.LastName = txtLastName.Text.ToString();
            rmUserVo.FirstName = txtFirstName.Text.ToString();
            rmUserVo.Email = txtEmail.Text.ToString();

            rmVo.Email = txtEmail.Text.ToString();
            if (ddlTitleList.SelectedItem.Text.Trim().ToUpper() == "OPS")
                rmUserVo.UserType = "OPS";
            else
                rmUserVo.UserType = "Associates";

            rmVo.FirstName = txtFirstName.Text.ToString();
            rmVo.LastName = txtLastName.Text.ToString();

            return rmUserVo;

        }

        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingDepartmentRoles(int.Parse(ddlDepart.SelectedValue));
        }

        private void BindingDepartmentRoles(int departId)
        {
            PnlDepartRole.Visible = false;
            if (departId.ToString() != "0")
            {
                BindAdviserrole(departId);
                PnlDepartRole.Visible = true;
            }
        }
        private void BindAdviserrole(int departmentId)
        {
            DataTable dtBindAdvisor = new DataTable();
            dtBindAdvisor = advisorStaffBo.GetUserRoleDepartmentWise(departmentId, advisorVo.advisorId);
            chkbldepart.DataSource = dtBindAdvisor;
            chkbldepart.DataTextField = dtBindAdvisor.Columns["AR_Role"].ToString();
            chkbldepart.DataValueField = dtBindAdvisor.Columns["AR_RoleId"].ToString();
            chkbldepart.DataBind();
        }

        private bool Validation(string agentCode)
        {
            bool result = true;
            int adviserId = advisorVo.advisorId;
            try
            {
                if (associatesBo.CodeduplicateCheck(adviserId, agentCode))
                {
                    result = false;
                    //lblPanDuplicate.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('SubBroker Code already exists !!');", true);
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
                FunctionInfo.Add("Method", "AddStaff.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        private bool EmailValidation(string email)
        {
            bool result = true;
            int adviserId = advisorVo.advisorId;
            try
            {
                if (advisorStaffBo.EmailduplicateCheck(adviserId, email))
                {
                    result = false;
                    //lblPanDuplicate.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Email already exists !!');", true);
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
                FunctionInfo.Add("Method", "AddStaff.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        private void BindBranchDropList(string userRole)
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = advisorStaffBo.GetAdviserBranchList(advisorVo.advisorId, userRole);
            ddlBranch.DataSource = dsAdviserBranchList;
            ddlBranch.DataValueField = dsAdviserBranchList.Tables[0].Columns["AB_BranchId"].ToString();
            ddlBranch.DataTextField = dsAdviserBranchList.Tables[0].Columns["AB_BranchName"].ToString();
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        private void BindStaffBranchDrop(string userRole)
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = advisorStaffBo.GetAdviserBranchList(advisorVo.advisorId, userRole);
            LBStaffBranch.DataSource = dsAdviserBranchList;
            LBStaffBranch.DataValueField = dsAdviserBranchList.Tables[0].Columns["AB_BranchId"].ToString(); ;
            LBStaffBranch.DataTextField = dsAdviserBranchList.Tables[0].Columns["AB_BranchName"].ToString();
            LBStaffBranch.DataBind();

        }
        private void BindTeamDropList()
        {
            DataTable dtAdviserTeamList = new DataTable();
            dtAdviserTeamList = advisorStaffBo.GetAdviserTeamList(0);
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
                ddlTitleList.DataValueField = dtAdviserTeamTitleList.Columns["AH_Id"].ToString();
                ddlTitleList.DataTextField = dtAdviserTeamTitleList.Columns["AH_HierarchyName"].ToString();
                ddlTitleList.DataBind();

                hidMinHierarchyTitleId.Value = dtAdviserTeamTitleList.Compute("min(AH_Sequence)", string.Empty).ToString();
                Session["StaffTeamList"] = dtAdviserTeamTitleList;
            }
            else
            {
                ClearDropList(ddlTitleList);
                ClearDropList(ddlRportingRole);
                ClearDropList(ddlReportingMgr);
            }

            ddlTitleList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            if (Request.QueryString["RmId"] != null)
            {
                ddlTitleList.SelectedValue = "135";
                ddlTitleList.Enabled = false;
            }

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
                // ddlChannel.Enabled = false;
                if (Request.QueryString["RmId"] != null)
                {
                    ddlChannel.SelectedValue = "12";
                    ddlChannel.Enabled = false;
                }
            }
            else
            {
                ClearDropList(ddlChannel);
            }

        }

        private void BindReportingManager(int hierarchayRoleId)
        {
            DataTable dtReportingManagerList = new DataTable();
            if (hierarchayRoleId != 0)
            {
                dtReportingManagerList = advisorStaffBo.GetAdviserReportingManagerList(hierarchayRoleId, advisorVo.advisorId);
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
            AgentCodesvalidation();
            BindTitleApplicableLevelAndChannel(Convert.ToInt32(ddlTitleList.SelectedValue));

        }

        protected void AgentCodesvalidation()
        {

            if (ddlTitleList.SelectedIndex != -1)
            {
                if (ddlTitleList.SelectedItem.Text == "OPS")
                {
                    lblrg.Visible = false;

                }
                else
                {
                    lblrg.Visible = true;

                }


            }

        }


        protected void ddlTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTeamList.SelectedIndex != -1)
            {

                BindTeamTitleDropList(Convert.ToInt32(ddlTeamList.SelectedValue));
                if (ddlTeamList.SelectedValue == "14")
                {
                    tdLb1Branch1.Visible = true;
                    tdDdl1Branch1.Visible = true;
                    tr4.Visible = false;
                    PLCustomer.Visible = false;
                    chkIsBranchOps.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    lblAgentCodeL.Visible = false;
                    Span10.Visible = false;
                    txtAgentCode.Visible = false;
                    Span9.Visible = false;
                }
                else
                {
                    tr4.Visible = true;
                    PLCustomer.Visible = true;
                    tdLb1Branch1.Visible = false;
                    tdDdl1Branch1.Visible = false;
                    chkIsBranchOps.Visible = true;
                    RequiredFieldValidator7.Enabled = true;
                    lblAgentCodeL.Visible = true;
                    Span10.Visible = true;
                    txtAgentCode.Visible = true;
                    Span9.Visible = true;
                }

            }
        }

        protected void ddlRportingRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRportingRole.SelectedIndex != -1)
            {
                BindReportingManager(Convert.ToInt32(ddlRportingRole.SelectedValue));
            }

        }
        protected void ListBoxSource_Transferred(object source, Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        {

            LBStaffBranch.Items.Sort();

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
                txtEmail.Enabled = false;

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
                txtEUIN.Enabled = false;
                txtAgentCode.Enabled = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;

                LBStaffBranch.Enabled = false;
                lnkAddNewStaff.Visible = false;
                lnkEditStaff.Visible = true;
                RadListBoxDestination.Enabled = false;
                txtFaxSTD.Enabled = false;
                ddlDepart.Enabled = false;
                chkbldepart.Enabled = false;
                chkIsBranchOps.Enabled = false;
            }
            else
            {
                txtFirstName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Enabled = true;
                txtStaffcode.Enabled = true;

                LBStaffBranch.Enabled = true;
                trTeamTitle.Visible = true;
                RadListBoxDestination.Enabled = true;
                ddlBranch.Enabled = true;
                ddlChannel.Enabled = false;
                ddlTeamList.Enabled = false;
                ddlRportingRole.Enabled = true;
                ddlReportingMgr.Enabled = true;

                txtMobileNumber.Enabled = true;
                txtEmail.Enabled = true;

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
                txtEUIN.Enabled = true;
                txtAgentCode.Enabled = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;

                lnkAddNewStaff.Visible = true;
                lnkEditStaff.Visible = false;
                txtFaxSTD.Enabled = true;
                ddlDepart.Enabled = true;
                chkbldepart.Enabled = true;
                chkIsBranchOps.Enabled = true;
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

        private void BinddepartDropList(int adviserId)
        {
            CommonLookupBo commonLookup = new CommonLookupBo();
            DataSet dsDepartmentlist = new DataSet();
            dsDepartmentlist = commonLookup.GetDepartment(adviserId);
            ddlDepart.DataSource = dsDepartmentlist.Tables[0];
            ddlDepart.DataTextField = dsDepartmentlist.Tables[0].Columns["AD_DepartmentName"].ToString();
            ddlDepart.DataValueField = dsDepartmentlist.Tables[0].Columns["AD_DepartmentId"].ToString();
            ddlDepart.DataBind();
            ddlDepart.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CustomerBo customerbo = new CustomerBo();
            String RoleIds = GetDepartmentRoleIds();
            RoleIds = RoleIds.Remove(RoleIds.Length - 1);
            string theme = userVo.theme;
            string BranchId = string.Empty;
            if (txtStaffcode.Text != string.Empty)
            {
                int staffCodeDuplicatechec = customerbo.CheckStaffCode(txtStaffcode.Text);
                if (staffCodeDuplicatechec > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Unique Staffcode Code. You Can Use Combination of 0-9 and a-z');", true);
                    return;
                }
            }
            if (ddlTeamList.SelectedValue != "14")
            {
                foreach (RadListBoxItem ListItem in this.RadListBoxDestination.Items)
                {
                    BranchId = BranchId + ListItem.Value.ToString();
                }

                if (BranchId == string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Branch.');", true);
                    return;
                }
            }
            if (ValidateStaffReportingManager())
            {
                rmStaffVo = CollectAdviserStaffData();
                rmUserVo = CollectAdviserStaffUserData();
                if (Validation(hdnAgentCode.Value))//removed unique validation of Email-- && EmailValidation(txtEmail.Text)
                {
                    if (ddlDepart.SelectedItem.Text == "OPS")
                    {
                        rmStaffVo.IsExternal = 0;
                        hidRMid.Value = Convert.ToString(advisorStaffBo.CreateAdviserStaff(rmUserVo, rmStaffVo, userVo.UserId, ddlTitleList.SelectedItem.Text.Trim().ToUpper() == "OPS" ? true : false, false, RoleIds, theme));
                    }
                    else if (ddlDepart.SelectedItem.Text == "SALES")
                    {
                        rmStaffVo.IsExternal = 1;
                        rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmStaffVo, userVo.UserId, ddlTitleList.SelectedItem.Text.Trim().ToUpper() == "OPS" ? true : false, false, RoleIds);
                        hidRMid.Value = rmIds[1].ToString();
                    }
                    ControlViewEditMode(true);
                    divMsgSuccess.InnerText = " Staff Added Sucessfully";
                    trSuccessMsg.Visible = true;
                    if (Request.QueryString["RmId"] != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddStaff','none');", true);
                        Response.Write("<script>alert('Add Staff has been successfully added');</script>");
                        Response.Write("<script>window.close();</" + "script>");
                    }
                }

            }

        }

        public string GetDepartmentRoleIds()
        {
            string departmentRoleids = string.Empty;
            foreach (RadListBoxItem li in chkbldepart.Items)
            {
                if (li.Checked == true)
                    departmentRoleids += li.Value + ",";
            }
            return departmentRoleids;
        }

        public void SetDepartmentRoleIds(string roleIds)
        {

            string[] roleIs = roleIds.Split(',');
            if (roleIds == string.Empty)
                return;
            for (int i = 0; i < roleIs.Length; i++)
            {
                foreach (RadListBoxItem li in chkbldepart.Items)
                {
                    if (li.Value == roleIs[i])
                    {
                        li.Checked = true;
                    }


                }
            }

        }

        protected bool ValidateStaffReportingManager()
        {
            DataTable dtTeamList = new DataTable();
            bool validReportingMrg = true;
            if (Session["StaffTeamList"] != null)
            {
                dtTeamList = (DataTable)Session["StaffTeamList"];
            }

            int rowIndex = dtTeamList.Rows.IndexOf(dtTeamList.Select("AH_Id=" + ddlTitleList.SelectedValue.ToString())[0]);
            string strD = Convert.ToString(dtTeamList.Rows[rowIndex]["AH_Sequence"]);

            if (strD != hidMinHierarchyTitleId.Value.ToString() && (ddlReportingMgr.SelectedIndex == 0 || ddlRportingRole.SelectedIndex == 0))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Reporting Manager!');", true);
                validReportingMrg = false;
            }

            return validReportingMrg;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateStaffReportingManager())
            {
                rmStaffVo = CollectAdviserStaffData();
                rmUserVo = CollectAdviserStaffUserData();
                rmStaffVo.RMId = Convert.ToInt32(hidRMid.Value);
                String RoleIds = GetDepartmentRoleIds();
                RoleIds = RoleIds.Remove(RoleIds.Length - 1);
                //userBo.UpdateUser(rmUserVo);
                advisorStaffBo.UpdateStaff(rmStaffVo);
                ControlViewEditMode(true);
                divMsgSuccess.InnerText = "Staff updated Sucessfully";
                trSuccessMsg.Visible = true;
            }
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
            txtEmail.Text = string.Empty;
            txtEUIN.Text = string.Empty;
            ddlChannel.Enabled = false;

            chkIsBranchOps.Checked = false;
            //imgBtnReferesh.Enabled = false;
            //imgAddAgentCode.Enabled = false;

        }

        private void ShowRMDetails(int rmId)
        {
            rmStaffVo = advisorStaffBo.GetAdvisorStaffDetails(rmId);
            BindTeamTitleDropList(rmStaffVo.HierarchyTeamId);
            BindTitleApplicableLevelAndChannel(rmStaffVo.HierarchyTitleId);
            BindReportingManager(rmStaffVo.HierarchyRoleId);

            txtFirstName.Text = rmStaffVo.FirstName;
            txtMiddleName.Text = rmStaffVo.MiddleName;
            txtLastName.Text = rmStaffVo.LastName;
            txtStaffcode.Text = rmStaffVo.StaffCode;
            ddlBranch.SelectedValue = rmStaffVo.BranchId.ToString();


            ddlTeamList.SelectedValue = rmStaffVo.HierarchyTeamId.ToString();
            if (ddlTeamList.SelectedValue == "14")
            {
                tdLb1Branch1.Visible = true;
                tdDdl1Branch1.Visible = true;
                tr4.Visible = false;
                PLCustomer.Visible = false;
                chkIsBranchOps.Visible = false;
                RequiredFieldValidator7.Enabled = false;
                lblAgentCodeL.Visible = false;
                Span10.Visible = false;
                txtAgentCode.Visible = false;
                Span9.Visible = false;
            }
            else
            {
                tr4.Visible = true;
                PLCustomer.Visible = true;
                tdLb1Branch1.Visible = false;
                tdDdl1Branch1.Visible = false;
                chkIsBranchOps.Visible = true;
                RequiredFieldValidator7.Enabled = true;
                lblAgentCodeL.Visible = true;
                Span10.Visible = true;
                txtAgentCode.Visible = true;
                Span9.Visible = true;
            }
            ddlTitleList.SelectedValue = rmStaffVo.HierarchyTitleId.ToString();
            AgentCodesvalidation();
            ddlRportingRole.SelectedValue = rmStaffVo.HierarchyRoleId.ToString();
            ddlReportingMgr.SelectedValue = rmStaffVo.ReportingManagerId.ToString();
            chkIsBranchOps.Checked = rmStaffVo.IsBranchOps==1;

            txtMobileNumber.Text = rmStaffVo.Mobile.ToString();
            txtEmail.Text = rmStaffVo.Email.ToString();
            if (ddlTeamList.SelectedItem.ToString().ToUpper() == "OPS")
            {
                ddlDepart.SelectedIndex = 1;
            }
            else
            {
                ddlDepart.SelectedIndex = 2;
            }
            BindingDepartmentRoles(Convert.ToInt32(ddlDepart.SelectedValue));
            SetDepartmentRoleIds(rmStaffVo.roleIds);
            if (!string.IsNullOrEmpty(rmStaffVo.OfficePhoneDirectIsd) )
                txtPhDirectISD.Text = rmStaffVo.OfficePhoneDirectIsd.ToString();
            if (rmStaffVo.OfficePhoneDirectNumber != 0)
                txtPhDirectPhoneNumber.Text = rmStaffVo.OfficePhoneDirectNumber.ToString();
            if (! string.IsNullOrEmpty(rmStaffVo.OfficePhoneExtIsd))
                txtPhExtISD.Text = rmStaffVo.OfficePhoneExtIsd.ToString();
            if (rmStaffVo.OfficePhoneExtNumber != 0)
                txtPhExtPhoneNumber.Text = rmStaffVo.OfficePhoneExtNumber.ToString();
            if (!string.IsNullOrEmpty(rmStaffVo.OfficePhoneExtStd))
                txtExtSTD.Text = rmStaffVo.OfficePhoneExtStd.ToString();
            if (rmStaffVo.ResPhoneIsd != 0)
                txtPhResiISD.Text = rmStaffVo.ResPhoneIsd.ToString();
            if (rmStaffVo.ResPhoneNumber != 0)
                txtPhResiPhoneNumber.Text = rmStaffVo.ResPhoneNumber.ToString();
            if (rmStaffVo.ResPhoneStd != 0)
                txtResiSTD.Text = rmStaffVo.ResPhoneStd.ToString();
            if (!string.IsNullOrEmpty(rmStaffVo.OfficePhoneDirectStd))
                txtPhDirectSTD.Text = rmStaffVo.OfficePhoneDirectStd.ToString();
            if (rmStaffVo.Fax != 0)
                txtFaxNumber.Text = rmStaffVo.Fax.ToString();
            if (rmStaffVo.FaxIsd != 0)
                txtFaxISD.Text = rmStaffVo.FaxIsd.ToString();
            if (!string.IsNullOrEmpty(rmStaffVo.EUIN))
                txtEUIN.Text = rmStaffVo.EUIN.ToString();
            if (rmStaffVo.FaxStd != 0)
                txtExtSTD.Text = rmStaffVo.FaxStd.ToString();
            if (!string.IsNullOrEmpty(rmStaffVo.AAC_AgentCode))
            {
                hdnAgentCode.Value = rmStaffVo.AAC_AgentCode.ToString();
                //lblAgentCode.Text = rmStaffVo.AAC_AgentCode;
                //imgBtnReferesh.Visible = false;
                //imgAddAgentCode.Visible = false;
            }

            else
            {
                //lblAgentCode.Text = "N/A";
                //imgBtnReferesh.Enabled = true;
                //imgAddAgentCode.Enabled = true;
            }
            if (!string.IsNullOrEmpty(rmStaffVo.StaffBranchAssociation))
            {
                DataSet ds = advisorStaffBo.GetStaffBranchAssociation(rmStaffVo.StaffBranchAssociation, advisorVo.advisorId);
                RadListBoxDestination.DataSource = ds.Tables[0];
                RadListBoxDestination.DataValueField = ds.Tables[0].Columns["StaffBranch"].ToString();
                RadListBoxDestination.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                RadListBoxDestination.DataBind();
                LBStaffBranch.DataSource = ds.Tables[1];
                LBStaffBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                LBStaffBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                LBStaffBranch.DataBind();
            }

        }
        private string GetAllSelectedCustomerID(DanLudwig.Controls.Web.ListBox CustomerSelectedListBox)
        {
            String AllBranchId = "";
            // loop through all source items to find selected ones
            for (int i = CustomerSelectedListBox.Items.Count - 1; i >= 0; i--)
            {
                ListItem TempItem = CustomerSelectedListBox.Items[i];

                AllBranchId = AllBranchId + "," + TempItem.Value.ToString();


            }
            return AllBranchId;


        }
        protected void imgBtnReferesh_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hidRMid.Value))
            {
                ShowRMDetails(Convert.ToInt32(hidRMid.Value));
            }
        }
        //protected void imgAddAgentCode_OnClick(object sender, EventArgs e)
        //{
        //    string queryString = "?userType=RM&rmId=" + hidRMid.Value;
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "window.open('PopUp.aspx?PageId=AddBranchRMAgentAssociation"',+"'" + queryString + "'" + ", 'mywindow', 'width=750,height=500,scrollbars=yes,location=no');", true);
        //}

    }
}
