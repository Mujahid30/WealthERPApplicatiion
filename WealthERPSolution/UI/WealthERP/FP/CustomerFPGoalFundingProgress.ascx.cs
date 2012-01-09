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
using System.Web.UI.HtmlControls;
namespace WealthERP.FP
{
    public partial class CustomerFPGoalFundingProgress : System.Web.UI.UserControl
    {
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerAssumptionVo customerAssumptionVo=new CustomerAssumptionVo();
        AdvisorVo advisorVo = new AdvisorVo();
        int goalId;
        CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerVo customerVo = new CustomerVo();
        DataTable dtCustomerGoalFunding = new DataTable();
        DataSet dsExistingInvestment = new DataSet();
        DataTable dtCustomerSIPGoalFunding = new DataTable();
        DataSet dsSIPInvestment = new DataSet();
        decimal weightedReturn = 0;
        decimal totalInvestedSIPamount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["customerVo"]!=null)
             customerVo = (CustomerVo)Session["customerVo"];
             advisorVo = (AdvisorVo)Session["advisorVo"];
          
            if (Request.QueryString["goalId"] != null)
            {
                goalId = int.Parse(Request.QueryString["goalId"].ToString());
            }
            if (!IsPostBack)
            {               
                
            }
            BindExistingFundingScheme();
            BindMonthlySIPFundingScheme();
            CalculateweightedReturn();
            ShowGoalDetails(goalId);
          
        }

        protected void ShowGoalDetails(int goalId)
        {

            if (goalPlanningVo != null)
            {
                if (goalPlanningVo.GoalName != null)
                    txtGoalName.Text = goalPlanningVo.GoalName.Trim();
                if (goalPlanningVo.GoalProfileDate != DateTime.MinValue)
                    txtStartDate.Text = goalPlanningVo.GoalProfileDate.Year.ToString();
                txtTargetDate.Text = goalPlanningVo.GoalYear.ToString();
                txtGoalAmount.Text = String.Format("{0:n2}", Math.Round(goalPlanningVo.FutureValueOfCostToday, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                txtTenureCompleted.Text = (DateTime.Now.Year - goalPlanningVo.GoalProfileDate.Year).ToString();
                txtBalanceTenor.Text = (goalPlanningVo.GoalYear - DateTime.Now.Year).ToString();
                //txtMonthlyContribution.Text = string.Empty;
                //txtAmountInvested.Text = goalPlanningVo.CurrInvestementForGoal.ToString();
                txtValueOfCurrentGoal.Text = string.Empty;
                txtReturnsXIRR.Text = string.Empty;
                txtCostAtBeginning.Text = String.Format("{0:n2}", Math.Round(goalPlanningVo.CostOfGoalToday, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                txtEstmdTimeToReachGoal.Text = string.Empty;
                //txtProjectedValueOnGoalDate.Text = string.Empty;
                txtProjectedGap.Text = string.Empty;
                txtAdditionalInvestmentsRequired.Text = string.Empty;
                txtAdditionalInvestments.Text = string.Empty;
                decimal totalMFFundingInvestedAmount = 0;
                decimal totalMFSIPFunding = 0;
                decimal totalMFCurrentValue = 0;
                decimal totalMFProjectedAmount = 0;

                foreach (DataRow dr in dtCustomerGoalFunding.Rows)
                {
                    totalMFFundingInvestedAmount = totalMFFundingInvestedAmount + decimal.Parse(dr["InvestedAmount"].ToString());
                    if (dr["CurrentValue"].ToString() != "")
                    {
                        totalMFCurrentValue = totalMFCurrentValue + decimal.Parse(dr["CurrentValue"].ToString());
                    }
                    else
                    {
                        totalMFCurrentValue = totalMFCurrentValue + 0;
                    }
                    if (dr["ProjectedAmount"].ToString() != "")
                    {
                        totalMFProjectedAmount = totalMFProjectedAmount + decimal.Parse(dr["ProjectedAmount"].ToString());
                    }
                    else
                    {
                        totalMFProjectedAmount = totalMFProjectedAmount + 0;
                    }
                }
                foreach (DataRow dr in dtCustomerSIPGoalFunding.Rows)
                {
                    totalMFSIPFunding = totalMFSIPFunding + decimal.Parse(dr["SIPInvestedAmount"].ToString());
                }
                txtAmountInvested.Text = String.Format("{0:n2}", Math.Round(totalMFFundingInvestedAmount, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                txtMonthlyContribution.Text = String.Format("{0:n2}", Math.Round(totalMFSIPFunding, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                txtProjectedValueOnGoalDate.Text = String.Format("{0:n2}", Math.Round(totalMFProjectedAmount, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                txtValueOfCurrentGoal.Text = String.Format("{0:n2}", Math.Round(totalMFCurrentValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                if (dtCustomerGoalFunding.Rows.Count > 0)
                {
                    double returns = double.Parse((weightedReturn / 100).ToString());
                    double remainingTime = customerGoalPlanningBo.NPER(returns, 0, -double.Parse(totalMFCurrentValue.ToString()), double.Parse(totalMFProjectedAmount.ToString()), 0);
                    int year = 0;
                    double month = 0;
                    year = (int)remainingTime;
                    month = remainingTime - year;
                    month = Math.Round((month * 12), 0);
                    txtEstmdTimeToReachGoal.Text = year + "-" + "Years" + "" + month + "-" + "Months";
                    txtAdditionalInvestments.Text = String.Format("{0:n2}", Math.Round(((double.Parse(txtProjectedValueOnGoalDate.Text) - double.Parse(txtGoalAmount.Text)) / remainingTime), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    txtAdditionalInvestmentsRequired.Text = String.Format("{0:n2}", Math.Round((double.Parse(txtAdditionalInvestments.Text) / 12), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                }
                txtReturnsXIRR.Text = Math.Round(weightedReturn, 2).ToString();
                if (txtGoalAmount.Text != "")
                {
                    txtProjectedGap.Text = String.Format("{0:n2}", Math.Round((totalMFProjectedAmount - decimal.Parse(txtGoalAmount.Text)), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                }
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

        protected void RadGrid2_ItemInserted(object source, GridCommandEventArgs e)
        {

            decimal totalOtherAllocation = 0;
            decimal currentAllocation = 0;
            decimal totalSIPAmount = 0;
            GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickSIPScheme");
            TextBox txt = (TextBox)e.Item.FindControl("TextBox3");
            int sipId = int.Parse(ddl.SelectedValue);

            DataRow[] drSIPId;
            DataRow[] drSIPInvestmentPlanId;
            DataRow[] drSIPCurrentInvestment;
            DataRow[] drTotalSIPamount;
            drSIPId = dtCustomerSIPGoalFunding.Select("SIPId=" + sipId.ToString());
            drSIPCurrentInvestment = dsSIPInvestment.Tables[0].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
         
            drTotalSIPamount = dsSIPInvestment.Tables[2].Select("CMFSS_SystematicSetupId=" + sipId.ToString());

            if (drTotalSIPamount.Count() > 0)
            {
                foreach (DataRow dr in drTotalSIPamount)
                {
                    totalSIPAmount = decimal.Parse(dr["CMFSS_Amount"].ToString());
                }
            }

            else
                totalSIPAmount = 0;

 

            if (drSIPCurrentInvestment.Count() > 0)
            {
                foreach (DataRow dr in drSIPCurrentInvestment)
                {
                    currentAllocation = decimal.Parse(dr["InvestedAmount"].ToString());
                }
            }

            else
                currentAllocation = 0;

            if (drSIPId.Count() > 0)
            {
                foreach (DataRow drSipId in drSIPId)
                {
                    totalOtherAllocation = totalOtherAllocation + decimal.Parse(drSipId["SIPInvestedAmount"].ToString()) - currentAllocation;
                }
            }

            if (!string.IsNullOrEmpty(txt.Text))
            {
                if ((decimal.Parse(txt.Text) + totalOtherAllocation) > totalSIPAmount)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have less available amount');", true);
                }
                else
                {
                    customerGoalPlanningBo.UpdateSIPGoalAllocationAmount(decimal.Parse(txt.Text), sipId, goalId);
                    BindMonthlySIPFundingScheme();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
            }
                
            //decimal totalOtherAllocation = 0;
            //decimal currentAllocation = 0;
            //decimal totalAllocation = 0;
            //GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
            //DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickSIPScheme");
            //TextBox txt = (TextBox)e.Item.FindControl("TextBox3");
            //int schemeplanId = int.Parse(ddl.SelectedValue);

            //DataRow[] drSchemePlanId;
            //DataRow[] drSIPInvestmentPlanId;
            //DataRow[] drSIPCurrentInvestment;
            //DataRow[] drTotalSIPInvestment;
            //drSchemePlanId = dtCustomerSIPGoalFunding.Select("SchemeCode=" + schemeplanId.ToString());
            //drSIPInvestmentPlanId = dsSIPInvestment.Tables[2].Select("PASP_SchemePlanCode=" + schemeplanId.ToString());
            //drSIPCurrentInvestment = dsSIPInvestment.Tables[0].Select("PASP_SchemePlanCode=" + schemeplanId.ToString());
            //drTotalSIPInvestment = dsSIPInvestment.Tables[1].Select("PASP_SchemePlanCode=" + schemeplanId.ToString());

            //if (drTotalSIPInvestment.Count() > 0)
            //{
            //    foreach (DataRow dr in drTotalSIPInvestment)
            //    {
            //        totalAllocation = decimal.Parse(dr["SIPAmount"].ToString());
            //    }
            //}

            //else
            //    totalAllocation = 0;

            //if (drSIPCurrentInvestment.Count() > 0)
            //{
            //    foreach (DataRow dr in drSIPCurrentInvestment)
            //    {
            //        currentAllocation = decimal.Parse(dr["CEMFSSTGA_AllocationAmt"].ToString());
            //    }
            //}

            //else
            //    currentAllocation = 0;

            //if (drSIPInvestmentPlanId.Count() > 0)
            //{
            //    foreach (DataRow drSchemeId in drSIPInvestmentPlanId)
            //    {
            //        totalOtherAllocation = totalOtherAllocation + decimal.Parse(drSchemeId["totalAllocation"].ToString()) - currentAllocation;
            //    }
            //}

            //if (!string.IsNullOrEmpty(txt.Text))
            //{
            //    if ((decimal.Parse(txt.Text) + totalOtherAllocation) > totalAllocation)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have less available amount');", true);
            //    }
            //    else
            //    {
            //        customerGoalPlanningBo.UpdateSIPGoalAllocationAmount(decimal.Parse(txt.Text), schemeplanId, goalId);
            //        BindMonthlySIPFundingScheme();
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
            //}
                

        }
        protected void RadGrid1_ItemInserted(object source, GridCommandEventArgs e)
        {
            decimal totalOtherAllocation=0;
            decimal currentAllocation = 0;
            GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickScheme");
            TextBox txt = (TextBox)e.Item.FindControl("TextBox3");
            int schemeplanId = int.Parse(ddl.SelectedValue);
            DataRow[] drExistingInvestmentSchemePlanId;
            DataRow[] drExistingInvestmentCurrentAllocation;
            DataRow[] drSchemePlanId;
             //expression ="SchemeCode="+schemeplanId.ToString();
             //dtCustomerGoalFundingDetails.DefaultView.RowFilter = expression;
            drSchemePlanId = dtCustomerGoalFunding.Select("SchemeCode=" + schemeplanId.ToString());

            drExistingInvestmentSchemePlanId = dsExistingInvestment.Tables[2].Select("PASP_SchemePlanCode=" + schemeplanId.ToString());

            drExistingInvestmentCurrentAllocation = dsExistingInvestment.Tables[3].Select("PASP_SchemePlanCode=" + schemeplanId.ToString());

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
                  totalOtherAllocation=totalOtherAllocation+  decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) - currentAllocation;
                }
            }
                      
            if (!string.IsNullOrEmpty(txt.Text))
            {
                if ((decimal.Parse(txt.Text) + totalOtherAllocation) > 100)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Allocation exceeding 100%');", true);
                }
                else
                {
                    decimal investedAmount = 0;
                    decimal acqCost = 0;
                    foreach (DataRow dr in dsExistingInvestment.Tables[6].Rows)
                    {
                        if (dr["PASP_SchemePlanCode"].ToString() == schemeplanId.ToString())
                        {
                            acqCost = decimal.Parse(dr["CMFNP_AcqCostExclDivReinvst"].ToString());
                            break;

                        }

                    }
                    investedAmount = (acqCost * decimal.Parse(txt.Text)) / 100;
                    customerGoalPlanningBo.UpdateGoalAllocationPercentage(decimal.Parse(txt.Text), schemeplanId, goalId, investedAmount);
                    BindExistingFundingScheme();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
            }
        }

        //protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        //{
            
        //}

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                //GridEditFormItem ed = (GridEditFormItem)e.Item;
                
            }
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
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
               
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txt = (TextBox)e.Item.FindControl("TextBox3");
               decimal allocationEntry = decimal.Parse(txt.Text);
               int schemePlanId = int.Parse(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SchemeCode"].ToString());
               decimal OtherGoalAllocation=decimal.Parse(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["OtherGoalAllocation"].ToString());
               InsertMFInvestmentAllocation(schemePlanId,OtherGoalAllocation,allocationEntry);
            }
          
        }


        protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                //GridEditFormItem ed = (GridEditFormItem)e.Item;

            }
          
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txt = (TextBox)e.Item.FindControl("TextBox3");
                TextBox txtotherSIPGoalAllocation = (TextBox)e.Item.FindControl("txtOtherSchemeAllocationPer");
                decimal OtherGoalAllocation = decimal.Parse(txtotherSIPGoalAllocation.Text);
                decimal allocationEntry = decimal.Parse(txt.Text);
                decimal totalSIPAmount = decimal.Parse(RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["TotalSIPamount"].ToString());
                int sipId = int.Parse(RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SIPId"].ToString());
                InsertMFSIPAllocation(sipId, OtherGoalAllocation, allocationEntry, totalSIPAmount);
            }

        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    RadGrid1.EditIndexes.Add(0);
            //    RadGrid1.Rebind();
            //}
        }
        protected void BindMonthlySIPFundingScheme()
        {


            DataTable dtCustomerGoalFundingSIPDetails = new DataTable();
            dtCustomerGoalFundingSIPDetails.Columns.Add("GoalId");
            dtCustomerGoalFundingSIPDetails.Columns.Add("SIPId");
            dtCustomerGoalFundingSIPDetails.Columns.Add("SchemeCode");
            dtCustomerGoalFundingSIPDetails.Columns.Add("SchemeName");
            dtCustomerGoalFundingSIPDetails.Columns.Add("SIPInvestedAmount");
            dtCustomerGoalFundingSIPDetails.Columns.Add("TotalSIPamount");
            dtCustomerGoalFundingSIPDetails.Columns.Add("OtherGoalAllocation");
            dtCustomerGoalFundingSIPDetails.Columns.Add("AvailableAllocation");
            DataRow drCustomerSIPGoalFundingDetails;

            
            DataRow[] drtotalSIPamount;
           
            
            dsSIPInvestment = customerGoalPlanningBo.GetSIPInvestmentDetails(customerVo.CustomerId, goalId);

          

            foreach (DataRow dr in dsSIPInvestment.Tables[0].Rows)
            {
                int sipId = int.Parse(dr["CMFSS_SystematicSetupId"].ToString());
                foreach (DataRow drTotalSIPInvestment in dsSIPInvestment.Tables[1].Rows)
                {
                    drtotalSIPamount = dsSIPInvestment.Tables[1].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
                    if (drtotalSIPamount.Count() > 0)
                    {
                        foreach (DataRow drtotalSIPAmount in drtotalSIPamount)
                        {
                            totalInvestedSIPamount = decimal.Parse(drtotalSIPAmount["TotalInvestedAmount"].ToString());
                        }
                    }

                    else
                        totalInvestedSIPamount = 0;
                }
                drCustomerSIPGoalFundingDetails = dtCustomerGoalFundingSIPDetails.NewRow();
                drCustomerSIPGoalFundingDetails["GoalId"] = dr["CG_GoalId"].ToString();
                drCustomerSIPGoalFundingDetails["SIPId"] = dr["CMFSS_SystematicSetupId"].ToString();
                drCustomerSIPGoalFundingDetails["SchemeCode"] = dr["PASP_SchemePlanCode"].ToString();
                drCustomerSIPGoalFundingDetails["SchemeName"] = dr["PASP_SchemePlanName"].ToString();
                drCustomerSIPGoalFundingDetails["SIPInvestedAmount"] = dr["InvestedAmount"].ToString();
                drCustomerSIPGoalFundingDetails["OtherGoalAllocation"] = (totalInvestedSIPamount - decimal.Parse(dr["InvestedAmount"].ToString())).ToString();
                drCustomerSIPGoalFundingDetails["AvailableAllocation"] = (decimal.Parse(dr["CMFSS_Amount"].ToString()) - totalInvestedSIPamount).ToString();
                drCustomerSIPGoalFundingDetails["TotalSIPamount"] = dr["CMFSS_Amount"].ToString();
                dtCustomerGoalFundingSIPDetails.Rows.Add(drCustomerSIPGoalFundingDetails);
            }
            dtCustomerSIPGoalFunding = dtCustomerGoalFundingSIPDetails;
            RadGrid2.DataSource = dtCustomerGoalFundingSIPDetails;
            RadGrid2.DataBind();
            //ShowGoalDetails(goalId);
            //DataTable dtCustomerGoalFundingSIPDetails = new DataTable();
            //dtCustomerGoalFundingSIPDetails.Columns.Add("GoalId");
            //dtCustomerGoalFundingSIPDetails.Columns.Add("SchemeCode");
            //dtCustomerGoalFundingSIPDetails.Columns.Add("SchemeName");
            //dtCustomerGoalFundingSIPDetails.Columns.Add("SIPAmount");
            //dtCustomerGoalFundingSIPDetails.Columns.Add("TotalSIPamount");
            //dtCustomerGoalFundingSIPDetails.Columns.Add("OtherGoalAllocation");
            //DataRow drCustomerSIPGoalFundingDetails;
            //int schemePlanCode = 0;
            //decimal totalSIPamount = 0;
            //decimal totalAllocatedAmount = 0;
            //DataRow[] drtotalSIPamount;
            //DataRow[] drtotalAllocatedAmount;
            //dtCustomerGoalFundingSIPDetails.Columns.Add("AvailableAllocation");
            //dsSIPInvestment = customerGoalPlanningBo.GetSIPInvestmentDetails(customerVo.CustomerId, goalId);
            //foreach (DataRow dr in dsSIPInvestment.Tables[0].Rows)
            //{
            //    schemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
            //    drtotalSIPamount = dsSIPInvestment.Tables[1].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
            //    if (drtotalSIPamount.Count() > 0)
            //    {
            //        foreach (DataRow drtotalSIPAmount in drtotalSIPamount)
            //        {
            //            totalSIPamount = decimal.Parse(drtotalSIPAmount["SIPAmount"].ToString());
            //        }
            //    }

            //    else
            //        totalSIPamount = 0;

            //    drtotalAllocatedAmount = dsSIPInvestment.Tables[2].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
            //    if (drtotalAllocatedAmount.Count() > 0)
            //    {
            //        foreach (DataRow drtotalAllocatedamount in drtotalAllocatedAmount)
            //        {
            //            totalAllocatedAmount = decimal.Parse(drtotalAllocatedamount["totalAllocation"].ToString());
            //        }
            //    }

            //    else
            //        totalAllocatedAmount = 0;


            //    drCustomerSIPGoalFundingDetails = dtCustomerGoalFundingSIPDetails.NewRow();
            //    drCustomerSIPGoalFundingDetails["GoalId"] = dr["CG_GoalId"].ToString();
            //    drCustomerSIPGoalFundingDetails["SchemeCode"] = dr["PASP_SchemePlanCode"].ToString();
            //    drCustomerSIPGoalFundingDetails["SchemeName"] = dr["PASP_SchemePlanName"].ToString();
            //    drCustomerSIPGoalFundingDetails["SIPAmount"] = dr["CEMFSSTGA_AllocationAmt"].ToString();
            //    drCustomerSIPGoalFundingDetails["OtherGoalAllocation"] = (totalAllocatedAmount - decimal.Parse(dr["CEMFSSTGA_AllocationAmt"].ToString())).ToString();
            //    drCustomerSIPGoalFundingDetails["AvailableAllocation"] = (totalSIPamount - totalAllocatedAmount).ToString();
            //    drCustomerSIPGoalFundingDetails["TotalSIPamount"] = totalSIPamount.ToString();
            //    dtCustomerGoalFundingSIPDetails.Rows.Add(drCustomerSIPGoalFundingDetails);
            //}
            //dtCustomerSIPGoalFunding = dtCustomerGoalFundingSIPDetails;
            //RadGrid2.DataSource = dtCustomerGoalFundingSIPDetails;
            //RadGrid2.DataBind();
        }
       protected void BindExistingFundingScheme()
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("SchemeName");
            //dt.Columns.Add("AllocationPercent");
            //dt.Columns.Add("InvestedAmount");
            //dt.Columns.Add("Units");
            //dt.Columns.Add("CurrentValue");
            //dt.Columns.Add("ReturnsXIRR");
            //dt.Columns.Add("ProjectedAmount");

            //DataRow dr = dt.NewRow();
            //dr["SchemeName"] = "Birla Sun Life";
            //dr["AllocationPercent"] = "50";
            //dr["InvestedAmount"] = "50000";
            //dr["Units"] = "60";
            //dr["CurrentValue"] = "100000";
            //dr["ReturnsXIRR"] = "12.78";
            //dr["ProjectedAmount"] = "1000000";

            //dt.Rows.Add(dr);
            //RadGrid1.DataSource = dt;
            //RadGrid1.DataBind();
            goalPlanningVo = customerGoalPlanningBo.GetGoalDetails(goalId);
           bool isHavingAssumption;
           customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);
            DataTable dtCustomerGoalFundingDetails = new DataTable();
            dtCustomerGoalFundingDetails.Columns.Add("GoalId");
            dtCustomerGoalFundingDetails.Columns.Add("SchemeCode");
            dtCustomerGoalFundingDetails.Columns.Add("SchemeName");
            dtCustomerGoalFundingDetails.Columns.Add("Category");
            dtCustomerGoalFundingDetails.Columns.Add("InvestedAmount");
            dtCustomerGoalFundingDetails.Columns.Add("Units");
            dtCustomerGoalFundingDetails.Columns.Add("CurrentValue");
            dtCustomerGoalFundingDetails.Columns.Add("AllocationEntry");
            dtCustomerGoalFundingDetails.Columns.Add("CurrentGoalAllocation");
            dtCustomerGoalFundingDetails.Columns.Add("OtherGoalAllocation");
            dtCustomerGoalFundingDetails.Columns.Add("ProjectedAmount");
            DataRow drCustomerGoalFundingDetails;
            dtCustomerGoalFundingDetails.Columns.Add("AvailableAllocation");
           
                dsExistingInvestment = customerGoalPlanningBo.GetExistingInvestmentDetails(customerVo.CustomerId, goalId);
           
            DataRow[] drExistingInvestmentGoalId=new DataRow[10];
            DataRow[] drExistingInvestmentSchemePlanId;
            DataRow[] drExistingInvestmentCurrentAllocation;
            DataRow[] drExistingInvestmentAssumptionValue;
            DataRow[] drExistingInvestmentReturnHybridCommodity;
            int schemePlanCode = 0;
           string assumptionId="";
           double assumptionValue = 0;
           double futureCost=0;
           double requiredAfter = 0;
           requiredAfter = goalPlanningVo.GoalYear- DateTime.Now.Year;
           double currentValue = 0;
            decimal currentAllocation = 0;
            double equityAllocation = 0;
            double debtAllocation = 0;
         
            //foreach(DataRow drGoalId in dsExistingInvestment.Tables[1].Rows)
            //{
            //drExistingInvestmentGoalId = dsExistingInvestment.Tables[1].Select("CG_GoalId=" + goalId.ToString());

            foreach (DataRow drGoalExistingInvestments in dsExistingInvestment.Tables[0].Rows)
            {
                schemePlanCode = int.Parse(drGoalExistingInvestments["PASP_SchemePlanCode"].ToString());
                drCustomerGoalFundingDetails = dtCustomerGoalFundingDetails.NewRow();
                drExistingInvestmentSchemePlanId = dsExistingInvestment.Tables[2].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
               
                drExistingInvestmentCurrentAllocation = dsExistingInvestment.Tables[3].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
                if(drGoalExistingInvestments["PAIC_AssetInstrumentCategoryCode"].ToString()=="Debt")
                {
                  assumptionValue= customerAssumptionVo.ReturnOnDebt/100;
                    
                }
                else if(drGoalExistingInvestments["PAIC_AssetInstrumentCategoryCode"].ToString()=="Equity")
                {
                    assumptionValue = customerAssumptionVo.ReturnOnEquity/100;
                }
                else 
                {
                    drExistingInvestmentReturnHybridCommodity = dsExistingInvestment.Tables[5].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
                    if (drExistingInvestmentReturnHybridCommodity.Count() > 0)
                    {
                        foreach (DataRow dr in drExistingInvestmentReturnHybridCommodity)
                        {
                            if (dr["WAC_AssetClassificationCode"].ToString() == "Equity")
                            {
                                equityAllocation = double.Parse(dr["WACPISSCA_PercentageAllocation"].ToString());
                            }
                            if (dr["WAC_AssetClassificationCode"].ToString() == "Debt")
                            {
                                debtAllocation = double.Parse(dr["WACPISSCA_PercentageAllocation"].ToString());
                            }
                           
                        }
                    }
                    assumptionValue = ((equityAllocation / 100) * customerAssumptionVo.ReturnOnEquity + (debtAllocation / 100) * customerAssumptionVo.ReturnOnDebt) / 100;
                
                }
                
                //if(assumptionId!="")
                //   drExistingInvestmentAssumptionValue=dsExistingInvestment.Tables[4].Select("WA_AssumptionId='"+assumptionId+"'");

                               
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
                        if (decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) <= 100)
                        {
                            drCustomerGoalFundingDetails["GoalId"] = goalId.ToString();
                            drCustomerGoalFundingDetails["SchemeName"] = drGoalExistingInvestments["PASP_SchemePlanName"].ToString();
                            drCustomerGoalFundingDetails["SchemeCode"] = drGoalExistingInvestments["PASP_SchemePlanCode"].ToString();

                            currentValue = (double.Parse((decimal.Parse(drGoalExistingInvestments["CMFNP_CurrentValue"].ToString()) * currentAllocation).ToString())) / 100;

                            drCustomerGoalFundingDetails["InvestedAmount"] = String.Format("{0:n2}", Math.Round(((Decimal.Parse(drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString()) * currentAllocation) / 100), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            //drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString();
                            drCustomerGoalFundingDetails["Units"] = String.Format("{0:n2}", Math.Round(Decimal.Parse(drGoalExistingInvestments["CMFNP_NetHoldings"].ToString()),0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drCustomerGoalFundingDetails["CurrentValue"] = String.Format("{0:n2}", Math.Round(((Decimal.Parse(drGoalExistingInvestments["CMFNP_CurrentValue"].ToString()) * currentAllocation) / 100),0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drCustomerGoalFundingDetails["AllocationEntry"] = drSchemeId["allocatedPercentage"].ToString();
                            drCustomerGoalFundingDetails["AvailableAllocation"] = 100 - decimal.Parse(drSchemeId["allocatedPercentage"].ToString());
                            drCustomerGoalFundingDetails["CurrentGoalAllocation"] = currentAllocation.ToString();
                            if (currentValue != 0)
                            {
                                futureCost = Math.Abs(customerGoalPlanningBo.FutureValue(assumptionValue, requiredAfter, 0, currentValue, 0));
                            }
                            else
                            {
                                futureCost = 0;
                            }
                                drCustomerGoalFundingDetails["OtherGoalAllocation"] = (decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) - currentAllocation).ToString();
                                drCustomerGoalFundingDetails["ProjectedAmount"] = String.Format("{0:n2}", Math.Round(futureCost, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drCustomerGoalFundingDetails["Category"] = drGoalExistingInvestments["PAIC_AssetInstrumentCategoryCode"].ToString();
                            dtCustomerGoalFundingDetails.Rows.Add(drCustomerGoalFundingDetails);
                        }

                    }
                }
                else
                {
                    drCustomerGoalFundingDetails["GoalId"] = goalId.ToString();
                    drCustomerGoalFundingDetails["SchemeName"] = drGoalExistingInvestments["PASP_SchemePlanName"].ToString();
                    drCustomerGoalFundingDetails["SchemeCode"] = drGoalExistingInvestments["PASP_SchemePlanCode"].ToString();
                    currentValue = (double.Parse((decimal.Parse(drGoalExistingInvestments["CMFNP_CurrentValue"].ToString()) * currentAllocation).ToString())) / 100;
                    drCustomerGoalFundingDetails["InvestedAmount"] = String.Format("{0:n2}", Math.Round(((Decimal.Parse(drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString()) * currentAllocation) / 100), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    //drGoalExistingInvestments["CMFNP_AcqCostExclDivReinvst"].ToString();
                    drCustomerGoalFundingDetails["Units"] = String.Format("{0:n2}", Math.Round(Decimal.Parse(drGoalExistingInvestments["CMFNP_NetHoldings"].ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drCustomerGoalFundingDetails["CurrentValue"] = String.Format("{0:n2}", Math.Round(((Decimal.Parse(drGoalExistingInvestments["CMFNP_CurrentValue"].ToString()) * currentAllocation) / 100), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drCustomerGoalFundingDetails["AllocationEntry"] = "0";
                    drCustomerGoalFundingDetails["AvailableAllocation"] = "100";
                    drCustomerGoalFundingDetails["CurrentGoalAllocation"] = currentAllocation.ToString();
                    drCustomerGoalFundingDetails["OtherGoalAllocation"] = "0";
                    if (currentValue != 0)
                    {
                        futureCost = Math.Abs(customerGoalPlanningBo.FutureValue(assumptionValue, requiredAfter, 0, currentValue, 0));
                    }
                    else
                    {
                        futureCost = 0;
                    }
                    drCustomerGoalFundingDetails["ProjectedAmount"] = String.Format("{0:n2}", Math.Round(futureCost, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drCustomerGoalFundingDetails["Category"] = drGoalExistingInvestments["PAIC_AssetInstrumentCategoryCode"].ToString();
                    dtCustomerGoalFundingDetails.Rows.Add(drCustomerGoalFundingDetails);

                }

            }
            dtCustomerGoalFunding = dtCustomerGoalFundingDetails;
            RadGrid1.DataSource = dtCustomerGoalFundingDetails;
            RadGrid1.DataBind();

           



        }


       protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
       {

           if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
           {
               GridEditFormItem gridEditFormItem = (GridEditFormItem)e.Item;
               DropDownList dropDownList = (DropDownList)gridEditFormItem.FindControl("ddlPickScheme");
               HtmlTableRow trSchemeDDL = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeDDL");
               HtmlTableRow trSchemeTextBox = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeTextBox");
               if (e.Item.RowIndex == -1)
               {
                 
                   
                   //TextBox txt = (TextBox)gridEditFormItem.FindControl("txtUnits");
                   //txt.Visible = false;
                   BindDDLSchemeAllocated(dropDownList);
                   trSchemeTextBox.Visible = false;
                   trSchemeDDL.Visible = true;
               }
               else
               {
                   trSchemeTextBox.Visible = true;
                   trSchemeDDL.Visible = false;
                  

               }
           }
          
       }

       protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
       {

           if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
           {

               GridEditFormItem gridEditFormItem = (GridEditFormItem)e.Item;
               DropDownList dropDownList = (DropDownList)gridEditFormItem.FindControl("ddlPickSIPScheme");
               HtmlTableRow trSchemeDDL = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeNameDDL");
               HtmlTableRow trSchemeTextBox = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeNameText");
               //TextBox txt = (TextBox)gridEditFormItem.FindControl("txtUnits");
               //txt.Visible = false;
               if (e.Item.RowIndex == -1)
               {
                   trSchemeDDL.Visible = true;
                   trSchemeTextBox.Visible = false;
                   BindDDLSIPSchemeAllocated(dropDownList);
               }
               else
               {
                   trSchemeDDL.Visible = false;
                   trSchemeTextBox.Visible = true;
               }
           }

       }

       //protected void ddlPickScheme_OnSelectedIndexChanged(object sender, GridItemEventArgs e)
       //{

       //}
       //protected void ddlPickScheme_OnSelectedIndexChanged(object sender, EventArgs e)
       //{
       //    //DropDownList dropdown = (DropDownList)sender;
       //    //string a = dropdown.SelectedValue;
       //}
       protected void RadGrid1_ItemUpdated(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                if (e.Item is GridEditFormItem)
                {
                    GridEditFormItem item = (GridEditFormItem)e.Item;
                    DropDownList ddl = (DropDownList)item.FindControl("ddlPickScheme");
                    BindDDLSchemeAllocated(ddl);

                    RadGrid1.Rebind();
                }
            }
        }

        protected void BindDDLSchemeAllocated(DropDownList ddl)
        {
            DataSet dsBindDDLSchemeAllocated = new DataSet();
            dsBindDDLSchemeAllocated = customerGoalPlanningBo.BindDDLSchemeAllocated(customerVo.CustomerId,goalId);
            ddl.DataSource = dsBindDDLSchemeAllocated.Tables[0];
            ddl.DataTextField = dsBindDDLSchemeAllocated.Tables[0].Columns["PASP_SchemePlanName"].ToString();
            ddl.DataValueField = dsBindDDLSchemeAllocated.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
            ddl.DataBind();


        }
        protected void BindDDLSIPSchemeAllocated(DropDownList ddl)
        {
            DataSet dsBindDDLSchemeAllocated = new DataSet();
            DataTable dtBindSIPDDLSchemeAlloted = new DataTable();
            dtBindSIPDDLSchemeAlloted.Columns.Add("CMFSS_SystematicSetupId");
            dtBindSIPDDLSchemeAlloted.Columns.Add("PASP_SchemePlanName");
            DataRow drBindDDLSIP;
           
            dsBindDDLSchemeAllocated = customerGoalPlanningBo.BindDDLSIPSchemeAllocated(customerVo.CustomerId, goalId);
            foreach (DataRow dr in dsBindDDLSchemeAllocated.Tables[0].Rows)
            {
                drBindDDLSIP = dtBindSIPDDLSchemeAlloted.NewRow();
                drBindDDLSIP["CMFSS_SystematicSetupId"] = dr["CMFSS_SystematicSetupId"].ToString();
                drBindDDLSIP["PASP_SchemePlanName"] = dr["PASP_SchemePlanName"].ToString() + "-" + dr["CMFSS_Amount"].ToString() + "-" + dr["CMFSS_SystematicDate"].ToString();
                dtBindSIPDDLSchemeAlloted.Rows.Add(drBindDDLSIP);
            }
            ddl.DataSource = dtBindSIPDDLSchemeAlloted;
            ddl.DataTextField = dtBindSIPDDLSchemeAlloted.Columns["PASP_SchemePlanName"].ToString();
            ddl.DataValueField = dtBindSIPDDLSchemeAlloted.Columns["CMFSS_SystematicSetupId"].ToString();
            ddl.DataBind();


        }

        protected void InsertMFSIPAllocation(int sipId, decimal otherAllocation, decimal allocationEntry,decimal totalAllocation)
        {
            if (allocationEntry + otherAllocation <= totalAllocation)
            {
                customerGoalPlanningBo.UpdateSIPGoalAllocationAmount(allocationEntry, sipId, goalId);
                BindMonthlySIPFundingScheme();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
            }
        }
        protected void InsertMFInvestmentAllocation(int schemeId,decimal otherAllocation,decimal allocationEntry)
        {
                               
           // decimal otherAllocation = decimal.Parse(gvExistInvestMapping.DataKeys[dr.RowIndex].Values["OtherGoalAllocation"].ToString());
            decimal investedAmount = 0;
            decimal acqCost = 0;
            foreach (DataRow dr in dsExistingInvestment.Tables[6].Rows)
            {
                if (dr["PASP_SchemePlanCode"].ToString() == schemeId.ToString())
                {
                    acqCost = decimal.Parse(dr["CMFNP_AcqCostExclDivReinvst"].ToString());
                    break;

                }

            }
            investedAmount=(acqCost * allocationEntry)/100;
            
            if (allocationEntry + otherAllocation <= 100)
            {
                customerGoalPlanningBo.UpdateGoalAllocationPercentage(allocationEntry, schemeId, goalId,investedAmount);
                BindExistingFundingScheme();
                ShowGoalDetails(goalId);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Allocation exceeding 100%');", true);
            }
         }

        protected void CalculateweightedReturn()
        {
            decimal equityAmount = 0;
            decimal debtAmount = 0;
            decimal hybridCommodityAmount = 0;
            decimal totalInvestedAmount = 0;
            double equityAllocation=0;
            double debtAllocation=0;
            int schemePlanCode=0;
             weightedReturn = 0;
            double hybridCommodityAllocation;
            DataRow[] drExistingInvestmentReturnHybridCommodity;
            if (dtCustomerGoalFunding.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCustomerGoalFunding.Rows)
                {
                    schemePlanCode = int.Parse(dr["SchemeCode"].ToString());
                    if (dr["Category"].ToString() == "Equity")
                    {
                        equityAmount = equityAmount + decimal.Parse(dr["InvestedAmount"].ToString());
                    }
                    else if (dr["Category"].ToString() == "Debt")
                    {
                        debtAmount = debtAmount + decimal.Parse(dr["InvestedAmount"].ToString());
                    }
                    else
                    {
                        hybridCommodityAmount = hybridCommodityAmount + decimal.Parse(dr["InvestedAmount"].ToString());
                        drExistingInvestmentReturnHybridCommodity = dsExistingInvestment.Tables[5].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
                        if (drExistingInvestmentReturnHybridCommodity.Count() > 0)
                        {
                            foreach (DataRow drReturnHybridCommodity in drExistingInvestmentReturnHybridCommodity)
                            {
                                if (drReturnHybridCommodity["WAC_AssetClassificationCode"].ToString() == "Equity")
                                {
                                    equityAllocation = double.Parse(drReturnHybridCommodity["WACPISSCA_PercentageAllocation"].ToString());
                                    equityAmount = equityAmount + (decimal.Parse(dr["InvestedAmount"].ToString()) * decimal.Parse(equityAllocation.ToString())) / 100;
                                    debtAmount = debtAmount + (decimal.Parse(dr["InvestedAmount"].ToString()) * (100 - decimal.Parse(equityAllocation.ToString()))) / 100;
                                }
                                if (drReturnHybridCommodity["WAC_AssetClassificationCode"].ToString() == "Debt")
                                {
                                    debtAllocation = double.Parse(drReturnHybridCommodity["WACPISSCA_PercentageAllocation"].ToString());
                                    debtAmount = debtAmount + (decimal.Parse(dr["InvestedAmount"].ToString()) * decimal.Parse(debtAllocation.ToString())) / 100;
                                    equityAmount = equityAmount + (decimal.Parse(dr["InvestedAmount"].ToString()) * (100 - decimal.Parse(debtAllocation.ToString()))) / 100;

                                }

                            }
                        }
                    }
                    totalInvestedAmount = equityAmount + debtAmount + hybridCommodityAmount;
                }
                weightedReturn = (equityAmount / totalInvestedAmount) * decimal.Parse(customerAssumptionVo.ReturnOnEquity.ToString()) + (debtAmount / totalInvestedAmount) * decimal.Parse(customerAssumptionVo.ReturnOnDebt.ToString());
            }
         }

        protected void btnSIPAdd_OnClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicEntry','login');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','?FromPage=CustomerFPGoalFundingProgress');", true);

        }
     }
 }