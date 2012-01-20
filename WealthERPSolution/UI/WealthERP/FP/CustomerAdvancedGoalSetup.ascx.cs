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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please first complete Risk Profile');", true);
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
                    }
 
                }
                if (goalId == 0)
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

                //trReturnOnNewInvestments.Visible = true;
                trCorpusToBeLeftBehind.Visible = true;

               
                //*****************Blank Table Cell**********************
                //tdMFBasedBlank.Visible = true;
                tdExistingInvestBlank.Visible = true;
                tdReturnOnExistingInvestBlank.Visible = true;
                tdReturnOnFutureInvestBlank.Visible = true;
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
                //txtExpRateOfReturn.Text = customerAssumptionVo.WeightedReturn.ToString();

                txtCustomerAge.Enabled = false;
                txtSpouseAge.Enabled = false;
                txtRetirementAge.Enabled = false;
                txtCustomerEOL.Enabled = false;
                txtSpouseEOL.Enabled = false;
                txtPostRetirementReturns.Enabled = false;

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

                //trReturnOnNewInvestments.Visible = false;

                trCorpusToBeLeftBehind.Visible = false;

                //*****************Blank Table Cell**********************
                //tdMFBasedBlank.Visible = false;
                tdExistingInvestBlank.Visible = false;
                tdReturnOnExistingInvestBlank.Visible = false;
                tdReturnOnFutureInvestBlank.Visible = false;
                //tdROIFutureInvestBlank.Visible = false;
                //tdReturnOnNewInvestBlank.Visible = false;
                tdCorpusToBeLeftBehindBlank.Visible = false;
                tdCommentBlank.Visible = false;


                //trReturnOnNewInvestments.Visible = false;
                trCorpusToBeLeftBehind.Visible = false;


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
                txtInflation.Text = "0";
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
                    txtAboveRateOfInterst.Text = "0";
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
            GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
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
                    //trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(customerVo.CustomerId);
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
                    //trROIFutureInvestment.Visible = false;
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    BindPickChildDropDown(customerVo.CustomerId);
                    ddlPickChild.SelectedValue = goalProfileSetupVo.AssociateId.ToString();
                    ddlPickChild.Enabled = false;                    
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
                    //trROIFutureInvestment.Visible = true;

                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Monthly Requirment Today :";
                    ddlGoalType.SelectedValue = goalProfileSetupVo.Goalcode;
                    txtGoalDate.Text = goalProfileSetupVo.GoalDate.ToShortDateString();
                    txtGoalCostToday.Text = goalProfileSetupVo.CostOfGoalToday.ToString();
                    ddlGoalYear.Text = goalProfileSetupVo.GoalYear.ToString();
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
                    customerAssumptionVo.ReturnOnNewInvestment = Convert.ToDouble(txtExpRateOfReturn.Text);
                    customerAssumptionVo.InflationPercent = Convert.ToDouble(txtInflation.Text);

                    customerGoalPlanningVo.CorpusLeftBehind = Convert.ToInt64(txtCorpusToBeLeftBehind.Text);

                }


                customerGoalPlanningBo.CreateCustomerGoalPlanning(customerGoalPlanningVo, customerAssumptionVo, customerVo.CustomerId, false,out goalId);

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
                GetGoalFundingProgress();
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                BindddlModelPortfolioGoalSchemes();
                SetGoalProgressImage(goalPlanningVo.Goalcode);

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

                GetGoalFundingProgress();
                BindExistingFundingScheme(dsGoalFundingDetails.Tables[0]);
                BindMonthlySIPFundingScheme(dsGoalFundingDetails.Tables[1]);
                ShowGoalDetails(customerGoalFundingProgressVo, goalPlanningVo);
                BindddlModelPortfolioGoalSchemes();
                SetGoalProgressImage(goalPlanningVo.Goalcode);

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

        protected void RadGrid1_ItemInserted(object source, GridCommandEventArgs e)
        {
            decimal totalOtherAllocation = 0;
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
                decimal OtherGoalAllocation = decimal.Parse(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["OtherGoalAllocation"].ToString());
                InsertMFInvestmentAllocation(schemePlanId, OtherGoalAllocation, allocationEntry);
            }
            //if (e.CommandName == RadGrid.EditCommandName || e.CommandName == RadGrid.CancelCommandName || e.CommandName == RadGrid.UpdateCommandName)
            //{
                GetGoalFundingProgress();
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
            dsBindDDLSchemeAllocated = customerGoalPlanningBo.BindDDLSchemeAllocated(customerVo.CustomerId, goalId);
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


   #endregion


    }
}