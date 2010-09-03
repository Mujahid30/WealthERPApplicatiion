using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;

namespace WealthERP.Advisor
{
    public partial class FinancialPlanningLeftPane : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("FP_UserName");
                Session.Remove("FP_UserID");
                if (Session[SessionContents.CurrentUserRole] != null)
                {
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('RMLeftPane');", true);
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorLeftPane');", true);
                    }
                }
            }

        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode.Value == "RiskProfileAssetAllocation")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FinancialPlanning','login')", true);

            }
            else if (TreeView1.SelectedNode.Value == "GoalProfiling")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageLoadscript", "loadcontrol('AddCustomerFinancialPlanningGoalSetup','login')", true);

            }
            else if (TreeView1.SelectedNode.Value == "Reports")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageLoadscript", "loadcontrol('FinancialPlanningReports','login')", true);

            }
        }
    }
}