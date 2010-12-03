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
       

        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            try
            {
                if (e.Item.Value == "Advisor")
                {
                    Session[SessionContents.CurrentUserRole] = "Admin";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','login');", true);
                }
                else if (e.Item.Value == "RM")
                {
                    Session[SessionContents.CurrentUserRole] = "RM";
                    if (Session["CurrentrmVo"] != null)
                    {
                        Session.Remove("CurrentrmVo");
                    }
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
                }
                else if (e.Item.Value == "Branch Manager")
                {
                    Session[SessionContents.CurrentUserRole] = "BM";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('BMDashBoard','login');", true);
                }

            }
            catch (Exception ex)
            {
                bool value = false;
            }
        }
    }
}
