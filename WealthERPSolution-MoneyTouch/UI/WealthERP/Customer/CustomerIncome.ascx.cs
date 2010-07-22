using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoUser;
using VoCustomerProfiling;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoUser;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace WealthERP.Customer
{
    public partial class ViewCustomerIncome : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();

        DataTable dtCurrency = new DataTable();
        DataTable dtProperties = new DataTable();


        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerIncomeVo customerIncomeVo = new CustomerIncomeVo();
        CustomerVo customerVo = new CustomerVo();
        UserVo tempUserVo = new UserVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();

        int rmId;
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
                rmVo = (RMVo)Session["rmVo"];
                this.Page.Culture = "en-GB";
                customerVo = (CustomerVo)Session["CustomerVo"];

                if (!IsPostBack)
                {
                    BindDropDowns(path);
                    BindPropertyDropDown(customerVo.CustomerId);
                    InitializeTextBoxes();
                    GetCustomerIncomeDetails(customerVo.CustomerId);
                }
                CompareValidator1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void BindDropDowns(string path)
        {
            dtCurrency = XMLBo.GetCurrency(path);
            ddlAICurrency.DataSource = dtCurrency;
            ddlAICurrency.DataTextField = "CurrencyName";
            ddlAICurrency.DataValueField = "CurrencyCode";
            ddlAICurrency.DataBind();
            ddlAICurrency.SelectedValue = "1";

            ddlBICurrency.DataSource = dtCurrency;
            ddlBICurrency.DataTextField = "CurrencyName";
            ddlBICurrency.DataValueField = "CurrencyCode";
            ddlBICurrency.DataBind();
            ddlBICurrency.SelectedValue = "1";

            ddlGSCurrency.DataSource = dtCurrency;
            ddlGSCurrency.DataTextField = "CurrencyName";
            ddlGSCurrency.DataValueField = "CurrencyCode";
            ddlGSCurrency.DataBind();
            ddlGSCurrency.SelectedValue = "1";

            ddlOSICurrency.DataSource = dtCurrency;
            ddlOSICurrency.DataTextField = "CurrencyName";
            ddlOSICurrency.DataValueField = "CurrencyCode";
            ddlOSICurrency.DataBind();
            ddlOSICurrency.SelectedValue = "1";

            ddlPICurrency.DataSource = dtCurrency;
            ddlPICurrency.DataTextField = "CurrencyName";
            ddlPICurrency.DataValueField = "CurrencyCode";
            ddlPICurrency.DataBind();
            ddlPICurrency.SelectedValue = "1";

            ddlRICurrency.DataSource = dtCurrency;
            ddlRICurrency.DataTextField = "CurrencyName";
            ddlRICurrency.DataValueField = "CurrencyCode";
            ddlRICurrency.DataBind();
            ddlRICurrency.SelectedValue = "1";

            ddlTHSCurrency.DataSource = dtCurrency;
            ddlTHSCurrency.DataTextField = "CurrencyName";
            ddlTHSCurrency.DataValueField = "CurrencyCode";
            ddlTHSCurrency.DataBind();
            ddlTHSCurrency.SelectedValue = "1";

            ddlTotal.DataSource = dtCurrency;
            ddlTotal.DataTextField = "CurrencyName";
            ddlTotal.DataValueField = "CurrencyCode";
            ddlTotal.DataBind();
            ddlTotal.SelectedValue = "1";
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            txttotal.Visible = true;
            txttotalyear.Visible = true;
            lbltotal.Visible = true;
            ddlTotal.Visible = true;
            try
            {

                customerIncomeVo.dateOfEntry = Convert.ToDateTime(txtDateOfEntry.Text.ToString());
                if (ddlRIProperty.SelectedValue.ToString() != "Pick a Property")
                    customerIncomeVo.rentalPropAccountId = int.Parse(ddlRIProperty.SelectedValue.ToString());
                else
                    customerIncomeVo.rentalPropAccountId = 0;
                customerIncomeVo.grossSalary = Double.Parse(txtGSMonthly.Text.ToString());
                customerIncomeVo.grossSalaryYr = Double.Parse(txtGSYearly.Text.ToString());
                customerIncomeVo.currencyCodeGrossSalary = int.Parse(ddlGSCurrency.SelectedValue.ToString());
                customerIncomeVo.takeHomeSalary = Double.Parse(txtTHSMonthly.Text.ToString());
                customerIncomeVo.takeHomeSalaryYr = Double.Parse(txtTHSYearly.Text.ToString());
                customerIncomeVo.currencyCodeTakeHomeSalary = int.Parse(ddlTHSCurrency.SelectedValue.ToString());
                customerIncomeVo.rentalIncome = Double.Parse(txtRIMonthly.Text.ToString());
                customerIncomeVo.rentalIncomeYr = Double.Parse(txtRIYearly.Text.ToString());
                customerIncomeVo.currencyCodeRentalIncome = int.Parse(ddlRICurrency.SelectedValue.ToString());
                customerIncomeVo.pensionIncome = Double.Parse(txtPIMonthly.Text.ToString());
                customerIncomeVo.pensionIncomeYr = Double.Parse(txtPIYearly.Text.ToString());
                customerIncomeVo.currencyCodePensionIncome = int.Parse(ddlPICurrency.SelectedValue.ToString());
                customerIncomeVo.AgriculturalIncome = Double.Parse(txtAIMonthly.Text.ToString());
                customerIncomeVo.AgriculturalIncomeYr = Double.Parse(txtAIYearly.Text.ToString());
                customerIncomeVo.currencyCodeAgriIncome = int.Parse(ddlAICurrency.SelectedValue.ToString());
                customerIncomeVo.businessIncome = Double.Parse(txtBIMonthly.Text.ToString());
                customerIncomeVo.businessIncomeYr = Double.Parse(txtBIYearly.Text.ToString());
                customerIncomeVo.currencyCodeBusinessIncome = int.Parse(ddlBICurrency.SelectedValue.ToString());
                customerIncomeVo.otherSourceIncome = Double.Parse(txtOSIMonthly.Text.ToString());
                customerIncomeVo.otherSourceIncomeYr = Double.Parse(txtOSIYearly.Text.ToString());
                customerIncomeVo.currencyCodeOtherIncome = int.Parse(ddlOSICurrency.SelectedValue.ToString());
                txttotal.Text = (decimal.Parse(txtGSMonthly.Text) + decimal.Parse(txtRIMonthly.Text) + decimal.Parse(txtPIMonthly.Text) + decimal.Parse(txtAIMonthly.Text) + decimal.Parse(txtBIMonthly.Text) + decimal.Parse(txtOSIMonthly.Text)).ToString();
                txttotalyear.Text = (decimal.Parse(txtGSYearly.Text) + decimal.Parse(txtRIYearly.Text) + decimal.Parse(txtPIYearly.Text) + decimal.Parse(txtAIYearly.Text) + decimal.Parse(txtBIYearly.Text) + decimal.Parse(txtOSIYearly.Text)).ToString();
                if (btnSave.Text == "Save")
                {
                    customerBo.AddCustomerIncomeDetails(rmVo.UserId, customerVo.CustomerId, customerIncomeVo);
                    DisableAllControls();
                    btnSave.Visible = false;
                    btnEdit.Visible = true;
                }
                else
                {
                    customerBo.UpdateCustomerIncomeDetails(rmVo.UserId, customerVo.CustomerId, customerIncomeVo);
                    DisableAllControls();
                    btnSave.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerIncome.ascx:btnSave_Click()");
                object[] objects = new object[2];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void btnEdit_Click(object sender, EventArgs e)
        {
            EnableAllControls();
            btnSave.Text = "Update";
            btnSave.Visible = true;
            txttotal.Visible = true;
            txttotalyear.Visible = true;
            lbltotal.Visible = true;
            ddlTotal.Visible = true;
            txttotal.Text = (decimal.Parse(txtGSMonthly.Text) + decimal.Parse(txtRIMonthly.Text) + decimal.Parse(txtPIMonthly.Text) + decimal.Parse(txtAIMonthly.Text) + decimal.Parse(txtBIMonthly.Text) + decimal.Parse(txtOSIMonthly.Text)).ToString();
            txttotalyear.Text = (decimal.Parse(txtGSYearly.Text) + decimal.Parse(txtRIYearly.Text) + decimal.Parse(txtPIYearly.Text) + decimal.Parse(txtAIYearly.Text) + decimal.Parse(txtBIYearly.Text) + decimal.Parse(txtOSIYearly.Text)).ToString();
        }

        public void GetCustomerIncomeDetails(int customerId)
        {
            try
            {
                DataTable dtIncomeDetails = new DataTable();
                CustomerBo customerBo = new CustomerBo();
                dtIncomeDetails = customerBo.GetCustomerIncomeDetails(customerId);
                if (dtIncomeDetails.Rows.Count == 0)
                {
                    btnEdit.Visible = false;
                    btnSave.Text = "Save";
                    EnableAllControls();
                    txttotal.Visible = true;
                    txttotalyear.Visible = true;

                }
                else
                {
                    txttotal.Visible = true;
                    txttotalyear.Visible = true;
                     if (dtIncomeDetails.Rows[0]["CI_AgriculturalIncome"].ToString() != "")
                    {
                        txtAIMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_AgriculturalIncome"].ToString()));
                        txtAIYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_AgriculturalIncome"].ToString()) * 12).ToString();
                        ddlAICurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeAgriculturalIncome"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_GrossSalary"].ToString() != "")
                    {
                        txtGSMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_GrossSalary"].ToString()));
                        txtGSYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_GrossSalary"].ToString()) * 12).ToString();
                        ddlGSCurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeGrossSalary"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_TakeHomeSalary"].ToString() != "")
                    {
                        txtTHSMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_TakeHomeSalary"].ToString()));
                        txtTHSYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_TakeHomeSalary"].ToString()) * 12).ToString();
                        ddlTHSCurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeTakeHomeSalary"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_RentalIncome"].ToString() != "")
                    {
                        txtRIMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_RentalIncome"].ToString()));
                        txtRIYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_RentalIncome"].ToString()) * 12).ToString();
                        ddlRICurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeRentalIncome"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_PensionIncome"].ToString() != "")
                    {
                        txtPIMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_PensionIncome"].ToString()));
                        txtPIYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_PensionIncome"].ToString()) * 12).ToString();
                        ddlPICurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodePensionIncome"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_BusinessIncome"].ToString() != "")
                    {
                        txtBIMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_BusinessIncome"].ToString()));
                        txtBIYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_BusinessIncome"].ToString()) * 12).ToString();
                        ddlBICurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeBusinessIncome"].ToString();
                    }
                    if (dtIncomeDetails.Rows[0]["CI_OtherSourceIncome"].ToString() != "")
                    {
                        txtOSIMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_OtherSourceIncome"].ToString()));
                        txtOSIYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtIncomeDetails.Rows[0]["CI_OtherSourceIncome"].ToString()) * 12).ToString();
                        ddlOSICurrency.SelectedValue = dtIncomeDetails.Rows[0]["XC_CurrencyCodeOtherSourceIncome"].ToString();
                    }
                    txttotal.Text = (decimal.Parse(txtGSMonthly.Text) + decimal.Parse(txtRIMonthly.Text) + decimal.Parse(txtPIMonthly.Text) + decimal.Parse(txtAIMonthly.Text) + decimal.Parse(txtBIMonthly.Text) + decimal.Parse(txtOSIMonthly.Text)).ToString();

                    txttotalyear.Text = (decimal.Parse(txtGSYearly.Text) + decimal.Parse(txtRIYearly.Text) + decimal.Parse(txtPIYearly.Text) + decimal.Parse(txtAIYearly.Text) + decimal.Parse(txtBIYearly.Text) + decimal.Parse(txtOSIYearly.Text)).ToString();
                  
                    if (dtIncomeDetails.Rows[0]["CI_DateOfEntry"].ToString() != "")
                        txtDateOfEntry.Text = DateTime.Parse(dtIncomeDetails.Rows[0]["CI_DateOfEntry"].ToString()).ToShortDateString();
                    if (dtIncomeDetails.Rows[0]["CPA_AccountId"].ToString() != "")
                        ddlRIProperty.SelectedValue = dtIncomeDetails.Rows[0]["CPA_AccountId"].ToString();
                    else
                        ddlRIProperty.SelectedValue = "Pick a Property";

                    btnEdit.Visible = true;
                    btnSave.Text = "Update";
                    btnSave.Visible = false;
                    DisableAllControls();
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
                FunctionInfo.Add("Method", "CustomerIncome.ascx:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void BindPropertyDropDown(int customerId)
        {

            dtProperties = customerBo.GetCustomerPropertyDetails(customerId);
            ddlRIProperty.DataSource = dtProperties;
            ddlRIProperty.DataTextField = "CPNP_Name";
            ddlRIProperty.DataValueField = "CPA_AccountId";
            ddlRIProperty.DataBind();
            ddlRIProperty.Items.Insert(0, new ListItem("Pick a Property", "Pick a Property"));
            ddlRIProperty.Enabled = false;

        }

        public void DisableAllControls()
        {
            txtDateOfEntry.Enabled = false;
            ddlRIProperty.Enabled = false;
            txtGSMonthly.Enabled = false;
            ddlGSCurrency.Enabled = false;
            txtGSYearly.Enabled = false;
            txtTHSMonthly.Enabled = false;
            ddlTHSCurrency.Enabled = false;
            txtTHSYearly.Enabled = false;
            txtRIMonthly.Enabled = false;
            ddlRICurrency.Enabled = false;
            txtRIYearly.Enabled = false;
            txtPIMonthly.Enabled = false;
            ddlPICurrency.Enabled = false;
            txtPIYearly.Enabled = false;
            txtAIMonthly.Enabled = false;
            ddlAICurrency.Enabled = false;
            txtAIYearly.Enabled = false;
            txtBIMonthly.Enabled = false;
            ddlBICurrency.Enabled = false;
            txtBIYearly.Enabled = false;
            txtOSIMonthly.Enabled = false;
            ddlOSICurrency.Enabled = false;
            txtOSIYearly.Enabled = false;
            btnSave.Enabled = false;
            ddlTotal.Enabled = false;
            //txttotal.Enabled = false;
            //txttotalyear.Enabled = false;
        }

        public void EnableAllControls()
        {
            txtDateOfEntry.Enabled = true;
            //ddlRIProperty.Enabled = true;
            txtGSMonthly.Enabled = true;
            ddlGSCurrency.Enabled = true;
            txtGSYearly.Enabled = true;
            txtTHSMonthly.Enabled = true;
            ddlTHSCurrency.Enabled = true;
            txtTHSYearly.Enabled = true;
            txtRIMonthly.Enabled = true;
            ddlRICurrency.Enabled = true;
            txtRIYearly.Enabled = true;
            txtPIMonthly.Enabled = true;
            ddlPICurrency.Enabled = true;
            txtPIYearly.Enabled = true;
            txtAIMonthly.Enabled = true;
            ddlAICurrency.Enabled = true;
            txtAIYearly.Enabled = true;
            txtBIMonthly.Enabled = true;
            ddlBICurrency.Enabled = true;
            txtBIYearly.Enabled = true;
            txtOSIMonthly.Enabled = true;
            ddlOSICurrency.Enabled = true;
            txtOSIYearly.Enabled = true;
            btnSave.Enabled = true;
            ddlTotal.Enabled = true;

        }

        public void InitializeTextBoxes()
        {
            txtAIMonthly.Text = "0.00";
            txtAIYearly.Text = "0.00";
            txtBIMonthly.Text = "0.00";
            txtBIYearly.Text = "0.00";
            txtGSMonthly.Text = "0.00";
            txtGSYearly.Text = "0.00";
            txtOSIMonthly.Text = "0.00";
            txtOSIYearly.Text = "0.00";
            txtPIMonthly.Text = "0.00";
            txtPIYearly.Text = "0.00";
            txtRIMonthly.Text = "0.00";
            txtRIYearly.Text = "0.00";
            txtTHSMonthly.Text = "0.00";
            txtTHSYearly.Text = "0.00";
            txttotal.Text = "0.00";
            txttotalyear.Text = "0.00";
            txtDateOfEntry.Text = DateTime.Now.ToShortDateString();
        }
   
    }
}
