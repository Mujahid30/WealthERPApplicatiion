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
            msgRecordStatus.Visible = false;
            lblMessage.Visible = false;
             customerVo=(CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                customerGoalDetailsDS = goalPlanningBo.GetCustomerGoalDetails(customerVo.CustomerId);
                
               

                if (customerGoalDetailsDS.Tables[0].Rows.Count > 0)
                    BindCustomerGoalDetailGrid(customerGoalDetailsDS);
                else
                    lblMessage.Visible = true;
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
            decimal goalFundPercentage = 0;
            decimal goalAmountInGoalYear=0;

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
            dtCustomerGoalDetails.Columns.Add("GoalFundPercentage");
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
                drCustomerGoalDetails["CostToday"] =Math.Round(double.Parse(dr["CG_CostToday"].ToString()),0);
                drCustomerGoalDetails["GaolYear"] = dr["CG_GoalYear"].ToString();
                goalAmountInGoalYear = Math.Round(Decimal.Parse(dr["CG_FVofCostToday"].ToString()), 0);
                drCustomerGoalDetails["GoalAmountInGoalYear"] = String.Format("{0:n2}", goalAmountInGoalYear.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drCustomerGoalDetails["GoalPriority"] = dr["CG_Priority"].ToString();
               
                drGoalFundDetails = customerGoalDetailsDS.Tables[1].Select("CG_GoalId=" + dr["CG_GoalId"].ToString());

                drCustomerGoalDetails["CorpusLeftBehind"] = String.Format("{0:n2}", 50000.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

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
                    goalFundPercentage = Math.Round(((totalFundAmount / goalAmountInGoalYear) * 100), 0);

                }
                drCustomerGoalDetails["GoalFundPercentage"] = goalFundPercentage;
                drCustomerGoalDetails["EquityFundedAmount"] = String.Format("{0:n2}", equityFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drCustomerGoalDetails["DebtFundedAmount"] = String.Format("{0:n2}", debtFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drCustomerGoalDetails["CashFundedAmount"] = String.Format("{0:n2}", cashFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drCustomerGoalDetails["AlternateFundedAmount"] = String.Format("{0:n2}", alternateFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drCustomerGoalDetails["TotalFundedAmount"] = String.Format("{0:n2}", totalFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                goalAmountRequired = decimal.Parse(dr["CG_FVofCostToday"].ToString());
                gapAmountAfterFund=goalAmountRequired-totalFundAmount;
                drCustomerGoalDetails["GoalFundedGap"] = String.Format("{0:n2}", gapAmountAfterFund.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
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
        protected void gvrGoalPlanning_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgPartiallyFunded = e.Row.FindControl("imgPartiallyFunded") as Image;
                Image imgFullyFunded = e.Row.FindControl("imgFullyFunded") as Image;
                Image imgNotFunded = e.Row.FindControl("imgNotFunded") as Image;
                Label lblgoalFundPercentage = e.Row.FindControl("lblGoalFundPercentage") as Label;
                int goalFundPercentage = 0;
                goalFundPercentage = int.Parse(lblgoalFundPercentage.Text.ToString());

                if (goalFundPercentage < 25)
                {
                    imgNotFunded.ImageUrl = "~/Images/cross.jpg";
                }
                else if (goalFundPercentage >= 25 || goalFundPercentage <= 99)
                {
                    imgPartiallyFunded.ImageUrl = "~/Images/check.jpg";
                }
                else if (goalFundPercentage > 99)
                {
                    imgFullyFunded.ImageUrl = "~/Images/help.jpg";
                }
            }

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
            hdndeleteId.Value = goalId;
            goalCatagory = gvrGoalPlanning.DataKeys[selectedRow].Values["GoalCategory"].ToString();
            goalAction = ddlAction.SelectedValue.ToString();

            if (ddlAction.SelectedValue == "View" || ddlAction.SelectedValue == "Edit")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerFPGoalSetup','?GoalId=" + goalId + "&GoalCategory=" + goalCatagory + "&GoalAction=" + goalAction + "');", true);
            }
            else if (ddlAction.SelectedValue == "Fund")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('CustomerFPGoalFunding','?GoalId=" + goalId + "');", true);
            }
            else if (ddlAction.SelectedValue == "Delete")
            {
               Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            }
            
        }
        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                goalPlanningBo.DeleteCustomerGoalFunding(int.Parse(hdndeleteId.Value), customerVo.CustomerId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalPlanningDetails','login');", true);
                msgRecordStatus.Visible = true; 
            }
        }
    }
}