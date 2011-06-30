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
using Telerik.Web.UI;

namespace WealthERP.FP
{
    public partial class CustomerFPGoalFunding : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        CustomerVo customerVo;
        DataSet dsRebalancing;

        protected void Page_Load(object sender, EventArgs e)
        {
            msgRecordStatus.Visible = false;
            if (Session["customerVo"]!=null)
            customerVo = (CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                BindGoalListDropDown(customerVo.CustomerId);
                dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
                GetThDetailsOfGoalFunding(Convert.ToInt32(ddlPickGoal.SelectedValue));
                SetDefaultGaolDetails(customerVo.CustomerId, Convert.ToInt32(ddlPickGoal.SelectedValue));
            }
            if (ViewState["ActionGoalFundPage"] == null)
            {
                ViewState["ActionGoalFundPage"] = "View";
            }

            if (ViewState["ActionGoalFundPage"].ToString() == "View")
            {
                SetEditViewMode(false);
            }
            else if (ViewState["ActionGoalFundPage"].ToString() == "Edit")
            {
                SetEditViewMode(true);
            }
            txtLoanAmountFunding.Enabled = false;
            txtStartLoanYr.Enabled = false;
            
            chkGoalFundByLoan.Checked = false;

        }

        protected void BindGoalListDropDown(int customerId)
        {
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerId);           
            ddlPickGoal.DataSource = dsGoalList.Tables[0];
            ddlPickGoal.DataTextField = dsGoalList.Tables[0].Columns["XG_GoalName"].ToString();
            ddlPickGoal.DataValueField = dsGoalList.Tables[0].Columns["CG_GoalId"].ToString();
            ddlPickGoal.DataBind();
            int count = dsGoalList.Tables[1].Rows.Count;
            if (count == 3)
            {
                trAlternate.Visible = false;
                hdnAlternate.Value = count.ToString();
               
            }
            else
                trAlternate.Visible = true;
            hdnAlternate.Value = count.ToString();
           

        }

        public void SetDefaultGaolDetails(int customerId,int goalId)
        {
            DataRow[] drSetDefaultGoal;
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerId);  
            
               drSetDefaultGoal = dsGoalList.Tables[2].Select("CG_GoalId=" + goalId.ToString());
               int count = dsGoalList.Tables[1].Rows.Count;

               if (drSetDefaultGoal.Count() > 0)
               {
                   txtEquityAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[0]["CGF_AllocatedAmount"].ToString()), 0).ToString();
                   Dictionary<string, decimal> equityAssetValues = calculateRemainingFields(decimal.Parse(txtEquityAllAmt.Text), decimal.Parse(txtEquityAvlCorps.Text));
                   txtEquityAllPer.Text = Math.Round(decimal.Parse(equityAssetValues["PERCENT"].ToString()), 2).ToString();
                   txtEquityRemainCorpus.Text = Math.Round(decimal.Parse(equityAssetValues["CORPS"].ToString()), 0).ToString();

                   txtDebtAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[1]["CGF_AllocatedAmount"].ToString()), 0).ToString();

                   Dictionary<string, decimal> debtAssetValues = calculateRemainingFields(decimal.Parse(txtDebtAllAmt.Text), decimal.Parse(txtDebtAvlCorps.Text));
                   txtDebtAllPer.Text = Math.Round(decimal.Parse(debtAssetValues["PERCENT"].ToString()), 2).ToString();
                   txtDebtRemainCorpus.Text = Math.Round(decimal.Parse(debtAssetValues["CORPS"].ToString()), 0).ToString();

                   txtCashAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[2]["CGF_AllocatedAmount"].ToString()), 0).ToString();

                   Dictionary<string, decimal> cashAssetValues = calculateRemainingFields(decimal.Parse(txtCashAllAmt.Text), decimal.Parse(txtCashAvlCorps.Text));
                   txtCashAllPer.Text = Math.Round(decimal.Parse(cashAssetValues["PERCENT"].ToString()), 2).ToString();
                   txtCashRemainCorpus.Text = Math.Round(decimal.Parse(cashAssetValues["CORPS"].ToString()), 0).ToString();

                   txtStartLoanYr.Text = drSetDefaultGoal[0]["CGF_LoanStartDate"].ToString();
                   txtLoanAmountFunding.Text = drSetDefaultGoal[0]["CGF_LoanAmount"].ToString();
                   if (count != 3)
                   {
                       txtAlternateAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[3]["CGF_AllocatedAmount"].ToString()), 0).ToString();
                       Dictionary<string, decimal> alternateAssetValues = calculateRemainingFields(decimal.Parse(txtAlternateAllAmt.Text), decimal.Parse(txtAlternateAvlCorps.Text));
                       txtAlternateAllPer.Text = Math.Round(decimal.Parse(alternateAssetValues["PERCENT"].ToString()), 2).ToString();
                       txtAlternateRemainCorpus.Text = Math.Round(decimal.Parse(alternateAssetValues["CORPS"].ToString()), 0).ToString();
                   }
                   decimal totalAmount;
                   if (txtAlternateAllAmt.Text != "")
                   {
                       totalAmount = decimal.Parse(txtCashAllAmt.Text) + decimal.Parse(txtDebtAllAmt.Text) + decimal.Parse(txtAlternateAllAmt.Text) + decimal.Parse(txtEquityAllAmt.Text);
                       Label1.Text = Math.Round(decimal.Parse(totalAmount.ToString()), 0).ToString();
                   }
                   else
                   {
                       totalAmount = decimal.Parse(txtCashAllAmt.Text) + decimal.Parse(txtDebtAllAmt.Text) + decimal.Parse(txtEquityAllAmt.Text);
                       Label1.Text = Math.Round(decimal.Parse(totalAmount.ToString()), 0).ToString();
                   }
                   txtGapAfterAllocation.Text = (decimal.Parse(txtGoalAmountReq.Text) - totalAmount).ToString();
               }
            
        }
        public Dictionary<string, decimal> calculateRemainingFields(decimal AllAmount, decimal AvlAmount)
        {
            //decimal[] assetValues = new decimal[] { };
            Dictionary<string, decimal> assetValues =new  Dictionary<string, decimal>();
            decimal assetPercent;
            decimal remaingCorps;            
            assetPercent = AllAmount * 100 / AvlAmount;           
            remaingCorps = AvlAmount - AllAmount;
            assetValues.Add("PERCENT", assetPercent);
            assetValues.Add("CORPS", remaingCorps);
            return assetValues;
            
        } 
           
        protected void ddlPickGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetThDetailsOfGoalFunding(Convert.ToInt32(ddlPickGoal.SelectedValue));
            txtAlternateAllAmt.Text = "";
            txtCashAllAmt.Text = "";
            txtDebtAllAmt.Text = "";
            txtEquityAllAmt.Text = "";
            txtGapAfterAllocation.Text = "";
            txtStartLoanYr.Text = "";
            txtLoanAmountFunding.Text = "";
            txtEquityRemainCorpus.Text = "";
            txtEquityAllPer.Text = "";
            txtDebtRemainCorpus.Text = "";
            txtDebtAllPer.Text = "";
            txtCashAllPer.Text = "";
           
           
            txtAlternateAllPer.Text = "";
            Label1.Text = "";
            txtCashRemainCorpus.Text = "";
            chkGoalFundByLoan.Checked = false;

            SetDefaultGaolDetails(customerVo.CustomerId, Convert.ToInt32(ddlPickGoal.SelectedValue));
        }
        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Edit")
            {
                ViewState["ActionGoalFundPage"] = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
                SetEditViewMode(true);
            }
        }
        public void SetEditViewMode(bool Bool)
        {

            if (Bool)
            {
                ddlPickGoal.Enabled = true;
                txtGoalAmountReq.Enabled = true;
                txtEquityAllAmt.Enabled = true;
                txtDebtAllAmt.Enabled = true;
                txtAlternateAllAmt.Enabled = true;
                txtCashAllAmt.Enabled = true;
                txtGapAfterAllocation.Enabled = true;
                //txtLoanAmountFunding.Enabled = true;
                //txtStartLoanYr.Enabled = true;
                txtEquityRemainCorpus.Enabled = true;
                txtEquityAvlCorps.Enabled = true;
                txtEquityAllPer.Enabled = true;
                txtDebtRemainCorpus.Enabled = true;
                txtDebtAvlCorps.Enabled = true;
                txtDebtAllPer.Enabled = true;
                txtCashRemainCorpus.Enabled = true;
                txtCashAvlCorps.Enabled = true;
                txtAlternateRemainCorpus.Enabled = true;
                txtAlternateAvlCorps.Enabled = true;
                txtAlternateAllPer.Enabled = true;
                chkGoalFundByLoan.Enabled = true;
                btnSubmit.Enabled = true;
                btnEdit.Enabled = false;
            }
            else
            {
                ddlPickGoal.Enabled = false;
                txtGoalAmountReq.Enabled = false;
                txtEquityAllAmt.Enabled = false;
                txtDebtAllAmt.Enabled = false;
                txtAlternateAllAmt.Enabled = false;
                txtCashAllAmt.Enabled = false;
                txtGapAfterAllocation.Enabled = false;
                //txtLoanAmountFunding.Enabled = false;
                //txtStartLoanYr.Enabled = false;
                txtEquityRemainCorpus.Enabled = false;
                txtEquityAvlCorps.Enabled = false;
                txtEquityAllPer.Enabled = false;
                txtDebtRemainCorpus.Enabled = false;
                txtDebtAvlCorps.Enabled = false;
                txtDebtAllPer.Enabled = false;
                txtCashRemainCorpus.Enabled = false;
                txtCashAvlCorps.Enabled = false;
                txtAlternateRemainCorpus.Enabled = false;
                txtAlternateAvlCorps.Enabled = false;
                txtAlternateAllPer.Enabled = false;
                chkGoalFundByLoan.Enabled = false;
                btnSubmit.Enabled = false;
                btnEdit.Enabled = true;
            }
        }
        public void GetThDetailsOfGoalFunding(int gaolId)
        {
            int goalYear = 0;
            int goalAmount = 0;
            DataRow[] drGoal;
            DataRow[] drAssetDetails;
            customerVo = (CustomerVo)Session["customerVo"];
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerVo.CustomerId);
            dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            drGoal = dsGoalList.Tables[0].Select("CG_GoalId=" + gaolId.ToString());
            drAssetDetails = dsRebalancing.Tables[1].Select("Year=" + drGoal[0][2].ToString());
            string dynamicLabel = drGoal[0]["CG_GoalYear"].ToString();
            lblGoalYear.Text = dynamicLabel+":";
            txtGoalAmountReq.Text = Math.Round(decimal.Parse(drGoal[0]["CG_CostToday"].ToString()),0).ToString();
            txtEquityAvlCorps.Text = drAssetDetails[0]["PreviousYearClosingBalance"].ToString();
            txtDebtAvlCorps.Text = drAssetDetails[1]["PreviousYearClosingBalance"].ToString();
            txtCashAvlCorps.Text = drAssetDetails[2]["PreviousYearClosingBalance"].ToString();
            txtAlternateAvlCorps.Text = drAssetDetails[3]["PreviousYearClosingBalance"].ToString();
 
 
 
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int goalId = 0;
               goalId = int.Parse(ddlPickGoal.SelectedValue.ToString());
               decimal equityAllocatedAmount = 0;
               decimal debtAllocatedAmount = 0;
               decimal cashAllocatedAmount = 0;
               decimal alternateAllocatedAmount = 0;

               if (txtEquityAllAmt.Text != "")
                   equityAllocatedAmount = decimal.Parse(txtEquityAllAmt.Text.ToString());
               if (txtDebtAllAmt.Text != "")
                   debtAllocatedAmount = decimal.Parse(txtDebtAllAmt.Text.ToString());
               if (txtCashAllAmt.Text != "")
                   cashAllocatedAmount = decimal.Parse(txtCashAllAmt.Text.ToString());
               if (txtAlternateAllAmt.Text != "")
                   alternateAllocatedAmount = decimal.Parse(txtAlternateAllAmt.Text.ToString());
            int isloanFunded=0;
            decimal loanAmount=0;
            DateTime loanStartDate=new DateTime();


            if(chkGoalFundByLoan.Checked==true)
            {
                isloanFunded=1;
                if(txtLoanAmountFunding.Text!="")
                loanAmount = decimal.Parse(txtLoanAmountFunding.Text.ToString());
                if(txtStartLoanYr.Text!="")
                loanStartDate=DateTime.Parse(txtStartLoanYr.Text.ToString());

            }


            goalPlanningBo.CreateCustomerGoalFunding(goalId, equityAllocatedAmount, debtAllocatedAmount, cashAllocatedAmount, alternateAllocatedAmount, isloanFunded, loanAmount, loanStartDate);
            msgRecordStatus.Visible = true;
            SetEditViewMode(false);
        }
    }
}