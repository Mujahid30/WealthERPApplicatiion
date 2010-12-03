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
using BoCustomerProfiling;

namespace WealthERP.Customer
{
    public partial class CustomerLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        bool isGrpHead = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            //    string First = customerVo.FirstName.ToString();
            //    string Middle = customerVo.MiddleName.ToString();
            //    string Last = customerVo.LastName.ToString();

            //    if (Middle != "")
            //    {
            //        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
            //    }
            //    else
            //    {
            //        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
            //    }

            //    lblEmailIdValue.Text = customerVo.Email.ToString();
            //}
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            if (!IsPostBack)
            {
                isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                if (isGrpHead == true)
                {
                    //TreeView1.Nodes.AddAt(0, new TreeNode("Group Home"));
                    RadPanelBar1.Items.Insert(0, new Telerik.Web.UI.RadPanelItem("Group Home"));
                    Session["IsDashboard"] = "true";
                }
                else
                    Session["IsDashboard"] = "CustDashboard";

                string IsDashboard = string.Empty;

                if (Session["IsDashboard"] != null)
                    IsDashboard = Session["IsDashboard"].ToString();
                if (IsDashboard == "true")
                {
                    //TreeView1.CollapseAll();
                    RadPanelBar1.CollapseAllItems();
                    if (customerVo.RelationShip == "SELF")
                    {
                        //TreeView1.FindNode("Group Home").Selected = true;
                        RadPanelBar1.FindItemByValue("Group Home").Selected = true;
                    }
                    else
                    {
                        //TreeView1.FindNode("Customer Dashboard").Selected = true;
                        RadPanelBar1.FindItemByValue("Customer Dashboard").Selected = true;
                    }
                    Session["IsDashboard"] = "false";
                }
                else if (IsDashboard == "CustDashboard")
                {
                    //TreeView1.CollapseAll();
                    //TreeView1.FindNode("Customer Dashboard").Selected = true;
                    RadPanelBar1.CollapseAllItems();
                    RadPanelBar1.FindItemByValue("Customer Dashboard").Selected = true;
                }
                else
                {
                    //TreeView1.CollapseAll();
                    //TreeView1.FindNode("Profile Dashboard").Expand();
                    RadPanelBar1.CollapseAllItems();
                    RadPanelBar1.FindItemByValue("Profile Dashboard").Expanded = true;
                    //TreeView1.FindNode("Profile Dashboard").Selected = true;
                    RadPanelBar1.FindItemByValue("Profile Dashboard").Selected = true;
                }
                
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('CustomerIndividualLeftPane');", true);
        }
        
       
        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            string strNodeValue = null;
            try
            {
                if (e.Item.Value == "Group Home")
                {
                    if (customerVo.RelationShip != "SELF")
                    {
                        customerVo = customerBo.GetCustomer(int.Parse(customerVo.ParentCustomer));
                        Session["CustomerVo"] = customerVo;
                    }

                    Session["IsDashboard"] = "true";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('AdvisorRMCustGroupDashboard','none');", true);
                }
                else if (e.Item.Value == "Customer Dashboard")
                {
                    Session["IsDashboard"] = "CustDashboard";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('AdvisorRMCustIndiDashboard','none');", true);
                }
                else if (e.Item.Value == "Portfolio Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioDashboard','none');", true);
                }
                else if (e.Item.Value == "Equity")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewEquityPortfolios', 'none')", true);
                }
                else if (e.Item.Value == "View Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('EquityTransactionsView','none')", true);
                }
                else if (e.Item.Value == "Add Equity Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('EquityManualSingleTransaction','none')", true);
                }
                else if (e.Item.Value == "Add Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerEQAccountAdd', 'none')", true);
                }
                else if (e.Item.Value == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewMutualFundPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "View MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('TransactionsView', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('MFManualSingleTran', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerMFAccountAdd', 'none')", true);
                }
                else if (e.Item.Value == "View MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView', 'none')", true);
                }
                else if (e.Item.Value == "MFReports")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MFReports', 'none')", true);
                }
                else if (e.Item.Value == "Insurance")
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewInsuranceDetails', 'none')", true);
                }
                else if (e.Item.Value == "Add Insurance")
                {
                    Session.Remove("table");
                    Session.Remove("insuranceVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=IN')", true);
                }
                else if (e.Item.Value == "Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioFixedIncomeView', 'none')", true);
                }
                else if (e.Item.Value == "Add Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=FI')", true);
                }
                else if (e.Item.Value == "Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewGovtSavings', 'none')", true);
                }
                else if (e.Item.Value == "Add Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=GS')", true);
                }
                else if (e.Item.Value == "Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioProperty', 'none')", true);
                }
                else if (e.Item.Value == "Add Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=PR')", true);
                }
                else if (e.Item.Value == "Pension And Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PensionPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Pension and Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=PG')", true);
                }
                else if (e.Item.Value == "Personal Assets")
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioPersonal', 'none')", true);
                }
                else if (e.Item.Value == "Add Personal Assets")
                {
                    Session.Remove("personalVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioPersonalEntry', 'none')", true);
                }
                else if (e.Item.Value == "Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewGoldPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioGoldEntry', '?action=GoldEntry')", true);
                }
                else if (e.Item.Value == "Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewCollectiblesPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioCollectiblesEntry', '?action=Col')", true);
                }
                else if (e.Item.Value == "Cash And Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioCashSavingsView', 'none')", true);
                }
                else if (e.Item.Value == "Add Cash and Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAccountAdd', '?action=CS')", true);
                }
                else if (e.Item.Value == "Register Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioSystematicEntry', '?action=entry')", true);
                }
                else if (e.Item.Value == "View Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioSystematicView', 'none')", true);
                }
                else if (e.Item.Value == "Profile Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('RMCustomerIndividualDashboard','none');", true);
                }
                else if (e.Item.Value == "Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('RMAlertNotifications','none');", true);
                }
                else if (e.Item.Value == "View Notifications")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('RMAlertNotifications','none');", true);
                }
                else if (e.Item.Value == "MF Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerMFAlert','none');", true);
                }
                else if (e.Item.Value == "FI Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerFIAlerts','none');", true);
                }
                else if (e.Item.Value == "Insurance Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerInsuranceAlerts','none');", true);
                }
                else if (e.Item.Value == "Equity Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerEQAlerts','none');", true);
                }
                else if (e.Item.Value == "View Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewNonIndividualProfile','none');", true);
                    }
                }
                else if (e.Item.Value == "Edit Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('EditCustomerIndividualProfile','none');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('EditCustomerNonIndividualProfile','none');", true);
                    }
                }
                else if (e.Item.Value == "Proof")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewCustomerProofs','none');", true);
                }
                else if (e.Item.Value == "Add Proof")
                {
                    Session["FlagProof"] = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerProofsAdd','none');", true);
                }
                else if (e.Item.Value == "Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('ViewBankDetails','none');", true);
                }
                else if (e.Item.Value == "Add Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('AddBankDetails','none');", true);
                }
                else if (e.Item.Value == "Add Group Member")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('FamilyDetails','none');", true);
                }
                else if (e.Item.Value == "Associate Member")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerAssociatesAdd','none');", true);
                }
                else if (e.Item.Value == "Add Liability")
                {
                    Session["menu"] = null;
                    Session.Remove("personalVo");
                    Session.Remove("propertyVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('LiabilitiesMaintenanceForm','none');", true);
                }
                else if (e.Item.Value == "Liabilities Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('LiabilityView','none');", true);
                }
                else if (e.Item.Value == "Income Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerIncome','none');", true);
                }
                else if (e.Item.Value == "Expense Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('CustomerExpense','none');", true);
                }
                else if (e.Item.Value == "General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
                }
                else if (e.Item.Value == "Add General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceAccountAdd','none');", true);
                }
                else if (e.Item.Value == "Add Life Insurance")
                {
                    Session.Remove("table");
                    Session.Remove("insuranceVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=IN')", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
                }
                else if (e.Item.Value == "Life Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','none');", true);
                }
                else if (e.Item.Value == "Reports")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('FinancialPlanningReports','login')", true);

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
    }
}
