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
using BoUser;
using BoCommon;
namespace WealthERP.Advisor
{
    public partial class RMCustomerNonIndividualLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        List<string> roleList = new List<string>();
        int count;
        UserBo userBo = new UserBo();
        UserVo userVo=new UserVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            string First = null;
            string Middle = null;
            string Last = null;

            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session[SessionContents.UserVo];
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('RMCustomerNonIndividualLeftPane');", true);
                }
                //if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
                //{
                //    SetNode();
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

                FunctionInfo.Add("Method", "RMCustomerNonIndividualLeftPane.ascx.cs:Page_Load()");

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
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            //SetNode();
        }
        public void SetNode()
        {
            string strNodeValue = null;
            try
            {
                if (TreeView1.SelectedNode.Value == "Home")
                {

                    roleList = userBo.GetUserRoles(userVo.UserId);
                    count = roleList.Count;
                    if (count == 3)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);

                    }
                    if (count == 2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (roleList[i] == "RM")
                            {

                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMDashBoard','none');", true);
                            }
                            if (roleList[i] == "BM")
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorBMDashBoard','none');", true);
                            }
                        }
                    }


                }
                else if (TreeView1.SelectedNode.Value.ToString() == "RM Home")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Customer Dashboard")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerNonIndividualDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Profile Dashboard")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerNonIndividualDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "View Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewNonIndividualProfile','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Edit Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Proofs")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerProofs','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Bank Details")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Bank Details")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AddBankDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Add Proof")
                {
                    Session["FlagProof"] = 1;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerProofsAdd','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Group Accounts")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Add Group Member")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('FamilyDetails','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Associate Member")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerAssociatesAdd','none');", true);
                }
                else if (TreeView1.SelectedNode.Value == "Portfolio Details")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerPortfolio','none');", true);
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
                    strNodeValue = TreeView1.SelectedNode.Parent.Value;

                    foreach (TreeNode node in TreeView1.Nodes)
                    {
                        if (node.Value != strNodeValue)
                            node.Collapse();
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
    }
}