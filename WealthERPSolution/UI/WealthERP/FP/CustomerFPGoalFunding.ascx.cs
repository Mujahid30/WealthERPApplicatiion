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
using Telerik.Web.UI;

namespace WealthERP.FP
{
    public partial class CustomerFPGoalFunding : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        CustomerVo customerVo;
        DataSet dsRebalancing;
        int fundGoalId=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            msgRecordStatus.Visible = false;
            if (Session["customerVo"]!=null)
            customerVo = (CustomerVo)Session["customerVo"];
           
            if (!Page.IsPostBack)
            {
                btnView.Visible = false;
                
                dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);

                if (Request.QueryString["GoalId"] != null)
                {
                    fundGoalId = int.Parse(Request.QueryString["GoalId"].ToString());
                    BindGoalListDropDown(customerVo.CustomerId, fundGoalId);
                    GetThDetailsOfGoalFunding(fundGoalId);
                    SetDefaultGaolDetails(customerVo.CustomerId, fundGoalId);
                    trSelectGoal.Visible = false;
                }
                else
                {
                    BindGoalListDropDown(customerVo.CustomerId, fundGoalId);
                    if (ddlPickGoal.SelectedValue.ToString() != "")
                    {
                        GetThDetailsOfGoalFunding(Convert.ToInt32(ddlPickGoal.SelectedValue));
                        SetDefaultGaolDetails(customerVo.CustomerId, Convert.ToInt32(ddlPickGoal.SelectedValue));

                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Set Up Goal First');", true);
                    }
                }
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

        protected void BindGoalListDropDown(int customerId,int goalId)
       {
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerId);
            if (goalId == 0)
            {
                ddlPickGoal.DataSource = dsGoalList.Tables[0];
                ddlPickGoal.DataTextField = dsGoalList.Tables[0].Columns["XG_GoalName"].ToString();
                ddlPickGoal.DataValueField = dsGoalList.Tables[0].Columns["CG_GoalId"].ToString();
                ddlPickGoal.DataBind();
                trGoalName.Visible = false;
            }
            else
            {
                ddlPickGoal.DataSource = dsGoalList.Tables[0];
                ddlPickGoal.DataTextField = dsGoalList.Tables[0].Columns["XG_GoalName"].ToString();
                ddlPickGoal.DataValueField = dsGoalList.Tables[0].Columns["CG_GoalId"].ToString();
                ddlPickGoal.DataBind();
                ddlPickGoal.SelectedValue = goalId.ToString();
                trGoalName.Visible = true;
                string goalName = ddlPickGoal.SelectedItem.ToString();
                lblGoalName.Text = goalName;
                
            }
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

                   if (int.Parse(txtEquityAllAmt.Text) != 0)
                   {
                       Dictionary<string, decimal> equityAssetValues = calculateRemainingFields(decimal.Parse(txtEquityAllAmt.Text), decimal.Parse(txtEquityAvlCorps.Text));
                       txtEquityAllPer.Text = Math.Round(decimal.Parse(equityAssetValues["PERCENT"].ToString()), 2).ToString();
                       txtEquityRemainCorpus.Text = Math.Round(decimal.Parse(equityAssetValues["CORPS"].ToString()), 0).ToString();
                   }
                   else
                   {
                       txtEquityRemainCorpus.Text = "0";
                       txtEquityAllPer.Text = "0";
                   }
                   txtDebtAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[1]["CGF_AllocatedAmount"].ToString()), 0).ToString();
                   if (int.Parse(txtDebtAllAmt.Text) != 0)
                   {
                       Dictionary<string, decimal> debtAssetValues = calculateRemainingFields(decimal.Parse(txtDebtAllAmt.Text), decimal.Parse(txtDebtAvlCorps.Text));
                       txtDebtAllPer.Text = Math.Round(decimal.Parse(debtAssetValues["PERCENT"].ToString()), 2).ToString();
                       txtDebtRemainCorpus.Text = Math.Round(decimal.Parse(debtAssetValues["CORPS"].ToString()), 0).ToString();
                   }
                   else
                   {
                       txtDebtRemainCorpus.Text = "0";
                       txtDebtAllPer.Text = "0";
                   }
                   txtCashAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[2]["CGF_AllocatedAmount"].ToString()), 0).ToString();
                   if (decimal.Parse(txtCashAvlCorps.Text) != 0)
                   {
                       Dictionary<string, decimal> cashAssetValues = calculateRemainingFields(decimal.Parse(txtCashAllAmt.Text), decimal.Parse(txtCashAvlCorps.Text));
                       txtCashAllPer.Text = Math.Round(decimal.Parse(cashAssetValues["PERCENT"].ToString()), 2).ToString();
                       txtCashRemainCorpus.Text = Math.Round(decimal.Parse(cashAssetValues["CORPS"].ToString()), 0).ToString();
                   }
                   else
                   {
                       txtCashAllPer.Text = "0";
                       txtCashRemainCorpus.Text = "0";
                   }
                   txtStartLoanYr.Text = drSetDefaultGoal[0]["CGF_LoanStartDate"].ToString();
                   txtLoanAmountFunding.Text = drSetDefaultGoal[0]["CGF_LoanAmount"].ToString();
                   if (count != 3)
                   {
                       txtAlternateAllAmt.Text = Math.Round(decimal.Parse(drSetDefaultGoal[3]["CGF_AllocatedAmount"].ToString()), 0).ToString();
                       if (int.Parse(txtAlternateAllAmt.Text) != 0)
                       {
                           Dictionary<string, decimal> alternateAssetValues = calculateRemainingFields(decimal.Parse(txtAlternateAllAmt.Text), decimal.Parse(txtAlternateAvlCorps.Text));
                           txtAlternateAllPer.Text = Math.Round(decimal.Parse(alternateAssetValues["PERCENT"].ToString()), 2).ToString();
                           txtAlternateRemainCorpus.Text = Math.Round(decimal.Parse(alternateAssetValues["CORPS"].ToString()), 0).ToString();
                       }
                       else
                       {
                           txtAlternateAllPer.Text = "0";
                           txtAlternateRemainCorpus.Text = "0";
                       }
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
                   txtAmountFunded.Text = totalAmount.ToString();
                   if (txtCashRemainCorpus.Text == "")
                   {
                       txtCashRemainCorpus.Text = "0";
                   }
                   if (txtAlternateRemainCorpus.Text != "")
                   {
                       txtAmountRemaining.Text = (int.Parse(txtEquityRemainCorpus.Text) + int.Parse(txtDebtRemainCorpus.Text) + int.Parse(txtCashRemainCorpus.Text) + int.Parse(txtAlternateRemainCorpus.Text)).ToString();
                   }
                   else
                   {
                       txtAmountRemaining.Text = (int.Parse(txtEquityRemainCorpus.Text) + int.Parse(txtDebtRemainCorpus.Text) + int.Parse(txtCashRemainCorpus.Text)).ToString();
                   }
               }
            
        }
        public Dictionary<string, decimal> calculateRemainingFields(decimal AllAmount, decimal AvlAmount)
        {
            //decimal[] assetValues = new decimal[] { };
            Dictionary<string, decimal> assetValues =new  Dictionary<string, decimal>();
            decimal assetPercent;
            decimal remaingCorps;
            if (AvlAmount != 0)
            {
                assetPercent = AllAmount * 100 / AvlAmount;
                remaingCorps = AvlAmount - AllAmount;
                assetValues.Add("PERCENT", assetPercent);
                assetValues.Add("CORPS", remaingCorps);
            }
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

            txtAmountFunded.Text = "";
            txtAmountRemaining.Text = "";
            
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
                btnEdit.Visible = false;
                btnView.Visible = true;
                ViewState["ActionGoalFundPage"] = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
                SetEditViewMode(true);
            }
            if (e.Item.Value == "View")
            {
                btnEdit.Visible = true;
                btnView.Visible = false;
                ViewState["ActionGoalFundPage"] = "View";
                SetEditViewMode(false);
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
                txtAmountFunded.Enabled = true;
                txtAmountRemaining.Enabled = true;
               
                
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
                txtAmountFunded.Enabled = false;
                txtAmountRemaining.Enabled = false;
               
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
            drGoal = dsGoalList.Tables[0].Select("CG_GoalId=" + gaolId.ToString());
            string dynamicLabel = drGoal[0]["CG_GoalYear"].ToString();
            lblGoalYear.Text = dynamicLabel + ":";
            lblAvailableCorpus.Text = lblAvailableCorpus.Text + dynamicLabel;
            txtGoalAmountReq.Text = Math.Round(decimal.Parse(drGoal[0]["CG_CostToday"].ToString()), 0).ToString();
            dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            if (dsRebalancing.Tables.Count > 0)
            {
                
                drAssetDetails = dsRebalancing.Tables[1].Select("Year=" + drGoal[0][2].ToString());
                if (drAssetDetails.Count() > 0)
                {
                    txtEquityAvlCorps.Text = drAssetDetails[0]["PreviousYearClosingBalance"].ToString();
                    txtDebtAvlCorps.Text = drAssetDetails[1]["PreviousYearClosingBalance"].ToString();
                    txtCashAvlCorps.Text = drAssetDetails[2]["PreviousYearClosingBalance"].ToString();
                    txtAlternateAvlCorps.Text = drAssetDetails[3]["PreviousYearClosingBalance"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have any avialable corps');", true);
                    btnEdit.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have future saving');", true);
                btnEdit.Enabled = false; 
            }

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
            btnView.Visible = false;
            btnEdit.Visible = true;
        }
    }
}