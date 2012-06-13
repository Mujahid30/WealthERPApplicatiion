using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using BoSuperAdmin;
using VoUser;
using BoUser;
using WealthERP.Base;
using BoCommon;
using Telerik.Web.UI;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;

namespace WealthERP.Advisor
{
    public partial class AdvisorLeftPane : UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdviserSubscriptionBo adviserSubscriptionBo = new AdviserSubscriptionBo();
        AdvisorVo advisorVo;
        DataSet dsTreeNodes;
        DataSet dsSubscriptionDetails;
        RMVo rmVo = new RMVo();
        UserBo userBo = new UserBo();
        UserVo userVo;
        int count;
        int noOfStaffWebLogins = 0;
        List<string> roleList = new List<string>();
        string logoPath = "";
        string sourcePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            Session["BranchAdd"] = "forRM";
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session[SessionContents.BranchLogoPath] != null)
                sourcePath = Session[SessionContents.BranchLogoPath].ToString();
            dsSubscriptionDetails = adviserSubscriptionBo.GetAdviserSubscriptionPlanDetails(advisorVo.advisorId);
            if (!IsPostBack)
            {
                if (Session["customerVo"] != null)
                    Session.Remove("customerVo");

                //Code to display and hide the searches based on the roles
                if (userVo.RoleList.Contains("Admin"))
                {
                    if (advisorVo.IsOpsEnable != 1)
                    {
                        txtFindRMCustomer.Visible = false;
                        btnSearchRMCustomer.Visible = false;
                    }
                    else
                    {
                        txtFindAdviserCustomer.Visible = false;
                        btnSearchAdviserCustomer.Visible = false;
                    }
                }
                else if (userVo.RoleList.Contains("RM"))
                {
                    txtFindRM.Visible = false;
                    btnSearchRM.Visible = false;
                    txtFindBranch.Visible = false;
                    btnSearchBranch.Visible = false;
                    txtFindAdviserCustomer.Visible = false;
                    btnSearchAdviserCustomer.Visible = false;
                }
                else if (userVo.RoleList.Contains("BM"))
                {
                    txtFindRM.Visible = false;
                    btnSearchRM.Visible = false;
                    txtFindBranch.Visible = false;
                    btnSearchBranch.Visible = false;
                    txtFindAdviserCustomer.Visible = false;
                    btnSearchAdviserCustomer.Visible = false;
                    txtFindRMCustomer.Visible = false;
                    btnSearchRMCustomer.Visible = false;
                }
                else if (userVo.RoleList.Contains("Ops"))
                {
                    txtFindRMCustomer.Visible = false;
                    txtFindBranch.Visible = false;
                    txtFindRM.Visible = false;
                    btnSearchRM.Visible = false;
                    btnSearchBranch.Visible = false;
                    btnSearchRMCustomer.Visible = false;
                }
                else if (userVo.RoleList.Contains("Research"))
                {
                    txtFindRM.Visible = false;
                    btnSearchRM.Visible = false;
                    txtFindBranch.Visible = false;
                    btnSearchBranch.Visible = false;
                    txtFindAdviserCustomer.Visible = false;
                    btnSearchAdviserCustomer.Visible = false;
                    txtFindRMCustomer.Visible = false;
                    btnSearchRMCustomer.Visible = false;
                }


                //Code to display the left tree based on the Roles
                if (!userVo.RoleList.Contains("Admin"))
                    RadPanelBar1.Visible = false;
                if (!userVo.RoleList.Contains("RM"))
                    RadPanelBar2.Visible = false;
                if (!userVo.RoleList.Contains("BM"))
                    RadPanelBar3.Visible = false;
                if (!userVo.RoleList.Contains("Ops"))
                    RadPanelBar4.Visible = false;
                if (!userVo.RoleList.Contains("Research"))
                    RadPanelBar5.Visible = false;

                //Code to unhide the tree nodes based on User Roles
                if (userVo.RoleList.Contains("Admin"))
                {
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles("Admin", "Admin");
                    SetAdminTreeNodesForRoles(dsTreeNodes, "Admin");
                }
                if (userVo.RoleList.Contains("RM"))
                {
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles("RM", "Admin");
                    SetAdminTreeNodesForRoles(dsTreeNodes, "RM");
                }
                if (userVo.RoleList.Contains("BM"))
                {
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles("BM", "Admin");
                    SetAdminTreeNodesForRoles(dsTreeNodes, "BM");
                }
                if (userVo.RoleList.Contains("Ops"))
                {
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles("Ops", "Admin");
                    SetAdminTreeNodesForRoles(dsTreeNodes, "Ops");
                }
                if (userVo.RoleList.Contains("Research"))
                {
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles("Research", "Admin");
                    SetAdminTreeNodesForRoles(dsTreeNodes, "Research");
                }


                //Code to unhide the tree nodes based on Plans
                dsTreeNodes = GetTreeNodesBasedOnPlans(advisorVo.advisorId, "Admin", "Admin");
                SetAdminTreeNodesForPlans(dsTreeNodes);

                if (advisorVo.MultiBranch != 1)
                {
                    RadPanelBar1.FindItemByValue("Branch/Association").Visible = false;
                }
                if (dsSubscriptionDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfStaffWebLogins"] != null)
                        noOfStaffWebLogins = int.Parse(dsSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfStaffWebLogins"].ToString());
                    if (noOfStaffWebLogins <= 1)
                    {
                        RadPanelBar1.FindItemByValue("Staff").Visible = false;
                        RadPanelBar3.FindItemByValue("Staff").Visible = false;
                    }
                }

                //Code to expand the home node based on the User Roles
                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    if (!userVo.RoleList.Contains("Ops"))
                    {
                        RadPanelBar1.FindItemByValue("Admin").Expanded = true;
                        if (Session["IsCustomerGrid"] == null)
                            RadPanelBar1.FindItemByValue("Admin Home").Selected = true;
                        else
                            RadPanelBar1.FindItemByValue("Customer").Selected = true;
                    }
                    else
                    {
                        RadPanelBar4.FindItemByValue("Ops").Expanded = true;
                        RadPanelBar4.FindItemByValue("Customer").Selected = true;
                    }

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    RadPanelBar3.FindItemByValue("BM").Expanded = true;

                    if (Session["IsCustomerGrid"] == null)
                        RadPanelBar3.FindItemByValue("BM Home").Selected = true;
                    else
                        RadPanelBar3.FindItemByValue("Customer").Selected = true;

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    RadPanelBar2.FindItemByValue("RM").Expanded = true;

                    if (Session["IsCustomerGrid"] == null)
                        RadPanelBar2.FindItemByValue("RM Home").Selected = true;
                    else
                        RadPanelBar2.FindItemByValue("Customer").Selected = true;

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);
                }
                else if (userVo.RoleList.Contains("Ops"))
                {
                    RadPanelBar4.FindItemByValue("Ops").Expanded = true;
                    RadPanelBar4.FindItemByValue("Customer").Selected = true;
                }
                else if (userVo.RoleList.Contains("Research"))
                {
                    RadPanelBar5.FindItemByValue("Reference_Data").Expanded = true;
                    RadPanelBar5.FindItemByValue("Reference_Data").Selected = true;
                }

                //
                // Code to display inbox/message links based on main role
                // 
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                {
                    /* Compose, Inbox, Outbox Visible only in admin pane */
                    RadPanelBar2.FindItemByValue("Message").Visible = false; // RM Bar
                    RadPanelBar3.FindItemByValue("Message").Visible = false; // BM Bar
                    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                {
                    /* Compose, Inbox, Outbox Visible only in bm pane */
                    RadPanelBar2.FindItemByValue("Message").Visible = false; // RM Bar
                    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                {
                    /* Inbox Visible only in rm pane */
                    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "research")
                {
                    /* Inbox Visible only in research pane */

                }               
                //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                //{
                //    /* None visible as of now */
                //}
                //else
                //{
                //    /* None visible for customer as of now */
                //}

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);

            }
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
        //    {
        //        SetNode();
        //    }
        //}

        protected void btnSearchRM_Click(object sender, EventArgs e)
        {

        }

        //public void SetNode()
        //{
        //    string strNodeValue = null;
        //    if (TreeView1.SelectedNode.Value == "Switch Roles")
        //    {
        //        logoPath = Session[SessionContents.LogoPath].ToString();
        //        Session[SessionContents.CurrentUserRole] = null;
        //        roleList = userBo.GetUserRoles(userVo.UserId);
        //        count = roleList.Count;
        //        if (count == 3)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);
        //            //ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loginloadcontrol('IFAAdminDashboard','login','" + UserName + "','" + sourcePath + "');", true);
        //        }
        //        else if (count == 2)
        //        {
        //            if (roleList.Contains("RM") && roleList.Contains("Admin"))
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);
        //            }
        //            else if (roleList.Contains("BM") && roleList.Contains("Admin"))
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorBMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);

        //            }
        //        }
        //        // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Advisor Home")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Profile")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Edit Profile")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditAdvisorProfile','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Edit User Details")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Branch")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "Add Branch")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranch','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value == "LOB")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Add LOB")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddLOB','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Staff")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Valuation")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('DailyValuation','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Add Staff")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Add Branch Association")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMBranchAssociation','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "RM Details")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "Customer Details")
        //    {
        //        //Session["Customer"] = "Customer";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Customer")
        //    {
        //        Session["Customer"] = "Customer";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Customer Accounts")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Association")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CuCustomerAssociationSetup','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "View Branch Association")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Uploads")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Process Log")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Rejected Records")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);
        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "Reject Folios")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedFoliosUploads','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Rejected Transactions")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTransactions','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "LoanMIS")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanMIS','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "SetupAssociateCategory")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);

        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "AdviserLoanCommsnStrucWithLoanPartner")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "AdviserLoanCommsnStrucWithLoanPartner")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "AdviserStaffSMTP")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);

        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "Schemes")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Add Schemes")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "Set Theme")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SetTheme','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "LeadManagement")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagement','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "LeadManagementAdd")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagementAdd','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MFReports")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);

        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "EquityReports")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','none');", true);
        //    }

        //    else if (TreeView1.SelectedNode.Value.ToString() == "CustomerSMSAlerts")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','none');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "SendSMS")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerManualSMS','none');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "CustomerMFSystematicTransactionReport")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MFReversal")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MFMIS")
        //    {
        //        Session["UserType"] = "rm";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "EView Transactions")
        //    {
        //        Session["UserType"] = "rm";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "CommissionMIS")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "EAdd Transactions")
        //    {
        //        Session["UserType"] = "rm";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
        //        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "EMIS")
        //    {
        //        Session["UserType"] = "rm";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MView Transactions")
        //    {

        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MMIS")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
        //    }
        //    else if (TreeView1.SelectedNode.Value.ToString() == "MAddTransactions")
        //    {
        //        Session["UserType"] = "adviser";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
        //    }
        //    if (TreeView1.SelectedNode.Parent == null)
        //    {
        //        foreach (TreeNode node in TreeView1.Nodes)
        //        {
        //            if (node.Value != TreeView1.SelectedNode.Value)
        //                node.Collapse();
        //            else
        //                node.Expand();
        //        }
        //    }
        //    else
        //    {
        //        if (TreeView1.SelectedNode.Parent.Parent != null)
        //        {
        //            string parentNode = TreeView1.SelectedNode.Parent.Parent.Value;
        //            foreach (TreeNode node in TreeView1.Nodes)
        //            {
        //                if (node.Value != parentNode)
        //                    node.Collapse();
        //            }
        //        }
        //        else
        //        {
        //            if (TreeView1.SelectedNode.Parent == null)
        //            {
        //                foreach (TreeNode node in TreeView1.Nodes)
        //                {
        //                    if (node.Value != TreeView1.SelectedNode.Value)
        //                        node.Collapse();
        //                    else
        //                        node.Expand();
        //                }
        //            }
        //            else
        //            {
        //                strNodeValue = TreeView1.SelectedNode.Parent.Value;
        //                string val = TreeView1.SelectedNode.Value;
        //                foreach (TreeNode node in TreeView1.Nodes)
        //                {

        //                    if (node.Value != strNodeValue)
        //                        node.Collapse();
        //                    else
        //                    {
        //                        foreach (TreeNode child in node.ChildNodes)
        //                        {
        //                            if (child.Value != val)
        //                                child.Collapse();
        //                            else
        //                                child.Expand();
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

        //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        //{

        //    //if (TreeView1.SelectedNode.Parent == null)
        //    //{
        //    //    foreach (TreeNode node in TreeView1.Nodes)
        //    //    {
        //    //        if (node.Value != TreeView1.SelectedNode.Value)
        //    //            node.Collapse();
        //    //        else
        //    //            node.Expand();
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    string strNodeValue = TreeView1.SelectedNode.Parent.Value;

        //    //    foreach (TreeNode node in TreeView1.Nodes)
        //    //    {
        //    //        if (node.Value != strNodeValue)
        //    //            node.Collapse();
        //    //    }

        //    //}
        //}

        protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar2.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "Admin";
            hdfSession.Value = "Admin";
            try
            {
                if (e.Item.Value == "Admin Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                }
                else if (e.Item.Value == "Setup Associate Category")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);
                }
                else if (e.Item.Value == "Setup Advisor Staff SMTP")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);
                }
                else if (e.Item.Value == "Set Theme")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SetTheme','login');", true);
                }
                else if (e.Item.Value == "Setup IP pool")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserIPPool','login');", true);
                }
                else if (e.Item.Value == "RepositoryCategory")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageRepositoryCategory','login');", true);
                }
                else if (e.Item.Value == "Environment_Settings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEnvironmentSettings','login');", true);
                }
                else if (e.Item.Value == "Profile")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','login');", true);
                }
                else if (e.Item.Value == "Edit Profile")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditAdvisorProfile','login');", true);
                }
                else if (e.Item.Value == "LOB")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','login');", true);
                }
                else if (e.Item.Value == "Add LOB")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddLOB','login');", true);
                }
                else if (e.Item.Value == "Staff")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
                }
                else if (e.Item.Value == "Add Staff")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
                }
                else if (e.Item.Value == "Branch/Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','login');", true);
                }
                else if (e.Item.Value == "Add Branch")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranch','login');", true);
                }
                else if (e.Item.Value == "View Branch Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','login');", true);
                }
                else if (e.Item.Value == "Customer")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "Add Customer")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                }
                else if (e.Item.Value == "Manage Group Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerFamily','login');", true);
                }
                else if (e.Item.Value == "Add Group Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GroupAccountSetup','login');", true);
                }
                else if (e.Item.Value == "Customer Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CuCustomerAssociationSetup','login');", true);
                }
                else if (e.Item.Value == "Alert Configuration")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertDashBoard','login');", true);
                }
                else if (e.Item.Value == "Customized SMS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomerManualSMS", "loadcontrol('AdviserCustomerManualSMS','none');", true);
                }
                else if (e.Item.Value == "MF Folios")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
                }
                else if (e.Item.Value == "View MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
                }
                else if (e.Item.Value == "Add MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "View EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                }
                else if (e.Item.Value == "Add EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);

                }
                else if (e.Item.Value == "Multi_Product_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MultiProductMIS','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','login');", true);
                }
                else if (e.Item.Value == "MF Commission MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (e.Item.Value == "Uploads History")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
                }
                else if (e.Item.Value == "View_Profile_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedWERPProfile','login');", true);
                }
                else if (e.Item.Value == "View Transaction Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "View MF Folio Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFFolio','login');", true);
                }
                else if (e.Item.Value == "View_Equity_Trade_Account_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
                else if (e.Item.Value == "View_Equity_Transaction_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "View_Systematic_Transaction_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "OrderEntry")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderEntry','login');", true);
                }
                else if (e.Item.Value == "OrderMIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderMIS','login');", true);
                }
                else if (e.Item.Value == "OrderRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderRecon','login');", true);
                }
                else if (e.Item.Value == "Staff User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
                }
                else if (e.Item.Value == "Customer User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
                }
                else if (e.Item.Value == "Valuation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('DailyValuation','login');", true);
                }
                else if (e.Item.Value == "MF Systematic Daily Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','login');", true);
                }
                else if (e.Item.Value == "MF Reversal Txn Exception Handling")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "Loan Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','login');", true);
                }
                else if (e.Item.Value == "Add Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','login');", true);
                }
                else if (e.Item.Value == "Loan Partner Commission")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

                }
                else if (e.Item.Value == "Multi Asset Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                }
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "Equity Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','login');", true);
                }
                else if (e.Item.Value == "Compose")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageCompose','login');", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "Outbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageOutbox','login');", true);
                }
                else if (e.Item.Value == "Repository")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageRepository','login');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void RadPanelBar2_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar1.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "RM";
            hdfSession.Value = "RM";
            try
            {
                if (e.Item.Value == "RM Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMDashBoard','login');", true);
                }
                else if (e.Item.Value == "Profile")
                {
                    Session["CurrentrmVo"] = null;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRMDetails','login');", true);
                }
                else if (e.Item.Value == "Customer")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomer','login');", true);
                }
                //else if (e.Item.Value == "Add Customer")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                //}
                else if (e.Item.Value == "Prospect List")
                {
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "Add FP Prospect")
                {
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                //else if (e.Item.Value == "Manage Group Account")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerFamily','login');", true);
                //}
                //else if (e.Item.Value == "Add Group Account")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GroupAccountSetup','login');", true);
                //}
                else if (e.Item.Value == "Manage Portfolio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolio','login');", true);
                }
                else if (e.Item.Value == "Add Portfolio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolioSetup','login');", true);
                }
                //else if (e.Item.Value == "Alert Configuration")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertDashBoard','login');", true);
                //}
                else if (e.Item.Value == "Alert Notifications")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','login');", true);
                }
                else if (e.Item.Value == "View MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "Add MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UnderConstruction", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "View EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                }
                else if (e.Item.Value == "Add EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
                }
                else if (e.Item.Value == "Loan Proposal")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                }
                else if (e.Item.Value == "Add Loan proposal")
                {
                    Session["LoanProcessAction"] = "add";
                    Session[SessionContents.LoanProcessTracking] = null;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanProcessTracking','login');", true);
                }
                else if (e.Item.Value == "FP Offline Form")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineForm','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UnderConstruction", "loadcontrol('UnderConstruction','login');", true);
                }
                //else if (e.Item.Value == "Customized SMS")
                //{
                //    Session["UserType"] = "rm";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomerManualSMS", "loadcontrol('AdviserCustomerManualSMS','none');", true);
                //}
                else if (e.Item.Value == "Multi Asset Report")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                }
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFReports", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "Equity Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EquityReports", "loadcontrol('EquityReports','login');", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "Compose")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageCompose','login');", true);
                }
                else if (e.Item.Value == "Outbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageOutbox','login');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void RadPanelBar3_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar1.CollapseAllItems();
            RadPanelBar2.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "BM";
            hdfSession.Value = "BM";
            try
            {
                if (e.Item.Value == "BM Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMDashBoard", "loadcontrol('BMDashBoard','login');", true);
                }
                else if (e.Item.Value == "Staff")
                {
                    Session[SessionContents.CurrentUserRole] = "BM";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewRM", "loadcontrol('ViewRM','login');", true);
                }
                else if (e.Item.Value == "Customer")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMCustomer", "loadcontrol('BMCustomer','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAMCwiseMIS", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserEQMIS", "loadcontrol('AdviserEQMIS','login');", true);
                }
                //else if (e.Item.Value == "Multi Asset Report")
                //{
                //    Session["UserType"] = "bm";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                //}
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);
                }
                //else if (e.Item.Value == "Equity Report")
                //{
                //    Session["UserType"] = "bm";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','login');", true);
                //}
                else if (e.Item.Value == "Compose")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageCompose','login');", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "Outbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageOutbox','login');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void RadPanelBar4_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar1.CollapseAllItems();
            RadPanelBar2.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "Ops";
            hdfSession.Value = "Ops";
            try
            {
                if (e.Item.Value == "Customer")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "Add Customer")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                }
                else if (e.Item.Value == "Manage Group Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerFamily','login');", true);
                }
                else if (e.Item.Value == "Add Group Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GroupAccountSetup','login');", true);
                }
                else if (e.Item.Value == "Customer Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CuCustomerAssociationSetup','login');", true);
                }
                else if (e.Item.Value == "Alert Configuration")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertDashBoard','login');", true);
                }
                else if (e.Item.Value == "Customized SMS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomerManualSMS", "loadcontrol('AdviserCustomerManualSMS','none');", true);
                }
                else if (e.Item.Value == "MF Folios")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
                }
                else if (e.Item.Value == "View MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
                }
                else if (e.Item.Value == "Add MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "View EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                }
                else if (e.Item.Value == "Add EQ Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);

                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MultiProductMIS','login');", true);
                }
                else if (e.Item.Value == "MF Commission MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (e.Item.Value == "Uploads History")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
                }
                else if (e.Item.Value == "View_Profile_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedWERPProfile','login');", true);
                }
                else if (e.Item.Value == "View Transaction Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "View MF Folio Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFFolio','login');", true);
                }
                else if (e.Item.Value == "View_Equity_Trade_Account_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
                else if (e.Item.Value == "View_Equity_Transaction_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "View_Systematic_Transaction_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                }
                else if (e.Item.Value == "Staff User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
                }
                else if (e.Item.Value == "Customer User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
                }
                else if (e.Item.Value == "Valuation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('DailyValuation','login');", true);
                }
                else if (e.Item.Value == "MF Systematic Daily Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','login');", true);
                }
                else if (e.Item.Value == "MF Reversal Txn Exception Handling")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "Loan Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','login');", true);
                }
                else if (e.Item.Value == "Add Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','login');", true);
                }
                else if (e.Item.Value == "Loan Partner Commission")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

                }
                else if (e.Item.Value == "Multi Asset Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                }
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "Equity Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','login');", true);
                }
                else if (e.Item.Value == "Compose")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageCompose','login');", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "Outbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageOutbox','login');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
       
        private DataSet GetTreeNodesBasedOnUserRoles(string userRole, string treeType)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetTreeNodesBasedOnUserRoles(userRole, treeType, advisorVo.advisorId);
            return dsTreeNodes;
        }

        private DataSet GetTreeNodesBasedOnPlans(int adviserId, string userRole, string treeType)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetTreeNodesBasedOnPlans(adviserId, userRole, treeType);
            return dsTreeNodes;
        }

        private void SetAdminTreeNodesForRoles(DataSet dsAdminTreeNodes, string userRole)
        {
            int flag = 0;
            DataView tempView;
            DataRow dr;
            if (userRole == "Admin")
            {
                //foreach (DataRow dr in dsAdminTreeNodes.Tables[0].Rows)
                //{
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "content" || dr[2].ToString().ToLower() == "message" || dr[2].ToString().ToLower() == "order")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                            
                        }
                    }
                }
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {

                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[1].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "multi product mis")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                        }
                    }
                }

                //foreach (DataRow dr in dsAdminTreeNodes.Tables[2].Rows)
                //{
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {

                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[2].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
                //}
            }
            else if (userRole == "RM")
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "message")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                        }
                    }
                }
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[1].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[2].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
            }
            else if (userRole == "BM")
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "message")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[1].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[2].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
            }
            else if (userRole == "Research")
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar5.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "message")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar5.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[1].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar5.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[2].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
            }

            else if (userRole == "Ops")
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                            if (dr[2].ToString().ToLower() == "message")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[1].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else
                        {
                            dr = dsAdminTreeNodes.Tables[2].Rows.Find(Item.Value);
                            Item.Text = dr[2].ToString();
                        }
                    }
                }
            }
        }

        private void SetAdminTreeNodesForPlans(DataSet dsAdminTreeNodes)
        {
            int flag = 0;
            DataView tempView;

            tempView = new DataView(dsAdminTreeNodes.Tables[0]);
            tempView.Sort = "WTN_TreeNode";

            if (userVo.RoleList.Contains("Admin"))
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else if (advisorVo.IsOpsEnable == 1)
                        {
                            switch (Item.Value)
                            {
                                case "Customer":
                                    Item.Visible = false;
                                    break;
                                case "Business MIS":
                                    Item.Visible = false;
                                    break;
                                case "Customer Report":
                                    Item.Visible = false;
                                    break;
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";

                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                        else if (advisorVo.IsOpsEnable == 1)
                        {
                            switch (Item.Value)
                            {
                                case "Upload":
                                    Item.Visible = false;
                                    break;
                                case "Daily Process":
                                    Item.Visible = false;
                                    break;
                                case "MF Systematic Daily Recon":
                                    Item.Visible = false;
                                    break;
                                case "MF Reversal Txn Exception Handling":
                                    Item.Visible = false;
                                    break;
                                case "Loan Schemes":
                                    Item.Visible = false;
                                    break;
                                case "Loan Partner Commission":
                                    Item.Visible = false;
                                    break;
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";

                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                        else if (advisorVo.IsOpsEnable == 1)
                        {
                            switch (Item.Value)
                            {
                                case "Customer User Management":
                                    Item.Visible = false;
                                    break;
                            }
                        }
                    }
                }

            }
            if (userVo.RoleList.Contains("RM"))
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";

                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";

                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";

                foreach (RadPanelItem Item in RadPanelBar2.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                    }
                }

            }
            if (userVo.RoleList.Contains("BM"))
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";

                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";

                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";

                foreach (RadPanelItem Item in RadPanelBar3.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                    }
                }
            }

            if (userVo.RoleList.Contains("Ops"))
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";

                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";

                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 3)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";

                foreach (RadPanelItem Item in RadPanelBar4.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 1 && Item.Level != 2)
                    {
                        flag = tempView.Find(Item.Value);
                        if (flag == -1)
                        {
                            Item.Visible = false;
                            //Item.Owner.Items.Remove(Item);
                        }
                        else if (advisorVo.IsOpsEnable == 1)
                        {
                            switch (Item.Value)
                            {
                                case "Staff User Management":
                                    Item.Visible = false;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        protected void RadPanelBar5_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar1.CollapseAllItems();
            RadPanelBar2.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            RadPanelBar4.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "Research";
            hdfSession.Value = "Research";
            try
            {
                if (e.Item.Value == "Research_Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ResearchDashboard','login');", true);
                }
                else if (e.Item.Value == "RiskGoal_Classes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RiskGoalClass','login');", true);
                }
                else if (e.Item.Value == "Risk_Score")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RiskScore','login');", true);
                }
                else if (e.Item.Value == "Goal_Score")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalScore','login');", true);
                }
                else if (e.Item.Value == "Asset_Mapping")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetMapping','login');", true);
                }
                else if (e.Item.Value == "Assumptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('Assumptions','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ModelPortfolioSetup','login');", true);
                }
                else if (e.Item.Value == "MF_Investment")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SchemeMappingToModelPortfolio','login');", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}
