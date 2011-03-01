using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCalculator;

namespace WealthERP.General
{
    public partial class Calculators : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }




        protected void btnCalculateEMI_Click(object sender, EventArgs e)
        {
            if (txtLoanAmount.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtTenureYears.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtInterest.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtStartDate.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtEndDate.Text == "")
                //output1.Text = "Fill the blank fields marked with *";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else
            {
                Calculator calculator = new Calculator();
                DataTable dtRepaymentSchedule = new DataTable();
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                double loanAmount = 0;
                string frequencyCode = "";
                double interestRate = 0;
                double interestRatePerPeriod = 0;
                double installmentAmount = 0;
                int noOfInstallments = 0; ;
                int tenureYears = 0;
                int tenureMonths = 0;
                double.TryParse(txtLoanAmount.Text.ToString(), out loanAmount);
                double.TryParse(txtInterest.Text, out interestRate);
                DateTime.TryParse(txtStartDate.Text, out startDate);
                DateTime.TryParse(txtEndDate.Text, out endDate);

                if (startDate > endDate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Deposit date should be after Start date');", true);
                    // output.Text = "Deposit date should be after Start date";

                }
                else
                {
                    frequencyCode = ddlFrequency.SelectedItem.Value.ToString();
                    int.TryParse(txtTenureYears.Text, out tenureYears);
                    int.TryParse(txtTenureMonths.Text, out tenureMonths);
                    noOfInstallments = calculator.GetNumberOfINstallments(tenureYears, tenureMonths, frequencyCode);
                    installmentAmount = calculator.GetInstallmentAmount(2, frequencyCode, startDate, endDate, loanAmount, interestRate, noOfInstallments, out interestRatePerPeriod);
                    dtRepaymentSchedule = calculator.GetRepaymentSchedule(startDate, endDate, loanAmount, frequencyCode, interestRatePerPeriod, installmentAmount, noOfInstallments);

                    tblResult.Visible = true;
                    lblEMIAmountValue.Text = installmentAmount.ToString("f2");
                    lblNoOfInstallmentsValue.Text = noOfInstallments.ToString("f0");
                    gvRepaymentSchedule.DataSource = dtRepaymentSchedule;
                    gvRepaymentSchedule.DataBind();
                }
            }
        }
        protected void btnCalculatePV_Click(object sender, EventArgs e)
        {
            if (txtPVPaymentMade.Text == "" && txtPVFutureValue.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtPVInterestRate.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtPVNoOfPayments.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else
            {
                Calculator calculator = new Calculator();
                double pvInterestRate = 0;
                double pvNoOfInstallments = 0;
                double pvPayment = 0;
                double pvFutureValue = 0;
                int pvType = 0;
                double presentValue = 0;
                string frequencyCode = "";
                //string type="";
                double interestRatePerPeriod = 0;
                double.TryParse(txtPVInterestRate.Text, out pvInterestRate);
                pvInterestRate = pvInterestRate / 100;
                double.TryParse(txtPVNoOfPayments.Text, out pvNoOfInstallments);
                double.TryParse(txtPVPaymentMade.Text, out pvPayment);
                if (pvPayment > 0)
                {
                    pvPayment = -pvPayment;
                }
                 double.TryParse(txtPVFutureValue.Text, out pvFutureValue);
                if (pvFutureValue > 0)
                    pvFutureValue = -pvFutureValue;
                int.TryParse(rblPVType.SelectedItem.Value.ToString(), out pvType);
                frequencyCode = ddlPVPaymentFrequency.SelectedItem.Value.ToString();
                switch (frequencyCode)
                {
                    case "AM":
                        break;
                    case "DA":
                        interestRatePerPeriod = pvInterestRate / 365;
                        break;
                    case "FN":
                        interestRatePerPeriod = pvInterestRate / 24;
                        break;
                    case "HY":
                        interestRatePerPeriod = pvInterestRate / 2; ;
                        break;
                    case "MN":
                        interestRatePerPeriod = pvInterestRate / 12; ;

                        break;
                    case "NA":

                        break;
                    case "QT":
                        interestRatePerPeriod = pvInterestRate / 4;
                        break;
                    case "WK":
                        interestRatePerPeriod = pvInterestRate / 52;
                        break;
                    case "YR":
                        interestRatePerPeriod = pvInterestRate;

                        break;
                }
                //type=ddlChooseType.SelectedItem.Value.ToString();

                //switch (type)
                //{
                //    case "PI" :

                //    case "FV"
                //    case "BT"
                //}
                presentValue = calculator.PresentValue(interestRatePerPeriod, pvNoOfInstallments, pvPayment, pvFutureValue, pvType);
                trPVResult.Visible = true;
                if (presentValue < 0)
                    presentValue = -presentValue;

                lblPVValue.Text = presentValue.ToString("f2");
            }
        }
        protected void btnCalculateFV_Click(object sender, EventArgs e)
        {
            if (txtFVPaymentMade.Text == "" && txtFVFutureValue.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            //Response.Write("<script>alert('Fill the blank fields')</script>");
            else if (txtFVInterestRate.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else if (txtFVNoOfPayments.Text == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Fill all details');", true);
            else
            {
                Calculator calculator = new Calculator();
                double fvInterestRate = 0;
                double fvNoOfInstallments = 0;
                double fvPayment = 0;
                double fvPresentValue = 0;
                int fvType = 0;
                double futureValue = 0;
                double.TryParse(txtFVInterestRate.Text, out fvInterestRate);
                fvInterestRate = fvInterestRate / 100;
                double.TryParse(txtFVNoOfPayments.Text, out fvNoOfInstallments);
                double.TryParse(txtFVPaymentMade.Text, out fvPayment);
                if (fvPayment > 0)
                {
                    fvPayment = -fvPayment;
                }
                double.TryParse(txtFVFutureValue.Text, out fvPresentValue);
                if (fvPresentValue > 0)
                    fvPresentValue = -fvPresentValue;
                int.TryParse(rblFVType.SelectedItem.Value.ToString(), out fvType);
                string frequencyCode = "";
                double interestRatePerPeriod = 0;
                frequencyCode = ddlFVPaymentFrequency.SelectedItem.Value.ToString();
                switch (frequencyCode)
                {
                    case "AM":
                        break;
                    case "DA":
                        interestRatePerPeriod = fvInterestRate / 365;
                        break;
                    case "FN":
                        interestRatePerPeriod = fvInterestRate / 24;
                        break;
                    case "HY":
                        interestRatePerPeriod = fvInterestRate / 2; ;
                        break;
                    case "MN":
                        interestRatePerPeriod = fvInterestRate / 12; ;

                        break;
                    case "NA":

                        break;
                    case "QT":
                        interestRatePerPeriod = fvInterestRate / 4;
                        break;
                    case "WK":
                        interestRatePerPeriod = fvInterestRate / 52;
                        break;
                    case "YR":
                        interestRatePerPeriod = fvInterestRate;

                        break;
                }
                futureValue = calculator.FutureValue(interestRatePerPeriod, fvNoOfInstallments, fvPayment, fvPresentValue, fvType);
                trFVResult.Visible = true;
                if (futureValue < 0)
                    futureValue = -futureValue;

                lblFVValue.Text = futureValue.ToString("f2");
            }
        }

        protected void ddlChooseTypePV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseTypePV.SelectedValue == "0" || ddlChooseTypePV.SelectedValue == "3")
            {
                lblPVFutureValue.Visible = true;
                txtPVFutureValue.Visible = true;
                lblPVPaymentMade.Visible = true;
                txtPVPaymentMade.Visible = true;
                txtPVPaymentMade.Text = null;
                txtPVFutureValue.Text = null;
                trPVPaymentMade.Visible = true;
                trFutureValue.Visible = true;
            }

            if (ddlChooseTypePV.SelectedValue == "1")
            {
                lblPVPaymentMade.Visible = true;
                txtPVPaymentMade.Visible = true;
                txtPVFutureValue.Text = null;
                lblPVFutureValue.Visible = false;
                txtPVFutureValue.Visible = false;
                trFutureValue.Visible = false;
                trPVPaymentMade.Visible = true;
            }
            if (ddlChooseTypePV.SelectedValue == "2")
            {
                lblPVFutureValue.Visible = true;
                txtPVFutureValue.Visible = true;
                txtPVPaymentMade.Text = null;
                lblPVPaymentMade.Visible = false;
                txtPVPaymentMade.Visible = false;
                trFutureValue.Visible = true;
                trPVPaymentMade.Visible = false;

            }
        }
        protected void ddlchooseTypeFV_SelectedIndexChanged1(object sender, EventArgs e)
        {
         if (ddlchooseTypeFV.SelectedValue == "0" || ddlchooseTypeFV.SelectedValue == "3")
            {
                lblFVPresentValue.Visible = true;
                txtFVFutureValue.Visible = true;
                lblFVPaymentMade.Visible = true;
                txtFVPaymentMade.Visible = true;
                txtFVPaymentMade.Text = null;
                txtFVFutureValue.Text = null;
                trFvPaymentMade.Visible = true;
                trFVPresentValue.Visible = true;
            }
         if (ddlchooseTypeFV.SelectedValue == "1")
            {
                lblFVPaymentMade.Visible = true;
                txtFVPaymentMade.Visible = true;
                txtPVFutureValue.Text = null;
                lblFVPresentValue.Visible = false;
                txtPVFutureValue.Visible = false;
                trFVPresentValue.Visible = false;
                trFvPaymentMade.Visible = true;
            }
            if (ddlchooseTypeFV.SelectedValue == "2")
            {
                lblFVPaymentMade.Visible = false;
                txtFVPaymentMade.Visible = false;
                txtPVFutureValue.Text = null;
                lblFVPresentValue.Visible = true;
                txtPVFutureValue.Visible = true;
                trFVPresentValue.Visible = true;
                trFvPaymentMade.Visible = false;

            }

        }
    }
}