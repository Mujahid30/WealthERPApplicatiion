using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using BoCommon;


namespace WealthERP.CustomerPortfolio
{
    public partial class Pension : System.Web.UI.Page
    {
        UserVo userVo = new UserVo();
        DataSet ds = new DataSet();
        AssetBo assetBo = new AssetBo();
        int customerId = 1008;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            LoadCategory("PG");
            LoadDebtIssuerCode(path);
            LoadInterestBasis(path);
            LoadFiscalYear(path);
            LoadFrequencyCode(path);
           
           
        }

        public void LoadCategory(string category)
        {
            DataSet ds = assetBo.GetAssetInstrumentCategory(category);
            ddlCategory.DataSource = ds.Tables[0];
            ddlCategory.DataTextField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataValueField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataBind();

        }
        public void LoadFrequencyCode(string path)
        {
            DataTable dt = assetBo.GetFrequencyCode(path);
            ddlSIFrequencyCode.DataSource = dt;
            ddlSIFrequencyCode.DataTextField = dt.Columns["WF_Frequency"].ToString();
            ddlSIFrequencyCode.DataValueField = dt.Columns["WF_FrequencyCode"].ToString();
            ddlSIFrequencyCode.DataBind();

            ddlCIInterestFrequencyCode.DataSource = dt;
            ddlCIInterestFrequencyCode.DataTextField = dt.Columns["WF_Frequency"].ToString();
            ddlCIInterestFrequencyCode.DataValueField = dt.Columns["WF_FrequencyCode"].ToString();
            ddlCIInterestFrequencyCode.DataBind();
            
        }
        public void LoadFiscalYear(string path)
        {
            DataTable dt = assetBo.GetFiscalYearCode(path);
            ddlFiscalYearCode.DataSource = dt;
            ddlFiscalYearCode.DataTextField = dt.Columns["WFY_FiscalYearCode"].ToString();
            ddlFiscalYearCode.DataValueField = dt.Columns["WFY_FiscalYear"].ToString();
            ddlFiscalYearCode.DataBind();

        }

        public void LoadInterestBasis(string path)
        {
            DataTable dt = assetBo.GetInterestBasis(path);
            ddlInterestBasis.DataSource = dt;
            ddlInterestBasis.DataTextField = dt.Columns["WIB_InterestBasis"].ToString();
            ddlInterestBasis.DataValueField = dt.Columns["WIB_InterestBasisCode"].ToString();
            ddlInterestBasis.DataBind();
        }
        public void LoadDebtIssuerCode(string path)
        {
            DataTable dt = assetBo.GetDebtIssuerCode(path);
            ddlDebtIssuerCode.DataSource = dt;
            ddlDebtIssuerCode.DataTextField = dt.Columns["WDI_DebtIssuerName"].ToString();
            ddlDebtIssuerCode.DataValueField = dt.Columns["WDI_DebtIssuerCode"].ToString();
            ddlDebtIssuerCode.DataBind();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            PensionAndGratuitiesBo pensionAndGratuitiesBo = new PensionAndGratuitiesBo();
            PensionAndGratuitiesVo pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
            pensionAndGratuitiesVo.AccountId = int.Parse(ddlAccountId.SelectedItem.Text.ToString());
            //pensionAndGratuitiesVo.AssetGroupCode = ddlAssetGroupCode.SelectedItem.Text.ToString();
            pensionAndGratuitiesVo.AssetGroupCode = "Pension";
            pensionAndGratuitiesVo.InterestBasis = ddlInterestBasis.SelectedItem.Value.ToString();
            if (pensionAndGratuitiesVo.InterestBasis == "SI")
            {
                pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlSIFrequencyCode.SelectedItem.Value.ToString();

            }
            else
            {
                pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlCIInterestFrequencyCode.SelectedItem.Value.ToString();
            }
            pensionAndGratuitiesVo.AssetInstrumentCategoryCode = ddlCategory.SelectedItem.Text.ToString();           
            pensionAndGratuitiesVo.CurrentValue = float.Parse(txtCurrentValue.Text);
            pensionAndGratuitiesVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Text.ToString();
            pensionAndGratuitiesVo.DepositAmount = float.Parse(txtDepositAmount.Text);
            pensionAndGratuitiesVo.FiscalYearCode = ddlFiscalYearCode.SelectedItem.Text.ToString();
            if (rbtnAccumulated.Checked)
            {
                pensionAndGratuitiesVo.IsInterestAccumalated = 1;
            }
            else
            {
                pensionAndGratuitiesVo.IsInterestAccumalated = 0;
            }
            if (pensionAndGratuitiesVo.IsInterestAccumalated == 1)
            {
                pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtAmountAccumulated.Text);
                pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
            }
            else
            {
                pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtAmountPaidout.Text);
                pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
            }
            
          //  pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlInterestPayableFrequencyCode.SelectedItem.Text.ToString();
            pensionAndGratuitiesVo.InterestRate = float.Parse(txtInterstRate.Text);
            pensionAndGratuitiesVo.LoanEndDate = DateTime.Parse(ddlLoanEndDay.SelectedItem.Value.ToString() + "/" + ddlLoanEndMonth.SelectedItem.Value.ToString() + "/" + ddlLoanEndYear.SelectedItem.Value.ToString());
            pensionAndGratuitiesVo.LoanOutstandingAmount = float.Parse(txtLoanOutstandingAmount.Text);
            pensionAndGratuitiesVo.LoanStartDate = DateTime.Parse(ddlLoanStartDay.SelectedItem.Value.ToString() + "/" + ddlLoanStartMonth.SelectedItem.Value.ToString() + "/" + ddlLoanStartYear.SelectedItem.Value.ToString());
            pensionAndGratuitiesVo.MaturityDate = DateTime.Parse(ddlMaturityDay.SelectedItem.Value.ToString() + "/" + ddlMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlMaturityYear.SelectedItem.Value.ToString());
            pensionAndGratuitiesVo.MaturityValue = float.Parse(txtMaturityValue.Text);
            pensionAndGratuitiesVo.OrganizationName = txtOrganizationName.Text;
            pensionAndGratuitiesVo.PurchaseDate = DateTime.Parse(ddlPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlProductMonth.SelectedItem.Value.ToString() + "/" + ddlProductYear.SelectedItem.Value.ToString());
            pensionAndGratuitiesBo.CreatePensionAndGratuities(pensionAndGratuitiesVo, userVo.UserId);

        }

        protected void rbtnAccumulated_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAccumulated.Checked)
            {
                lblAmountPaidout.Enabled = false;
                txtAmountPaidout.Enabled = false;
            }
            else
            {
                lblAmountAccumulated.Enabled = false;
                txtAmountAccumulated.Enabled = false;
            }
        }

        protected void rbtnPaidout_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPaidout.Checked && !rbtnAccumulated.Checked)
            {
                lblAmountAccumulated.Enabled = false;
                txtAmountAccumulated.Enabled = false;
            }
            if(rbtnAccumulated.Checked && !rbtnPaidout.Checked)
            {
                lblAmountPaidout.Enabled = false;
                txtAmountPaidout.Enabled = false;
            }

        }

        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInterestBasis.SelectedItem.Value.ToString() == "SI")
            {
                ddlCIInterestFrequencyCode.Enabled = false;
                ddlSIFrequencyCode.Enabled = true;
            }
            else
            {
                ddlSIFrequencyCode.Enabled = false;
                ddlCIInterestFrequencyCode.Enabled = true;
            }

        }

    }
}
