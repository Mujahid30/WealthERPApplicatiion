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

        string agentCode=string.Empty;
        int agentId = 0;
        int associationId;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            associatesVo = (AssociatesVO)Session["associatesVo"];
            if(!IsPostBack)
            {
                if (Request.QueryString["AgentId"] != null)
                {
                    agentCode = Request.QueryString["AgentId"];
                    txtAgentCode.Text = agentCode;
                }
                else
                {
                    agentId = associatesBo.GetAgentId();
                    agentId = agentId + 1;
                    agentCode = agentId.ToString();
                    txtAgentCode.Text = agentCode;
                }
                if (Request.QueryString["AssociationId"] != null)
                {
                    BindAgentList();
                    associationId = int.Parse(Request.QueryString["AssociationId"]);
                    ddlUserType.SelectedValue = "Associates";
                    ddlSelectType.SelectedValue = associationId.ToString();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (Request.QueryString["AgentId"] != null)
            {
                agentId = int.Parse(Request.QueryString["AgentId"]);
                associatesVo.AAC_AdviserAgentId = agentId;
            }
            else
                agentId = associatesVo.AAC_AdviserAgentId;
            if (ddlUserType.SelectedIndex != 0)
            {
                if (ddlUserType.SelectedValue == "BM")
                {
                    if (ddlSelectType.SelectedIndex!=0)
                         associatesVo.BranchId = int.Parse(ddlSelectType.SelectedValue);
                }
                else if (ddlUserType.SelectedValue == "RM")
                {
                    if (ddlSelectType.SelectedIndex != 0)
                        associatesVo.RMId = int.Parse(ddlSelectType.SelectedValue);
                }
                else if (ddlUserType.SelectedValue == "Associates")
                {
                    if (ddlSelectType.SelectedIndex != 0)
                    {
                        associatesVo.AdviserAssociateId = int.Parse(ddlSelectType.SelectedValue);
                        associatesVo.ContactPersonName = ddlSelectType.SelectedItem.Text;
                    }
                }

            }
            //if (rbtBM.Checked == true)
            //{
            //    associatesVo.AAC_UserType = "BM";
            //    if (ddlSelectType.SelectedIndex!=0)
            //        associatesVo.BranchId = int.Parse(ddlSelectType.SelectedValue);
            //}
            //else if (rbtRM.Checked == true)
            //{
            //    associatesVo.AAC_UserType = "RM";
            //    if (ddlSelectType.SelectedIndex != 0)
            //        associatesVo.RMId = int.Parse(ddlSelectType.SelectedValue);
            //}
            //else if (rbtnAgent.Checked == true)
            //{
            //    associatesVo.AAC_UserType = "Agent";
            //    if (ddlSelectType.SelectedIndex != 0)
            //        associatesVo.AdviserAssociateId = int.Parse(ddlSelectType.SelectedValue);
            //}
            associatesVo.AAC_AgentCode = txtAgentCode.Text;
            associatesVo.AAC_CreatedBy = userVo.UserId;
            associatesVo.AAC_ModifiedBy = userVo.UserId;
            result = associatesBo.CreateAdviserAgentCode(associatesVo,agentId);
            if(result==true)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewAgentCode", "loadcontrol('ViewAgentCode','login');", true);

        }

        private void BindAgentList()
        {
            DataSet ds;
            DataTable dt;
            dt = associatesBo.GetAssociatesList(advisorVo.advisorId);
            if (dt.Rows.Count>0)
            {
                ddlSelectType.DataSource = dt;
                ddlSelectType.DataValueField = dt.Columns["AA_AdviserAssociateId"].ToString();
                ddlSelectType.DataTextField = dt.Columns["AA_ContactPersonName"].ToString();
                ddlSelectType.DataBind();
            }
            ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Agent", "Select Agent"));

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
                ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Branch", "Select Branch"));
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
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlSelectType.DataSource = dt;
                    ddlSelectType.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlSelectType.DataTextField = dt.Columns["RMName"].ToString();
                    ddlSelectType.DataBind();
                }
                ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select RM", "Select RM"));
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
            if (ddlUserType.SelectedIndex != 0)
            {
                if(ddlUserType.SelectedValue=="BM")
                    BindBranchDropDown();
                else if (ddlUserType.SelectedValue == "RM")
                    BindRMDropDown();
                else if (ddlUserType.SelectedValue == "Associates")
                    BindAgentList();
            }
        }
    }
}