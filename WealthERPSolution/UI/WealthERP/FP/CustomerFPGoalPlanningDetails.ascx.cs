using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoFPSuperlite;
using System.Data;
using VoUser;
using WealthERP.Base;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using BoCommon;
using VoFPSuperlite;
using VoUser;


namespace WealthERP.FP
{
    public partial class CustomerFPGoalPlanningDetails : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        DataSet customerGoalDetailsDS;
        protected void Page_Load(object sender, EventArgs e)
        {
             customerVo=(CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                customerGoalDetailsDS = goalPlanningBo.GetCustomerGoalDetails(customerVo.CustomerId);
                BindCustomerGoalDetailGrid(customerGoalDetailsDS);
            }
        }

        public void BindCustomerGoalDetailGrid(DataSet customerGoalDetailsDS)
        {
            decimal equityFundAmount = 0;
            decimal debtFundAmount = 0;
            decimal cashFundAmount = 0;
            decimal alternateFundAmount = 0;
            decimal totalFundAmount = 0;
            decimal gapAmountAfterFund = 0;
            decimal goalAmountRequired = 0;

            DataTable dtCustomerGoalDetails = new DataTable();
            dtCustomerGoalDetails.Columns.Add("GoalId");
            dtCustomerGoalDetails.Columns.Add("GoalCategory");
            dtCustomerGoalDetails.Columns.Add("GoalType");
            dtCustomerGoalDetails.Columns.Add("ChildName");
            dtCustomerGoalDetails.Columns.Add("CostToday");
            dtCustomerGoalDetails.Columns.Add("GaolYear");

            dtCustomerGoalDetails.Columns.Add("GoalAmountInGoalYear");
            dtCustomerGoalDetails.Columns.Add("CorpusLeftBehind");
            dtCustomerGoalDetails.Columns.Add("GoalPriority");
            

            dtCustomerGoalDetails.Columns.Add("EquityFundedAmount");
            dtCustomerGoalDetails.Columns.Add("DebtFundedAmount");
            dtCustomerGoalDetails.Columns.Add("CashFundedAmount");
            dtCustomerGoalDetails.Columns.Add("AlternateFundedAmount");

            dtCustomerGoalDetails.Columns.Add("TotalFundedAmount");
            dtCustomerGoalDetails.Columns.Add("GoalFundedGap");
            dtCustomerGoalDetails.Columns.Add("GoalFundedType");
            

            DataRow drCustomerGoalDetails;
            DataRow[] drGoalFundDetails;
            foreach (DataRow dr in customerGoalDetailsDS.Tables[0].Rows)
            {
                drCustomerGoalDetails = dtCustomerGoalDetails.NewRow();
                drCustomerGoalDetails["GoalId"] = dr["CG_GoalId"].ToString();

                if (dr["XG_GoalCode"].ToString() == "RT")
                     drCustomerGoalDetails["GoalCategory"] = "RT";
                else
                     drCustomerGoalDetails["GoalCategory"] = "NonRT";

                drCustomerGoalDetails["GoalType"] = dr["XG_GoalName"].ToString();
                drCustomerGoalDetails["ChildName"] = dr["ChildName"].ToString();
                drCustomerGoalDetails["CostToday"] =Math.Round(double.Parse(dr["CG_CostToday"].ToString()),2);
                drCustomerGoalDetails["GaolYear"] = dr["CG_GoalYear"].ToString();
                drCustomerGoalDetails["GoalAmountInGoalYear"] = Math.Round(double.Parse(dr["CG_FVofCostToday"].ToString()), 2);
                drCustomerGoalDetails["GoalPriority"] = dr["CG_Priority"].ToString();
               
                drGoalFundDetails = customerGoalDetailsDS.Tables[1].Select("CG_GoalId=" + dr["CG_GoalId"].ToString());

                drCustomerGoalDetails["CorpusLeftBehind"] = 50000;

                if (drGoalFundDetails.Count() > 0)
                {
                    equityFundAmount = decimal.Parse((drGoalFundDetails[0]["CGF_AllocatedAmount"].ToString()));
                    debtFundAmount = decimal.Parse(drGoalFundDetails[1]["CGF_AllocatedAmount"].ToString());
                    cashFundAmount = decimal.Parse(drGoalFundDetails[2]["CGF_AllocatedAmount"].ToString());
                    if (drGoalFundDetails[2] != null)
                        alternateFundAmount = decimal.Parse(drGoalFundDetails[3]["CGF_AllocatedAmount"].ToString());
                    else
                        alternateFundAmount = 0;
                    totalFundAmount = equityFundAmount + debtFundAmount + cashFundAmount + alternateFundAmount;

                }

                drCustomerGoalDetails["EquityFundedAmount"] = equityFundAmount;
                drCustomerGoalDetails["DebtFundedAmount"] = debtFundAmount;
                drCustomerGoalDetails["CashFundedAmount"] = cashFundAmount;
                drCustomerGoalDetails["AlternateFundedAmount"] = alternateFundAmount;
                drCustomerGoalDetails["TotalFundedAmount"] = totalFundAmount;
                goalAmountRequired = decimal.Parse(dr["CG_FVofCostToday"].ToString());
                gapAmountAfterFund=goalAmountRequired-totalFundAmount;
                drCustomerGoalDetails["GoalFundedGap"] = gapAmountAfterFund;
                if (totalFundAmount >= goalAmountRequired)
                {
                    drCustomerGoalDetails["GoalFundedType"] = "FULL";
                }
                dtCustomerGoalDetails.Rows.Add(drCustomerGoalDetails);
                equityFundAmount = 0;
                debtFundAmount = 0;
                cashFundAmount = 0;
                alternateFundAmount = 0;
                totalFundAmount = 0;
                gapAmountAfterFund = 0;
 
            }

            gvrGoalPlanning.DataSource = dtCustomerGoalDetails;
            gvrGoalPlanning.DataBind();
 
        }


        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            DropDownList ddlAction = null;
            GridViewRow gvGoal = null;
            int selectedRow = 0;
            string goalId = "";
            string goalCatagory = "";
            string goalAction = "";

            ddlAction = (DropDownList)sender;
            gvGoal = (GridViewRow)ddlAction.NamingContainer;
            selectedRow = gvGoal.RowIndex; 
            goalId = gvrGoalPlanning.DataKeys[selectedRow].Values["GoalId"].ToString();
            goalCatagory = gvrGoalPlanning.DataKeys[selectedRow].Values["GoalCategory"].ToString();
            goalAction = ddlAction.SelectedValue.ToString();            

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerFPGoalSetup','?GoalId=" + goalId + "&GoalCategory=" + goalCatagory + "&GoalAction=" + goalAction + "');", true);
           

        }
        
    }
}