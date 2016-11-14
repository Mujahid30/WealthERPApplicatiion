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
using System.Collections;
using BoAdvisorProfiling;
namespace WealthERP.Associates
{
    public partial class ReassignStaffAssociats : System.Web.UI.UserControl
    {
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();
        string message;

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
        protected void ddlStaff_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStaff.SelectedValue != "Staff")
            {
                
            }
            else
            {
             
                
            }
        }
        protected void ddltitlechannelId_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtNewReporting_autoCompleteExtender.ContextKey = ddltitlechannelId.SelectedValue + "/" + advisorVo.advisorId.ToString();
                txtNewReporting_autoCompleteExtender.ServiceMethod = "GetRMStaffList";
            }
            BindTitle();
            radStaffList.Items.Clear();
            ExistingStaffList.Items.Clear();
            MappedStaffList.Items.Clear();
            txtStaffId.Value = "";
            txtNewReporting.Text = "";
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
                //if (ddlChannel.SelectedValue == "10")
                //{
                //    ddlStaff.SelectedValue = "Associates";
                //    ddlStaff.Enabled = false;
                //}
                //else
                //{
                //    ddlStaff.Enabled = true;

                //}
                radStaffList.Items.Clear();
                ExistingStaffList.Items.Clear();
                MappedStaffList.Items.Clear();
                txtNewReporting.Text = "";
                BindTitleList(int.Parse(ddlChannel.SelectedValue));
                //BindTitle();
            }
        }
        protected void BindTitle()
        {
            DataTable dtTitle = advisorStaffBo.GetTitleList(int.Parse(ddltitlechannelId.SelectedValue), advisorVo.advisorId,ddlStaff.SelectedValue);
            ddlTitle.DataSource = dtTitle;
            ddlTitle.DataValueField = dtTitle.Columns["AH_Id"].ToString();
            ddlTitle.DataTextField = dtTitle.Columns["AH_HierarchyName"].ToString();
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void ddlTitle_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtStaffId.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select New Reporting Manager.');", true);
                return;
            }
            else
            {
                BindSourceManager();
            }
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
            DataTable dtBindTitleList = advisorStaffBo.GetStaffTitleList(channelId, advisorVo.advisorId,ddlStaff.SelectedValue);
            ddltitlechannelId.DataSource = dtBindTitleList;
            ddltitlechannelId.DataValueField = dtBindTitleList.Columns["AH_Id"].ToString();
            ddltitlechannelId.DataTextField = dtBindTitleList.Columns["AH_HierarchyName"].ToString();
            ddltitlechannelId.DataBind();
            ddltitlechannelId.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void BindAssociateList(string rnIds)
        {
            DataTable dtBindSourceManager = advisorStaffBo.GetStaffAssociateList(rnIds, advisorVo.advisorId,ddlStaff.SelectedValue);
            ExistingStaffList.DataSource = dtBindSourceManager;
            ExistingStaffList.DataValueField = dtBindSourceManager.Columns["AR_RMId"].ToString();
            ExistingStaffList.DataTextField = dtBindSourceManager.Columns["AR_FirstName"].ToString();
            ExistingStaffList.DataBind();
        }
        protected void chkSourceStaff_OnCheckedChanged(object sender, EventArgs e)
        {
        }
        protected void ChkExistingStaff_OnCheckedChanged(object sender, EventArgs e)
        {

        }
        protected void chkNewStaff_OnCheckedChanged(object sender, EventArgs e)
        {
        }
        //protected void radStaffList_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //string selectStaffAssociate = string.Empty;
        //foreach (RadListBoxItem li in radStaffList.Items)
        //{
        //    if (li.Checked == true)
        //    {
        //        selectStaffAssociate += li.Value + ',';
        //    }
        //}
        //BindAssociateList(selectStaffAssociate.TrimEnd(','));
        //}
        //protected void ExistingStaffList_Transferred(object source, Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        //{
        //    ExistingStaffList.Items.Sort();
        //}
        protected void RightArrow_Click(Object sender, EventArgs e)
        {
            if (ExistingStaffList.SelectedIndex >= 0)
            {
                for (int i = 0; i < ExistingStaffList.Items.Count; i++)
                {
                    if (ExistingStaffList.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(ExistingStaffList.Items[i]))
                        {
                            arraylist1.Add(ExistingStaffList.Items[i]);

                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!MappedStaffList.Items.Contains(((ListItem)arraylist1[i])))
                    {
                        MappedStaffList.Items.Add(((ListItem)arraylist1[i]));
                    }
                    ExistingStaffList.Items.Remove(((ListItem)arraylist1[i]));
                }
                MappedStaffList.SelectedIndex = -1;
            }
            else
            {
                message = "Please select atleast one item";
                Response.Write("<script>alert('" + message + "')</script>");
            }
        }

        protected void LeftArrow_Click(Object sender, EventArgs e)
        {
            if (MappedStaffList.SelectedIndex >= 0)
            {
                for (int i = 0; i < MappedStaffList.Items.Count; i++)
                {
                    if (MappedStaffList.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(MappedStaffList.Items[i]))
                        {
                            arraylist2.Add(MappedStaffList.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!ExistingStaffList.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        ExistingStaffList.Items.Add(((ListItem)arraylist2[i]));
                    }
                    MappedStaffList.Items.Remove(((ListItem)arraylist2[i]));
                }
                ExistingStaffList.SelectedIndex = -1;
            }
            else
            {
                message = "Please select atleast one item";
                Response.Write("<script>alert('" + message + "')</script>");
            }
        }
        protected void RightShift_Click(Object sender, EventArgs e)
        {
            while (ExistingStaffList.Items.Count != 0)
            {
                for (int i = 0; i < ExistingStaffList.Items.Count; i++)
                {
                    MappedStaffList.Items.Add(ExistingStaffList.Items[i]);
                    ExistingStaffList.Items.Remove(ExistingStaffList.Items[i]);
                }
            }
        }
        protected void LeftShift_Click(Object sender, EventArgs e)
        {
            while (MappedStaffList.Items.Count != 0)
            {
                for (int i = 0; i < MappedStaffList.Items.Count; i++)
                {
                    ExistingStaffList.Items.Add(MappedStaffList.Items[i]);
                    MappedStaffList.Items.Remove(MappedStaffList.Items[i]);
                }
            }
        }
        protected void btnStaffList_Click(object sender, EventArgs e)
        {
            string selectStaffAssociate = string.Empty;

            foreach (ListItem li in radStaffList.Items)
            {
                if (li.Selected == true)
                {
                    selectStaffAssociate += li.Value + ',';
                }
            }
            if (selectStaffAssociate == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Source Reporting Manager.');", true);
                return;

            }
            BindAssociateList(selectStaffAssociate.TrimEnd(','));
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int newManagerId;
            string mappedStaffAssociate = string.Empty;
            if (txtStaffId.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select New Reporing Manager.');", true);
                return;
            }
            else
            {
                newManagerId = int.Parse(txtStaffId.Value);
            }
            if (MappedStaffList.Items.Count > 0)
            {
                foreach (ListItem li in MappedStaffList.Items)
                {
                    //if (li.Selected == true)
                    //{
                        mappedStaffAssociate += li.Value + ',';
                    //}
                }
                if (mappedStaffAssociate == string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Mapped Staff.');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Existing Staff To Mapped Staff.');", true);
                return;
            }
            advisorStaffBo.UpdateReportingManager(mappedStaffAssociate.TrimEnd(','), newManagerId, advisorVo.advisorId, ddlStaff.SelectedValue);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Reporting Manager Change Successfully!!');", true);

        }
    }
}