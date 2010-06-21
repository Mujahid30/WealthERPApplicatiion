using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using BoCommon;

namespace WealthERP.UserManagement
{
    public partial class BMDashBoard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            UserVo userVo = new UserVo();
            RMVo rmVo = new RMVo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            AdvisorBranchBo advisorBranchBo=new AdvisorBranchBo();
            int branchId;
            userVo = (UserVo)Session["userVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            Session["rmVo"] = rmVo;
            branchId = advisorBranchBo.GetBranchId(rmVo.RMId);
            if (branchId != null)
            {
                advisorBranchVo = advisorBranchBo.GetBranch(branchId);
                Session["advisorBranchVo"] = advisorBranchVo;
            }
           
            

        }
    }
}