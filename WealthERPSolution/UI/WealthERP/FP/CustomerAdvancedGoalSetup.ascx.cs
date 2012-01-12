using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using WealthERP.Base;
using BoCommon;
using VoUser;
using BoCustomerGoalProfiling;
using DaoCustomerGoalProfiling;
using VoCustomerGoalProfiling;
using VoCustomerPortfolio;
using System.Collections;
using BoFPSuperlite;
using VoFPSuperlite;


namespace WealthERP.FP
{
    public partial class CustomerAdvancedGoalSetup : System.Web.UI.UserControl
    {

        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
        GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
        CustomerGoalSetupDao customerGoalSetupDao = new CustomerGoalSetupDao();       
        List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();
        CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
        CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo customerGoalPlanningVo = new CustomerGoalPlanningVo();

        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();

        bool isHavingAssumption = false;
        int goalId = 0;
        string goalAction = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            //rdoMFBasedGoalNo.Attributes.Add("onClick", "javascript:MFBasedGoalSelection(value);");
            //rdoMFBasedGoalYes.Attributes.Add("onClick", "javascript:MFBasedGoalSelection(value);");

            customerVo = (CustomerVo)Session["CustomerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                //AdvisorRMId = rmVo.RMId;
                //txtPickCustomer_autoCompleteExtender.ServiceMethod = "GetCustomerName";
            }

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["GoalId"] != null)
                    goalId = int.Parse(Request.QueryString["GoalId"].ToString());
                if (Request.QueryString["goalAction"] != null)
                    goalAction = Request.QueryString["goalAction"];

                if (goalId == 0 && string.IsNullOrEmpty(goalAction.Trim()))
                {
                    customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);

                    if (!isHavingAssumption)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please SetUp The Assumptions For This Customer');", true);

                    }

                    if (customerAssumptionVo.IsRiskProfileComplete == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please first complete Risk Profile');", true);
                    }
                    ControlShowHideBasedOnGoal(customerAssumptionVo, "OT");
                    //BindGoalObjTypeDropDown();
                    InitialPageLoadState();
                }
                else
                {
                    ViewState["ViewEditID"] = goalId;
                    ShowGoalDetails(customerVo.CustomerId, goalId);
                    if (goalAction == "View")
                    {
                        ControlSetVisiblity("View");
                        BtnSetVisiblity("View");                        
                    }
                    else if (goalAction == "Edit")
                    {
                        ControlSetVisiblity("Edit");
                        BtnSetVisiblity("Edit");
                    }
 
                }

            }


        }

        protected void ControlShowHideBasedOnGoal(CustomerAssumptionVo customerAssumptionVo, String goalCode)
        {
            if (goalCode == "RT")
            {
                trCustomerAge.Visible = true;
                trSpouseAge.Visible = true;
                trRetirementAge.Visible = true;
                trCustomerEOL.Visible = true;
                trSpouseEOL.Visible = true;
                trPostRetirementReturns.Visible = true;
                trReturnOnNewInvestments.Visible = true;
                trCorpusToBeLeftBehind.Visible = true;


                //trExistingInvestmentAllocated.Visible = false;
                //trReturnOnExistingInvestmentAll.Visible = false;
                trReturnOnFutureInvest.Visible = false;
                trROIFutureInvestment.Visible = false;

                txtCustomerAge.Text = customerAssumptionVo.CustomerAge.ToString();
                txtSpouseAge.Text = customerAssumptionVo.SpouseAge.ToString();
                txtRetirementAge.Text = customerAssumptionVo.RetirementAge.ToString();
                txtCustomerEOL.Text = customerAssumptionVo.CustomerEOL.ToString();
                txtSpouseEOL.Text = customerAssumptionVo.SpouseEOL.ToString();
                txtPostRetirementReturns.Text = customerAssumptionVo.PostRetirementReturn.ToString();
                txtReturnOnNewInvestments.Text = customerAssumptionVo.WeightedReturn.ToString();

            }
            else
            {
                trCustomerAge.Visible = false;
                trSpouseAge.Visible = false;
                trRetirementAge.Visible = false;
                trCustomerEOL.Visible = false;
                trSpouseEOL.Visible = false;
                trPostRetirementReturns.Visible = false;
                trReturnOnNewInvestments.Visible = false;
                trCorpusToBeLeftBehind.Visible = false;


                //trExistingInvestmentAllocated.Visible = true;
                //trReturnOnExistingInvestmentAll.Visible = true;
                trReturnOnFutureInvest.Visible = true;
                //trROIFutureInvestment.Visible = true;


            }

            txtExpRateOfReturn.Text = customerAssumptionVo.WeightedReturn.ToString();
            txtInflation.Text = customerAssumptionVo.InflationPercent.ToString();

            //if (customerAssumptionVo.IsGoalFundingFromInvestMapping == true)
            //{
            //    trExistingInvestmentAllocated.Visible = true;
            //    trReturnOnExistingInvestmentAll.Visible = true;

            //}
            //else
            //{
            //    trExistingInvestmentAllocated.Visible = false;
            //    trReturnOnExistingInvestmentAll.Visible = false;

            //}


        }
        
        //private void BindGoalObjTypeDropDown()
        //{

        //    DataSet ds = GoalSetupBo.GetCustomerGoalProfiling();
        //    ddlGoalType.DataSource = ds;
        //    ddlGoalType.DataValueField = ds.Tables[0].Columns["XG_GoalCode"].ToString();
        //    ddlGoalType.DataTextField = ds.Tables[0].Columns["XG_GoalName"].ToString();
        //    ddlGoalType.DataBind();
        //    ddlGoalType.Items.Insert(0, new ListItem("Select", "0"));
        //    ddlGoalType.SelectedIndex = 0;

        //}

        private void BindPickChildDropDown(int CustomerId)
        {
            DataSet ds = GoalSetupBo.GetCustomerAssociationDetails(CustomerId);
            ddlPickChild.DataSource = ds;
            ddlPickChild.DataValueField = ds.Tables[0].Columns["CA_AssociationId"].ToString();
            ddlPickChild.DataTextField = ds.Tables[0].Columns["ChildName"].ToString();
            ddlPickChild.DataBind();
            ddlPickChild.Items.Insert(0, new ListItem("Select a Child", "0"));
            ddlPickChild.SelectedIndex = 0;

        }

        protected void InitialPageLoadState()
        {

            //lblGoalDescription.Visible = false;
            //txtGoalDescription.Visible = false;
            //trGoalDesc.Visible = false;
            RequiredFieldValidator6.Visible = false;
            //ddlPickChild.Visible = false;
            //lblPickChild.Visible = false;
            trPickChild.Visible = false;
            btnBackToAddMode.Visible = false;

            txtGoalDate.Text = DateTime.Now.ToShortDateString();

            //TabContainer1_ActiveTabChanged(TabContainer1, null);
            ddlGoalType.Enabled = true;
            //chkApprove.Visible = true;
            trchkApprove.Visible = true;

            //lblApproveOn.Visible = false;
            trlblApproveOn.Visible = false;


            //lblROIFutureInvest.Visible = false;
            //txtROIFutureInvest.Visible = false;
            //SpanROIFutureInvest.Visible = false;
            //RangeValidator2.Visible = false;
            //RequiredFieldValidator6.Visible = false;
            trROIFutureInvestment.Visible = false;
            //txtInflation.Text = ViewState["InflationPer"].ToString();
            btnEdit.Visible = false;
            btnUpdate.Visible = false;
            btnBackToView.Visible = false;
        }

        private void ControlSetVisiblity(string action)
        {
            if (action == "View")
            {
                // For View Mode
                //txtPickCustomer.Enabled = false;
                ddlGoalType.Enabled = false;
                txtGoalDate.Enabled = false;
                ddlPickChild.Enabled = false;
                txtGoalCostToday.Enabled = false;
                ddlGoalYear.Enabled = false;
                txtCurrentInvestPurpose.Enabled = false;
                txtAboveRateOfInterst.Enabled = false;
                txtExpRateOfReturn.Enabled = false;
                txtExpRateOfReturn.Enabled = false;
                txtROIFutureInvest.Enabled = false;
                txtInflation.Enabled = false;
                txtComment.Enabled = false;
                txtGoalDescription.Enabled = false;
                rdoMFBasedGoalYes.Enabled = false;
                rdoMFBasedGoalNo.Enabled = false;
                //SpanPicCustomerReq.Visible = false;
                SpanGoalDateReq.Visible = false;
                SpanGoalCostTodayReq.Visible = false;
                SpanGoalYearReq.Visible = false;
                //SpanCurrInPurReq.Visible = false;
                //SpanAboveROIReq.Visible = false;
                SpanExpROI.Visible = false;
                SpanROIFutureInvest.Visible = false;
                spanGoalType.Visible = false;
                spnInflation.Visible = false;
                SpanCurrInvestmentAllocated.Visible = false;
                SpanReturnOnExistingInvestment.Visible = false;

                lblGoalbjective.Text = "Goal Type :";
                lblPickChild.Text = "Child Name :";



            }
            else
                if (action == "Add")
                {
                    //for Add Mode
                    //txtPickCustomer.Enabled = true;
                    ddlGoalType.Enabled = true;
                    txtGoalDate.Enabled = true;
                    ddlPickChild.Enabled = true;
                    txtGoalCostToday.Enabled = true;
                    txtGoalDescription.Enabled = true;
                    ddlGoalYear.Enabled = true;
                    txtCurrentInvestPurpose.Enabled = true;
                    txtAboveRateOfInterst.Enabled = true;
                    txtExpRateOfReturn.Enabled = true;
                    txtExpRateOfReturn.Enabled = true;
                    txtROIFutureInvest.Enabled = true;
                    txtInflation.Enabled = true;
                    txtComment.Enabled = true;
                    rdoMFBasedGoalYes.Enabled = true;
                    rdoMFBasedGoalNo.Enabled = true;


                    //SpanPicCustomerReq.Visible = true;
                    SpanGoalDateReq.Visible = true;
                    SpanGoalCostTodayReq.Visible = true;
                    SpanGoalYearReq.Visible = true;
                    //SpanCurrInPurReq.Visible = true;
                    //SpanAboveROIReq.Visible = true;
                    SpanExpROI.Visible = true;
                    SpanROIFutureInvest.Visible = true;
                    spanGoalType.Visible = true;
                    spnInflation.Visible = true;
                    SpanCurrInvestmentAllocated.Visible = true;
                    SpanReturnOnExistingInvestment.Visible = true;

                    //lblHeader.Visible = true;
                    lblNote.Visible = true;
                    //lblReqNote.Visible = true;
                    trRequiedNote.Visible = true;
                    //lblHeader.Text = "Goal Profile ";
                    //lblPickCustomer.Text = "Pick a Customer :";
                    lblGoalbjective.Text = "Pick Goal Objective :";
                    lblPickChild.Text = "Select a child for Goal planning :";
                    //lblGoalCostToday.Text = "Goal Cost Today :";

                }
                else if (action=="Edit")
                {
                    //For Edit mode
                    ddlGoalType.Enabled = false;
                    txtGoalDate.Enabled = true;
                    ddlPickChild.Enabled = false;
                    txtGoalCostToday.Enabled = true;
                    txtGoalDescription.Enabled = true;
                    ddlGoalYear.Enabled = true;
                    txtCurrentInvestPurpose.Enabled = true;
                    txtAboveRateOfInterst.Enabled = true;
                    txtExpRateOfReturn.Enabled = true;
                    txtExpRateOfReturn.Enabled = true;
                    txtROIFutureInvest.Enabled = true;
                    txtInflation.Enabled = true;
                    txtComment.Enabled = true;
                    rdoMFBasedGoalYes.Enabled = true;
                    rdoMFBasedGoalNo.Enabled = true;


                    //SpanPicCustomerReq.Visible = true;
                    SpanGoalDateReq.Visible = true;
                    SpanGoalCostTodayReq.Visible = true;
                    SpanGoalYearReq.Visible = true;
                    //SpanCurrInPurReq.Visible = true;
                    //SpanAboveROIReq.Visible = true;
                    SpanExpROI.Visible = true;
                    SpanROIFutureInvest.Visible = true;
                    spanGoalType.Visible = true;
                    spnInflation.Visible = true;
                    SpanCurrInvestmentAllocated.Visible = true;
                    SpanReturnOnExistingInvestment.Visible = true;

                    //lblHeader.Visible = true;
                    lblNote.Visible = true;
                    //lblReqNote.Visible = true;
                    trRequiedNote.Visible = true;
                    //lblHeader.Text = "Goal Profile ";
                    //lblPickCustomer.Text = "Pick a Customer :";
                    //lblGoalbjective.Text = "Pick Goal Objective :";
                    //lblPickChild.Text = "Select a child for Goal planning :";
                    //lblGoalCostToday.Text = "Goal Cost Today :";


                }
        }

        private void BtnSetVisiblity(string action)
        {
            if (action == "View")
            {   //for view selected
                btnCancel.Visible = false;
                btnSaveAdd.Visible = false;
                //btnNext.Visible = false;
                btnBackToAddMode.Visible = true;
                btnEdit.Visible = true;
                btnBackToView.Visible = false;
                btnUpdate.Visible = false;
            }
            else
                if (action == "Add")
                {  //for Add selected 
                    btnCancel.Visible = true;
                    btnSaveAdd.Visible = true;
                    //btnNext.Visible = true;
                    btnBackToAddMode.Visible = false;
                    //lblApproveOn.Visible = false;
                    trlblApproveOn.Visible = false;
                    //chkApprove.Visible = true;
                    trchkApprove.Visible = true;
                    btnBackToAddMode.Visible = false;
                    btnEdit.Visible = false;
                    btnUpdate.Visible = false;
                    btnBackToView.Visible = false;


                }
                else if (action == "Edit")
                {//For Edit Selected
                    btnBackToView.Visible = true;
                    btnBackToAddMode.Visible = true;
                    btnUpdate.Visible = true;

                    btnCancel.Visible = false;
                    btnSaveAdd.Visible = false;
                    //btnNext.Visible = false;
                    btnEdit.Visible = false;
                }


        }

        public void SetPageLoadState(string action)
        {
            if (action == "AddNew")
            {
                //lblHeader.Text = "Goal Profile";
                lblGoalbjective.Text = "Pick Goal Objective :";
                lblPickChild.Text = "Select a child for Goal planning :";
                lblGoalCostToday.Text = "Goal Cost Today :";
                ddlGoalType.SelectedIndex = 0;

                //trGoalDesc.Visible = false;
                txtGoalDate.Text = DateTime.Now.ToShortDateString();

                trPickChild.Visible = false;
                txtGoalCostToday.Text = "";
                ddlGoalYear.SelectedIndex = 0;
                txtCurrentInvestPurpose.Text = "0";
                txtAboveRateOfInterst.Text = "0";
                //txtInflation.Text = ViewState["InflationPer"].ToString();
                trROIFutureInvestment.Visible = false;
                txtComment.Text = "";
                txtGoalDescription.Text = string.Empty;
                //double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                //txtExpRateOfReturn.Text = ExpROI.ToString();

                chkApprove.Checked = false;
                trlblApproveOn.Visible = false;
                trchkApprove.Visible = true;
                //lblHeader.Visible = true;
                lblNote.Visible = true;
                trRequiedNote.Visible = true;



            }
            else if (action=="Cancel")
            {
                ddlGoalType.SelectedIndex = 0;

                //trGoalDesc.Visible = false;
                txtGoalDate.Text = DateTime.Now.ToShortDateString();

                trPickChild.Visible = false;
                txtGoalCostToday.Text = "";
                lblGoalCostToday.Text = "Goal Cost Today :";
                ddlGoalYear.SelectedIndex = 0;
                txtCurrentInvestPurpose.Text = "0";
                txtAboveRateOfInterst.Text = "0";
                txtInflation.Text = customerAssumptionVo.InflationPercent.ToString();
                trROIFutureInvestment.Visible = false;
                txtROIFutureInvest.Text = "";

                txtComment.Text = "";
                txtGoalDescription.Text = string.Empty;
                //double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                //txtExpRateOfReturn.Text = ExpROI.ToString();
                if (chkApprove.Checked == true)
                {
                    chkApprove.Checked = false;

                }
                if (chkApprove.Enabled == false)
                    chkApprove.Enabled = true;

            }


        }

        protected void ddlGoalType_OnSelectedIndexChange(object sender, EventArgs e)
        {
            
            customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);
            txtGoalDescription.Text = string.Empty;

            switch (ddlGoalType.SelectedValue.ToString())
            {
                case "BH":
                    lblGoalCostToday.Text = "Home Cost Today :";
                    lblGoalYear.Text = "Goal Year :";
                    //trGoalDesc.Visible = true;

                    //trGoalDesc.Visible = false;

                    trROIFutureInvestment.Visible = false;

                    trPickChild.Visible = false;

                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    txtAboveRateOfInterst.Text = "0";
                    //TabContainer1.ActiveTabIndex = 0;

                    break;
                case "ED":
                    lblGoalCostToday.Text = "Education Cost Today :";
                    lblGoalYear.Text = "Goal Year :";


                    trPickChild.Visible = true;


                    //trGoalDesc.Visible = false;


                    trROIFutureInvestment.Visible = false;

                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    txtAboveRateOfInterst.Text = "0";


                    BindPickChildDropDown(customerVo.CustomerId);
                    //TabContainer1.ActiveTabIndex = 0;
                    break;
                case "MR":
                    lblGoalCostToday.Text = "Mariage Cost Today:";
                    lblGoalYear.Text = "Goal Year :";


                    trPickChild.Visible = true;


                    //trGoalDesc.Visible = false;

                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    txtAboveRateOfInterst.Text = "0";


                    trROIFutureInvestment.Visible = false;

                    BindPickChildDropDown(customerVo.CustomerId);
                    //TabContainer1.ActiveTabIndex = 0;
                    break;
                case "RT":

                    trPickChild.Visible = false;


                    //trGoalDesc.Visible = false;

                    //trROIFutureInvestment.Visible = true;
                    //txtROIFutureInvest.Text = "7";
                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Monthly Requirment Today :";
                    //default  current investment and Rate of return of above to 0
                    //txtCurrentInvestPurpose.Text = "0";
                    //txtAboveRateOfInterst.Text = "0";


                    //TabContainer1.ActiveTabIndex = 0;
                    break;
                case "OT":

                    trPickChild.Visible = false;


                    //trGoalDesc.Visible = true;
                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    txtAboveRateOfInterst.Text = "0";
                    lblGoalCostToday.Text = "Goal Cost Today :";
                    lblGoalYear.Text = "Goal Year :";

                    trROIFutureInvestment.Visible = false;

                    //TabContainer1.ActiveTabIndex = 0;

                    break;
                default:
                    break;

            }
            ControlShowHideBasedOnGoal(customerAssumptionVo, ddlGoalType.SelectedValue.ToString());

        }

        protected void ShowGoalDetails(int CustomerId, int GoalId)
        {
            GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
            customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);
            goalProfileSetupVo = GoalSetupBo.GetCustomerGoal(CustomerId, GoalId);
            BtnSetVisiblity("View");
            lblNote.Visible = false;
            //lblReqNote.Visible = false;
            trRequiedNote.Visible = false;
            //lblHeader.Text = "Goal Details";
            if (goalProfileSetupVo.IsFundFromAsset == true)
            {
                trExistingInvestmentAllocated.Visible = false;
                trReturnOnExistingInvestmentAll.Visible = false;

            }
            else
            {
                trExistingInvestmentAllocated.Visible = true;
                trReturnOnExistingInvestmentAll.Visible = true;

            }

            switch (goalProfileSetupVo.Goalcode)
            {
                case "BH":
                    lblGoalCostToday.Text = "Home Cost Today :";
                    lblGoalYear.Text = "Goal Year :";

                    //lblGoalDescription.Visible = false;
                    //txtGoalDescription.Visible = false;
                    //trGoalDesc.Visible = true;
                    //lblROIFutureInvest.Visible = false;
                    //txtROIFutureInvest.Visible = false;
                    //RequiredFieldValidator6.Visible = false;
                    trROIFutureInvestment.Visible = false;

                    //lblPickChild.Visible = false;
                    //ddlPickChild.Visible = false;
                    trPickChild.Visible = false;
                    ddlGoalType.Text = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                    {
                        //lblApproveOn.Visible = true;
                        trlblApproveOn.Visible = true;
                        lblApproveOn.Text = "Customer Approved On " + goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        chkApprove.Checked = true;
                        //chkApprove.Visible = false;
                        trchkApprove.Visible = false;

                    }
                    else
                    {
                        //lblApproveOn.Visible = false;
                        trlblApproveOn.Visible = false;
                        //chkApprove.Visible = true;
                        trchkApprove.Visible = true;
                        chkApprove.Enabled = false;

                    }
                    break;
                case "ED":
                    lblGoalCostToday.Text = "Education Cost Today :";
                    lblGoalYear.Text = "Goal Year :";
                    //lblPickChild.Visible = true;
                    //ddlPickChild.Visible = true;
                    trPickChild.Visible = true;
                    //txtGoalDescription.Visible = false;
                    //lblGoalDescription.Visible = false;
                    //trGoalDesc.Visible = false;
                    //lblROIFutureInvest.Visible = false;
                    //txtROIFutureInvest.Visible = false;
                    //RequiredFieldValidator6.Visible = false;
                    trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    ddlPickChild.Enabled = false;
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                    {
                        //lblApproveOn.Visible = true;
                        trlblApproveOn.Visible = true;
                        lblApproveOn.Text = "Customer Approved On " + goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        chkApprove.Checked = true;
                        //chkApprove.Visible = false;
                        trchkApprove.Visible = false;

                    }
                    else
                    {
                        //lblApproveOn.Visible = false;
                        trlblApproveOn.Visible = false;
                        //chkApprove.Visible = true;
                        trchkApprove.Visible = true;
                        chkApprove.Enabled = false;

                    }
                    break;
                case "MR":
                    lblGoalCostToday.Text = "Mariage Cost Today :";
                    lblGoalYear.Text = "Goal Year :";
                    //lblPickChild.Visible = true;
                    //ddlPickChild.Visible = true;
                    trPickChild.Visible = true;
                    //txtGoalDescription.Visible = false;
                    //lblGoalDescription.Visible = false;
                    //trGoalDesc.Visible = false;
                    //lblROIFutureInvest.Visible = false;
                    //txtROIFutureInvest.Visible = false;
                    //RequiredFieldValidator6.Visible = false;
                    trROIFutureInvestment.Visible = false;
                    ddlGoalType.Text = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    //ListItem lstPickChild2 = new ListItem(goalProfileSetupVo.ChildName);
                    //ddlPickChild.Items.Add(lstPickChild2);
                    BindPickChildDropDown(CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                    {
                        //lblApproveOn.Visible = true;
                        trlblApproveOn.Visible = true;
                        lblApproveOn.Text = "Customer Approved On " + goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        chkApprove.Checked = true;
                        //chkApprove.Visible = false;
                        trchkApprove.Visible = false;

                    }
                    else
                    {
                        //lblApproveOn.Visible = false;
                        trlblApproveOn.Visible = false;
                        //chkApprove.Visible = true;
                        trchkApprove.Visible = true;
                        chkApprove.Enabled = false;

                    }
                    break;
                case "OT":
                    //ddlPickChild.Visible = false;
                    //lblPickChild.Visible = false;
                    trPickChild.Visible = false;
                    //txtGoalDescription.Visible = true;
                    //lblGoalDescription.Visible = true;
                    //trGoalDesc.Visible = true;
                    lblGoalCostToday.Text = "Goal Cost Today :";
                    lblGoalYear.Text = "Goal Year :";
                    //lblROIFutureInvest.Visible = false;
                    //txtROIFutureInvest.Visible = false;
                    //RequiredFieldValidator6.Visible = false;
                    trROIFutureInvestment.Visible = false;
                    ddlGoalType.Text = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;
                    txtGoalDescription.Text = goalProfileSetupVo.GoalDescription;
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                    {
                        //lblApproveOn.Visible = true;
                        trlblApproveOn.Visible = true;
                        lblApproveOn.Text = "Customer Approved On " + goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        chkApprove.Checked = true;
                        //chkApprove.Visible = false;
                        trchkApprove.Visible = false;

                    }
                    else
                    {
                        //lblApproveOn.Visible = false;
                        trlblApproveOn.Visible = false;
                        //chkApprove.Visible = true;
                        trchkApprove.Visible = true;
                        chkApprove.Enabled = false;

                    }
                    break;
                case "RT":
                    //ddlPickChild.Visible = false;
                    //lblPickChild.Visible = false;
                    trPickChild.Visible = false;
                    //txtGoalDescription.Visible = false;
                    //lblGoalDescription.Visible = false;
                    //trGoalDesc.Visible = false;
                    //lblROIFutureInvest.Visible = true;
                    //txtROIFutureInvest.Visible = true;
                    //RequiredFieldValidator6.Visible = true;
                    trROIFutureInvestment.Visible = true;

                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Annual Requirment Today :";
                    ddlGoalType.Text = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    txtROIFutureInvest.Text = goalProfileSetupVo.RateofInterestOnFture.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                    {
                        //lblApproveOn.Visible = true;
                        trlblApproveOn.Visible = true;
                        lblApproveOn.Text = "Customer Approved On " + goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        chkApprove.Checked = true;
                        //chkApprove.Visible = false;
                        trchkApprove.Visible = false;

                    }
                    else
                    {
                        //lblApproveOn.Visible = false;
                        trlblApproveOn.Visible = false;
                        //chkApprove.Visible = true;
                        trchkApprove.Visible = true;
                        chkApprove.Enabled = false;

                    }
                    break;
                default:
                    break;
            }
            ControlShowHideBasedOnGoal(customerAssumptionVo, goalProfileSetupVo.Goalcode.ToString());

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SetPageLoadState("Cancel");
            
        }

        protected void btnBackToAddMode_Click(object sender, EventArgs e)
        {
            //Tab2ControlVisibility(0);
            BtnSetVisiblity("Add");
            ControlSetVisiblity("Add");
            SetPageLoadState("AddNew");


        }

        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //SessionBo.CheckSession();
                //Customer id select from AutoComplite TextBox Values
                //rmVo = (RMVo)Session[SessionContents.RmVo];

                //int ParentCustomerId = int.Parse(Session["FP_UserID"].ToString());
                customerGoalPlanningVo.CustomerId = customerVo.CustomerId;
                customerGoalPlanningVo.Goalcode = ddlGoalType.SelectedValue.ToString();
                customerGoalPlanningVo.CostOfGoalToday = double.Parse(txtGoalCostToday.Text.Trim());
                customerGoalPlanningVo.GoalDate = DateTime.Parse(txtGoalDate.Text);
                customerGoalPlanningVo.GoalYear = int.Parse(ddlGoalYear.SelectedValue);
                if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
                {
                    customerGoalPlanningVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtGoalDescription.Text))
                {
                    customerGoalPlanningVo.GoalDescription = txtGoalDescription.Text;
                }

                if (rdoMFBasedGoalYes.Checked)
                {
                    customerGoalPlanningVo.IsFundFromAsset = true;
                }
                if (!string.IsNullOrEmpty(txtCurrentInvestPurpose.Text.Trim()))
                    customerGoalPlanningVo.CurrInvestementForGoal = double.Parse(txtCurrentInvestPurpose.Text.Trim());

                if (!string.IsNullOrEmpty(txtAboveRateOfInterst.Text.Trim()))
                    customerGoalPlanningVo.ROIEarned = double.Parse(txtAboveRateOfInterst.Text.Trim());
                customerGoalPlanningVo.ExpectedROI = double.Parse(txtExpRateOfReturn.Text);
                if (!string.IsNullOrEmpty(txtInflation.Text))
                {
                    customerGoalPlanningVo.InflationPercent = double.Parse(txtInflation.Text);
                }
                if (txtComment.Text != "")
                {
                    customerGoalPlanningVo.Comments = txtComment.Text.ToString();

                }
                customerGoalPlanningVo.CreatedBy = int.Parse(rmVo.RMId.ToString());
                if (chkApprove.Checked == true)
                    customerGoalPlanningVo.CustomerApprovedOn = DateTime.Parse(txtGoalDate.Text);

                

                if (ddlGoalType.SelectedValue == "RT")
                {
                    //customerAssumptionVo.c
                    //goalProfileSetupVo.RateofInterestOnFture = double.Parse(txtROIFutureInvest.Text);
                    //GoalSetupBo.CreateCustomerGoalProfileForRetirement(goalProfileSetupVo, customerAssumptionVo, ParentCustomerId, 0);
                    //customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, ParentCustomerId, false);
                    customerAssumptionVo.CustomerAge = Convert.ToUInt16(txtCustomerAge.Text);
                    customerAssumptionVo.SpouseAge = Convert.ToUInt16(txtSpouseAge.Text);
                    customerAssumptionVo.RetirementAge = Convert.ToUInt16(txtRetirementAge.Text);
                    customerAssumptionVo.CustomerEOL = Convert.ToUInt16(txtCustomerEOL.Text);
                    customerAssumptionVo.SpouseEOL = Convert.ToUInt16(txtSpouseEOL.Text);
                    customerAssumptionVo.PostRetirementReturn = Convert.ToDouble(txtPostRetirementReturns.Text);
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtReturnOnNewInvestments.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }


                customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, false);


                //Tab2ControlVisibility(0);

                //int gvRT=BindRTGoalOutputGridView();
                //if (gvRT == 1)
                //{
                //    gvRetirement.Visible = true;
                //    lblTotalText.Visible = true;
                //    lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                //}

                // this.BindGoalOutputGridView(1);

                //ShowOutPutTab();
                SetPageLoadState("AddNew");
                //TabContainer1.ActiveTabIndex = 0;

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:btnSaveAdd_Click()");
                object[] objects = new object[1];
                objects[0] = Session["FP_UserID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            try
            {
                //SessionBo.CheckSession();
                //Customer id select from AutoComplite TextBox Values
                //rmVo = (RMVo)Session[SessionContents.RmVo];

                //int ParentCustomerId = int.Parse(Session["FP_UserID"].ToString());
                customerGoalPlanningVo.CustomerId = customerVo.CustomerId;
                customerGoalPlanningVo.Goalcode = ddlGoalType.SelectedValue.ToString();
                customerGoalPlanningVo.CostOfGoalToday = double.Parse(txtGoalCostToday.Text.Trim());
                customerGoalPlanningVo.GoalDate = DateTime.Parse(txtGoalDate.Text);
                customerGoalPlanningVo.GoalYear = int.Parse(ddlGoalYear.SelectedValue);
                if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
                {
                    customerGoalPlanningVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtGoalDescription.Text))
                {
                    customerGoalPlanningVo.GoalDescription = txtGoalDescription.Text;
                }

                if (rdoMFBasedGoalYes.Checked)
                {
                    customerGoalPlanningVo.IsFundFromAsset = true;
                }
                if (!string.IsNullOrEmpty(txtCurrentInvestPurpose.Text.Trim()))
                    customerGoalPlanningVo.CurrInvestementForGoal = double.Parse(txtCurrentInvestPurpose.Text.Trim());

                if (!string.IsNullOrEmpty(txtAboveRateOfInterst.Text.Trim()))
                    customerGoalPlanningVo.ROIEarned = double.Parse(txtAboveRateOfInterst.Text.Trim());
                customerGoalPlanningVo.ExpectedROI = double.Parse(txtExpRateOfReturn.Text);
                if (!string.IsNullOrEmpty(txtInflation.Text))
                {
                    customerGoalPlanningVo.InflationPercent = double.Parse(txtInflation.Text);
                }
                if (txtComment.Text != "")
                {
                    customerGoalPlanningVo.Comments = txtComment.Text.ToString();

                }
                customerGoalPlanningVo.CreatedBy = int.Parse(rmVo.RMId.ToString());
                if (chkApprove.Checked == true)
                    customerGoalPlanningVo.CustomerApprovedOn = DateTime.Parse(txtGoalDate.Text);



                if (ddlGoalType.SelectedValue == "RT")
                {
                    //customerAssumptionVo.c
                    //goalProfileSetupVo.RateofInterestOnFture = double.Parse(txtROIFutureInvest.Text);
                    //GoalSetupBo.CreateCustomerGoalProfileForRetirement(goalProfileSetupVo, customerAssumptionVo, ParentCustomerId, 0);
                    //customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, ParentCustomerId, false);
                    customerAssumptionVo.CustomerAge = Convert.ToUInt16(txtCustomerAge.Text);
                    customerAssumptionVo.SpouseAge = Convert.ToUInt16(txtSpouseAge.Text);
                    customerAssumptionVo.RetirementAge = Convert.ToUInt16(txtRetirementAge.Text);
                    customerAssumptionVo.CustomerEOL = Convert.ToUInt16(txtCustomerEOL.Text);
                    customerAssumptionVo.SpouseEOL = Convert.ToUInt16(txtSpouseEOL.Text);
                    customerAssumptionVo.PostRetirementReturn = Convert.ToDouble(txtPostRetirementReturns.Text);
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtReturnOnNewInvestments.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }


                customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, false);


                //Tab2ControlVisibility(0);

                //int gvRT=BindRTGoalOutputGridView();
                //if (gvRT == 1)
                //{
                //    gvRetirement.Visible = true;
                //    lblTotalText.Visible = true;
                //    lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                //}

                // this.BindGoalOutputGridView(1);

                //ShowOutPutTab();
                //SetPageLoadState(1);
                //TabContainer1.ActiveTabIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FPGoalFunding", "loadcontrol('CustomerFPGoalPlanningDetails');", true);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:btnSaveAdd_Click()");
                object[] objects = new object[1];
                objects[0] = Session["FP_UserID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }




        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            BtnSetVisiblity("Edit");
            ControlSetVisiblity("Edit");
            ddlGoalType.Enabled = false;
            //lblApproveOn.Visible = false;
            trlblApproveOn.Visible = false;
            //if (chkApprove.Visible == false)
            //    chkApprove.Visible = true;
            if (trchkApprove.Visible == false)
                trchkApprove.Visible = true;

            if (chkApprove.Enabled == false)
                chkApprove.Enabled = true;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Customer id select from AutoComplite TextBox Values
                int GoalId = int.Parse(ViewState["ViewEditID"].ToString());

                int ParentCustomerId = int.Parse(Session["FP_UserID"].ToString());
                goalProfileSetupVo.CustomerId = ParentCustomerId;
                goalProfileSetupVo.Goalcode = ddlGoalType.SelectedValue.ToString();
                goalProfileSetupVo.CostOfGoalToday = double.Parse(txtGoalCostToday.Text);
                goalProfileSetupVo.GoalDate = DateTime.Parse(txtGoalDate.Text);
                goalProfileSetupVo.GoalYear = int.Parse(ddlGoalYear.SelectedValue);
                if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
                {
                    goalProfileSetupVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(txtGoalDescription.Text.ToString().Trim()))
                {
                    goalProfileSetupVo.GoalDescription = txtGoalDescription.Text.ToString();
                }
                if (!string.IsNullOrEmpty(txtCurrentInvestPurpose.Text.Trim()))
                {
                    goalProfileSetupVo.CurrInvestementForGoal = double.Parse(txtCurrentInvestPurpose.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtAboveRateOfInterst.Text.Trim()))
                    goalProfileSetupVo.ROIEarned = double.Parse(txtAboveRateOfInterst.Text.Trim());
                goalProfileSetupVo.ExpectedROI = double.Parse(txtExpRateOfReturn.Text);
                if (!string.IsNullOrEmpty(txtInflation.Text))
                {
                    goalProfileSetupVo.InflationPercent = double.Parse(txtInflation.Text);
                }
                if (txtComment.Text != "")
                {
                    goalProfileSetupVo.Comments = txtComment.Text.ToString();

                }
                goalProfileSetupVo.CreatedBy = int.Parse(rmVo.RMId.ToString());
                if (chkApprove.Checked == true)
                    goalProfileSetupVo.CustomerApprovedOn = DateTime.Parse(txtGoalDate.Text);



                if (ddlGoalType.SelectedValue == "RT")
                {
                    goalProfileSetupVo.RateofInterestOnFture = double.Parse(txtROIFutureInvest.Text);
                    GoalSetupBo.CreateCustomerGoalProfileForRetirement(goalProfileSetupVo, GoalId, 1);

                }
                else
                    GoalSetupBo.CreateCustomerGoalProfile(goalProfileSetupVo, GoalId, 1);


                //Tab2ControlVisibility(0);

                //int gvRT=BindRTGoalOutputGridView();
                //if (gvRT == 1)
                //{
                //    gvRetirement.Visible = true;
                //    lblTotalText.Visible = true;
                //    lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                //}

                // this.BindGoalOutputGridView(1);
                 ddlGoalType.Enabled = true;

                //ShowOutPutTab();
                SetPageLoadState("AddNew");
                BtnSetVisiblity("Add");
                //TabContainer1.ActiveTabIndex = 1;


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:btnUpdate_Click()");
                object[] objects = new object[2];
                objects[0] = Session["FP_UserID"];
                objects[1] = Session["GoalId"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }

        protected void btnBackToView_Click(object sender, EventArgs e)
        {
            if (ViewState["ViewEditID"].ToString() != "")
            {
                int GoalId = int.Parse(ViewState["ViewEditID"].ToString());
                ShowGoalDetails(customerVo.CustomerId, GoalId);
                ControlSetVisiblity("View");
                lblGoalbjective.Text = "Goal Objective :";
            }
        }

        protected void rdoMFBasedGoalNo_CheckedChanged(object sender, EventArgs e)
        {
            trExistingInvestmentAllocated.Visible = true;
            trReturnOnExistingInvestmentAll.Visible = true;
        }

        protected void rdoMFBasedGoalYes_CheckedChanged(object sender, EventArgs e)
        {
            trExistingInvestmentAllocated.Visible = false;
            trReturnOnExistingInvestmentAll.Visible = false;

        }

    }
}