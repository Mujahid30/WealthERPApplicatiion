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
using BoCustomerProfiling;
using Telerik.Web.UI;
using BoAdvisorProfiling;
using System.Data;


namespace WealthERP.Advisor
{
    public partial class RMIndividualCustomerLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        

        List<string> roleList = new List<string>();
        int count;
        UserBo userBo = new UserBo();
        UserVo userVo;
        string strNodeValue = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string First = null;
            string Middle = null;
            string Last = null;
            string treeType = null;
            DataSet dsTreeNodes;
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            bool isGrpHead = false;

            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                if ((customerVo != null) && (customerVo.CustomerId != 0))
                {
                    Session[SessionContents.FPS_ProspectList_CustomerId] = customerVo.CustomerId;
                }
                if (!IsPostBack)
                {
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        hdnUserRole.Value = "RM";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    {
                        hdnUserRole.Value = "BM";
                        
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        hdnUserRole.Value = "Adviser";
                        
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                    {
                        RadPanelBar1.FindItemByValue("Home").Visible = false;
                        txtFindCustomer.Visible = false;
                        btnSearchCustomer.Visible = false;
                        
                    }
                    if (userVo.RoleList != null)
                    {
                        if (userVo.RoleList.Contains("Admin"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                        }
                        else if (userVo.RoleList.Contains("BM"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                        }
                        else if (userVo.RoleList.Contains("RM"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                        }
                    }
                    if (Session["S_CurrentUserRole"].ToString() == "Customer")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                    }

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
                        //TreeView1.Nodes.AddAt(1, new TreeNode("Group Dashboard"));
                        RadPanelBar1.FindItemByValue("Group Dashboard").Visible = true;
                    }

                    string IsDashboard = string.Empty;

                    if (Session["IsDashboard"] != null)
                        IsDashboard = Session["IsDashboard"].ToString();
                    RadPanelBar1.CollapseAllItems();
                    if (IsDashboard == "true")
                    {
                        if (isGrpHead == true)
                        {
                            RadPanelBar1.FindItemByValue("Group Dashboard").Selected = true;
                        }
                        else
                        {
                            RadPanelBar1.FindItemByValue("Customer Dashboard").Selected = true;
                        }
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "CustDashboard")
                    {
                        RadPanelBar1.FindItemByValue("Customer Dashboard").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "portfolio")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Selected = true;
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "FP")
                    {
                        RadPanelBar1.FindItemByValue("Financial Planning").Selected = true;
                        RadPanelBar1.FindItemByValue("Financial Planning").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "alerts")
                    {
                        RadPanelBar1.FindItemByValue("Alerts").Selected = true;
                        RadPanelBar1.FindItemByValue("Alerts").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "MFAssets")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("MF").Selected = true;
                        RadPanelBar1.FindItemByValue("MF").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "FixedIncome")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Fixed Income").Selected = true;
                        RadPanelBar1.FindItemByValue("Fixed Income").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "GovtSavings")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Govt Savings").Selected = true;
                        RadPanelBar1.FindItemByValue("Govt Savings").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "Property")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Property").Selected = true;
                        RadPanelBar1.FindItemByValue("Property").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "PensionGratuities")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Pension and Gratuities").Selected = true;
                        RadPanelBar1.FindItemByValue("Pension and Gratuities").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "PersonalItems")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Personal Assets").Selected = true;
                        RadPanelBar1.FindItemByValue("Personal Assets").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "Gold")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Gold Assets").Selected = true;
                        RadPanelBar1.FindItemByValue("Gold Assets").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "Collectibles")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Collectibles").Selected = true;
                        RadPanelBar1.FindItemByValue("Collectibles").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "CashandSavings")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Cash and Savings").Selected = true;
                        RadPanelBar1.FindItemByValue("Cash and Savings").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "DirectEquity")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Equity").Selected = true;
                        RadPanelBar1.FindItemByValue("Equity").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "LiabilitiesView")
                    {
                        RadPanelBar1.FindItemByValue("Liabilities").Expanded = true;
                        RadPanelBar1.FindItemByValue("Liabilities").Selected = true;
                        //RadPanelBar1.FindItemByValue("Equity").Expanded = true;

                    }
                    else if (IsDashboard == "BankDetails")
                    {
                        RadPanelBar1.FindItemByValue("Profile Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Bank Details").Selected = true;
                        RadPanelBar1.FindItemByValue("Bank Details").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else
                    {
                        RadPanelBar1.CollapseAllItems();
                        RadPanelBar1.FindItemByValue("Profile Dashboard").Selected = true;
                        RadPanelBar1.FindItemByValue("Profile Dashboard").Expanded = true;
                    }

                    //Code to unhide the tree nodes based on User Roles
                    if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                        treeType = "Customer";
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        treeType = "RM";

                    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                        treeType = "BM";
                    else
                        treeType = "Admin";

                    dsTreeNodes = GetTreeNodesBasedOnUserRoles(treeType, "Customer");
                    if (dsTreeNodes.Tables[0].Rows.Count > 0)
                        SetAdminTreeNodesForRoles(dsTreeNodes, "Customer");

                    //Code to unhide the tree nodes based on Plans
                    dsTreeNodes = GetTreeNodesBasedOnPlans(advisorVo.advisorId, "Customer", treeType);
                    //if (dsTreeNodes.Tables[0].Rows.Count > 0)
                    SetAdminTreeNodesForPlans(dsTreeNodes);


                    //Code to add the Home node if drilled down from Adviser or RM or BM customer list 
                    if (Session[SessionContents.CurrentUserRole].ToString() != "Customer")
                    {
                        RadPanelItem Item = new RadPanelItem();
                        Item.Text = "Home";
                        Item.Value = "Home";
                        RadPanelBar1.Items.Insert(0, Item);
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

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
        //    {
        //        SetNode();
        //    }
        //}

        //public void SetNode()
        //{

        //    try
        //    {

        //        if (TreeView1.SelectedNode.Value == "RM Home")
        //        {
        //            if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
        //            {
        //                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('BMDashBoard','none');", true);
        //            }
        //            else if(Session[SessionContents.CurrentUserRole].ToString() == "RM")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
        //            }
        //            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','none');", true);
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Group Dashboard")
        //        {
        //            Session["IsDashboard"] = "true";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Customer Dashboard")
        //        {
        //            Session["IsDashboard"] = "CustDashboard";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Equity")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolios', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View Equity Transaction")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Equity Transaction")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Equity Account")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View Equity Account")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "MF")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View MF Folio")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView', 'none')", true);

        //        }
        //        else if (TreeView1.SelectedNode.Value == "View MF Transaction")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('TransactionsView', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add MF Transaction")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MFManualSingleTran', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add MF Folio")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=');", true);
        //            // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Fixed Income")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioFixedIncomeView', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Fixed Income")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=FI')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Govt Savings")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGovtSavings', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Govt Savings")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=GS')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Property")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioProperty', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Property")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PR')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Pension And Gratuities")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PensionPortfolio', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Pension and Gratuities")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=PG')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Personal Assets")
        //        {

        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Personal Assets")
        //        {
        //            Session.Remove("personalVo");
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioPersonalEntry', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Gold Assets")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Gold Assets")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGoldEntry', '?action=GoldEntry')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Collectibles")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Collectibles")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioCollectiblesEntry', '?action=Col')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Cash And Savings")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Cash and Savings")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=CS')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Register Systematic Schemes")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry', '?action=entry')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View Systematic Schemes")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicView', 'none')", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Alerts")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View Notifications")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "MF Alerts")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAlert','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "FI Alerts")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerFIAlerts','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Insurance Alerts")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerInsuranceAlerts','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Equity Alerts")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAlerts','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "View Profile")
        //        {
        //            if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewNonIndividualProfile','none');", true);
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Edit Profile")
        //        {
        //            if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
        //            {

        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EditCustomerIndividualProfile','none');", true);
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Proof")
        //        {

        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewCustomerProofs','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Proof")
        //        {
        //            Session["FlagProof"] = 1;
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerProofsAdd','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Bank Details")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Bank Details")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBankDetails','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Demat Account Details")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Demat Account")
        //        {
        //            Session["DematDetailsView"] = "Add";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
        //        }

        //        else if (TreeView1.SelectedNode.Value == "Add Liability")
        //        {
        //            Session["menu"] = null;
        //            Session.Remove("personalVo");
        //            Session.Remove("propertyVo");
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Liabilities Dashboard")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilityView','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Income Details")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerIncome','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Expense Details")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerExpense','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Advisor Notes")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAdvisorsNote','none');", true);
        //        }                    
        //        else if (TreeView1.SelectedNode.Value == "General Insurance")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add General Insurance")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceAccountAdd','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Add Life Insurance")
        //        {
        //            Session.Remove("table");
        //            Session.Remove("insuranceVo");
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', '?action=IN')", true);
        //            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
        //        }
        //        else if (TreeView1.SelectedNode.Value == "Life Insurance")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','none');", true);
        //        }
        //        if (TreeView1.SelectedNode.Value == "RiskProfileAssetAllocation")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('FinancialPlanning','login')", true);

        //        }
        //        else if (TreeView1.SelectedNode.Value == "GoalProfiling")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddCustomerFinancialPlanningGoalSetup','login')", true);

        //        }
        //        else if (TreeView1.SelectedNode.Value == "Reports")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('FinancialPlanningReports','login')", true);

        //        }
        //        else if (TreeView1.SelectedNode.Value == "FinanceProfile")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerProspect','login')", true);
        //        }

        //        // Code to Expand/Collapse the Tree View Nodes based on selections
        //        //ExpandCollapseTreeView();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {

        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

        //        object[] objects = new object[1];

        //        objects[0] = strNodeValue;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //private void ExpandCollapseTreeView()
        //{
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

        protected void RadPanelBar1_ItemClick(object sender, RadPanelBarEventArgs e)
        {
            RadPanelItem ItemClicked = e.Item;
            DataSet dspotentialHomePage;
            string potentialHomePage = "";

            try
            {
                if (e.Item.Value == "Home")
                {
                    if(Session["FPDataSet"] != null)
                     Session["FPDataSet"] = null;
                    if(Session["UserType"] != null)
                     Session["UserType"] = null;
                    if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    {
                        dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                        if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                            potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                        if (potentialHomePage == "BM Home")
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BMMdadasdsdard", "loadcontrol('BMDashBoard','login');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "BManDasdadsard", "loadcontrol('BMCustomer','login');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "RM");
                        if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                            potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                        if (potentialHomePage == "RM Home")
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAAdminMasdadasdsdard", "loadcontrol('RMDashBoard','login');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAAdminMainDasdadsard", "loadcontrol('RMCustomer','login');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                        if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                            potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                        if (potentialHomePage == "Admin Home")
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IFAAdminMasdadasdsdard", "loadcontrol('IFAAdminMainDashboard','login');", true);
                        else if (potentialHomePage == "Admin Small IFA Home")
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IFAAdminMainDasdadsard", "loadcontrol('IFAAdminMainDashboard','login');", true);
                        else
                        {
                            Session["Customer"] = "Customer";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IFAAdminMboardasdasdsd", "loadcontrol('AdviserCustomer','login');", true);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorLeftPane", "loadlinks('AdvisorLeftPane','none');", true);
                }
                else if (e.Item.Value == "Group Dashboard")
                {
                    Session["IsDashboard"] = "true";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
                }
                else if (e.Item.Value == "Customer Dashboard")
                {
                    Session["IsDashboard"] = "CustDashboard";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustIndiDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
                }
                else if (e.Item.Value == "Profile Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
                }
                else if (e.Item.Value == "View Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','login');", true);
                        }
                        else if(customerVo.IsProspect == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerIndividualProfile", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                        }
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','none');", true);
                        }
                        else if(customerVo.IsProspect == 0)
                        {
                            //if (Session["ButtonConvertToCustomer"] != null)
                            //{
                            //    Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','none');", true);
                            //}
                            //else
                            //{
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerIndividualProfile", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                            //}
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewNonIndividualProfile", "loadcontrol('ViewNonIndividualProfile','none');", true);
                    }
                }
                else if (e.Item.Value == "Edit Profile")
                {
                    if (customerVo.Type.ToUpper().ToString() == "IND" || customerVo.Type == null)
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "Edit";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','login');", true);
                        }
                        else if (customerVo.IsProspect == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EditCustomerIndividualProfile", "loadcontrol('EditCustomerIndividualProfile','none');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EditCustomerNonIndividualProfile", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
                    }
                }
                else if (e.Item.Value == "Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewBankDetails", "loadcontrol('ViewBankDetails','none');", true);
                }
                else if (e.Item.Value == "Add Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankDetails", "loadcontrol('AddBankDetails','none');", true);
                }
                else if (e.Item.Value == "Demat Account Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "DematAccountDetails", "loadcontrol('DematAccountDetails','none');", true);
                }
                else if (e.Item.Value == "Add Demat Account")
                {
                    Session["DematDetailsView"] = "Add";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddDematAccountDetails", "loadcontrol('AddDematAccountDetails','none');", true);
                }
                else if (e.Item.Value == "Proof")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','none');", true);
                }
                else if (e.Item.Value == "Add Proof")
                {
                    Session["FlagProof"] = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerProofsAdd", "loadcontrol('CustomerProofsAdd','none');", true);
                }
                else if (e.Item.Value == "Financial Planning")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFPDashboard", "loadcontrol('CustomerFPDashBoard','login')", true);
                }
                else if (e.Item.Value == "Finance Profile")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerProspect", "loadcontrol('CustomerProspect','login')", true);
                }
                else if (e.Item.Value == "Preferences")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAssumptionsPreferencesSetup", "loadcontrol('CustomerAssumptionsPreferencesSetup','login')", true);
                }
                else if (e.Item.Value == "Advisor Notes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAdvisorsNote", "loadcontrol('CustomerAdvisorsNote','none');", true);
                }
                else if (e.Item.Value == "Risk profile and asset allocation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('FinancialPlanning','login')", true);
                }
                else if (e.Item.Value == "Projections")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPProjections','login')", true);
                }
                //else if (e.Item.Value == "Goal Planning")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalPlanningDetails','login')", true);
                //}
                //else if (e.Item.Value == "Goal Setup")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalSetup','login')", true);
                //}
                //else if (e.Item.Value == "Goal Funding")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalFunding','login')", true);
                //}
                else if (e.Item.Value == "Goal Planning")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('AddCustomerFinancialPlanningGoalSetup','login')", true);
                }
                else if (e.Item.Value == "Standard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPAnalyticsStandard','login')", true);
                }
                //else if (e.Item.Value == "Dynamic")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPAnalyticsDynamic','login')", true);
                //}
                else if (e.Item.Value == "Goal Profiling")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddCustomerFinancialPlanningGoalSetup", "loadcontrol('AddCustomerFinancialPlanningGoalSetup','login')", true);
                }
                else if (e.Item.Value == "Portfolio Dashboard")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','none');", true);
                }
                else if (e.Item.Value == "Equity")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewEquityPortfolios", "loadcontrol('ViewEquityPortfolios', 'none')", true);
                }
                else if (e.Item.Value == "View Equity Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EquityTransactionsView", "loadcontrol('EquityTransactionsView','none')", true);
                }
                else if (e.Item.Value == "Add Equity Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EquityManualSingleTransaction", "loadcontrol('EquityManualSingleTransaction','none')", true);
                }
                else if (e.Item.Value == "Add Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerEQAccountAdd", "loadcontrol('CustomerEQAccountAdd', 'none')", true);
                }
                else if (e.Item.Value == "View Equity Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerEQAccountView", "loadcontrol('CustomerEQAccountView', 'none')", true);
                }
                else if (e.Item.Value == "MF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewMutualFundPortfolio", "loadcontrol('ViewMutualFundPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "View MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionsView", "loadcontrol('TransactionsView', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFManualSingleTran", "loadcontrol('MFManualSingleTran', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerMFAccountAdd", "loadcontrol('CustomerMFAccountAdd','?action=');", true);
                }
                else if (e.Item.Value == "View MF Folio")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerMFFolioView", "loadcontrol('CustomerMFFolioView','?action=');", true);
                }
                else if (e.Item.Value == "View Systematic Schemes")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioSystematicView", "loadcontrol('PortfolioSystematicView', 'none')", true);
                }
                else if (e.Item.Value == "Register Systematic Schemes")
                {
                    Session.Remove("systematicSetupVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioSystematicEntry", "loadcontrol('PortfolioSystematicEntry', 'none')", true);
                }
                else if (e.Item.Value == "Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioFixedIncomeView", "loadcontrol('PortfolioFixedIncomeView', 'none')", true);
                }
                else if (e.Item.Value == "Add Fixed Income")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=FI')", true);
                }
                else if (e.Item.Value == "Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewGovtSavings", "loadcontrol('ViewGovtSavings', 'none')", true);
                }
                else if (e.Item.Value == "Add Govt Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=GS')", true);
                }
                else if (e.Item.Value == "Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioProperty", "loadcontrol('PortfolioProperty', 'none')", true);
                }
                else if (e.Item.Value == "Add Property")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=PR')", true);
                }
                else if (e.Item.Value == "Pension and Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PensionPortfolio", "loadcontrol('PensionPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Pension and Gratuities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=PG')", true);
                }
                else if (e.Item.Value == "Personal Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioPersonal", "loadcontrol('PortfolioPersonal', 'none')", true);
                }
                else if (e.Item.Value == "Add Personal Assets")
                {
                    Session.Remove("personalVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioPersonalEntry", "loadcontrol('PortfolioPersonalEntry', 'none')", true);
                }
                else if (e.Item.Value == "Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewGoldPortfolio", "loadcontrol('ViewGoldPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Gold Assets")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioGoldEntry", "loadcontrol('PortfolioGoldEntry', '?action=GoldEntry')", true);
                }
                else if (e.Item.Value == "Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCollectiblesPortfolio", "loadcontrol('ViewCollectiblesPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "Add Collectibles")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioCollectiblesEntry", "loadcontrol('PortfolioCollectiblesEntry', '?action=Col')", true);
                }
                else if (e.Item.Value == "Cash and Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioCashSavingsView", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
                }
                else if (e.Item.Value == "Add Cash and Savings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=CS')", true);
                }

                else if (e.Item.Value == "Life Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewInsuranceDetails", "loadcontrol('ViewInsuranceDetails','none');", true);
                }
                else if (e.Item.Value == "Add Life Insurance")
                {
                    Session.Remove("table");
                    Session.Remove("insuranceVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAccountAdd", "loadcontrol('CustomerAccountAdd', '?action=IN')", true);
                }
                else if (e.Item.Value == "General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewGeneralInsuranceDetails", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
                }
                else if (e.Item.Value == "Add General Insurance")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioGeneralInsuranceAccountAdd", "loadcontrol('PortfolioGeneralInsuranceAccountAdd','none');", true);
                }
                else if (e.Item.Value == "Liabilities")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpLiabilityViewane", "loadcontrol('LiabilityView','none');", true);
                }
                else if (e.Item.Value == "Add Liability")
                {
                    Session["menu"] = null;
                    Session.Remove("personalVo");
                    Session.Remove("propertyVo");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LiabilitiesMaintenanceForm", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                else if (e.Item.Value == "MF Report")
                {
                    Session["UserType"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFReports", "loadcontrol('MFReports','login');", true);
                }
                else if (e.Item.Value == "FP Report")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanningReports", "loadcontrol('FPSectional','login')", true);
                }
                else if (e.Item.Value == "View Notifications")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMAlertNotifications", "loadcontrol('RMAlertNotifications','none');", true);
                }
                else if (e.Item.Value == "MF Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerMFAlert", "loadcontrol('CustomerMFAlert','none');", true);
                }
                else if (e.Item.Value == "FI Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerFIAlerts','none');", true);
                }
                else if (e.Item.Value == "Insurance Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerInsuranceAlerts", "loadcontrol('CustomerInsuranceAlerts','none');", true);
                }
                else if (e.Item.Value == "Equity Alerts")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerEQAlerts", "loadcontrol('CustomerEQAlerts','none');", true);
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
                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:RadPanelBar1_ItemClick()");
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

        private void SetAdminTreeNodesForRoles(DataSet dsAdminTreeNodes, string userRole)
        {
            int flag = 0;
            DataView tempView;
            DataRow dr;
            if (userRole == "Customer")
            {
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNode"] };

                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 1 && Item.Level != 2)
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
                        }
                    }
                }
                //}
                flag = 0;
                tempView = new DataView(dsAdminTreeNodes.Tables[1]);
                tempView.Sort = "WTSN_TreeSubNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[1].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[1].Columns["WTSN_TreeSubNode"] };
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {
                    if (Item.Level != 0 && Item.Level != 2)
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
                foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
                {

                    if (Item.Level != 0 && Item.Level != 1)
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

            //foreach (RadPanelItem myItem in RadPanelBar1.GetAllItems())
            //{

            //    if (myItem.Items.Count == 0)
            //    {
            //        myItem.CssClass = "hideArrow";
            //    }  
            //}


            //if(RadPanelBar1.Items.FindItemByValue(dr["WTN_TreeNode"].ToString()) != null)
            //    RadPanelBar1.Items.FindItemByValue(dr["WTN_TreeNode"].ToString()).Visible = true;
            //}
        }

        private DataSet GetTreeNodesBasedOnPlans(int adviserId, string userRole, string treeType)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetTreeNodesBasedOnPlans(adviserId, userRole, treeType);
            return dsTreeNodes;
        }

        private void SetAdminTreeNodesForPlans(DataSet dsAdminTreeNodes)
        {
            int flag = 0;
            DataView tempView;

            tempView = new DataView(dsAdminTreeNodes.Tables[0]);
            tempView.Sort = "WTN_TreeNode";

            foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
            {
                if (Item.Level != 1 && Item.Level != 2)
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
            tempView = new DataView(dsAdminTreeNodes.Tables[1]);
            tempView.Sort = "WTSN_TreeSubNode";

            foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
            {
                if (Item.Level != 0 && Item.Level != 2)
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

            foreach (RadPanelItem Item in RadPanelBar1.GetAllItems())
            {
                if (Item.Level != 0 && Item.Level != 1)
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
