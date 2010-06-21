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
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    if (customerVo.FirstName != null && customerVo.MiddleName != null && customerVo.LastName != null)
                    {
                        string First = customerVo.FirstName.ToString();
                        string Middle = customerVo.MiddleName.ToString();
                        string Last = customerVo.LastName.ToString();

                        if (Middle != "")
                        {
                            lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        }
                        else
                        {
                            lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                        }

                        lblEmailIdValue.Text = customerVo.Email.ToString();
                    }
                    TreeView1.CollapseAll();
                    TreeView1.FindNode("Portfolio Dashboard").Expand();

                   
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
                FunctionInfo.Add("Method", "PortfolioLeftPane.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = customerVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNodeValue = "";
            try
                
            {
                if (TreeView1.SelectedNode.Value == "RM Home")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('RMDashBoard', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Liabilities Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilityView','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Liability")
                {
                    Session["menu"] = null;
                    Session.Remove("personalVo");
                    Session.Remove("propertyVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Equity")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolios', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView', 'none')", true);
                    
                }
                else if (TreeView1.SelectedNode.Value == "View MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('TransactionsView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('MFManualSingleTran', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=');", true);
                   // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Insurance")
                {
                    Session.Remove("table");
                    Session.Remove("insuranceVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=IN')", true);
                }
                //else if (TreeView1.SelectedNode.Value == "General Insurance")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails', 'none')", true);
                //}
                //else if (TreeView1.SelectedNode.Value == "Add General Insurance")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceEntry', '?action=IN')", true);
                //}
                else if (TreeView1.SelectedNode.Value == "Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioFixedIncomeView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=FI')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewGovtSavings', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=GS')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioProperty', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PR')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Pension And Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PensionPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Pension and Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PG')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Personal Assets")
                {
                  
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Personal Assets")
                {
                    Session.Remove("personalVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioPersonalEntry', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioGoldEntry', '?action=GoldEntry')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioCollectiblesEntry', '?action=Col')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Cash And Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Cash and Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=CS')", true);
                }
                else if (TreeView1.SelectedNode.Value == "Register Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry', '?action=entry')", true);
                }
                else if (TreeView1.SelectedNode.Value == "View Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicView', 'none')", true);
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
                FunctionInfo.Add("Method", "PortfolioLeftPane.ascx:TreeView1_SelectedNodeChanged()");
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