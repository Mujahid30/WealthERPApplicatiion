﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;
using BoUser;
using BoCommon;
using BoCustomerProfiling;


namespace WealthERP.Advisor
{
    public partial class RMIndividualCustomerLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();

        List<string> roleList = new List<string>();
        int count;
        UserBo userBo = new UserBo();
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string First = null;
            string Middle = null;
            string Last = null;
            userVo = (UserVo)Session["userVo"];
            bool isGrpHead = false;

            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

                if (!IsPostBack)
                {
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        hdnUserRole.Value = "RM";
                    else
                        hdnUserRole.Value = "Adviser";
                    if (Middle != "")
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                    }
                    else
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    }

                    lblEmailIdValue.Text = customerVo.Email.ToString();

                    isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                    if (isGrpHead == true)
                    {
                        TreeView1.Nodes.AddAt(1, new TreeNode("Group Dashboard"));
                    }

                    string IsDashboard = string.Empty;

                    if (Session["IsDashboard"] != null)
                        IsDashboard = Session["IsDashboard"].ToString();
                    if (IsDashboard == "true")
                    {
                        TreeView1.CollapseAll();

                        if (isGrpHead == true)
                        {
                            TreeView1.FindNode("Group Dashboard").Selected = true;
                        }
                        else
                        {
                            TreeView1.FindNode("Customer Dashboard").Selected = true;
                        }
                        Session["IsDashboard"] = "false";

                        
                    }
                    else if (IsDashboard == "CustDashboard")
                    {
                        TreeView1.CollapseAll();
                        TreeView1.FindNode("Customer Dashboard").Selected = true;
                        Session["IsDashboard"] = "false";
                    }
                    else
                    {
                        TreeView1.CollapseAll();
                    //    TreeView1.FindNode("Profile Dashboard").Expand();
                    //    TreeView1.FindNode("Profile Dashboard").Selected = true;
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('CustomerIndividualLeftPane');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:Page_Load()");

                object[] objects = new object[4];

                objects[0] = customerVo;
                objects[1] = First;
                objects[2] = Middle;
                objects[3] = Last;

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

                if (TreeView1.SelectedNode.Value == "RM Home")
                {
                    if(Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Group Dashboard")
                {
                    Session["IsDashboard"] = "true";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Customer Dashboard")
                {
                    Session["IsDashboard"] = "CustDashboard";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Equity")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolios', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView', 'none')", true);

                }
                else if (TreeView1.SelectedNode.Value == "View MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('TransactionsView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MFManualSingleTran', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=');", true);
                    // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioFixedIncomeView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=FI')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGovtSavings', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=GS')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioProperty', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PR')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Pension And Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PensionPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Pension and Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PG')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Personal Assets")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Personal Assets")
                {
                    Session.Remove("personalVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioPersonalEntry', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGoldEntry', '?action=GoldEntry')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioCollectiblesEntry', '?action=Col')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Cash And Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Cash and Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=CS')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Register Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry', '?action=entry')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Notifications")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "MF Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAlert','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "FI Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerFIAlerts','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Insurance Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerInsuranceAlerts','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Equity Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAlerts','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewNonIndividualProfile','none');", true);
                    }
                }
                else if (TreeView1.SelectedNode.Value == "Edit Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EditCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
                    }
                }
                else if (TreeView1.SelectedNode.Value == "Proof")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCustomerProofs','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Proof")
                {
                    Session["FlagProof"] = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerProofsAdd','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBankDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Demat Account Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Demat Account")
                {
                    Session["DematDetailsView"] = "Add";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
                }
                
                else if (TreeView1.SelectedNode.Value == "Add Liability")
                {
                    Session["menu"] = null;
                    Session.Remove("personalVo");
                    Session.Remove("propertyVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Liabilities Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilityView','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Income Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerIncome','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Expense Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerExpense','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Advisor Notes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAdvisorsNote','none');", true);
                }                    
                else if (TreeView1.SelectedNode.Value == "General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceAccountAdd','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Life Insurance")
                {
                    Session.Remove("table");
                    Session.Remove("insuranceVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=IN')", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Life Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','none');", true);
                }

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

                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

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
