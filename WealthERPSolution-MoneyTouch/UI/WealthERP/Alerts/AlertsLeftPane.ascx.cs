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
using BoCommon;
using BoCustomerProfiling;

namespace WealthERP.Alerts
{
    public partial class AlertsLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string First = null;
            string Middle = null;
            string Last = null;
            bool isGrpHead = false;

            try
            {
                if (!IsPostBack)
                {
                    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();

                    isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                    if (isGrpHead == true)
                    {
                        TreeView1.Nodes.AddAt(1, new TreeNode("Group Dashboard"));
                    }

                    if (Middle != "")
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                    }
                    else
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    }
                    TreeView1.CollapseAll();
                    TreeView1.FindNode("Alert Dashboard").Expand();
                    TreeView1.FindNode("Alert Dashboard").ChildNodes[0].Selected = true;
                    
                    lblEmailIdValue.Text = customerVo.Email.ToString();
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
            {
                SetNode();
            }
        }
        public void SetNode()
        {
            string strNodeValue = "";

            if (TreeView1.SelectedNode.Value == "Advisor Home")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
            if (TreeView1.SelectedNode.Value == "Home")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMDashBoard','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Group Dashboard")
            {
                Session["IsDashboard"] = "true";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Customer Dashboard")
            {
                Session["IsDashboard"] = "CustDashboard";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioDashboard','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Alert Dashboard")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertNotifications','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "View Notifications")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertNotifications','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "MF Alerts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFAlert','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "FI Alerts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFIAlerts','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Insurance Alerts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerInsuranceAlerts','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Equity Alerts")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerEQAlerts','none');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Liabilities Dashboard")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('LiabilityView', 'none')", true);
            }
            else if (TreeView1.SelectedNode.Value == "Add Liability")
            {
                Session["menu"] = null;
                Session.Remove("personalVo");
                Session.Remove("propertyVo");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
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
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            
               
        }
    }
}