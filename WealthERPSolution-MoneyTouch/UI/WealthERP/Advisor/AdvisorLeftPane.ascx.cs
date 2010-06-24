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
                    TreeView1.FindNode("Branch").ChildNodes.RemoveAt(0);
                }
                TreeView1.CollapseAll();
            }
        }

        protected void btnSearchRM_Click(object sender, EventArgs e)
        {

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNodeValue = null;
            if (TreeView1.SelectedNode.Value == "Switch Roles")
            {
                logoPath = Session[SessionContents.LogoPath].ToString();

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
            else if (TreeView1.SelectedNode.Value == "Advisor Home")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Profile")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorProfile','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Edit Profile")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditAdvisorProfile','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Edit User Details")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Branch")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Add Branch")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranch','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "LOB")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewLOB','login');", true);
            }
            //else if (TreeView1.SelectedNode.Value.ToString() == "Add LOB")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddLOB','login');", true);
            //}
            else if (TreeView1.SelectedNode.Value.ToString() == "Staff")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Valuation")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('DailyValuation','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Add Staff")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddRM','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Add Branch Association")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMBranchAssociation','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "RM Details")
            {
                Session["UserManagement"] = "RM";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMUserDetails','login');", true);
            }

            else if (TreeView1.SelectedNode.Value.ToString() == "Customer Details")
            {
                //Session["Customer"] = "Customer";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Customer")
            {
                Session["Customer"] = "Customer";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Customer Accounts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorCustomerAccounts','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "View Branch Association")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Uploads")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerUpload','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Process Log")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Rejected Records")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);
            }

            else if (TreeView1.SelectedNode.Value.ToString() == "Reject Folios")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedFoliosUploads','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "Rejected Transactions")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTransactions','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "LoanMIS")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "SetupAssociateCategory")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserAssociateCategorySetup','login');", true);

            }

            else if (TreeView1.SelectedNode.Value.ToString() == "AdviserLoanCommsnStrucWithLoanPartner")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

            }

            else if (TreeView1.SelectedNode.Value.ToString() == "AdviserLoanCommsnStrucWithLoanPartner")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','login');", true);

            }
            else if (TreeView1.SelectedNode.Value.ToString() == "AdviserStaffSMTP")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserStaffSMTP','login');", true);

            }

            else if (TreeView1.SelectedNode.Value.ToString() == "Schemes")
            {
                Session["LoanSchemeView"] = "Advisor";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','login');", true);
            }
            //else if (TreeView1.SelectedNode.Value.ToString() == "Add Schemes")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','login');", true);
            //}
            //else if (TreeView1.SelectedNode.Value.ToString() == "Set Theme")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SetTheme','login');", true);
            //}
            else if (TreeView1.SelectedNode.Value.ToString() == "LeadManagement")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagement','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "LeadManagementAdd")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LeadManagementAdd','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MFReports")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFReports','login');", true);
                
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "EquityReports")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityReports','none');", true);
            }

            else if (TreeView1.SelectedNode.Value.ToString() == "CustomerSMSAlerts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerSMSAlerts','none');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "SendSMS")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomerManualSMS','none');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "CustomerMFSystematicTransactionReport")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MFReversal")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ReverseTransactionExceptionHandling','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MFMIS")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "EView Transactions")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "EAdd Transactions")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "EMIS")
            {
                Session["UserType"] = "rm";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MView Transactions")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','none');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MMIS")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAMCSchemewiseMIS','login');", true);
            }
            else if (TreeView1.SelectedNode.Value.ToString() == "MAddTransactions")
            {
                Session["UserType"] = "adviser";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
            }
            if (TreeView1.SelectedNode.Parent == null)
            {
                foreach (TreeNode node in TreeView1.Nodes)
                {
                    if (node.Value != TreeView1.SelectedNode.Value)
                        node.Collapse();
                    else
                        node.Expand();
                }
            }
            else
            {
                if (TreeView1.SelectedNode.Parent.Parent != null)
                {
                    string parentNode = TreeView1.SelectedNode.Parent.Parent.Value;
                    foreach (TreeNode node in TreeView1.Nodes)
                    {
                        if (node.Value != parentNode)
                            node.Collapse();
                    }
                }
                else
                {
                    if (TreeView1.SelectedNode.Parent == null)
                    {
                        foreach (TreeNode node in TreeView1.Nodes)
                        {
                            if (node.Value != TreeView1.SelectedNode.Value)
                                node.Collapse();
                            else
                                node.Expand();
                        }
                    }
                    else
                    {
                        strNodeValue = TreeView1.SelectedNode.Parent.Value;
                        string val = TreeView1.SelectedNode.Value;
                        foreach (TreeNode node in TreeView1.Nodes)
                        {

                            if (node.Value != strNodeValue)
                                node.Collapse();
                            else
                            {
                                foreach (TreeNode child in node.ChildNodes)
                                {
                                    if (child.Value != val)
                                        child.Collapse();
                                    else
                                        child.Expand();
                                }
                            }

                        }
                    }
                }
            }
            //if (TreeView1.SelectedNode.Parent == null)
            //{
            //    foreach (TreeNode node in TreeView1.Nodes)
            //    {
            //        if (node.Value != TreeView1.SelectedNode.Value)
            //            node.Collapse();
            //        else
            //            node.Expand();
            //    }
            //}
            //else
            //{
            //    string strNodeValue = TreeView1.SelectedNode.Parent.Value;

            //    foreach (TreeNode node in TreeView1.Nodes)
            //    {
            //        if (node.Value != strNodeValue)
            //            node.Collapse();
            //    }

            //}
        }
    }
}