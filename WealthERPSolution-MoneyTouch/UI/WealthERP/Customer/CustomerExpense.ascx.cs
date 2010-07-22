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
    public partial class ViewCustomerExpense : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();
        DateBo dateBo = new DateBo();

        DataTable dtCurrency = new DataTable();
        DataTable dtProperties = new DataTable();


        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerExpenseVo customerExpenseVo = new CustomerExpenseVo();
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
            this.Page.Culture = "en-GB";
            try
            {
                rmVo = (RMVo)Session["rmVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                if (!IsPostBack)
                {
                    BindDropDowns(path);
                    InitializeTextBoxes();
                    GetCustomerExpenseDetails(customerVo.CustomerId);
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
        /// <summary>
        /// Bind all the Drop down for Customer Expense Screen
        /// </summary>
        /// <param name="path"></param>
        public void BindDropDowns(string path)
        {
            dtCurrency = XMLBo.GetCurrency(path);
            ddlTranCurrency.DataSource = dtCurrency;
            ddlTranCurrency.DataTextField = "CurrencyName";
            ddlTranCurrency.DataValueField = "CurrencyCode";
            ddlTranCurrency.DataBind();
            ddlTranCurrency.SelectedValue = "1";

            ddlFoodCurrency.DataSource = dtCurrency;
            ddlFoodCurrency.DataTextField = "CurrencyName";
            ddlFoodCurrency.DataValueField = "CurrencyCode";
            ddlFoodCurrency.DataBind();
            ddlFoodCurrency.SelectedValue = "1";

            ddlCloCurrency.DataSource = dtCurrency;
            ddlCloCurrency.DataTextField = "CurrencyName";
            ddlCloCurrency.DataValueField = "CurrencyCode";
            ddlCloCurrency.DataBind();
            ddlCloCurrency.SelectedValue = "1";

            ddlHomeCurrency.DataSource = dtCurrency;
            ddlHomeCurrency.DataTextField = "CurrencyName";
            ddlHomeCurrency.DataValueField = "CurrencyCode";
            ddlHomeCurrency.DataBind();
            ddlHomeCurrency.SelectedValue = "1";

            ddlUtiCurrency.DataSource = dtCurrency;
            ddlUtiCurrency.DataTextField = "CurrencyName";
            ddlUtiCurrency.DataValueField = "CurrencyCode";
            ddlUtiCurrency.DataBind();
            ddlUtiCurrency.SelectedValue = "1";

            ddlSCCurrency.DataSource = dtCurrency;
            ddlSCCurrency.DataTextField = "CurrencyName";
            ddlSCCurrency.DataValueField = "CurrencyCode";
            ddlSCCurrency.DataBind();
            ddlSCCurrency.SelectedValue = "1";

            ddlHCCurrency.DataSource = dtCurrency;
            ddlHCCurrency.DataTextField = "CurrencyName";
            ddlHCCurrency.DataValueField = "CurrencyCode";
            ddlHCCurrency.DataBind();
            ddlHCCurrency.SelectedValue = "1";

            ddlEduCurrency.DataSource = dtCurrency;
            ddlEduCurrency.DataTextField = "CurrencyName";
            ddlEduCurrency.DataValueField = "CurrencyCode";
            ddlEduCurrency.DataBind();
            ddlEduCurrency.SelectedValue = "1";

            ddlPetsCurrency.DataSource = dtCurrency;
            ddlPetsCurrency.DataTextField = "CurrencyName";
            ddlPetsCurrency.DataValueField = "CurrencyCode";
            ddlPetsCurrency.DataBind();
            ddlPetsCurrency.SelectedValue = "1";

            ddlEntCurrency.DataSource = dtCurrency;
            ddlEntCurrency.DataTextField = "CurrencyName";
            ddlEntCurrency.DataValueField = "CurrencyCode";
            ddlEntCurrency.DataBind();
            ddlEntCurrency.SelectedValue = "1";

            ddlMisCurrency.DataSource = dtCurrency;
            ddlMisCurrency.DataTextField = "CurrencyName";
            ddlMisCurrency.DataValueField = "CurrencyCode";
            ddlMisCurrency.DataBind();
            ddlMisCurrency.SelectedValue = "1";

            ddlTotal.DataSource = dtCurrency;
            ddlTotal.DataTextField = "CurrencyName";
            ddlTotal.DataValueField = "CurrencyCode";
            ddlTotal.DataBind();
            ddlTotal.SelectedValue = "1";
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

 //               customerExpenseVo.DateOfEntry = DateBo.GetFormattedDate(Convert.ToDateTime(txtDateOfEntry.Text.ToString()),"en-GB");
                customerExpenseVo.DateOfEntry = Convert.ToDateTime(txtDateOfEntry.Text.ToString());
                customerExpenseVo.Transportation = Double.Parse(txtTranMonthly.Text.ToString());
                customerExpenseVo.TransportationYr = Double.Parse(txtTranYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeTransportation = int.Parse(ddlTranCurrency.SelectedValue.ToString());
                customerExpenseVo.Food = Double.Parse(txtFoodMonthly.Text.ToString());
                customerExpenseVo.FoodYr = Double.Parse(txtFoodYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeFood = int.Parse(ddlFoodCurrency.SelectedValue.ToString());
                customerExpenseVo.Clothing = Double.Parse(txtCloMonthly.Text.ToString());
                customerExpenseVo.ClothingYr = Double.Parse(txtCloYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeClothing = int.Parse(ddlCloCurrency.SelectedValue.ToString());
                customerExpenseVo.Home = Double.Parse(txtHomeMonthly.Text.ToString());
                customerExpenseVo.HomeYr = Double.Parse(txtHomeYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeHome = int.Parse(ddlHomeCurrency.SelectedValue.ToString());
                customerExpenseVo.Utilities = Double.Parse(txtUtiMonthly.Text.ToString());
                customerExpenseVo.UtilitiesYr = Double.Parse(txtUtiYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeUtilities = int.Parse(ddlUtiCurrency.SelectedValue.ToString());
                customerExpenseVo.SelfCare = Double.Parse(txtSCMonthly.Text.ToString());
                customerExpenseVo.SelfCareYr = Double.Parse(txtSCYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeSelfCare = int.Parse(ddlSCCurrency.SelectedValue.ToString());
                customerExpenseVo.HealthCare = Double.Parse(txtHCMonthly.Text.ToString());
                customerExpenseVo.HealthCareYr = Double.Parse(txtHCYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeHealthCare = int.Parse(ddlHCCurrency.SelectedValue.ToString());
                customerExpenseVo.Education = Double.Parse(txtEduMonthly.Text.ToString());
                customerExpenseVo.EducationYr = Double.Parse(txtEduYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeEducation = int.Parse(ddlEduCurrency.SelectedValue.ToString());
                customerExpenseVo.Pets = Double.Parse(txtPetsMonthly.Text.ToString());
                customerExpenseVo.PetsYr = Double.Parse(txtPetsYearly.Text.ToString());
                customerExpenseVo.CurrencyCodePets = int.Parse(ddlPetsCurrency.SelectedValue.ToString());
                customerExpenseVo.Entertainment = Double.Parse(txtEntMonthly.Text.ToString());
                customerExpenseVo.EntertainmentYr = Double.Parse(txtEntYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeEntertainment = int.Parse(ddlEntCurrency.SelectedValue.ToString());
                customerExpenseVo.Miscellaneous = Double.Parse(txtMisMonthly.Text.ToString());
                customerExpenseVo.MiscellaneousYr = Double.Parse(txtMisYearly.Text.ToString());
                customerExpenseVo.CurrencyCodeMiscellaneous = int.Parse(ddlMisCurrency.SelectedValue.ToString());
                txttotal.Text = (double.Parse(txtTranMonthly.Text) + double.Parse(txtFoodMonthly.Text) + double.Parse(txtCloMonthly.Text) + double.Parse(txtHomeMonthly.Text) + double.Parse(txtUtiMonthly.Text) + double.Parse(txtSCMonthly.Text) + double.Parse(txtHCMonthly.Text) + double.Parse(txtEduMonthly.Text) + double.Parse(txtPetsMonthly.Text) + double.Parse(txtEntMonthly.Text) + double.Parse(txtMisMonthly.Text)).ToString();
                txttotalyear.Text = (double.Parse(txtTranYearly.Text) + double.Parse(txtFoodYearly.Text) + double.Parse(txtCloYearly.Text) + double.Parse(txtHomeYearly.Text) + double.Parse(txtUtiYearly.Text) + double.Parse(txtSCYearly.Text) + double.Parse(txtHCYearly.Text) + double.Parse(txtEduYearly.Text) + double.Parse(txtPetsYearly.Text) + double.Parse(txtEntYearly.Text) + double.Parse(txtMisYearly.Text)).ToString();
                txttotalyear.Visible = true;
                txttotal.Visible = true;
                lbltotal.Visible = true;
                ddlTotal.Visible = true;
                if (btnSave.Text == "Save")
                {
                    customerBo.AddCustomerExpenseDetails(rmVo.UserId, customerVo.CustomerId, customerExpenseVo);
                    DisableAllControls();
                    btnSave.Visible = false;
                    btnEdit.Visible = true;
                    txttotalyear.Visible = true;
                    txttotal.Visible = true;
                    lbltotal.Visible = true;
                    ddlTotal.Visible = true;
                }
                else
                {
                    customerBo.UpdateCustomerExpenseDetails(rmVo.UserId, customerVo.CustomerId, customerExpenseVo);
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
                FunctionInfo.Add("Method", "CustomerExpense.ascx:btnSave_Click()");
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
            txttotalyear.Visible = false;
            txttotal.Visible = true;
            lbltotal.Visible = true;
            ddlTotal.Visible = true;
            txttotal.Text = (double.Parse(txtTranMonthly.Text) + double.Parse(txtFoodMonthly.Text) + double.Parse(txtCloMonthly.Text) + double.Parse(txtHomeMonthly.Text) + double.Parse(txtUtiMonthly.Text) + double.Parse(txtSCMonthly.Text) + double.Parse(txtHCMonthly.Text) + double.Parse(txtEduMonthly.Text) + double.Parse(txtPetsMonthly.Text) + double.Parse(txtEntMonthly.Text) + double.Parse(txtMisMonthly.Text)).ToString();
            txttotalyear.Text = (double.Parse(txtTranYearly.Text) + double.Parse(txtFoodYearly.Text) + double.Parse(txtCloYearly.Text) + double.Parse(txtHomeYearly.Text) + double.Parse(txtUtiYearly.Text) + double.Parse(txtSCYearly.Text) + double.Parse(txtHCYearly.Text) + double.Parse(txtEduYearly.Text) + double.Parse(txtPetsYearly.Text) + double.Parse(txtEntYearly.Text) + double.Parse(txtMisYearly.Text)).ToString();
        }
        /// <summary>
        /// It will get the Customer Details and returns their Expense details based on that Customer
        /// </summary>
        /// <param name="customerId"></param>
        public void GetCustomerExpenseDetails(int customerId)
        {
            try
            {
                DataTable dtExpenseDetails = new DataTable();
                CustomerBo customerBo = new CustomerBo();
                dtExpenseDetails = customerBo.GetCustomerExpenseDetails(customerId);
                if (dtExpenseDetails.Rows.Count == 0)
                {
                    btnEdit.Visible = false;
                    btnSave.Text = "Save";
                    EnableAllControls();

                }
                else
                {
                    if (dtExpenseDetails.Rows[0]["CE_Transportation"].ToString() != "")
                    {
                        txtTranMonthly.Text = String.Format("{0:0.00}",decimal.Parse(dtExpenseDetails.Rows[0]["CE_Transportation"].ToString()));
                        txtTranYearly.Text = String.Format("{0:0.00}",decimal.Parse(dtExpenseDetails.Rows[0]["CE_Transportation"].ToString()) * 12).ToString();
                        ddlTranCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeTransportation"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Food"].ToString() != "")
                    {
                        txtFoodMonthly.Text = String.Format("{0:0.00}",decimal.Parse(dtExpenseDetails.Rows[0]["CE_Food"].ToString()));
                        txtFoodYearly.Text = String.Format("{0:0.00}",decimal.Parse(dtExpenseDetails.Rows[0]["CE_Food"].ToString()) * 12).ToString();
                        ddlFoodCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeFood"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Clothing"].ToString() != "")
                    {
                        txtCloMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Clothing"].ToString()));
                        txtCloYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Clothing"].ToString()) * 12).ToString();
                        ddlCloCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeClothing"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Home"].ToString() != "")
                    {
                        txtHomeMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Home"].ToString()));
                        txtHomeYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Home"].ToString()) * 12).ToString();
                        ddlHomeCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeHome"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Utilities"].ToString() != "")
                    {
                        txtUtiMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Utilities"].ToString()));
                        txtUtiYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Utilities"].ToString()) * 12).ToString();
                        ddlUtiCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeUtilities"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_SelfCare"].ToString() != "")
                    {
                        txtSCMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_SelfCare"].ToString()));
                        txtSCYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_SelfCare"].ToString()) * 12).ToString();
                        ddlSCCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeSelfCare"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_HealthCare"].ToString() != "")
                    {
                        txtHCMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_HealthCare"].ToString()));
                        txtHCYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_HealthCare"].ToString()) * 12).ToString();
                        ddlHCCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeHealthCare"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Education"].ToString() != "")
                    {
                        txtEduMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Education"].ToString()));
                        txtEduYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Education"].ToString()) * 12).ToString();
                        ddlEduCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeEducation"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Pets"].ToString() != "")
                    {
                        txtPetsMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Pets"].ToString()));
                        txtPetsYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Pets"].ToString()) * 12).ToString();
                        ddlPetsCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodePets"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Entertainment"].ToString() != "")
                    {
                        txtEntMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Entertainment"].ToString()));
                        txtEntYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Entertainment"].ToString()) * 12).ToString();
                        ddlEntCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeEntertainment"].ToString();
                    }
                    if (dtExpenseDetails.Rows[0]["CE_Miscellaneous"].ToString() != "")
                    {
                        txtMisMonthly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Miscellaneous"].ToString()));
                        txtMisYearly.Text = String.Format("{0:0.00}", decimal.Parse(dtExpenseDetails.Rows[0]["CE_Miscellaneous"].ToString()) * 12).ToString();
                        ddlMisCurrency.SelectedValue = dtExpenseDetails.Rows[0]["XC_CurrencyCodeMiscellaneous"].ToString();
                    }

                    txttotal.Text = (double.Parse(txtTranMonthly.Text) + double.Parse(txtFoodMonthly.Text) + double.Parse(txtCloMonthly.Text) + double.Parse(txtHomeMonthly.Text) + double.Parse(txtUtiMonthly.Text) + double.Parse(txtSCMonthly.Text) + double.Parse(txtHCMonthly.Text) + double.Parse(txtEduMonthly.Text) + double.Parse(txtPetsMonthly.Text) + double.Parse(txtEntMonthly.Text) + double.Parse(txtMisMonthly.Text)).ToString();


                    txttotalyear.Text = (double.Parse(txtTranYearly.Text) + double.Parse(txtFoodYearly.Text) + double.Parse(txtCloYearly.Text) + double.Parse(txtHomeYearly.Text) + double.Parse(txtUtiYearly.Text) + double.Parse(txtSCYearly.Text) + double.Parse(txtHCYearly.Text) + double.Parse(txtEduYearly.Text) + double.Parse(txtPetsYearly.Text) + double.Parse(txtEntYearly.Text) + double.Parse(txtMisYearly.Text)).ToString();


                    if (dtExpenseDetails.Rows[0]["CE_DateOfEntry"].ToString() != "")
                        txtDateOfEntry.Text = DateTime.Parse(dtExpenseDetails.Rows[0]["CE_DateOfEntry"].ToString()).ToShortDateString();

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
        /// <summary>
        /// Disable all controls of the Customer Expense Screen
        /// </summary>
        public void DisableAllControls()
        {
            txtDateOfEntry.Enabled = false;
            txtTranMonthly.Enabled = false;
            ddlTranCurrency.Enabled = false;
            txtTranYearly.Enabled = false;
            txtFoodMonthly.Enabled = false;
            ddlFoodCurrency.Enabled = false;
            txtFoodYearly.Enabled = false;
            txtCloMonthly.Enabled = false;
            ddlCloCurrency.Enabled = false;
            txtCloYearly.Enabled = false;
            txtHomeMonthly.Enabled = false;
            ddlHomeCurrency.Enabled = false;
            txtHomeYearly.Enabled = false;
            txtUtiMonthly.Enabled = false;
            ddlUtiCurrency.Enabled = false;
            txtUtiYearly.Enabled = false;
            txtSCMonthly.Enabled = false;
            ddlSCCurrency.Enabled = false;
            txtSCYearly.Enabled = false;
            txtHCMonthly.Enabled = false;
            ddlHCCurrency.Enabled = false;
            txtHCYearly.Enabled = false;
            txtEduMonthly.Enabled = false;
            ddlEduCurrency.Enabled = false;
            txtEduYearly.Enabled = false;
            txtPetsMonthly.Enabled = false;
            ddlPetsCurrency.Enabled = false;
            txtPetsYearly.Enabled = false;
            txtEntMonthly.Enabled = false;
            ddlEntCurrency.Enabled = false;
            txtEntYearly.Enabled = false;
            txtMisMonthly.Enabled = false;
            ddlMisCurrency.Enabled = false;
            txtMisYearly.Enabled = false;
            btnSave.Enabled = false;
            ddlTotal.Enabled = false;
            txttotal.Enabled = false;
            txttotalyear.Enabled = false;
        }
        /// <summary>
        /// Enable all controls in Customer Expense Screen
        /// </summary>
        public void EnableAllControls()
        {
            txtDateOfEntry.Enabled = true;
            txtTranMonthly.Enabled = true;
            ddlTranCurrency.Enabled = true;
            txtTranYearly.Enabled = true;
            txtFoodMonthly.Enabled = true;
            ddlFoodCurrency.Enabled = true;
            txtFoodYearly.Enabled = true;
            txtCloMonthly.Enabled = true;
            ddlCloCurrency.Enabled = true;
            txtCloYearly.Enabled = true;
            txtHomeMonthly.Enabled = true;
            ddlHomeCurrency.Enabled = true;
            txtHomeYearly.Enabled = true;
            txtUtiMonthly.Enabled = true;
            ddlUtiCurrency.Enabled = true;
            txtUtiYearly.Enabled = true;
            txtSCMonthly.Enabled = true;
            ddlSCCurrency.Enabled = true;
            txtSCYearly.Enabled = true;
            txtHCMonthly.Enabled = true;
            ddlHCCurrency.Enabled = true;
            txtHCYearly.Enabled = true;
            txtEduMonthly.Enabled = true;
            ddlEduCurrency.Enabled = true;
            txtEduYearly.Enabled = true;
            txtPetsMonthly.Enabled = true;
            ddlPetsCurrency.Enabled = true;
            txtPetsYearly.Enabled = true;
            txtEntMonthly.Enabled = true;
            ddlEntCurrency.Enabled = true;
            txtEntYearly.Enabled = true;
            txtMisMonthly.Enabled = true;
            ddlMisCurrency.Enabled = true;
            txtMisYearly.Enabled = true;
            btnSave.Enabled = true;
            ddlTotal.Enabled = true;
        }
        /// <summary>
        /// Initializes all the TextBox to "0.00"
        /// </summary>
        public void InitializeTextBoxes()
        {
            txtTranMonthly.Text = "0.00";
            txtTranYearly.Text = "0.00";
            txtFoodMonthly.Text = "0.00";
            txtFoodYearly.Text = "0.00";
            txtCloMonthly.Text = "0.00";
            txtCloYearly.Text = "0.00";
            txtHomeMonthly.Text = "0.00";
            txtHomeYearly.Text = "0.00";
            txtUtiMonthly.Text = "0.00";
            txtUtiYearly.Text = "0.00";
            txtSCMonthly.Text = "0.00";
            txtSCYearly.Text = "0.00";
            txtHCMonthly.Text = "0.00";
            txtHCYearly.Text = "0.00";
            txtEduMonthly.Text = "0.00";
            txtEduYearly.Text = "0.00";
            txtPetsMonthly.Text = "0.00";
            txtPetsYearly.Text = "0.00";
            txtEntMonthly.Text = "0.00";
            txtEntYearly.Text = "0.00";
            txtMisMonthly.Text = "0.00";
            txtMisYearly.Text = "0.00";
            txtDateOfEntry.Text =DateTime.Now.ToString("dd/MM/yyyy");
            txttotal.Text = "0.00";
            txttotalyear.Text = "0.00";
            //txtDateOfEntry.Text = DateTime.Now.ToShortDateString();
            //DateTime.Now.GetDateTimeFormats(
        }
    }
}