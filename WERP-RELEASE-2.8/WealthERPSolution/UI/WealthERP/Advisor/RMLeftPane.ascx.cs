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
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class RMLeftPane : System.Web.UI.UserControl
    {
        List<string> roleList = new List<string>();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        UserBo userBo = new UserBo();
        UserVo userVo;
        int count;
        RMVo rmVo = new RMVo();
        string UserName = "";
        string sourcepath = "";
        string branchLogoSourcePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                Session["dashBoard"] = "RM";
                Session["FromAdvisorView"] = "FromRMView";
                userVo = (UserVo)Session["userVo"];
                UserName = userVo.FirstName + userVo.LastName;
                roleList = userBo.GetUserRoles(userVo.UserId);

                if (!IsPostBack)
                {

                    if (roleList.Count == 1 && roleList.Contains("RM"))
                    {
                        TreeView1.Nodes.RemoveAt(0);
                    }
                    TreeView1.CollapseAll();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('RMLeftPane');", true);
                }

                sourcepath = Session[SessionContents.LogoPath].ToString();
                if (Session[SessionContents.BranchLogoPath] != null)
                    branchLogoSourcePath = Session[SessionContents.BranchLogoPath].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMLeftPane.ascx.cs:Page_Load()");

                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
            {
                SetNode();
            }
        }
        public void SetNode()
        {
            string strNodeValue = null;
            try
            {
                if (TreeView1.SelectedNode.Value.ToString() == "Switch Roles")
                {
                    Session.Remove("UserType");
                    Session[SessionContents.CurrentUserRole] = null;
                    count = roleList.Count;
                    if (count == 3)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('IFAAdminDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                    }
                    if (count == 2)
                    {
                        if (roleList.Contains("RM") && roleList.Contains("BM"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcepath + "','" + branchLogoSourcePath + "');", true);
                        }
                        else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + UserName + "','" + sourcepath + "');", true);
                        }

                    }
                    //   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMBMDashBoard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Dashboard")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewRMDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Customers")
                {
                    Session["Customer"] = "Customer";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Customer")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerType','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Customer Group Accounts")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Group Account")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GroupAccountSetup','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Customer Portfolio")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Portfolio")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolioSetup','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Customer Alerts")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMAlertDashBoard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Notifications")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdviserCustomerSMSAlerts','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "LoanMIS")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserLoanMIS','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Loan Proposal")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Loan Proposal")
                {
                    Session["LoanProcessAction"] = "add";
                    Session[SessionContents.LoanProcessTracking] = null;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanProcessTracking','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "MFReports")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFReports','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "EquityReports")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityReports','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "MultiAssetReports")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioReports','none');", true);
                }
                //else if (TreeView1.SelectedNode.Value.ToString() == "MFMIS")
                //{
                //    Session["UserType"] = "rm";
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
                //}
                else if (TreeView1.SelectedNode.Value.ToString() == "EView Transactions")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMEQMultipleTransactionsView','login');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "EAdd Transactions")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMMultipleEqTransactionsEntry','login');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "EMIS")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "MView Transactions")
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMMultipleTransactionView','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "MMIS")
                {
                    Session["UserType"] = "rm";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "MAdd Transactions")
                {
                    Session["UserType"] = "adviser";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "ProspectList")
                {
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProspectList','login');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "AddProspect")
                {
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_ProspectList_CustomerId);
                    Session.Remove(SessionContents.FPS_AddProspectListActionStatus);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                //else if (TreeView1.SelectedNode.Value == "DownloadForm")
                //{
                //    Response.Write("<script type='text/javascript'>window.open('FP/FinancialPlanning.htm','Print Form');</script>");

                //}
                // Code to Expand/Collapse the Tree View Nodes based on selections
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
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[1];
                objects[0] = strNodeValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}
