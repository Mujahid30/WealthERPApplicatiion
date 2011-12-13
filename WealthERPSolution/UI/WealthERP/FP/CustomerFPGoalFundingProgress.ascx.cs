using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoFPSuperlite;
using VoFPSuperlite;
using System.Data;
using VoUser;
using Telerik.Web.UI;
using BoCommon;

namespace WealthERP.FP
{
    public partial class CustomerFPGoalFundingProgress : System.Web.UI.UserControl
    {
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        int goalId;
        CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["customerVo"]!=null)
             customerVo = (CustomerVo)Session["customerVo"];
          
            if (Request.QueryString["goalId"] != null)
            {
                goalId = int.Parse(Request.QueryString["goalId"].ToString());
            }
            ShowGoalDetails(goalId);
            BindExistingFundingScheme();
        }

        protected void ShowGoalDetails(int goalId)
        {
            goalPlanningVo = customerGoalPlanningBo.GetGoalDetails(goalId);
            if (goalPlanningVo != null)
            {
               if (goalPlanningVo.GoalName!=null)
               txtGoalName.Text= goalPlanningVo.GoalName.Trim();
               if (goalPlanningVo.GoalProfileDate!=DateTime.MinValue)
               txtStartDate.Text= goalPlanningVo.GoalProfileDate.Year.ToString();
               txtTargetDate.Text = goalPlanningVo.GoalYear.ToString();
               txtGoalAmount.Text = goalPlanningVo.FutureValueOfCostToday.ToString();
               txtTenureCompleted.Text = (goalPlanningVo.GoalProfileDate.Year - DateTime.Now.Year).ToString();
               txtBalanceTenor.Text = (goalPlanningVo.GoalYear - DateTime.Now.Year).ToString();
               txtMonthlyContribution.Text = string.Empty;
               txtAmountInvested.Text = goalPlanningVo.CurrInvestementForGoal.ToString();
               txtValueOfCurrentGoal.Text = string.Empty;
               txtReturnsXIRR.Text = string.Empty;
               txtCostAtBeginning.Text = string.Empty;
               txtEstmdTimeToReachGoal.Text = string.Empty;
               txtProjectedValueOnGoalDate.Text = string.Empty;
               txtProjectedGap.Text = string.Empty;
               txtAdditionalInvestmentsRequired.Text = string.Empty;
               txtAdditionalInvestments.Text = string.Empty;
 
            }
            SetControlsReadOnly();
        }

        protected void SetControlsReadOnly()
        {
            txtGoalName.Enabled = false;
            txtStartDate.Enabled = false;
            txtTargetDate.Enabled = false;
            txtGoalAmount.Enabled = false;
            txtTenureCompleted.Enabled = false;
            txtBalanceTenor.Enabled = false;
            txtMonthlyContribution.Enabled = false;
            txtAmountInvested.Enabled = false;
            txtValueOfCurrentGoal.Enabled = false;
            txtReturnsXIRR.Enabled = false;
            txtCostAtBeginning.Enabled = false;
            txtEstmdTimeToReachGoal.Enabled = false;
            txtProjectedValueOnGoalDate.Enabled = false;
            txtProjectedGap.Enabled = false;
            txtAdditionalInvestmentsRequired.Enabled = false;
            txtAdditionalInvestments.Enabled = false;

        }


        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            
        }

        //protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        //{
            
        //}

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
            //    editColumn.Visible = false;
            //}
            //else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            //{
            //    e.Canceled = true;
            //}
            //else
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
            //    if (!editColumn.Visible)
            //        editColumn.Visible = true;
            //}
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadGrid1.EditIndexes.Add(0);
                RadGrid1.Rebind();
            }
        }

        protected void BindExistingFundingScheme()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SchemeName");
            dt.Columns.Add("AllocationPercent");
            dt.Columns.Add("InvestedAmount");
            dt.Columns.Add("Units");
            dt.Columns.Add("CurrentValue");
            dt.Columns.Add("ReturnsXIRR");
            dt.Columns.Add("ProjectedAmount");

            DataRow dr = dt.NewRow();
            dr["SchemeName"] = "Birla Sun Life";
            dr["AllocationPercent"] = "50";
            dr["InvestedAmount"] = "50000";
            dr["Units"] = "60";
            dr["CurrentValue"] = "100000";
            dr["ReturnsXIRR"] = "12.78";
            dr["ProjectedAmount"] = "1000000";

            dt.Rows.Add(dr);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();



        }
      
    }
}