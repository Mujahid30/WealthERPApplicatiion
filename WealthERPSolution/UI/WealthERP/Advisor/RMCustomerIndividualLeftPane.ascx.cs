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
using Telerik.Web.UI;
using BoAdvisorProfiling;
using System.Data;
using System.Configuration;

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
        RMVo rmVo = new RMVo();
        string strNodeValue = null;
        bool ShowGroupOrNot = false;
        int groupCustomerId = 0;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string First = null;
            string Middle = null;
            string RMName = string.Empty;
            string RMEmail = string.Empty;
            string RMMobile = string.Empty;
            string Last = null;
            string treeType = null;
            DataSet dsTreeNodes;
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            bool isGrpHead = false;


            Session["IsCustomerDrillDown"] = "Yes";

            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                if ((customerVo != null) && (customerVo.CustomerId != 0))
                {
                    Session[SessionContents.FPS_ProspectList_CustomerId] = customerVo.CustomerId;
                }
                groupCustomerId = customerBo.GetCustomerGroupHead(customerVo.CustomerId);
                Session["rmVo"] = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                rmVo = (RMVo)Session["rmVo"];
                if (!string.IsNullOrEmpty(customerVo.AccountId))
                {
                    btnOnlineOrder.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=MF&x-Account-ID=" + customerVo.AccountId.ToString();
                    btnOnlineOrder.ToolTip = "Click here for MF online Order";

                    btnNCDOnline.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=NCD&x-Account-ID=" + customerVo.AccountId.ToString();
                    btnNCDOnline.ToolTip = "Click here for NCD online Order";

                    btnIPOOnline.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=IPO&x-Account-ID=" + customerVo.AccountId.ToString();
                    btnIPOOnline.ToolTip = "Click here for IPO online Order";
                }
                else
                {
                    btnOnlineOrder.ToolTip = "ClientId not available, Please update clientId from customer profile";
                    btnOnlineOrder.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=MF";
                    btnOnlineOrder.Enabled = false;

                    btnNCDOnline.ToolTip = "ClientId not available, Please update clientId from customer profile";
                    btnNCDOnline.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=NCD";
                    btnNCDOnline.Enabled = false;

                    btnIPOOnline.ToolTip = "ClientId not available, Please update clientId from customer profile";
                    btnIPOOnline.PostBackUrl = "~/OnlineMainHost.aspx?WERP=CustomerDrillDown&x-SBI-PType=IPO";
                    btnIPOOnline.Enabled = false;
                }
                if (!IsPostBack)
                {
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();

                    RMName = rmVo.FirstName + " " + rmVo.MiddleName + " " + rmVo.LastName;
                    RMEmail = rmVo.Email;
                    RMMobile = rmVo.Mobile.ToString();

                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        hdnUserRole.Value = "RM";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    {
                        hdnUserRole.Value = "BM";

                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString() == "Ops")
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
                    if (Request.QueryString["customerStatusVar"] != null)
                    {
                        ShowGroupOrNot = true;
                    }

                    if (Session["custStatusToShowGroupDashBoard"] != null)
                    {
                        if (Session["custStatusToShowGroupDashBoard"].ToString() == "customerStatus")
                        {
                            ShowGroupOrNot = true;
                        }
                    }

                    if (Session["S_CurrentUserRole"].ToString() == "Customer")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "topMenu", "loadtopmenu('AdvisorLeftPane');", true);
                    }

                    if (userVo.UserType == "Customer")
                    {
                        trCustDetails.Visible = true;
                        trCustRMDetailsDivider.Visible = true;
                        trMobileDetails.Visible = true;
                        lblClientInfo.Text = "Adviser/RM";

                        if (Middle != "")
                        {
                            lblCustomerDetails.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        }
                        else
                        {
                            lblCustomerDetails.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                        }
                        if (RMName != "")
                        {
                            lblNameValue.Text = RMName;
                        }
                        else
                        {
                            lblNameValue.Text = string.Empty;
                        }

                        if (RMEmail != "")
                        {
                            lblEmailIdValue.Text = RMEmail;
                        }
                        else
                        {
                            lblEmailIdValue.Text = string.Empty;
                        }
                        if (RMMobile != "0")
                        {
                            lblMobileValue.Text = RMMobile;
                        }
                        else
                        {
                            lblMobileValue.Text = string.Empty;
                        }
                    }
                    else
                    {
                        trCustDetails.Visible = false;
                        trCustRMDetailsDivider.Visible = false;
                        trMobileDetails.Visible = false;
                        lblClientInfo.Text = "Customer Contact Info";

                        if (Middle != "")
                        {
                            lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                        }
                        else
                        {
                            lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Email))
                        {
                            lblEmailIdValue.Text = customerVo.Email.ToString();
                        }
                        else
                        {
                            lblEmailIdValue.Text = "";
                        }
                    }

                    //if (Middle != "")
                    //{
                    //    lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                    //}
                    //else
                    //{
                    //    lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    //}

                    //lblEmailIdValue.Text = customerVo.Email.ToString();


                    isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                    if ((isGrpHead == true) || (ShowGroupOrNot == true))
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
                    else if (IsDashboard == "CusDashBoardQuicklinks")
                    {
                        RadPanelBar1.FindItemByValue("CusQuickLinks").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "portfolio")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Selected = true;
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "UnitHoldingss")
                    {
                        RadPanelBar1.FindItemByValue("MF_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("Holdings").Expanded = true;
                        RadPanelBar1.FindItemByValue("UnitHoldings").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "TransactionBooks")
                    {
                        RadPanelBar1.FindItemByValue("MF_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("Books").Expanded = true;
                        RadPanelBar1.FindItemByValue("TransactionBook").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "SIPBooks")
                    {
                        RadPanelBar1.FindItemByValue("MF_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("Books").Expanded = true;
                        RadPanelBar1.FindItemByValue("SIPBook").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "OrderBooks")
                    {
                        RadPanelBar1.FindItemByValue("MF_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("Books").Expanded = true;
                        RadPanelBar1.FindItemByValue("OrderBook").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "NCDIssueLists")
                    {
                        RadPanelBar1.FindItemByValue("NCD_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("NCDTransact").Expanded = true;
                        RadPanelBar1.FindItemByValue("NCDIssueList").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "NCDHoldingss")
                    {
                        RadPanelBar1.FindItemByValue("NCD_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("NCDHoldings").Expanded = true;
                        RadPanelBar1.FindItemByValue("NCDHolding").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "IPOIssueLists")
                    {
                        RadPanelBar1.FindItemByValue("IPO_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("IPOIssueTransact").Expanded = true;
                        RadPanelBar1.FindItemByValue("IPOIssueList").Selected = true;
                        //Session["IsDashboard"] = "false";
                    }
                    else if (IsDashboard == "IPOHoldingss")
                    {
                        RadPanelBar1.FindItemByValue("IPO_Online_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("IPOIssueTransact").Expanded = true;
                        RadPanelBar1.FindItemByValue("IPOHolding").Selected = true;
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
                    else if (IsDashboard == "Online_Order")
                    {
                        RadPanelBar1.FindItemByValue("Profile Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Online Order").Selected = true;
                        RadPanelBar1.FindItemByValue("Online Order").Expanded = true;
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
                    //if (Session["customerVo"] != null)
                    //    Session.Remove("customerVo");

                    //if (Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()] == null)
                    //{
                    //    dsTreeNodes = GetAdviserRoleTreeNodes(advisorVo.advisorId);
                    //    Cache.Insert("AdminLeftTreeNode" + advisorVo.advisorId.ToString(), dsTreeNodes, null, DateTime.Now.AddMinutes(4 * 60), TimeSpan.Zero);
                    //}
                    //else
                    //{
                    //    dsTreeNodes = (DataSet)Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()];

                    //}
                    dsTreeNodes = GetTreeNodesBasedOnUserRoles(treeType, "Customer");
                    DataSet dt = FilterUserTreeNodeData(dsTreeNodes);
                    SetUserTreeNode(dt);
                    if (dt.Tables[0].Rows.Count > 0)
                        SetAdminTreeNodesForRoles(dt, "Customer");
           
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

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('RMCustomerIndividualLeftPane');", true);
                    if (Session["NodeType"] == "M_F")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("M_F").Expanded = true;
                        RadPanelBar1.FindItemByValue("M_F").Selected = true;

                    }
                    else if (Session["NodeType"] == "Equity")
                    {
                        RadPanelBar1.FindItemByValue("Portfolio Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Equity").Expanded = true;
                        RadPanelBar1.FindItemByValue("Equity").Selected = true;

                    }
                    else if (Session["NodeType"] == "Order")
                    {
                        RadPanelBar1.FindItemByValue("Customer_Order").Expanded = true;
                        RadPanelBar1.FindItemByValue("Order_Approval_List").Expanded = true;
                        RadPanelBar1.FindItemByValue("Order_Approval_List").Selected = true;

                    }
                    else if (Session["NodeType"] == "FPDashBoard")
                    {
                        RadPanelBar1.FindItemByValue("Financial Planning").Expanded = true;
                        RadPanelBar1.FindItemByValue("Financial Planning").Selected = true;

                    }
                    else if (Session["NodeType"] == "CustomerDashBoard")
                    {
                        RadPanelBar1.FindItemByValue("Customer Dashboard").Expanded = true;
                        RadPanelBar1.FindItemByValue("Customer Dashboard").Selected = true;

                    }
                    else if (Session["NodeType"] == "Notification")
                    {
                        RadPanelBar1.FindItemByValue("Alerts").Expanded = true;
                        RadPanelBar1.FindItemByValue("View Notifications").Expanded = true;
                        RadPanelBar1.FindItemByValue("View Notifications").Selected = true;

                    }
                    if (Session["NodeType"] != null)
                    {

                        Session.Remove("NodeType");
                        Session.Remove("NodeType");
                    }

                    if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                    {
                        //if (ConfigurationSettings.AppSettings["NCD_TREE_NODE"].ToString().Contains(advisorVo.advisorId.ToString()))
                        //{
                        //    RPBOnlineOrder.FindItemByValue("IPOOrder").Visible = false;
                        //    RPBOnlineOrder.FindItemByValue("MF_Online_Landing_Page").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Insurance").Visible = false;
                        //    if (Session[SessionContents.CurrentUserRole].ToString() == "Ops")
                        //    {
                        //        RPBOnlineOrder.FindItemByValue("Transact").Visible = false; ;
                        //    }
                        //    RadPanelBar1.FindItemByValue("Alerts").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Report").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Liabilities").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Customer Dashboard").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Group Dashboard").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Financial Planning").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Equity").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Fixed Income").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Govt Savings").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Property").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Pension and Gratuities").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Personal Assets").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Gold Assets").Visible = false;
                        //    RadPanelBar1.FindItemByValue("Collectibles").Visible = false;
                        //}
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

        private DataSet GetAdviserRoleTreeNodes(int adviserId)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            DataSet dsTreeNodes;
            dsTreeNodes = advisorBo.GetAdviserRoleTreeNodes(adviserId);
            return dsTreeNodes;
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
                    Session["IsCustomerDrillDown"] = null;
                    if (Session["FPDataSet"] != null)
                        Session["FPDataSet"] = null;
                    if (Session["UserType"] != null)
                        Session["UserType"] = null;
                    Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
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
                    else if ((Session[SessionContents.CurrentUserRole].ToString() == "Ops"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer','login');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        if (!userVo.RoleList.Contains("Ops"))
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
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserCustomer", "loadcontrol('AdviserCustomer','login');", true);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorLeftPane", "loadlinks('AdvisorLeftPane','none');", true);
                }
                else if (e.Item.Value == "CusQuickLinks")
                {
                    Session["IsDashboard"] = "CusDashBoardQuicklinks";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerDashBoardShortcut", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                }
                else if (e.Item.Value == "CusHome")
                {
                    Session["CustomerHome"] = "CustomerHome";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerDashBoardShortcut", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                }
                else if (e.Item.Value == "Group Dashboard")
                {
                    customerVo = customerBo.GetCustomer(groupCustomerId);
                    Session["CustomerVo"] = customerVo;

                    Session["IsDashboard"] = "true";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdvisorRMCustGroupDashboard", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','none');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
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
                    if (customerVo.Type == null || customerVo.Type.ToUpper().ToString() == "IND")
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','login');", true);
                        }
                        else if (customerVo.IsProspect == 0)
                        {
                            Session.Remove("LinkAction");
                            Session["action"] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EditCustomerIndividualProfile", "loadcontrol('EditCustomerIndividualProfile','none');", true);
                        }
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','none');", true);
                        }
                        else if (customerVo.IsProspect == 0)
                        {
                            //if (Session["ButtonConvertToCustomer"] != null)
                            //{
                            //    Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','none');", true);
                            //}
                            //else
                            //{
                            Session.Remove("LinkAction");
                            Session["action"] = "View";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EditCustomerIndividualProfile", "loadcontrol('EditCustomerIndividualProfile','none');", true);
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
                    if (customerVo.Type == null || customerVo.Type.ToUpper().ToString() == "IND" )
                    {
                        if (customerVo.IsProspect == 1)
                        {
                            Session[SessionContents.FPS_AddProspectListActionStatus] = "Edit";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddProspectList", "loadcontrol('AddProspectList','login');", true);
                        }
                        else if (customerVo.IsProspect == 0)
                        {
                            Session.Remove("LinkAction");
                            Session["action"] = "Edit";
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
                else if (e.Item.Value == "Add_Bank_Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankAccount", "loadcontrol('AddBankAccount','action=Add');", true);
                }
                else if (e.Item.Value == "Add Bank Details")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankDetails", "loadcontrol('AddBankDetails','name=Add');", true);
                }
                //else if (e.Item.Value == "Add Bank Details")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankDetails", "loadcontrol('AddBankDetails','name=Editbalance');", true);
                //}
                else if (e.Item.Value == "View Bank Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankDetails", "loadcontrol('AddBankDetails','name=viewTransaction');", true);
                }
                else if (e.Item.Value == "View_Demate_Account")
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
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','none');", true);
                }
                else if (e.Item.Value == "VaultProof")
                {
                    Session.Remove("LinkAction");
                    Session.Remove("SessionSpecificProof");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','none');", true);
                }
                else if (e.Item.Value == "Order_Approval_List")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerProspect", "loadcontrol('MFOrderManagement','login')", true);
                }

                else if (e.Item.Value == "Financial Planning")
                {
                    if (advisorVo.advisorId == 1157 && advisorVo.OrganizationName.Contains("Birla Money"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalDashBoard','login');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFPDashboard", "loadcontrol('CustomerFPDashBoard','login')", true);
                    }
                }
                else if (e.Item.Value == "Finance Profile")
                {
                    if (SessionContents.FPS_CustomerPospect_ActionStatus != null)
                        Session.Remove(SessionContents.FPS_CustomerPospect_ActionStatus);
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
                else if (e.Item.Value == "CustomerFPRecommendation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerAdvisorsNote", "loadcontrol('CustomerFPRecommendation','none');", true);
                }
                else if (e.Item.Value == "Risk profile and asset allocation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('FinancialPlanning','login')", true);
                }
                //else if (e.Item.Value == "Projections")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPProjections','login')", true);
                //}
                //else if (e.Item.Value == "Goal Planning")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalPlanningDetails','login')", true);
                //}
                else if (e.Item.Value == "Projections")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerAdvancedGoalSetup','login')", true);
                }
                //else if (e.Item.Value == "Goal Funding")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalFunding','login')", true);
                //}
                //else if (e.Item.Value == "Goal_Planning")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('AddCustomerFinancialPlanningGoalSetup','login')", true);
                //}
                else if (e.Item.Value == "Goal_Setup")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerAdvancedGoalSetup','login')", true);
                }
                else if (e.Item.Value == "Goal_List")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FinancialPlanning", "loadcontrol('CustomerFPGoalPlanningDetails','login')", true);
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
                    if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                    {
                        if (ConfigurationSettings.AppSettings["NCD_TREE_NODE"].ToString().Contains(advisorVo.advisorId.ToString())) ;
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','none');", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioDashboard", "loadcontrol('PortfolioDashboard','none');", true);
                    }
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
                else if (e.Item.Value == "M_F")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewMutualFundPortfolio", "loadcontrol('ViewMutualFundPortfolio', 'none')", true);
                }
                else if (e.Item.Value == "View MF Transactions")
                {
                    //RMMultipleTransactionView
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionsView", "loadcontrol('TransactionsView', 'none')", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionsView", "loadcontrol('RMMultipleTransactionView', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Transactions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFManualSingleTran", "loadcontrol('MFManualSingleTran', 'none')", true);
                }
                else if (e.Item.Value == "Add MF Folio")
                {
                    if (Session["AddMFFolioLinkIdLinkAction"] != null)
                        Session["AddMFFolioLinkIdLinkAction"] = null;
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerGovtSaving", "loadcontrol('ViewGovtSavings', 'none')", true);
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
                else if (e.Item.Value == "Equity Report")
                {
                    Session["UserType"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "EquityReports", "loadcontrol('EquityReports','login')", true);
                }
                else if (e.Item.Value == "Multi Asset report")
                {
                    Session["UserType"] = "Customer";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioReports", "loadcontrol('PortfolioReports','login')", true);
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
                else if (e.Item.Value.ToLower() == "Inbox_Customer")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
                }
                else if (e.Item.Value == "MF_Online_Landing_Page")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOnlineUserLandingPage", "loadcontrol('MFOnlineUserLandingPage','none');", true);
                }
             
                else if (e.Item.Value == "MF_Order")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('MFOrderManagement','login');", true);
                }
                else if (e.Item.Value == "NewPurchase")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderPurchaseTransType','none');", true);
                }
                else if (e.Item.Value == "AdditionalPurchase")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderAdditionalPurchase','none');", true);
                }
                else if (e.Item.Value == "Redeem")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderRdemptionTransType','none');", true);
                }
                else if (e.Item.Value == "SIP")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderSIPTransType','none');", true);
                }
                else if (e.Item.Value == "NFO")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderBuyTransTypeOffline','none');", true);
                }
                else if (e.Item.Value == "OrderBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerMFOrderBookList','none');", true);
                }
                else if (e.Item.Value == "TransactionBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerTransactionBookList','none');", true);
                }
                else if (e.Item.Value == "SIPBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','none');", true);
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerSIPBookList','none');", true);
                }
                else if (e.Item.Value == "UnitHoldings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerMFUnitHoldingList','none');", true);
                }
                else if (e.Item.Value == "NCDIssueList")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueList", "loadcontrol('NCDIssueList','none');", true);
                }
                else if (e.Item.Value == "NCDIssueTransact")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueTransact", "loadcontrol('NCDIssueTransact','none');", true);
                }
                else if (e.Item.Value == "NCDOrderBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueBooks", "loadcontrol('NCDIssueBooks','none');", true);
                }
                else if (e.Item.Value == "NCDHolding")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('NCDIssueHoldings','none');", true);
                }
                else if (e.Item.Value == "SIPSumBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','none');", true);
                }
                else if (e.Item.Value == "MF_Online_Landing_Page")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOnlineUserLandingPage", "loadcontrol('MFOnlineUserLandingPage','none');", true);
                }
                else if (e.Item.Value == "IPOIssueList")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('IPOIssueList','none');", true);
                }
                else if (e.Item.Value == "IPOIssueBook")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('CustomerIPOOrderBook','none');", true);
                }
                else if (e.Item.Value == "IPOHolding")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIPOHolding", "loadcontrol('CustomerIPOHolding','none');", true);
                }
                else if (e.Item.Value == "Bond_Order")
                {
                }
                else if (e.Item.Value == "Online_Order_MIS")
                {
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
        private DataSet FilterUserTreeNodeData(DataSet dsTreeNodes)
        {
            DataSet dsUserTreeNode = new DataSet();
            DataTable dtUserTreeNode = dsTreeNodes.Tables[0].Clone();
            DataTable dtUserTreeSubNode = dsTreeNodes.Tables[1].Clone();
            DataTable dtUserTreeSubSubsNode = dsTreeNodes.Tables[2].Clone();
            Dictionary<Int16, string> userAdviserRole = (Dictionary<Int16, string>)userVo.AdviserRole;

            foreach (int role in userAdviserRole.Keys)
            {
                dsTreeNodes.Tables[0].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeNode.Merge(dsTreeNodes.Tables[0].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);

                dsTreeNodes.Tables[1].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeSubNode.Merge(dsTreeNodes.Tables[1].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);

                dsTreeNodes.Tables[2].DefaultView.RowFilter = "AR_RoleId=" + role.ToString();
                dtUserTreeSubSubsNode.Merge(dsTreeNodes.Tables[2].DefaultView.ToTable(), false, MissingSchemaAction.Ignore);
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
            //DataView tempView = new DataView(dsTreeNodes.Tables[1]);
            //tempView.Sort = "WTSN_TreeSubNode";


            //dsFilteredData = FilterUserTreeNode(userVo.RoleList[0], dsTreeNodes);

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
        protected DataSet FilterUserTreeNode(string userRole, DataSet dsTreeNode)
        {
            DataSet dsTreeFilterNode = new DataSet();
            DataTable dtTreeNode = new DataTable();
            DataTable dtTreeSubNode = new DataTable();
            DataTable dtTreeSubSubNode = new DataTable();
            dtTreeNode = dsTreeNode.Tables[0].Clone();
            dtTreeSubNode = dsTreeNode.Tables[1].Clone();
            dtTreeSubSubNode = dsTreeNode.Tables[2].Clone();
           userRole = "Customer";
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
    
        private void SetAdminTreeNodesForRoles(DataSet dsAdminTreeNodes, string userRole)
        {
          
            int flag = 0;
            DataView tempView;
            DataRow dr;
            string dr1;
            // Filter on user level
            dsAdminTreeNodes = FilterUserTreeNode(userVo.RoleList[0], dsAdminTreeNodes);
            userRole = "Customer";
            if (userRole == "Customer")
            {
                tempView = new DataView(dsAdminTreeNodes.Tables[0]);
                tempView.Sort = "WTN_TreeNode";
                //Setting Primary key for the datatable inorder to find a value based on the key
                dsAdminTreeNodes.Tables[0].PrimaryKey = new DataColumn[] { dsAdminTreeNodes.Tables[0].Columns["WTN_TreeNodeId"] };

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
                            //dr = dsAdminTreeNodes.Tables[0].Rows.Find(Item.Value);
                            //Item.Text = dr[2].ToString();

                            ////dr1 = dsAdminTreeNodes.Tables[0].Rows.Find("Alerts").ToString();

                            //if (dsAdminTreeNodes.Tables[0].Rows.Contains(Item.Value))
                            //{
                               
                            //}
                            ////Item.Text = dr[2].ToString();
                            ////if (dr[2].ToString().ToLower() == "message")
                            ////{
                            ////    //Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            ////}
                            //if (Item.Value == "CusQuickLinks" || Item.Text == "CusHome")
                            //{
                            //    //Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            //}
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
                            if (dr[2].ToString() == "Bank Details")
                            {
                                //Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }

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
                            if (dr[2].ToString().ToLower() == "vault proof" || dr[2].ToString() == "Add Bank Account" || dr[2].ToString() == "Add Bank Transaction/Balance" || dr[2].ToString() == "View Bank Transaction")
                            {
                                //Item.Text += " <img id='img1' src='/Images/new.gif'/>";
                            }
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


        //protected void RPBOnlineOrder_ItemClick(object sender, RadPanelBarEventArgs e)
        //{
        //    RadPanelItem ItemClicked = e.Item;


        //    try
        //    {
                //if (e.Item.Value == "NewPurchase")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderPurchaseTransType','none');", true);
                //}
                //else if (e.Item.Value == "AdditionalPurchase")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderAdditionalPurchase','none');", true);
                //}
                //else if (e.Item.Value == "Redeem")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderRdemptionTransType','none');", true);
                //}
                //else if (e.Item.Value == "SIP")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderSIPTransType','none');", true);
                //}
                //else if (e.Item.Value == "NFO")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('MFOrderBuyTransTypeOffline','none');", true);
                //}
                //else if (e.Item.Value == "OrderBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerMFOrderBookList','none');", true);
                //}
                //else if (e.Item.Value == "TransactionBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerTransactionBookList','none');", true);
                //}
                //else if (e.Item.Value == "SIPBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','none');", true);
                //    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerSIPBookList','none');", true);
                //}
                //else if (e.Item.Value == "UnitHoldings")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerFIAlerts", "loadcontrol('CustomerMFUnitHoldingList','none');", true);
                //}
                //else if (e.Item.Value == "NCDIssueList")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueList", "loadcontrol('NCDIssueList','none');", true);
                //}
                //else if (e.Item.Value == "NCDIssueTransact")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueTransact", "loadcontrol('NCDIssueTransact','none');", true);
                //}
                //else if (e.Item.Value == "NCDOrderBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueBooks", "loadcontrol('NCDIssueBooks','none');", true);
                //}
                //else if (e.Item.Value == "NCDHolding")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('NCDIssueHoldings','none');", true);
                //}
                //else if (e.Item.Value == "SIPSumBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','none');", true);
                //}
                //else if (e.Item.Value == "MF_Online_Landing_Page")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOnlineUserLandingPage", "loadcontrol('MFOnlineUserLandingPage','none');", true);
                //}
                //else if (e.Item.Value == "IPOIssueList")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('IPOIssueList','none');", true);
                //}
                //else if (e.Item.Value == "IPOIssueBook")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueHoldings", "loadcontrol('CustomerIPOOrderBook','none');", true);
                //}
                //else if (e.Item.Value == "IPOHolding")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIPOHolding", "loadcontrol('CustomerIPOHolding','none');", true);
                //}
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {

        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:RadPanelBar1_ItemClick()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
    }
}
