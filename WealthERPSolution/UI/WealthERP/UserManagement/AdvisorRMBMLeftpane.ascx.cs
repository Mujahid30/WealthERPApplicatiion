using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using BoCommon;
using WealthERP.Base;


namespace WealthERP.UserManagement
{
    public partial class AdvisorRMBMLeftpane : System.Web.UI.UserControl
    {
        UserVo userVo;
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        int bmBranchId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            Session["rmVo"] = rmVo;
            bmBranchId = advisorBranchBo.GetBranchId(rmVo.RMId);
            if (bmBranchId ==0 )
            {

            }
            else
            {
                advisorBranchVo = advisorBranchBo.GetBranch(bmBranchId);
                Session["advisorBranchVo"] = advisorBranchVo;
            }
            if (!IsPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorRMBMLeftpane');", true);
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
            {
                SetNode();
            }
        }
        public void SetNode()
        {
            if (TreeView1.SelectedNode.Value == "Advisor")
            {
                Session[SessionContents.CurrentUserRole] = "Admin";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "RM")
            {
                Session[SessionContents.CurrentUserRole] = "RM";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "Branch Manager")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('BMDashBoard','login');", true);

            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            
           
        }
    }
}
