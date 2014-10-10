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
using BoUploads;
using VoUser;
using BoCommon;
using VOAssociates;
using BOAssociates;
using BoAdvisorProfiling;
using Telerik.Web.UI;

namespace WealthERP.Associates
{
    public partial class AddBranchRMAgentAssociation : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        String userType;

        string agentCode = string.Empty;
        int agentId = 0;
        int associationId;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                userType = "advisor";
            associatesVo = (AssociatesVO)Session["associatesVo"];
            if (!IsPostBack)
            {
                lblPanDuplicate.Visible = false;
                BindAgentList();
                if (Request.QueryString["rmId"] != null)
                {
                    ddlUserType.SelectedValue = "RM";
                    BindRMDropDown();
                    ddlSelectType.SelectedValue = Request.QueryString["rmId"];
                    ddlUserType.Enabled = false;
                    ddlSelectType.Enabled = false;

                }
                if (Request.QueryString["AssociationId"] != null)
                {

                    associationId = int.Parse(Request.QueryString["AssociationId"]);
                    ddlUserType.Enabled = false;
                    ddlSelectType.Enabled = false;
                    ddlUserType.SelectedValue = "Associates";
                    ddlSelectType.SelectedValue = associationId.ToString();

                }

                if (Request.QueryString["StaffRole"] != null)
                {
                    ddlUserType.Text = Request.QueryString["StaffRole"].ToString();
                    ddlUserType_SelectedIndexChanged(this, null);
                }
                if (Request.QueryString["StaffName"] != null)
                {
                    if (ddlSelectType.Items.FindByText(Request.QueryString["StaffName"].ToString()) != null)
                        ddlSelectType.Items.FindByText(Request.QueryString["StaffName"].ToString()).Selected = true;
                    else
                        ddlSelectType.Text = "Select";

                }
                if (Request.QueryString["BranchRole"] != null)
                {
                    ddlUserType.Text = Request.QueryString["BranchRole"].ToString();
                    ddlUserType_SelectedIndexChanged(this, null);
                }
                if (Request.QueryString["BranchName"] != null)
                {
                    if (ddlSelectType.Items.FindByText(Request.QueryString["BranchName"].ToString()) != null)
                        ddlSelectType.Items.FindByText(Request.QueryString["BranchName"].ToString()).Selected = true;
                    else
                        ddlSelectType.Text = "Select";

                }
                if (Request.QueryString["AgentCode"] != null)
                {
                    txtAgentCode.Text = Request.QueryString["AgentCode"].ToString();
                    if (Request.QueryString["Flag"] != null)
                    {
                        checkUser();
                        getChildCodes();

                    }

                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AssociatesVO associatesVo = new AssociatesVO();
            lblPanDuplicate.Visible = false;
            bool result = false;
            if (Request.QueryString["AgentId"] != null)
            {
                agentId = int.Parse(Request.QueryString["AgentId"]);
                associatesVo.AAC_AdviserAgentId = agentId;
            }
            if (ddlUserType.SelectedIndex != 0)
            {
                if (ddlUserType.SelectedValue == "BM")
                {
                    if (ddlSelectType.SelectedIndex != 0)
                    {
                        associatesVo.BranchId = int.Parse(ddlSelectType.SelectedValue);
                        associatesVo.AAC_UserType = ddlUserType.SelectedValue;
                    }
                }
                else if (ddlUserType.SelectedValue == "RM")
                {
                    if (ddlSelectType.SelectedIndex != 0)
                    {
                        associatesVo.RMId = int.Parse(ddlSelectType.SelectedValue);
                        associatesVo.AAC_UserType = ddlUserType.SelectedValue;
                    }
                }
                else if (ddlUserType.SelectedValue == "Associates")
                {
                    if (ddlSelectType.SelectedIndex != 0)
                    {
                        associatesVo.AdviserAssociateId = int.Parse(ddlSelectType.SelectedValue);
                        associatesVo.ContactPersonName = ddlSelectType.SelectedItem.Text;
                        associatesVo.AAC_UserType = ddlUserType.SelectedValue;

                    }
                }

            }
            if (!string.IsNullOrEmpty(txtAgentCode.Text))
                associatesVo.AAC_AgentCode = txtAgentCode.Text;
            else
                associatesVo.AAC_AgentCode = null;
            associatesVo.AAC_CreatedBy = userVo.UserId;
            associatesVo.AAC_ModifiedBy = userVo.UserId;


            if (Session["agentCodelist"] != null)
            {

                List<string> agentcodelist = (List<string>)Session["agentCodelist"];
                for (int i = 0; i < agentcodelist.Count; i++)
                {
                    associatesBo.AddAgentChildCode(associatesVo, agentcodelist[i]);

                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
            }
            else
            {
                if (Validation(txtAgentCode.Text))
                {
                    result = associatesBo.CreateAdviserAgentCode(associatesVo, agentId, advisorVo.advisorId);
                    if (Request.QueryString["AssociationId"] != null)
                    {
                        int associationId = int.Parse(Request.QueryString["AssociationId"]);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('SubBroker code Added Successfully');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociates", "loadcontrol('AddAssociatesDetails','?AssociationId=" + associationId + "&fromPage=" + "AddCode" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('SubBroker code Added Successfully');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);
                    }
                }
            }

            if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "AddRM")
            {
                string queryString = "?prevPage=AddRM";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewRM','" + queryString + "');", true);
            }
            else if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "AddBranch")
            {
                //string queryString = "?prevPage=AddBranch";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBranch','" + queryString + "');", true);

            }
            else if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "EditRMDetails")
            {
                string queryString = "?prevPage=EditRMDetails&AgentCode=" + txtAgentCode.Text + "&Action=Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditRMDetails','" + queryString + "');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBranch','" + queryString + "');", true);

            }
            else if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "EditBranchDetails")
            {
                string queryString = "?prevPage=EditBranchDetails&AgentCode=" + txtAgentCode.Text + "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EditBranchDetails','" + queryString + "');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBranch','" + queryString + "');", true);

            }

        }

        private bool Validation(string agentCode)
        {
            bool result = true;
            int adviserId = advisorVo.advisorId;
            try
            {
                if (associatesBo.CodeduplicateCheck(adviserId, agentCode))
                {
                    result = false;
                    //lblPanDuplicate.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('SubBroker Code already exists !!');", true);
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
                FunctionInfo.Add("Method", "AddBranchRMAgentAssociation.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        private void BindAgentList()
        {
            DataSet ds;
            DataTable dt;
            dt = associatesBo.GetAssociatesList(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlSelectType.DataSource = dt;
                ddlSelectType.DataValueField = dt.Columns["AA_AdviserAssociateId"].ToString();
                ddlSelectType.DataTextField = dt.Columns["AA_ContactPersonName"].ToString();
                ddlSelectType.DataBind();
            }
            ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlSelectType.DataSource = ds;
                    ddlSelectType.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlSelectType.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlSelectType.DataBind();
                }
                ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMDropDown()
        {
            try
            {
                //AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                //DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                DataTable dt = associatesBo.GetSalesListToAddCode(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlSelectType.DataSource = dt;
                    ddlSelectType.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlSelectType.DataTextField = dt.Columns["RMName"].ToString();
                    ddlSelectType.DataBind();
                }
                ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkUser();
        }
        protected void checkUser()
        {
            if (ddlUserType.SelectedIndex != 0)
            {
                if (ddlUserType.SelectedValue == "BM")
                {
                    BindBranchDropDown();
                    gvChildCode.Visible = false;
                    txtAgentCode.Enabled = true;
                    txtAgentCode.Text = "";
                }
                else if (ddlUserType.SelectedValue == "RM")
                {
                    BindRMDropDown();
                    gvChildCode.Visible = false;
                    txtAgentCode.Enabled = true;
                    txtAgentCode.Text = "";
                }
                else if (ddlUserType.SelectedValue == "Associates")
                {
                    BindAgentList();
                    txtAgentCode.Enabled = true;
                    txtAgentCode.Text = "";
                }
            }
        }

        protected void ddlSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            getChildCodes();
        }
        protected void getChildCodes()
        {
            int PagentId = 0;
            if (ddlSelectType.SelectedIndex != 0)
            {
                lblPanDuplicate.Visible = false;
                GetAgentCode(int.Parse(ddlSelectType.SelectedValue), ddlUserType.SelectedValue);
                if (!string.IsNullOrEmpty(Session["Code"].ToString()) && !string.IsNullOrEmpty(Session["PagentId"].ToString()))
                {
                    PagentId = (int)Session["PagentId"];
                    txtAgentCode.Text = Session["Code"].ToString();
                    txtAgentCode.Enabled = false;
                }
                if (ddlUserType.SelectedValue == "Associates")
                {
                    if (!string.IsNullOrEmpty(Session["Code"].ToString()) && !string.IsNullOrEmpty(Session["PagentId"].ToString()))
                    {
                        BindChildCodeGrid((int)Session["PagentId"]);
                        gvChildCode.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        gvChildCode.Visible = false;
                        txtAgentCode.Text = "";
                        txtAgentCode.Enabled = true;
                    }
                }
                else if (ddlUserType.SelectedValue == "RM" || ddlUserType.SelectedValue == "BM")
                {
                    gvChildCode.Visible = false;
                    if (!string.IsNullOrEmpty(Session["Code"].ToString()) && !string.IsNullOrEmpty(Session["PagentId"].ToString()))
                    {
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        txtAgentCode.Text = "";
                        txtAgentCode.Enabled = true;
                    }

                }
            }
            //else if()

        }

        private void GetAgentCode(int id, string type)
        {
            DataTable dt;
            string code = string.Empty;
            int PagentId = 0;
            dt = associatesBo.GetAgentCodeFromAgentPaaingAssociateId(id, type);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["AAC_AgentCode"].ToString();
                Session["Code"] = code;
                PagentId = int.Parse(dt.Rows[0]["AAC_AdviserAgentId"].ToString());
                Session["PagentId"] = PagentId;

            }
            else
            {
                txtAgentCode.Text = "";
                txtAgentCode.Enabled = true;
                Session["Code"] = "";
                Session["PagentId"] = "";
            }
        }

        private void BindChildCodeGrid(int PagentId)
        {
            DataTable dtChildCodeList;
            dtChildCodeList = associatesBo.GetAgentChildCodeList(PagentId);
            //dtChildCodeList = dsChildCodeList.Tables[0];
            ViewState["ChildCodeList"] = dtChildCodeList;
            if (dtChildCodeList != null)
            {
                gvChildCode.DataSource = dtChildCodeList;
                gvChildCode.DataBind();
            }

        }

        protected void btnAddCode_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtNoOfCodes.Text))
            //{
            //    int count = int.Parse(txtNoOfCodes.Text);
            //    Session["AgentCodeCount"] = count;
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showpopup();", true);
            //}
        }
        protected void gvChildCode_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["ChildCodeList"] != null)
            {
                dt = (DataTable)ViewState["ChildCodeList"];
                gvChildCode.DataSource = dt;
            }

        }
        protected void gvChildCode_ItemCommand(object source, GridCommandEventArgs e)
        {
            int PagentId = 0;
            int childAgentId = 0;
            string ChildCode = string.Empty;
            AssociatesVO associatesVo = new AssociatesVO();
            if (Session["PagentId"] != null)
                PagentId = (int)Session["PagentId"];

            associatesVo.AAC_UserType = "Associates";
            associatesVo.AAC_CreatedBy = userVo.UserId;
            associatesVo.AAC_ModifiedBy = userVo.UserId;

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                associatesVo.AAC_AdviserAgentId = int.Parse(gvChildCode.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AdviserAgentId"].ToString());
                AdvisorBo advisorBo = new AdvisorBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtChildCode = (TextBox)e.Item.FindControl("txtChildCode");
                ChildCode = txtChildCode.Text;
                isUpdated = associatesBo.EditAddChildAgentCodeList(associatesVo, ChildCode, PagentId, 'U');

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                GridDataItem dataItem = (GridDataItem)e.Item;
                //TableCell strChildCodeForDelete = dataItem["AAC_AdviserAgentId"];
                childAgentId = int.Parse(gvChildCode.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AdviserAgentId"].ToString());
                isDeleted = associatesBo.DeleteChildAgentCode(childAgentId);
                if (isDeleted)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Record has been deleted successfully !!');", true);
                }
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtChildCode = (TextBox)e.Item.FindControl("txtChildCode");
                ChildCode = txtChildCode.Text;
                if (Validation(ChildCode))
                {
                    isInserted = associatesBo.EditAddChildAgentCodeList(associatesVo, ChildCode, PagentId, 'I');
                }
            }
            BindChildCodeGrid(PagentId);
        }

        protected void gvChildCode_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {

                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                TextBox txtChildCode = (TextBox)item.FindControl("txtChildCode");
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strExtType = gvChildCode.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AgentCode"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
            }
        }
    }
}