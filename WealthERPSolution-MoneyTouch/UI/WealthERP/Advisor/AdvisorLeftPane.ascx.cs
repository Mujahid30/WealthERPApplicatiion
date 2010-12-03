using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdvisorLeftPane : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        UserBo userBo = new UserBo();
        UserVo userVo;
        int count;
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
            if (!IsPostBack)
            {
                if (advisorVo.MultiBranch != 1)
                {
                    RadPanelBar1.FindItemByValue("Add Branch").Visible = false;
                        //FindNode("Branch").ChildNodes.RemoveAt(0);
                }
                RadPanelBar1.CollapseAllItems();
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
        //        Session["UserType"] = "Adviser";
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
        //        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
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
        //    else if (TreeView1.SelectedNode.Value.ToString() == "CommissionMIS")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
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

        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            if (e.Item.Value == "Switch Roles")
            {
                logoPath = Session[SessionContents.LogoPath].ToString();
                Session[SessionContents.CurrentUserRole] = null;
                roleList = userBo.GetUserRoles(userVo.UserId);
                count = roleList.Count;
                if (count == 3)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loginloadcontrol('IFAAdminDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                }
                else if (count == 2)
                {
                    if (roleList.Contains("RM") && roleList.Contains("Admin"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);
                    }
                    else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorBMDashBoard','login','" + userVo.FirstName + userVo.LastName + "','" + logoPath + "');", true);

                    }
                }
                // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);
            }
            else if (e.Item.Value == "Advisor Home")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
            else if (e.Item.Value == "Profile")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','login');", true);
            }
            else if (e.Item.Value == "Edit Profile")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditAdvisorProfile','login');", true);
            }
            else if (e.Item.Value == "Edit User Details")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','login');", true);
            }
            else if (e.Item.Value == "Branch")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','login');", true);
            }
            else if (e.Item.Value == "Add Branch")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranch','login');", true);
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
            else if (e.Item.Value == "Valuation")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('DailyValuation','login');", true);
            }
            else if (e.Item.Value == "Add Staff")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
            }
            else if (e.Item.Value == "Add Branch Association")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMBranchAssociation','login');", true);
            }
            else if (e.Item.Value == "RM Details")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
            }

            else if (e.Item.Value == "Customer Details")
            {
                //Session["Customer"] = "Customer";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
            }
            else if (e.Item.Value == "Customer")
            {
                Session["Customer"] = "Customer";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
            }
            else if (e.Item.Value == "Customer Accounts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
            }
            else if (e.Item.Value == "Association")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CuCustomerAssociationSetup','login');", true);
            }
            else if (e.Item.Value == "View Branch Association")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','login');", true);
            }
            else if (e.Item.Value == "Uploads")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
            }
            else if (e.Item.Value == "Upload History")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
            }
            else if (e.Item.Value == "View Trans Rejects")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);
            }

            else if (e.Item.Value == "View Rejected Folio")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedFoliosUploads','login');", true);
            }
            else if (e.Item.Value == "Rejected Transactions")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTransactions','login');", true);
            }
            else if (e.Item.Value == "LoanMIS")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanMIS','login');", true);
            }
            else if (e.Item.Value == "Setup Associate Category")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);

            }

            else if (e.Item.Value == "Loan Partner Commission")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

            }

            else if (e.Item.Value == "Setup Advisor Staff SMTP")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);

            }

            else if (e.Item.Value == "Schemes")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','login');", true);
            }
            else if (e.Item.Value == "Add Schemes")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','login');", true);
            }
            else if (e.Item.Value == "Set Theme")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SetTheme','login');", true);
            }
            else if (e.Item.Value == "LeadManagement")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagement','login');", true);
            }
            else if (e.Item.Value == "LeadManagementAdd")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagementAdd','login');", true);
            }
            else if (e.Item.Value == "MF Reports")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);

            }
            else if (e.Item.Value == "Equity Reports")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','none');", true);
            }

            else if (e.Item.Value == "Alerts SMS")
            {
                Session["UserType"] = "Adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','none');", true);
            }
            else if (e.Item.Value == "Customized SMS")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerManualSMS','none');", true);
            }
            else if (e.Item.Value == "Systematic Recon")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','login');", true);
            }
            else if (e.Item.Value == "Reversal Trxn Handling")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
            }
            else if (e.Item.Value == "MFMIS")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (e.Item.Value == "EView Transactions")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (e.Item.Value == "EAdd Transactions")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (e.Item.Value == "EMIS")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
            }
            else if (e.Item.Value == "MView Transactions")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
            }
            else if (e.Item.Value == "MMIS")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
            }
            else if (e.Item.Value == "Commission MIS")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorMISCommission','login');", true);
            }
            else if (e.Item.Value == "MAdd Transactions")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
            }
        }
    }
}