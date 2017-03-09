using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using BoUploads;
using BoAdvisorProfiling;
using VOAssociates;

namespace WealthERP.Advisor
{
    public partial class ViewStaff : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        string userType;
        string currentUserRole;
        int adviserId = 0;
        string agentCode = "";

        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            rmVo = (RMVo)Session[SessionContents.RmVo];
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            adviserId = advisorVo.advisorId;
            currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];

            if (!IsPostBack)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    BindBranchDropDown(advisorVo.advisorId);
                    BindStaffGrid(advisorVo.advisorId, true, false, false, adviserId);
                }
                else if (currentUserRole == "bm")
                {
                    BindBranchDropDown(rmVo.RMId);
                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindStaffGrid(rmVo.RMId, false, true, false, adviserId);
                    }
                    else
                    {
                        BindStaffGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true, adviserId);
                    }
                }
                else if (currentUserRole == "associates")
                {
                    BindBranchDropDown(advisorVo.advisorId);
                    agentCode = associateuserheirarchyVo.AgentCode;
                    BindStaffGrid(rmVo.RMId, false, true, false, adviserId);

                }
            }
        }

        private void BindBranchDropDown(int id)
        {
            try
            {
                UploadCommonBo uploadCommonBo = new UploadCommonBo();
                DataSet ds = uploadCommonBo.GetAdviserBranchList(id, currentUserRole);

                ddlBMBranch.DataSource = ds.Tables[0];
                ddlBMBranch.DataTextField = "AB_BranchName";
                ddlBMBranch.DataValueField = "AB_BranchId";
                ddlBMBranch.DataBind();
                if (ds.Tables[0].Rows.Count > 1)
                {
                    ddlBMBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", id.ToString()));
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

                FunctionInfo.Add("Method", "BMDashBoard.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindStaffGrid(int id, bool isAdviser, bool isBranchHead, bool isBranchId, int adviserId)
        {
            DataSet dsViewStaff;
            DataTable dtViewStaff = new DataTable();
            dsViewStaff = advisorStaffBo.BindStaffGridWithTeamChanelDetails(id, isAdviser, isBranchHead, isBranchId, currentUserRole, adviserId, agentCode);
            dtViewStaff = dsViewStaff.Tables[0];
            if (dtViewStaff != null)
            {
                rgvViewStaff.DataSource = dtViewStaff;
                rgvViewStaff.DataBind();
                pnlViewStaff.Visible = true;
                rgvViewStaff.Visible = true;
                imgViewStaff.Visible = true;
                if (Cache["rgvViewStaff" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("rgvViewStaff" + userVo.UserId + currentUserRole, dtViewStaff);
                }
                else
                {
                    Cache.Remove("rgvViewStaff" + userVo.UserId + currentUserRole);
                    Cache.Insert("rgvViewStaff" + userVo.UserId + currentUserRole, dtViewStaff);
                }
            }
        }

        protected void ddlBMBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBMBranch.SelectedIndex == 0)
            {
                if (currentUserRole == "admin" || currentUserRole == "ops")
                {
                    if (ddlBMBranch.Items.Count > 1)
                        BindStaffGrid(advisorVo.advisorId, true, false, false, adviserId);
                    else
                        BindStaffGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true, adviserId);
                }
                else if (currentUserRole == "bm")
                {

                    if (ddlBMBranch.Items.Count > 1)
                    {
                        BindStaffGrid(rmVo.RMId, false, true, false, adviserId);
                    }
                    else
                    {
                        BindStaffGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true, adviserId);
                    }
                }
                else if (currentUserRole == "associates")
                {
                  
                    agentCode = associateuserheirarchyVo.AgentCode;
                    if (ddlBMBranch.Items.Count > 1)
                    {
                    
                        BindStaffGrid(0, false, false, true, adviserId);
                    }
                }

            }
            else
            {
                if (currentUserRole == "associates")
                {
                    agentCode = associateuserheirarchyVo.AgentCode;
                }
                BindStaffGrid(Convert.ToInt32(ddlBMBranch.SelectedValue.ToString()), false, false, true, adviserId);

            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RadComboBox ddlAction = (RadComboBox)sender;
            DropDownList ddlAction = (DropDownList)sender; 
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;

            string action = "";
            int rmId = int.Parse(rgvViewStaff.MasterTableView.DataKeyValues[selectedRow - 1]["AR_RMId"].ToString());
            if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            {
                action = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddStaff", "loadcontrol('AddStaff','?RmId=" + rmId + "&action=" + action + "');", true);

            }
            if (ddlAction.SelectedItem.Value.ToString() == "View")
            {
                action = "View";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddStaff", "loadcontrol('AddStaff','?RmId=" + rmId + "&action=" + action + "');", true);

            }
        }
        protected void gvAssociates_rgvViewStaff(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtViewStaff = new DataTable();
            dtViewStaff = (DataTable)Cache["rgvViewStaff" + userVo.UserId + currentUserRole];
            rgvViewStaff.DataSource = dtViewStaff;
            rgvViewStaff.Visible = true;

            pnlViewStaff.Visible = true;
            imgViewStaff.Visible = true;
        }

        protected void rgvViewStaff_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (userVo.UserType == "Advisor") { return; }

            if (e.Item is GridDataItem) {
                GridDataItem item = (GridDataItem)e.Item;
                DropDownList actions = (DropDownList)item.FindControl("ddlMenu");
                //RadComboBoxItem rbcItem = actions.Items.FindItemByValue("Edit", true);
                //rbcItem.Visible = false;
            }
            if (currentUserRole == "associates")
                rgvViewStaff.Columns[0].Visible = false;
        }

        protected void imgViewStaff_Click(object sender, ImageClickEventArgs e)
        {
            rgvViewStaff.ExportSettings.OpenInNewWindow = true;
            rgvViewStaff.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in rgvViewStaff.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            rgvViewStaff.MasterTableView.ExportToExcel();
        }
    }
}