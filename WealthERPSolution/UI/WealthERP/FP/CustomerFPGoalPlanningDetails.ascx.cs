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
            customerVo=(CustomerVo)Session["customerVo"];

            if (!Page.IsPostBack)
            {
                GoalProfileList = GoalSetupBo.GetCustomerGoalProfile(customerVo.CustomerId, 1, out investmentTotal, out surplusTotal, out investedAmountForAllGaol, out monthlySavingRequired);
                if (GoalProfileList.Count > 0)
                    BindGoalOutputGridView(GoalProfileList);
                else
                    trNoRecordFound.Visible = false;
            }
           
        }

        private int BindGoalOutputGridView(List<GoalProfileSetupVo> goalList)
        {

             DataTable dtGoalProfile = new DataTable();

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
                        drGoalProfile["GoalCode"] = goalProfileSetupVo.Goalcode;
                        drGoalProfile["GoalName"] = goalProfileSetupVo.GoalName.ToString();
                        drGoalProfile["ChildName"] = goalProfileSetupVo.ChildName.ToString();
                        drGoalProfile["CostToday"] = goalProfileSetupVo.CostOfGoalToday != 0 ? String.Format("{0:n2}", goalProfileSetupVo.CostOfGoalToday.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                        allGoalCostToday += goalProfileSetupVo.CostOfGoalToday;
                        drGoalProfile["CurrentInvestment"] = goalProfileSetupVo.CurrInvestementForGoal != 0 ? String.Format("{0:n2}", goalProfileSetupVo.CurrInvestementForGoal.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))) : "0";
                        currentInvestmentTotal += goalProfileSetupVo.CurrInvestementForGoal;
                        drGoalProfile["SavingRequired"] = goalProfileSetupVo.MonthlySavingsReq != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.MonthlySavingsReq.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                        monthlySaveRequired += goalProfileSetupVo.MonthlySavingsReq;
                        drGoalProfile["GoalAmount"] = goalProfileSetupVo.FutureValueOfCostToday != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", goalProfileSetupVo.FutureValueOfCostToday.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
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

                        //drGoalProfile["CurrentGoalValue"] = goalProfileSetupVo.CurrentGoalValue.ToString();
                        //drGoalProfile["CompletionPer"] = goalProfileSetupVo.GoalCompletionPercent.ToString();
                        CustomerGoalFundingProgressVo customerGoalFundingProgressVo = new CustomerGoalFundingProgressVo();
                        DataSet dsGoalFundingDetails;
                        customerGoalFundingProgressVo = goalPlanningBo.GetGoalFundingProgressDetails(goalProfileSetupVo.GoalId,customerVo.CustomerId,advisorVo.advisorId,out dsGoalFundingDetails);
                       
                        if (customerGoalFundingProgressVo!=null)
                        {
                            drGoalProfile["CurrentGoalValue"]=customerGoalFundingProgressVo.GoalCurrentValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.GoalCurrentValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["SIPAmount"]=customerGoalFundingProgressVo.MonthlyContribution != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.MonthlyContribution.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["ProjectedValue"]=customerGoalFundingProgressVo.ProjectedValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.ProjectedValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["AdditionalSavingReq"] = customerGoalFundingProgressVo.AdditionalMonthlyRequirement != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.AdditionalMonthlyRequirement.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["ProjectedValue"] = customerGoalFundingProgressVo.ProjectedValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.ProjectedValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            drGoalProfile["ProjectedGapValue"] = customerGoalFundingProgressVo.ProjectedGapValue != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerGoalFundingProgressVo.ProjectedGapValue.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                            if (customerGoalFundingProgressVo.ProjectedGapValue > 0)
                                drGoalProfile["IsGoalBehind"] = "No";
                            else
                                drGoalProfile["IsGoalBehind"] = "Yes";

                        }
                        else
                        {
                            drGoalProfile["CurrentGoalValue"]="N/A";
                            drGoalProfile["SIPAmount"] = "N/A";
                            drGoalProfile["ProjectedValue"] = "N/A";
                            drGoalProfile["ProjectedGapValue"] = "N/A";
                            drGoalProfile["AdditionalSavingReq"] = "N/A";
                            drGoalProfile["IsGoalBehind"] = "NA";
 
                        }

                        

                        dtGoalProfile.Rows.Add(drGoalProfile);
                    }

                    gvGoalList.DataSource = dtGoalProfile;
                    gvGoalList.DataBind();

                    Label lblAllGoalCostTotal = (Label)gvGoalList.FooterRow.FindControl("lblCostTodayTotal");
                    lblAllGoalCostTotal.Text = Math.Round(double.Parse(allGoalCostToday.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    //Label lblCurrentInvestmentTotal = (Label)gvGoalList.FooterRow.FindControl("lblCurrentInvestmentTotal");
                    //lblCurrentInvestmentTotal.Text = Math.Round(double.Parse(currentInvestmentTotal.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    //Label lblMonthlyTotal = (Label)gvGoalList.FooterRow.FindControl("lblMonthlySavingTotal");
                    //lblMonthlyTotal.Text = Math.Round(double.Parse(monthlySaveRequired.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    //Label lblGoalCostInGoalYear = (Label)gvGoalList.FooterRow.FindControl("lblGoalAmountTotal");
                    //lblGoalCostInGoalYear.Text = Math.Round(double.Parse(allGoalCostInGoalYear.ToString()), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));


                    if (!String.IsNullOrEmpty(hidRTSaveReq.Value))
                    {
                        TotalSaveReq = monthlySaveRequired + double.Parse(hidRTSaveReq.Value);
                    }
                    else
                        TotalSaveReq = monthlySaveRequired;

                    //lblHeaderOutPut.Text = "Customer Goal Profile Details";
                    //lblTotalText.Visible = true;
                    //LabelNote.Visible = true;

                    //Label lblActiveMsg = (Label)gvGoalList.HeaderRow.FindControl("ActiveMessage");
                    //lblActiveMsg.Visible = false;
                    //lblTotalText.Text = "Total Saving Required Every Month=Rs." + Math.Round(TotalSaveReq, 0).ToString();
                    //lblOtherGoalParagraph.Text = GoalSetupBo.OtherGoalDescriptionText(int.Parse(Session["FP_UserID"].ToString()));


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
                else if (lblGoalFundPercentage.Text.Trim() == "NA")
                {
                    imgGoalFundIndicator.ImageUrl = "~/Images/NotApplicable.png";
 
                }
                lblGoalFundPercentage.Visible = false;

                Image imgGoalImage = e.Row.FindControl("imgGoalImage") as Image;
                Label lblGoalCode = e.Row.FindControl("lblGoalName") as Label;

                if (lblGoalCode.Text.Trim() == "BH")
                {
                    imgGoalImage.ImageUrl = "~/Images/HomeGoal.png";
                }
                else if (lblGoalCode.Text.Trim() == "ED")
                {
                    imgGoalImage.ImageUrl = "~/Images/EducationGoal.png";
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

            ddlAction = (RadComboBox)sender;
            gvGoal = (GridViewRow)ddlAction.NamingContainer;
            selectedRow = gvGoal.RowIndex;
            goalId = gvGoalList.DataKeys[selectedRow].Values["GoalId"].ToString();
            hdndeleteId.Value = goalId;
            //goalCatagory = gvGoalList.DataKeys[selectedRow].Values["GoalCategory"].ToString();
            goalAction = ddlAction.SelectedValue.ToString();
            if (ddlAction.SelectedValue == "View" || ddlAction.SelectedValue == "Edit")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('CustomerAdvancedGoalSetup','?GoalId=" + goalId + "&GoalAction=" + goalAction + "');", true);
            }
            else if (ddlAction.SelectedValue == "Fund")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('CustomerFPGoalFundingProgress','?GoalId=" + goalId + "');", true);
            }
            else if (ddlAction.SelectedValue == "Delete")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            }

        }

    //    protected void hiddenassociation_Click(object sender, EventArgs e)
    //    {
    //        string val = Convert.ToString(hdnMsgValue.Value);
    //        if (val == "1")
    //        {
    //            goalPlanningBo.DeleteCustomerGoalFunding(int.Parse(hdndeleteId.Value), customerVo.CustomerId);
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPGoalPlanningDetails','login');", true);
    //            msgRecordStatus.Visible = true; 
    //        }
    //    }
    }
}