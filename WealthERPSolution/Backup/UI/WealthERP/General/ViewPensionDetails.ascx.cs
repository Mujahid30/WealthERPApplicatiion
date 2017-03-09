using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewPensionDetails : System.Web.UI.UserControl
    {
        PensionAndGratuitiesBo pensionAndGratuitesBo = new PensionAndGratuitiesBo();
        PensionAndGratuitiesVo pensionAndGratuitiesVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                pensionAndGratuitiesVo = (PensionAndGratuitiesVo)Session["pensionAndGratuities"];
                ShowPensionDetails();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewPensionDetails.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = pensionAndGratuitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void ShowPensionDetails()
        {
            try
            {
                lblAssetInsturmentCategory.Text = pensionAndGratuitiesVo.AssetInstrumentCategoryCode.ToString();
                lblCurrentValue.Text = pensionAndGratuitiesVo.CurrentValue.ToString();
                lblDebtIssuerCode.Text = pensionAndGratuitiesVo.DebtIssuerCode.ToString();
                lblDepositAmount.Text = pensionAndGratuitiesVo.DepositAmount.ToString();
                lblFiscalYearCode.Text = pensionAndGratuitiesVo.FiscalYearCode.ToString();
                lblInterestPayableFrequency.Text = pensionAndGratuitiesVo.InterestPayableFrequencyCode.ToString();
                lblInterestRate.Text = pensionAndGratuitiesVo.InterestRate.ToString();
                lblLoanEndDate.Text = pensionAndGratuitiesVo.LoanEndDate.ToShortDateString();
                lblLoanOutstandingAmount.Text = pensionAndGratuitiesVo.LoanOutstandingAmount.ToString();
                lblLoanStartDate.Text = pensionAndGratuitiesVo.LoanStartDate.ToShortDateString();
                lblMaturityValue.Text = pensionAndGratuitiesVo.MaturityValue.ToString();
                lblMaturiyDate.Text = pensionAndGratuitiesVo.MaturityDate.ToShortDateString();
                lblOrganizationName.Text = pensionAndGratuitiesVo.OrganizationName.ToString();
                lblPurchaseDate.Text = pensionAndGratuitiesVo.PurchaseDate.ToShortDateString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewPensionDetails.cs:ShowPensionDetails()");
                object[] objects = new object[1];
                objects[0] = pensionAndGratuitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }
}