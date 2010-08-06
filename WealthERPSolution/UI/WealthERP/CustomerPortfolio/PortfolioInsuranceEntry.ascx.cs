using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using VoCustomerPortfolio;
using VoUser;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoCommon;
using System.Text;
using AjaxControlToolkit;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioInsuranceEntry : System.Web.UI.UserControl
    {
        CustomerVo customerVo;
        UserVo userVo;
        AssetBo assetBo = new AssetBo();

        InsuranceVo insuranceVo = new InsuranceVo();
        InsuranceBo insuranceBo = new InsuranceBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
        List<InsuranceULIPVo> insuranceUlipList = new List<InsuranceULIPVo>();
        List<MoneyBackEpisodeVo> moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
        CalendarExtender ce;
        TextBoxWatermarkExtender txtWE;

        int count;
        int insuranceId;
        string AssetGroupCode = "IN";
        string Manage = string.Empty;
        string path = "";

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    if (ddlUlipPlans.SelectedIndex != 0 && ddlUlipPlans.SelectedIndex != -1)
        //    {
        //        LoadUlipSubPlans(ddlUlipPlans.SelectedValue.ToString().Trim());
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            cvDepositDate1.ValueToCompare = DateTime.Now.ToShortDateString();
            try
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                userVo = (UserVo)Session["userVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];
                insuranceVo = (InsuranceVo)Session["insuranceVo"];


                if (Session["table"] != null)
                {
                    if (customerAccountVo.AssetCategory.ToString().Trim() == "INMP")
                    {
                        this.PlaceHolder2.Controls.Clear();
                        Table tb = (Table)Session["table"];
                        this.PlaceHolder2.Controls.Add(tb);
                    }
                    else
                    {
                        this.PlaceHolder1.Controls.Clear();
                        Table tb = (Table)Session["table"];
                        this.PlaceHolder1.Controls.Add(tb);
                    }
                }

                if (!IsPostBack)
                {
                    // Check Querystring to see if its an Edit/View/Entry
                    if (Request.QueryString["action"] != null)
                        Manage = Request.QueryString["action"].ToString();

                    ClearFields();
                    BindDropDowns(path, customerAccountVo.AssetCategory.ToString().Trim());
                    LoadInsuranceIssuerCode(path);

                    if (insuranceVo != null)
                    {
                        if (Manage == "edit")
                        {
                            SetControls("edit", insuranceVo, customerAccountVo, path);
                        }
                        else if (Manage == "view")
                        {
                            SetControls("view", insuranceVo, customerAccountVo, path);
                        }
                    }
                    else
                    {
                        SetControls("entry", insuranceVo, customerAccountVo, path);
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = path;
                objects[3] = customerAccountVo;
                objects[4] = insuranceVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void ClearFields()
        {
            trEditButton.Visible = false;
            trEditSpace.Visible = false;

            trEPPremiumAmount.Visible = false;
            trEPPremiumPeriod.Visible = false;
            trEPPremiumFirstLast.Visible = false;
            trEPGracePeriod.Visible = false;
            trEPBonus.Visible = false;
            trEPSurrenderValue.Visible = false;
            trEPRemarks.Visible = false;

            trWLPPremiumAmount.Visible = false;
            trWLPPremiumPeriod.Visible = false;
            trWLPPremiumFirstLast.Visible = false;
            trWLPGracePeriod.Visible = false;
            trWPBonus.Visible = false;
            trWPSurrenderValue.Visible = false;
            trWLPRemarks.Visible = false;

            trMPPremiumAmount.Visible = false;
            trMPPeriod.Visible = false;
            trMPGracePeriod.Visible = false;
            trMPPremiumFirstLast.Visible = false;
            trMPBonus.Visible = false;
            trMPSurrenderValue.Visible = false;
            trMPRemarks.Visible = false;
            trMPHeader.Visible = false;
            trMPPolicyTerm.Visible = false;
            trMPDetails.Visible = false;
            trMPSpace.Visible = false;

            trULIPPremiumCycle.Visible = false;
            trULIPGracePeriod.Visible = false;
            trULIPPremiumFirstLast.Visible = false;
            trULIPHeader.Visible = false;
            trULIPHeader.Visible = false;
            trULIPSchemeBasket.Visible = false;
            trULIPAllocation.Visible = false;
            pnlUlip.Visible = false;
            trULIPError.Visible = false;
            trULIPSurrenderValue.Visible = false;
            trULIPCharges.Visible = false;
            trULIPRemarks.Visible = false;

            trTPPremiumAmount.Visible = false;
            trTPPremiumPeriod.Visible = false;
            trTPPremiumFirstLast.Visible = false;
            trTPGracePeriod.Visible = false;
            trTPPremiumAccumn.Visible = false;
            trTPRemarks.Visible = false;

            trValuationHeader.Visible = false;

            trDeleteButton.Visible = false;
        }

        private void BindDropDowns(string path, string CategoryCode)
        {
            try
            {
                DataTable dt = XMLBo.GetFrequency(path);

                if (CategoryCode == "INEP")
                {
                    ddlEPPremiumFrequencyCode.DataSource = dt;
                    ddlEPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlEPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlEPPremiumFrequencyCode.DataBind();
                    ddlEPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));

                }
                else if (CategoryCode == "INMP")
                {
                    ddlMPPremiumFrequencyCode.DataSource = dt;
                    ddlMPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlMPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlMPPremiumFrequencyCode.DataBind();
                    ddlMPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));
                }
                else if (CategoryCode == "INTP")
                {
                    ddlTPPremiumFrequencyCode.DataSource = dt;
                    ddlTPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlTPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlTPPremiumFrequencyCode.DataBind();
                    ddlTPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));
                }
                else if (CategoryCode == "INUP")
                {
                    ddlULIPPremiumFrequencyCode.DataSource = dt;
                    ddlULIPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlULIPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlULIPPremiumFrequencyCode.DataBind();
                    ddlULIPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));
                }
                else if (CategoryCode == "INWP")
                {
                    ddlWLPPremiumFrequencyCode.DataSource = dt;
                    ddlWLPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlWLPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlWLPPremiumFrequencyCode.DataBind();
                    ddlWLPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:BindDropDowns()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = CategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadInsuranceIssuerCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetInsuranceIssuer(path);
                ddlInsuranceIssuerCode.DataSource = dt;
                ddlInsuranceIssuerCode.DataTextField = dt.Columns["InsuranceIsserName"].ToString();
                ddlInsuranceIssuerCode.DataValueField = dt.Columns["InsuranceIssuerCode"].ToString();
                ddlInsuranceIssuerCode.DataBind();
                ddlInsuranceIssuerCode.Items.Insert(0, new ListItem("Select an Insurance Issuer", "Select an Insurance Issuer"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:LoadInsuranceIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void SetControls(string action, InsuranceVo insuranceVo, CustomerAccountsVo customerAccountVo, string path)
        {
            List<InsuranceULIPVo> insuranceULIPList = new List<InsuranceULIPVo>();
            try
            {

                // Loading Account Details Here
                lblInsCategory.Text = customerAccountVo.AssetCategoryName.ToString();
                txtPolicyNumber.Text = customerAccountVo.PolicyNum.ToString();
                // Joint Holders
                // Nominees

                //EnableDisableControlsAndCells(action);

                if (action == "entry")
                {
                    ClearControlValues(customerAccountVo.AssetCategory.ToString().Trim());
                    EnableDisableControls(action, customerAccountVo.AssetCategory.ToString().Trim());
                }
                else
                {
                    /*****
                     * 
                     * Note:: The Dynamic Controls Need to Be regenerated here
                     * 
                     * *****/

                    // Bind values to the respective controls
                    txtName.Text = insuranceVo.Name.Trim();
                    ddlInsuranceIssuerCode.SelectedValue = insuranceVo.InsuranceIssuerCode.Trim();
                    txtPolicyCommencementDate.Text = insuranceVo.StartDate.ToShortDateString();
                    txtPolicyMaturity.Text = insuranceVo.EndDate.ToShortDateString();
                    txtSumAssured.Text = insuranceVo.SumAssured.ToString();
                    txtApplicationNumber.Text = insuranceVo.ApplicationNumber.ToString();
                    if (insuranceVo.ApplicationDate != DateTime.MinValue)
                        txtApplDate.Text = insuranceVo.ApplicationDate.ToShortDateString();

                    if (customerAccountVo.AssetCategory.Trim() == "INEP")
                    {
                        txtEPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlEPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtEPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        txtEPPrPayDate.Text = insuranceVo.PremiumPaymentDate.ToString();
                        txtLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtEPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtEPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtEPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtEPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtEPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtEPRemarks.Text = insuranceVo.Remarks.ToString();

                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INMP")
                    {
                        txtMPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlMPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtMPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        txtMPPrPayDate.Text = insuranceVo.PremiumPaymentDate.ToString();
                        txtMPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtMPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtMPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtMoneyBackEpisode.Text = Int32.Parse(insuranceVo.PolicyEpisode.ToString()).ToString();
                        txtMPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtMPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtMPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtMPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtMPRemarks.Text = insuranceVo.Remarks.ToString();
                        txtMPPolicyTerm.Text = insuranceVo.PolicyPeriod.ToString();
                        count = 0;
                        Int32.TryParse(txtMoneyBackEpisode.Text.Trim(), out count);
                        // This Method Loads dynamic tables based on count
                        ShowMoneyBackContent(count);

                        List<MoneyBackEpisodeVo> moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                        moneyBackEpisodeList = insuranceBo.GetMoneyBackEpisodeList(insuranceVo.CustInsInvId);
                        Session["moneyBackEpisodeList"] = moneyBackEpisodeList;
                        MoneyBackEpisodeVo moneyBackEpisodeVo;

                        if (moneyBackEpisodeList != null)
                        {
                            for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                            {
                                moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                moneyBackEpisodeVo = moneyBackEpisodeList[i];

                                TextBox txtBox1 = new TextBox();
                                txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString()));
                                txtBox1.Text = moneyBackEpisodeVo.CIMBE_RepaymentDate.ToShortDateString();

                                TextBox txtBox2 = new TextBox();
                                txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString()));
                                txtBox2.Text = moneyBackEpisodeVo.CIMBE_RepaidPer.ToString();
                            }
                        }

                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INTP")
                    {
                        txtTPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlTPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtTPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        txtTPPrPayDate.Text = insuranceVo.PremiumPaymentDate.ToString();
                        txtTPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtTPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtTPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtTPPremiumAccum.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtWLPRemarks.Text = insuranceVo.Remarks.ToString();
                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INUP")
                    {
                        ddlULIPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtULIPPrPayDate.Text = insuranceVo.PremiumPaymentDate.ToString();
                        txtULIPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtULIPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtULIPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        LoadUlipPlan();
                        // Get the ULIP Plan Code from CustomerInsuraceULIPPlan Table
                        DataSet dsUlipPlanCode = insuranceBo.GetUlipPlanCode(insuranceVo.CustInsInvId);
                        ddlUlipPlans.SelectedValue = dsUlipPlanCode.Tables[0].Rows[0]["WUP_ULIPPlanCode"].ToString().Trim();

                        // Bind ULIP Sub-Plans
                        LoadUlipSubPlans(ddlUlipPlans.SelectedValue.ToString().Trim());


                        insuranceULIPList = insuranceBo.GetInsuranceULIPList(insuranceVo.CustInsInvId);
                        Session["insuranceULIPList"] = insuranceULIPList;
                        InsuranceULIPVo insuranceULIPVo;

                        for (int i = 0; i < insuranceULIPList.Count; i++)
                        {
                            insuranceULIPVo = new InsuranceULIPVo();
                            insuranceULIPVo = insuranceULIPList[i];

                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtSubPlanId" + i.ToString()));
                            txtBox1.Text = insuranceULIPVo.WUP_ULIPSubPlaCode.ToString();

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtUnitsId" + i.ToString()));
                            txtBox2.Text = insuranceULIPVo.CIUP_Unit.ToString();

                            TextBox txtBox3 = new TextBox();
                            txtBox3 = ((TextBox)PlaceHolder2.FindControl("txtPurchasePriceId" + i.ToString()));
                            txtBox3.Text = insuranceULIPVo.CIUP_PurchasePrice.ToString();

                            TextBox txtBox4 = new TextBox();
                            txtBox4 = ((TextBox)PlaceHolder2.FindControl("txtPurchaseDateId" + i.ToString()));
                            txtBox4.Text = insuranceULIPVo.CIUP_PurchaseDate.ToShortDateString();

                            TextBox txtBox5 = new TextBox();
                            txtBox5 = ((TextBox)PlaceHolder2.FindControl("txtAllocationId" + i.ToString()));
                            txtBox5.Text = insuranceULIPVo.CIUP_AllocationPer.ToString();
                        }

                        txtULIPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtULIPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtULIPCharges.Text = insuranceVo.ULIPCharges.ToString();
                        txtULIPRemarks.Text = insuranceVo.Remarks.ToString();
                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INWP")
                    {
                        txtWLPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlWLPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtWLPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        txtWLPPrPayDate.Text = insuranceVo.PremiumPaymentDate.ToString();
                        txtWLPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtWLPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtWLPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtWPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtWPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtWPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtWPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtWLPRemarks.Text = insuranceVo.Remarks.ToString();
                    }

                    // Enable/Disable Controls
                    EnableDisableControls(action, customerAccountVo.AssetCategory.ToString().Trim());
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:SetControls()");
                object[] objects = new object[5];
                objects[0] = insuranceVo;
                objects[1] = action;
                objects[2] = customerAccountVo;
                objects[3] = path;
                objects[4] = insuranceULIPList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void ClearControlValues(string CategoryCode)
        {

            try
            {
                txtName.Text = "";
                ddlInsuranceIssuerCode.SelectedIndex = 0;
                txtPolicyCommencementDate.Text = "";
                txtPolicyMaturity.Text = "";
                txtSumAssured.Text = "";
                txtApplicationNumber.Text = "";
                txtApplDate.Text = "";

                if (CategoryCode == "INEP")
                {
                    txtEPPremiumAmount.Text = "";
                    ddlEPPremiumFrequencyCode.SelectedIndex = -1;
                    txtEPPremiumDuration.Text = "";
                    txtEPPrPayDate.Text = "";
                    txtLastPremiumDate.Text = "";
                    txtFirstPremiumDate.Text = "";
                    txtEPGracePeriod.Text = "";
                    txtEPPremiumAccumulated.Text = "";
                    txtEPBonusAccumulated.Text = "";
                    txtEPSurrenderValue.Text = "";
                    txtEPMaturityValue.Text = "";
                    txtEPRemarks.Text = "";
                }
                else if (CategoryCode == "INMP")
                {
                    txtMPPremiumAmount.Text = "";
                    ddlMPPremiumFrequencyCode.SelectedIndex = -1;
                    txtMPPremiumDuration.Text = "";
                    txtMPPrPayDate.Text = "";
                    txtMPLastPremiumDate.Text = "";
                    txtMPFirstPremiumDate.Text = "";
                    txtMPGracePeriod.Text = "";
                    txtMoneyBackEpisode.Text = "";
                    txtMPPremiumAccumulated.Text = "";
                    txtMPBonusAccumulated.Text = "";
                    txtMPSurrenderValue.Text = "";
                    txtMPMaturityValue.Text = "";
                    txtMPRemarks.Text = "";
                    txtMPPolicyTerm.Text = "";
                }
                else if (CategoryCode == "INTP")
                {
                    txtTPPremiumAmount.Text = "";
                    ddlTPPremiumFrequencyCode.SelectedIndex = -1;
                    txtTPPremiumDuration.Text = "";
                    txtTPPrPayDate.Text = "";
                    txtTPLastPremiumDate.Text = "";
                    txtTPFirstPremiumDate.Text = "";
                    txtTPGracePeriod.Text = "";
                    txtTPPremiumAccum.Text = "";
                    txtWLPRemarks.Text = "";
                }
                else if (CategoryCode == "INUP")
                {
                    ddlULIPPremiumFrequencyCode.SelectedIndex = -1;
                    txtULIPPrPayDate.Text = "";
                    ddlUlipPlans.SelectedIndex = -1;
                    txtULIPLastPremiumDate.Text = "";
                    txtULIPFirstPremiumDate.Text = "";
                    txtULIPSurrenderValue.Text = "";
                    txtULIPMaturityValue.Text = "";
                    txtULIPCharges.Text = "";
                    txtULIPRemarks.Text = "";
                }
                else if (CategoryCode == "INWP")
                {
                    txtWLPPremiumAmount.Text = "";
                    ddlWLPPremiumFrequencyCode.SelectedIndex = -1;
                    txtWLPPremiumDuration.Text = "";
                    txtWLPPrPayDate.Text = "";
                    txtWLPFirstPremiumDate.Text = "";
                    txtWLPLastPremiumDate.Text = "";
                    txtWLPGracePeriod.Text = "";
                    txtWPPremiumAccumulated.Text = "";
                    txtWPBonusAccumulated.Text = "";
                    txtWPSurrenderValue.Text = "";
                    txtWPMaturityValue.Text = "";
                    txtWLPRemarks.Text = "";
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:ClearControlValues()");
                object[] objects = new object[1];
                objects[0] = CategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void EnableDisableControls(string action, string CategoryCode)
        {
            try
            {
                if (action == "view")
                {
                    //lblInsuranceHeader.Text = "Insurance Details View Form";


                    trDeleteButton.Visible = true;
                    trEditButton.Visible = true;
                    trEditSpace.Visible = true;
                    trSubmitButton.Visible = false;
                    btnSubmit.Text = "";

                    txtName.Enabled = false;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = false;
                    txtPolicyCommencementDate.Enabled = false;
                    txtPolicyMaturity.Enabled = false;
                    txtSumAssured.Enabled = false;
                    txtApplicationNumber.Enabled = false;
                    txtApplDate.Enabled = false;

                    if (CategoryCode == "INEP")
                    {
                        trEPPremiumAmount.Visible = true;
                        trEPPremiumPeriod.Visible = true;
                        trEPGracePeriod.Visible = true;
                        trEPPremiumFirstLast.Visible = true;
                        trEPBonus.Visible = true;
                        trEPSurrenderValue.Visible = true;
                        trEPRemarks.Visible = true;
                        trValuationHeader.Visible = true;


                        txtEPPremiumAmount.Enabled = false;
                        ddlEPPremiumFrequencyCode.Enabled = false;
                        txtEPPremiumDuration.Enabled = false;
                        txtEPPrPayDate.Enabled = false;
                        txtFirstPremiumDate.Enabled = false;
                        txtLastPremiumDate.Enabled = false;
                        txtEPGracePeriod.Enabled = false;
                        txtEPPremiumAccumulated.Enabled = false;
                        txtEPBonusAccumulated.Enabled = false;
                        txtEPSurrenderValue.Enabled = false;
                        txtEPMaturityValue.Enabled = false;
                        txtEPRemarks.Enabled = false;
                    }
                    else if (CategoryCode == "INMP")
                    {
                        trMPPremiumAmount.Visible = true;
                        trMPPeriod.Visible = true;
                        trMPGracePeriod.Visible = true;
                        trMPPremiumFirstLast.Visible = true;
                        trMPBonus.Visible = true;
                        trMPSurrenderValue.Visible = true;
                        trMPRemarks.Visible = true;
                        trMPHeader.Visible = true;
                        trMPPolicyTerm.Visible = true;
                        trMPDetails.Visible = true;
                        pnlMoneyBackEpisode.Visible = true;
                        trMPSpace.Visible = true;
                        trValuationHeader.Visible = true;

                        txtMPPremiumAmount.Enabled = false;
                        ddlMPPremiumFrequencyCode.Enabled = false;
                        txtMPPremiumDuration.Enabled = false;
                        txtMPPrPayDate.Enabled = false;
                        txtMPFirstPremiumDate.Enabled = false;
                        txtMPLastPremiumDate.Enabled = false;
                        txtMPGracePeriod.Enabled = false;
                        txtMoneyBackEpisode.Enabled = false;
                        txtMPPremiumAccumulated.Enabled = false;
                        txtMPBonusAccumulated.Enabled = false;
                        txtMPSurrenderValue.Enabled = false;
                        txtMPMaturityValue.Enabled = false;
                        txtMPRemarks.Enabled = false;
                        txtMPPolicyTerm.Enabled = false;

                        count = Int32.Parse(txtMoneyBackEpisode.Text);
                        for (int i = 0; i < count; i++)
                        {
                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtPaymentDate" + i.ToString()));
                            txtBox1.Enabled = false;

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtRepaidPer" + i.ToString()));
                            txtBox2.Enabled = false;
                        }
                    }
                    else if (CategoryCode == "INTP")
                    {
                        trTPPremiumAmount.Visible = true;
                        trTPPremiumPeriod.Visible = true;
                        trTPPremiumFirstLast.Visible = true;
                        trTPGracePeriod.Visible = true;
                        trTPPremiumAccumn.Visible = true;
                        trTPRemarks.Visible = true;

                        txtTPPremiumAmount.Enabled = false;
                        ddlTPPremiumFrequencyCode.Enabled = false;
                        txtTPPremiumDuration.Enabled = false;
                        txtTPPrPayDate.Enabled = false;
                        txtTPFirstPremiumDate.Enabled = false;
                        txtTPLastPremiumDate.Enabled = false;
                        txtTPGracePeriod.Enabled = false;
                        txtTPPremiumAccum.Enabled = false;
                        txtWLPRemarks.Enabled = false;
                    }
                    else if (CategoryCode == "INUP")
                    {
                        trValuationHeader.Visible = true;
                        trULIPPremiumCycle.Visible = true;
                        trULIPPremiumFirstLast.Visible = true;
                        trULIPGracePeriod.Visible = true;
                        trULIPHeader.Visible = true;
                        trULIPSchemeBasket.Visible = true;
                        trULIPAllocation.Visible = true;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;

                        ddlULIPPremiumFrequencyCode.Enabled = false;
                        txtULIPPrPayDate.Enabled = false;
                        txtULIPFirstPremiumDate.Enabled = false;
                        txtULIPLastPremiumDate.Enabled = false;
                        txtULIPGracePeriod.Enabled = false;
                        ddlUlipPlans.Enabled = false;

                        // Get ULIP Sub-Plans Count
                        DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedValue.ToString()));
                        int count = ds.Tables[0].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtSubPlanId" + i.ToString()));
                            txtBox1.Enabled = false;

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString()));
                            txtBox2.Enabled = false;

                            TextBox txtBox3 = new TextBox();
                            txtBox3 = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString()));
                            txtBox3.Enabled = false;

                            TextBox txtBox4 = new TextBox();
                            txtBox4 = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString()));
                            txtBox4.Enabled = false;

                            TextBox txtBox5 = new TextBox();
                            txtBox5 = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString()));
                            txtBox5.Enabled = false;
                        }

                        txtULIPSurrenderValue.Enabled = false;
                        txtULIPMaturityValue.Enabled = false;
                        txtULIPCharges.Enabled = false;
                        txtULIPRemarks.Enabled = false;

                    }
                    else if (CategoryCode == "INWP")
                    {
                        trWLPPremiumAmount.Visible = true;
                        trWLPPremiumPeriod.Visible = true;
                        trWLPGracePeriod.Visible = true;
                        trWLPPremiumFirstLast.Visible = true;
                        trWPBonus.Visible = true;
                        trWPSurrenderValue.Visible = true;
                        trWLPRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtWLPPremiumAmount.Enabled = false;
                        ddlWLPPremiumFrequencyCode.Enabled = false;
                        txtWLPPremiumDuration.Enabled = false;
                        txtWLPPrPayDate.Enabled = false;
                        txtWLPFirstPremiumDate.Enabled = false;
                        txtWLPLastPremiumDate.Enabled = false;
                        txtWLPGracePeriod.Enabled = false;
                        txtWPPremiumAccumulated.Enabled = false;
                        txtWPBonusAccumulated.Enabled = false;
                        txtWPSurrenderValue.Enabled = false;
                        txtWPMaturityValue.Enabled = false;
                        txtWLPRemarks.Enabled = false;
                    }
                }
                else if (action == "edit")
                {
                    //lblInsuranceHeader.Text = "Insurance Details Edit Form";

                    trDeleteButton.Visible = false;
                    trEditButton.Visible = false;
                    trEditSpace.Visible = false;
                    trSubmitButton.Visible = true;
                    btnSubmit.Text = "Update";

                    txtName.Enabled = true;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = true;
                    txtPolicyCommencementDate.Enabled = true;
                    txtPolicyMaturity.Enabled = true;
                    txtSumAssured.Enabled = true;
                    txtApplicationNumber.Enabled = true;
                    txtApplDate.Enabled = true;


                    if (CategoryCode == "INEP")
                    {
                        trEPPremiumAmount.Visible = true;
                        trEPPremiumPeriod.Visible = true;
                        trEPPremiumFirstLast.Visible = true;
                        trEPGracePeriod.Visible = true;
                        trEPBonus.Visible = true;
                        trEPSurrenderValue.Visible = true;
                        trEPRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtEPPremiumAmount.Enabled = true;
                        ddlEPPremiumFrequencyCode.Enabled = true;
                        txtEPPremiumDuration.Enabled = true;
                        txtEPPrPayDate.Enabled = true;
                        txtFirstPremiumDate.Enabled = true;
                        txtLastPremiumDate.Enabled = true;
                        txtEPGracePeriod.Enabled = true;
                        txtEPPremiumAccumulated.Enabled = true;
                        txtEPBonusAccumulated.Enabled = true;
                        txtEPSurrenderValue.Enabled = true;
                        txtEPMaturityValue.Enabled = true;
                        txtEPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INMP")
                    {
                        trMPPremiumAmount.Visible = true;
                        trMPPeriod.Visible = true;
                        trMPPremiumFirstLast.Visible = true;
                        trMPGracePeriod.Visible = true;
                        trMPBonus.Visible = true;
                        trMPSurrenderValue.Visible = true;
                        trMPRemarks.Visible = true;
                        trMPHeader.Visible = true;
                        trMPPolicyTerm.Visible = true;
                        trMPDetails.Visible = true;
                        pnlMoneyBackEpisode.Visible = true;
                        trMPSpace.Visible = true;
                        trValuationHeader.Visible = true;

                        txtMPPremiumAmount.Enabled = true;
                        ddlMPPremiumFrequencyCode.Enabled = true;
                        txtMPPremiumDuration.Enabled = true;
                        txtMPPrPayDate.Enabled = true;
                        txtMPFirstPremiumDate.Enabled = true;
                        txtMPLastPremiumDate.Enabled = true;
                        txtMPGracePeriod.Enabled = true;
                        txtMoneyBackEpisode.Enabled = true;
                        txtMPPremiumAccumulated.Enabled = true;
                        txtMPBonusAccumulated.Enabled = true;
                        txtMPSurrenderValue.Enabled = true;
                        txtMPMaturityValue.Enabled = true;
                        txtMPRemarks.Enabled = true;
                        txtMPPolicyTerm.Enabled = true;

                        count = Int32.Parse(txtMoneyBackEpisode.Text);
                        for (int i = 0; i < count; i++)
                        {
                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtPaymentDate" + i.ToString()));
                            txtBox1.Enabled = true;

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtRepaidPer" + i.ToString()));
                            txtBox2.Enabled = true;
                        }
                    }
                    else if (CategoryCode == "INTP")
                    {
                        trTPPremiumAmount.Visible = true;
                        trTPPremiumPeriod.Visible = true;
                        trTPPremiumFirstLast.Visible = true;
                        trTPGracePeriod.Visible = true;
                        trTPPremiumAccumn.Visible = true;
                        trTPRemarks.Visible = true;

                        txtTPPremiumAmount.Enabled = true;
                        ddlTPPremiumFrequencyCode.Enabled = true;
                        txtTPPremiumDuration.Enabled = true;
                        txtTPPrPayDate.Enabled = true;
                        txtTPFirstPremiumDate.Enabled = true;
                        txtTPLastPremiumDate.Enabled = true;
                        txtTPGracePeriod.Enabled = true;
                        txtTPPremiumAccum.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INUP")
                    {
                        trValuationHeader.Visible = true;
                        trULIPPremiumCycle.Visible = true;
                        trULIPPremiumFirstLast.Visible = true;
                        trULIPGracePeriod.Visible = true;
                        trULIPHeader.Visible = true;
                        trULIPSchemeBasket.Visible = true;
                        trULIPAllocation.Visible = true;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;

                        ddlULIPPremiumFrequencyCode.Enabled = true;
                        txtULIPPrPayDate.Enabled = true;
                        txtULIPFirstPremiumDate.Enabled = true;
                        txtULIPLastPremiumDate.Enabled = true;
                        txtULIPGracePeriod.Enabled = false;
                        ddlUlipPlans.Enabled = true;
                        //ddlUlipSubPlans.Enabled = true;

                        // Get ULIP Sub-Plans Count
                        DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedValue.ToString()));
                        int count = ds.Tables[0].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtSubPlanId" + i.ToString()));
                            txtBox1.Enabled = false;

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString()));
                            txtBox2.Enabled = true;

                            TextBox txtBox3 = new TextBox();
                            txtBox3 = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString()));
                            txtBox3.Enabled = true;

                            TextBox txtBox4 = new TextBox();
                            txtBox4 = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString()));
                            txtBox4.Enabled = true;

                            TextBox txtBox5 = new TextBox();
                            txtBox5 = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString()));
                            txtBox5.Enabled = true;
                        }

                        txtULIPSurrenderValue.Enabled = true;
                        txtULIPMaturityValue.Enabled = true;
                        txtULIPCharges.Enabled = true;
                        txtULIPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INWP")
                    {
                        trWLPPremiumAmount.Visible = true;
                        trWLPPremiumPeriod.Visible = true;
                        trWLPPremiumFirstLast.Visible = true;
                        trWLPGracePeriod.Visible = true;
                        trWPBonus.Visible = true;
                        trWPSurrenderValue.Visible = true;
                        trWLPRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtWLPPremiumAmount.Enabled = true;
                        ddlWLPPremiumFrequencyCode.Enabled = true;
                        txtWLPPremiumDuration.Enabled = true;
                        txtWLPPrPayDate.Enabled = true;
                        txtWLPFirstPremiumDate.Enabled = true;
                        txtWLPLastPremiumDate.Enabled = true;
                        txtWLPGracePeriod.Enabled = true;
                        txtWPPremiumAccumulated.Enabled = true;
                        txtWPBonusAccumulated.Enabled = true;
                        txtWPSurrenderValue.Enabled = true;
                        txtWPMaturityValue.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }

                }
                else if (action == "entry")
                {
                    //lblInsuranceHeader.Text = "Insurance Details Entry Form";

                    trDeleteButton.Visible = false;
                    trEditButton.Visible = false;
                    trEditSpace.Visible = false;
                    trSubmitButton.Visible = true;
                    btnSubmit.Text = "Submit";


                    txtName.Enabled = true;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = true;
                    txtPolicyCommencementDate.Enabled = true;
                    txtPolicyMaturity.Enabled = true;
                    txtSumAssured.Enabled = true;
                    txtApplicationNumber.Enabled = true;
                    txtApplDate.Enabled = true;

                    if (CategoryCode == "INEP")
                    {
                        trEPPremiumAmount.Visible = true;
                        trEPPremiumPeriod.Visible = true;
                        trEPPremiumFirstLast.Visible = true;
                        trEPGracePeriod.Visible = true;
                        trEPBonus.Visible = true;
                        trEPSurrenderValue.Visible = true;
                        trEPRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtEPPremiumAmount.Enabled = true;
                        ddlEPPremiumFrequencyCode.Enabled = true;
                        txtEPPremiumDuration.Enabled = true;
                        txtEPPrPayDate.Enabled = true;
                        txtFirstPremiumDate.Enabled = true;
                        txtLastPremiumDate.Enabled = true;
                        txtEPGracePeriod.Enabled = true;
                        txtEPPremiumAccumulated.Enabled = true;
                        txtEPBonusAccumulated.Enabled = true;
                        txtEPSurrenderValue.Enabled = true;
                        txtEPMaturityValue.Enabled = true;
                        txtEPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INMP")
                    {
                        trMPPremiumAmount.Visible = true;
                        trMPPeriod.Visible = true;
                        trMPPremiumFirstLast.Visible = true;
                        trMPGracePeriod.Visible = true;
                        trMPBonus.Visible = true;
                        trMPSurrenderValue.Visible = true;
                        trMPRemarks.Visible = true;
                        trMPHeader.Visible = true;
                        trMPPolicyTerm.Visible = true;
                        trMPDetails.Visible = true;
                        pnlMoneyBackEpisode.Visible = true;
                        trMPSpace.Visible = true;
                        trValuationHeader.Visible = true;

                        txtMPPremiumAmount.Enabled = true;
                        ddlMPPremiumFrequencyCode.Enabled = true;
                        txtMPPremiumDuration.Enabled = true;
                        txtMPPrPayDate.Enabled = true;
                        txtMPFirstPremiumDate.Enabled = true;
                        txtMPLastPremiumDate.Enabled = true;
                        txtMPGracePeriod.Enabled = true;
                        txtMoneyBackEpisode.Enabled = true;
                        txtMPPremiumAccumulated.Enabled = true;
                        txtMPBonusAccumulated.Enabled = true;
                        txtMPSurrenderValue.Enabled = true;
                        txtMPMaturityValue.Enabled = true;
                        txtMPRemarks.Enabled = true;
                        txtMPPolicyTerm.Enabled = true;
                    }
                    else if (CategoryCode == "INTP")
                    {
                        trTPPremiumAmount.Visible = true;
                        trTPPremiumPeriod.Visible = true;
                        trTPPremiumFirstLast.Visible = true;
                        trTPGracePeriod.Visible = true;
                        trTPPremiumAccumn.Visible = true;
                        trTPRemarks.Visible = true;

                        txtTPPremiumAmount.Enabled = true;
                        ddlTPPremiumFrequencyCode.Enabled = true;
                        txtTPPremiumDuration.Enabled = true;
                        txtTPPrPayDate.Enabled = true;
                        txtTPFirstPremiumDate.Enabled = true;
                        txtTPLastPremiumDate.Enabled = true;
                        txtTPGracePeriod.Enabled = true;
                        txtTPPremiumAccum.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INUP")
                    {
                        trValuationHeader.Visible = true;
                        trULIPPremiumCycle.Visible = true;
                        trULIPPremiumFirstLast.Visible = true;
                        trULIPGracePeriod.Visible = true;
                        trULIPHeader.Visible = true;
                        trULIPSchemeBasket.Visible = true;
                        trULIPAllocation.Visible = false;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;

                        ddlULIPPremiumFrequencyCode.Enabled = true;
                        txtULIPPrPayDate.Enabled = true;
                        ddlUlipPlans.Enabled = true;
                        txtULIPFirstPremiumDate.Enabled = true;
                        txtULIPLastPremiumDate.Enabled = true;

                        txtULIPSurrenderValue.Enabled = true;
                        txtULIPMaturityValue.Enabled = true;
                        txtULIPCharges.Enabled = true;
                        txtULIPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INWP")
                    {
                        trWLPPremiumAmount.Visible = true;
                        trWLPPremiumPeriod.Visible = true;
                        trWLPPremiumFirstLast.Visible = true;
                        trWLPGracePeriod.Visible = true;
                        trWPBonus.Visible = true;
                        trWPSurrenderValue.Visible = true;
                        trWLPRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtWLPPremiumAmount.Enabled = true;
                        ddlWLPPremiumFrequencyCode.Enabled = true;
                        txtWLPPremiumDuration.Enabled = true;
                        txtWLPPrPayDate.Enabled = true;
                        txtWLPFirstPremiumDate.Enabled = true;
                        txtWLPLastPremiumDate.Enabled = true;
                        txtWLPGracePeriod.Enabled = true;
                        txtWPPremiumAccumulated.Enabled = true;
                        txtWPBonusAccumulated.Enabled = true;
                        txtWPSurrenderValue.Enabled = true;
                        txtWPMaturityValue.Enabled = true;
                        txtWLPRemarks.Enabled = true;
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:EnableDisableControls()");
                object[] objects = new object[2];
                objects[0] = CategoryCode;
                objects[1] = action;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    customerVo = (CustomerVo)Session["customerVo"];
                    userVo = (UserVo)Session["userVo"];
                    customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];


                    if (btnSubmit.Text == "Submit")
                    {
                        InsuranceVo insuranceVo = new InsuranceVo();

                        insuranceVo.AccountId = customerAccountVo.AccountId;
                        insuranceVo.AssetGroupCode = AssetGroupCode;
                        insuranceVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory.ToString().Trim();
                        insuranceVo.Name = txtName.Text;
                        insuranceVo.PolicyNumber = customerAccountVo.PolicyNum;
                        insuranceVo.InsuranceIssuerCode = ddlInsuranceIssuerCode.SelectedValue.ToString();
                        insuranceVo.StartDate = DateTime.Parse(txtPolicyCommencementDate.Text.Trim());
                        insuranceVo.EndDate = DateTime.Parse(txtPolicyMaturity.Text.Trim());
                        insuranceVo.SumAssured = float.Parse(txtSumAssured.Text);
                        if (txtApplDate.Text.Trim() != "")
                            insuranceVo.ApplicationDate = DateTime.Parse(txtApplDate.Text.Trim());
                        insuranceVo.ApplicationNumber = txtApplicationNumber.Text;

                        // Insert Empty Values
                        insuranceVo.PremiumFrequencyCode = "";
                        insuranceVo.PremiumAmount = 0;
                        insuranceVo.PremiumDuration = 0;
                        insuranceVo.PolicyPeriod = 0;
                        insuranceVo.PremiumAccumalated = 0;
                        insuranceVo.PolicyEpisode = 0;
                        insuranceVo.BonusAccumalated = 0;
                        insuranceVo.SurrenderValue = 0;
                        insuranceVo.Remarks = "";
                        insuranceVo.MaturityValue = 0;
                        insuranceVo.GracePeriod = 0;
                        insuranceVo.ULIPCharges = 0;
                        insuranceVo.PremiumPaymentDate = 0;


                        if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INEP")
                        {
                            if (txtEPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtEPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue.ToString().Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtEPPrPayDate.Text);
                            if (txtEPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtEPGracePeriod.Text);
                            if (txtEPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtEPPremiumAccumulated.Text);
                            if (txtEPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtEPBonusAccumulated.Text);
                            if (txtEPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtEPSurrenderValue.Text);
                            if (txtEPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtEPMaturityValue.Text);
                            insuranceVo.Remarks = txtEPRemarks.Text.Trim();

                            insuranceId = insuranceBo.CreateInsurancePortfolio(insuranceVo, userVo.UserId);
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                        }
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INWP")
                        {
                            if (txtWLPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtWLPPremiumAmount.Text);
                            insuranceVo.PremiumFrequencyCode = ddlWLPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtWLPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtWLPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtWLPPrPayDate.Text.Trim());
                            if (txtWLPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtWLPGracePeriod.Text);
                            if (txtWPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtWPPremiumAccumulated.Text);
                            if (txtWPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtWPBonusAccumulated.Text);
                            if (txtWPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtWPSurrenderValue.Text);
                            if (txtWPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtWPMaturityValue.Text);
                            insuranceVo.Remarks = txtWLPRemarks.Text.Trim();

                            insuranceId = insuranceBo.CreateInsurancePortfolio(insuranceVo, userVo.UserId);
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                        }
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INTP")
                        {
                            if (txtTPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtTPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlTPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtTPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtTPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtTPPrPayDate.Text.Trim());
                            if (txtTPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtTPGracePeriod.Text.Trim());
                            if (txtTPPremiumAccum.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtTPPremiumAccum.Text.Trim());
                            insuranceVo.Remarks = txtWLPRemarks.Text.Trim();

                            insuranceId = insuranceBo.CreateInsurancePortfolio(insuranceVo, userVo.UserId);
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                        }
                        #region ULIP Submit
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INUP")
                        {
                            List<float> subPlanList = null;
                            List<InsuranceULIPVo> insuranceUlipList;
                            InsuranceULIPVo insuranceUlipVo;

                            insuranceVo.PremiumAmount = 0;
                            insuranceVo.PremiumDuration = 0;
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtULIPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtULIPLastPremiumDate.Text.ToString());
                            insuranceVo.PremiumFrequencyCode = ddlULIPPremiumFrequencyCode.SelectedValue;

                            insuranceVo.PremiumAccumalated = 0;
                            insuranceVo.BonusAccumalated = 0;

                            DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedValue.ToString()));
                            int count = ds.Tables[0].Rows.Count;
                            subPlanList = new List<float>();
                            insuranceUlipList = new List<InsuranceULIPVo>();
                            float tot = 0;
                            int txt = 0;

                            // Calcuating the total Asset Allocation value
                            for (int i = 0; i < count; i++)
                            {
                                string temp = (((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString());
                                if (temp == "")
                                    txt = 0;
                                else
                                    txt = int.Parse(temp.ToString());

                                tot = tot + (float)txt;
                                subPlanList.Add(txt);
                            }

                            // Check the total asset Allocation and assign Unit, Purchase price and Allocation percentage 
                            if (tot == 100)
                            {
                                // lblError.Text = "Hundred";
                                for (int i = 0; i < count; i++)
                                {
                                    insuranceUlipVo = new InsuranceULIPVo();
                                    string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                                    string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                                    string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                                    string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                                    insuranceUlipVo.WUP_ULIPSubPlaCode = ds.Tables[0].Rows[i][0].ToString();
                                    insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlUlipPlans.SelectedValue.ToString());

                                    if (allocationPer == "")
                                    {
                                        insuranceUlipVo.CIUP_AllocationPer = 0;
                                        insuranceUlipVo.CIUP_PurchasePrice = 0;
                                        insuranceUlipVo.CIUP_Unit = 0;
                                        insuranceUlipVo.CIUP_PurchaseDate = DateTime.MinValue;
                                    }
                                    else
                                    {
                                        insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                                        insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                                        insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                                        insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());
                                    }
                                    insuranceUlipList.Add(insuranceUlipVo);
                                }

                                try
                                {
                                    insuranceId = insuranceBo.CreateInsurancePortfolio(insuranceVo, userVo.UserId);
                                    for (int i = 0; i < insuranceUlipList.Count; i++)
                                    {
                                        insuranceUlipVo = new InsuranceULIPVo();
                                        insuranceUlipVo = insuranceUlipList[i];
                                        insuranceUlipVo.CIP_CustInsInvId = insuranceId;
                                        insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                                        insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                                        insuranceBo.CreateInsuranceULIPPlan(insuranceUlipVo);
                                    }
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                                }
                                catch (BaseApplicationException Ex)
                                {
                                    throw Ex;
                                }
                                catch (Exception Ex)
                                {
                                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                                    NameValueCollection FunctionInfo = new NameValueCollection();
                                    FunctionInfo.Add("Method", "PortfolioInsuranceEntry.cs:btnSubmit_Click()");
                                    //object[] objects = new object[1];
                                    //objects[0] = moneyBackEpisodeVo;
                                    FunctionInfo = exBase.AddObject(FunctionInfo, null);
                                    exBase.AdditionalInformation = FunctionInfo;
                                    ExceptionManager.Publish(exBase);
                                    throw exBase;
                                }

                                Session.Remove("table");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                            }
                            else
                            {
                                lblError.Text = "Check the Allocation";
                                lblError.CssClass = "Error";
                                trULIPError.Visible = true;
                            }
                        }
                        #endregion
                        #region MBP Submit
                        else if (customerAccountVo.AssetCategory.ToString().Trim() == "INMP")
                        {
                            if (txtMPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtMPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlMPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtMPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtMPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtMPPrPayDate.Text.Trim());
                            if (txtMPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtMPGracePeriod.Text.Trim());

                            if (txtMoneyBackEpisode.Text.Trim() != "")
                                insuranceVo.PolicyEpisode = float.Parse(txtMoneyBackEpisode.Text.Trim());
                            if (txtMPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtMPPremiumAccumulated.Text.Trim());
                            if (txtMPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtMPBonusAccumulated.Text.Trim());
                            if (txtMPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtMPSurrenderValue.Text.Trim());
                            if (txtMPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtMPMaturityValue.Text.Trim());
                            insuranceVo.Remarks = txtMPRemarks.Text.Trim();
                            insuranceVo.PolicyPeriod = float.Parse(txtMPPolicyTerm.Text.Trim());

                            try
                            {
                                insuranceId = insuranceBo.CreateInsurancePortfolio(insuranceVo, userVo.UserId);

                                List<MoneyBackEpisodeVo> moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                                MoneyBackEpisodeVo moneyBackEpisodeVo;
                                count = 0;
                                Int32.TryParse(txtMoneyBackEpisode.Text.Trim(), out count);
                                for (int i = 0; i < count; i++)
                                {
                                    moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                    string paymentDate = (((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString())).Text.ToString());
                                    moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                    string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                    moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                    moneyBackEpisodeVo.CustInsInvId = insuranceId;
                                    moneyBackEpisodeList.Add(moneyBackEpisodeVo);
                                }


                                for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                                {
                                    moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                    moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                    moneyBackEpisodeVo.CIMBE_CreatedBy = userVo.UserId;
                                    moneyBackEpisodeVo.CIMBE_ModifiedBy = userVo.UserId;
                                    insuranceBo.CreateMoneyBackEpisode(moneyBackEpisodeVo);
                                }

                                Session.Remove("moneyBackEpisodeList");
                                Session.Remove("table");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                            }
                            catch (BaseApplicationException Ex)
                            {
                                throw Ex;
                            }
                            catch (Exception Ex)
                            {
                                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                                NameValueCollection FunctionInfo = new NameValueCollection();
                                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.cs:btnSubmit_Click()");
                                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                                exBase.AdditionalInformation = FunctionInfo;
                                ExceptionManager.Publish(exBase);
                                throw exBase;
                            }
                        }
                        #endregion
                        else
                        {

                        }
                        //Session.Remove("table");
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        // Get Insurance VO from Sessioin
                        insuranceVo = (InsuranceVo)Session["insuranceVo"];

                        // Start Update Process
                        insuranceVo.Name = txtName.Text;
                        insuranceVo.PolicyNumber = customerAccountVo.PolicyNum;
                        insuranceVo.InsuranceIssuerCode = ddlInsuranceIssuerCode.SelectedValue.ToString();
                        insuranceVo.StartDate = DateTime.Parse(txtPolicyCommencementDate.Text.Trim());
                        insuranceVo.EndDate = DateTime.Parse(txtPolicyMaturity.Text.Trim());
                        insuranceVo.SumAssured = float.Parse(txtSumAssured.Text);
                        if (txtApplDate.Text.Trim() != "")
                            insuranceVo.ApplicationDate = DateTime.Parse(txtApplDate.Text.Trim());
                        insuranceVo.ApplicationNumber = txtApplicationNumber.Text;

                        if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INEP")
                        {
                            if (txtEPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtEPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue.ToString().Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtLastPremiumDate.Text.ToString());
                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtEPPrPayDate.Text);

                            if (txtEPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtEPGracePeriod.Text);
                            if (txtEPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtEPPremiumAccumulated.Text);
                            if (txtEPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtEPBonusAccumulated.Text);
                            if (txtEPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtEPSurrenderValue.Text);
                            if (txtEPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtEPMaturityValue.Text);
                            insuranceVo.Remarks = txtEPRemarks.Text.Trim();

                            if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                            {
                                Session.Remove("table");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                            }
                        }
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INWP")
                        {
                            if (txtWLPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtWLPPremiumAmount.Text);
                            insuranceVo.PremiumFrequencyCode = ddlWLPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtWLPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtWLPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtWLPPrPayDate.Text.Trim());
                            if (txtWLPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtWLPGracePeriod.Text);
                            if (txtWPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtWPPremiumAccumulated.Text);
                            if (txtWPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtWPBonusAccumulated.Text);
                            if (txtWPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtWPSurrenderValue.Text);
                            if (txtWPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtWPMaturityValue.Text);
                            insuranceVo.Remarks = txtWLPRemarks.Text.Trim();

                            if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                            {
                                Session.Remove("table");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                            }
                        }
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INTP")
                        {
                            if (txtTPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtTPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlTPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtTPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtTPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtTPPrPayDate.Text.Trim());
                            if (txtTPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtTPGracePeriod.Text.Trim());
                            if (txtTPPremiumAccum.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtTPPremiumAccum.Text.Trim());
                            insuranceVo.Remarks = txtWLPRemarks.Text.Trim();

                            if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                            {
                                Session.Remove("table");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                            }
                        }
                        #region ULIP Update
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INUP")
                        {
                            List<float> subPlanList = null;
                            List<InsuranceULIPVo> insuranceUlipList;
                            InsuranceULIPVo insuranceUlipVo;

                            int PrevUlipSubPlanCode = 0;
                            int PrevUlipPlanCode = 0;

                            insuranceVo.PremiumAmount = 0;
                            insuranceVo.PremiumDuration = 0;
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtULIPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtULIPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumAccumalated = 0;
                            insuranceVo.BonusAccumalated = 0;

                            DataSet prevUlipSubPlansDS = assetBo.GetPrevULIPSubPlans(insuranceVo.CustInsInvId);
                            PrevUlipSubPlanCode = Int32.Parse(prevUlipSubPlansDS.Tables[0].Rows[0]["WUSP_ULIPSubPlanCode"].ToString().Trim());
                            DataSet prevUlipPlanCodeDS = assetBo.GetPrevUlipPlanCode(PrevUlipSubPlanCode);
                            PrevUlipPlanCode = Int32.Parse(prevUlipPlanCodeDS.Tables[0].Rows[0]["WUP_ULIPPlanCode"].ToString().Trim());

                            DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedValue.ToString()));
                            int count = ds.Tables[0].Rows.Count;
                            subPlanList = new List<float>();

                            float tot = 0;
                            int txt = 0;

                            // Calcuating the total Asset Allocation value
                            for (int i = 0; i < count; i++)
                            {
                                string temp = (((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString());
                                if (temp == "")
                                    txt = 0;
                                else
                                    txt = int.Parse(temp.ToString());

                                tot = tot + (float)txt;
                                subPlanList.Add(txt);
                            }


                            // Check the total asset Allocation and assign Unit, Purchase price and Allocation percentage 
                            if (tot == 100)
                            {
                                // If the Drop Down Selection has changed then 
                                // Delete the previous records saved 
                                // Update the new records

                                if (int.Parse(ddlUlipPlans.SelectedValue.ToString()) == PrevUlipPlanCode)
                                {
                                    // Do an Update Here
                                    insuranceUlipList = new List<InsuranceULIPVo>();
                                    insuranceUlipList = (List<InsuranceULIPVo>)Session["insuranceULIPList"];

                                    for (int i = 0; i < count; i++)
                                    {
                                        insuranceUlipVo = insuranceUlipList[i];
                                        string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                                        string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                                        string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                                        string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                                        if (allocationPer == "")
                                        {
                                            insuranceUlipVo.CIUP_AllocationPer = 0;
                                            insuranceUlipVo.CIUP_PurchasePrice = 0;
                                            insuranceUlipVo.CIUP_Unit = 0;
                                            insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse("1/1/1900");
                                        }
                                        else
                                        {
                                            insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                                            insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                                            insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                                            insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());
                                        }
                                        insuranceUlipList.Add(insuranceUlipVo);
                                    }

                                    if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                                    {
                                        for (int i = 0; i < insuranceUlipList.Count; i++)
                                        {
                                            insuranceUlipVo = new InsuranceULIPVo();
                                            insuranceUlipVo = insuranceUlipList[i];
                                            insuranceUlipVo.CIP_CustInsInvId = insuranceVo.CustInsInvId;
                                            insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                                            insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                                            insuranceBo.UpdateInsuranceULIPPlan(insuranceUlipVo);
                                        }

                                        Session.Remove("table");
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                                    }
                                }
                                else
                                {
                                    // Delete the old sub-plans
                                    // --
                                    if (insuranceBo.DeleteInsuranceUlipPlans(insuranceVo.CustInsInvId))
                                    {
                                        insuranceUlipList = new List<InsuranceULIPVo>();

                                        // Add New Ones Here
                                        // --
                                        for (int i = 0; i < count; i++)
                                        {
                                            insuranceUlipVo = new InsuranceULIPVo();
                                            string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                                            string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                                            string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                                            string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                                            insuranceUlipVo.WUP_ULIPSubPlaCode = ds.Tables[0].Rows[i][0].ToString();
                                            insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlUlipPlans.SelectedValue.ToString());

                                            if (allocationPer == "")
                                            {
                                                insuranceUlipVo.CIUP_AllocationPer = 0;
                                                insuranceUlipVo.CIUP_PurchasePrice = 0;
                                                insuranceUlipVo.CIUP_Unit = 0;
                                                insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse("1/1/1900");
                                            }
                                            else
                                            {
                                                insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                                                insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                                                insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                                                insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());
                                            }
                                            insuranceUlipList.Add(insuranceUlipVo);
                                        }

                                        if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                                        {
                                            for (int i = 0; i < insuranceUlipList.Count; i++)
                                            {
                                                insuranceUlipVo = new InsuranceULIPVo();
                                                insuranceUlipVo = insuranceUlipList[i];
                                                insuranceUlipVo.CIP_CustInsInvId = insuranceVo.CustInsInvId;
                                                insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                                                insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                                                insuranceBo.CreateInsuranceULIPPlan(insuranceUlipVo);
                                            }

                                            Session.Remove("table");
                                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lblError.Text = "Check the Allocation";
                                lblError.CssClass = "Error";
                            }
                        }
                        #endregion
                        #region MBP Update
                        else if (customerAccountVo.AssetCategory.ToString().Trim() == "INMP")
                        {
                            int previousEpisodeCount = 0;

                            if (txtMPPremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtMPPremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlMPPremiumFrequencyCode.SelectedValue.Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtMPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtMPLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(txtMPPrPayDate.Text.Trim());
                            if (txtMPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtMPGracePeriod.Text.Trim());
                            else
                                insuranceVo.GracePeriod = 0.0F;
                            // Important Step for Updates
                            previousEpisodeCount = Int32.Parse(insuranceVo.PolicyEpisode.ToString());
                            if (txtMoneyBackEpisode.Text.Trim() != "")
                                insuranceVo.PolicyEpisode = float.Parse(txtMoneyBackEpisode.Text.Trim());
                            else
                                insuranceVo.PolicyEpisode = 0.0F;

                            if (txtMPPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtMPPremiumAccumulated.Text.Trim());
                            else
                                insuranceVo.PremiumAccumalated = 0.0F;
                            if (txtMPBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtMPBonusAccumulated.Text.Trim());
                            else
                                insuranceVo.BonusAccumalated = 0.0F;
                            if (txtMPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtMPSurrenderValue.Text.Trim());
                            else
                                insuranceVo.SurrenderValue = 0.0F;
                            if (txtMPMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtMPMaturityValue.Text.Trim());
                            else
                                insuranceVo.MaturityValue = 0.0F;
                            insuranceVo.PolicyPeriod = float.Parse(txtMPPolicyTerm.Text.Trim());

                            insuranceVo.Remarks = txtMPRemarks.Text.Trim();

                            // Put this under a C# Transaction Block
                            try
                            {
                                List<MoneyBackEpisodeVo> moneyBackEpisodeList;
                                MoneyBackEpisodeVo moneyBackEpisodeVo;
                                count = 0;
                                Int32.TryParse(txtMoneyBackEpisode.Text.Trim(), out count);

                                // If Current Count is equal to previous count or less than it
                                // then just update the moneyback episodes
                                if (count == previousEpisodeCount || count < previousEpisodeCount)
                                {
                                    moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                                    moneyBackEpisodeList = (List<MoneyBackEpisodeVo>)Session["moneyBackEpisodeList"];

                                    // Update the rows
                                    for (int i = 0; i < count; i++)
                                    {
                                        moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                        string paymentDate = (((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                        string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                        moneyBackEpisodeList[i] = moneyBackEpisodeVo;
                                    }

                                    if (moneyBackEpisodeList != null)
                                    {
                                        for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                                        {
                                            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                            moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                            moneyBackEpisodeVo.CIMBE_ModifiedBy = userVo.UserId;
                                            insuranceBo.UpdateMoneyBackEpisode(moneyBackEpisodeVo);
                                        }
                                    }


                                    if (count < previousEpisodeCount)
                                    {
                                        moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                                        moneyBackEpisodeList = (List<MoneyBackEpisodeVo>)Session["moneyBackEpisodeList"];

                                        int NewCount = previousEpisodeCount - count;

                                        // Delete Extra Entries
                                        for (int i = previousEpisodeCount - 1; i > count - 1; i--)
                                        {
                                            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                            moneyBackEpisodeVo = moneyBackEpisodeList[i];

                                            insuranceBo.DeleteMoneyBackEpisode(moneyBackEpisodeVo);
                                        }
                                    }
                                }
                                else if (count > previousEpisodeCount)
                                {
                                    moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                                    if (Session["moneyBackEpisodeList"] != null)
                                        moneyBackEpisodeList = (List<MoneyBackEpisodeVo>)Session["moneyBackEpisodeList"];

                                    // Update the old entries
                                    for (int i = 0; i < previousEpisodeCount; i++)
                                    {
                                        moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                        string paymentDate = (((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                        string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                        moneyBackEpisodeList[i] = moneyBackEpisodeVo;
                                    }


                                    for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                                    {
                                        moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                        moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                        moneyBackEpisodeVo.CIMBE_ModifiedBy = userVo.UserId;
                                        insuranceBo.UpdateMoneyBackEpisode(moneyBackEpisodeVo);
                                    }


                                    // Add the New Entries Here
                                    moneyBackEpisodeList.Clear();
                                    for (int i = previousEpisodeCount; i < count; i++)
                                    {
                                        moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                        string paymentDate = (((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                        string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                        moneyBackEpisodeVo.CustInsInvId = insuranceVo.CustInsInvId;
                                        moneyBackEpisodeList.Add(moneyBackEpisodeVo);
                                    }

                                    for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                                    {
                                        moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                                        moneyBackEpisodeVo = moneyBackEpisodeList[i];
                                        moneyBackEpisodeVo.CIMBE_CreatedBy = userVo.UserId;
                                        moneyBackEpisodeVo.CIMBE_ModifiedBy = userVo.UserId;
                                        insuranceBo.CreateMoneyBackEpisode(moneyBackEpisodeVo);
                                    }
                                }

                                insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId);

                                Session.Remove("table");
                                Session.Remove("moneyBackEpisodeList");
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);

                            }
                            catch (BaseApplicationException Ex)
                            {
                                throw Ex;
                            }
                            catch (Exception Ex)
                            {
                                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                                NameValueCollection FunctionInfo = new NameValueCollection();
                                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.cs:btnSubmit_Click()");
                                //object[] objects = new object[1];
                                //objects[0] = moneyBackEpisodeVo;
                                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                                exBase.AdditionalInformation = FunctionInfo;
                                ExceptionManager.Publish(exBase);
                                throw exBase;
                            }
                        }
                        #endregion
                        else
                        {

                        }
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerAccountVo;
                objects[3] = insuranceVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtMoneyBackEpisode_TextChanged(object sender, EventArgs e)
        {
            MoneyBackEpisodeVo moneyBackEpisodeVo = new MoneyBackEpisodeVo();


            try
            {
                //Session["episodeCount"] = txtMoneyBackEpisode.Text;
                pnlMoneyBackEpisode.Visible = true;
                trMPDetails.Visible = true;
                count = int.Parse(txtMoneyBackEpisode.Text);
                ShowMoneyBackContent(count);

                // OnPostBack it should load the existing values
                if (Session["moneyBackEpisodeList"] != null)
                {
                    moneyBackEpisodeList = (List<MoneyBackEpisodeVo>)Session["moneyBackEpisodeList"];

                    // Do a Count Check Here Too
                    if (count < moneyBackEpisodeList.Count)
                    {// Special Case
                        for (int i = 0; i < count; i++)
                        {
                            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                            moneyBackEpisodeVo = moneyBackEpisodeList[i];

                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString()));
                            txtBox1.Text = moneyBackEpisodeVo.CIMBE_RepaymentDate.ToShortDateString();

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString()));
                            txtBox2.Text = moneyBackEpisodeVo.CIMBE_RepaidPer.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                        {
                            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                            moneyBackEpisodeVo = moneyBackEpisodeList[i];

                            TextBox txtBox1 = new TextBox();
                            txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString()));
                            txtBox1.Text = moneyBackEpisodeVo.CIMBE_RepaymentDate.ToShortDateString();

                            TextBox txtBox2 = new TextBox();
                            txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString()));
                            txtBox2.Text = moneyBackEpisodeVo.CIMBE_RepaidPer.ToString();
                        }
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:txtMoneyBackEpisode_TextChanged()");
                object[] objects = new object[2];
                objects[0] = moneyBackEpisodeVo;
                objects[1] = moneyBackEpisodeList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void ShowMoneyBackContent(int count)
        {
            try
            {
                PlaceHolder2.Controls.Clear();
                Table tb = new Table();
                TableCell tc;
                for (int i = 0; i < count; i++)
                {
                    TableRow tr = new TableRow();
                    // First Cell
                    tc = new TableCell();
                    TextBox txtBox1 = new TextBox();
                    txtBox1.ID = "txtPaymentDate" + i.ToString();
                    txtBox1.CssClass = "txtField";
                    tc.Controls.Add(txtBox1);
                    tc.ColumnSpan = 2;
                    tr.Cells.Add(tc);

                    // Middle Empty Cells Used for Alignment
                    for (int j = 0; j < 13; j++)
                    {
                        tc = new TableCell();
                        tr.Cells.Add(tc);
                    }

                    // Last Cell
                    tc = new TableCell();
                    TextBox txtBox2 = new TextBox();
                    txtBox2.ID = "txtRepaidPer" + i.ToString();
                    //txtBox2.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox2.CssClass = "txtField";
                    tc.Controls.Add(txtBox2);
                    tr.Cells.Add(tc);
                    //// Fourth Cell
                    //tc = new TableCell();
                    //tr.Cells.Add(tc);

                    tb.Rows.Add(tr);
                }
                PlaceHolder2.Controls.Add(tb);
                Session["table"] = tb;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:ShowMoneyBackContent()");
                object[] objects = new object[1];
                objects[0] = count;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlInsuranceIssuerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customerAccountVo.AssetCategory.ToString().Trim() == "INUP")
            {
                LoadUlipPlan();
            }
        }

        public void LoadUlipPlan()
        {
            DataSet ds = assetBo.GetULIPPlans(ddlInsuranceIssuerCode.SelectedItem.Value.ToString());
            ddlUlipPlans.DataSource = ds;
            ddlUlipPlans.DataTextField = ds.Tables[0].Columns["WUP_ULIPPlanName"].ToString();
            ddlUlipPlans.DataValueField = ds.Tables[0].Columns["WUP_ULIPPlanCode"].ToString();
            ddlUlipPlans.DataBind();
            ddlUlipPlans.Items.Insert(0, new ListItem("Select a ULIP Plan", "Select a ULIP Plan"));
            trULIPAllocation.Visible = true;
            pnlUlip.Visible = true;
        }

        public void LoadUlipSubPlans(string UlipPlan)
        {
            try
            {
                PlaceHolder1.Controls.Clear();
                DataSet ds = assetBo.GetULIPSubPlans(int.Parse(UlipPlan));
                int count = ds.Tables[0].Rows.Count;
                // LiteralControl literal = new LiteralControl();
                Table tb = new Table();
                TableCell tc;

                for (int i = 0; i < count; i++)
                {
                    TableRow tr = new TableRow();

                    tc = new TableCell();
                    TextBox txtBox1 = new TextBox();
                    txtBox1.ID = "txtSubPlanId" + i.ToString();
                    // txtBox1.ID = ds.Tables[0].Rows[i][0].ToString();                 
                    txtBox1.CssClass = "txtField";
                    txtBox1.Text = ds.Tables[0].Rows[i]["WUSP_ULIPSubPlanName"].ToString();
                    txtBox1.Enabled = false;
                    tc.Controls.Add(txtBox1);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox2 = new TextBox();
                    txtBox2.ID = "txtUnitsId" + i.ToString();
                    //txtBox2.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox2.CssClass = "txtField";
                    tc.Controls.Add(txtBox2);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox3 = new TextBox();
                    txtBox3.ID = "txtPurchasePriceId" + i.ToString();
                    // txtBox3.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox3.CssClass = "txtField";
                    tc.Controls.Add(txtBox3);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox4 = new TextBox();
                    txtBox4.ID = "txtPurchaseDateId" + i.ToString();
                    // txtBox3.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox4.CssClass = "txtField";
                    tc.Controls.Add(txtBox4);

                    //// Bind Ajax Date Controls
                    //ce = new CalendarExtender();
                    //ce.ID = "txtPurchaseDateId" + i.ToString() + "_CalendarExtender";
                    //ce.TargetControlID = "txtPurchaseDateId" + i.ToString();
                    //ce.Format = "dd/MM/yyyy";
                    ////ce.EnableViewState = true;

                    //txtWE = new TextBoxWatermarkExtender();
                    //txtWE.ID = "txtPurchaseDateId" + i.ToString() + "_TextBoxWatermarkExtender";
                    //txtWE.TargetControlID = "txtPurchaseDateId" + i.ToString();
                    //txtWE.WatermarkText = "dd/mm/yyyy";
                    ////txtWE.EnableViewState = true;

                    //tc.Controls.Add(ce);
                    //tc.Controls.Add(txtWE);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox5 = new TextBox();
                    txtBox5.ID = "txtAllocationId" + i.ToString();
                    txtBox5.CssClass = "txtField";
                    tc.Controls.Add(txtBox5);
                    tr.Cells.Add(tc);

                    tb.Rows.Add(tr);
                }

                PlaceHolder1.Controls.Add(tb);
                trULIPAllocation.Visible = true;
                Session["table"] = tb;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:LoadUlipSubPlans()");
                object[] objects = new object[1];
                objects[0] = UlipPlan;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlUlipPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUlipSubPlans(ddlUlipPlans.SelectedValue.ToString().Trim());
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControls("edit", customerAccountVo.AssetCategory.ToString().Trim());
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (insuranceBo.DeleteInsurancePortfolio(insuranceVo.CustInsInvId, insuranceVo.AccountId))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
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
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:btnDelete_Click()");
                object[] objects = new object[1];
                objects[0] = insuranceVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
        }

        protected void txtLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.Parse(txtFirstPremiumDate.Text);
            DateTime dtTo = DateTime.Parse(txtLastPremiumDate.Text);
            DateBo dtBo = new DateBo();

            float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
            txtEPPremiumDuration.Text = NoOfMonths.ToString("f2");
        }

        protected void txtWLPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.Parse(txtWLPFirstPremiumDate.Text);
            DateTime dtTo = DateTime.Parse(txtWLPLastPremiumDate.Text);
            DateBo dtBo = new DateBo();

            float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
            txtWLPPremiumDuration.Text = NoOfMonths.ToString("f2");
        }

        protected void txtMPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.Parse(txtMPFirstPremiumDate.Text);
            DateTime dtTo = DateTime.Parse(txtMPLastPremiumDate.Text);
            DateBo dtBo = new DateBo();

            float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
            txtMPPremiumDuration.Text = NoOfMonths.ToString("f2");
        }

        protected void txtTPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = DateTime.Parse(txtTPFirstPremiumDate.Text);
            DateTime dtTo = DateTime.Parse(txtTPLastPremiumDate.Text);
            DateBo dtBo = new DateBo();

            float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
            txtTPPremiumDuration.Text = NoOfMonths.ToString("f2");
        }

        protected void txtPolicyMaturity_TextChanged(object sender, EventArgs e)
        {
            if (customerAccountVo.AssetCategory.Trim() == "INMP")
            {
                DateTime dtFrom = DateTime.Parse(txtPolicyCommencementDate.Text);
                DateTime dtTo = DateTime.Parse(txtPolicyMaturity.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtMPPolicyTerm.Text = NoOfMonths.ToString("f2");
            }
        }
    }
}
