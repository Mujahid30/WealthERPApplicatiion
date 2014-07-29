using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoUser;
using BoCommon;

using BoAdvisorProfiling;

namespace WealthERP.AdvsierPreferenceSettings
{
    public partial class AdviserRoleToTreeNodeMapping : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        AdviserPreferenceBo advisorPreferenceBo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindRolename();
                if (Request.QueryString["roleId"] != null)
                {
                    int roleId = int.Parse(Request.QueryString["roleId"].ToString());
                    //int levelId = int.Parse(Request.QueryString["levelId"].ToString());
                    ddlRole.SelectedValue = roleId.ToString();
                    GetActualRoles(roleId);
                    //ViewRoleLink(roleId, levelId);
                    ContRolModes("New");
                }
                else
                {
                    ContRolModes("New");

                }
            }

        }

        private void ViewRoleLink(int roleId, int levelId)
        {
            ddlRole.SelectedValue = roleId.ToString();
            ddlLevel.SelectedValue = levelId.ToString();
            //  GetActualRoles(roleId);
            BtnGoClicked();
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            ContRolModes("LnkEdit");
            tblMessage.Visible = false;
        }
        protected void lnlBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdviserDepartmentRoleSetup", "loadcontrol('AdviserDepartmentRoleSetup');", true);

        }
        protected void ddlRole_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue != "Select")
            {
                GetActualRoles(Convert.ToInt32(ddlRole.SelectedValue));
                visblityOfControls(false);
            }
        }
        private void GetActualRoles(int roleId)
        {
            try
            {
                DataTable dtRoles = new DataTable();
                advisorPreferenceBo = new AdviserPreferenceBo();
                dtRoles = advisorPreferenceBo.GetActualRoles(roleId, advisorVo.advisorId).Tables[0];
                ddlLevel.Items.Clear();
                if (dtRoles.Rows.Count > 0)
                {
                    ddlLevel.DataSource = dtRoles;
                    ddlLevel.DataValueField = dtRoles.Columns["UR_RoleId"].ToString();
                    ddlLevel.DataTextField = dtRoles.Columns["UR_RoleName"].ToString();
                    ddlLevel.DataBind();
                }
                ddlLevel.Items.Insert(0, new ListItem("Select", "Select"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void ContRolModes(string mode)
        {
            if (mode == "New")
            {
                visblityOfControls(false);
                EnablityOfButtons(false, false, false);
            }
            else if (mode == "View")
            {
                EnablityOfControls(false);
                EnablityOfButtons(false, false, true);
            }
            else if (mode == "Edit")
            {

                EnablityOfControls(true);


            }
            else if (mode == "Update")
            {
                EnablityOfControls(true);
                EnablityOfButtons(false, false, true);
            }
            else if (mode == "Submitted")
            {
                EnablityOfControls(false);
                EnablityOfButtons(false, false, false);
            }
            else if (mode == "LnkEdit")
            {
                EnablityOfControls(true);
                EnablityOfButtons(false, true, false);
            }
            else if (mode == "GO")
            {
                EnablityOfControls(true);
                EnablityOfButtons(false, true, false);
            }
        }
        private void EnablityOfControls(bool value)
        {
            ddlRole.Enabled = value;
            ddlLevel.Enabled = value;
            BtnGo.Enabled = value;
            PnlAdmin.Enabled = value;
            PnlRM.Enabled = value;
            PnlBM.Enabled = value;
            PnlCustomer.Enabled = value;
            PnlOps.Enabled = value;
            PnlResearch.Enabled = value;
            PnlSuperAdmin.Enabled = value;
            PnlAssociates.Enabled = value;
            BtnGo.Enabled = value;
            //btnAdminRemoveNodes.Visible = false;
            //btnRMRemoveNodes.Visible = false;
            //btnBMRemoveNodes.Visible = false;
            //btnCustomerRemoveNodes.Visible = false;
            //btnAssociatesRemoveNodes.Visible = false;
            //btnSuperAdminRemoveNodes.Visible = false;
            //btnOpsRemoveNodes.Visible = false;
            //btnResearchRemoveNodes.Visible = false;
            switch (ddlLevel.SelectedValue)
            {
                case "1000": //btnAdminRemoveNodes.Visible = true;
                    break;
                case "1001": //btnRMRemoveNodes.Visible = true;
                    break;
                case "1002": //btnBMRemoveNodes.Visible = true;
                    break;
                case "1003": //btnCustomerRemoveNodes.Visible = true;
                    break;
                case "1004": //btnOpsRemoveNodes.Visible = true;
                    break;
                case "1005": //btnResearchRemoveNodes.Visible = true;
                    break;
                case "1006": //btnSuperAdminRemoveNodes.Visible = true;
                    break;
                case "1009": //btnAssociatesRemoveNodes.Visible = true;
                    break;
            }
        }
        private void visblityOfControls(bool value)
        {
            // ddlRole.Visble = value;
            PnlAdmin.Visible = value;
            PnlRM.Visible = value;
            PnlBM.Visible = value;
            PnlCustomer.Visible = value;
            PnlOps.Visible = value;
            PnlResearch.Visible = value;
            PnlSuperAdmin.Visible = value;
            PnlAssociates.Visible = value;
            btnMapingSubmit.Visible = value;
            btnUpdate.Visible = value;
            lnkBtnEdit.Visible = value;


        }
        private void EnablityOfButtons(bool value, bool value1, bool value2)
        {
            btnMapingSubmit.Visible = value;
            btnUpdate.Visible = value1;
            lnkBtnEdit.Visible = value2;
        }

        private void GetTreeView(int levelId, ref RadTreeNode RadNode)
        {

            if (levelId == 1000)
            {
                RTVAdmin.Nodes.Add(RadNode);
                PnlAdmin.Visible = true;


            }
            else if (levelId == 1001)
            {

                RTVRM.Nodes.Add(RadNode);
                PnlRM.Visible = true;


            }
            else if (levelId == 1002)
            {
                RTVBM.Nodes.Add(RadNode);
                PnlBM.Visible = true;

            }
            else if (levelId == 1003)
            {
                RTVCustomer.Nodes.Add(RadNode);
                PnlCustomer.Visible = true;


            }
            else if (levelId == 1004)
            {

                RTVOps.Nodes.Add(RadNode);
                PnlOps.Visible = true;

            }
            else if (levelId == 1005)
            {
                RTVResearch.Nodes.Add(RadNode);
                PnlResearch.Visible = true;

            }
            else if (levelId == 1006)
            {
                RTVSuperAdmin.Nodes.Add(RadNode);
                PnlSuperAdmin.Visible = true;


            }
            else if (levelId == 1009)
            {
                RTVAssociates.Nodes.Add(RadNode);
                PnlAssociates.Visible = true;


            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            BtnGoClicked();
            ContRolModes("GO");
        }



        protected void btnAdminRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVAdmin) == true)
            {
                RemoveTreeNodes(RTVAdmin);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }


        //protected void btnAdminRemNodes_Click(object sender, EventArgs e)
        //{
        //    if (CheckNodeGotSelected(RTVAdmin) == true)
        //    {
        //        RemoveTreeNodes(RTVAdmin);
        //    }
        //    else
        //    {
        //        CallingUserMsg();
        //    }
        //}

        protected void btnRMRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVRM) == true)
            {
                RemoveTreeNodes(RTVRM);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnBMRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVBM) == true)
            {
                RemoveTreeNodes(RTVBM);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnCustomerRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVCustomer) == true)
            {
                RemoveTreeNodes(RTVCustomer);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnOpsRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVOps) == true)
            {
                RemoveTreeNodes(RTVOps);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnResearchRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVResearch) == true)
            {
                RemoveTreeNodes(RTVResearch);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnSuperAdminRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVSuperAdmin) == true)
            {
                RemoveTreeNodes(RTVSuperAdmin);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        protected void btnAssociatesRemoveNodes_Click(object sender, EventArgs e)
        {
            if (CheckNodeGotSelected(RTVAssociates) == true)
            {
                RemoveTreeNodes(RTVAssociates);
                string message = "Removed SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());

            }
            else
            {
                CallingUserMsg();
            }

        }
        private void CallingUserMsg()
        {
            string message = CreateUserMessage();
            ShowMessage(message);
        }
        private bool CheckNodeGotSelected(RadTreeView RTV)
        {
            bool result = false;
            foreach (RadTreeNode RTVTreeNodes in RTV.CheckedNodes)
            {
                if (RTVTreeNodes.Checked == true)
                {
                    result = true;
                }
            }
            return result;
        }

        private string CreateUserMessage()
        {
            string userMessage = string.Empty;
            userMessage = "Pls select Nodes To Update";
            return userMessage;
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }




        private void RemoveTreeNodes(RadTreeView RTV)
        {

            advisorPreferenceBo = new AdviserPreferenceBo();

            DataTable dtTreeNodes = new DataTable();
            DataTable dtSubTreeNodes = new DataTable();
            DataTable dtSubSubTreeNodes = new DataTable();


            // Adding treenode Columns 
            dtTreeNodes.Columns.Add("AR_RoleId");
            dtTreeNodes.Columns.Add("WTN_TreeNodeId");

            //Adding treeSubNode Columns 
            dtSubTreeNodes.Columns.Add("AR_RoleId");
            dtSubTreeNodes.Columns.Add("WTSN_SubTreeNodeId");
            dtSubTreeNodes.Columns.Add("WTN_TreeNodeId");

            //Adding treeSubNode Columns 
            dtSubSubTreeNodes.Columns.Add("AR_RoleId");
            dtSubSubTreeNodes.Columns.Add("WTSSN_SubSubTreeNodeId");
            dtSubSubTreeNodes.Columns.Add("WTSN_SubTreeNodeId");
            dtSubSubTreeNodes.Columns.Add("WTN_TreeNodeId");



            // ---------------- Collecting Treenodes -----
            foreach (RadTreeNode RTVTreeNodes in RTV.CheckedNodes)
            {
                if (RTVTreeNodes.Checked == true && RTVTreeNodes.Level == 0)
                {
                    string a = RTVTreeNodes.CheckState.ToString();
                    dtTreeNodes.Rows.Add();
                    dtTreeNodes.Rows[dtTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtTreeNodes.Rows[dtTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = RTVTreeNodes.Value;
                }
            }
            if (dtTreeNodes.Rows.Count > 0)
            {
                // int i = advisorPreferenceBo.CreateOrUpdateTreeNodeMapping(dtTreeNodes, commanType, userVo.UserId);
            }
            //--------------Collecting SubTreeNodes-----

            foreach (RadTreeNode RTVSubTreeNodes in RTV.CheckedNodes)
            {
                if (RTVSubTreeNodes.Checked == true & RTVSubTreeNodes.Level == 1)
                {
                    dtSubTreeNodes.Rows.Add();
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = RTVSubTreeNodes.ParentNode.Value;
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["WTSN_SubTreeNodeId"] = RTVSubTreeNodes.Value;

                }
            }
            if (dtSubTreeNodes.Rows.Count > 0)
            {
                // int i = advisorPreferenceBo.CreateOrUpdateTreeSubNodeMapping(dtSubTreeNodes, commanType, userVo.UserId);
            }
            //------- Collecting SubSubTreeNodes--------------

            foreach (RadTreeNode RTVSubSubTreeNodes in RTV.CheckedNodes)
            {
                if (RTVSubSubTreeNodes.Checked == true & RTVSubSubTreeNodes.Level == 2)
                {
                    dtSubSubTreeNodes.Rows.Add();
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = (RTVSubSubTreeNodes.ParentNode).ParentNode.Value;
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTSN_SubTreeNodeId"] = RTVSubSubTreeNodes.ParentNode.Value;
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTSSN_SubSubTreeNodeId"] = RTVSubSubTreeNodes.Value;
                }
            }

            int i = advisorPreferenceBo.RemoveTreeNodeMapping(dtSubSubTreeNodes, dtSubTreeNodes, dtTreeNodes);




        }

        private void BtnGoClicked()
        {
            if (ddlRole.SelectedValue == "Select")
                return;
            if (ddlLevel.SelectedValue == "Select")
                return;
            clearRadtreeview();
            BindTreeNodesBasedOnRoles(Convert.ToInt32(ddlLevel.SelectedValue));
            GetCheckedTreeNodes(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(ddlLevel.SelectedValue));
            chkRTVAdmin.Visible = true;
        }
        private void clearRadtreeview()
        {
            PnlRM.Visible = false;
            PnlRM.Visible = false;
            PnlBM.Visible = false;
            PnlCustomer.Visible = false;
            PnlOps.Visible = false;
            PnlResearch.Visible = false;
            PnlSuperAdmin.Visible = false;
            PnlAssociates.Visible = false;
            PnlAdmin.Visible = false;
            //btnAdminRemoveNodes.Visible = false;
            //btnRMRemoveNodes.Visible = false;
            //btnBMRemoveNodes.Visible = false;
            //btnCustomerRemoveNodes.Visible = false;
            //btnAssociatesRemoveNodes.Visible = false;
            //btnSuperAdminRemoveNodes.Visible = false;
            //btnOpsRemoveNodes.Visible = false;
            //btnResearchRemoveNodes.Visible = false;
            switch (ddlLevel.SelectedValue)
            {
                case "1000": RTVAdmin.Nodes.Clear();
                    //  btnAdminRemoveNodes.Visible = true;
                    break;
                case "1001": RTVRM.Nodes.Clear();
                    // btnRMRemoveNodes.Visible = true;
                    break;
                case "1002": RTVBM.Nodes.Clear();
                    // btnBMRemoveNodes.Visible = true;
                    break;
                case "1003": RTVCustomer.Nodes.Clear();
                    //  btnCustomerRemoveNodes.Visible = true;
                    break;
                case "1004": RTVOps.Nodes.Clear();
                    // btnOpsRemoveNodes.Visible = true;
                    break;
                case "1005": RTVResearch.Nodes.Clear();
                    //  btnResearchRemoveNodes.Visible = true;
                    break;
                case "1006": RTVSuperAdmin.Nodes.Clear();
                    //  btnSuperAdminRemoveNodes.Visible = true;
                    break;
                case "1009": RTVAssociates.Nodes.Clear();
                    //  btnAssociatesRemoveNodes.Visible = true;
                    break;
            }
        }

        private void MapingBetweenExistandAvailable()
        {


        }

        private void GetCheckedTreeNodes(int roleId, int levelId)
        {

            DataTable dtTree = new DataTable();
            advisorPreferenceBo = new AdviserPreferenceBo();
            dtTree = advisorPreferenceBo.GetRoleLevelTreeNodes(roleId, levelId).Tables[0];
            RadTreeView rtv = GetLevelTree(levelId);

            if (dtTree == null)
            {
                //RadTreeView rtv = GetLevelTree(levelId);
                foreach (RadTreeNode RTVTreeNodes in rtv.Nodes)
                {
                    RTVTreeNodes.Checked = false;
                }
                return;
            }
            else if (dtTree.Rows.Count <= 0)
            {
                //RadTreeView rtv = GetLevelTree(levelId);
                foreach (RadTreeNode RTVTreeNodes in rtv.Nodes)
                {
                    RTVTreeNodes.Checked = false;
                }
                return;
            }

            //RadTreeView rtv = GetLevelTree(levelId);

            foreach (DataRow dr in dtTree.Rows)
            {

                foreach (RadTreeNode RTVTreeNodes in rtv.Nodes)
                {

                    //if (RTVTreeNodes.Value == dr["treeNodeId"].ToString())
                    //{
                    //    RTVTreeNodes.Checked = true;

                    //}
                    if (RTVTreeNodes.Nodes.Count != 0)
                    {
                        foreach (RadTreeNode RTVTreeChildNodes in RTVTreeNodes.Nodes)
                        {
                            if (RTVTreeChildNodes.Nodes.Count != 0)
                            {
                                foreach (RadTreeNode RTVTreeChildNodesChild in RTVTreeChildNodes.Nodes)
                                {
                                    if (RTVTreeChildNodesChild.Value == dr["SubSubTreeNodeId"].ToString())
                                    {
                                        RTVTreeChildNodesChild.Checked = true;
                                    }
                                }
                            }
                            else if (RTVTreeChildNodes.Value == dr["SubTreeNodeId"].ToString())
                            {
                                RTVTreeChildNodes.Checked = true;

                            }
                        }
                    }
                    else if (RTVTreeNodes.Value == dr["treeNodeId"].ToString())
                    {
                        RTVTreeNodes.Checked = true;
                    }
                    //else if (RTVTreeNodes.Value == dr["SubSubTreeNodeId"].ToString())
                    //{
                    //    RTVTreeNodes.Checked = true;
                    //}

                    //if ((RTVTreeNodes.Level == 0) && (RTVTreeNodes.Value == dr["treeNodeId"].ToString()))
                    //{

                    //    RTVTreeNodes.Checkable=true;
                    //}
                    // if (RTVTreeNodes.Level == 1)
                    //{
                    //    if (RTVTreeNodes.Value =="34")
                    //    {
                    //        RTVTreeNodes.Checked = true;

                    //    }
                    //    else
                    //    {
                    //        RTVTreeNodes.Checked = false;
                    //    }

                    //}
                    ////else if ((RTVTreeNodes.Level == 2) && RTVTreeNodes.Value == dr["SubSubTreeNodeId"].ToString())
                    ////{
                    ////    RTVTreeNodes.Checkable = true;

                    ////}


                }



                //foreach (RadTreeNode RTVTreeNodes in rtv.Nodes)
                //{
                //    if (RTVTreeNodes.Level == 2 && RTVTreeNodes.Value == dr["SubTreeNodeId"].ToString())
                //    {

                //        RTVTreeNodes.Checked = true;
                //    }
                //}
                //foreach (RadTreeNode RTVTreeNodes in rtv.Nodes)
                //{
                //    if (RTVTreeNodes.Level == 3 && RTVTreeNodes.Value == dr["SubSubTreeNodeId"].ToString())
                //    {

                //        RTVTreeNodes.Checked = true;
                //    }
                //}
            }
        }

        private RadTreeView GetLevelTree(int levelId)
        {
            RadTreeView RTV = new RadTreeView();
            if (levelId == 1000)
            {
                RTV = RTVAdmin;
            }
            else if (levelId == 1001)
            {
                RTV = RTVRM;
            }
            else if (levelId == 1002)
            {
                RTV = RTVBM;
            }
            else if (levelId == 1003)
            {
                RTV = RTVCustomer;

            }
            else if (levelId == 1004)
            {
                RTV = RTVOps;

            }
            else if (levelId == 1005)
            {
                RTV = RTVResearch;

            }
            else if (levelId == 1006)
            {
                RTV = RTVSuperAdmin;

            }
            else if (levelId == 1009)
            {
                RTV = RTVAssociates;
            }
            return RTV;
        }


        protected void btnMapingSubmit_Click(object sender, EventArgs e)
        {
            if (CheckAnyOneNodeSelected() == true)
            {
                switch (ddlLevel.SelectedValue)
                {
                    case "1000": CreateOrUpdateMaping(RTVAdmin, "Insert");
                        break;
                    case "1001": CreateOrUpdateMaping(RTVRM, "Insert");
                        break;
                    case "1002": CreateOrUpdateMaping(RTVBM, "Insert");
                        break;
                    case "1003": CreateOrUpdateMaping(RTVCustomer, "Insert");
                        break;
                    case "1004": CreateOrUpdateMaping(RTVOps, "Insert");
                        break;
                    case "1005": CreateOrUpdateMaping(RTVResearch, "Insert");
                        break;
                    case "1006": CreateOrUpdateMaping(RTVSuperAdmin, "Insert");
                        break;
                    case "1009": CreateOrUpdateMaping(RTVAssociates, "Insert");
                        break;
                }
                ContRolModes("Submitted");
                string message = "Submitted SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }
        }

        private bool CheckAnyOneNodeSelected()
        {
            bool result = false;
            if (CheckNodeGotSelected(RTVAdmin) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVRM) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVBM) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVCustomer) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVOps) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVResearch) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVSuperAdmin) == true)
            {
                result = true;
                return result;
            }
            else if (CheckNodeGotSelected(RTVAssociates) == true)
            {
                result = true;
                return result;
            }


            return result;
        }

        protected void btnMapingUpdate_Click(object sender, EventArgs e)
        {
            if (CheckAnyOneNodeSelected() == true)
            {
                switch (ddlLevel.SelectedValue)
                {
                    case "1000": CreateOrUpdateMaping(RTVAdmin, "Update");
                        break;
                    case "1001": CreateOrUpdateMaping(RTVRM, "Update");
                        break;
                    case "1002": CreateOrUpdateMaping(RTVBM, "Update");
                        break;
                    case "1003": CreateOrUpdateMaping(RTVCustomer, "Update");
                        break;
                    case "1004": CreateOrUpdateMaping(RTVOps, "Update");
                        break;
                    case "1005": CreateOrUpdateMaping(RTVResearch, "Update");
                        break;

                    case "1006": CreateOrUpdateMaping(RTVSuperAdmin, "Update");
                        break;
                    case "1009": CreateOrUpdateMaping(RTVAssociates, "Update");
                        break;
                }
                ContRolModes("Update");
                string message = "Update SuccessFully.";
                ShowMessage(message);
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
            else
            {
                CallingUserMsg();
            }

        }

        private void CreateOrUpdateMaping(RadTreeView RTV, string commanType)
        {
            advisorPreferenceBo = new AdviserPreferenceBo();

            DataTable dtTreeNodes = new DataTable();
            DataTable dtSubTreeNodes = new DataTable();
            DataTable dtSubSubTreeNodes = new DataTable();


            // Adding treenode Columns 
            dtTreeNodes.Columns.Add("AR_RoleId");
            dtTreeNodes.Columns.Add("WTN_TreeNodeId");

            //Adding treeSubNode Columns 
            dtSubTreeNodes.Columns.Add("AR_RoleId");
            dtSubTreeNodes.Columns.Add("WTSN_SubTreeNodeId");
            dtSubTreeNodes.Columns.Add("WTN_TreeNodeId");

            //Adding treeSubNode Columns 
            dtSubSubTreeNodes.Columns.Add("AR_RoleId");
            dtSubSubTreeNodes.Columns.Add("WTSSN_SubSubTreeNodeId");
            dtSubSubTreeNodes.Columns.Add("WTSN_SubTreeNodeId");
            dtSubSubTreeNodes.Columns.Add("WTN_TreeNodeId");



            // ---------------- Collecting Treenodes -----
            foreach (RadTreeNode RTVTreeNodes in RTV.CheckedNodes)
            {
                if (RTVTreeNodes.Checked == true && RTVTreeNodes.Level == 0)
                {
                    dtTreeNodes.Rows.Add();
                    dtTreeNodes.Rows[dtTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtTreeNodes.Rows[dtTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = RTVTreeNodes.Value;
                }
            }
            if (dtTreeNodes.Rows.Count > 0)
            {
                int i = advisorPreferenceBo.CreateOrUpdateTreeNodeMapping(dtTreeNodes, commanType, userVo.UserId, Convert.ToInt32(ddlLevel.SelectedValue));
            }
            //--------------Collecting SubTreeNodes-----

            foreach (RadTreeNode RTVSubTreeNodes in RTV.CheckedNodes)
            {
                if (RTVSubTreeNodes.Checked == true & RTVSubTreeNodes.Level == 1)
                {
                    dtSubTreeNodes.Rows.Add();
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = RTVSubTreeNodes.ParentNode.Value;
                    dtSubTreeNodes.Rows[dtSubTreeNodes.Rows.Count - 1]["WTSN_SubTreeNodeId"] = RTVSubTreeNodes.Value;

                }
            }
            if (dtSubTreeNodes.Rows.Count > 0)
            {
                int i = advisorPreferenceBo.CreateOrUpdateTreeSubNodeMapping(dtSubTreeNodes, commanType, userVo.UserId, Convert.ToInt32(ddlLevel.SelectedValue));
            }
            //------- Collecting SubSubTreeNodes--------------

            foreach (RadTreeNode RTVSubSubTreeNodes in RTV.CheckedNodes)
            {
                if (RTVSubSubTreeNodes.Checked == true & RTVSubSubTreeNodes.Level == 2)
                {
                    dtSubSubTreeNodes.Rows.Add();
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["AR_RoleId"] = Convert.ToInt32(ddlRole.SelectedValue);
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTN_TreeNodeId"] = (RTVSubSubTreeNodes.ParentNode).ParentNode.Value;
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTSN_SubTreeNodeId"] = RTVSubSubTreeNodes.ParentNode.Value;
                    dtSubSubTreeNodes.Rows[dtSubSubTreeNodes.Rows.Count - 1]["WTSSN_SubSubTreeNodeId"] = RTVSubSubTreeNodes.Value;
                }
            }
            if (dtSubSubTreeNodes.Rows.Count > 0)
            {
                int i = advisorPreferenceBo.CreateOrUpdateTreeSubSubNodeMapping(dtSubSubTreeNodes, commanType, userVo.UserId, Convert.ToInt32(ddlLevel.SelectedValue));
            }

        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {

        }
        private void BindTreeNodesBasedOnRoles(int levelId)
        {

            DataTable dtTree = new DataTable();
            advisorPreferenceBo = new AdviserPreferenceBo();
            dtTree = advisorPreferenceBo.GetTreeNodes(levelId, advisorVo.advisorId).Tables[0];
            foreach (DataRow dr in dtTree.Rows)
            {
                RadTreeNode node = new RadTreeNode(dr["WTN_TreeNodeText"].ToString(), dr["WTN_TreeNodeId"].ToString());
                BindSubTreeNodesBasedOnRoles(ref node, levelId);
                GetTreeView(levelId, ref node);
                // RTVAdmin.Nodes.Add(node);

            }
        }

        private void BindSubTreeNodesBasedOnRoles(ref RadTreeNode parentNode, int levelId)
        {
            DataTable childNode = new DataTable();
            advisorPreferenceBo = new AdviserPreferenceBo();

            childNode = advisorPreferenceBo.GetSubTreeNodes(Convert.ToInt32(parentNode.Value), levelId, advisorVo.advisorId).Tables[0];
            foreach (DataRow dr1 in childNode.Rows)
            {
                RadTreeNode child_node = new RadTreeNode(dr1["WTSN_TreeSubNodeText"].ToString(), dr1["WTSN_TreeSubNodeId"].ToString());
                BindSubSubTreeNodesBasedOnRoles(ref child_node, levelId);
                parentNode.Nodes.Add(child_node);
            }
        }
        private void BindSubSubTreeNodesBasedOnRoles(ref RadTreeNode subNode, int levelId)
        {
            DataTable subChildNode = new DataTable();
            advisorPreferenceBo = new AdviserPreferenceBo();
            subChildNode = advisorPreferenceBo.GetSubSubTreeNodes(Convert.ToInt32(subNode.Value), levelId, advisorVo.advisorId).Tables[0];
            foreach (DataRow dr1 in subChildNode.Rows)
            {
                RadTreeNode child_node = new RadTreeNode(dr1["WTSSN_TreeSubSubNodeText"].ToString(), dr1["WTSSN_TreeSubSubNodeId"].ToString());
                subNode.Nodes.Add(child_node);

            }
        }
        private void BindRolename()
        {
            DataTable dtRoles = new DataTable();
            advisorPreferenceBo = new AdviserPreferenceBo();
            dtRoles = advisorPreferenceBo.GetAdviserRoles(advisorVo.advisorId).Tables[0];


            if (dtRoles.Rows.Count > 0)
            {
                ddlRole.DataSource = dtRoles;
                ddlRole.DataValueField = dtRoles.Columns["AR_RoleId"].ToString();
                ddlRole.DataTextField = dtRoles.Columns["AR_Role"].ToString();
                ddlRole.DataBind();
            }
            ddlRole.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void chkRTVAdmin_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkRTVAdmin.Checked == true)
            {
               RTVAdmin.CheckAllNodes();
               RTVCustomer.CheckAllNodes();
               RTVOps.CheckAllNodes();
            }
            else
            {
                RTVAdmin.UncheckAllNodes();
                RTVCustomer.UncheckAllNodes();
                RTVOps.UncheckAllNodes();
            }
        }
    }
}
