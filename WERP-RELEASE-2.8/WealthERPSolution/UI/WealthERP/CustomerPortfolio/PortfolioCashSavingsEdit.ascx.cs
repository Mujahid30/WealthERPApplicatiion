using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioCashSavingsEdit : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();
        CashAndSavingsBo CashSavingsBo = new CashAndSavingsBo();
        CashAndSavingsVo CashSavingsVo = new CashAndSavingsVo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        int customerId;
        string assetGroupCode = "CS"; // Replace with Group Code
        string assetGroupName = "Cash & Savings";
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (!IsPostBack)
                {
                    // Bind Drop Down Lists
                    BindDropDownLists();

                    // Bind Porfolio Specific Details Here 
                    BindDetails();
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsEdit.ascx.cs:Page_Load()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindDetails()
        {
            // Get the Portfolio ID from Session'
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;

                CashSavingsVo = new CashAndSavingsVo();
                int CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());

                CashSavingsVo = CashSavingsBo.GetSpecificCashSavings(CashSavingsPortfolioId, customerId);

                if (CashSavingsVo != null)
                {
                    ddlAssetIC.SelectedValue = CashSavingsVo.AssetInstrumentCategoryCode.ToString(); //.Trim()


                    DataSet dsAccounts = new DataSet();
                    CustomerAccountBo custAccBo = new CustomerAccountBo();

                    dsAccounts = custAccBo.GetCustomerCashSavingsAccounts(customerVo.CustomerId, assetGroupCode, ddlAssetIC.SelectedValue);

                    if (dsAccounts.Tables[0].Rows.Count > 0)
                    {
                        ddlAccountID.DataSource = dsAccounts.Tables[0];
                        ddlAccountID.DataTextField = "CCSA_AccountNum";
                        ddlAccountID.DataValueField = "CCSA_AccountId";
                        ddlAccountID.DataBind();
                        ddlAccountID.Items.Insert(0, "Select an Account");
                    }

                    ddlAccountID.SelectedValue = CashSavingsVo.AccountId.ToString(); // .Trim()
                    txtName.Text = CashSavingsVo.Name.ToString().Trim();
                    txtDepositAmount.Text = CashSavingsVo.DepositAmount.ToString().Trim();
                    txtDepositDate.Text = CashSavingsVo.DepositDate.ToShortDateString().Trim();
                    txtCurrentValue.Text = CashSavingsVo.CurrentValue.ToString().Trim();
                    txtInterestRate.Text = CashSavingsVo.InterestRate.ToString().Trim();
                    ddlDebtIssuer.SelectedValue = CashSavingsVo.DebtIssuerCode.ToString().Trim();//
                    ddlInterestBasis.SelectedValue = CashSavingsVo.InterestBasisCode.ToString().Trim();//

                    if (CashSavingsVo.InterestBasisCode.Trim() == "CI")
                    {
                        trCIFrequency.Visible = true;
                        ddlCIFrequency.SelectedValue = CashSavingsVo.CompoundInterestFrequencyCode.ToString().Trim();
                    }
                    else
                    {
                        trCIFrequency.Visible = false;
                    }

                    if (CashSavingsVo.IsInterestAccumalated == 0)
                    {
                        rbtnInterestAccumYes.Checked = false;
                        rbtnInterestAccumNo.Checked = true;
                        trIA.Visible = false;
                        trIP.Visible = true;
                        trIPFrequency.Visible = true;

                        txtInterestAmtPaidOut.Text = CashSavingsVo.InterestAmntPaidOut.ToString().Trim();
                        ddlIPFrequency.SelectedValue = CashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
                    }
                    else if (CashSavingsVo.IsInterestAccumalated == 1)
                    {
                        rbtnInterestAccumYes.Checked = true;
                        rbtnInterestAccumNo.Checked = false;
                        trIA.Visible = true;
                        trIP.Visible = false;
                        trIPFrequency.Visible = false;

                        txtInterestAmtAccumulated.Text = CashSavingsVo.InterestAmntAccumulated.ToString().Trim();
                    }
                    else
                    {
                        trIA.Visible = false;
                        trIP.Visible = false;
                        trIPFrequency.Visible = false;
                    }
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsEdit.ascx.cs:BindDetails()");
                object[] objects = new object[3];
                objects[0] = CashSavingsVo;
                objects[1] = customerVo;
                objects[2] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindDropDownLists()
        {
            AssetBo assetBo = new AssetBo();
            DataTable dtInterestBasis = new DataTable();
            DataSet dsInstrumentCategory = new DataSet();
            DataTable dtDebtIssuer = new DataTable();
            DataTable dtFrequency = new DataTable();
            string xmlLookUpPath="";
            try
            {
                xmlLookUpPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                // Bind the drop down with values from the XML
                dtInterestBasis = assetBo.GetInterestBasis(xmlLookUpPath);
                ddlInterestBasis.DataSource = dtInterestBasis;
                ddlInterestBasis.DataTextField = "WIB_InterestBasis";
                ddlInterestBasis.DataValueField = "WIB_InterestBasisCode";
                ddlInterestBasis.DataBind();
                ddlInterestBasis.Items.Insert(0, "Select an Interest Basis");

                dsInstrumentCategory = assetBo.GetAssetInstrumentCategory(assetGroupCode);
                ddlAssetIC.DataSource = dsInstrumentCategory.Tables[0];
                ddlAssetIC.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlAssetIC.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlAssetIC.DataBind();
                ddlAssetIC.Items.Insert(0, "Select an Asset Instrument Category");

                dtDebtIssuer = assetBo.GetDebtIssuerCode(xmlLookUpPath);
                ddlDebtIssuer.DataSource = dtDebtIssuer;
                ddlDebtIssuer.DataTextField = "WDI_DebtIssuerName";
                ddlDebtIssuer.DataValueField = "WDI_DebtIssuerCode";
                ddlDebtIssuer.DataBind();
                ddlDebtIssuer.Items.Insert(0, "Select a Debt Issuer");

                dtFrequency = assetBo.GetFrequencyCode(xmlLookUpPath);

                ddlCIFrequency.DataSource = dtFrequency;
                ddlCIFrequency.DataTextField = "WF_Frequency";
                ddlCIFrequency.DataValueField = "WF_FrequencyCode";
                ddlCIFrequency.DataBind();
                ddlCIFrequency.Items.Insert(0, "Select a Compounding Frequency");

                ddlIPFrequency.DataSource = dtFrequency;
                ddlIPFrequency.DataTextField = "WF_Frequency";
                ddlIPFrequency.DataValueField = "WF_FrequencyCode";
                ddlIPFrequency.DataBind();
                ddlIPFrequency.Items.Insert(0, "Select an Interest Payout Frequency");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEdit.ascx.cs:BindDropDownLists()");
                object[] objects = new object[2];
                objects[0] = xmlLookUpPath;
                objects[1] = assetGroupCode;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnUpdateDetails_Click(object sender, EventArgs e)
        {
            CashAndSavingsVo customerCashSavingsPortfolioVo = new CashAndSavingsVo();
            CashAndSavingsBo customerCashSavingsPortfolioBo = new CashAndSavingsBo();
            try
            {
            // Get Session Values
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            userId = userVo.UserId;
            customerId = customerVo.CustomerId;

          

            customerCashSavingsPortfolioVo.CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());
            customerCashSavingsPortfolioVo.AccountId = Int64.Parse(ddlAccountID.SelectedValue);
            customerCashSavingsPortfolioVo.AssetGroupCode = assetGroupCode;
            customerCashSavingsPortfolioVo.AssetInstrumentCategoryCode = ddlAssetIC.SelectedValue.ToString();
            customerCashSavingsPortfolioVo.CustomerId = customerId;
            customerCashSavingsPortfolioVo.Name = txtName.Text.Trim();
            customerCashSavingsPortfolioVo.DepositAmount = float.Parse(txtDepositAmount.Text.Trim());
            customerCashSavingsPortfolioVo.DepositDate = DateTime.Parse(txtDepositDate.Text.Trim());
            customerCashSavingsPortfolioVo.CurrentValue = float.Parse(txtCurrentValue.Text.Trim());
            customerCashSavingsPortfolioVo.InterestRate = float.Parse(txtInterestRate.Text.Trim());
            customerCashSavingsPortfolioVo.DebtIssuerCode = ddlDebtIssuer.SelectedValue.ToString();
            customerCashSavingsPortfolioVo.InterestBasisCode = ddlInterestBasis.SelectedValue.ToString();
            if (ddlInterestBasis.SelectedValue == "CI")
            {
                customerCashSavingsPortfolioVo.CompoundInterestFrequencyCode = ddlCIFrequency.SelectedValue.ToString();
            }
            else if (ddlInterestBasis.SelectedValue == "SI")
            {
                customerCashSavingsPortfolioVo.InterestPayoutFrequencyCode = ddlIPFrequency.SelectedValue.ToString();
            }

            if (rbtnInterestAccumYes.Checked == true)
            {
                customerCashSavingsPortfolioVo.IsInterestAccumalated = 1;
                customerCashSavingsPortfolioVo.InterestAmntAccumulated = float.Parse(txtInterestAmtAccumulated.Text.Trim());
            }
            else if (rbtnInterestAccumNo.Checked == true)
            {
                customerCashSavingsPortfolioVo.IsInterestAccumalated = 0;
                customerCashSavingsPortfolioVo.InterestAmntPaidOut = float.Parse(txtInterestAmtPaidOut.Text.Trim());
            }

           
                if (customerCashSavingsPortfolioBo.UpdateCashSavingsDetails(customerCashSavingsPortfolioVo, userId))
                {
                    // Redirect to View Cash Details Control 
                    trError.Visible = false;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioCashSavingsView','none');", true);
                }
                else
                {
                    // Display Error Message if the Addition has failed
                    trError.Visible = true;
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsEdit.ascx:btnUpdateDetails_Click()");
                object[] objects = new object[3];
                objects[0] = customerCashSavingsPortfolioVo;
                objects[1] = customerVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInterestBasis.SelectedItem.Text == "Compound Interest")
            {
                trCIFrequency.Visible = true;
            }
            else if (ddlInterestBasis.SelectedItem.Text == "Simple Interest")
                trCIFrequency.Visible = false;
        }

        protected void rbtnInterestAccum_CheckChanged(object sender, EventArgs e)
        {
            if (rbtnInterestAccumYes.Checked == true)
            {
                trIA.Visible = true;
            }
            else if (rbtnInterestAccumNo.Checked == true)
            {
                trIP.Visible = true;
                trIPFrequency.Visible = true;
            }
        }

        protected void ddlAssetIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsAccounts = new DataSet();
            CustomerAccountBo custAccBo = new CustomerAccountBo();

            try
            {
                dsAccounts = custAccBo.GetCustomerCashSavingsAccounts(customerVo.CustomerId, assetGroupCode, ddlAssetIC.SelectedValue);
                if (dsAccounts.Tables[0].Rows.Count > 0)
                {
                    ddlAccountID.DataSource = dsAccounts.Tables[0];
                    ddlAccountID.DataTextField = "CCSA_AccountNum";
                    ddlAccountID.DataValueField = "CCSA_AccountId";
                    ddlAccountID.DataBind();
                    ddlAccountID.Items.Insert(0, "Select an Account");
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsEdit.ascx:ddlAssetIC_SelectedIndexChanged()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = assetGroupCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

           
        }

        protected void lnkbtnAddAccounts_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerAccountsAdd','none');", true);
        }
    }
}