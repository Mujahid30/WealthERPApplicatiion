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
using BoResearch;
using System.Web.UI.HtmlControls;
namespace WealthERP.FP
{
    public partial class CustomerFPGoalFundingProgress : System.Web.UI.UserControl
    {
        CustomerGoalFundingProgressVo customerGoalFundingProgressVo = new CustomerGoalFundingProgressVo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
        ModelPortfolioBo modelportfolioBo = new ModelPortfolioBo();
        AdvisorVo advisorVo = new AdvisorVo();
        int goalId;
        CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsExistingInvestment = new DataSet();
        DataSet dsSIPInvestment = new DataSet();

        //DataTable dtCustomerGoalFunding = new DataTable();        
        //DataTable dtCustomerSIPGoalFunding = new DataTable();
        
        DataSet dsModelPortFolioSchemeDetails = new DataSet();
        decimal weightedReturn = 0;
        double totalInvestedSIPamount = 0;
        string goalCode = string.Empty;

        DataSet dsGoalFundingDetails = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();           
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            tblModelPortFolioDropDown.Visible = true;
            tblMessage.Visible = false;
            ErrorMessage.Visible = false;
           
         
            if (Request.QueryString["GoalId"] != null)
            {
               goalId = int.Parse(Request.QueryString["GoalId"].ToString());
               Session["GoalId"] = goalId;
            }
            if (Session["GoalId"] != null)
            {
                goalId = (int)Session["GoalId"];
            }    
                 
            
           
                GetGoalFundingProgress();
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                BindddlModelPortfolioGoalSchemes();
                SetGoalProgressImage(goalPlanningVo.Goalcode);
                
            
        }

        private void GetGoalFundingProgress()
        {
            customerGoalFundingProgressVo = customerGoalPlanningBo.GetGoalFundingProgressDetails(goalId, customerVo.CustomerId, advisorVo.advisorId, out dsGoalFundingDetails,out dsExistingInvestment,out dsSIPInvestment, out goalPlanningVo);
            
        }
        private void SetGoalProgressImage(string goalCode)
        {
            switch (goalCode)
            {
                case "BH":
                    {
                        imgGoalImage.ImageUrl = "~/Images/HomeGoal.png";
                        break;
                    }
                case "ED":
                    {
                        imgGoalImage.ImageUrl = "~/Images/EducationGoal.png";
                        break;
                    }
                case "MR":
                    {
                        imgGoalImage.ImageUrl = "~/Images/ChildMarraiageGoal.png";
                        break;
                    }
                case "OT":
                    {
                        imgGoalImage.ImageUrl = "~/Images/OtherGoal.png";
                        break;
                    }
                case "RT":
                    {
                        imgGoalImage.ImageUrl = "~/Images/RetirementGoal.png";
                        break;
                    }
            }

            if (Convert.ToDouble(txtProjectedGap.Text.Trim()) > 0)
            {

                imgGoalFundIndicator.ImageUrl = "~/Images/GoalUP.png"; 

            }
            else if (Convert.ToDouble(txtProjectedGap.Text.Trim()) < 0)
            {

                imgGoalFundIndicator.ImageUrl = "~/Images/GoalDown.png";

            }
            else
            {
                imgGoalFundIndicator.ImageUrl = "~/Images/NotApplicable.png";
 
            }

        }
        protected void ShowGoalDetails(CustomerGoalFundingProgressVo customerGoalFundingProgressVo, CustomerGoalPlanningVo goalPlanningVo)
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
                txtCostAtBeginning.Text = String.Format("{0:n2}", Math.Round(goalPlanningVo.CostOfGoalToday, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }

            if (customerGoalFundingProgressVo != null)
            {
                txtAmountInvested.Text = customerGoalFundingProgressVo.AmountInvestedTillDate != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.AmountInvestedTillDate, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtMonthlyContribution.Text = customerGoalFundingProgressVo.MonthlyContribution != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.MonthlyContribution, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtEstmdTimeToReachGoal.Text = customerGoalFundingProgressVo.GEstimatedTimeToAchiveGoal != "" ? customerGoalFundingProgressVo.GEstimatedTimeToAchiveGoal : "0";
                txtReturnsXIRR.Text = customerGoalFundingProgressVo.ReturnsXIRR != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.ReturnsXIRR, 2).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtValueOfCurrentGoal.Text = customerGoalFundingProgressVo.GoalCurrentValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.GoalCurrentValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtProjectedGap.Text = customerGoalFundingProgressVo.ProjectedGapValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.ProjectedGapValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtProjectedValueOnGoalDate.Text = customerGoalFundingProgressVo.ProjectedValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.ProjectedValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtAdditionalInvestmentsRequired.Text = customerGoalFundingProgressVo.AdditionalMonthlyRequirement != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.AdditionalMonthlyRequirement, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtAdditionalInvestments.Text = customerGoalFundingProgressVo.AdditionalYearlyRequirement != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.AdditionalYearlyRequirement, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                
            }

            
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
            drSIPId = dsGoalFundingDetails.Tables[1].Select("SIPId=" + sipId.ToString());
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
                    //BindMonthlySIPFundingScheme();
                    BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
            }
                
            
                

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
            drSchemePlanId = dsGoalFundingDetails.Tables[0].Select("SchemeCode=" + schemeplanId.ToString());

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
                    BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
            }
        }      
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
        protected void BindMonthlySIPFundingScheme(DataTable dtCustomerGoalFundingSIPDetails)
        { 
            RadGrid2.DataSource = dtCustomerGoalFundingSIPDetails;
            RadGrid2.DataBind();
            ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
           
        }

        //public double CalculateValuebasedOnFrequency(string frequencyCode, double assumptionValue)
        //{
        //    switch (frequencyCode)
        //    {
        //        case "AM":
        //            break;
        //        case "DA":
        //            assumptionValue = assumptionValue / 365;
        //            break;
        //        case "FN":
        //            assumptionValue = assumptionValue / 26;
        //            break;
        //        case "HY":
        //            assumptionValue = assumptionValue / 2; 
        //            break;
        //        case "MN":
        //            assumptionValue = assumptionValue / 12; 

        //            break;
        //        case "NA":

        //            break;
        //        case "QT":
        //            assumptionValue = assumptionValue / 4;
        //            break;
        //        case "WK":
        //            assumptionValue = assumptionValue / 52;
        //            break;
        //        case "YR":
        //            break;
        //    }

        //    return assumptionValue;
        //}

        //public double CalculateYearValuebasedOnFrequency(string frequencyCode, double year)
        //{
        //    switch (frequencyCode)
        //    {
        //        case "AM":
        //            break;
        //        case "DA":
        //            year = year * 365;
        //            break;
        //        case "FN":
        //            year = year * 26;
        //            break;
        //        case "HY":
        //            year = year * 2;
        //            break;
        //        case "MN":
        //            year = year * 12;

        //            break;
        //        case "NA":

        //            break;
        //        case "QT":
        //            year = year * 4;
        //            break;
        //        case "WK":
        //            year = year * 52;
        //            break;
        //        case "YR":
        //            break;
        //    }

        //    return year;

        //}
        protected void BindExistingFundingScheme(DataTable dtCustomerGoalFundingDetails)
        {

            RadGrid1.DataSource = dtCustomerGoalFundingDetails;
            RadGrid1.DataBind();

            ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);

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
           //if (e.Item is GridCommandItem)
           //{
           //    LinkButton refreshButton = e.Item.Controls[0].Controls[0].Controls[0].Controls[1].Controls[0] as LinkButton;
           //    refreshButton.Visible = false;
           //}
          
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
           //if (e.Item is GridCommandItem)
           //{
           //    LinkButton refreshButton = e.Item.Controls[0].Controls[0].Controls[0].Controls[1].Controls[0] as LinkButton;
           //    refreshButton.Visible = false;
           //}

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
               BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
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
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                //BindExistingFundingScheme();
                //ShowGoalDetails(goalId);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Allocation exceeding 100%');", true);
            }
         }

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                int schemeCode = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SchemeCode"].ToString());
                customerGoalPlanningBo.DeleteFundedScheme(schemeCode,goalId);
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Delete the Record. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }

        protected void RadGrid2_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                int sipId = Convert.ToInt32(RadGrid2.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SIPId"].ToString());
                customerGoalPlanningBo.DeleteSIPFundedScheme(sipId, goalId);
                BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Delete the Record. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }   


        //protected void CalculateweightedReturn()
        //{
        //    decimal equityAmount = 0;
        //    decimal debtAmount = 0;
        //    decimal hybridCommodityAmount = 0;
        //    decimal totalInvestedAmount = 0;
        //    double equityAllocation=0;
        //    double debtAllocation=0;
        //    int schemePlanCode=0;
        //     weightedReturn = 0;
        //     decimal investedAmount = 0;
        //    DataRow[] drExistingInvestmentReturnHybridCommodity;
        //    if (dtCustomerGoalFunding.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtCustomerGoalFunding.Rows)
        //        {
        //            if (dr["InvestedAmount"].ToString() != "")
        //            {
        //                investedAmount = decimal.Parse(dr["InvestedAmount"].ToString());
        //            }

        //            schemePlanCode = int.Parse(dr["SchemeCode"].ToString());
        //            if (dr["Category"].ToString() == "Equity")
        //            {
        //                equityAmount = equityAmount + investedAmount;
        //            }
        //            else if (dr["Category"].ToString() == "Debt")
        //            {
        //                debtAmount = debtAmount + investedAmount;
        //            }
        //            else
        //            {
        //                hybridCommodityAmount = hybridCommodityAmount + decimal.Parse(dr["InvestedAmount"].ToString());
        //                drExistingInvestmentReturnHybridCommodity = dsExistingInvestment.Tables[5].Select("PASP_SchemePlanCode=" + schemePlanCode.ToString());
        //                if (drExistingInvestmentReturnHybridCommodity.Count() > 0)
        //                {
        //                    foreach (DataRow drReturnHybridCommodity in drExistingInvestmentReturnHybridCommodity)
        //                    {
        //                        if (drReturnHybridCommodity["WAC_AssetClassificationCode"].ToString() == "Equity")
        //                        {
        //                            equityAllocation = double.Parse(drReturnHybridCommodity["WACPISSCA_PercentageAllocation"].ToString());
        //                            equityAmount = equityAmount + (investedAmount * decimal.Parse(equityAllocation.ToString())) / 100;
        //                            debtAmount = debtAmount + (decimal.Parse(dr["InvestedAmount"].ToString()) * (100 - decimal.Parse(equityAllocation.ToString()))) / 100;
        //                        }
        //                        if (drReturnHybridCommodity["WAC_AssetClassificationCode"].ToString() == "Debt")
        //                        {
        //                            debtAllocation = double.Parse(drReturnHybridCommodity["WACPISSCA_PercentageAllocation"].ToString());
        //                            debtAmount = debtAmount + (investedAmount * decimal.Parse(debtAllocation.ToString())) / 100;
        //                            equityAmount = equityAmount + (investedAmount * (100 - decimal.Parse(debtAllocation.ToString()))) / 100;

        //                        }

        //                    }
        //                }
        //            }
        //            totalInvestedAmount = equityAmount + debtAmount + hybridCommodityAmount;
        //        }
        //        if (totalInvestedAmount != 0)
        //        {
        //            weightedReturn = (equityAmount / totalInvestedAmount) * decimal.Parse(customerAssumptionVo.ReturnOnEquity.ToString()) + (debtAmount / totalInvestedAmount) * decimal.Parse(customerAssumptionVo.ReturnOnDebt.ToString());
        //        }
        //        else
        //        {
        //            weightedReturn = 0;
        //        }
        //    }
        // }

        protected void btnSIPAdd_OnClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicEntry','login');", true);
           // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','?FromPage=CustomerFPGoalFundingProgress');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioSystematic", "loadcontrol('PortfolioSystematicEntry','?GoalId=" + goalId + "');", true);
        }

        protected void BindddlModelPortfolioGoalSchemes()
        {
            int modelPortfolioId;
            dsModelPortFolioSchemeDetails = modelportfolioBo.GetGoalModelPortFolioAttachedSchemes(customerVo.CustomerId, advisorVo.advisorId, goalId);
            //if (dsModelPortFolioSchemeDetails.Tables[2].Rows.Count > 0)
            //{
            //    int modelportfolioCode = int.Parse(dsModelPortFolioSchemeDetails.Tables[2].Rows[0]["WFPCB_CalculationBasisId"].ToString());
            //    if (modelportfolioCode == 7)
            //    {

            if (dsModelPortFolioSchemeDetails.Tables[1].Rows.Count > 0)
            {                
                ddlModelPortFolio.DataSource = dsModelPortFolioSchemeDetails.Tables[1];
                ddlModelPortFolio.DataTextField = dsModelPortFolioSchemeDetails.Tables[1].Columns["XAMP_ModelPortfolioName"].ToString();
                ddlModelPortFolio.DataValueField = dsModelPortFolioSchemeDetails.Tables[1].Columns["XAMP_ModelPortfolioCode"].ToString();
                ddlModelPortFolio.DataBind();
                modelPortfolioId = int.Parse(dsModelPortFolioSchemeDetails.Tables[1].Rows[0]["XAMP_ModelPortfolioCode"].ToString());
                BindModelPortFolioSchemes(modelPortfolioId);

            }
            else
            {
                tblModelPortFolioDropDown.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";

            }

            //    }
            //}

        }

        protected void BindModelPortFolioSchemes(int modelPortfolioId)
        {
           DataTable dtModelPortFolioSchemeDetails = dsModelPortFolioSchemeDetails.Tables[0];
            string expression = "XAMP_ModelPortfolioCode=" + modelPortfolioId;
            dtModelPortFolioSchemeDetails.DefaultView.RowFilter = expression;
            RadGrid3.DataSource = dtModelPortFolioSchemeDetails.DefaultView;
            RadGrid3.DataBind();
        }

        protected void ddlModelPortFolio_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int modelportfoliocode = 0;
            modelportfoliocode = int.Parse(ddlModelPortFolio.SelectedValue);
            BindModelPortFolioSchemes(modelportfoliocode);
        }
        
     }
 }

