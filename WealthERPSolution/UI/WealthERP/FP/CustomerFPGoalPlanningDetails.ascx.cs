﻿using System;
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
using BoCommon;
using VoUser;
using BoCustomerGoalProfiling;
using DaoCustomerGoalProfiling;
using VoCustomerGoalProfiling;
using VoCustomerPortfolio;
using System.Collections;

using BoFPSuperlite;
using VoFPSuperlite;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Telerik.Web.UI;


namespace WealthERP.FP
{
    public partial class CustomerFPGoalPlanningDetails : System.Web.UI.UserControl
    {

        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
        GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
        CustomerGoalSetupDao customerGoalSetupDao = new CustomerGoalSetupDao();
        CustomerVo customerVo = new CustomerVo();
        List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();
        CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
        //CustomerGoalPlanningBo customerGoalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo customerGoalPlanningVo = new CustomerGoalPlanningVo();


        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();

        RMVo rmVo = new RMVo();
        DataSet customerGoalDetailsDS;


        double RTSaveReq = 0.0;
        string ActiveFilter = "";
        int activeTabIndex = 0;
        decimal InflationPercent;
        //RMVo rmVo = new RMVo();
        //int ParentcustomerID = 0;
        AdvisorVo advisorVo = new AdvisorVo();
        int AdvisorRMId = 0;

        int investmentTotal = 0;
        int surplusTotal = 0;
        int investedAmountForAllGaol = 0;
        int monthlySavingRequired = 0;
        bool isHavingAssumption = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];

            if (!Page.IsPostBack)
            {
                trNote1.Visible=false;
                trNote2.Visible = false;
                tbl.Visible = false;
                //GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(customerVo.CustomerId, 1, out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);
                //if (GoalProfileList != null)
                //    BindGoalOutputGridView(GoalProfileList);
                //else
                //    trNoRecordFound.Visible = true;
            }

        }

        private int BindGoalOutputGridView(List<GoalProfileSetupVo> goalList)
        {

            DataTable dtGoalProfile = new DataTable();
            DataSet dsExistingInvestment = new DataSet();
            DataSet dsSIPInvestment = new DataSet();
            DataSet dsEQInvestment = new DataSet();

            try
            {
                dtGoalProfile.Columns.Add("GoalID");
                dtGoalProfile.Columns.Add("GoalCode");
                dtGoalProfile.Columns.Add("GoalName");
                dtGoalProfile.Columns.Add("ChildName");
                dtGoalProfile.Columns.Add("CostToday");
                dtGoalProfile.Columns.Add("CurrentInvestment");
                dtGoalProfile.Columns.Add("SavingRequired");
                dtGoalProfile.Columns.Add("GoalAmount");
                dtGoalProfile.Columns.Add("ExpROI");
                dtGoalProfile.Columns.Add("Inflation");
                dtGoalProfile.Columns.Add("ROIEarned");
                dtGoalProfile.Columns.Add("GoalPrifileDate");
                dtGoalProfile.Columns.Add("GoalYear");
                dtGoalProfile.Columns.Add("IsActive");
                dtGoalProfile.Columns.Add("CustomerApprovedOn");

                dtGoalProfile.Columns.Add("CurrentGoalValue");
                //dtGoalProfile.Columns.Add("CompletionPer");
                dtGoalProfile.Columns.Add("SIPAmount");
                dtGoalProfile.Columns.Add("ProjectedValue");
                dtGoalProfile.Columns.Add("ProjectedGapValue");
                dtGoalProfile.Columns.Add("AdditionalSavingReq");
                dtGoalProfile.Columns.Add("IsGoalBehind");
                dtGoalProfile.Columns.Add("LumpsumInvestment");
                dtGoalProfile.Columns.Add("AllocatedAmountToWardsGoal");


                double costTodayTotal = 0;
                double goalAmountTotal = 0;
                double lumpsumInvestmentTotal = 0;
                double monthlySavingReqTotal = 0;
                double allocAmountToWardsGoalTotal = 0;

                double currentGoalValueTotal = 0;
                double sipAmountTotal = 0;
                double projectedValueTotal = 0;
                double additionalSavingReqTotal = 0;
                double projectedGapValueTotal = 0;

                DataRow drGoalProfile;

                for (int i = 0; i < GoalProfileList.Count; i++)
                {

                    double lumpsumInvestment = 0;
                    drGoalProfile = dtGoalProfile.NewRow();
                    goalProfileSetupVo = new GoalProfileSetupVo();
                    goalProfileSetupVo = GoalProfileList[i];
                    drGoalProfile["GoalID"] = goalProfileSetupVo.GoalId.ToString();
                    drGoalProfile["GoalCode"] = goalProfileSetupVo.Goalcode;
                    drGoalProfile["GoalName"] = goalProfileSetupVo.GoalName.ToString();
                    drGoalProfile["ChildName"] = goalProfileSetupVo.ChildName.ToString();
                    if (drGoalProfile["GoalCode"].ToString() == "RT")
                        drGoalProfile["CostToday"] = goalProfileSetupVo.CostOfGoalToday != 0 ? String.Format("{0:n2}", (goalProfileSetupVo.CostOfGoalToday * 12).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    else
                        drGoalProfile["CostToday"] = goalProfileSetupVo.CostOfGoalToday != 0 ? String.Format("{0:n2}", (goalProfileSetupVo.CostOfGoalToday).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    if (goalProfileSetupVo.Goalcode == "RT")
                        costTodayTotal += (goalProfileSetupVo.CostOfGoalToday * 12);
                    else
                        costTodayTotal += goalProfileSetupVo.CostOfGoalToday;
                    drGoalProfile["ExpROI"] = goalProfileSetupVo.ExpectedROI.ToString();
                    drGoalProfile["Inflation"] = goalProfileSetupVo.InflationPercent.ToString();
                    drGoalProfile["ROIEarned"] = goalProfileSetupVo.ROIEarned.ToString();
                    drGoalProfile["CurrentInvestment"] = goalProfileSetupVo.CurrInvestementForGoal != 0 ? String.Format("{0:n2}", goalProfileSetupVo.CurrInvestementForGoal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    allocAmountToWardsGoalTotal += goalProfileSetupVo.CurrInvestementForGoal;
                    drGoalProfile["SavingRequired"] = goalProfileSetupVo.MonthlySavingsReq != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.MonthlySavingsReq.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    monthlySavingReqTotal += goalProfileSetupVo.MonthlySavingsReq;
                    drGoalProfile["GoalAmount"] = goalProfileSetupVo.FutureValueOfCostToday != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.FutureValueOfCostToday.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    goalAmountTotal += goalProfileSetupVo.FutureValueOfCostToday;
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

                    //drGoalProfile["CurrentGoalValue"] = goalProfileSetupVo.CurrentGoalValue.ToString();
                    //drGoalProfile["CompletionPer"] = goalProfileSetupVo.GoalCompletionPercent.ToString();
                    CustomerGoalFundingProgressVo customerGoalFundingProgressVo = new CustomerGoalFundingProgressVo();
                    DataSet dsGoalFundingDetails;
                    customerGoalFundingProgressVo = goalPlanningBo.GetGoalFundingProgressDetails(goalProfileSetupVo.GoalId, customerVo.CustomerId, advisorVo.advisorId, out dsGoalFundingDetails, out dsExistingInvestment, out dsSIPInvestment, out goalPlanningVo, out dsEQInvestment);
                    //lumpsumInvestment = goalPlanningBo.PV(goalProfileSetupVo.ExpectedROI / 100, goalProfileSetupVo.GoalYear - DateTime.Now.Year, 0, -goalProfileSetupVo.FutureValueOfCostToday, 1);
                    drGoalProfile["LumpsumInvestment"] = goalProfileSetupVo.LumpsumInvestRequired != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.LumpsumInvestRequired.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    lumpsumInvestmentTotal += goalProfileSetupVo.LumpsumInvestRequired;
                    if (customerGoalFundingProgressVo != null)
                    {
                        if (goalProfileSetupVo.IsFundFromAsset == true)
                        {
                            drGoalProfile["CurrentGoalValue"] = customerGoalFundingProgressVo.GoalCurrentValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.GoalCurrentValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            currentGoalValueTotal += customerGoalFundingProgressVo.GoalCurrentValue;
                            drGoalProfile["SIPAmount"] = customerGoalFundingProgressVo.MonthlyContribution != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.MonthlyContribution.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            sipAmountTotal += customerGoalFundingProgressVo.MonthlyContribution;
                            drGoalProfile["ProjectedValue"] = customerGoalFundingProgressVo.ProjectedValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.ProjectedValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            projectedValueTotal += customerGoalFundingProgressVo.ProjectedValue;
                            drGoalProfile["AdditionalSavingReq"] = customerGoalFundingProgressVo.AdditionalMonthlyRequirement != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.AdditionalMonthlyRequirement.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            additionalSavingReqTotal += customerGoalFundingProgressVo.AdditionalMonthlyRequirement;
                            //drGoalProfile["ProjectedValue"] = customerGoalFundingProgressVo.ProjectedValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.ProjectedValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["ProjectedGapValue"] = customerGoalFundingProgressVo.ProjectedGapValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", Math.Abs(customerGoalFundingProgressVo.ProjectedGapValue).ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            projectedGapValueTotal += customerGoalFundingProgressVo.ProjectedGapValue;
                            drGoalProfile["AllocatedAmountToWardsGoal"] = customerGoalFundingProgressVo.AmountInvestedTillDate != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", Math.Abs(customerGoalFundingProgressVo.AmountInvestedTillDate).ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            allocAmountToWardsGoalTotal += customerGoalFundingProgressVo.AmountInvestedTillDate;

                            if (customerGoalFundingProgressVo.ProjectedGapValue >= 0)
                                drGoalProfile["IsGoalBehind"] = "No";
                            else if (customerGoalFundingProgressVo.ProjectedGapValue < 0)
                                drGoalProfile["IsGoalBehind"] = "Yes";


                        }
                        else
                        {
                            drGoalProfile["AllocatedAmountToWardsGoal"] = goalProfileSetupVo.CurrInvestementForGoal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", Math.Abs(goalProfileSetupVo.CurrInvestementForGoal).ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            allocAmountToWardsGoalTotal += customerGoalFundingProgressVo.AmountInvestedTillDate;
                            drGoalProfile["CurrentGoalValue"] = "N/A";
                            drGoalProfile["SIPAmount"] = "N/A";
                            drGoalProfile["ProjectedValue"] = "N/A";
                            drGoalProfile["ProjectedGapValue"] = "N/A";
                            drGoalProfile["AdditionalSavingReq"] = "N/A";
                            drGoalProfile["IsGoalBehind"] = "N/A";

                        }
                    }

                    dtGoalProfile.Rows.Add(drGoalProfile);
                }

                gvGoalList.DataSource = dtGoalProfile;
                gvGoalList.DataBind();

                Label lblCostTodayTotal = (Label)gvGoalList.FooterRow.FindControl("lblCostTodayTotal");
                Label lblGoalAmountTotal = (Label)gvGoalList.FooterRow.FindControl("lblGoalAmountTotal");
                Label lblLumpsumInvestmentTotal = (Label)gvGoalList.FooterRow.FindControl("lblLumpsumInvestmentTotal");
                Label lblMonthlySavingReqTotal = (Label)gvGoalList.FooterRow.FindControl("lblMonthlySavingReqTotal");
                Label lblAllocAmountToWardsGoalTotal = (Label)gvGoalList.FooterRow.FindControl("lblAllocAmountToWardsGoalTotal");

                Label lblCurrentGoalValueTotal = (Label)gvGoalList.FooterRow.FindControl("lblCurrentGoalValueTotal");
                Label lblSIPAmountTotal = (Label)gvGoalList.FooterRow.FindControl("lblSIPAmountTotal");
                Label lblProjectedValueTotal = (Label)gvGoalList.FooterRow.FindControl("lblProjectedValueTotal");
                Label lblAdditionalSavingReqTotal = (Label)gvGoalList.FooterRow.FindControl("lblAdditionalSavingReqTotal");
                Label lblProjectedGapValueTotal = (Label)gvGoalList.FooterRow.FindControl("lblProjectedGapValueTotal");


                lblCostTodayTotal.Text = costTodayTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", costTodayTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblGoalAmountTotal.Text = goalAmountTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalAmountTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblLumpsumInvestmentTotal.Text = lumpsumInvestmentTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", lumpsumInvestmentTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblMonthlySavingReqTotal.Text = monthlySavingReqTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", monthlySavingReqTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblAllocAmountToWardsGoalTotal.Text = allocAmountToWardsGoalTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", allocAmountToWardsGoalTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblAllocAmountToWardsGoalTotal.Text = allocAmountToWardsGoalTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", allocAmountToWardsGoalTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblCurrentGoalValueTotal.Text = currentGoalValueTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", currentGoalValueTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblSIPAmountTotal.Text = sipAmountTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", sipAmountTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblProjectedValueTotal.Text = projectedValueTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", projectedValueTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblAdditionalSavingReqTotal.Text = additionalSavingReqTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", additionalSavingReqTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblAdditionalSavingReqTotal.Text = additionalSavingReqTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", additionalSavingReqTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                lblProjectedGapValueTotal.Text = projectedGapValueTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", Math.Abs(projectedGapValueTotal).ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";

                return 1;



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

        protected void gvGoalList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Image imgGoalFundIndicator = e.Row.FindControl("imgGoalFundGap") as Image;
                Label lblGoalFundPercentage = e.Row.FindControl("lblIsGoalGap") as Label;


                if (lblGoalFundPercentage.Text.Trim() == "Yes")
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/GoalDown.png";
                }
                else if (lblGoalFundPercentage.Text.Trim() == "No")
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/GoalUP.png";

                }
                else if (lblGoalFundPercentage.Text.Trim() == "N/A")
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/NotApplicable.png";

                }
                lblGoalFundPercentage.Visible = false;

                Image imgGoalImage = e.Row.FindControl("imgGoalImage") as Image;
                Label lblGoalCode = e.Row.FindControl("lblGoalName") as Label;

                if (lblGoalCode.Text.Trim() == "BH")
                {
                    imgGoalImage.ImageUrl = "~/Images/HomeGoal.png";
                    imgGoalImage.ToolTip = "Buy Home";
                }
                else if (lblGoalCode.Text.Trim() == "ED")
                {
                    imgGoalImage.ImageUrl = "~/Images/EducationGoal.png";
                    imgGoalImage.ToolTip = "Children Education";
                }
                else if (lblGoalCode.Text.Trim() == "MR")
                {
                    imgGoalImage.ToolTip = "Children Marriage";
                    imgGoalImage.ImageUrl = "~/Images/ChildMarraiageGoal.png";
                }
                else if (lblGoalCode.Text.Trim() == "OT")
                {
                    imgGoalImage.ToolTip = "Other";
                    imgGoalImage.ImageUrl = "~/Images/OtherGoal.png";
                }
                else if (lblGoalCode.Text.Trim() == "RT")
                {
                    imgGoalImage.ToolTip = "Retirement";
                    imgGoalImage.ImageUrl = "~/Images/RetirementGoal.png";
                }
                lblGoalCode.Visible = false;

            }
        }






        //    public void BindCustomerGoalDetailGrid(DataSet customerGoalDetailsDS)
        //    {
        //        decimal equityFundAmount = 0;
        //        decimal debtFundAmount = 0;
        //        decimal cashFundAmount = 0;
        //        decimal alternateFundAmount = 0;
        //        decimal totalFundAmount = 0;
        //        decimal gapAmountAfterFund = 0;
        //        decimal goalAmountRequired = 0;
        //        decimal goalFundPercentage = 0;
        //        decimal goalAmountInGoalYear=0;
        //        decimal goalCostToday = 0;
        //        int goalId = 0;
        //        decimal equityFunded = 0;
        //        decimal debtFunded = 0;
        //        decimal cashFunded = 0;
        //        decimal alternateFunded = 0;
        //        decimal equityTotalAvl = 0;
        //        decimal debtTotalAvl = 0;
        //        decimal cashTotalAvl = 0;
        //        decimal alternateTotalAvl = 0;
        //        DataTable dtCustomerGoalDetails = new DataTable();
        //        dtCustomerGoalDetails.Columns.Add("GoalId");
        //        dtCustomerGoalDetails.Columns.Add("GoalCategory");
        //        dtCustomerGoalDetails.Columns.Add("GoalType");
        //        dtCustomerGoalDetails.Columns.Add("ChildName");
        //        dtCustomerGoalDetails.Columns.Add("CostToday");
        //        dtCustomerGoalDetails.Columns.Add("GaolYear");

        //        dtCustomerGoalDetails.Columns.Add("GoalAmountInGoalYear");
        //        dtCustomerGoalDetails.Columns.Add("CorpusLeftBehind");
        //        dtCustomerGoalDetails.Columns.Add("GoalPriority");


        //        dtCustomerGoalDetails.Columns.Add("EquityFundedAmount");
        //        dtCustomerGoalDetails.Columns.Add("DebtFundedAmount");
        //        dtCustomerGoalDetails.Columns.Add("CashFundedAmount");
        //        dtCustomerGoalDetails.Columns.Add("AlternateFundedAmount");

        //        dtCustomerGoalDetails.Columns.Add("TotalFundedAmount");
        //        dtCustomerGoalDetails.Columns.Add("GoalFundedGap");
        //        dtCustomerGoalDetails.Columns.Add("GoalFundPercentage");
        //        dtCustomerGoalDetails.Columns.Add("GoalFundedType");
        //        dtCustomerGoalDetails.Columns.Add("IsEquityDeficient");
        //        dtCustomerGoalDetails.Columns.Add("IsDebtDeficient");
        //        dtCustomerGoalDetails.Columns.Add("IsCashDeficient");
        //        dtCustomerGoalDetails.Columns.Add("IsAlternateDeficient");


        //        DataRow drCustomerGoalDetails;
        //        DataRow[] drGoalFundDetails;
        //        int tempYear = 0;
        //        DataRow[] drgoalYearWise;
        //        DataRow[] drgoalIdWise;
        //        DataSet dsRebalancing = new DataSet();
        //        DataRow[] drAssetDetails;
        //        dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);

        //        foreach (DataRow dr in customerGoalDetailsDS.Tables[0].Rows)
        //        {
        //            drCustomerGoalDetails = dtCustomerGoalDetails.NewRow();
        //            if (tempYear != int.Parse(dr["CG_GoalYear"].ToString()))
        //            {
        //                tempYear = int.Parse(dr["CG_GoalYear"].ToString());
        //                //drFinalAssumption["Year"] = tempYear.ToString();
        //                drgoalYearWise = customerGoalDetailsDS.Tables[0].Select("CG_GoalYear=" + tempYear.ToString());
        //                foreach (DataRow drgoalYear in drgoalYearWise)
        //                {
        //                    goalId = int.Parse(drgoalYear["CG_GoalId"].ToString());
        //                    drgoalIdWise = customerGoalDetailsDS.Tables[1].Select("CG_GoalId=" + goalId.ToString());
        //                    if (drgoalIdWise.Count() > 0)
        //                    {
        //                        foreach (DataRow drgoalId in drgoalIdWise)
        //                        {
        //                            if (int.Parse(drgoalId["WAC_AssetClassificationCode"].ToString()) == 1)
        //                            {
        //                                equityFunded += decimal.Parse(drgoalId["CGF_AllocatedAmount"].ToString());
        //                            }
        //                            if (int.Parse(drgoalId["WAC_AssetClassificationCode"].ToString()) == 2)
        //                            {
        //                                debtFunded += decimal.Parse(drgoalId["CGF_AllocatedAmount"].ToString());
        //                            }
        //                            if (int.Parse(drgoalId["WAC_AssetClassificationCode"].ToString()) == 3)
        //                            {
        //                                cashFunded += decimal.Parse(drgoalId["CGF_AllocatedAmount"].ToString());
        //                            }
        //                            if (int.Parse(drgoalId["WAC_AssetClassificationCode"].ToString()) == 4)
        //                            {
        //                                alternateFunded += decimal.Parse(drgoalId["CGF_AllocatedAmount"].ToString());
        //                            }

        //                        }

        //                    }
        //                    else
        //                    {
        //                        equityFunded = 0;
        //                        debtFunded = 0;
        //                        cashFunded = 0;
        //                        alternateFunded = 0;
        //                    }


        //                    drAssetDetails = dsRebalancing.Tables[1].Select("Year=" + tempYear.ToString());
        //                    if (drAssetDetails.Count() > 0)
        //                    {
        //                        equityTotalAvl = decimal.Parse(drAssetDetails[0]["PreviousYearClosingBalance"].ToString());
        //                        debtTotalAvl = decimal.Parse(drAssetDetails[1]["PreviousYearClosingBalance"].ToString());
        //                        cashTotalAvl = decimal.Parse(drAssetDetails[2]["PreviousYearClosingBalance"].ToString());
        //                        alternateTotalAvl = decimal.Parse(drAssetDetails[3]["PreviousYearClosingBalance"].ToString());
        //                    }

        //                }


        //            }


        //            if (equityFunded > equityTotalAvl)
        //            {
        //                drCustomerGoalDetails["IsEquityDeficient"] = 1;
        //            }
        //            else
        //                drCustomerGoalDetails["IsEquityDeficient"] = 0;
        //            if (debtFunded > debtTotalAvl)
        //            {
        //                drCustomerGoalDetails["IsDebtDeficient"] = 1;

        //            }
        //            else
        //                drCustomerGoalDetails["IsDebtDeficient"] = 0;
        //            if (cashFunded > cashTotalAvl)
        //            {
        //                drCustomerGoalDetails["IsCashDeficient"] = 1;
        //            }
        //            else
        //                drCustomerGoalDetails["IsCashDeficient"] = 0;
        //            if (alternateTotalAvl < alternateFunded)
        //            {
        //                drCustomerGoalDetails["IsAlternateDeficient"] = 1;
        //            }
        //            else
        //                drCustomerGoalDetails["IsAlternateDeficient"] = 0;




        //            drCustomerGoalDetails["GoalId"] = dr["CG_GoalId"].ToString();

        //            if (dr["XG_GoalCode"].ToString() == "RT")
        //                 drCustomerGoalDetails["GoalCategory"] = "RT";
        //            else
        //                 drCustomerGoalDetails["GoalCategory"] = "NonRT";

        //            drCustomerGoalDetails["GoalType"] = dr["XG_GoalName"].ToString();
        //            drCustomerGoalDetails["ChildName"] = dr["ChildName"].ToString();
        //            goalCostToday =Math.Round(decimal.Parse(dr["CG_CostToday"].ToString()),0);
        //            drCustomerGoalDetails["CostToday"] = String.Format("{0:n2}", goalCostToday.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["GaolYear"] = dr["CG_GoalYear"].ToString();
        //            goalAmountInGoalYear = Math.Round(Decimal.Parse(dr["CG_FVofCostToday"].ToString()), 0);
        //            drCustomerGoalDetails["GoalAmountInGoalYear"] = String.Format("{0:n2}", goalAmountInGoalYear.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["GoalPriority"] = dr["CG_Priority"].ToString();

        //            drGoalFundDetails = customerGoalDetailsDS.Tables[1].Select("CG_GoalId=" + dr["CG_GoalId"].ToString());
        //            if (dr["XG_GoalCode"].ToString() == "RT")
        //            {
        //                if (dr["CG_CorpusLeftBehind"].ToString() == "")
        //                {
        //                    drCustomerGoalDetails["CorpusLeftBehind"] = 0;
        //                }
        //                else if(dr["CG_CorpusLeftBehind"].ToString() == "0.0000")
        //                {
        //                    drCustomerGoalDetails["CorpusLeftBehind"] = 0;
        //                }
        //                else
        //                {
        //                    decimal corpusLeft = Math.Round(decimal.Parse(dr["CG_CorpusLeftBehind"].ToString()),0);

        //                    drCustomerGoalDetails["CorpusLeftBehind"] = String.Format("{0:n2}",corpusLeft.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //                }
        //            }
        //            else
        //            {
        //                drCustomerGoalDetails["CorpusLeftBehind"] = "-";
        //            }
        //            if (drGoalFundDetails.Count() > 0)
        //            {
        //                equityFundAmount = decimal.Parse((drGoalFundDetails[0]["CGF_AllocatedAmount"].ToString()));
        //                debtFundAmount = decimal.Parse(drGoalFundDetails[1]["CGF_AllocatedAmount"].ToString());
        //                cashFundAmount = decimal.Parse(drGoalFundDetails[2]["CGF_AllocatedAmount"].ToString());
        //                if (drGoalFundDetails[2] != null)
        //                    alternateFundAmount = decimal.Parse(drGoalFundDetails[3]["CGF_AllocatedAmount"].ToString());
        //                else
        //                    alternateFundAmount = 0;
        //                totalFundAmount = equityFundAmount + debtFundAmount + cashFundAmount + alternateFundAmount;
        //                goalFundPercentage = Math.Round(((totalFundAmount / goalAmountInGoalYear) * 100), 0);

        //            }
        //            drCustomerGoalDetails["GoalFundPercentage"] = goalFundPercentage;
        //            drCustomerGoalDetails["EquityFundedAmount"] = String.Format("{0:n2}", equityFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["DebtFundedAmount"] = String.Format("{0:n2}", debtFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["CashFundedAmount"] = String.Format("{0:n2}", cashFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["AlternateFundedAmount"] = String.Format("{0:n2}", alternateFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            drCustomerGoalDetails["TotalFundedAmount"] = String.Format("{0:n2}", totalFundAmount.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            goalAmountRequired = decimal.Parse(dr["CG_FVofCostToday"].ToString());
        //            gapAmountAfterFund=goalAmountRequired-totalFundAmount;
        //            drCustomerGoalDetails["GoalFundedGap"] = String.Format("{0:n2}", gapAmountAfterFund.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
        //            if (totalFundAmount >= goalAmountRequired)
        //            {
        //                drCustomerGoalDetails["GoalFundedType"] = "FULL";
        //            }
        //            dtCustomerGoalDetails.Rows.Add(drCustomerGoalDetails);
        //            equityFundAmount = 0;
        //            debtFundAmount = 0;
        //            cashFundAmount = 0;
        //            alternateFundAmount = 0;
        //            totalFundAmount = 0;
        //            gapAmountAfterFund = 0;

        //        }

        //        gvrGoalPlanning.DataSource = dtCustomerGoalDetails;
        //        gvrGoalPlanning.DataBind();
        //        int customerId = customerVo.CustomerId;
        //        DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerId);
        //         int count = dsGoalList.Tables[1].Rows.Count;
        //         if (count == 3)
        //         {
        //             gvrGoalPlanning.Columns[11].Visible = false;
        //         }
        //         else
        //             gvrGoalPlanning.Columns[11].Visible = true;

        //    }

        //    protected void gvrGoalPlanning_RowDataBound(object sender, GridViewRowEventArgs e)
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            Image imgNotFunded = e.Row.FindControl("imgNotFunded") as Image;
        //            Label lblgoalFundPercentage = e.Row.FindControl("lblGoalFundPercentage") as Label;
        //            Label lblIsEquityDeficient = e.Row.FindControl("lblIsEquityDeficient") as Label;
        //            Label lblIsDebtDeficient = e.Row.FindControl("lblIsDebtDeficient") as Label;
        //            Label lblIsCashDeficient = e.Row.FindControl("lblIsCashDeficient") as Label;
        //            Label lblIsAlternateDeficient = e.Row.FindControl("lblIsAlternateDeficient") as Label;


        //            int isEquityDeficient = 0;
        //            int isDebtDeficient = 0;
        //            int isCashDeficient = 0;
        //            int isAlternateDeficient = 0;
        //            int goalFundPercentage = 0;
        //            goalFundPercentage = int.Parse(lblgoalFundPercentage.Text.ToString());
        //            isEquityDeficient = int.Parse(lblIsEquityDeficient.Text.ToString());
        //            isDebtDeficient = int.Parse(lblIsDebtDeficient.Text.ToString());
        //            isCashDeficient = int.Parse(lblIsCashDeficient.Text.ToString());
        //            isAlternateDeficient = int.Parse(lblIsAlternateDeficient.Text.ToString());

        //            if (goalFundPercentage < 25)
        //            {
        //                imgNotFunded.ImageUrl = "~/Images/cross.jpg";
        //            }
        //            else if (goalFundPercentage >= 25 || goalFundPercentage <= 99)
        //            {
        //                imgNotFunded.ImageUrl = "~/Images/check.jpg";
        //            }
        //            else if (goalFundPercentage > 99)
        //            {
        //                imgNotFunded.ImageUrl = "~/Images/help.jpg";
        //            }

        //            if (isEquityDeficient == 1)
        //            {
        //                e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
        //            }
        //            if (isDebtDeficient == 1)
        //            {
        //                e.Row.Cells[9].BackColor = System.Drawing.Color.Red;
        //            }
        //            if (isCashDeficient == 1)
        //            {
        //                e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
        //            }
        //            if (isAlternateDeficient == 1)
        //            {
        //                e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
        //            }
        //        }

        //    }


        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            RadComboBox ddlAction = null;
            GridViewRow gvGoal = null;
            int selectedRow = 0;
            string goalId = "";
            string goalCatagory = "";
            string goalAction = "";
            string goalCode = string.Empty;
            bool isGoalFundFromAsset = false;

            ddlAction = (RadComboBox)sender;
            gvGoal = (GridViewRow)ddlAction.NamingContainer;
            selectedRow = gvGoal.RowIndex;
            goalId = gvGoalList.DataKeys[selectedRow].Values["GoalId"].ToString();
            goalCode = gvGoalList.DataKeys[selectedRow].Values["GoalCode"].ToString();
            if (gvGoalList.DataKeys[selectedRow].Values["IsGoalBehind"].ToString() != "N/A")
            {
                isGoalFundFromAsset = true;
            }
            hdndeleteId.Value = goalId;
            //goalCatagory = gvGoalList.DataKeys[selectedRow].Values["GoalCategory"].ToString();
            goalAction = ddlAction.SelectedValue.ToString();
            if (ddlAction.SelectedValue == "View" || ddlAction.SelectedValue == "Edit")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerAdvancedGoalSetup','?GoalId=" + goalId + "&GoalAction=" + goalAction + "');", true);
            }
            else if (ddlAction.SelectedValue == "Fund")
            {
                if (isGoalFundFromAsset == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Goal is not marked for MF Based Goal Planning.Edit the goal and mark it first');", true);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('CustomerFPGoalFundingProgress','?GoalId=" + goalId + "');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerAdvancedGoalSetup','?GoalId=" + goalId + "&GoalAction=" + goalAction + "');", true);
                }
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
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalPlanningDetails','login');", true);
                trDeleteSuccess.Visible = true;

                GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(customerVo.CustomerId, 1, out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);
                if (GoalProfileList != null)
                {
                    if (hdnGoalType.Value == "Advanced")
                    {
                        BindGoalOutputGridView(GoalProfileList);
                    }
                    else
                    {
                        BindGoalOutputStandardRadGrid(GoalProfileList);
                    }
                }
                else
                {

                    trNoRecordFound.Visible = true;
                    gvGoalList.Visible = false;
                }


            }
        }
        protected void ddlActionGoalType_OnSelectedIndexChange(object sender, EventArgs e)
        {
            RadComboBox ddlAction = (RadComboBox)sender;

                   }

        private void BindGoalOutputStandardRadGrid(List<GoalProfileSetupVo> goalList)
        {

            DataTable dtGoalProfile = new DataTable();
            DataSet dsExistingInvestment = new DataSet();
            DataSet dsSIPInvestment = new DataSet();
            DataSet dsEQInvestment = new DataSet();

                dtGoalProfile.Columns.Add("GoalID");
                dtGoalProfile.Columns.Add("GoalCode");
                dtGoalProfile.Columns.Add("GoalName");
                dtGoalProfile.Columns.Add("ChildName");
                dtGoalProfile.Columns.Add("CostToday",typeof(double));
                dtGoalProfile.Columns.Add("CurrentInvestment");
                dtGoalProfile.Columns.Add("SavingRequired", typeof(double));
                dtGoalProfile.Columns.Add("GoalAmount",typeof(double));
                dtGoalProfile.Columns.Add("ExpROI");
                dtGoalProfile.Columns.Add("Inflation");
                dtGoalProfile.Columns.Add("ROIEarned");
                dtGoalProfile.Columns.Add("GoalPrifileDate");
                dtGoalProfile.Columns.Add("GoalYear");
                dtGoalProfile.Columns.Add("IsActive");

                dtGoalProfile.Columns.Add("LumpsumInvestment", typeof(double));
                dtGoalProfile.Columns.Add("IsGoalBehind");
            
                double costTodayTotal = 0;
                double goalAmountTotal = 0;
                double monthlySavingReqTotal = 0;
                double allocAmountToWardsGoalTotal = 0;


                DataRow drGoalProfile;

                for (int i = 0; i < GoalProfileList.Count; i++)
                {

                    drGoalProfile = dtGoalProfile.NewRow();
                    goalProfileSetupVo = new GoalProfileSetupVo();
                    goalProfileSetupVo = GoalProfileList[i];
                    drGoalProfile["GoalID"] = goalProfileSetupVo.GoalId.ToString();
                    drGoalProfile["GoalCode"] = goalProfileSetupVo.Goalcode;
                    drGoalProfile["GoalName"] = goalProfileSetupVo.GoalName.ToString();
                    drGoalProfile["ChildName"] = goalProfileSetupVo.ChildName.ToString();
                    if (drGoalProfile["GoalCode"].ToString() == "RT")
                        drGoalProfile["CostToday"] = goalProfileSetupVo.CostOfGoalToday != 0 ? String.Format("{0:n2}", (goalProfileSetupVo.CostOfGoalToday * 12).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    else
                        drGoalProfile["CostToday"] = goalProfileSetupVo.CostOfGoalToday != 0 ? String.Format("{0:n2}", (goalProfileSetupVo.CostOfGoalToday).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    if (goalProfileSetupVo.Goalcode == "RT")
                        costTodayTotal += (goalProfileSetupVo.CostOfGoalToday * 12);
                    else
                        costTodayTotal += goalProfileSetupVo.CostOfGoalToday;
                    drGoalProfile["ExpROI"] = goalProfileSetupVo.ExpectedROI.ToString();
                    drGoalProfile["Inflation"] = goalProfileSetupVo.InflationPercent.ToString();
                    drGoalProfile["ROIEarned"] = goalProfileSetupVo.ROIEarned.ToString();
                    drGoalProfile["CurrentInvestment"] = goalProfileSetupVo.CurrInvestementForGoal != 0 ? String.Format("{0:n2}", goalProfileSetupVo.CurrInvestementForGoal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                    allocAmountToWardsGoalTotal += goalProfileSetupVo.CurrInvestementForGoal;
                    drGoalProfile["SavingRequired"] = goalProfileSetupVo.MonthlySavingsReq != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.MonthlySavingsReq.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    monthlySavingReqTotal += goalProfileSetupVo.MonthlySavingsReq;
                    drGoalProfile["GoalAmount"] = goalProfileSetupVo.FutureValueOfCostToday != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.FutureValueOfCostToday.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    goalAmountTotal += goalProfileSetupVo.FutureValueOfCostToday;
                    drGoalProfile["GoalPrifileDate"] = goalProfileSetupVo.GoalProfileDate.ToShortDateString();
                    drGoalProfile["GoalYear"] = goalProfileSetupVo.GoalYear.ToString();
                    if (goalProfileSetupVo.IsActice == 0)
                    {
                        drGoalProfile["IsActive"] = "Inactive";

                    }
                    else
                        drGoalProfile["IsActive"] = "Active";


                   
                    if (goalProfileSetupVo.IsFundFromAsset == true)
                    {
                        drGoalProfile["IsGoalBehind"] = "";
                    }
                    else
                    {
                        drGoalProfile["IsGoalBehind"] = "N/A";
                    }
                    drGoalProfile["LumpsumInvestment"] = goalProfileSetupVo.LumpsumInvestRequired;
                    dtGoalProfile.Rows.Add(drGoalProfile);
                }


                gvStandardGoaldetails.DataSource = dtGoalProfile;
                gvStandardGoaldetails.DataBind();
                Cache[customerVo.CustomerId + "dtGoalProfile"] = dtGoalProfile;
            }
        protected void gvStandardGoaldetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache[customerVo.CustomerId + "dtGoalProfile"];
            gvStandardGoaldetails.DataSource = dt;
        }
        public void gvStandardGoaldetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (e.Item as GridDataItem);
                RadComboBox rcb = new RadComboBox();
                TemplateColumn tm = new TemplateColumn();
                Label lblGoalCode = new Label();
                Image imgGoalImage = e.Item.FindControl("imgGoalImage") as Image;

                lblGoalCode = (Label)e.Item.FindControl("lblGoalName");

                if (lblGoalCode.Text.Trim() == "BH")
                {
                    imgGoalImage.ImageUrl = "~/Images/HomeGoal.png";
                    imgGoalImage.ToolTip = "Buy Home";
                }
                else if (lblGoalCode.Text.Trim() == "ED")
                {
                    imgGoalImage.ImageUrl = "~/Images/EducationGoal.png";
                    imgGoalImage.ToolTip = "Children Education";
                }
                else if (lblGoalCode.Text.Trim() == "MR")
                {
                    imgGoalImage.ToolTip = "Children Marriage";
                    imgGoalImage.ImageUrl = "~/Images/ChildMarraiageGoal.png";
                }
                else if (lblGoalCode.Text.Trim() == "OT")
                {
                    imgGoalImage.ToolTip = "Other";
                    imgGoalImage.ImageUrl = "~/Images/OtherGoal.png";
                }
                else if (lblGoalCode.Text.Trim() == "RT")
                {
                    imgGoalImage.ToolTip = "Retirement";                 
                    imgGoalImage.ImageUrl = "~/Images/RetirementGoal.png";
                }
                lblGoalCode.Visible = false;
            }
        }
        protected void ddlActionSteps_OnSelectedIndexChange(object sender, EventArgs e)
        {
            GridViewRow gvGoal = null;
            int selectedRow = 0;
            string goalId = "";
            string goalCatagory = "";
            string goalAction = "";
            string goalCode = string.Empty;
            bool isGoalFundFromAsset = false;

            RadComboBox ddlAction = (RadComboBox)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            
            goalId = gvStandardGoaldetails.MasterTableView.DataKeyValues[gvr.ItemIndex]["GoalId"].ToString();
            goalCode = gvStandardGoaldetails.MasterTableView.DataKeyValues[gvr.ItemIndex]["GoalCode"].ToString();
            string IsGoalBehind = gvStandardGoaldetails.MasterTableView.DataKeyValues[gvr.ItemIndex]["IsGoalBehind"].ToString();
            if (IsGoalBehind != "N/A")
            {
                isGoalFundFromAsset = true;
            }
            hdndeleteId.Value = goalId;
            //goalCatagory = gvGoalList.DataKeys[selectedRow].Values["GoalCategory"].ToString();
            goalAction = ddlAction.SelectedValue.ToString();
            if (ddlAction.SelectedValue == "View" || ddlAction.SelectedValue == "Edit")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerAdvancedGoalSetup','?GoalId=" + goalId + "&GoalAction=" + goalAction + "');", true);
            }
            else if (ddlAction.SelectedValue == "Fund")
            {
                if (isGoalFundFromAsset == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Goal is not marked for MF Based Goal Planning.Edit the goal and mark it first');", true);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('CustomerFPGoalFundingProgress','?GoalId=" + goalId + "');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerAdvancedGoalSetup','?GoalId=" + goalId + "&GoalAction=" + goalAction + "');", true);
                }
            }
            else if (ddlAction.SelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            }

        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            if (ddlActionGoalType.SelectedValue == "Standard")
            {
                hdnGoalType.Value = "Standard";
                tbl.Visible = false;
                Panel2.Visible = true;

                GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(customerVo.CustomerId, 1, out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);
                if (GoalProfileList != null)
                {
                    BindGoalOutputStandardRadGrid(GoalProfileList);
                    imgBtnStandardGoalList.Visible = true;
                    trNote1.Visible = true;
                    trNote2.Visible = true;
                }
                else
                {
                    trNoRecordFound.Visible = true;
                    trNote1.Visible = false;
                    trNote2.Visible = false;
                }
            }
            else if (ddlActionGoalType.SelectedValue == "Advanced")
            {
                hdnGoalType.Value = "Advanced";
                tbl.Visible = true;
                Panel2.Visible = false;
                GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(customerVo.CustomerId, 1, out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);
                if (GoalProfileList != null)
                {
                    imgBtnStandardGoalList.Visible = false;
                    trNote1.Visible = true;
                    trNote2.Visible = true;
                    BindGoalOutputGridView(GoalProfileList);
                }
                else
                {
                    trNote1.Visible = false;
                    trNote2.Visible = false;
                    trNoRecordFound.Visible = true;
                    tbl.Visible = false;
                }
            }


        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
           DataTable dtGoalProfile=  (DataTable)Cache[customerVo.CustomerId + "dtGoalProfile"];
           foreach (DataRow dr in dtGoalProfile.Rows)
           {    
               if (dr["GoalCode"].ToString() == "MR")
               {
                   dr["GoalCode"] = "Children Marriage";
               }
               else if (dr["GoalCode"].ToString() == "RT")
               {
                   dr["GoalCode"] = "Retirement";
               }

               else if (dr["GoalCode"].ToString() == "OT")
               {
                   dr["GoalCode"] = "Other";
               }

               else if (dr["GoalCode"].ToString() == "ED")
               {
                   dr["GoalCode"] = "Children Education";
               }

               else if (dr["GoalCode"].ToString() == "BH")
               {
                   dr["GoalCode"] = "Buy Home";
               }


           }
           gvStandardGoaldetails.DataSource = dtGoalProfile;
            //if(dtGoalProfile.Columns[
            gvStandardGoaldetails.ExportSettings.OpenInNewWindow = true;
            gvStandardGoaldetails.ExportSettings.IgnorePaging = true;
            gvStandardGoaldetails.ExportSettings.HideStructureColumns = true;
            gvStandardGoaldetails.ExportSettings.ExportOnlyData = true;
            gvStandardGoaldetails.ExportSettings.FileName = "Standard Goal Details";
            gvStandardGoaldetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvStandardGoaldetails.MasterTableView.ExportToExcel();
        }

    }
}