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

namespace WealthERP.Advisor
{
    public partial class AddCustomerFinancialPlanningGoalSetup : System.Web.UI.UserControl
    {

        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
        GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
        CustomerGoalSetupDao customerGoalSetupDao = new CustomerGoalSetupDao();
        CustomerVo customerVo = new CustomerVo();
        List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();

        double RTSaveReq = 0.0;
        string ActiveFilter = "";
        int activeTabIndex = 0;
        decimal InflationPercent;
        RMVo rmVo = new RMVo();
        //int ParentcustomerID = 0;
        AdvisorVo advisorVo = new AdvisorVo();
        int AdvisorRMId = 0;

        int investmentTotal = 0;
        int surplusTotal = 0;
        int investedAmountForAllGaol = 0;
        int monthlySavingRequired = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionContents.FPS_ProspectList_CustomerId] != null)
            {
                Session["FP_UserID"] = Session[SessionContents.FPS_ProspectList_CustomerId];
            }
            try
            {

                //this.txtPickCustomer.Attributes.Add("onkeypress", "ShowImage()");
                //this.txtPickCustomer.Attributes.Add("onblur", "HideImage()");


                SessionBo.CheckSession();
                if (ViewState["FilterValue"] != null)
                    ActiveFilter = ViewState["FilterValue"].ToString();
                lblNoteFunding.Visible = false;
                if (!IsPostBack)
                {
                    InflationPercent = GoalSetupBo.GetInflationPercent();
                    ViewState["InflationPer"] = InflationPercent;
                    //Session["FP_UserID"] = "";
                    //Session["FP_UserName"] = "";
                    //Session[SessionContents.CurrentUserRole] = "RM";
                    if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                    {
                        rmVo = (RMVo)Session[SessionContents.RmVo];
                        AdvisorRMId = rmVo.RMId;
                        //txtPickCustomer_autoCompleteExtender.ServiceMethod = "GetCustomerName";
                    }
                    else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                    {
                        advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                        AdvisorRMId = advisorVo.advisorId;
                        //txtPickCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }

                    if ((Session["FP_UserID"] == null) && (Session["FP_UserName"] == null))
                    {
                        SessionBo.CheckSession();
                        //rmVo = (RMVo)Session[SessionContents.RmVo];
                        //txtPickCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

                        //txtPickCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                        BindGoalObjTypeDropDown();
                        InitialPageLoadState();
                        Tab2ControlVisibility(0);
                        TabContainer1.ActiveTabIndex = 0;
                    }
                    else
                    {

                        //txtPickCustomer.Text = Session["FP_UserName"].ToString();
                        //txtCustomerId.Value = Session[SessionContents.FPS_ProspectList_CustomerId].ToString();
                        SessionBo.CheckSession();
                        //txtPickCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                        InitialPageLoadState();
                        double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                        txtExpRateOfReturn.Text = ExpROI.ToString();
                        ddlGoalType.Enabled = true;
                        txtCurrentInvestPurpose.Text = "0";
                        txtAboveRateOfInterst.Text = "0";
                        BindGoalObjTypeDropDown();

                        int gvRT = this.BindRTGoalOutputGridView();
                        int gvOUT = this.BindGoalOutputGridView(2);


                        if (gvRT == 0 && gvOUT == 0)
                        {

                            //There is no goal exist for this customer.
                            Tab2ControlVisibility(0);
                            lblHeaderOutPut.Text = "No Records Found";
                            TabContainer1.ActiveTabIndex = 0;
                            trRTParaGraph.Visible = false;
                            trOtherGoalParagraph.Visible = false;
                            BindGoalGapTabGrid(true, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);

                        }
                        else
                            if (gvRT == 1 && gvOUT == 1)
                            {


                                Tab2ControlVisibility(1);
                                this.BindGoalOutputGridView(1);
                                TabContainer1.ActiveTabIndex = 1;
                                trRTParaGraph.Visible = true;
                                trOtherGoalParagraph.Visible = true;

                                BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);

                            }
                            //this.BindGoalOutputGridView(1);
                            else
                                if (gvOUT == 1 && gvRT == 0)
                                {

                                    lblHeaderOutPut.Text = "Customer Goal Profile Details";
                                    gvGoalOutPut.Visible = true;
                                    Delete.Visible = true;
                                    Activate.Visible = true;
                                    Deactive.Visible = true;

                                    hidRTSaveReq.Value = string.Empty;
                                    this.BindGoalOutputGridView(1);
                                    TabContainer1.ActiveTabIndex = 1;
                                    trRTParaGraph.Visible = false;
                                    trOtherGoalParagraph.Visible = true;

                                    BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);
                                    //gvRetirement.Visible = true;
                                    //lblTotalText.Visible = true;
                                    //lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                                }
                                else
                                    if (gvOUT == 0 && gvRT == 1)
                                    {
                                        Delete.Visible = true;
                                        lblHeaderOutPut.Text = "Customer Goal Profile Details";
                                        gvRetirement.Visible = true;
                                        lblTotalText.Visible = true;
                                        lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;
                                        TabContainer1.ActiveTabIndex = 1;
                                        trRTParaGraph.Visible = true;
                                        trOtherGoalParagraph.Visible = false;
                                        BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);
                                    }




                    }
                    Trigger();
                }






            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void Trigger()
        {


            if (Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != string.Empty && Session[SessionContents.FPS_ProspectList_CustomerId] != null)
            {
                //ParentcustomerID = int.Parse(txtCustomerId.Value);

                Session["FP_UserID"] = Session[SessionContents.FPS_ProspectList_CustomerId].ToString();
                //Session["FP_UserName"] = txtPickCustomer.Text;


                BindGoalObjTypeDropDown();
                //ShowOutPutTab();

            }

            //For Tab1 Add mode.....................
            SetPageLoadState(1);
            BtnSetVisiblity(1);
            ControlSetVisiblity(1);

        }

        protected void ShowOutPutTab()
        {

            int gvRT = this.BindRTGoalOutputGridView();
            int gvOUT = this.BindGoalOutputGridView(2);
            Tab2ControlVisibility(0);

            if (gvRT == 0 && gvOUT == 0)
            {

                //There is no goal exist for this customer.
                Tab2ControlVisibility(0);
                lblHeaderOutPut.Text = "No Records Found";
                TabContainer1.ActiveTabIndex = 0;
                trRTParaGraph.Visible = false;
                trOtherGoalParagraph.Visible = false;
                BindGoalGapTabGrid(true, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);

            }
            else
                if (gvRT == 1 && gvOUT == 1)
                {


                    Tab2ControlVisibility(1);
                    this.BindGoalOutputGridView(1);
                    TabContainer1.ActiveTabIndex = 1;
                    trRTParaGraph.Visible = true;
                    trOtherGoalParagraph.Visible = true;
                    BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);
                }
                //this.BindGoalOutputGridView(1);
                else
                    if (gvOUT == 1 && gvRT == 0)
                    {

                        lblHeaderOutPut.Text = "Customer Goal Profile Details";
                        gvGoalOutPut.Visible = true;
                        Delete.Visible = true;
                        Activate.Visible = true;
                        Deactive.Visible = true;

                        hidRTSaveReq.Value = string.Empty;
                        this.BindGoalOutputGridView(1);
                        trRTParaGraph.Visible = false;
                        trOtherGoalParagraph.Visible = true;
                        TabContainer1.ActiveTabIndex = 1;
                        BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);

                        //gvRetirement.Visible = true;
                        //lblTotalText.Visible = true;
                        //lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                    }
                    else
                        if (gvOUT == 0 && gvRT == 1)
                        {
                            Delete.Visible = true;
                            lblHeaderOutPut.Text = "Customer Goal Profile Details";
                            gvRetirement.Visible = true;
                            lblTotalText.Visible = true;
                            lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;
                            trRTParaGraph.Visible = true;
                            trOtherGoalParagraph.Visible = false;
                            TabContainer1.ActiveTabIndex = 1;
                            BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);

                        }


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
            ddlGoalType.Enabled = false;
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
            txtInflation.Text = ViewState["InflationPer"].ToString();
            btnEdit.Visible = false;
            btnUpdate.Visible = false;
            btnBackToView.Visible = false;
        }

        protected void lnkRTGoalType_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvRetirement.DataKeys[rowIndex];
            int GoalId = Convert.ToInt32(dk.Value);
            TabContainer1.ActiveTabIndex = 0;
            ControlSetVisiblity(0);
            ViewState["ViewEditID"] = GoalId;
            ShowGoalDetails(int.Parse(Session["FP_UserID"].ToString()), GoalId);
            //lblPickCustomer.Text = "Customer Name";
            lblGoalbjective.Text = "Goal Objective :";
            lblPickChild.Text = "Child Name :";
            //chkApprove.Visible = false;
            trchkApprove.Visible = false;
        }

        protected void lnkGoalType_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvGoalOutPut.DataKeys[rowIndex];
            int GoalId = Convert.ToInt32(dk.Value);
            if (chkApprove.Checked == true)
                chkApprove.Checked = false;
            TabContainer1.ActiveTabIndex = 0;
            ControlSetVisiblity(0);
            ViewState["ViewEditID"] = GoalId;
            ShowGoalDetails(int.Parse(Session["FP_UserID"].ToString()), GoalId);
            //lblPickCustomer.Text = "Customer Name";
            lblGoalbjective.Text = "Goal Objective :";





        }
        protected void ShowGoalDetails(int CustomerId, int GoalId)
        {
            GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
            goalProfileSetupVo = GoalSetupBo.GetCustomerGoal(CustomerId, GoalId);
            BtnSetVisiblity(0);
            lblNote.Visible = false;
            //lblReqNote.Visible = false;
            trRequiedNote.Visible = false;
            lblHeader.Text = "Goal Details";


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
                    ddlGoalType.Text = goalProfileSetupVo.Goalcode;
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

        }

        protected void clearAll()
        {


        }
        protected void ddlGoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ParentCustomerId = int.Parse(Session["FP_UserID"].ToString());
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
                    TabContainer1.ActiveTabIndex = 0;

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


                    BindPickChildDropDown(ParentCustomerId);
                    TabContainer1.ActiveTabIndex = 0;
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

                    BindPickChildDropDown(ParentCustomerId);
                    TabContainer1.ActiveTabIndex = 0;
                    break;
                case "RT":

                    trPickChild.Visible = false;


                    //trGoalDesc.Visible = false;

                    trROIFutureInvestment.Visible = true;
                    txtROIFutureInvest.Text = "7";
                    lblGoalYear.Text = "Goal Year :";
                    lblGoalCostToday.Text = "Annual Requirment Today :";
                    //default  current investment and Rate of return of above to 0
                    txtCurrentInvestPurpose.Text = "0";
                    txtAboveRateOfInterst.Text = "0";
                    TabContainer1.ActiveTabIndex = 0;
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

                    TabContainer1.ActiveTabIndex = 0;

                    break;
                default:
                    break;

            }

        }

        private void BindGoalObjTypeDropDown()
        {

            DataSet ds = GoalSetupBo.GetCustomerGoalProfiling();
            ddlGoalType.DataSource = ds;
            ddlGoalType.DataValueField = ds.Tables[0].Columns["XG_GoalCode"].ToString();
            ddlGoalType.DataTextField = ds.Tables[0].Columns["XG_GoalName"].ToString();
            ddlGoalType.DataBind();
            ddlGoalType.Items.Insert(0, new ListItem("Select", "0"));
            ddlGoalType.SelectedIndex = 0;

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

        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                //Customer id select from AutoComplite TextBox Values
                rmVo = (RMVo)Session[SessionContents.RmVo];

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
                if (!string.IsNullOrEmpty(txtGoalDescription.Text))
                {
                    goalProfileSetupVo.GoalDescription = txtGoalDescription.Text;
                }

                goalProfileSetupVo.CurrInvestementForGoal = 0;
                goalProfileSetupVo.ROIEarned =double.Parse(txtAboveRateOfInterst.Text);
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
                    GoalSetupBo.CreateCustomerGoalProfileForRetirement(goalProfileSetupVo, ParentCustomerId, 0);

                }
                else
                    GoalSetupBo.CreateCustomerGoalProfile(goalProfileSetupVo, ParentCustomerId, 0);


                //Tab2ControlVisibility(0);

                //int gvRT=BindRTGoalOutputGridView();
                //if (gvRT == 1)
                //{
                //    gvRetirement.Visible = true;
                //    lblTotalText.Visible = true;
                //    lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                //}

                // this.BindGoalOutputGridView(1);

                ShowOutPutTab();
                SetPageLoadState(1);
                TabContainer1.ActiveTabIndex = 0;

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
        private int BindGoalOutputGridView(int ActiveFlag)
        {

           

            try
            {
                //use same seeion name what ever used in risk profilling..


                GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(int.Parse(Session["FP_UserID"].ToString()), ActiveFlag,out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);


                //if (GoalProfileList == null)
                //{
                //    BindGoalGapTabGrid(true, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);
                //}
                //else
                //{
                //    BindGoalGapTabGrid(false, investmentTotal, surplusTotal, investedAmountForAllGaol, monthlySavingRequired);
                //}
                



                // lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (GoalProfileList == null)
                {
                    DataTable dtGoalProfile = new DataTable();

                    dtGoalProfile.Columns.Add("GoalID");
                    dtGoalProfile.Columns.Add("GoalName");
                    dtGoalProfile.Columns.Add("ChildName");
                    dtGoalProfile.Columns.Add("CostToday");
                    dtGoalProfile.Columns.Add("CurrentInvestment");
                    dtGoalProfile.Columns.Add("SavingRequired");
                    dtGoalProfile.Columns.Add("GoalAmount");
                    dtGoalProfile.Columns.Add("GoalPrifileDate");
                    dtGoalProfile.Columns.Add("GoalYear");
                    dtGoalProfile.Columns.Add("IsActive");
                    dtGoalProfile.Columns.Add("CustomerApprovedOn");



                    DataRow drGoalProfile;

                    drGoalProfile = dtGoalProfile.NewRow();
                    //goalProfileSetupVo = new GoalProfileSetupVo();

                    drGoalProfile["GoalID"] = string.Empty;
                    drGoalProfile["GoalName"] = string.Empty;
                    drGoalProfile["ChildName"] = string.Empty;
                    drGoalProfile["CostToday"] = string.Empty;
                    drGoalProfile["CurrentInvestment"] = string.Empty;
                    drGoalProfile["SavingRequired"] = string.Empty;
                    drGoalProfile["GoalAmount"] = string.Empty;
                    drGoalProfile["GoalPrifileDate"] = string.Empty;
                    drGoalProfile["GoalYear"] = string.Empty;
                    drGoalProfile["IsActive"] = string.Empty;

                    if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                        drGoalProfile["CustomerApprovedOn"] = string.Empty;
                    else
                        drGoalProfile["CustomerApprovedOn"] = string.Empty;
                    dtGoalProfile.Rows.Add(drGoalProfile);


                    gvGoalOutPut.DataSource = dtGoalProfile;
                    gvGoalOutPut.DataBind();

                    gvGoalOutPut.Rows[0].Visible = false;

                    Label TotalText = (Label)gvGoalOutPut.FooterRow.FindControl("lblTotalText");
                    TotalText.Visible = false;

                    lblTotalText.Visible = false;

                    Activate.Visible = false;
                    Deactive.Visible = false;
                    Delete.Visible = false;
                    Label lblActiveMsg = (Label)gvGoalOutPut.HeaderRow.FindControl("ActiveMessage");

                    lblActiveMsg.Visible = true;

                    return 0;

                    //gvGoalOutPut.DataSource = null;
                    //gvGoalOutPut.DataBind();  
                }
                else
                {

                    DataTable dtGoalProfile = new DataTable();

                    dtGoalProfile.Columns.Add("GoalID");
                    dtGoalProfile.Columns.Add("GoalName");
                    dtGoalProfile.Columns.Add("ChildName");
                    dtGoalProfile.Columns.Add("CostToday");
                    dtGoalProfile.Columns.Add("CurrentInvestment");
                    dtGoalProfile.Columns.Add("SavingRequired");
                    dtGoalProfile.Columns.Add("GoalAmount");
                    dtGoalProfile.Columns.Add("GoalPrifileDate");
                    dtGoalProfile.Columns.Add("GoalYear");
                    dtGoalProfile.Columns.Add("IsActive");
                    dtGoalProfile.Columns.Add("CustomerApprovedOn");

                    double monthlySaveRequired = 0.0;
                    double allGoalCostToday = 0;
                    double currentInvestmentTotal = 0;
                    double allGoalCostInGoalYear = 0;

                    double TotalSaveReq = 0.0;

                    DataRow drGoalProfile;

                    for (int i = 0; i < GoalProfileList.Count; i++)
                    {

                        drGoalProfile = dtGoalProfile.NewRow();
                        goalProfileSetupVo = new GoalProfileSetupVo();
                        goalProfileSetupVo = GoalProfileList[i];
                        drGoalProfile["GoalID"] = goalProfileSetupVo.GoalId.ToString();
                        drGoalProfile["GoalName"] = goalProfileSetupVo.GoalName.ToString();
                        drGoalProfile["ChildName"] = goalProfileSetupVo.ChildName.ToString();
                        drGoalProfile["CostToday"] =goalProfileSetupVo.CostOfGoalToday!=0 ? String.Format("{0:n2}", goalProfileSetupVo.CostOfGoalToday.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))):"0"; 
                        allGoalCostToday += goalProfileSetupVo.CostOfGoalToday;
                        drGoalProfile["CurrentInvestment"] = goalProfileSetupVo.CurrInvestementForGoal!=0 ? String.Format("{0:n2}", goalProfileSetupVo.CurrInvestementForGoal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))):"0";
                        currentInvestmentTotal += goalProfileSetupVo.CurrInvestementForGoal;
                        drGoalProfile["SavingRequired"] =goalProfileSetupVo.MonthlySavingsReq!=0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.MonthlySavingsReq.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")):"0";
                        monthlySaveRequired += goalProfileSetupVo.MonthlySavingsReq;
                        drGoalProfile["GoalAmount"] =goalProfileSetupVo.FutureValueOfCostToday !=0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.FutureValueOfCostToday.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")):"0";
                        allGoalCostInGoalYear += goalProfileSetupVo.FutureValueOfCostToday;
                        drGoalProfile["GoalPrifileDate"] = goalProfileSetupVo.GoalProfileDate.ToShortDateString();
                        drGoalProfile["GoalYear"] = goalProfileSetupVo.GoalYear.ToString();
                        if (goalProfileSetupVo.IsActice == 0)
                        {
                            drGoalProfile["IsActive"] = "Inactive";

                        }
                        else
                            drGoalProfile["IsActive"] = "Active";


                        if (goalProfileSetupVo.CustomerApprovedOn != DateTime.MinValue)
                            drGoalProfile["CustomerApprovedOn"] = goalProfileSetupVo.CustomerApprovedOn.ToShortDateString();
                        else
                            drGoalProfile["CustomerApprovedOn"] = string.Empty;
                        dtGoalProfile.Rows.Add(drGoalProfile);
                    }

                    gvGoalOutPut.DataSource = dtGoalProfile;
                    gvGoalOutPut.DataBind();

                    Label lblAllGoalCostTotal = (Label)gvGoalOutPut.FooterRow.FindControl("lblCostTodayTotal");
                    lblAllGoalCostTotal.Text = Math.Round(double.Parse(allGoalCostToday.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    Label lblCurrentInvestmentTotal = (Label)gvGoalOutPut.FooterRow.FindControl("lblCurrentInvestmentTotal");
                    lblCurrentInvestmentTotal.Text = Math.Round(double.Parse(currentInvestmentTotal.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    Label lblMonthlyTotal = (Label)gvGoalOutPut.FooterRow.FindControl("lblMonthlySavingTotal");
                    lblMonthlyTotal.Text = Math.Round(double.Parse(monthlySaveRequired.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    Label lblGoalCostInGoalYear = (Label)gvGoalOutPut.FooterRow.FindControl("lblGoalAmountTotal");
                    lblGoalCostInGoalYear.Text = Math.Round(double.Parse(allGoalCostInGoalYear.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));


                    if (!String.IsNullOrEmpty(hidRTSaveReq.Value))
                    {
                        TotalSaveReq = monthlySaveRequired + double.Parse(hidRTSaveReq.Value);
                    }
                    else
                        TotalSaveReq = monthlySaveRequired;

                    lblHeaderOutPut.Text = "Customer Goal Profile Details";
                    lblTotalText.Visible = true;

                    Activate.Visible = true;
                    Deactive.Visible = true;
                    Delete.Visible = true;
                    Label lblActiveMsg = (Label)gvGoalOutPut.HeaderRow.FindControl("ActiveMessage");
                    lblActiveMsg.Visible = false;
                    lblTotalText.Text = "Total Saving Required Every Month=Rs." + Math.Round(TotalSaveReq, 0).ToString();
                    lblOtherGoalParagraph.Text = GoalSetupBo.OtherGoalDescriptionText(int.Parse(Session["FP_UserID"].ToString()));


                    return 1;
                }


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:BindGoalOutputGridView()");
                object[] objects = new object[1];
                objects[0] = Session["FP_UserID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        private void BindGoalGapTabGrid(bool isGoalListEmpty,int investmentTotal,int surplusTotal, int investedAmountForAllGaol,int monthlySavingRequired)
        {

            try
            {
                int existingInvestmentGap = investmentTotal - investedAmountForAllGaol;
                int surplusGap = surplusTotal - monthlySavingRequired;

                if (isGoalListEmpty == false)
                {
                    DataTable dtGoalFundedGap = new DataTable();
                    dtGoalFundedGap.Columns.Add("Source");
                    dtGoalFundedGap.Columns.Add("GoalFunding");
                    dtGoalFundedGap.Columns.Add("GoalGap");
                    dtGoalFundedGap.Columns.Add("IsGap");

                    DataRow drGoalFundedGap;

                    drGoalFundedGap = dtGoalFundedGap.NewRow();
                    drGoalFundedGap["Source"] = "ExistingInvestment = " + investmentTotal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    if (investedAmountForAllGaol != 0)
                    {
                        drGoalFundedGap["GoalFunding"] = "Total money allocated to Goals = " + investedAmountForAllGaol.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    { 
                        drGoalFundedGap["GoalFunding"] = "Total money allocated to Goals = 0" ;
                    }
                    if (existingInvestmentGap != 0)
                    {
                        drGoalFundedGap["GoalGap"] = existingInvestmentGap.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    {
                        drGoalFundedGap["GoalGap"] = "0";
                    }
                    if (existingInvestmentGap < 0)
                        drGoalFundedGap["IsGap"] = 1;
                    else
                        drGoalFundedGap["IsGap"] = 0;
                    dtGoalFundedGap.Rows.Add(drGoalFundedGap);


                    drGoalFundedGap = dtGoalFundedGap.NewRow();
                    if (surplusTotal != 0)
                    {
                        drGoalFundedGap["Source"] = "Available Surplus (monthly) = " + surplusTotal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    {
                        drGoalFundedGap["Source"] = "Available Surplus (monthly)= 0";
                    }
                    if (monthlySavingRequired != 0)
                    {
                        drGoalFundedGap["GoalFunding"] = "Amount to be saved Per month =  " + monthlySavingRequired.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    {
                        drGoalFundedGap["GoalFunding"] = "Amount to be saved Per month = 0";
                    }
                    if (surplusGap != 0)
                    {
                        drGoalFundedGap["GoalGap"] = surplusGap.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    }
                    else
                        drGoalFundedGap["GoalGap"] = "0";
                    if (surplusGap < 0)
                        drGoalFundedGap["IsGap"] = 1;
                    else
                        drGoalFundedGap["IsGap"] = 0;
                    dtGoalFundedGap.Rows.Add(drGoalFundedGap);



                    gvGoalFundingGap.DataSource = dtGoalFundedGap;
                    gvGoalFundingGap.DataBind();
                    lblNoteFunding.Visible = true;
                    gvGoalFundingGap.Columns[3].Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:BindGoalGapTabGrid()");
                object[] objects = new object[5];
                objects[0] = isGoalListEmpty;
                objects[1] = investmentTotal;
                objects[2] = surplusTotal;
                objects[3] = investedAmountForAllGaol;
                objects[4] = monthlySavingRequired;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            } 
                
            
 
        }
        protected void gvGoalFundingGap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int isGoalFundGap = 0;

                Image imgGoalFundIndicator = e.Row.FindControl("imgGoalFundIndicator") as Image;
                Label lblgoalFundPercentage = e.Row.FindControl("lblIsGap") as Label;
                isGoalFundGap = int.Parse(lblgoalFundPercentage.Text);
                if (isGoalFundGap == 1)
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/cross.jpg";
                }
                else if (isGoalFundGap == 0)
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/check.jpg";

                }
            }
        }

        private int BindRTGoalOutputGridView()
        {
            double hidvalue = 0;
            string GoalDescription = "";
            //use same session name what ever used in risk profilling..
            DataSet dsRTGoal = (DataSet)GoalSetupBo.GetCustomerRTDetails(int.Parse(Session["FP_UserID"].ToString()));
            if (dsRTGoal != null && dsRTGoal.Tables.Count > 0 && dsRTGoal.Tables[0].Rows.Count > 0)
            {
                DataTable dtRTGoal = new DataTable();
                dtRTGoal.Columns.Add("CG_GoalId");
                dtRTGoal.Columns.Add("XG_GoalName");
                dtRTGoal.Columns.Add("CG_GoalYear");
                dtRTGoal.Columns.Add("CG_FVofCostToday");
                dtRTGoal.Columns.Add("CG_CurrentInvestment");
                dtRTGoal.Columns.Add("CG_LumpsumInvestmentRequired");
                dtRTGoal.Columns.Add("CG_YearlySavingsRequired");
                dtRTGoal.Columns.Add("CG_MonthlySavingsRequired");
                dtRTGoal.Columns.Add("CG_GoalProfileDate");
                
                DataRow drRTGoal;

                foreach (DataRow dr in dsRTGoal.Tables[0].Rows)
                {
                    drRTGoal = dtRTGoal.NewRow();
                    drRTGoal["CG_GoalId"] = dr["CG_GoalId"].ToString();
                    drRTGoal["XG_GoalName"] = dr["XG_GoalName"].ToString();
                    drRTGoal["CG_GoalYear"] = dr["CG_GoalYear"].ToString();
                    drRTGoal["CG_FVofCostToday"] = String.Format("{0:n2}", Math.Round(decimal.Parse(dr["CG_FVofCostToday"].ToString()),0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drRTGoal["CG_CurrentInvestment"] = String.Format("{0:n2}", Math.Round(decimal.Parse(dr["CG_CurrentInvestment"].ToString()),0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drRTGoal["CG_LumpsumInvestmentRequired"] = String.Format("{0:n2}", Math.Round(decimal.Parse(dr["CG_LumpsumInvestmentRequired"].ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drRTGoal["CG_YearlySavingsRequired"] = String.Format("{0:n2}", Math.Round(decimal.Parse(dr["CG_YearlySavingsRequired"].ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drRTGoal["CG_MonthlySavingsRequired"] = String.Format("{0:n2}", Math.Round(decimal.Parse(dr["CG_MonthlySavingsRequired"].ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drRTGoal["CG_GoalProfileDate"] =  dr["CG_GoalProfileDate"].ToString();

                    dtRTGoal.Rows.Add(drRTGoal);
                }

                gvRetirement.DataSource = dtRTGoal;
                gvRetirement.DataBind();
                //RTSaveReq = double.Parse(dsRTGoal.Tables[0].Rows[0]["CG_MonthlySavingsRequired"].ToString());
                if (dsRTGoal.Tables[0].Rows[0]["CG_MonthlySavingsRequired"] != null)
                    hidvalue = double.Parse(dsRTGoal.Tables[0].Rows[0]["CG_MonthlySavingsRequired"].ToString());
                hidRTSaveReq.Value = Math.Round(hidvalue, 2).ToString();
                GoalDescription = GoalSetupBo.RTGoalDescriptionText(int.Parse(Session["FP_UserID"].ToString()));
                lblRTParagraph.Text = GoalDescription;
                return 1;
            }
            return 0;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SetPageLoadState(0);
            Tab2ControlVisibility(0);
        }

        public void SetPageLoadState(int Bool)
        {
            if (Bool == 0)
            {
                lblHeader.Text = "Goal Profile";
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
                txtInflation.Text = ViewState["InflationPer"].ToString();
                trROIFutureInvestment.Visible = false;
                txtComment.Text = "";
                double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                txtExpRateOfReturn.Text = ExpROI.ToString();

                chkApprove.Checked = false;

                trlblApproveOn.Visible = false;

                trchkApprove.Visible = true;
                lblHeader.Visible = true;
                lblNote.Visible = true;

                trRequiedNote.Visible = true;



            }
            else
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
                txtInflation.Text = ViewState["InflationPer"].ToString();
                trROIFutureInvestment.Visible = false;

                txtROIFutureInvest.Text = "";

                txtComment.Text = "";
                double ExpROI = (Double)GoalSetupBo.GetExpectedROI(int.Parse(Session["FP_UserID"].ToString()));
                txtExpRateOfReturn.Text = ExpROI.ToString();
                if (chkApprove.Checked == true)
                {
                    chkApprove.Checked = false;

                }
                if (chkApprove.Enabled == false)
                    chkApprove.Enabled = true;



            }


        }


        private string GetSelectedGoalIDString()
        {
            string gvGoalIds = "";

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridViewRow gvRow in gvGoalOutPut.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkGoalOutput");
                if (ChkBxItem.Checked)
                {
                    gvGoalIds += Convert.ToString(gvGoalOutPut.DataKeys[gvRow.RowIndex].Value) + "~";
                }
            }
            foreach (GridViewRow gvRow in gvRetirement.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkRTGoalOutput");
                if (ChkBxItem.Checked)
                {
                    gvGoalIds += Convert.ToString(gvRetirement.DataKeys[gvRow.RowIndex].Value) + "~";
                }
            }

            return gvGoalIds;

        }
        protected void Activate_Click(object sender, EventArgs e)
        {
            try
            {
                string GoalIds = GetSelectedGoalIDString();
                GoalSetupBo.SetCustomerGoalIsActive(GoalIds, int.Parse(Session["FP_UserID"].ToString()));
                //this.BindRTGoalOutputGridView();
                //this.BindGoalOutputGridView(1);
                ShowOutPutTab();
                ViewState["FilterValue"] = 1;


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:Activate_Click()");
                object[] objects = new object[1];
                objects[0] = Session["FP_UserID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Deactive_Click(object sender, EventArgs e)
        {

            try
            {
                string GoalIds = GetSelectedGoalIDString();
                GoalSetupBo.SetCustomerGoalDeActive(GoalIds, int.Parse(Session["FP_UserID"].ToString()));
                //this.BindRTGoalOutputGridView();
                //this.BindGoalOutputGridView(1);
                ShowOutPutTab();
                ViewState["FilterValue"] = 1;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:Deactive_Click()");
                object[] objects = new object[1];
                objects[0] = Session["FP_UserID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void Delete_Click(object sender, EventArgs e)
        {

            try
            {
                string GoalIds = GetSelectedGoalIDString();
                GoalSetupBo.DeleteCustomerGoal(GoalIds, int.Parse(Session["FP_UserID"].ToString()));
                //this.BindRTGoalOutputGridView();
                //this.BindGoalOutputGridView(1);
                ShowOutPutTab();
                ViewState["FilterValue"] = 1;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddCustomerFinancialPlanningGoalSetup.ascx:Delete_Click()");
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
                SessionBo.CheckSession();
                //Customer id select from AutoComplite TextBox Values
                rmVo = (RMVo)Session[SessionContents.RmVo];

                int ParentCustomerId = int.Parse(Session["FP_UserID"].ToString());
                goalProfileSetupVo.CustomerId = ParentCustomerId;
                goalProfileSetupVo.Goalcode = ddlGoalType.SelectedValue.ToString();
                goalProfileSetupVo.CostOfGoalToday = double.Parse(txtGoalCostToday.Text);
                goalProfileSetupVo.GoalDate = DateTime.Parse(txtGoalDate.Text);
                goalProfileSetupVo.GoalYear = int.Parse(ddlGoalYear.SelectedValue);
                if (ddlGoalType.SelectedValue == "OT" || ddlGoalType.SelectedValue == "BH")
                {
                    goalProfileSetupVo.GoalDescription = txtGoalDescription.Text.ToString();
                }
                if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
                {
                    goalProfileSetupVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
                }
                if(!string.IsNullOrEmpty(txtCurrentInvestPurpose.Text))
                goalProfileSetupVo.CurrInvestementForGoal = double.Parse(txtCurrentInvestPurpose.Text);
                if (!string.IsNullOrEmpty(txtAboveRateOfInterst.Text.Trim()))
                goalProfileSetupVo.ROIEarned = double.Parse(txtAboveRateOfInterst.Text);
                if (!string.IsNullOrEmpty(txtExpRateOfReturn.Text.Trim()))
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
                    GoalSetupBo.CreateCustomerGoalProfileForRetirement(goalProfileSetupVo, ParentCustomerId, 0);

                }
                else
                    GoalSetupBo.CreateCustomerGoalProfile(goalProfileSetupVo, ParentCustomerId, 0);

                // SetPageLoadState(1);
                ////Tab2ControlVisibility(0);

                //int gvRT = BindRTGoalOutputGridView();
                //if (gvRT == 1)
                //{
                //    gvRetirement.Visible = true;
                //    lblTotalText.Visible = true;
                //    lblTotalText.Text = "Total Saving Required Per Month=RS." + hidRTSaveReq.Value;

                //}

                //this.BindGoalOutputGridView(1);
                //TabContainer1.ActiveTabIndex = 1;

                ShowOutPutTab();
                SetPageLoadState(1);
                TabContainer1.ActiveTabIndex = 1;

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



        private void ControlSetVisiblity(int Bool)
        {
            if (Bool == 0)
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






            }
            else
                if (Bool == 1)
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

                    lblHeader.Visible = true;
                    lblNote.Visible = true;
                    //lblReqNote.Visible = true;
                    trRequiedNote.Visible = true;
                    lblHeader.Text = "Goal Profile ";
                    //lblPickCustomer.Text = "Pick a Customer :";
                    lblGoalbjective.Text = "Pick Goal Objective :";
                    lblPickChild.Text = "Select a child for Goal planning :";
                    //lblGoalCostToday.Text = "Goal Cost Today :";

                }
                else
                {//For Edit mode

                }
        }

        private void BtnSetVisiblity(int Bool)
        {
            if (Bool == 0)
            {   //for view selected
                btnCancel.Visible = false;
                btnSaveAdd.Visible = false;
                btnNext.Visible = false;
                btnBackToAddMode.Visible = true;
                btnEdit.Visible = true;
                btnBackToView.Visible = false;
                btnUpdate.Visible = false;
            }
            else
                if (Bool == 1)
                {  //for Add selected 
                    btnCancel.Visible = true;
                    btnSaveAdd.Visible = true;
                    btnNext.Visible = true;
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
                else
                {//For Edit Selected
                    btnBackToView.Visible = true;
                    btnBackToAddMode.Visible = true;
                    btnUpdate.Visible = true;

                    btnCancel.Visible = false;
                    btnSaveAdd.Visible = false;
                    btnNext.Visible = false;
                    btnEdit.Visible = false;
                }


        }

        private void Tab2ControlVisibility(int Bool)
        {
            if (Bool == 0)
            {
                lblHeaderOutPut.Text = "Please pick a customer for viewing the Goal Output";
                gvGoalOutPut.Visible = false;
                gvRetirement.Visible = false;

                Delete.Visible = false;
                Activate.Visible = false;
                Deactive.Visible = false;
                lblTotalText.Visible = false;


            }
            else
            {
                lblHeaderOutPut.Text = "Customer Goal Profile Details";
                gvGoalOutPut.Visible = true;
                gvRetirement.Visible = true;

                Delete.Visible = true;
                Activate.Visible = true;
                Deactive.Visible = false;
                lblTotalText.Visible = true;

            }
        }

        protected void btnBackToAddMode_Click(object sender, EventArgs e)
        {
            //Tab2ControlVisibility(0);
            BtnSetVisiblity(1);
            ControlSetVisiblity(1);
            SetPageLoadState(1);


        }

        protected void ddlActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFilter = (DropDownList)gvGoalOutPut.HeaderRow.FindControl("ddlActiveFilter");
            //ddlFilter.SelectedIndex = 2;
            int ActiveFlag = int.Parse(ddlFilter.SelectedValue);
            BindGoalOutputGridView(ActiveFlag);

            ViewState.Add("FilterValue", ActiveFlag);

            TabContainer1.ActiveTabIndex = 1;



        }

        protected void SetValue(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ViewState["FilterValue"] == null)
            {
                ddl.SelectedValue = "1";
            }
            else
                ddl.SelectedValue = ViewState["FilterValue"].ToString();


        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            BtnSetVisiblity(2);
            ControlSetVisiblity(1);
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
                    goalProfileSetupVo.CurrInvestementForGoal = int.Parse(txtCurrentInvestPurpose.Text.Trim());
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
                ViewState["FilterValue"] = 1;
                ddlGoalType.Enabled = true;

                ShowOutPutTab();
                SetPageLoadState(1);
                BtnSetVisiblity(1);
                TabContainer1.ActiveTabIndex = 1;


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
                ShowGoalDetails(int.Parse(Session["FP_UserID"].ToString()), GoalId);
                ControlSetVisiblity(0);                
                lblGoalbjective.Text = "Goal Objective :";

            }



        }







    }
}
