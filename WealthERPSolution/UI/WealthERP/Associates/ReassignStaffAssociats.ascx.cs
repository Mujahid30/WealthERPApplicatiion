using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VOAssociates;
using BOAssociates;
using WealthERP.Base;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using BoCustomerProfiling;
using BoCommon;
using VoUser;
using System.Data;
using BoAdvisorProfiling;
namespace WealthERP.Associates
{
    public partial class ReassignStaffAssociats : System.Web.UI.UserControl
    {
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["RMVo"];

            if (!IsPostBack)
            {
                BindChannelList();
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    //txtNewReporting_autoCompleteExtender.ContextKey = ddltitlechannelId.SelectedValue + "/" + advisorVo.advisorId.ToString();
                    //txtNewReporting_autoCompleteExtender.ServiceMethod = "GetRMStaffList";
                }
            }
        }
        protected void txtNewReporting_OnTextChanged(object sender, EventArgs e)
        {
            if (txtNewReporting.Text == "")
            {
                radStaffList.Items.Clear();
                ExistingStaffList.Items.Clear();
                MappedStaffList.Items.Clear();
            }
        }
        protected void ddltitlechannelId_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtNewReporting_autoCompleteExtender.ContextKey = ddltitlechannelId.SelectedValue + "/" + advisorVo.advisorId.ToString();
                txtNewReporting_autoCompleteExtender.ServiceMethod = "GetRMStaffList";
            }
            radStaffList.Items.Clear();
            ExistingStaffList.Items.Clear();
            MappedStaffList.Items.Clear();
        }
        protected void BindChannelList()
        {
            DataTable dtChannel = advisorStaffBo.GetIFAChannel(advisorVo.advisorId);
            ddlChannel.DataSource = dtChannel;
            ddlChannel.DataValueField = dtChannel.Columns["AH_ChannelId"].ToString();
            ddlChannel.DataTextField = dtChannel.Columns["AH_ChannelName"].ToString();
            ddlChannel.DataBind();
            ddlChannel.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void ddlChannel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChannel.SelectedValue != "Select")
            {
                if (ddlChannel.SelectedValue == "10")
                {
                    ddlStaff.SelectedValue = "Associates";
                    ddlStaff.Enabled = false;
                }
                else
                {
                    ddlStaff.Enabled = true;

                }
                radStaffList.Items.Clear();
                ExistingStaffList.Items.Clear();
                MappedStaffList.Items.Clear();
                txtNewReporting.Text = "";
                BindTitle();
                BindTitleList(int.Parse(ddlChannel.SelectedValue));
            }
        }
        protected void BindTitle()
        {
            DataTable dtTitle = advisorStaffBo.GetTitleList(int.Parse(ddlChannel.SelectedValue), advisorVo.advisorId);
            ddlTitle.DataSource = dtTitle;
            ddlTitle.DataValueField = dtTitle.Columns["AH_Id"].ToString();
            ddlTitle.DataTextField = dtTitle.Columns["AH_HierarchyName"].ToString();
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void ddlTitle_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTitle.SelectedValue != "Select")
            //{
            BindSourceManager();
            //}
        }
        protected void BindSourceManager()
        {
            DataTable dtBindSourceManager = advisorStaffBo.GetStaffList(int.Parse(ddlTitle.SelectedValue), advisorVo.advisorId, int.Parse(txtStaffId.Value));
            radStaffList.DataSource = dtBindSourceManager;
            radStaffList.DataValueField = dtBindSourceManager.Columns["AR_RMId"].ToString();
            radStaffList.DataTextField = dtBindSourceManager.Columns["AR_FirstName"].ToString();
            radStaffList.DataBind();

        }
        protected void BindTitleList(int channelId)
        {
            DataTable dtBindTitleList = advisorStaffBo.GetStaffTitleList(channelId, advisorVo.advisorId);
            ddltitlechannelId.DataSource = dtBindTitleList;
            ddltitlechannelId.DataValueField = dtBindTitleList.Columns["AH_Id"].ToString();
            ddltitlechannelId.DataTextField = dtBindTitleList.Columns["AH_HierarchyName"].ToString();
            ddltitlechannelId.DataBind();
            ddltitlechannelId.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void BindAssociateList(string rnIds)
        {
            DataTable dtBindSourceManager = advisorStaffBo.GetStaffAssociateList(rnIds, advisorVo.advisorId);
            ExistingStaffList.DataSource = dtBindSourceManager;
            ExistingStaffList.DataValueField = dtBindSourceManager.Columns["AR_RMId"].ToString();
            ExistingStaffList.DataTextField = dtBindSourceManager.Columns["AR_FirstName"].ToString();
            ExistingStaffList.DataBind();
        }
        protected void radStaffList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectStaffAssociate = string.Empty;
            foreach (RadListBoxItem li in radStaffList.Items)
            {
                if (li.Checked == true)
                {
                    selectStaffAssociate += li.Value + ',';
                }
            }
            BindAssociateList(selectStaffAssociate.TrimEnd(','));
        }
        protected void ExistingStaffList_Transferred(object source, Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        {
            ExistingStaffList.Items.Sort();
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int newManagerId;
            newManagerId = int.Parse(txtStaffId.Value);
            string mappedStaffAssociate = string.Empty;
            foreach (RadListBoxItem li in MappedStaffList.Items)
            {
                if (li.Checked == true)
                {
                    mappedStaffAssociate += li.Value + ',';
                }
            }
            advisorStaffBo.UpdateReportingManager(mappedStaffAssociate.TrimEnd(','), newManagerId, advisorVo.advisorId);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Reporting Manager Change Successful!!');", true);

        }
    }
}