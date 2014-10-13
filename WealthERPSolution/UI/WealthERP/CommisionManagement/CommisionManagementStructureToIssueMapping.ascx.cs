using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;

namespace WealthERP.CommisionManagement
{
    public partial class CommisionManagementStructureToIssueMapping : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        CommissionStructureMasterVo commissionStructureMasterVo;
        CommissionStructureRuleVo commissionStructureRuleVo = new CommissionStructureRuleVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];

            if (!IsPostBack)
            {
               
                if (Cache[userVo.UserId.ToString() + "MappedIssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "MappedIssueList");
                GetMapped_Unmapped_Issues("Mapped", "");
                GetUnamppedIssues(ddlIssueType.SelectedValue);

            }
        }

        public void GetMapped_Unmapped_Issues(string type, string issueType)
        {
            DataTable dtmappedIssues = new DataTable();
            dtmappedIssues = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, type, issueType).Tables[0];
            if (dtmappedIssues == null)
                return;
            if (dtmappedIssues.Rows.Count == 0)
                return;

            if (type == "Mapped")
            {
                gvMappedIssueList.DataSource = dtmappedIssues;
                gvMappedIssueList.DataBind();

                if (Cache[userVo.UserId.ToString() + "MappedIssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "MappedIssueList");
                              
                Cache.Insert(userVo.UserId.ToString() + "MappedIssueList", dtmappedIssues);
            }
            else if (type == "UnMapped")
            {
                ddlUnMappedIssues.Items.Clear();
                ddlUnMappedIssues.DataSource = dtmappedIssues;
                ddlUnMappedIssues.DataTextField = "AIM_IssueName";
                ddlUnMappedIssues.DataValueField = "AIM_IssueId";
                ddlUnMappedIssues.DataBind();
                ddlUnMappedIssues.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }


        private void GetUnamppedIssues(string issueType)
        {
            GetMapped_Unmapped_Issues("UnMapped", issueType);

        }

        protected void ddlIssueType_Selectedindexchanged(object sender, EventArgs e)
        {
            GetUnamppedIssues(ddlIssueType.SelectedValue);

        }
        protected void gvMappedIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssues = (DataTable)Cache[userVo.UserId.ToString() + "MappedIssueList"];
            if (dtIssues != null) gvMappedIssueList.DataSource = dtIssues;

        }
        protected void btnMAP_Click(object sender, EventArgs e)
        {
            int mappingId;
            if (Request.QueryString["ID"] != null)
            {
                commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(Request.QueryString["ID"].Trim().ToString());
            }
            commissionStructureRuleVo.IssueId = Convert.ToInt32(ddlUnMappedIssues.SelectedValue);
            commisionReceivableBo.CreateIssuesStructureMapings(commissionStructureRuleVo, out mappingId);
            GetUnamppedIssues(ddlIssueType.SelectedValue);
            GetMapped_Unmapped_Issues("Mapped", "");

        }
    }
}