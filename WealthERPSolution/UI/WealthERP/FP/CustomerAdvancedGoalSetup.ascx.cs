﻿using System;
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
using BoResearch;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

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

        CustomerGoalFundingProgressVo customerGoalFundingProgressVo = new CustomerGoalFundingProgressVo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        //CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
        ModelPortfolioBo modelportfolioBo = new ModelPortfolioBo();
        //AdvisorVo advisorVo = new AdvisorVo();
        //int goalId;
        //CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        //CustomerVo customerVo = new CustomerVo();
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
            tdlblInvestmntLumpsum.Visible = false;
            tdlblInvestmntLumpsumTxt.Visible = false;
            tdSavingsRequiredMonthly.Visible = false;
            tdSavingsRequiredMonthlyTxt.Visible = false;
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
                Session["GoalId"] = goalId;
                Session["GoalAction"] = goalAction;
                if (Session["GoalId"] != null)
                {
                    goalId = (int)Session["GoalId"];
                }  


                if (goalId == 0 && string.IsNullOrEmpty(goalAction.Trim()))
                {
                    customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);

                    if (!isHavingAssumption)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please SetUp The Assumptions For This Customer');", true);

                    }

                    if (customerAssumptionVo.IsRiskProfileComplete == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please be aware that riskprofile doesn't exist for the customer!!');", true);
                    }
                    ControlShowHideBasedOnGoal(customerAssumptionVo, "OT");
                    //BindGoalObjTypeDropDown();
                    InitialPageLoadState();
                }
                else
                {
                    ViewState["ViewEditID"] = goalId;
                    ShowGoalDetails();
                    if (goalAction == "View" || goalAction == "Fund")
                    {
                        ControlSetVisiblity("View");
                        BtnSetVisiblity("View");                        
                    }
                    else if (goalAction == "Edit")
                    {
                        ControlSetVisiblity("Edit");
                        BtnSetVisiblity("Edit");
                        if (goalProfileSetupVo.Goalcode == "RT")
                            ddlGoalYear.Enabled = false;
                        else
                            ddlGoalYear.Enabled = true;
                    }
 
                }
                if (goalId == 0 || goalProfileSetupVo.IsFundFromAsset==false)
                {
                    //RadPageView2.Visible = false;

                    
                    //ModelPortFolio
                    pnlModelPortfolio.Visible = false;
                    pnlModelPortfolioNoRecoredFound.Visible = true;

                    //GoalFunding and Progress
                    pnlFundingProgress.Visible = false;
                    pnlDocuments.Visible = false;
                    pnlMFFunding.Visible = false;
                    pnlNoRecordFoundGoalFundingProgress.Visible = true;

                    

                }
                else
                {
                    GetGoalFundingProgress();
                    BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                    BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                    ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                    BindddlModelPortfolioGoalSchemes();
                    SetGoalProgressImage(goalPlanningVo.Goalcode);
                }

                

            }
            if (Session["GoalId"] != null)
                goalId = (int)Session["GoalId"];

            if(Session["GoalAction"]!=null)
                goalAction=(string)Session["GoalAction"];

            //if(ViewState["ViewEditID"]!=null)
            //    goalId=Convert.ToInt32(ViewState["ViewEditID"].ToString());

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Current Tab Selection Retain            
            TabSelectionBasedOnGoalAction();


        }

        protected void TabSelectionBasedOnGoalAction()
        {
            if (goalAction == "View" || goalAction == "Edit" || string.IsNullOrEmpty(goalAction.Trim()))
            {
                RadTabStripFPGoalDetails.SelectedIndex = 0;
                RadTabStripFPGoalDetails.SelectedTab.Enabled = true;
                CustomerFPGoalDetail.SelectedIndex = 0;

            }
            else if (goalAction == "Fund")
            {
                //RadTabStripFPGoalDetails.TabIndex = 1;
                RadTabStripFPGoalDetails.SelectedIndex = 1;
                RadTabStripFPGoalDetails.SelectedTab.Enabled = true;
                CustomerFPGoalDetail.SelectedIndex = 1;

               //RadTabStripFPGoalDetails.SelectedTab = RadTabStripFPGoalDetails.Tabs[1];
            }
        }

//***********************GOAL FUNDING AND PROGRESS SECTION******************************//
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        #region Goal Add View Edit(First Tab)


        protected void ControlShowHideBasedOnGoal(CustomerAssumptionVo customerAssumptionVo, String goalCode)
        {
            if (goalCode == "RT")
            {
                tdCustomerAge1.Visible = true;
                tdCustomerAge2.Visible = true;

                tdSpouseAge1.Visible = true;
                tdSpouseAge2.Visible = true;

                tdRetirementAge1.Visible = true;
                tdRetirementAge2.Visible = true;

                tdCustomerEOL1.Visible = true;
                tdCustomerEOL2.Visible = true;

                tdSpouseEOL1.Visible = true;
                tdSpouseEOL2.Visible = true;

                tdPostRetirementReturns1.Visible = true;
                tdPostRetirementReturns2.Visible = true;

                tdPostRetirementInflation1.Visible = true;
                tdPostRetirementInflation2.Visible = true;

                //trReturnOnNewInvestments.Visible = true;
                if (customerAssumptionVo.IsCorpusToBeLeftBehind == true)
                    trCorpusToBeLeftBehind.Visible = true;
                else
                    trCorpusToBeLeftBehind.Visible = false;
                if (customerAssumptionVo.CustomerAge < customerAssumptionVo.RetirementAge)
                {
                    ddlGoalYear.SelectedValue = (DateTime.Now.Year + (customerAssumptionVo.RetirementAge - customerAssumptionVo.CustomerAge)).ToString();
                    ddlGoalYear.Enabled = false;
                }

               
                //*****************Blank Table Cell**********************
                //tdMFBasedBlank.Visible = true;
                //tdExistingInvestBlank.Visible = true;
                tdReturnOnExistingInvestBlank.Visible = true;
                //tdReturnOnFutureInvestBlank.Visible = true;
                //tdROIFutureInvestBlank.Visible = true;
                //tdReturnOnNewInvestBlank.Visible = true;
                tdCorpusToBeLeftBehindBlank.Visible = true;
                tdCommentBlank.Visible = true;


                //trExistingInvestmentAllocated.Visible = false;
                //trReturnOnExistingInvestmentAll.Visible = false;
                //trReturnOnFutureInvest.Visible = false;
                //trROIFutureInvestment.Visible = false;

                txtCustomerAge.Text = customerAssumptionVo.CustomerAge.ToString();
                txtSpouseAge.Text = customerAssumptionVo.SpouseAge.ToString();
                txtRetirementAge.Text = customerAssumptionVo.RetirementAge.ToString();
                txtCustomerEOL.Text = customerAssumptionVo.CustomerEOL.ToString();
                txtSpouseEOL.Text = customerAssumptionVo.SpouseEOL.ToString();
                txtPostRetirementReturns.Text = customerAssumptionVo.PostRetirementReturn.ToString();
                txtPostRetirementInflation.Text = customerAssumptionVo.PostRetirementInflation.ToString();
                //txtExpRateOfReturn.Text = customerAssumptionVo.WeightedReturn.ToString();

                txtCustomerAge.Enabled = false;
                txtSpouseAge.Enabled = false;
                txtRetirementAge.Enabled = false;
                txtCustomerEOL.Enabled = false;
                txtSpouseEOL.Enabled = false;
                txtPostRetirementReturns.Enabled = false;
                txtPostRetirementInflation.Enabled = false;

            }
            else
            {
                tdCustomerAge1.Visible = false;
                tdCustomerAge2.Visible = false;

                tdSpouseAge1.Visible = false;
                tdSpouseAge2.Visible = false;

                tdRetirementAge1.Visible = false;
                tdRetirementAge2.Visible = false;

                tdCustomerEOL1.Visible = false;
                tdCustomerEOL2.Visible = false;

                tdSpouseEOL1.Visible = false;
                tdSpouseEOL2.Visible = false;

                tdPostRetirementReturns1.Visible = false;
                tdPostRetirementReturns2.Visible = false;

                tdPostRetirementInflation1.Visible = false;
                tdPostRetirementInflation2.Visible = false;

                //trReturnOnNewInvestments.Visible = false;

                trCorpusToBeLeftBehind.Visible = false;

                //*****************Blank Table Cell**********************
                //tdMFBasedBlank.Visible = false;
                //tdExistingInvestBlank.Visible = false;
                tdReturnOnExistingInvestBlank.Visible = false;
                //tdReturnOnFutureInvestBlank.Visible = false;
                //tdROIFutureInvestBlank.Visible = false;
                //tdReturnOnNewInvestBlank.Visible = false;
                tdCorpusToBeLeftBehindBlank.Visible = false;
                tdCommentBlank.Visible = false;


                //trReturnOnNewInvestments.Visible = false;
                trCorpusToBeLeftBehind.Visible = false;
                ddlGoalYear.Enabled = true;
                ddlGoalYear.SelectedIndex = 0;

                //trExistingInvestmentAllocated.Visible = true;
                //trReturnOnExistingInvestmentAll.Visible = true;
                //trReturnOnFutureInvest.Visible = true;
                //trROIFutureInvestment.Visible = true;


            }

            //txtExpRateOfReturn.Text = customerAssumptionVo.WeightedReturn.ToString();
            //txtInflation.Text = customerAssumptionVo.InflationPercent.ToString();

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
              
        private void BindPickChildDropDown(int CustomerId)
        {
            DataSet ds = GoalSetupBo.GetCustomerAssociationDetails(CustomerId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPickChild.DataSource = ds;
                ddlPickChild.DataValueField = ds.Tables[0].Columns["CA_AssociationId"].ToString();
                ddlPickChild.DataTextField = ds.Tables[0].Columns["ChildName"].ToString();
                ddlPickChild.DataBind();
            }
            else
            {
                ddlPickChild.DataSource = null;
                ddlPickChild.DataBind();
            }
            ddlPickChild.Items.Insert(0, new ListItem("Select a Child", "0"));
            ddlPickChild.SelectedIndex = 0;

        }

        protected void InitialPageLoadState()
        {

            //lblGoalDescription.Visible = false;
            //txtGoalDescription.Visible = false;
            //trGoalDesc.Visible = false;
            //RequiredFieldValidator6.Visible = false;
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
            //trROIFutureInvestment.Visible = false;
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
                //txtROIFutureInvest.Enabled = false;
                txtCorpusToBeLeftBehind.Enabled = false;
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
                //SpanROIFutureInvest.Visible = false;
                spanGoalTypeGoalAdd.Visible = false;
                spnInflation.Visible = false;
                SpanCurrInvestmentAllocated.Visible = false;
                SpanReturnOnExistingInvestment.Visible = false;
                spnCorpsToBeLeftBehind.Visible = false;

                lblGoalbjective.Text = "Goal Type :";
                lblPickChild.Text = "Child Name :";

                lblNoteHeading.Visible = false;
                lblNote.Visible = false;
                trRequiedNote.Visible = false;


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
                    //txtROIFutureInvest.Enabled = true;
                    txtInflation.Enabled = true;
                    txtComment.Enabled = true;
                    rdoMFBasedGoalYes.Enabled = true;
                    rdoMFBasedGoalNo.Enabled = true;
                    txtCorpusToBeLeftBehind.Enabled = true;

                    //SpanPicCustomerReq.Visible = true;
                    SpanGoalDateReq.Visible = true;
                    SpanGoalCostTodayReq.Visible = true;
                    SpanGoalYearReq.Visible = true;
                    //SpanCurrInPurReq.Visible = true;
                    //SpanAboveROIReq.Visible = true;
                    SpanExpROI.Visible = true;
                    //SpanROIFutureInvest.Visible = true;
                    spanGoalTypeGoalAdd.Visible = true;
                    spnInflation.Visible = true;
                    SpanCurrInvestmentAllocated.Visible = true;
                    SpanReturnOnExistingInvestment.Visible = true;
                    spnCorpsToBeLeftBehind.Visible = true;

                    lblNoteHeading.Visible = true;
                    lblNote.Visible = true;
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
                    //txtROIFutureInvest.Enabled = true;
                    txtInflation.Enabled = true;
                    txtComment.Enabled = true;
                    rdoMFBasedGoalYes.Enabled = true;
                    rdoMFBasedGoalNo.Enabled = true;
                    txtCorpusToBeLeftBehind.Enabled = true;

                    //SpanPicCustomerReq.Visible = true;
                    SpanGoalDateReq.Visible = true;
                    SpanGoalCostTodayReq.Visible = true;
                    SpanGoalYearReq.Visible = true;
                    //SpanCurrInPurReq.Visible = true;
                    //SpanAboveROIReq.Visible = true;
                    SpanExpROI.Visible = true;
                    //SpanROIFutureInvest.Visible = true;
                    spanGoalTypeGoalAdd.Visible = true;
                    spnInflation.Visible = true;
                    SpanCurrInvestmentAllocated.Visible = true;
                    SpanReturnOnExistingInvestment.Visible = true;
                    spnCorpsToBeLeftBehind.Visible = true;

                    lblNoteHeading.Visible = true;
                    lblNote.Visible = true;
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
            if (trUpdateSuccess.Visible == true)
                trUpdateSuccess.Visible = false;
            if (trSumbitSuccess.Visible == true)
                trSumbitSuccess.Visible = false;

            txtCorpusToBeLeftBehind.Text = "0";
            txtExpRateOfReturn.Text = "0";

            
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
                //txtExpRateOfReturn.Text = "0";
                if (customerAssumptionVo != null)
                    txtInflation.Text = customerAssumptionVo.InflationPercent.ToString();
                else
                    txtInflation.Text = "4";
                //trROIFutureInvestment.Visible = false;
                txtComment.Text = "";
                txtGoalDescription.Text = string.Empty;
                //double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                //txtExpRateOfReturn.Text = ExpROI.ToString();

                chkApprove.Checked = false;
                trlblApproveOn.Visible = false;
                trchkApprove.Visible = true;

                lblNoteHeading.Visible = true;
                lblNote.Visible = true;
                trRequiedNote.Visible = true;

                rdoMFBasedGoalNo.Checked = true;
                trExistingInvestmentAllocated.Visible=true;
                trReturnOnExistingInvestmentAll.Visible = true;
                rdoMFBasedGoalYes.Checked = false;

                
                trCorpusToBeLeftBehind.Visible = false;

                tdCustomerAge1.Visible = false;
                tdCustomerAge2.Visible = false;

                tdSpouseAge1.Visible = false;
                tdSpouseAge2.Visible = false;

                tdRetirementAge1.Visible = false;
                tdRetirementAge2.Visible = false;

                tdCustomerEOL1.Visible = false;
                tdCustomerEOL2.Visible = false;

                tdSpouseEOL1.Visible = false;
                tdSpouseEOL2.Visible = false;

                tdPostRetirementReturns1.Visible = false;
                tdPostRetirementReturns2.Visible = false;

                tdPostRetirementInflation1.Visible = false;
                tdPostRetirementInflation2.Visible = false;

               
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
                //trROIFutureInvestment.Visible = false;
                //txtROIFutureInvest.Text = "";
                if (txtCorpusToBeLeftBehind.Visible==true)
                txtCorpusToBeLeftBehind.Text = "0";
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
            txtInflation.Text = customerAssumptionVo.InflationPercent.ToString();
            txtExpRateOfReturn.Text = customerAssumptionVo.WeightedReturn.ToString();
            txtAboveRateOfInterst.Text = customerAssumptionVo.WeightedReturn.ToString();

            switch (ddlGoalType.SelectedValue.ToString())
            {
                case "BH":
                    lblGoalCostToday.Text = "Home Cost Today :";
                    lblGoalYear.Text = "Goal Year :";
                    //trGoalDesc.Visible = true;

                    //trGoalDesc.Visible = false;

                    //trROIFutureInvestment.Visible = false;

                    trPickChild.Visible = false;

                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    //txtAboveRateOfInterst.Text = "0";
                    //TabContainer1.ActiveTabIndex = 0;

                    break;
                case "ED":
                    lblGoalCostToday.Text = "Education Cost Today :";
                    lblGoalYear.Text = "Goal Year :";


                    trPickChild.Visible = true;


                    //trGoalDesc.Visible = false;


                    //trROIFutureInvestment.Visible = false;

                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    //txtAboveRateOfInterst.Text = "0";


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
                    //txtAboveRateOfInterst.Text = "0";


                    //trROIFutureInvestment.Visible = false;

                    BindPickChildDropDown(customerVo.CustomerId);
                    //TabContainer1.ActiveTabIndex = 0;
                    break;
                case "RT":

                    trPickChild.Visible = false;


                    //trGoalDesc.Visible = false;

                    //trROIFutureInvestment.Visible = true;
                    //txtROIFutureInvest.Text = "7";
                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Monthly Requirement Today :";
                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    //txtAboveRateOfInterst.Text = "0";                    
                    //TabContainer1.ActiveTabIndex = 0;
                    if (customerAssumptionVo.CustomerAge == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Customer age required for retirement calculation, Update DOB first in customer profile');", true);
                        ddlGoalType.SelectedIndex = 0;
                        return;
                    }
                   
                    break;
                case "OT":

                    trPickChild.Visible = false;


                    //trGoalDesc.Visible = true;
                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    //txtAboveRateOfInterst.Text = "0";
                    lblGoalCostToday.Text = "Goal Cost Today :";
                    lblGoalYear.Text = "Goal Year :";

                    //trROIFutureInvestment.Visible = false;

                    //TabContainer1.ActiveTabIndex = 0;

                    break;
                default:
                    break;

            }
            ControlShowHideBasedOnGoal(customerAssumptionVo, ddlGoalType.SelectedValue.ToString());

        }

        protected void ShowGoalDetails()
        {
            //GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
            customerAssumptionVo = customerGoalPlanningBo.GetCustomerAssumptions(customerVo.CustomerId, advisorVo.advisorId, out isHavingAssumption);
            goalProfileSetupVo = GoalSetupBo.GetCustomerGoal(customerVo.CustomerId, goalId);
            //BtnSetVisiblity("View");
            lblNoteHeading.Visible = false;
            lblNote.Visible = false;            
            trRequiedNote.Visible = false;

            //lblHeader.Text = "Goal Details";
            if (goalProfileSetupVo.IsFundFromAsset == true)
            {
                rdoMFBasedGoalYes.Checked = true;
                rdoMFBasedGoalNo.Checked = false;
                trExistingInvestmentAllocated.Visible = false;
                trReturnOnExistingInvestmentAll.Visible = false;
                

            }
            else
            {
                rdoMFBasedGoalNo.Checked = true;
                rdoMFBasedGoalYes.Checked = false;
                trExistingInvestmentAllocated.Visible = true;
                trReturnOnExistingInvestmentAll.Visible = true;

            }

            txtGoalDescription.Text = goalProfileSetupVo.GoalDescription;          

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
                    //trROIFutureInvestment.Visible = false;

                    //lblPickChild.Visible = false;
                    //ddlPickChild.Visible = false;
                    trPickChild.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    //ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
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
                    //trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(customerVo.CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    ddlPickChild.Enabled = false;
                    //ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
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
                    //trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    //ListItem lstPickChild2 = new ListItem(goalProfileSetupVo.ChildName);
                    //ddlPickChild.Items.Add(lstPickChild2);
                    BindPickChildDropDown(customerVo.CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    //ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
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
                    //trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(customerVo.CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;                    
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    //ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
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
                    //trROIFutureInvestment.Visible = true;

                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Monthly Requirement Today :";
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    //ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
                    txtInflation.Text = goalProfileSetupVo.InflationPercent.ToString();
                    txtCurrentInvestPurpose.Text = goalProfileSetupVo.CurrInvestementForGoal.ToString();
                    txtAboveRateOfInterst.Text = goalProfileSetupVo.ROIEarned.ToString();
                    txtExpRateOfReturn.Text = goalProfileSetupVo.ExpectedROI.ToString();
                    //txtROIFutureInvest.Text = goalProfileSetupVo.RateofInterestOnFture.ToString();
                    txtComment.Text = goalProfileSetupVo.Comments;
                    if (goalProfileSetupVo.CorpsToBeLeftBehind != 0)
                        txtCorpusToBeLeftBehind.Text = goalProfileSetupVo.CorpsToBeLeftBehind.ToString();

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
            if (int.Parse(ddlGoalYear.Items[0].ToString()) <= goalProfileSetupVo.GoalYear)
                ddlGoalYear.SelectedValue = goalProfileSetupVo.GoalYear.ToString();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SetPageLoadState("Cancel");
            goalAction = "Edit";
            Session["GoalAction"] = goalAction;
            
        }

        protected void btnBackToAddMode_Click(object sender, EventArgs e)
        {
            //Tab2ControlVisibility(0);
            BtnSetVisiblity("Add");
            ControlSetVisiblity("Add");
            SetPageLoadState("AddNew");
            goalAction = "Add";
            Session["GoalAction"] = goalAction;
            
            //Add Goal State
            pnlModelPortfolio.Visible = false;
            pnlModelPortfolioNoRecoredFound.Visible = true;

            //GoalFunding and Progress
            pnlFundingProgress.Visible = false;
            pnlDocuments.Visible = false;
            pnlMFFunding.Visible = false;
            pnlNoRecordFoundGoalFundingProgress.Visible = true;

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

                if (!string.IsNullOrEmpty(txtExpRateOfReturn.Text.Trim()))
                {
                    customerGoalPlanningVo.ExpectedROI = double.Parse(txtExpRateOfReturn.Text);
                    customerAssumptionVo.ReturnOnNewInvestment = double.Parse(txtExpRateOfReturn.Text);
                }

                if (!string.IsNullOrEmpty(txtInflation.Text))
                {
                    customerGoalPlanningVo.InflationPercent = double.Parse(txtInflation.Text.Trim());
                    customerAssumptionVo.InflationPercent = double.Parse(txtInflation.Text.Trim());
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
                    customerAssumptionVo.PostRetirementInflation = Convert.ToDouble(txtPostRetirementInflation.Text.Trim());
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtExpRateOfReturn.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    if(!string.IsNullOrEmpty(txtCorpusToBeLeftBehind.Text.Trim()))
                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }
                double lumpsumInvestment = 0;
                CustomerGoalPlanningVo goalplanningSetUpVo = new CustomerGoalPlanningVo();
               goalplanningSetUpVo = customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, false,out goalId);
               tdlblInvestmntLumpsum.Visible = true;
               tdlblInvestmntLumpsumTxt.Visible=true;
               tdSavingsRequiredMonthly.Visible=true;
               tdSavingsRequiredMonthlyTxt.Visible = true;
               lumpsumInvestment = customerGoalPlanningBo.PV(goalplanningSetUpVo.ExpectedROI / 100, goalplanningSetUpVo.GoalYear - DateTime.Now.Year, 0, -goalplanningSetUpVo.FutureValueOfCostToday, 1);
               lblInvestmntLumpsumTxt.Text = lumpsumInvestment != 0 ? String.Format("{0:n2}", Math.Round(lumpsumInvestment, 2).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
          
               lblSavingsRequiredMonthlyTxt.Text = goalplanningSetUpVo.MonthlySavingsReq != 0 ? String.Format("{0:n2}", Math.Round(goalplanningSetUpVo.MonthlySavingsReq, 2).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
              
                //After Save Goal Is in View Mode
                Session["GoalId"] = goalId;
                ShowGoalDetails();
                ControlSetVisiblity("View");
                BtnSetVisiblity("View");
                trSumbitSuccess.Visible = true;
                //TabContainer1.ActiveTabIndex = 0;

                pnlModelPortfolio.Visible = true;
                pnlModelPortfolioNoRecoredFound.Visible = false;

                //GoalFunding and Progress
                pnlFundingProgress.Visible = true;
                pnlDocuments.Visible = true;
                pnlMFFunding.Visible = true;
                pnlNoRecordFoundGoalFundingProgress.Visible = false;
                //After Goal add Funding & Progress page data reflect
                if (customerGoalPlanningVo.IsFundFromAsset == true)
                {
                    GetGoalFundingProgress();
                    BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                    BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                    ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                    BindddlModelPortfolioGoalSchemes();
                    SetGoalProgressImage(goalPlanningVo.Goalcode);
                }
                else
                {
                    pnlModelPortfolio.Visible = false;
                    pnlModelPortfolioNoRecoredFound.Visible = true;

                    //GoalFunding and Progress
                    pnlFundingProgress.Visible = false;
                    pnlDocuments.Visible = false;
                    pnlMFFunding.Visible = false;
                    pnlNoRecordFoundGoalFundingProgress.Visible = true;
                }

                goalAction = "View";
                Session["GoalAction"] = goalAction;
             
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
                    customerAssumptionVo.PostRetirementInflation = Convert.ToDouble(txtPostRetirementInflation.Text.Trim());
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtExpRateOfReturn.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }


                customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, false,out goalId);


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
            if (trSumbitSuccess.Visible == true)
                trSumbitSuccess.Visible = false;
            if (trUpdateSuccess.Visible == true)
                trUpdateSuccess.Visible = false;
            

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

            if (ddlGoalType.SelectedValue == "RT")
                ddlGoalYear.Enabled = false;
            else
                ddlGoalYear.Enabled = true;
           
            goalAction = "Edit";
            Session["GoalAction"] = goalAction;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Customer id select from AutoComplite TextBox Values
                //int GoalId = (int)Session["GoalId"];

                customerGoalPlanningVo.GoalId = goalId;
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
                customerGoalPlanningVo.ExpectedROI = double.Parse(txtExpRateOfReturn.Text.Trim());
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
                    customerAssumptionVo.PostRetirementInflation = Convert.ToDouble(txtPostRetirementInflation.Text.Trim());
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtExpRateOfReturn.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    if (!string.IsNullOrEmpty(txtCorpusToBeLeftBehind.Text.Trim()))
                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }


                customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, true, out goalId);

                //After Save Goal Is in View Mode
                Session["GoalId"] = goalId;
                ShowGoalDetails();
                ControlSetVisiblity("View");
                BtnSetVisiblity("View");
                trUpdateSuccess.Visible = true;

               if (customerGoalPlanningVo.IsFundFromAsset == true)
               {
                    GetGoalFundingProgress();
                    BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                    BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                    ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                    BindddlModelPortfolioGoalSchemes();
                    SetGoalProgressImage(goalPlanningVo.Goalcode);
                    pnlModelPortfolio.Visible = true;
                    pnlFundingProgress.Visible = true;
                    pnlDocuments.Visible = true;
                    pnlMFFunding.Visible = true;
                    //pnlModelPortfolioNoRecoredFound.Visible = false;
                    pnlNoRecordFoundGoalFundingProgress.Visible = false;
               }else
               {
                    pnlModelPortfolio.Visible = false;
                    pnlModelPortfolioNoRecoredFound.Visible = true;
                    //GoalFunding and Progress
                    pnlFundingProgress.Visible = false;
                    pnlDocuments.Visible = false;
                    pnlMFFunding.Visible = false;
                    pnlNoRecordFoundGoalFundingProgress.Visible = true;

               }
                goalAction = "View";
                Session["GoalAction"] = goalAction;

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
                ShowGoalDetails();
                ControlSetVisiblity("View");
                BtnSetVisiblity("View");
                lblGoalbjective.Text = "Goal Objective :";
            }
            goalAction = "View";
            Session["GoalAction"] = goalAction;
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

        #endregion


//***********************GOAL FUNDING AND PROGRESS SECTION******************************//
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//



        #region Goal Funding Progress(Second Tab)



        private void GetGoalFundingProgress()
        {
            customerGoalFundingProgressVo = customerGoalPlanningBo.GetGoalFundingProgressDetails(goalId, customerVo.CustomerId, advisorVo.advisorId, out dsGoalFundingDetails, out dsExistingInvestment, out dsSIPInvestment, out goalPlanningVo);
            if (Cache["GoalFundingDetailsdsExistingInvestment" + customerVo.CustomerId.ToString()] != null)
            {
                Cache.Remove("GoalFundingDetailsdsExistingInvestment" + customerVo.CustomerId.ToString());
            }
            Cache.Insert("GoalFundingDetailsdsExistingInvestment" + customerVo.CustomerId, dsExistingInvestment, null, DateTime.Now.AddMinutes(4 * 60), TimeSpan.Zero);

            if (Cache["GoalFundingDetailsdsSIPInvestment" + customerVo.CustomerId.ToString()] != null)
            {
                Cache.Remove("GoalFundingDetailsdsSIPInvestment" + customerVo.CustomerId.ToString());
            }
            Cache.Insert("GoalFundingDetailsdsSIPInvestment" + customerVo.CustomerId, dsSIPInvestment, null, DateTime.Now.AddMinutes(4 * 60), TimeSpan.Zero);
        
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
                txtReturnsXIRR.Text = customerGoalFundingProgressVo.ReturnsXIRR != 0 ? Math.Round(customerGoalFundingProgressVo.ReturnsXIRR, 2).ToString() : "0";
                txtValueOfCurrentGoal.Text = customerGoalFundingProgressVo.GoalCurrentValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.GoalCurrentValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                
                txtProjectedGap.Text = customerGoalFundingProgressVo.ProjectedGapValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.ProjectedGapValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                if (double.Parse(txtProjectedGap.Text) < 0)
                {
                    txtProjectedGap.ForeColor = System.Drawing.Color.Red;
                    txtEstmdTimeToReachGoal.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtProjectedGap.ForeColor = System.Drawing.Color.Green;
                    txtEstmdTimeToReachGoal.ForeColor = System.Drawing.Color.Green;
                }
                txtProjectedValueOnGoalDate.Text = customerGoalFundingProgressVo.ProjectedValue != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.ProjectedValue, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtAdditionalInvestmentsRequired.Text = customerGoalFundingProgressVo.AdditionalMonthlyRequirement != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.AdditionalMonthlyRequirement, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                txtAdditionalInvestments.Text = customerGoalFundingProgressVo.AdditionalYearlyRequirement != 0 ? String.Format("{0:n2}", Math.Round(customerGoalFundingProgressVo.AdditionalYearlyRequirement, 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                if (customerGoalFundingProgressVo.ProjectedEndYear < goalPlanningVo.GoalProfileDate.Year)
                {
                    customerGoalFundingProgressVo.ProjectedEndYear = goalPlanningVo.GoalProfileDate.Year;
                }                
                txtProjectedCompleteYear.Text = customerGoalFundingProgressVo.ProjectedEndYear.ToString();
             
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
            if (!string.IsNullOrEmpty(ddl.SelectedValue))
            {
                int sipId = int.Parse(ddl.SelectedValue);

                DataRow[] drSIPId;
                DataRow[] drSIPInvestmentPlanId;
                DataRow[] drSIPCurrentInvestment;
                DataRow[] drTotalSIPamount;
                drSIPId = dsGoalFundingDetails.Tables[1].Select("SIPId=" + sipId.ToString());
                drSIPCurrentInvestment = dsSIPInvestment.Tables[0].Select("CMFSS_SystematicSetupId=" + sipId.ToString());

                drTotalSIPamount = dsSIPInvestment.Tables[2].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
                drSIPInvestmentPlanId = dsSIPInvestment.Tables[1].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
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

                if (drSIPInvestmentPlanId.Count() > 0)
                {
                    foreach (DataRow drSipInvestmentPlan in drSIPInvestmentPlanId)
                    {
                        totalOtherAllocation = totalOtherAllocation + decimal.Parse(drSipInvestmentPlan["TotalInvestedAmount"].ToString()) - currentAllocation;
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
                        GetGoalFundingProgress();
                        BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                        ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
                }



            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have not selected any scheme');", true);

            }
        }

        protected void RadGrid1_ItemInserted(object source, GridCommandEventArgs e)
        {
            decimal totalOtherAllocation = 0;
            decimal currentAllocation = 0;
            GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickScheme");
            TextBox txt = (TextBox)e.Item.FindControl("TextBox4");
            if (ddl.SelectedValue != "" && ddl.SelectedValue != "Select")
            {
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
                        totalOtherAllocation = totalOtherAllocation + decimal.Parse(drSchemeId["allocatedPercentage"].ToString()) - currentAllocation;
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
                        GetGoalFundingProgress();
                        BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                        ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill the allocation');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have not selected any scheme');", true);
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
                GetGoalFundingProgress();
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txt = (TextBox)e.Item.FindControl("TextBox4");
                decimal allocationEntry = decimal.Parse(txt.Text);
                int schemePlanId = int.Parse(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SchemeCode"].ToString());
                decimal OtherGoalAllocation = decimal.Parse(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["OtherGoalAllocation"].ToString());
                InsertMFInvestmentAllocation(schemePlanId, OtherGoalAllocation, allocationEntry);
            }
            //if (e.CommandName == RadGrid.EditCommandName || e.CommandName == RadGrid.CancelCommandName || e.CommandName == RadGrid.UpdateCommandName)
            //{
            if (e.CommandName != RadGrid.UpdateCommandName)
            {
                GetGoalFundingProgress();
            }
            BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
            //BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
            //ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
            //BindddlModelPortfolioGoalSchemes();
            //SetGoalProgressImage(goalPlanningVo.Goalcode);
            //}
            if (e.CommandName == RadGrid.UpdateCommandName || e.CommandName == RadGrid.InitInsertCommandName)
            {
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                SetGoalProgressImage(goalPlanningVo.Goalcode);
            }

            goalAction = "Fund";
            Session["GoalAction"] = goalAction;
                

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

            GetGoalFundingProgress();
            //BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
            BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
            if (e.CommandName == RadGrid.UpdateCommandName || e.CommandName == RadGrid.DeleteCommandName || e.CommandName == RadGrid.InitInsertCommandName)
            {
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                SetGoalProgressImage(goalPlanningVo.Goalcode);
            }

            goalAction = "Fund";
            Session["GoalAction"] = goalAction;

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
            //ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);

        }

        protected void BindExistingFundingScheme(DataTable dtCustomerGoalFundingDetails)
        {

            RadGrid1.DataSource = dtCustomerGoalFundingDetails;
            RadGrid1.DataBind();

            //ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlPickScheme");
                HtmlTableRow trSchemeDDL = (HtmlTableRow)editedItem.FindControl("trSchemeDDL");
                HtmlTableRow trSchemeTextBox = (HtmlTableRow)editedItem.FindControl("trSchemeTextBox");
                Label txtAvailableAllocationEditMode = editedItem.FindControl("txtAvailableAllocationEditMode") as Label;
                Label txtAvailableAllocationAddMode = editedItem.FindControl("txtAvailableAllocationAddMode") as Label;
                Label txtSchemeAllocationPerEditMode = editedItem.FindControl("txtSchemeAllocationPerEditMode") as Label;
                Label txtSchemeAllocationPerAddMode = editedItem.FindControl("txtSchemeAllocationPerAddMode") as Label;
                Label txtInvestedAmtAdd = editedItem.FindControl("txtInvestedAmtAdd") as Label;
                Label txtInvestedAmt = editedItem.FindControl("txtInvestedAmt") as Label;
                Label txtAllocationEntryAddMode = editedItem.FindControl("txtAllocationEntryAddMode") as Label;
                Label TextBox1 = editedItem.FindControl("TextBox1") as Label;
                Label txtCurrentValueEditMode = editedItem.FindControl("txtCurrentValueEditMode") as Label;
                Label txtCurrentValueAddMode = editedItem.FindControl("txtCurrentValueAddMode") as Label;
                Label txtAmtAvailableEditMode = editedItem.FindControl("txtAmtAvailableEditMode") as Label;
                Label txtAmtAvailableAddMode = editedItem.FindControl("txtAmtAvailableAddMode") as Label;
                Label txtUnitsAddMode = editedItem.FindControl("txtUnitsAddMode") as Label;
                Label txtUnits = editedItem.FindControl("txtUnits") as Label;
                Label lblMemberNameAddMode = editedItem.FindControl("lblMemberNameAddMode") as Label;
                DropDownList ddlFamilyMembers = (DropDownList)editedItem.FindControl("ddlMemberName");
                HtmlTableRow trUnits = editedItem.FindControl("trUnits") as HtmlTableRow;
                HtmlTableRow trCurrentValue = editedItem.FindControl("trCurrentValue") as HtmlTableRow;
                HtmlTableRow trTotalGoalAllocation = editedItem.FindControl("trTotalGoalAllocation") as HtmlTableRow;
                HtmlTableRow trOtherGoalAllocation = editedItem.FindControl("trOtherGoalAllocation") as HtmlTableRow;

                HtmlTableCell tdlblSchemeName = editedItem.FindControl("tdlblSchemeName") as HtmlTableCell;
                HtmlTableCell tdddlPickScheme = editedItem.FindControl("tdddlPickScheme") as HtmlTableCell;

                
                trUnits.Visible = false;
                trCurrentValue.Visible = false;
                trTotalGoalAllocation.Visible = false;
                trOtherGoalAllocation.Visible = false;
                tdddlPickScheme.Visible = false;
                tdlblSchemeName.Visible = false;

                if (e.Item.RowIndex == -1)
                {
                    //TextBox txt = (TextBox)gridEditFormItem.FindControl("txtUnits");
                    //txt.Visible = false;
                    BindFamilyMembers(ddlFamilyMembers, customerVo.CustomerId);
                    // BindDDLSchemeAllocated(dropDownList);
                    trSchemeTextBox.Visible = false;
                    trSchemeDDL.Visible = true;
                    txtAvailableAllocationEditMode.Visible = false;
                    txtSchemeAllocationPerEditMode.Visible = false;
                    txtInvestedAmt.Visible = false;
                    TextBox1.Visible = false;
                    txtCurrentValueEditMode.Visible = false;
                    txtAmtAvailableEditMode.Visible = false;
                    txtUnits.Visible = false;
                    txtCurrentValueAddMode.Visible = true;
                    txtAvailableAllocationAddMode.Visible = true;
                    txtSchemeAllocationPerAddMode.Visible = true;
                    txtInvestedAmtAdd.Visible = true;
                    txtAllocationEntryAddMode.Visible = true;
                    txtAmtAvailableAddMode.Visible = true;
                    txtUnitsAddMode.Visible = true;

                }
                else
                {
                    trUnits.Visible = true;
                    trCurrentValue.Visible = true;
                    trTotalGoalAllocation.Visible = true;
                    trOtherGoalAllocation.Visible = true;

                    trSchemeTextBox.Visible = true;
                    trSchemeDDL.Visible = false;
                    txtAvailableAllocationEditMode.Visible = true;
                    txtSchemeAllocationPerEditMode.Visible = true;
                    txtInvestedAmt.Visible = true;
                    TextBox1.Visible = true;
                    txtCurrentValueEditMode.Visible = true;
                    txtAmtAvailableEditMode.Visible = true;
                    txtUnits.Visible = true;
                    txtCurrentValueAddMode.Visible = false;
                    txtAvailableAllocationAddMode.Visible = false;
                    txtSchemeAllocationPerAddMode.Visible = false;
                    txtInvestedAmtAdd.Visible = false;
                    txtAllocationEntryAddMode.Visible = false;
                    txtAmtAvailableAddMode.Visible = false;
                    txtUnitsAddMode.Visible = false;
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
                DropDownList ddlSIPMemberName = (DropDownList)gridEditFormItem.FindControl("ddlSIPMemberName");
                HtmlTableRow trSchemeDDL = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeNameDDL");
                HtmlTableRow trSchemeTextBox = (HtmlTableRow)gridEditFormItem.FindControl("trSchemeNameText");
                TextBox txtSIPEndDate = (TextBox)gridEditFormItem.FindControl("txtSIPEndDate");
                TextBox txtSIPEndDateAdd = (TextBox)gridEditFormItem.FindControl("txtSIPEndDateAdd");
                TextBox txtSIPStartDateAdd = (TextBox)gridEditFormItem.FindControl("txtSIPStartDateAdd");
                TextBox txtSIPStartDate = (TextBox)gridEditFormItem.FindControl("txtSIPStartDate");
                TextBox txtTotalSIPAmount = (TextBox)gridEditFormItem.FindControl("txtTotalSIPAmount");
                TextBox txtTotalSIPAmountAdd = (TextBox)gridEditFormItem.FindControl("txtTotalSIPAmountAdd");
                TextBox TextBox2 = (TextBox)gridEditFormItem.FindControl("TextBox2");
                TextBox TextBox2Add = (TextBox)gridEditFormItem.FindControl("TextBox2Add");
                TextBox txtOtherSchemeAllocationPer = (TextBox)gridEditFormItem.FindControl("txtOtherSchemeAllocationPer");
                TextBox txtOtherSchemeAllocationPerAdd = (TextBox)gridEditFormItem.FindControl("txtOtherSchemeAllocationPerAdd");
                Label txtMemberName = (Label)gridEditFormItem.FindControl("txtMemberName");
                Label txtMemberNameAdd = (Label)gridEditFormItem.FindControl("txtMemberNameAdd");
                TextBox txtSIPFrequencyAdd = (TextBox)gridEditFormItem.FindControl("txtSIPFrequencyAdd");
                TextBox txtSIPFrequency = (TextBox)gridEditFormItem.FindControl("txtSIPFrequency");
                Label lblSchemeName = (Label)gridEditFormItem.FindControl("lblSchemeName");
                Label lblSchemeAdd = (Label)gridEditFormItem.FindControl("lblSchemeAdd");
                //TextBox txt = (TextBox)gridEditFormItem.FindControl("txtUnits");
                //txt.Visible = false;
                if (e.Item.RowIndex == -1)
                {
                    trSchemeDDL.Visible = true;
                    trSchemeTextBox.Visible = false;
                   // BindDDLSIPSchemeAllocated(dropDownList);
                    BindFamilyMembers(ddlSIPMemberName, customerVo.CustomerId);
                    //***************************\\
                    txtSIPEndDate.Visible=false;  
                    txtSIPEndDateAdd.Visible=true;
                    txtSIPStartDate.Visible=false;   
                    txtSIPStartDateAdd.Visible=true;
                    txtTotalSIPAmount.Visible=false;     
                    txtTotalSIPAmountAdd.Visible=true;
                    TextBox2.Visible=false;    
                    TextBox2Add.Visible=true;  
                    txtOtherSchemeAllocationPer.Visible=false;    
                    txtOtherSchemeAllocationPerAdd.Visible=true;                    
                    txtMemberName.Visible=false;       
                    txtMemberNameAdd.Visible=true;
                    txtSIPFrequency.Visible=false;     
                    txtSIPFrequencyAdd.Visible=true;
                    lblSchemeName.Visible = false;    
                    lblSchemeAdd.Visible=true;

                    //***************************\\
                }
                else
                {
                    trSchemeDDL.Visible = false;
                    trSchemeTextBox.Visible = true;
                    //***************************\\
                    txtSIPEndDate.Visible = true;
                    txtSIPEndDateAdd.Visible = false;
                    txtSIPStartDate.Visible = true;
                    txtSIPStartDateAdd.Visible = false;
                    txtTotalSIPAmount.Visible = true;
                    txtTotalSIPAmountAdd.Visible = false;
                    TextBox2.Visible = true;
                    TextBox2Add.Visible = false;
                    txtOtherSchemeAllocationPer.Visible = true;
                    txtOtherSchemeAllocationPerAdd.Visible = false;
                    txtMemberName.Visible = true;
                    txtMemberNameAdd.Visible = false;
                    txtSIPFrequency.Visible = true;
                    txtSIPFrequencyAdd.Visible = false;
                    lblSchemeName.Visible = true;
                    lblSchemeAdd.Visible = false;

                    //***************************\\
                
                }
            }
            //if (e.Item is GridCommandItem)
            //{
            //    LinkButton refreshButton = e.Item.Controls[0].Controls[0].Controls[0].Controls[1].Controls[0] as LinkButton;
            //    refreshButton.Visible = false;
            //}

        }

        protected void RadGrid1_ItemUpdated(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                if (e.Item is GridEditFormItem)
                {
                    GridEditFormItem item = (GridEditFormItem)e.Item;
                    DropDownList ddl = (DropDownList)item.FindControl("ddlPickScheme");
                    DropDownList ddlMembers = (DropDownList)item.FindControl("ddlMemberName");
                    BindFamilyMembers(ddlMembers, customerVo.CustomerId);
                    //BindDDLSchemeAllocated(ddl);
                    RadGrid1.Rebind();
                }
            }
        }


        protected void BindDDLSchemeAllocated(DropDownList ddl, int customerId)
        {
            DataSet dsBindDDLSchemeAllocated = new DataSet();
            dsBindDDLSchemeAllocated = customerGoalPlanningBo.BindDDLSchemeAllocated(customerId, goalId);
            //if (dsBindDDLSchemeAllocated.Tables[0].Rows.Count > 0)
            //{
            ddl.DataSource = dsBindDDLSchemeAllocated.Tables[0];
            ddl.DataTextField = dsBindDDLSchemeAllocated.Tables[0].Columns["PASP_SchemePlanName"].ToString();
            ddl.DataValueField = dsBindDDLSchemeAllocated.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Select", "Select"));
            // }


        }

        protected void BindDDLSIPSchemeAllocated(DropDownList ddl,int customerId)
        {
            DataSet dsBindDDLSchemeAllocated = new DataSet();
            DataTable dtBindSIPDDLSchemeAlloted = new DataTable();
            dtBindSIPDDLSchemeAlloted.Columns.Add("CMFSS_SystematicSetupId");
            dtBindSIPDDLSchemeAlloted.Columns.Add("PASP_SchemePlanName");
            DataRow drBindDDLSIP;

            dsBindDDLSchemeAllocated = customerGoalPlanningBo.BindDDLSIPSchemeAllocated(customerId, goalId);
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
            ddl.Items.Insert(0, new ListItem("Select", "Select"));


        }

        protected void InsertMFSIPAllocation(int sipId, decimal otherAllocation, decimal allocationEntry, decimal totalAllocation)
        {
            if (allocationEntry + otherAllocation <= totalAllocation)
            {
                customerGoalPlanningBo.UpdateSIPGoalAllocationAmount(allocationEntry, sipId, goalId);
                //BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
            }
        }

        protected void InsertMFInvestmentAllocation(int schemeId, decimal otherAllocation, decimal allocationEntry)
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
            investedAmount = (acqCost * allocationEntry) / 100;

            if (allocationEntry + otherAllocation <= 100)
            {
                customerGoalPlanningBo.UpdateGoalAllocationPercentage(allocationEntry, schemeId, goalId, investedAmount);
                //BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
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
                customerGoalPlanningBo.DeleteFundedScheme(schemeCode, goalId);
                GetGoalFundingProgress();
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
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
                GetGoalFundingProgress();
                BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Delete the Record. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }

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
                //tblMessage.Visible = true;
                //ErrorMessage.Visible = true;
                //ErrorMessage.InnerText = "No Records Found...!";
                pnlModelPortfolioNoRecoredFound.Visible = true;

            }

            //    }
            //}

        }
        protected void btnexistmfinvest_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in RadGrid1.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            RadGrid1.MasterTableView.ExportToExcel();
        }
        protected void FutureMFinvest_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in RadGrid2.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            RadGrid2.MasterTableView.ExportToExcel();
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
        protected void BindFamilyMembers(DropDownList ddl, int customerId)
        {
            DataSet dsFamilyMembers = new DataSet();
            dsFamilyMembers = customerGoalPlanningBo.BindDDLFamilyMembers(customerId);


            ddl.DataSource = dsFamilyMembers.Tables[0];
            ddl.DataTextField = dsFamilyMembers.Tables[0].Columns["MemberName"].ToString();
            ddl.DataValueField = dsFamilyMembers.Tables[0].Columns["C_CustomerId"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));


        }
        protected void ddlPickSIPScheme_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsSIPFundingDetails = new DataSet();
            double currentAllocation = 0;
            double otherAllocation = 0;
            double totalInvestedSIPamount = 0;
            double AvialableAmount = 100;
            double currentValue = 0;
            double totalAmounts = 0;
            double totalUnits = 0;
            DateTime sipStartDate = new DateTime();            
            DateTime sipEndDate = new DateTime();

            if (Cache["GoalFundingDetailsdsSIPInvestment" + customerVo.CustomerId.ToString()] != null)
            {
                dsSIPFundingDetails = (DataSet)Cache["GoalFundingDetailsdsSIPInvestment" + customerVo.CustomerId.ToString()];
                DataRow[] drTotalInvestmentAllocationStatus;
                DataRow[] drCurrentGoalInvestmentAllocationStatus;
                DataRow[] drTotalAmount;
                
                DropDownList dropdown = (DropDownList)sender;
                string categoryCode = dropdown.SelectedValue;
                GridEditableItem gridEditFormItem = dropdown.NamingContainer as GridEditableItem;
                TextBox txtSIPEndDate = (TextBox)gridEditFormItem.FindControl("txtSIPEndDate");
                TextBox txtSIPEndDateAdd = (TextBox)gridEditFormItem.FindControl("txtSIPEndDateAdd");
                TextBox txtSIPStartDateAdd = (TextBox)gridEditFormItem.FindControl("txtSIPStartDateAdd");
                TextBox txtSIPStartDate = (TextBox)gridEditFormItem.FindControl("txtSIPStartDate");
                TextBox txtTotalSIPAmount = (TextBox)gridEditFormItem.FindControl("txtTotalSIPAmount");
                TextBox txtTotalSIPAmountAdd = (TextBox)gridEditFormItem.FindControl("txtTotalSIPAmountAdd");
                TextBox TextBox2 = (TextBox)gridEditFormItem.FindControl("TextBox2");
                TextBox TextBox2Add = (TextBox)gridEditFormItem.FindControl("TextBox2Add");
                TextBox txtOtherSchemeAllocationPer = (TextBox)gridEditFormItem.FindControl("txtOtherSchemeAllocationPer");
                TextBox txtOtherSchemeAllocationPerAdd = (TextBox)gridEditFormItem.FindControl("txtOtherSchemeAllocationPerAdd");
                Label txtMemberName = (Label)gridEditFormItem.FindControl("txtMemberName");
                Label txtMemberNameAdd = (Label)gridEditFormItem.FindControl("txtMemberNameAdd");
                TextBox txtSIPFrequencyAdd = (TextBox)gridEditFormItem.FindControl("txtSIPFrequencyAdd");
                TextBox txtSIPFrequency = (TextBox)gridEditFormItem.FindControl("txtSIPFrequency");
                Label lblSchemeName = (Label)gridEditFormItem.FindControl("lblSchemeName");
                Label lblSchemeAdd = (Label)gridEditFormItem.FindControl("lblSchemeAdd");
                string frequency = "";
                  if(categoryCode != "Select" && categoryCode != "")
                  {
                      int sipId =Convert.ToInt32(categoryCode);
                    DataRow[] drtotalSIPamount ;
                    drtotalSIPamount = dsSIPFundingDetails.Tables[1].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
                    drTotalAmount = dsSIPFundingDetails.Tables[2].Select("CMFSS_SystematicSetupId=" + sipId.ToString());
                      
                      if (drtotalSIPamount.Count() > 0)
                        {
                            foreach (DataRow drtotalSIPInvestedAmount in drtotalSIPamount)
                            {
                                totalInvestedSIPamount =totalInvestedSIPamount + double.Parse(drtotalSIPInvestedAmount["TotalInvestedAmount"].ToString());
                              
                            }
                        }
                      if (drTotalAmount.Count() > 0)
                        {
                            foreach (DataRow drAmount in drTotalAmount)
                            {
                                totalAmounts = double.Parse(drAmount["CMFSS_Amount"].ToString());
                                sipStartDate = DateTime.Parse(drAmount["CMFSS_StartDate"].ToString());
                                sipEndDate = DateTime.Parse(drAmount["CMFSS_EndDate"].ToString());
                                frequency = drAmount["XF_FrequencyCode"].ToString();
                            }
                        }

                      AvialableAmount =  totalAmounts - totalInvestedSIPamount;
                      otherAllocation = totalInvestedSIPamount;
                      txtSIPEndDateAdd.Text = sipEndDate.ToShortDateString();
                      txtSIPStartDateAdd.Text = sipStartDate.ToShortDateString();
                      txtTotalSIPAmountAdd.Text = totalAmounts.ToString();
                      TextBox2Add.Text = AvialableAmount.ToString();
                      txtOtherSchemeAllocationPerAdd.Text = otherAllocation.ToString();
                      txtSIPFrequencyAdd.Text = frequency;
                    }
                
                 
                else
                  {

                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have not selected any SIP');", true);

                  }
                  }
        }
        protected void ddlPickScheme_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsFundingDetails = new DataSet();
            decimal currentAllocation = 0;
            decimal otherAllocation = 0;
            decimal totalAllocation = 0;
            decimal AvialableAllocation = 100;
            decimal currentValue = 0;
            decimal totalAmounts = 0;
            decimal totalUnits = 0;
            if (Cache["GoalFundingDetailsdsExistingInvestment" + customerVo.CustomerId.ToString()] != null)
            {
                DropDownList dropdown = (DropDownList)sender;
                string categoryCode = dropdown.SelectedValue;
                GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
                Label txtAvailableAllocationEditMode = editedItem.FindControl("txtAvailableAllocationEditMode") as Label;
                Label txtAvailableAllocationAddMode = editedItem.FindControl("txtAvailableAllocationAddMode") as Label;
                Label txtSchemeAllocationPerEditMode = editedItem.FindControl("txtSchemeAllocationPerEditMode") as Label;
                Label txtSchemeAllocationPerAddMode = editedItem.FindControl("txtSchemeAllocationPerAddMode") as Label;
                Label txtInvestedAmtAdd = editedItem.FindControl("txtInvestedAmtAdd") as Label;
                Label txtInvestedAmt = editedItem.FindControl("txtInvestedAmt") as Label;
                Label txtAllocationEntryAddMode = editedItem.FindControl("txtAllocationEntryAddMode") as Label;
                Label TextBox1 = editedItem.FindControl("TextBox1") as Label;
                Label txtCurrentValueEditMode = editedItem.FindControl("txtCurrentValueEditMode") as Label;
                Label txtCurrentValueAddMode = editedItem.FindControl("txtCurrentValueAddMode") as Label;
                Label txtAmtAvailableEditMode = editedItem.FindControl("txtAmtAvailableEditMode") as Label;
                Label txtAmtAvailableAddMode = editedItem.FindControl("txtAmtAvailableAddMode") as Label;
                Label txtUnitsAddMode = editedItem.FindControl("txtUnitsAddMode") as Label;
                Label txtUnits = editedItem.FindControl("txtUnits") as Label;
                Label lblMemberNameAddMode = editedItem.FindControl("lblMemberNameAddMode") as Label;
                HtmlTableRow trUnits =  editedItem.FindControl("trUnits") as HtmlTableRow;
                HtmlTableRow trCurrentValue =  editedItem.FindControl("trCurrentValue") as HtmlTableRow;
                HtmlTableRow trTotalGoalAllocation =  editedItem.FindControl("trTotalGoalAllocation") as HtmlTableRow;
                HtmlTableRow trOtherGoalAllocation = editedItem.FindControl("trOtherGoalAllocation") as HtmlTableRow;

                trUnits.Visible=false;
                trCurrentValue.Visible=false;
                trTotalGoalAllocation.Visible=false;
                trOtherGoalAllocation.Visible = false;


                txtAvailableAllocationEditMode.Visible = false;
                txtSchemeAllocationPerEditMode.Visible = false;
                txtInvestedAmt.Visible = false;
                TextBox1.Visible = false;
                txtCurrentValueEditMode.Visible = false;
                txtAmtAvailableEditMode.Visible = false;
                txtUnits.Visible = false;

                txtAvailableAllocationAddMode.Visible = true;
                txtSchemeAllocationPerAddMode.Visible = true;
                txtInvestedAmtAdd.Visible = true;
                txtAllocationEntryAddMode.Visible = true;
                txtAmtAvailableAddMode.Visible = true;
                txtUnitsAddMode.Visible = true;

                dsFundingDetails = (DataSet)Cache["GoalFundingDetailsdsExistingInvestment" + customerVo.CustomerId.ToString()];
                DataRow[] drTotalInvestmentAllocationStatus;
                DataRow[] drCurrentGoalInvestmentAllocationStatus;
                DataRow[] drAmountUnitsAllocation;
                //txtAvailableAllocationEditMode
                // Session.Remove(SessionContents.FPS_AddProspect_DataTable);

                if(dropdown.SelectedValue != "" && dropdown.SelectedValue != "Select")
                {
                    trUnits.Visible = true;
                    trCurrentValue.Visible = true;
                    trTotalGoalAllocation.Visible = true;
                    trOtherGoalAllocation.Visible = true;

                foreach (DataRow drGoalExistingInvestments in dsFundingDetails.Tables[2].Rows)
                {
                    drTotalInvestmentAllocationStatus = dsFundingDetails.Tables[2].Select("PASP_SchemePlanCode=" + "'" + dropdown.SelectedValue + "'");
                    if (drTotalInvestmentAllocationStatus.Count() > 0)
                    {
                        foreach (DataRow dr in drTotalInvestmentAllocationStatus)
                        {
                            totalAllocation = decimal.Parse(dr["allocatedPercentage"].ToString());
                        }
                    }
                }

                foreach (DataRow drGoalExistingInvestments in dsFundingDetails.Tables[3].Rows)
                {
                    drCurrentGoalInvestmentAllocationStatus = dsFundingDetails.Tables[3].Select("PASP_SchemePlanCode=" + "'" + dropdown.SelectedValue + "'");
                    if (drCurrentGoalInvestmentAllocationStatus.Count() > 0)
                    {
                        foreach (DataRow dr in drCurrentGoalInvestmentAllocationStatus)
                        {
                            currentAllocation = decimal.Parse(dr["allocatedPercentage"].ToString());
                        }
                    }
                }
                foreach (DataRow drGoalExistingInvestments in dsFundingDetails.Tables[6].Rows)
                {
                    drAmountUnitsAllocation = dsFundingDetails.Tables[6].Select("PASP_SchemePlanCode=" + "'" + dropdown.SelectedValue + "'");
                    if (drAmountUnitsAllocation.Count() > 0)
                    {
                        foreach (DataRow dr in drAmountUnitsAllocation)
                        {
                            currentValue = decimal.Parse(dr["CMFNP_CurrentValue"].ToString());
                            totalUnits = decimal.Parse(dr["CMFNP_NetHoldings"].ToString());
                            totalAmounts = decimal.Parse(dr["CMFNP_AcqCostExclDivReinvst"].ToString());

                        }
                    }
                }
                if (currentAllocation > totalAllocation)
                {
                    otherAllocation = totalAllocation - currentAllocation;
                }
                else
                {
                    otherAllocation = totalAllocation;
                }
                AvialableAllocation = 100 - totalAllocation;

          
                txtCurrentValueAddMode.Text = currentValue.ToString();
                txtUnitsAddMode.Text = totalUnits.ToString();
                txtAmtAvailableAddMode.Text = ((totalAmounts * AvialableAllocation) / 100).ToString();
                txtAvailableAllocationAddMode.Text = AvialableAllocation.ToString();
                txtSchemeAllocationPerAddMode.Text = otherAllocation.ToString();
                txtAllocationEntryAddMode.Text = totalAllocation.ToString();
                txtInvestedAmtAdd.Text = "0";
                //lblMemberNameAddMode.Text ="0";
                //txtUnitsAddMode.Text = "0";
                }
                else
                {
                txtCurrentValueAddMode.Text ="0";
                txtUnitsAddMode.Text = "0";
                txtAmtAvailableAddMode.Text = "0";
                txtAvailableAllocationAddMode.Text = "0";
                txtSchemeAllocationPerAddMode.Text = "0";
                txtAllocationEntryAddMode.Text = "0";
                txtInvestedAmtAdd.Text = "0";

                }

            }
        }

        protected void ddlMemberName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = 0;
            DropDownList dropdown = (DropDownList)sender;
            if (dropdown.SelectedValue != "" && dropdown.SelectedValue != "Select")
            {
                customerId = Int32.Parse(dropdown.SelectedValue);
            }
            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlPickScheme = editedItem.FindControl("ddlPickScheme") as DropDownList;
            HtmlTableCell tdlblSchemeName = editedItem.FindControl("tdlblSchemeName") as HtmlTableCell;
            HtmlTableCell tdddlPickScheme = editedItem.FindControl("tdddlPickScheme") as HtmlTableCell;
            HtmlTableRow trUnits = editedItem.FindControl("trUnits") as HtmlTableRow;
            HtmlTableRow trCurrentValue = editedItem.FindControl("trCurrentValue") as HtmlTableRow;
            HtmlTableRow trTotalGoalAllocation = editedItem.FindControl("trTotalGoalAllocation") as HtmlTableRow;
            HtmlTableRow trOtherGoalAllocation = editedItem.FindControl("trOtherGoalAllocation") as HtmlTableRow;

            trUnits.Visible = false;
            trCurrentValue.Visible = false;
            trTotalGoalAllocation.Visible = false;
            trOtherGoalAllocation.Visible = false;
            BindDDLSchemeAllocated(ddlPickScheme, customerId);
            tdddlPickScheme.Visible = true;
            tdlblSchemeName.Visible = true;

        }
        protected void ddlSIPMemberName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = 0;
            DropDownList dropdown = (DropDownList)sender;
            if (dropdown.SelectedValue != "" && dropdown.SelectedValue != "Select")
            {
                customerId = Int32.Parse(dropdown.SelectedValue);
            }
            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlPickSIPScheme = editedItem.FindControl("ddlPickSIPScheme") as DropDownList;
            BindDDLSIPSchemeAllocated(ddlPickSIPScheme, customerId);
        }
   #endregion


    }
}
