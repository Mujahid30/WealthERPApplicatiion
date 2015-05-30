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
using System.Configuration;
using BoOnlineOrderManagement;
namespace WealthERP.Advisor
{
    public partial class AdvisorLeftPane : UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdviserSubscriptionBo adviserSubscriptionBo = new AdviserSubscriptionBo();
        AdvisorVo advisorVo;
        DataSet dsSubscriptionDetails;
        DataTable dtPlanDetails;
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
            DataSet dsAdviserTreeNodes;
            SessionBo.CheckSession();
            Session["BranchAdd"] = "forRM";
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session[SessionContents.BranchLogoPath] != null)
                sourcePath = Session[SessionContents.BranchLogoPath].ToString();

            if (!IsPostBack)
            {
                if (Session["customerVo"] != null)
                    Session.Remove("customerVo");

                if (Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()] == null)
                {
                    dsAdviserTreeNodes = GetAdviserRoleTreeNodes(advisorVo.advisorId);
                    Cache.Insert("AdminLeftTreeNode" + advisorVo.advisorId.ToString(), dsAdviserTreeNodes, null, DateTime.Now.AddMinutes(4 * 60), TimeSpan.Zero);
                }
                else
                {
                    dsAdviserTreeNodes = (DataSet)Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()];

                }
                DataSet dsFilteredData = new DataSet();

                //Code to display and hide the searches based on the roles
                //Code to display and hide the searches based on the roles
                SetUserSearchBox();
                ShowUserRoleBasedPannel();
                DataSet dsUserTreeNode = new DataSet();
                dsUserTreeNode = FilterUserTreeNodeData(dsAdviserTreeNodes);

                SetUserTreeNode(dsUserTreeNode);
                SetAllTreeNodeDefaultExpandSelection();


                //dsSubscriptionDetails = FilterUserTreeNodeSubscription(dsTreeNodes);
                //dsTreeNodes = FilterUserTreeNodePlan(dsTreeNodes);
                //SetAdminTreeNodesForPlans(dsTreeNodes);

                if (advisorVo.MultiBranch != 1)
                {
                    RadPanelBar1.FindItemByValue("Branch/Association").Visible = false;
                }
                //if (dsSubscriptionDetails.Tables[0].Rows.Count > 0)
                //{
                //    if (dsSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfStaffWebLogins"] != null)
                //        noOfStaffWebLogins = int.Parse(dsSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfStaffWebLogins"].ToString());
                //    if (noOfStaffWebLogins <= 1)
                //    {
                //        RadPanelBar1.FindItemByValue("Staff").Visible = false;
                //        RadPanelBar3.FindItemByValue("Staff").Visible = false;
                //    }
                //    //else
                //    //{
                //    //    RadPanelBar1.FindItemByValue("Staff").Visible = true;
                //    //    RadPanelBar3.FindItemByValue("Staff").Visible = true;
                //    //}
                //}
                //dtPlanDetails = dsTreeNodes.Tables[3];
                //if (!userVo.RoleList.Contains("Admin"))
                //{
                //    RadPanelBar1.Visible = false;
                //}
                //else
                //{
                //    if (int.Parse(dtPlanDetails.Rows[0]["WP_IsMultiBranchPlan"].ToString()) == 1)
                //        RadPanelBar3.Visible = true;
                //    else
                //        RadPanelBar3.Visible = false;
                //    if (int.Parse(dtPlanDetails.Rows[0]["WP_IsOtherStaffEnabled"].ToString()) == 1)
                //        RadPanelBar2.Visible = true;
                //    else
                //        RadPanelBar2.Visible = false;
                //    if (int.Parse(dtPlanDetails.Rows[0]["WP_PlanId"].ToString()) != 1)
                //        RadPanelBar5.Visible = true;
                //    else
                //        RadPanelBar5.Visible = false;
                //}
                //if (!userVo.RoleList.Contains("RM"))
                //    RadPanelBar2.Visible = false;
                //if (!userVo.RoleList.Contains("BM"))
                //    RadPanelBar3.Visible = false;
                //if (!userVo.RoleList.Contains("Ops"))
                //    RadPanelBar4.Visible = false;
                //if (!userVo.RoleList.Contains("Research"))
                //    RadPanelBar5.Visible = false;


                ////
                //// Code to display inbox/message links based on main role
                //// 
                //if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                //{
                //    /* Compose, Inbox, Outbox Visible only in admin pane */
                //    RadPanelBar2.FindItemByValue("Message").Visible = false; // RM Bar
                //    RadPanelBar3.FindItemByValue("Message").Visible = false; // BM Bar
                //    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                //}
                //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                //{
                //    /* Compose, Inbox, Outbox Visible only in bm pane */
                //    RadPanelBar2.FindItemByValue("Message").Visible = false; // RM Bar
                //    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                //}
                //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                //{
                //    /* Inbox Visible only in rm pane */
                //    RadPanelBar5.FindItemByValue("Message").Visible = false; // Research Bar
                //}
                //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "research")
                //{
                //    /* Inbox Visible only in research pane */

                //}


                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);


                if (Session["NodeType"] != null)
                {

                    Session.Remove("NodeType");
                }
            }

            //if (advisorVo.A_AgentCodeBased == 1)
            //{
            //    RadPanelBar2.Visible = false;
            //    RadPanelBar3.Visible = false;
            //}

        }

        private DataSet FilterUserTreeNodeData(DataSet dsAdviserTreeNodes)
        {
            DataSet dsUserTreeNode = new DataSet();
            DataTable dtUserTreeNode = dsAdviserTreeNodes.Tables[0].Clone();
            DataTable dtUserTreeSubNode = dsAdviserTreeNodes.Tables[1].Clone();
            DataTable dtUserTreeSubSubsNode = dsAdviserTreeNodes.Tables[2].Clone();
            Dictionary<Int16, string> userAdviserRole = (Dictionary<Int16, string>)userVo.AdviserRole;

            foreach (int role in userAdviserRole.Keys)
            {
                dsAdviserTreeNodes.Tables[0].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeNode.Merge(dsAdviserTreeNodes.Tables[0].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);

                dsAdviserTreeNodes.Tables[1].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeSubNode.Merge(dsAdviserTreeNodes.Tables[1].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);

                dsAdviserTreeNodes.Tables[2].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeSubSubsNode.Merge(dsAdviserTreeNodes.Tables[2].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);
            }
            dsUserTreeNode.Tables.Clear();
            dsUserTreeNode.Tables.Add(dtUserTreeNode.Copy().DefaultView.ToTable(true, "WTN_TreeNodeId", "WTN_TreeNode", "WTN_TreeNodeText", "WTN_IsApplicableForMultiBranch", "WTN_IsApplicableForMultiStaff", "UR_RoleId", "UR_RoleName"));
            dsUserTreeNode.Tables.Add(dtUserTreeSubNode.Copy().DefaultView.ToTable(true, "WTSN_TreeSubNodeId", "WTSN_TreeSubNode", "WTSN_TreeSubNodeText", "WTSN_IsApplicableForMultiBranch", "WTSN_IsApplicableForMultiStaff", "UR_RoleId", "UR_RoleName"));
            dsUserTreeNode.Tables.Add(dtUserTreeSubSubsNode.Copy().DefaultView.ToTable(true, "WTSSN_TreeSubSubNodeId", "WTSSN_TreeSubSubNode", "WTSSN_TreeSubSubNodeText", "WTSSN_IsApplicableForMultiBranch", "WTSSN_IsApplicableForMultiStaff", "UR_RoleId", "UR_RoleName"));

            return dsUserTreeNode;
        }

        private void SetUserTreeNode(DataSet dsTreeNodes)
        {
            string userRole;
            DataSet dsFilteredData;
            if (userVo.RoleList.Contains("Admin"))
            {
                userRole = "Admin";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "Admin");

            }
            if (userVo.RoleList.Contains("RM"))
            {
                userRole = "RM";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "RM");
            }
            if (userVo.RoleList.Contains("BM"))
            {
                userRole = "BM";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "BM");
            }
            if (userVo.RoleList.Contains("Ops"))
            {
                userRole = "Ops";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "Ops");
            }
            if (userVo.RoleList.Contains("Research"))
            {
                userRole = "Research";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "Research");
            }
            if (userVo.RoleList.Contains("Associates"))
            {
                userRole = "Associates";
                dsFilteredData = FilterUserTreeNode(userRole, dsTreeNodes);
                SetAdminTreeNodesForRoles(dsFilteredData, "Associates");
            }

        }

        private void SetAllTreeNodeDefaultExpandSelection()
        {
            //Code to expand the home node based on the User Roles
            if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            {
                if (!userVo.RoleList.Contains("Ops"))
                {
                    RadPanelBar1.FindItemByValue("Admin").Expanded = true;
                    if (Session["IsCustomerGrid"] == null)
                    {
                        RadPanelBar1.FindItemByValue("Admin Home").Selected = true;
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                    }
                    else
                        RadPanelBar1.FindItemByValue("Customer").Selected = true;
                }
                else
                {
                    //RadPanelBar1.FindItemByValue("Admin Home").Visible = true;
                    RadPanelBar4.FindItemByValue("Ops").Expanded = true;
                    //RadPanelBar4.FindItemByValue("Customer").Selected = true;
                    RadPanelBar1.FindItemByValue("Admin Home").Selected = true;
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);
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
                RadPanelBar4.FindItemByValue("Admin Home").Selected = true;
            }
            else if (userVo.RoleList.Contains("Research"))
            {
                RadPanelBar5.FindItemByValue("Reference_Data").Expanded = true;
                RadPanelBar5.FindItemByValue("Reference_Data").Selected = true;
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
            {
                RadPanelBar6.FindItemByValue("Associates").Expanded = true;

            }

            if (Session["NodeType"] == "CustomerUpload")
            {
                RadPanelBar1.FindItemByValue("File_Upload").Expanded = true;
                RadPanelBar1.FindItemByValue("Start_Upload").Expanded = true;
                RadPanelBar1.FindItemByValue("Start_Upload").Selected = true;
            }
            else if (Session["NodeType"] == "AdviserCustomer")
            {

                RadPanelBar1.FindItemByValue("Customer").Expanded = true;
                RadPanelBar1.FindItemByValue("CustomerList").Selected = true;
            }
            else if (Session["NodeType"] == "MFOrderEntry")
            {
                RadPanelBar1.FindItemByValue("Order_Management").Expanded = true;
                RadPanelBar1.FindItemByValue("OrderEntry").Expanded = true;
                RadPanelBar1.FindItemByValue("OrderEntry").Selected = true;

            }
            else if (Session["NodeType"] == "MFDashBoard")
            {
                RadPanelBar1.FindItemByValue("Business MIS").Expanded = true;
                RadPanelBar1.FindItemByValue("MFDashBoard").Expanded = true;
                RadPanelBar1.FindItemByValue("MFDashBoard").Selected = true;
                RadPanelBar6.FindItemByValue("Business MIS").Expanded = true;
            }

            else if (Session["NodeType"] == "MessageInbox")
            {
                RadPanelBar1.FindItemByValue("Message").Expanded = true;
                RadPanelBar1.FindItemByValue("Inbox").Expanded = true;
                RadPanelBar1.FindItemByValue("Inbox").Selected = true;

            }

            else if (Session["NodeType"] == "AddProspectList")
            {
                RadPanelBar1.FindItemByValue("Admin").Expanded = false;
                RadPanelBar2.FindItemByValue("RM").Expanded = true;
                RadPanelBar2.FindItemByValue("Customer").Expanded = true;

                RadPanelBar2.FindItemByValue("Add FP Prospect").Expanded = true;
                RadPanelBar2.FindItemByValue("Add FP Prospect").Selected = true;

            }

            else if (Session["NodeType"] == "CustomerReportsDashBoard")
            {
                RadPanelBar1.FindItemByValue("Customer").Expanded = true;
                RadPanelBar1.FindItemByValue("Customer_Report").Expanded = true;
                RadPanelBar1.FindItemByValue("Customer_Report").Selected = true;

            }
        }

        private void ShowUserRoleBasedPannel()
        {
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
            if (!userVo.RoleList.Contains("Associates"))
                RadPanelBar6.Visible = false;

        }

        private void SetUserSearchBox()
        {
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
            else if (userVo.RoleList.Contains("Associates"))
            {
                txtFindRMCustomer.Visible = false;
                txtFindBranch.Visible = false;
                txtFindRM.Visible = false;
                btnSearchRM.Visible = false;
                btnSearchBranch.Visible = false;
                btnSearchRMCustomer.Visible = false;
            }
        }

        protected DataSet FilterUserTreeNode(string userRole, DataSet dsTreeNode)
        {
            DataSet dsTreeFilterNode = new DataSet();
            DataTable dtTreeNode = new DataTable();
            DataTable dtTreeSubNode = new DataTable();
            DataTable dtTreeSubSubNode = new DataTable();
            dtTreeNode = dsTreeNode.Tables[0].Clone();
            dtTreeSubNode = dsTreeNode.Tables[1].Clone();
            dtTreeSubSubNode = dsTreeNode.Tables[2].Clone();

            if (dsTreeNode.Tables[0].Rows.Count > 0)
            {
                dsTreeNode.Tables[0].DefaultView.RowFilter = "UR_RoleName='" + userRole + "'";
                dtTreeNode = dsTreeNode.Tables[0].DefaultView.ToTable();
            }

            if (dsTreeNode.Tables[1].Rows.Count > 0)
            {
                dsTreeNode.Tables[1].DefaultView.RowFilter = "UR_RoleName='" + userRole + "'";
                dtTreeSubNode = dsTreeNode.Tables[1].DefaultView.ToTable();
            }

            if (dsTreeNode.Tables[2].Rows.Count > 0)
            {
                dsTreeNode.Tables[2].DefaultView.RowFilter = "UR_RoleName='" + userRole + "'";
                dtTreeSubSubNode = dsTreeNode.Tables[2].DefaultView.ToTable();
            }
            dsTreeFilterNode.Tables.Add(dtTreeNode);
            dsTreeFilterNode.Tables.Add(dtTreeSubNode);
            dsTreeFilterNode.Tables.Add(dtTreeSubSubNode);

            return dsTreeFilterNode;
        }

        protected DataSet FilterUserTreeNodePlan(DataSet dsTreeNode)
        {
            DataSet dsTreeFilterNodePlan = new DataSet();
            //DataTable dtTreeNodePlan = new DataTable();
            //DataTable dtTreeSubNodePlan = new DataTable();
            //DataTable dtTreeSubSubNodePlan = new DataTable();

            if (dsTreeNode.Tables[3].Rows.Count > 0)
            {
                dsTreeFilterNodePlan.Tables.Add(dsTreeNode.Tables[3].Copy()); ;
            }

            if (dsTreeNode.Tables[4].Rows.Count > 0)
            {
                dsTreeFilterNodePlan.Tables.Add(dsTreeNode.Tables[4].Copy()); ;
            }

            if (dsTreeNode.Tables[5].Rows.Count > 0)
            {
                dsTreeFilterNodePlan.Tables.Add(dsTreeNode.Tables[5].Copy()); ;
            }
            if (dsTreeNode.Tables[8].Rows.Count > 0)
            {
                dsTreeFilterNodePlan.Tables.Add(dsTreeNode.Tables[8].Copy()); ;
            }

            return dsTreeFilterNodePlan;
        }

        protected DataSet FilterUserTreeNodeSubscription(DataSet dsTreeNode)
        {
            DataSet dsTreeFilterNodeSubscription = new DataSet();

            if (dsTreeNode.Tables[6].Rows.Count > 0)
            {
                dsTreeFilterNodeSubscription.Tables.Add(dsTreeNode.Tables[6].Copy()); ;
            }

            if (dsTreeNode.Tables[7].Rows.Count > 0)
            {
                dsTreeFilterNodeSubscription.Tables.Add(dsTreeNode.Tables[7].Copy()); ;
            }
            return dsTreeFilterNodeSubscription;
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



        protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar2.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "Admin";
            hdfSession.Value = "Admin";
            try
            {

                if (e.Item.Value == "Manage Lookups")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageLookups','login');", true);
                }
                else if (e.Item.Value == "Commision_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionReceivableRecon','login');", true);
                }
                else if (e.Item.Value == "Non-MF_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOfflineOrderRecon','login');", true);

                }
                else if (e.Item.Value == "View_AMC")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AMCManage','login');", true);
                }
                else if (e.Item.Value == "Advertisement_Manage")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BannerManager','login');", true);
                }
                else if (e.Item.Value == "View_UTIAMC")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UTIAMCManage','login');", true);
                }
                else if (e.Item.Value == "External_Header_Mapping")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ExternalHeaderMapping','login');", true);
                }
                else if (e.Item.Value == "54EC_ORDER_bOOK")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderBook','login');", true);
                }
                else if (e.Item.Value == "View_Associate_Payable_Rule")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PayableStructureView','login');", true);
                }
                else if (e.Item.Value == "SIP_Offline_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomerSIPOrderBook','login');", true);
                }
                else if (e.Item.Value == "SubBroker_Code_Cleansing")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SubBrokerCodecCeansing','login');", true);
                }
                else if (e.Item.Value == "NCD_IPO_Allotments")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDOrderMatchExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "Category")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCategoryConfiguration','login');", true);
                }
                else if (e.Item.Value == "Add_Request")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BulkRequestManagement','login');", true);
                }
                else if (e.Item.Value == "Request_Status")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReassignStaffAssociats','login');", true);
                }
                else if (e.Item.Value == "Staff_ReAssign")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReassignStaffAssociats','login');", true);
                }
                else if (e.Item.Value == "Alert_configure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAlertConfiguration','login');", true);
                }
                else if (e.Item.Value == "Alert_SetUp")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAlertSetup','login');", true);
                }

                else if (e.Item.Value == "Alert_SMSNotification")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','login');", true);
                }
                else if (e.Item.Value == "Add_NCD_Order")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "NCD_Offline_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersNCDOrderBook','login');", true);
                }
                else if (e.Item.Value == "Add_IPO_Order")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "IPO_Offline_Order_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersIPOOrderBook','login');", true);
                }
                if (e.Item.Value == "Trade_Business_Date")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TradeBusinessDate','login');", true);
                }
                else if (e.Item.Value == "Admin Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                }
                else if (e.Item.Value == "RTA_Unit_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFHoldingRecon','login');", true);
                }
                else if (e.Item.Value == "Audit_Log")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProfileAudit','login');", true);
                }
                else if (e.Item.Value == "Setup Associate Category")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);
                }
                else if (e.Item.Value == "ProductOrderMaster")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderEntry','login');", true);
                }
                else if (e.Item.Value == "FixedIncomeOrderEntry")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncomeOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Hierarchy_Setup")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HierarchySetup','login');", true);
                }
                else if (e.Item.Value == "View_Customer_Association")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssociateCustomerList','login');", true);
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
                else if (e.Item.Value == "User_Role")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserDepartmentRoleSetup','login');", true);
                }
                else if (e.Item.Value == "User_Role_privileges")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRoleToTreeNodeMapping','login');", true);
                }
                else if (e.Item.Value == "Setup_customer_category")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerCategorySetup','login');", true);
                }
                else if (e.Item.Value == "Setup IP pool")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserIPPool','login');", true);
                }

                else if (e.Item.Value == "Customer_Association_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SubBrokerCustomerAssociationSync','login');", true);
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
                    if (advisorVo.A_AgentCodeBased == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewStaffDashBoard','login');", true);
                    }
                    //----------For Existing WERP---------------------------

                    //----------For Existing SBI Zone,Cluster,Channel,Team...---------------------------

                }
                else if (e.Item.Value == "Add Staff")
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
                    if (advisorVo.A_AgentCodeBased == 0)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAdviserStaff','login');", true);
                }
                else if (e.Item.Value == "Add_Staff_Offline")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddStaff','login');", true);
                }
                else if (e.Item.Value == "Branch/Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','login');", true);
                }
                else if (e.Item.Value == "Zone_Cluster")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserZoneCluster','login');", true);
                }

                else if (e.Item.Value == "Add Branch")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranch','login');", true);
                }
                else if (e.Item.Value == "View Branch Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','login');", true);
                }
                else if (e.Item.Value == "CustomerList")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "OfflineCustomerMerge")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomerMerge','login');", true);

                }
                else if (e.Item.Value == "Client_access")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineClientAccess", "loadcontrol('OnlineClientAccess','login');", true);
                }
                else if (e.Item.Value == "Add Customer")
                {
                    Session.Remove("LinkAction");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                }
                else if (e.Item.Value == "Add FP Prospect")
                {
                    Session["UserType"] = "adviser";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                else if (e.Item.Value == "Manage Portfolio")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolio','login');", true);
                }
                else if (e.Item.Value == "Add Portfolio")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolioSetup','login');", true);
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
                else if (e.Item.Value == "FP Offline Form")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineForm','login');", true);
                }
                else if (e.Item.Value == "Customer_Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }
                else if (e.Item.Value == "View MF Transaction")
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFManualSingleTran','login');", true);

                }
                else if (e.Item.Value == "Multi_Product_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MultiProductMIS','login');", true);
                }
                else if (e.Item.Value == "Business_MIS_Dashboard")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboardOld','login');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoginTrack','login');", true);
                }
                else if (e.Item.Value == "Prospect List")
                {
                    Session["UserType"] = "adviser";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_MIS');", true);
                }
                else if (e.Item.Value == "MF_SIP_Projection")
                {
                    Session["UserType"] = "adviser";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFSIPProjection','login');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_Projection');", true);
                }
                else if (e.Item.Value == "Performance_Allocation")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PerformanceAndAllocation','login');", true);
                }
                else if (e.Item.Value == "MF Commission MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Goal_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalMIS','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetAllocationMIS','login');", true);
                }
                else if (e.Item.Value == "MFDashBoard")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                }
                else if (e.Item.Value == "CustomerSignUp")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserNewSignupMIS','login');", true);
                }


                else if (e.Item.Value == "MFTurnOverMIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TurnOverDashBoard','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Customer_AUM")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAUM','login');", true);
                }
                else if (e.Item.Value == "Customer_Holdings")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MFT")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "Client_Initial_Order")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HoldingIssueAllotment','login');", true);
                }
                else if (e.Item.Value == "Scheme_DataTrans_Mapping")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddSchemeMapping','login')", true);
                }

                else if (e.Item.Value == "Start_Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (e.Item.Value == "Request_Upload_Status")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UploadRequestStatus','login');", true);
                }
                else if (e.Item.Value == "Offline_Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','OfflineUpload=Offline');", true);
                }
                else if (e.Item.Value == "Uploads_Exception")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UploadDashBoard','login');", true);
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
                else if (e.Item.Value == "Trail_Commission_Exception")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TrailCommisionTransactionRejects','login');", true);
                }
                else if (e.Item.Value == "Customer_Accounts_Compare")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "MFNP_Tranx_Compare")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFNPAndTransactionCompare','login');", true);
                }
                else if (e.Item.Value == "OrderEntry")
                {
                    if (advisorVo.A_AgentCodeBased == 0)
                    {
                        Session["UserType"] = "adviser";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderDashBoard','login');", true);

                    }
                    else
                    {
                        Session["UserType"] = "adviser";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFOrderEntry','login');", true);
                    }
                }

                else if (e.Item.Value == "LI_Order")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Order_List")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderList','login');", true);
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
                else if (e.Item.Value == "Associate_User_Mnagement")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AgentManagement','login');", true);
                }

                else if (e.Item.Value == "Adviser_Login_Track")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoginTrack','login');", true);
                }
                else if (e.Item.Value == "Valuation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFAccountValuation','login');", true);
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
                else if (e.Item.Value == "Customer Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
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
                else if (e.Item.Value == "FP Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
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
                else if (e.Item.Value == "View MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
                }
                else if (e.Item.Value == "View_Receivable_structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureRuleGrid','none');", true);
                }
                else if (e.Item.Value == "Receivable_Strucrure_setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReceivableSetup','none');", true);
                }
                else if (e.Item.Value == "View_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewSchemeStructureAssociation','none');", true);
                }
                else if (e.Item.Value == "Receivable_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SchemeStructureRuleAssociation','none');", true);
                }
                else if (e.Item.Value == "View_Payable_Structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "Payable_Structure_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "AddAssociates")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociates", "loadcontrol('AddAssociatesDetails','login');", true);
                }
                else if (e.Item.Value == "ViewAssociates")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAssociates','login');", true);
                }
                else if (e.Item.Value == "ViewAssociatess")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAdviserAssociateList','login');", true);
                }
                else if (e.Item.Value == "AddAgentCode")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation','login');", true);
                }
                else if (e.Item.Value == "ViewAgentCode")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
                }
                else if (e.Item.Value == "Map_scheme")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureToSchemeMapping','login');", true);
                }
                else if (e.Item.Value == "Commission_Receivable_Recon")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionReconMIS','login');", true);
                }
                else if (e.Item.Value == "File_Generation")
                {
                    Session["UserType"] = "adviser";

                }
                else if (e.Item.Value == "RTA_Extract")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderExtract','login');", true);
                }
                else if (e.Item.Value == "NCD_Extract")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineIssueExtract','login');", true);
                }
                else if (e.Item.Value == "File_Extraction")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderAccountingExtract','login');", true);
                }
                else if (e.Item.Value == "NCD/IPO Accounting")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderNCDIPOAccountingExtract','login');", true);
                }
                else if (e.Item.Value == "MF_Online_SIP_Ord_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerSIPOrderBook','login');", true);

                }
                else if (e.Item.Value == "MF_Online_OrderBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerOrderBook','login');", true);

                }
                else if (e.Item.Value == "SIP_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderBackOffice','login');", true);

                }
                else if (e.Item.Value == "MF_Transaction_Books")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerTransctionBook','login');", true);

                }

                else if (e.Item.Value == "Scheme_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineSchemeSetUp','login');", true);

                }
                else if (e.Item.Value == "NCDIssuesetup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDIssueSetup','login');", true);
                }
                else if (e.Item.Value == "NCDIssueList")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDIssueList','login');", true);
                }

                else if (e.Item.Value == "View_Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineSchemeMIS','login');", true);

                }
                else if (e.Item.Value == "NCD_Order_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserNCDOrderBook','login');", true);

                }
                else if (e.Item.Value == "NCD_IPO_Issue_Allotment")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HoldingIssueAllotment','login');", true);

                }
                else if (e.Item.Value == "NcdIpoRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDOrderMatchExceptionHandling','login');", true);

                }

                else if (e.Item.Value == "IPO_Order_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerIPOOrderBook','login');", true);

                }
                else if (e.Item.Value == "NCD/IPO_allotment")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineIssueUpload','login');", true);

                }
                else if (e.Item.Value == "NCD_Holdings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerNCDHoldings','login');", true);

                }
                else if (e.Item.Value == "IPO_Holdings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerIPOHoldings','login');", true);

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
                else if (e.Item.Value == "CustomerList")
                {
                    Session["Customer"] = "Customer";
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "Customer_Report")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }
                //else if (e.Item.Value == "Add Customer")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                //}
                else if (e.Item.Value == "Prospect List")
                {
                    Session["UserType"] = "rm";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "OrderEntry")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderDashBoard','login');", true);
                }
                else if (e.Item.Value == "LI_Order")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Order_List")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderList','login');", true);
                }
                else if (e.Item.Value == "OrderMIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderMIS','login');", true);
                }
                else if (e.Item.Value == "OrderRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderRecon','login');", true);
                }
                else if (e.Item.Value == "Add FP Prospect")
                {
                    Session["UserType"] = "rm";
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
                else if (e.Item.Value == "MF Folios")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
                }
                else if (e.Item.Value == "Manage Portfolio")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolio','login');", true);
                }
                else if (e.Item.Value == "Add Portfolio")
                {
                    Session["UserType"] = "rm";
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
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineForm','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MFDashboard")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                }
                else if (e.Item.Value == "MF_SIP_Projection")
                {
                    Session["UserType"] = "rm";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFSIPProjection','login');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_Projection');", true);
                }
                else if (e.Item.Value == "Performance_Allocation")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PerformanceAndAllocation','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Goal_MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalMIS','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation_MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetAllocationMIS','login');", true);
                }
                else if (e.Item.Value == "MFTurnOverMIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TurnOverDashBoard','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UnderConstruction", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Customer_Holdings")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "Transactions")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "Customer Report")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_MIS');", true);
                }
                //else if (e.Item.Value == "Customized SMS")
                //{
                //    Session["UserType"] = "rm";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomerManualSMS", "loadcontrol('AdviserCustomerManualSMS','none');", true);
                //}
                else if (e.Item.Value == "Associateslist")
                {
                    Session["UserType"] = "rm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAdviserAssociateList','login');", true);
                }
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
                else if (e.Item.Value == "FP Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
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
                    //----------For Existing WERP---------------------------
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
                    //----------For Existing SBI Zone,Cluster,Channel,Team...---------------------------
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewStaff','login');", true);
                }
                else if (e.Item.Value == "CustomerList")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer','login');", true);
                }


                else if (e.Item.Value == "Customer_Report")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }
                else if (e.Item.Value == "Status_ISA")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMCustomer", "loadcontrol('CustomerISARequestList','login');", true);
                }
                else if (e.Item.Value == "Generate_ISA")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMCustomer", "loadcontrol('CustomerISARequest','login');", true);
                }
                else if (e.Item.Value == "ISA_Mapp")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMCustomer", "loadcontrol('CustomerISAFolioMapping','login');", true);
                }
                else if (e.Item.Value == "OrderEntry")
                {

                    if (advisorVo.A_AgentCodeBased == 1)
                    {
                        Session["UserType"] = "bm";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFOrderEntry','login');", true);
                    }
                    else
                    {
                        Session["UserType"] = "bm";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderDashBoard','login');", true);
                    }

                }
                else if (e.Item.Value == "LI_Order")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Order_List")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderList','login');", true);
                }
                else if (e.Item.Value == "OrderMIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderMIS','login');", true);
                }
                else if (e.Item.Value == "OrderRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderRecon','login');", true);
                }
                else if (e.Item.Value == "Prospect List")
                {
                    Session["UserType"] = "bm";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "MF Folios")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAMCwiseMIS", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MFDashboard")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_MIS');", true);
                }
                else if (e.Item.Value == "MF_SIP_Projection")
                {
                    Session["UserType"] = "bm";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFSIPProjection','login');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_Projection');", true);
                }
                else if (e.Item.Value == "Performance_Allocation")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PerformanceAndAllocation','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserEQMIS", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Goal_MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalMIS','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation_MIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetAllocationMIS','login');", true);
                }
                else if (e.Item.Value == "MFTurnOverMIS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TurnOverDashBoard','login');", true);
                }

                else if (e.Item.Value == "Customer_Holdings")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "Transactions")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                //else if (e.Item.Value == "Multi Asset Report")
                //{
                //    Session["UserType"] = "bm";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                //}
                else if (e.Item.Value == "Customer Report")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }

                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "FP Report")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
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
                else if (e.Item.Value == "AddAssociates")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociates", "loadcontrol('AddAssociates','login');", true);
                }
                else if (e.Item.Value == "ViewAssociates")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAssociates','login');", true);
                }
                else if (e.Item.Value == "Associateslist")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAdviserAssociateList','login');", true);
                }
                else if (e.Item.Value == "Customized SMS")
                {
                    Session["UserType"] = "bm";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomerManualSMS", "loadcontrol('AdviserCustomerManualSMS','none');", true);
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
                if (e.Item.Value == "Manage Lookups")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageLookups','login');", true);
                }
                else if (e.Item.Value == "OfflineCustomerMerge")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomerMerge','login');", true);

                }
                else if (e.Item.Value == "Non-MF_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOfflineOrderRecon','login');", true);

                }
                else if (e.Item.Value == "Advertisement_Manage")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BannerManager','login');", true);
                }
                else if (e.Item.Value == "View_AMC")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AMCManage','login');", true);
                }
                else if (e.Item.Value == "View_UTIAMC")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UTIAMCManage','login');", true);
                }
                else if (e.Item.Value == "External_Header_Mapping")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ExternalHeaderMapping','login');", true);
                }
                else if (e.Item.Value == "Admin Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                }
                else if (e.Item.Value == "54EC_ORDER_bOOK")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderBook','login');", true);
                }
                else if (e.Item.Value == "SIP_Offline_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomerSIPOrderBook','login');", true);
                }
                else if (e.Item.Value == "View_Associate_Payable_Rule")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PayableStructureView','login');", true);
                }
                else if (e.Item.Value == "NCD_IPO_Allotments")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDOrderMatchExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "Client_Initial_Order")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HoldingIssueAllotment','login');", true);
                }
                else if (e.Item.Value == "Add_Request")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('BulkRequestManagement','login');", true);
                }
                else if (e.Item.Value == "Request_Status")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReassignStaffAssociats','login');", true);
                }
                else if (e.Item.Value == "Offline_Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','OfflineUpload=Offline');", true);
                }
                else if (e.Item.Value == "Add_NCD_Order")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "NCD_Offline_Book")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersNCDOrderBook','login');", true);
                }
                else if (e.Item.Value == "Add_IPO_Order")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "IPO_Offline_Order_Book")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersIPOOrderBook','login');", true);
                }
                if (e.Item.Value == "Trade_Business_Date")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TradeBusinessDate','login');", true);
                }
                else if (e.Item.Value == "Setup Advisor Staff SMTP")
                {
                    //Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);
                }
                else if (e.Item.Value == "RTA_Unit_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFHoldingRecon','login');", true);
                }
                else if (e.Item.Value == "Audit_Log")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProfileAudit','login');", true);
                }
                else if (e.Item.Value == "Set Theme")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionReconMIS','login');", true);
                }
                else if (e.Item.Value == "Setup_customer_category")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerCategorySetup','login');", true);
                }
                else if (e.Item.Value == "Environment_Settings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEnvironmentSettings','login');", true);
                }
                else if (e.Item.Value == "Setup IP pool")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserIPPool','login');", true);
                }

                else if (e.Item.Value == "RepositoryCategory")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageRepositoryCategory','login');", true);
                }
                else if (e.Item.Value == "Repository")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ManageRepository','login');", true);
                }
                else if (e.Item.Value == "Setup Associate Category")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);
                }
                else if (e.Item.Value == "Profile")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','login');", true);
                }
                else if (e.Item.Value == "Edit Profile")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditAdvisorProfile','login');", true);
                }
                else if (e.Item.Value == "Zone_Cluster")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserZoneCluster','login');", true);
                }
                else if (e.Item.Value == "Staff")
                {
                    if (advisorVo.A_AgentCodeBased == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewStaffDashBoard','login');", true);
                    }
                }
                else if (e.Item.Value == "Add Staff")
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
                    if (advisorVo.A_AgentCodeBased == 0)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAdviserStaff','login');", true);
                }
                else if (e.Item.Value == "Add_Staff_Offline")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddStaff','login');", true);
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
                else if (e.Item.Value == "Hierarchy_Setup")
                {
                    //Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HierarchySetup','login');", true);
                }
                else if (e.Item.Value == "LOB")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','login');", true);
                }
                else if (e.Item.Value == "Add LOB")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddLOB','login');", true);
                }
                if (e.Item.Value == "Client_access")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineClientAccess", "loadcontrol('OnlineClientAccess','login');", true);
                }
                if (e.Item.Value == "CustomerList")
                {
                    Session["Customer"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "View_Customer_Association")
                {
                    //Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssociateCustomerList','login');", true);
                }
                else if (e.Item.Value == "Status_ISA")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerISARequestList','login');", true);
                }
                else if (e.Item.Value == "ISA_Folio_Mapp")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerISAFolioMapping','login');", true);
                }
                else if (e.Item.Value == "Customer_Association_Recon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SubBrokerCustomerAssociationSync','login');", true);
                }
                else if (e.Item.Value == "Add Customer")
                {
                    Session.Remove("LinkAction");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerType','login');", true);
                }
                else if (e.Item.Value == "Add FP Prospect")
                {
                    Session["UserType"] = "adviser";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                else if (e.Item.Value == "Manage Portfolio")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolio','login');", true);
                }
                else if (e.Item.Value == "Add Portfolio")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolioSetup','login');", true);
                }
                else if (e.Item.Value == "FP Offline Form")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineForm','login');", true);
                }
                else if (e.Item.Value == "ProductOrderMaster")
                {
                    // Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Customer_Report")
                {
                    Session["UserType"] = "ops";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
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
                else if (e.Item.Value == "Associate_User_Mnagement")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AgentManagement','login');", true);
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TransactionsView','none');", true);
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
                else if (e.Item.Value == "Prospect List")
                {
                    Session["UserType"] = "adviser";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_MIS');", true);
                }
                else if (e.Item.Value == "MF_SIP_Projection")
                {
                    Session["UserType"] = "adviser";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFSIPProjection','login');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_Projection');", true);
                }
                else if (e.Item.Value == "Performance_Allocation")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PerformanceAndAllocation','login');", true);
                }
                else if (e.Item.Value == "Business_MIS_Dashboard")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboardOld','login');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoginTrack','login');", true);
                }
                else if (e.Item.Value == "Multi_Product_MIS")
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
                else if (e.Item.Value == "Goal_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalMIS','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation_MIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetAllocationMIS','login');", true);
                }
                else if (e.Item.Value == "MFDashBoard")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                }
                else if (e.Item.Value == "CustomerSignUp")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserNewSignupMIS','login');", true);
                }

                else if (e.Item.Value == "MFTurnOverMIS")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TurnOverDashBoard','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Commission_Receivable_Recon")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionReconMIS','login');", true);
                }
                else if (e.Item.Value == "NcdIpoRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDOrderMatchExceptionHandling','login');", true);

                }
                else if (e.Item.Value == "Start_Upload")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
                }
                else if (e.Item.Value == "Request_Upload_Status")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UploadRequestStatus','login');", true);
                }
                else if (e.Item.Value == "Customer_AUM")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAUM','login');", true);
                }
                else if (e.Item.Value == "Customer_Holdings")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "NCD_Allotments")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('HoldingIssueAllotment','login');", true);
                }
                else if (e.Item.Value == "Transactions")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                //else if (e.Item.Value == "Upload")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
                //}
                else if (e.Item.Value == "Uploads_Exception")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UploadDashBoard','login');", true);
                }
                else if (e.Item.Value == "Uploads History")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
                }
                else if (e.Item.Value == "NCD/IPO_allotment")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineIssueUpload','login');", true);

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
                else if (e.Item.Value == "MFT")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "Trail_Commission_Exception")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TrailCommisionTransactionRejects','login');", true);
                }

                else if (e.Item.Value == "Staff User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
                }
                else if (e.Item.Value == "Customer User Management")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
                }
                else if (e.Item.Value == "Adviser_Login_Track")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoginTrack','login');", true);
                }
                else if (e.Item.Value == "Valuation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFAccountValuation','login');", true);
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
                else if (e.Item.Value == "Customer_Accounts_Compare")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerExceptionHandling','login');", true);
                }

                else if (e.Item.Value == "MFNP_Tranx_Compare")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFNPAndTransactionCompare','login');", true);
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
                else if (e.Item.Value == "FP Report")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
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
                else if (e.Item.Value == "OrderEntry")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderDashBoard','login');", true);
                }
                else if (e.Item.Value == "LI_Order")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Order_List")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderList','login');", true);
                }
                else if (e.Item.Value == "OrderMIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderMIS','login');", true);
                }
                else if (e.Item.Value == "OrderRecon")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderRecon','login');", true);
                }
                else if (e.Item.Value == "AddAssociates")
                {
                    //Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociates", "loadcontrol('AddAssociates','login');", true);
                }
                else if (e.Item.Value == "ViewAssociates")
                {
                    // Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAssociates','login');", true);
                }
                else if (e.Item.Value == "ViewAssociatess")
                {
                    // Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAdviserAssociateList','login');", true);
                }

                else if (e.Item.Value == "AddAgentCode")
                {
                    //  Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation','login');", true);
                }
                else if (e.Item.Value == "ViewAgentCode")
                {
                    // Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
                }
                else if (e.Item.Value == "View_Receivable_structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureRuleGrid','none');", true);
                }
                else if (e.Item.Value == "Receivable_Strucrure_setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReceivableSetup','none');", true);
                }
                else if (e.Item.Value == "View_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewSchemeStructureAssociation','none');", true);
                }

                else if (e.Item.Value == "Receivable_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SchemeStructureRuleAssociation','none');", true);
                }
                else if (e.Item.Value == "View_Payable_Structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "Payable_Structure_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "ViewAgentCode")
                {
                    Session["UserType"] = "ops";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
                }
                else if (e.Item.Value == "Map_scheme")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureToSchemeMapping','login');", true);
                }
                else if (e.Item.Value == "Scheme_DataTrans_Mapping")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddSchemeMapping','login')", true);
                }
                else if (e.Item.Value == "Extract")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderExtract','login');", true);
                }
                else if (e.Item.Value == "File_Extraction")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderAccountingExtract','login');", true);
                }
                else if (e.Item.Value == "RTA_Extract")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderExtract','login');", true);
                }
                else if (e.Item.Value == "NCD_Extract")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineIssueExtract','login');", true);
                }
                else if (e.Item.Value == "NCD/IPO Accounting")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderNCDIPOAccountingExtract','login');", true);
                }

                else if (e.Item.Value == "File_Generation")
                {
                    Session["UserType"] = "adviser";

                }
                else if (e.Item.Value == "MF_Online_SIP_Ord_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerSIPOrderBook','login');", true);

                }
                else if (e.Item.Value == "MF_Online_OrderBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerOrderBook','login');", true);

                }
                else if (e.Item.Value == "SIP_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderBackOffice','login');", true);
                }
                else if (e.Item.Value == "Scheme_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineSchemeSetUp','login');", true);

                }
                else if (e.Item.Value == "NCDIssuesetup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDIssueSetup','login');", true);
                }
                else if (e.Item.Value == "NCDIssueList")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDIssueList','login');", true);
                }
                else if (e.Item.Value == "View_Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineSchemeMIS','login');", true);

                }
                else if (e.Item.Value == "NCD_Order_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserNCDOrderBook','login');", true);

                }
                else if (e.Item.Value == "IPO_Order_Book")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerIPOOrderBook','login');", true);

                }
                else if (e.Item.Value == "MF_Transaction_Books")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineAdviserCustomerTransctionBook','login');", true);

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



        private DataSet GetTreeNodesBasedOnUserRoles(string treeType)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetTreeNodesBasedOnUserRoles(treeType, advisorVo.advisorId);
            return dsTreeNodes;
        }

        private DataSet GetAdviserRoleTreeNodes(int adviserId)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetAdviserRoleTreeNodes(adviserId);
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
                            //if (dr[2].ToString().ToLower() == "content" || dr[2].ToString().ToLower() == "message" || dr[2].ToString().ToLower() == "order")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}

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
                            //if (dr[2].ToString() == "Goal MIS"
                            //    || dr[2].ToString() == "FP Report" || dr[2].ToString() == "Asset Allocation MIS"
                            //    || dr[2].ToString() == "MF Turnover MIS" || dr[2].ToString() == "MF Dashboard"
                            //    || dr[2].ToString() == "Customer Accounts Compare" || dr[2].ToString() == "Returns" || dr[2].ToString() == "Customer SignUp" || dr[2].ToString() == "Commission")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            //if (dr[2].ToString() == "Login History" || dr[2].ToString() == "MF NP & Tranx Compare")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            //if (dr[2].ToString().ToLower() == "message")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            if (dr[2].ToString() == "Goal MIS"
                                || dr[2].ToString() == "FP Report" || dr[2].ToString() == "Asset Allocation MIS"
                                || dr[2].ToString() == "MF Turnover MIS" || dr[2].ToString() == "Returns")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
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
                        //else
                        //{
                        //    dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                        //    Item.Text = dr[2].ToString();
                        //    if (dr[2].ToString().ToLower() == "message")
                        //    {
                        //        Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                        //    }
                        //}
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
                            //if (dr[2].ToString() == "Goal MIS"
                            //    || dr[2].ToString() == "FP Report" || dr[2].ToString() == "Asset Allocation MIS"
                            //    || dr[2].ToString() == "MF Turnover MIS" || dr[2].ToString() == "Generate ISA"
                            //    || dr[2].ToString() == "ISA Status" || dr[2].ToString() == "Returns" || dr[2].ToString() == "Commission")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            //if (dr[2].ToString().ToLower() == "content" || dr[2].ToString().ToLower() == "message" || dr[2].ToString().ToLower() == "order")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            if (dr[2].ToString() == "Goal MIS"
                                || dr[2].ToString() == "FP Report" || dr[2].ToString() == "Asset Allocation MIS"
                                || dr[2].ToString() == "MF Turnover MIS"
                                || dr[2].ToString() == "ISA Status" || dr[2].ToString() == "MF Dashboard"
                                || dr[2].ToString() == "ISA Folio Mapping" || dr[2].ToString() == "Customer Accounts Compare" || dr[2].ToString() == "Returns" || dr[2].ToString() == "Customer SignUp")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
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
                            if (dr[2].ToString() == "Login History" || dr[2].ToString() == "MF NP & Tranx Compare")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }

                        }
                    }
                }
            }
            else if (userRole == "Associates")
            {
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };
                foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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
                            if (dr[2].ToString() == "Queries")
                            {
                                Item.Visible = false;
                            }
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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
                            if (dr[2].ToString() == "Add FD&54EC order" || dr[2].ToString() == "Customer AUM")
                            {
                                Item.Visible = false;
                            }
                            //if (dr[2].ToString() == "Goal MIS"
                            //    || dr[2].ToString() == "FP Report" || dr[2].ToString() == "Asset Allocation MIS"
                            //    || dr[2].ToString() == "MF Turnover MIS"
                            //    || dr[2].ToString() == "ISA Status" || dr[2].ToString() == "MF Dashboard"
                            //    || dr[2].ToString() == "ISA Folio Mapping" || dr[2].ToString() == "Customer Accounts Compare" || dr[2].ToString() == "Returns" || dr[2].ToString() == "Customer SignUp")
                            //{
                            //    Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
                        }
                    }
                }

                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[2]);
                tempView.Sort = "WTSSN_TreeSubSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[2].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[2].Columns["WTSSN_TreeSubSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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
                            if (dr[2].ToString() == "Login History" || dr[2].ToString() == "MF NP & Tranx Compare")
                            {
                                Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }

                        }
                    }
                }
            }



        }

        private void SetAdminTreeNodesForPlans(DataSet dsAdminTreeNodes)
        {
            int flag = 0;
            DataView tempView;
            //int isMultiBranch = 0;
            //int isStaffEnabled = 0;
            //string noOfAgents;

            tempView = new DataView(dsAdminTreeNodes.Tables[0]);
            tempView.Sort = "WTN_TreeNode";

            if (userVo.RoleList.Contains("Admin"))
            {
                //Session["PlanDetails"] = dsAdminTreeNodes.Tables[3];
                //isMultiBranch = int.Parse(dsAdminTreeNodes.Tables[3].Rows[0]["AS_NoOfStaffWebLogins"].ToString()); 
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
                        //else if (advisorVo.IsOpsEnable == 1)
                        //{
                        //    switch (Item.Value)
                        //    {
                        //        case "Customer":
                        //            Item.Visible = false;
                        //            break;
                        //        //case "Operations":
                        //        //    Item.Visible = false;
                        //        //    break;
                        //        case "Returns_Analytics":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Queries":
                        //            Item.Visible = false;
                        //            break;
                        //        case "AUM_Holdings":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Business MIS":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Order_Management":
                        //            Item.Visible = false;
                        //            break;
                        //    }
                        //}

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
                        //else if (advisorVo.IsOpsEnable == 1)
                        //{
                        //    switch (Item.Value)
                        //    {
                        //        case "Upload":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Daily Process":
                        //            Item.Visible = false;
                        //            break;
                        //        case "MF Systematic Daily Recon":
                        //            Item.Visible = false;
                        //            break;
                        //        case "MF Reversal Txn Exception Handling":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Reconciliation":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Add EQ Transactions":
                        //            Item.Visible = false;
                        //            break;

                        //        case "Valuation":
                        //            Item.Visible = false;
                        //            break;

                        //        case "Loan Schemes":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Loan Partner Commission":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Add Customer":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Add FP Prospect":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Manage Portfolio":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Manage Group Account":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Customer Association":
                        //            Item.Visible = false;
                        //            break;

                        //        case "FP Offline Form":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Associates":
                        //            Item.Visible = false;
                        //            break;
                        //        case "Commissions":
                        //            Item.Visible = false;
                        //            break;



                        //    }
                        // }
                        //if (int.Parse(dsAdminTreeNodes.Tables[3].Rows[0]["WP_IsOtherStaffEnabled"].ToString()) == 0)
                        //{
                        //    if (Item.Value == "Staff")
                        //        Item.Visible = false;
                        //}
                        if (int.Parse(dsAdminTreeNodes.Tables[3].Rows[0]["WP_IsMultiBranchPlan"].ToString()) == 0)
                        {
                            if (Item.Value == "Branch/Association")
                                Item.Visible = false;
                        }
                        if (string.IsNullOrEmpty(dsAdminTreeNodes.Tables[3].Rows[0]["WP_NoOfAgents"].ToString().Trim()))
                        {
                            if (Item.Value == "Setup Associate Category")
                                Item.Visible = false;
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
                        if (int.Parse(dsAdminTreeNodes.Tables[3].Rows[0]["WP_IsOtherStaffEnabled"].ToString()) == 0)
                        {

                            switch (Item.Value)
                            {
                                //case "Add Staff":
                                //    Item.Visible = false;
                                //    break;
                                case "Staff User Management":
                                    Item.Visible = false;
                                    break;
                            }
                        }
                        if (int.Parse(dsAdminTreeNodes.Tables[3].Rows[0]["WP_IsMultiBranchPlan"].ToString()) == 0)
                        {
                            switch (Item.Value)
                            {
                                case "Add Branch":
                                    Item.Visible = false;
                                    break;
                                case "View Branch Association":
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
                if (userVo.RoleList.Contains("Associates"))
                {
                    flag = 0;
                    tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                    tempView.Sort = "WTN_TreeNode";

                    foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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

                    foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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

                    foreach (RadPanelItem Item in RadPanelBar6.GetAllItems())
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
        protected void RadPanelBar6_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            RadPanelBar1.CollapseAllItems();
            RadPanelBar3.CollapseAllItems();
            Session[SessionContents.CurrentUserRole] = "Associates";
            hdfSession.Value = "Associates";
            try
            {
                if (e.Item.Value == "RM Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SalesDashBoard','login');", true);
                }
                else if (e.Item.Value == "54EC_ORDER_bOOK")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderBook','login');", true);
                }
                else if (e.Item.Value == "View_Associate_Payable_Rule")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PayableStructureView','login');", true);
                }
                else if (e.Item.Value == "NCD_IPO_Allotments")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineNCDOrderMatchExceptionHandling','login');", true);
                }
                else if (e.Item.Value == "Add_NCD_Order")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "NCD_Offline_Book")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersNCDOrderBook','login');", true);
                }
                else if (e.Item.Value == "Add_IPO_Order")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueTransactOffline','login');", true);
                }
                else if (e.Item.Value == "IPO_Offline_Order_Book")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineCustomersIPOOrderBook','login');", true);
                }
                else if (e.Item.Value == "MFT")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "ViewAssociates")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAssociates','login');", true);
                }
                else if (e.Item.Value == "ViewAssociatess")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAssociates", "loadcontrol('ViewAdviserAssociateList','login');", true);
                }
                else if (e.Item.Value == "AddAgentCode")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation','login');", true);
                }
                else if (e.Item.Value == "ViewAgentCode")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
                }
                else if (e.Item.Value == "Profile")
                {
                    Session["action"] = "View";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAssociatesDetails','login');", true);
                }
                else if (e.Item.Value == "CustomerList")
                {
                    Session["Customer"] = "Customer";
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
                }
                else if (e.Item.Value == "Customer_Report")
                {
                    Session["UserType"] = "Associates";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerReportsDashBoard','login');", true);
                }
                else if (e.Item.Value == "View_Customer_Association")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssociateCustomerList','login');", true);
                }
                else if (e.Item.Value == "Prospect List")
                {
                    Session["UserType"] = "Associates";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (e.Item.Value == "OrderEntry")
                {
                    if (advisorVo.A_AgentCodeBased == 1)
                    {
                        Session["UserType"] = "Associates";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFOrderEntry','login');", true);
                    }
                    else
                    {
                        Session["UserType"] = "Associates";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderDashBoard','login');", true);
                    }
                }
                else if (e.Item.Value == "ProductOrderMaster")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderEntry','login');", true);
                }
                else if (e.Item.Value == "LI_Order")
                {
                    Session["UserType"] = "adviser";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LifeInsuranceOrderEntry','login');", true);
                }
                else if (e.Item.Value == "Order_List")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderList','login');", true);
                }
                else if (e.Item.Value == "OrderMIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderMIS','login');", true);
                }
                else if (e.Item.Value == "OrderRecon")
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OrderRecon','login');", true);
                }
                else if (e.Item.Value == "Add FP Prospect")
                {
                    Session["UserType"] = "Associates";
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                else if (e.Item.Value == "Staff")
                {
                    //----------For Existing WERP---------------------------
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
                    //----------For Existing SBI Zone,Cluster,Channel,Team...---------------------------
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewStaff','login');", true);
                }


                else if (e.Item.Value == "MF Folios")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
                }
                else if (e.Item.Value == "Manage Portfolio")
                {
                    Session["UserType"] = "Associates";
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolio','login');", true);
                }
                else if (e.Item.Value == "Add Portfolio")
                {
                    Session["UserType"] = "Associates";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerPortfolioSetup','login');", true);
                }
                else if (e.Item.Value == "Alert Notifications")
                {
                    Session["UserType"] = "Associates";
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','login');", true);
                }
                else if (e.Item.Value == "Transactions")
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
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                }
                else if (e.Item.Value == "Add EQ Transactions")
                {
                    Session["UserType"] = "Associates";
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
                }
                else if (e.Item.Value == "Loan Proposal")
                {
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                }
                else if (e.Item.Value == "Add Loan proposal")
                {
                    Session["LoanProcessAction"] = "add";
                    Session[SessionContents.LoanProcessTracking] = null;
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanProcessTracking','login');", true);
                }
                else if (e.Item.Value == "FP Offline Form")
                {
                    Session["UserType"] = "Associates";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OfflineForm','login');", true);
                }
                else if (e.Item.Value == "MF MIS")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "MFDashBoard")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                }
                //else if (e.Item.Value == "MFDashboard")
                //{
                //    Session["UserType"] = "Associates";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);

                //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFDashBoard','login');", true);
                //}
                else if (e.Item.Value == "CustomerSignUp")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserNewSignupMIS','login');", true);
                }

                else if (e.Item.Value == "MF Commission MIS")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
                }
                else if (e.Item.Value == "MF systematic MIS")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_MIS');", true);
                }
                else if (e.Item.Value == "MF_SIP_Projection")
                {
                    Session["UserType"] = "Associates";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFSIPProjection','login');", true);
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserRMMFSystematicMIS','action=SIP_Projection');", true);
                }
                else if (e.Item.Value == "Performance_Allocation")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PerformanceAndAllocation','login');", true);
                }
                else if (e.Item.Value == "Equity MIS")
                {
                    Session["UserType"] = "rm";
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (e.Item.Value == "Goal_MIS")
                {
                    Session["UserType"] = "Associates";
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('GoalMIS','login');", true);
                }
                else if (e.Item.Value == "Asset_Allocation_MIS")
                {
                    Session["UserType"] = "Associates";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AssetAllocationMIS','login');", true);
                }
                else if (e.Item.Value == "MFTurnOverMIS")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('TurnOverDashBoard','login');", true);
                }
                else if (e.Item.Value == "Loan MIS")
                {
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UnderConstruction", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (e.Item.Value == "Customer_AUM")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAUM','login');", true);
                }
                else if (e.Item.Value == "Customer_Holdings")
                {
                    Session["UserType"] = "Associates";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MutualFundMIS','login');", true);
                }
                else if (e.Item.Value == "View_Receivable_structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureRuleGrid','none');", true);
                }
                else if (e.Item.Value == "Receivable_Strucrure_setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReceivableSetup','none');", true);
                }
                else if (e.Item.Value == "View_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewSchemeStructureAssociation','none');", true);
                }
                else if (e.Item.Value == "Receivable_Scheme_Structure_Association")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SchemeStructureRuleAssociation','none');", true);
                }
                else if (e.Item.Value == "View_Payable_Structure")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "Payable_Structure_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('','none');", true);
                }
                else if (e.Item.Value == "Map_scheme")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionStructureToSchemeMapping','login');", true);
                }
                else if (e.Item.Value == "Brokerage_Received")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CommissionReconMIS','login');", true);
                }
                else if (e.Item.Value == "Transactions")
                {
                    Session["UserType"] = "Associates";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (e.Item.Value == "Multi Asset Report")
                {
                    Session["UserType"] = "Associates";
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioReports','login');", true);
                }
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "adviser";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFReports", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "Equity Report")
                {
                    Session["UserType"] = "adviser";
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EquityReports", "loadcontrol('EquityReports','login');", true);
                }
                else if (e.Item.Value == "FP Report")
                {
                    Session["UserType"] = "adviser";
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
                }
                else if (e.Item.Value == "Inbox")
                {
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "Compose")
                {
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageCompose','login');", true);
                }
                else if (e.Item.Value == "Outbox")
                {
                    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageOutbox','login');", true);
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
        public void imgButton_OnClick(object sender, ImageClickEventArgs e)
        {
            int orderId = 0, isonline = 0; string productcode = string.Empty; string subCategoryType = string.Empty; string Type = string.Empty;
            OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
            if (ddlSearchtype.SelectedValue == "ON")
            {
                if (txtOrderNo.Text != "")
                {
                    DataTable dtOrderNo = OnlineOrderBackOfficeBo.SearchOnPRoduct(int.Parse(txtOrderNo.Text),0);
                    foreach (DataRow dr in dtOrderNo.Rows)
                    {
                        orderId = int.Parse(dr["CO_OrderId"].ToString());
                        isonline = int.Parse(dr["CO_IsOnline"].ToString());
                        productcode = dr["PAG_AssetGroupCode"].ToString();
                        subCategoryType = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        Type = dr["WMTT_TransactionClassificationCode"].ToString();
                    }

                    if (productcode == "MF" && isonline == 1 && Type != "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerOrderBook", "loadcontrol('OnlineAdviserCustomerOrderBook','?orderId=" + orderId + "&txtFolioNo=" + null + "');", true);

                    }
                    if (productcode == "MF" && isonline == 1 && Type == "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerSIPOrderBook", "loadcontrol('OnlineAdviserCustomerSIPOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "FI" && isonline == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserNCDOrderBook", "loadcontrol('OnlineAdviserNCDOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "IP" && isonline == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerIPOOrderBook", "loadcontrol('OnlineAdviserCustomerIPOOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "MF" && isonline == 0 && Type != "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderList", "loadcontrol('OrderList','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "FI" && isonline == 0 && subCategoryType != "FICDCD" && subCategoryType != "FICGCG" && subCategoryType != "FINPNP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OfflineCustomersNCDOrderBook", "loadcontrol('OfflineCustomersNCDOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "IP" && isonline == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OfflineCustomersIPOOrderBook", "loadcontrol('OfflineCustomersIPOOrderBook','?orderId=" + orderId + "');", true);
                    }
                    if (productcode == "FI" && isonline == 0 && (subCategoryType == "FICDCD" || subCategoryType == "FICGCG" || subCategoryType == "FINPNP"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderBook", "loadcontrol('FixedIncome54ECOrderBook','?orderId=" + orderId + "&category=" + subCategoryType + "');", true);
                    }
                }
            }
            else if (ddlSearchtype.SelectedValue == "FNO")
            {
                if (txtFolioNo.Text != "")
                {
                    DataTable dtFolio = OnlineOrderBackOfficeBo.GetProductSearchType(txtFolioNo.Text);
                    foreach (DataRow dr in dtFolio.Rows)
                    {
                        isonline = int.Parse(dr["CO_IsOnline"].ToString());
                        productcode = dr["PAG_AssetGroupCode"].ToString();
                        Type = dr["WMTT_TransactionClassificationCode"].ToString();
                    }
                    if (productcode == "MF" && isonline == 1 && Type != "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerOrderBook", "loadcontrol('OnlineAdviserCustomerOrderBook','?txtFolioNo=" + txtFolioNo.Text + "&orderId=" + 0 + "');", true);

                    }
                    if (productcode == "MF" && isonline == 1 && Type == "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerSIPOrderBook", "loadcontrol('OnlineAdviserCustomerSIPOrderBook','?txtFolioNo=" + txtFolioNo.Text + "&orderId=" + 0 + "');", true);

                    }
                    if (productcode == "MF" && isonline == 0 && Type != "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderList", "loadcontrol('OrderList','?txtFolioNo=" + txtFolioNo.Text + "&orderId=" + 0 + "');", true);

                    }

                }
                else
                {

                }
            }
            else if (ddlSearchtype.SelectedValue == "APPNO")
            {
                if (txtAppno.Text != "")
                {
                    DataTable dtOrderNo = OnlineOrderBackOfficeBo.SearchOnPRoduct(0,int.Parse(txtAppno.Text));
                    foreach (DataRow dr in dtOrderNo.Rows)
                    {
                        orderId = int.Parse(dr["CO_OrderId"].ToString());
                        isonline = int.Parse(dr["CO_IsOnline"].ToString());
                        productcode = dr["PAG_AssetGroupCode"].ToString();
                        subCategoryType = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        Type = dr["WMTT_TransactionClassificationCode"].ToString();
                    }
                    if (productcode == "FI" && isonline == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserNCDOrderBook", "loadcontrol('OnlineAdviserNCDOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "IP" && isonline == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineAdviserCustomerIPOOrderBook", "loadcontrol('OnlineAdviserCustomerIPOOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "FI" && isonline == 0 && subCategoryType != "FICDCD" && subCategoryType != "FICGCG" && subCategoryType != "FINPNP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OfflineCustomersNCDOrderBook", "loadcontrol('OfflineCustomersNCDOrderBook','?orderId=" + orderId + "');", true);

                    }
                    if (productcode == "IP" && isonline == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OfflineCustomersIPOOrderBook", "loadcontrol('OfflineCustomersIPOOrderBook','?orderId=" + orderId + "');", true);
                    }
                    if (productcode == "FI" && isonline == 0 && (subCategoryType == "FICDCD" || subCategoryType == "FICGCG" || subCategoryType == "FINPNP"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderBook", "loadcontrol('FixedIncome54ECOrderBook','?orderId=" + orderId + "&category=" + subCategoryType + "');", true);
                    }
                }
            }
            else
            {
                if (txtClentCode.Text != "")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer','?CustCode=" + txtClentCode.Text + "&customerId=" + ddlSearchtype.SelectedValue + "');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer');", true);

            }
        }
        protected void ddlSearchtype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtClentCode.Text = "";
            if (ddlSearchtype.SelectedValue == "ON")
            {
                tdOrderNo.Visible = true;
                trClientCode.Visible = false;
                tdFolioNo.Visible = false;
                tdtxtAppno.Visible = false;
            }
            else if (ddlSearchtype.SelectedValue == "FNO")
            {
                tdOrderNo.Visible = false;
                trClientCode.Visible = false;
                tdFolioNo.Visible = true;
                tdtxtAppno.Visible = false;

            }
            else if (ddlSearchtype.SelectedValue == "APPNO")
            {
                tdOrderNo.Visible = false;
                trClientCode.Visible = false;
                tdFolioNo.Visible = false;
                tdtxtAppno.Visible = true;
            }
            else
            {
                tdOrderNo.Visible = false;
                trClientCode.Visible = true;
                tdFolioNo.Visible = false;
                tdtxtAppno.Visible = false;
            }
        }
    }
}
