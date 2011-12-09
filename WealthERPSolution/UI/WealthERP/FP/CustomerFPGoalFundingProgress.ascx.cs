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

namespace WealthERP.FP
{
    public partial class CustomerFPGoalFundingProgress : System.Web.UI.UserControl
    {
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        int goalId = 0;
        CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
          
            if (Request.QueryString["goalId"] != null)
            {
                goalId = int.Parse(Request.QueryString["goalId"].ToString());
            }
            SetGoalDetails();
            //mdlPopupSlabCalculate.TargetControlID = "hdnModalPopupId";
            //mdlPopupSlabCalculate.Hide();
            if(!IsPostBack)
           BindExistingInvestmentGrid();
           gvExistInvestMapping.Visible = false;
           btnMapAllocation.Visible = false;
        }


        public void BindExistingInvestmentGrid()
        {
            DataSet dsExistingInvestment = new DataSet();
            DataTable dtCustomerGoalFundingDetails = new DataTable();
            dtCustomerGoalFundingDetails.Columns.Add("GoalId");
            dtCustomerGoalFundingDetails.Columns.Add("SchemeCode");
            dtCustomerGoalFundingDetails.Columns.Add("Schemes");
            dtCustomerGoalFundingDetails.Columns.Add("InvestedAmount");
            dtCustomerGoalFundingDetails.Columns.Add("Units");
            dtCustomerGoalFundingDetails.Columns.Add("CurrentValue");
            dtCustomerGoalFundingDetails.Columns.Add("AllocationEntry");
            dtCustomerGoalFundingDetails.Columns.Add("CurrentGoalAllocation");
            dtCustomerGoalFundingDetails.Columns.Add("OtherGoalAllocation");
            DataRow drCustomerGoalFundingDetails;
            dtCustomerGoalFundingDetails.Columns.Add("AvailableAllocation");
          
            dsExistingInvestment = customerGoalPlanningBo.GetExistingInvestmentDetails(customerVo.CustomerId, goalId);
           
            DataRow[] drExistingInvestmentGoalId;
            DataRow[] drExistingInvestmentSchemePlanId;
            DataRow[] drExistingInvestmentCurrentAllocation;
            int schemePlanCode = 0;
            decimal currentAllocation = 0; ;
            //foreach(DataRow drGoalId in dsExistingInvestment.Tables[1].Rows)
            //{
            //drExistingInvestmentGoalId = dsExistingInvestment.Tables[1].Select("CG_GoalId=" + goalId.ToString());

            foreach (DataRow drGoalExistingInvestments in dsExistingInvestment.Tables[0].Rows)
            {
                schemePlanCode = int.Parse(drGoalExistingInvestments["PASP_SchemePlanCode"].ToString());
                drCustomerGoalFundingDetails = dtCustomerGoalFundingDetails.NewRow();
                drExistingInvestmentSchemePlanId = dsExistingInvestment.Tables[2].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());

                drExistingInvestmentCurrentAllocation = dsExistingInvestment.Tables[3].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());

                if (drExistingInvestmentCurrentAllocation.Count() > 0)
                {
                    foreach (DataRow dr in drExistingInvestmentCurrentAllocation)
                    {
                        currentAllocation = decimal.Parse(dr["allocatedPercentage"].ToString());
                    }
                }

                else
                    currentAllocation = 0;
                if (drExistingInvestmentSchemePlanId.Count() > 0)
                        {
                            foreach (DataRow drSchemeId in drExistingInvestmentSchemePlanId)
                            {
                               
                                if (decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) < 100)
                                {
                                    drCustomerGoalFundingDetails["GoalId"]= goalId.ToString();
                                    drCustomerGoalFundingDetails["Schemes"] = drGoalExistingInvestments["PASP_SchemePlanName"].ToString();
                                    drCustomerGoalFundingDetails["SchemeCode"] = drGoalExistingInvestments["PASP_SchemePlanCode"].ToString();
                                
                                    drCustomerGoalFundingDetails["InvestedAmount"] = drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString();
                                    drCustomerGoalFundingDetails["Units"] = drGoalExistingInvestments["CMFNP_NetHoldings"].ToString();
                                    drCustomerGoalFundingDetails["CurrentValue"] = drGoalExistingInvestments["CMFNP_CurrentValue"].ToString();
                                    drCustomerGoalFundingDetails["AllocationEntry"] = drSchemeId["allocatedPercentage"].ToString();
                                    drCustomerGoalFundingDetails["AvailableAllocation"] = 100 - decimal.Parse(drSchemeId["allocatedPercentage"].ToString());
                                    drCustomerGoalFundingDetails["CurrentGoalAllocation"] = currentAllocation.ToString();
                                    drCustomerGoalFundingDetails["OtherGoalAllocation"] = (decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) - currentAllocation).ToString();
                                    dtCustomerGoalFundingDetails.Rows.Add(drCustomerGoalFundingDetails);
                                }
                                
                            }
                        }
                        else
                            {
                                drCustomerGoalFundingDetails["GoalId"] = goalId.ToString();
                                drCustomerGoalFundingDetails["Schemes"] = drGoalExistingInvestments["PASP_SchemePlanName"].ToString();
                                drCustomerGoalFundingDetails["SchemeCode"] = drGoalExistingInvestments["PASP_SchemePlanCode"].ToString();
                                  
                                drCustomerGoalFundingDetails["InvestedAmount"] = drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString();
                                drCustomerGoalFundingDetails["Units"] = drGoalExistingInvestments["CMFNP_NetHoldings"].ToString();
                                drCustomerGoalFundingDetails["CurrentValue"] = drGoalExistingInvestments["CMFNP_CurrentValue"].ToString();
                                drCustomerGoalFundingDetails["AllocationEntry"] = "0";
                                drCustomerGoalFundingDetails["AvailableAllocation"] = "100";
                                drCustomerGoalFundingDetails["CurrentGoalAllocation"] = currentAllocation.ToString();
                                drCustomerGoalFundingDetails["OtherGoalAllocation"] =  "0";
                               
                                dtCustomerGoalFundingDetails.Rows.Add(drCustomerGoalFundingDetails);

                            }

                 
                    
                


            }
           
            gvExistInvestMapping.DataSource = dtCustomerGoalFundingDetails;
            gvExistInvestMapping.DataBind();
            
        }

        public void SetGoalDetails()
     {
            
            goalPlanningVo = customerGoalPlanningBo.GetGoalDetails(goalId);
            if (goalPlanningVo.Goalcode == "ED")
                goalPlanningVo.GoalName = "Child Education";
            else if (goalPlanningVo.Goalcode == "BH")
                goalPlanningVo.GoalName = "Buy Home";
            else if (goalPlanningVo.Goalcode == "MR")
                goalPlanningVo.GoalName = "Child Marriage";
            else if (goalPlanningVo.Goalcode == "OT")
                goalPlanningVo.GoalName = "Other";
            else if (goalPlanningVo.Goalcode == "RT")
                goalPlanningVo.GoalName = "Retirement";
            txtGoalName.Text = goalPlanningVo.GoalName;
            txtGoalYearDetails.Text = goalPlanningVo.GoalYear.ToString();
            txtGoalTargetAmountDetails.Text = goalPlanningVo.CostOfGoalToday.ToString();
            

        }


        protected void btnMapAllocation_OnClick(object sender, EventArgs e)
        {
             string allocationEntry = string.Empty;
            string allocationAvailable = string.Empty;
         foreach (GridViewRow dr in gvExistInvestMapping.Rows)
            { 
                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked)
                    {
                        int goalId = int.Parse(gvExistInvestMapping.DataKeys[dr.RowIndex].Values["GoalId"].ToString());
                        int schemeId = int.Parse(gvExistInvestMapping.DataKeys[dr.RowIndex].Values["SchemeCode"].ToString());
                        decimal  otherAllocation = decimal.Parse(gvExistInvestMapping.DataKeys[dr.RowIndex].Values["OtherGoalAllocation"].ToString());
                        allocationEntry = ((TextBox)dr.FindControl("txtAllocationEntry")).Text;
                        allocationAvailable = ((Label)dr.FindControl("lblAvailableAllocation")).Text;

                        if (decimal.Parse(allocationEntry) + otherAllocation <= 100)
                        {
                            customerGoalPlanningBo.UpdateGoalAllocationPercentage(decimal.Parse(allocationEntry), schemeId, goalId);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Allocation exceeding 100%');", true);
       

                        }

                    }
             

                   
                   
                
            }
         BindExistingInvestmentGrid();
         gvExistInvestMapping.Visible = true;
         btnMapAllocation.Visible = true;

        }
        protected void ddlGoalMapping_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //mdlPopupSlabCalculate.TargetControlID = "ddlGoalMapping";
            //mdlPopupSlabCalculate.Show();
            //hdnModalPopupId.Value = ddlGoalMapping.SelectedValue;
            if (ddlGoalMapping.SelectedValue == "Investment")
            {
                gvExistInvestMapping.Visible = true;
                btnMapAllocation.Visible = true;

            }

        }
    }
}