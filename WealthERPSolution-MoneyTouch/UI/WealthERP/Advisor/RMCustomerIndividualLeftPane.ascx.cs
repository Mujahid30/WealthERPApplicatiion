using System;
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


namespace WealthERP.Advisor
{
    public partial class RMIndividualCustomerLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
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
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

                if (!IsPostBack)
                {
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();

                    if (Middle != "")
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                    }
                    else
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    }

                    lblEmailIdValue.Text = customerVo.Email.ToString();


                    string IsDashboard = string.Empty;

                    if (Session["IsDashboard"] != null)
                        IsDashboard = Session["IsDashboard"].ToString();
                    if (IsDashboard == "true")
                    {
                        TreeView1.CollapseAll();

                        TreeView1.FindNode("Customer Dashboard").Selected = true;
                        Session["IsDashboard"] = "false";
                    }
                    else
                    {
                        TreeView1.CollapseAll();
                        TreeView1.FindNode("Profile Dashboard").Expand();
                        TreeView1.FindNode("Profile Dashboard").Selected = true;
                    }

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('RMCustomerIndividualLeftPane');", true);
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

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNodeValue = null;
            try
            {
                //if (TreeView1.SelectedNode.Value == "Home")
                //{

                //    roleList = userBo.GetUserRoles(userVo.UserId);
                //    count = roleList.Count;
                //    if (count == 3)
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);

                //    }
                //    if (count == 2)
                //    {
                //        for (int i = 0; i < 2; i++)
                //        {
                //            if (roleList[i] == "RM")
                //            {

                //                ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMDashBoard','none');", true);
                //            }
                //            if (roleList[i] == "BM")
                //            {
                //                ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('AdvisorBMDashBoard','none');", true);
                //            }
                //        }
                //    }


                //}

                //else 
                if (TreeView1.SelectedNode.Value == "RM Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Customer Dashboard")
                {
                    Session["IsDashboard"] = "true";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
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
                //else if (TreeView1.SelectedNode.Value == "Group Accounts")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
                //}
                //else if (TreeView1.SelectedNode.Value == "Add Group Member")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('FamilyDetails','none');", true);
                //}
                //else if (TreeView1.SelectedNode.Value == "Associate Member")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAssociatesAdd','none');", true);
                //}
                //else if (TreeView1.SelectedNode.Value == "Portfolio Details")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);
                //}
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
                //    if (TreeView1.SelectedNode.Parent.Parent != null)
                //    {
                //        string parentNode = TreeView1.SelectedNode.Parent.Parent.Value;
                //        foreach (TreeNode node in TreeView1.Nodes)
                //        {
                //            if (node.Value != parentNode)
                //                node.Collapse();
                //        }
                //    }
                //    else
                //    {
                //        if (TreeView1.SelectedNode.Parent == null)
                //        {
                //            foreach (TreeNode node in TreeView1.Nodes)
                //            {
                //                if (node.Value != TreeView1.SelectedNode.Value)
                //                    node.Collapse();
                //                else
                //                    node.Expand();
                //            }
                //        }
                //        else
                //        {
                //            strNodeValue = TreeView1.SelectedNode.Parent.Value;
                //            foreach (TreeNode node in TreeView1.Nodes)
                //            {
                //                if (node.Value != strNodeValue)
                //                    node.Collapse();
                //            }
                //        }
                //    }
                //}
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
    }
}
