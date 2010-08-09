using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using BoCommon;

namespace WealthERP.UserManagement
{
    public partial class AdvisorRMDashboard : System.Web.UI.UserControl
    {
        UserVo userVo;
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        int branchId;
        AdvisorVo adviserVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (Session["rmVo"] == null)
            {
                rmVo = advisorStaffBo.GetAdvisorStaff(adviserVo.UserId);
                Session["rmVo"] = rmVo;
            }
            else
            {
                rmVo = (RMVo)Session["rmVo"];
            }
            branchId = advisorBranchBo.GetBranchId(rmVo.RMId);
            //adviserVo = (AdvisorVo)Session["advisorVo"];
            if (branchId != 0 || adviserVo.MultiBranch == 0)
            {
                lnkAdd.Visible = false;
                lbl.Visible = false;
                Session["advisorBranchVo"] = advisorBranchBo.GetBranch(branchId);
            }
            else
            {
                lnkAdd.Visible = true;
                lbl.Visible = true;
            }

        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Session["BranchAdd"] = "forAdvisor";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AddBranch','none');", true);
        }
    }
}