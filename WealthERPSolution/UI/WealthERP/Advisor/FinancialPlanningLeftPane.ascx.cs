using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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