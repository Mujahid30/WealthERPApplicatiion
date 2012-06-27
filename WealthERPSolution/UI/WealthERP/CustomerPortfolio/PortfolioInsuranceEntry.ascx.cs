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
using BoOps;
using Telerik.Web.UI;

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
        DataTable dtULIPSubPlanSchedule = new DataTable();

        int count;
        int insuranceId;
        string AssetGroupCode = "IN";
        string Manage = string.Empty;
        string path = "";

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

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

                if (!IsPostBack)
                {
                    Session["moneyBackEpisodeList"] = null;
                    // Check Querystring to see if its an Edit/View/Entry
                    if (Request.QueryString["action"] != null)
                        Manage = Request.QueryString["action"].ToString();

                    ClearFields();
                    LoadNominees();
                    BindDropDowns(path, customerAccountVo.AssetCategory.ToString().Trim());
                    LoadInsuranceIssuerCode(path);
                    if (Session["insuranceVo"] != null)
                    BindAssetParticular(insuranceVo.InsuranceIssuerCode);
                    LoadInsuranceIssuerDateTP(path);
                    LoadInsuranceIssuerDateULIP(path);
                    LoadInsuranceIssuerDateMP(path);
                    LoadInsuranceIssuerDatePAY(path);
                    LoadInsuranceIssuerDateTR(path);
                    LoadInsuranceIssuerDateOT(path);
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
                else
                {
                    if (customerAccountVo.AssetCategory.Trim() == "INUP")
                    {
                        if (ddlAssetPerticular.SelectedIndex != 0 && ddlAssetPerticular.SelectedIndex != -1)
                        {
                            //LoadUlipSubPlans(ddlUlipPlans.SelectedValue.ToString().Trim());
                        }
                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INMP")
                    {
                        if (txtMoneyBackEpisode.Text.Trim() != string.Empty && txtMoneyBackEpisode.Text.Trim() != "0")
                        {
                            Int32.TryParse(txtMoneyBackEpisode.Text.Trim(), out count);
                            ShowMoneyBackContent(count);
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

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //this.rgULIPSubPlanSchedule.MasterTableView.Items[0].Edit = true;
                //this.rgULIPSubPlanSchedule.MasterTableView.Rebind();
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

            trOTPremiumAmount.Visible = false;
            trOTPremiumPeriod.Visible = false;
            trOTPremiumFirstLast.Visible = false;
            trOTGracePeriod.Visible = false;
            trOTBonus.Visible = false;
            trOTSurrenderValue.Visible = false;
            trOTRemarks.Visible = false;

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
            //trULIPSchemeBasket.Visible = false;
            //trULIPAllocation.Visible = false;
            pnlUlip.Visible = false;
            trULIPError.Visible = false;
            trULIPSurrenderValue.Visible = false;
            trULIPCharges.Visible = false;
            trULIPRemarks.Visible = false;
            trUlipPremiumAmount.Visible = false;

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
                else if (CategoryCode == "INOT")
                {
                    ddlOTPremiumFrequencyCode.DataSource = dt;
                    ddlOTPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlOTPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlOTPremiumFrequencyCode.DataBind();
                    ddlOTPremiumFrequencyCode.Items.Insert(0, new ListItem("Select a Frequency Code", "Select a Frequency Code"));

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
        public void LoadInsuranceIssuerDateTP(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlTPPrPayDate.DataSource = dt;
            ddlTPPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlTPPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlTPPrPayDate.DataBind();
            ddlTPPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }
        public void LoadInsuranceIssuerDateULIP(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlULIPPrPayDate.DataSource = dt;
            ddlULIPPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlULIPPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlULIPPrPayDate.DataBind();
            ddlULIPPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }
        public void LoadInsuranceIssuerDatePAY(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlWLPPrPayDate.DataSource = dt;
            ddlWLPPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlWLPPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlWLPPrPayDate.DataBind();
            ddlWLPPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }

        public void LoadInsuranceIssuerDateMP(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlMPPrPayDate.DataSource = dt;
            ddlMPPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlMPPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlMPPrPayDate.DataBind();
            ddlMPPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }

        public void LoadInsuranceIssuerDateTR(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlEPPrPayDate.DataSource = dt;
            ddlEPPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlEPPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlEPPrPayDate.DataBind();
            ddlEPPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }
        public void LoadInsuranceIssuerDateOT(string path)
        {
            DataTable dt = XMLBo.GetInsuranceIssuerDate(path);
            ddlOTPrPayDate.DataSource = dt;
            ddlOTPrPayDate.DataSource = dt;
            ddlOTPrPayDate.DataTextField = dt.Columns["Name"].ToString();
            ddlOTPrPayDate.DataValueField = dt.Columns["Code"].ToString();
            ddlOTPrPayDate.DataBind();
            ddlOTPrPayDate.Items.Insert(0, new ListItem("Select Premium Date", "Select Premium Date"));
        }
        private void SetControls(string action, InsuranceVo insuranceVo, CustomerAccountsVo customerAccountVo, string path)
        {
            List<InsuranceULIPVo> insuranceULIPList = new List<InsuranceULIPVo>();
            try
            {
                //Varibles to calculate the premium duration 
                DateTime dtFrom = DateTime.MinValue;
                DateTime dtTo = DateTime.MinValue;
                DateBo dtBo = new DateBo();
                float noOfMonths = 0;


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
                    //txtName.Text = insuranceVo.Name.Trim();
                    ddlInsuranceIssuerCode.SelectedValue = insuranceVo.InsuranceIssuerCode.Trim();
                    ddlAssetPerticular.SelectedValue = insuranceVo.SchemeId.ToString().Trim();                    
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
                        ddlEPPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtEPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtEPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtEPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtEPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtEPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtEPRemarks.Text = insuranceVo.Remarks.ToString();

                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INOT")
                    {
                        txtOTpremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlOTPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtOTPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        ddlOTPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtOTLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtOTFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtOTGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtOTPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtOTBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtOTSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtOTMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtOTRemarks.Text = insuranceVo.Remarks.ToString();

                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INMP")
                    {
                        txtMPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlMPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtMPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        ddlMPPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtMPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtMPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        dtFrom = DateTime.Parse(txtMPFirstPremiumDate.Text);
                        dtTo = DateTime.Parse(txtMPLastPremiumDate.Text);
                        noOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                        txtMPPremiumDuration.Text = noOfMonths.ToString("f2");
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
                                if (moneyBackEpisodeVo.CIMBE_RepaymentDate != DateTime.MinValue)
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
                        ddlTPPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtTPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtTPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtTPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtTPPremiumAccum.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtWLPRemarks.Text = insuranceVo.Remarks.ToString();
                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INUP")
                    {
                        ddlULIPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        ddlULIPPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtULIPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtULIPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtULIPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtUlipPremiumInstAmt.Text = insuranceVo.PremiumAmount.ToString();

                        // Get the ULIP Plan Code from CustomerInsuraceULIPPlan Table
                        DataSet dsUlipPlanCode = insuranceBo.GetUlipPlanCode(insuranceVo.CustInsInvId);
                        string schemeId = insuranceVo.SchemeId.ToString().Trim();
                        //BindRadGridULIPSubPlanSchedule(schemeId);

                        insuranceULIPList = insuranceBo.GetInsuranceULIPList(insuranceVo.CustInsInvId);
                        BindRadGridULIPSubPlanSchedule(insuranceVo.CustInsInvId.ToString().Trim());
                        Session["insuranceULIPList"] = insuranceULIPList;

                        txtULIPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtULIPMaturityValue.Text = insuranceVo.BonusAccumalated.ToString();
                        txtULIPCharges.Text = insuranceVo.ULIPCharges.ToString();
                        txtULIPRemarks.Text = insuranceVo.Remarks.ToString();
                    }
                    else if (customerAccountVo.AssetCategory.Trim() == "INWP")
                    {
                        txtWLPPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                        ddlWLPPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                        txtWLPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                        ddlWLPPrPayDate.SelectedValue = insuranceVo.PremiumPaymentDate.ToString();
                        txtWLPFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                        txtWLPLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                        txtWLPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                        txtWPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                        txtWPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                        txtWPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                        txtWPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                        txtWLPRemarks.Text = insuranceVo.Remarks.ToString();
                    }
                    //if (customerAccountVo.AssetCategory.Trim() == "INOT")
                    //{ 
                    //    txtOTPremiumAmount.Text = insuranceVo.PremiumAmount.ToString();
                    //    ddlOTPremiumFrequencyCode.SelectedValue = insuranceVo.PremiumFrequencyCode.ToString().Trim();
                    //    txtEPPremiumDuration.Text = insuranceVo.PremiumDuration.ToString();
                    //    ddlEPPrPayDate.SelectedItem.Text = insuranceVo.PremiumPaymentDate.ToString();
                    //    txtLastPremiumDate.Text = insuranceVo.LastPremiumDate.ToShortDateString();
                    //    txtFirstPremiumDate.Text = insuranceVo.FirstPremiumDate.ToShortDateString();
                    //    txtEPGracePeriod.Text = insuranceVo.GracePeriod.ToString();
                    //    txtEPPremiumAccumulated.Text = insuranceVo.PremiumAccumalated.ToString();
                    //    txtEPBonusAccumulated.Text = insuranceVo.BonusAccumalated.ToString();
                    //    txtEPSurrenderValue.Text = insuranceVo.SurrenderValue.ToString();
                    //    txtEPMaturityValue.Text = insuranceVo.MaturityValue.ToString();
                    //    txtEPRemarks.Text = insuranceVo.Remarks.ToString();
                    //}

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
                //txtName.Text = "";
                ddlAssetPerticular.SelectedIndex = 0;
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
                    ddlEPPrPayDate.Text = "";
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
                    ddlMPPrPayDate.Text = "";
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
                    ddlTPPrPayDate.Text = "";
                    txtTPLastPremiumDate.Text = "";
                    txtTPFirstPremiumDate.Text = "";
                    txtTPGracePeriod.Text = "";
                    txtTPPremiumAccum.Text = "";
                    txtWLPRemarks.Text = "";
                }
                else if (CategoryCode == "INUP")
                {
                    ddlULIPPremiumFrequencyCode.SelectedIndex = -1;
                    ddlULIPPrPayDate.Text = "";
                    //ddlUlipPlans.SelectedIndex = -1;
                    ddlAssetPerticular.SelectedIndex = -1;

                    txtULIPLastPremiumDate.Text = "";
                    txtULIPFirstPremiumDate.Text = "";
                    txtULIPSurrenderValue.Text = "";
                    txtULIPMaturityValue.Text = "";
                    txtULIPCharges.Text = "";
                    txtULIPRemarks.Text = "";
                    Session.Remove("ULIPSubPlanSchedule");
                }
                else if (CategoryCode == "INWP")
                {
                    txtWLPPremiumAmount.Text = "";
                    ddlWLPPremiumFrequencyCode.SelectedIndex = -1;
                    txtWLPPremiumDuration.Text = "";
                    ddlWLPPrPayDate.Text = "";
                    txtWLPFirstPremiumDate.Text = "";
                    txtWLPLastPremiumDate.Text = "";
                    txtWLPGracePeriod.Text = "";
                    txtWPPremiumAccumulated.Text = "";
                    txtWPBonusAccumulated.Text = "";
                    txtWPSurrenderValue.Text = "";
                    txtWPMaturityValue.Text = "";
                    txtWLPRemarks.Text = "";
                }
                else if (CategoryCode == "INOT")
                {
                    txtOTpremiumAmount.Text = "";
                    ddlOTPremiumFrequencyCode.SelectedIndex = -1;
                    txtOTPremiumDuration.Text = "";
                    ddlOTPrPayDate.Text = "";
                    txtOTFirstPremiumDate.Text = "";
                    txtOTLastPremiumDate.Text = "";
                    txtOTGracePeriod.Text = "";
                    txtOTPremiumAccumulated.Text = "";
                    txtOTBonusAccumulated.Text = "";
                    txtOTSurrenderValue.Text = "";
                    txtOTMaturityValue.Text = "";
                    txtOTRemarks.Text = "";
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
                    trDeleteButton.Visible = true;
                    trEditButton.Visible = true;
                    trEditSpace.Visible = true;
                    trSubmitButton.Visible = false;
                    btnSubmit.Text = "";

                    //pnlforULIP.Visible = false;
                    //txtName.Enabled = false;
                    ddlAssetPerticular.Enabled = false;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = false;
                    txtPolicyCommencementDate.Enabled = false;
                    txtPolicyMaturity.Enabled = false;
                    txtSumAssured.Enabled = false;
                    txtApplicationNumber.Enabled = false;
                    txtApplDate.Enabled = false;
                    txtPolicyTerms.Enabled = false;
                    pnlGridView.Visible = false;
                    trULIPAllocation.Visible = false;
                    rgULIPSubPlanSchedule.Visible = false;
                    rgULIPSubPlanSchedule.Enabled = false;

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
                        ddlEPPrPayDate.Enabled = false;
                        txtFirstPremiumDate.Enabled = false;
                        txtLastPremiumDate.Enabled = false;
                        txtEPGracePeriod.Enabled = false;
                        txtEPPremiumAccumulated.Enabled = false;
                        txtEPBonusAccumulated.Enabled = false;
                        txtEPSurrenderValue.Enabled = false;
                        txtEPMaturityValue.Enabled = false;
                        txtEPRemarks.Enabled = false;
                    }
                    else if (CategoryCode == "INOT")
                    {
                        trOTPremiumAmount.Visible = true;
                        trOTPremiumPeriod.Visible = true;
                        trOTPremiumFirstLast.Visible = true;
                        trOTGracePeriod.Visible = true;
                        trOTBonus.Visible = true;
                        trOTSurrenderValue.Visible = true;
                        trOTRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtOTpremiumAmount.Enabled = false;
                        ddlOTPremiumFrequencyCode.Enabled = false;
                        txtOTPremiumDuration.Enabled = false;
                        ddlOTPrPayDate.Enabled = false;
                        txtOTFirstPremiumDate.Enabled = false;
                        txtOTLastPremiumDate.Enabled = false;
                        txtOTGracePeriod.Enabled = false;
                        txtOTPremiumAccumulated.Enabled = false;
                        txtOTBonusAccumulated.Enabled = false;
                        txtOTSurrenderValue.Enabled = false;
                        txtOTMaturityValue.Enabled = false;
                        txtOTRemarks.Enabled = false;
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
                        ddlMPPrPayDate.Enabled = false;
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
                        if (count > 0)
                        {
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
                        else
                            trMPDetails.Visible = false;
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
                        ddlTPPrPayDate.Enabled = false;
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
                        //trULIPSchemeBasket.Visible = true;
                        //trULIPAllocation.Visible = true;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;
                        trUlipPremiumAmount.Visible = true;
                        txtUlipPremiuimPeriod.Enabled = false;
                        txtUlipPremiumInstAmt.Enabled = false;
                        ddlULIPPremiumFrequencyCode.Enabled = false;
                        ddlULIPPrPayDate.Enabled = false;
                        txtULIPFirstPremiumDate.Enabled = false;
                        txtULIPLastPremiumDate.Enabled = false;
                        txtULIPGracePeriod.Enabled = false;
                        //ddlUlipPlans.Enabled = false;
                        ddlAssetPerticular.Enabled = false;
                        pnlGridView.Visible = true;
                        trULIPAllocation.Visible = true;
                        rgULIPSubPlanSchedule.Visible = true;
                        rgULIPSubPlanSchedule.Enabled = false;
                        gvNominee.Enabled = false;
                        txtPolicyTerms.Enabled = false;
                        // Get ULIP Sub-Plans Count
                        #region NotNeededAnyMore
                        
                        //DataSet ds = assetBo.GetULIPSubPlans(schemeid);
                        //int count = ds.Tables[0].Rows.Count;
                        //for (int i = 0; i < count; i++)
                        //{
                        //    TextBox txtBox1 = new TextBox();
                        //    txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtSubPlanId" + i.ToString()));
                        //    txtBox1.Enabled = false;

                        //    TextBox txtBox2 = new TextBox();
                        //    txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString()));
                        //    txtBox2.Enabled = false;

                        //    TextBox txtBox3 = new TextBox();
                        //    txtBox3 = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString()));
                        //    txtBox3.Enabled = false;

                        //    TextBox txtBox4 = new TextBox();
                        //    txtBox4 = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString()));
                        //    txtBox4.Enabled = false;

                        //    TextBox txtBox5 = new TextBox();
                        //    txtBox5 = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString()));
                        //    txtBox5.Enabled = false;
                        //}
                        #endregion

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
                        ddlWLPPrPayDate.Enabled = false;
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

                    //pnlforULIP.Visible = false;
                    //txtName.Enabled = true;
                    ddlAssetPerticular.Enabled = true;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = true;
                    txtPolicyCommencementDate.Enabled = true;
                    txtPolicyMaturity.Enabled = true;
                    txtSumAssured.Enabled = true;
                    txtApplicationNumber.Enabled = true;
                    txtApplDate.Enabled = true;
                    txtPolicyTerms.Enabled = true;
                    pnlGridView.Visible = false;
                    trULIPAllocation.Visible = false;
                    rgULIPSubPlanSchedule.Visible = false;
                    rgULIPSubPlanSchedule.Enabled = false;

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
                        ddlEPPrPayDate.Enabled = true;
                        txtFirstPremiumDate.Enabled = true;
                        txtLastPremiumDate.Enabled = true;
                        txtEPGracePeriod.Enabled = true;
                        txtEPPremiumAccumulated.Enabled = true;
                        txtEPBonusAccumulated.Enabled = true;
                        txtEPSurrenderValue.Enabled = true;
                        txtEPMaturityValue.Enabled = true;
                        txtEPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INOT")
                    {
                        trOTPremiumAmount.Visible = true;
                        trOTPremiumPeriod.Visible = true;
                        trOTPremiumFirstLast.Visible = true;
                        trOTGracePeriod.Visible = true;
                        trOTBonus.Visible = true;
                        trOTSurrenderValue.Visible = true;
                        trOTRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtOTpremiumAmount.Enabled = true;
                        ddlOTPremiumFrequencyCode.Enabled = true;
                        txtOTPremiumDuration.Enabled = true;
                        ddlOTPrPayDate.Enabled = true;
                        txtOTFirstPremiumDate.Enabled = true;
                        txtOTLastPremiumDate.Enabled = true;
                        txtOTGracePeriod.Enabled = true;
                        txtOTPremiumAccumulated.Enabled = true;
                        txtOTBonusAccumulated.Enabled = true;
                        txtOTSurrenderValue.Enabled = true;
                        txtOTMaturityValue.Enabled = true;
                        txtOTRemarks.Enabled = true;
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
                        ddlMPPrPayDate.Enabled = true;
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
                        if (count > 0)
                        {
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
                        else
                            trMPDetails.Visible = false;
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
                        ddlTPPrPayDate.Enabled = true;
                        txtTPFirstPremiumDate.Enabled = true;
                        txtTPLastPremiumDate.Enabled = true;
                        txtTPGracePeriod.Enabled = true;
                        txtTPPremiumAccum.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INUP")
                    {
                        pnlGridView.Visible = true;
                        trULIPAllocation.Visible = true;
                        rgULIPSubPlanSchedule.Visible = true;
                        rgULIPSubPlanSchedule.Enabled = true;

                        trValuationHeader.Visible = true;
                        trULIPPremiumCycle.Visible = true;
                        trULIPPremiumFirstLast.Visible = true;
                        trULIPGracePeriod.Visible = true;
                        trULIPHeader.Visible = true;
                        //trULIPSchemeBasket.Visible = true;
                        //trULIPAllocation.Visible = true;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;
                        trUlipPremiumAmount.Visible = true;
                        txtUlipPremiuimPeriod.Enabled = true;
                        txtUlipPremiumInstAmt.Enabled = true;
                        ddlULIPPremiumFrequencyCode.Enabled = true;
                        ddlULIPPrPayDate.Enabled = true;
                        txtULIPFirstPremiumDate.Enabled = true;
                        txtULIPLastPremiumDate.Enabled = true;
                        txtULIPGracePeriod.Enabled = true;
                        //ddlUlipPlans.Enabled = true;
                        ddlAssetPerticular.Enabled = true;
                        //ddlUlipSubPlans.Enabled = true;
                        txtPolicyTerms.Enabled = true;
                        // Get ULIP Sub-Plans Count
                        //DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlAssetPerticular.SelectedValue.ToString()));
                        //int count = ds.Tables[0].Rows.Count;
                        //for (int i = 0; i < count; i++)
                        //{
                        //    TextBox txtBox1 = new TextBox();
                        //    txtBox1 = ((TextBox)PlaceHolder1.FindControl("txtSubPlanId" + i.ToString()));
                        //    txtBox1.Enabled = false;

                        //    TextBox txtBox2 = new TextBox();
                        //    txtBox2 = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString()));
                        //    txtBox2.Enabled = true;

                        //    TextBox txtBox3 = new TextBox();
                        //    txtBox3 = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString()));
                        //    txtBox3.Enabled = true;

                        //    TextBox txtBox4 = new TextBox();
                        //    txtBox4 = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString()));
                        //    txtBox4.Enabled = true;

                        //    TextBox txtBox5 = new TextBox();
                        //    txtBox5 = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString()));
                        //    txtBox5.Enabled = true;
                        //}

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
                        ddlWLPPrPayDate.Enabled = true;
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
                    
                    //pnlforULIP.Visible = false;
                    //txtName.Enabled = true;
                    ddlAssetPerticular.Enabled = true;
                    txtPolicyNumber.Enabled = false;
                    ddlInsuranceIssuerCode.Enabled = true;
                    txtPolicyCommencementDate.Enabled = true;
                    txtPolicyMaturity.Enabled = true;
                    txtSumAssured.Enabled = true;
                    txtApplicationNumber.Enabled = true;
                    txtApplDate.Enabled = true;
                    txtPolicyTerms.Enabled = true;
                    pnlGridView.Visible = false;
                    trULIPAllocation.Visible = false;
                    rgULIPSubPlanSchedule.Visible = false;
                    rgULIPSubPlanSchedule.Enabled = false;

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
                        ddlEPPrPayDate.Enabled = true;
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
                        trMPDetails.Visible = false;
                        pnlMoneyBackEpisode.Visible = true;
                        trMPSpace.Visible = true;
                        trValuationHeader.Visible = true;

                        txtMPPremiumAmount.Enabled = true;
                        ddlMPPremiumFrequencyCode.Enabled = true;
                        txtMPPremiumDuration.Enabled = true;
                        ddlMPPrPayDate.Enabled = true;
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
                        ddlTPPrPayDate.Enabled = true;
                        txtTPFirstPremiumDate.Enabled = true;
                        txtTPLastPremiumDate.Enabled = true;
                        txtTPGracePeriod.Enabled = true;
                        txtTPPremiumAccum.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INUP")
                    {
                        pnlGridView.Visible = true;
                        trULIPAllocation.Visible = true;
                        rgULIPSubPlanSchedule.Visible = true;
                        rgULIPSubPlanSchedule.Enabled = true;

                        trValuationHeader.Visible = true;
                        trULIPPremiumCycle.Visible = true;
                        trULIPPremiumFirstLast.Visible = true;
                        trULIPGracePeriod.Visible = true;
                        trULIPHeader.Visible = true;
                        //trULIPSchemeBasket.Visible = true;
                        //trULIPAllocation.Visible = false;
                        pnlUlip.Visible = true;
                        trULIPSurrenderValue.Visible = true;
                        trULIPCharges.Visible = true;
                        trULIPRemarks.Visible = true;
                        // Error Tr
                        trULIPError.Visible = false;
                        trUlipPremiumAmount.Visible = true;

                        txtUlipPremiumInstAmt.Enabled = true;
                        txtUlipPremiuimPeriod.Enabled = true;
                        ddlULIPPremiumFrequencyCode.Enabled = true;
                        ddlULIPPrPayDate.Enabled = true;
                        //ddlUlipPlans.Enabled = true;
                        ddlAssetPerticular.Enabled = true;
                        txtULIPFirstPremiumDate.Enabled = true;
                        txtULIPLastPremiumDate.Enabled = true;
                        txtPolicyTerms.Enabled = true;

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
                        ddlWLPPrPayDate.Enabled = true;
                        txtWLPFirstPremiumDate.Enabled = true;
                        txtWLPLastPremiumDate.Enabled = true;
                        txtWLPGracePeriod.Enabled = true;
                        txtWPPremiumAccumulated.Enabled = true;
                        txtWPBonusAccumulated.Enabled = true;
                        txtWPSurrenderValue.Enabled = true;
                        txtWPMaturityValue.Enabled = true;
                        txtWLPRemarks.Enabled = true;
                    }
                    else if (CategoryCode == "INOT")
                    {
                        trOTPremiumAmount.Visible = true;
                        trOTPremiumPeriod.Visible = true;
                        trOTPremiumFirstLast.Visible = true;
                        trOTGracePeriod.Visible = true;
                        trOTBonus.Visible = true;
                        trOTSurrenderValue.Visible = true;
                        trOTRemarks.Visible = true;
                        trValuationHeader.Visible = true;

                        txtOTpremiumAmount.Enabled = true;
                        ddlOTPremiumFrequencyCode.Enabled = true;
                        txtOTPremiumDuration.Enabled = true;
                        ddlOTPrPayDate.Enabled = true;
                        txtOTFirstPremiumDate.Enabled = true;
                        txtOTLastPremiumDate.Enabled = true;
                        txtOTGracePeriod.Enabled = true;
                        txtOTPremiumAccumulated.Enabled = true;
                        txtOTBonusAccumulated.Enabled = true;
                        txtOTSurrenderValue.Enabled = true;
                        txtOTMaturityValue.Enabled = true;
                        txtOTRemarks.Enabled = true;

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
                        //insuranceVo.Name = txtName.Text;
                        insuranceVo.Name = ddlAssetPerticular.SelectedItem.ToString();
                        if (ddlAssetPerticular.SelectedValue != "Select")
                        insuranceVo.SchemeId = int.Parse(ddlAssetPerticular.SelectedValue);
                        insuranceVo.PolicyNumber = customerAccountVo.PolicyNum;
                        insuranceVo.InsuranceIssuerCode = ddlInsuranceIssuerCode.SelectedValue.ToString();
                        insuranceVo.StartDate = DateTime.Parse(txtPolicyCommencementDate.Text.Trim());
                        insuranceVo.EndDate = DateTime.Parse(txtPolicyMaturity.Text.Trim());
                        insuranceVo.SumAssured = double.Parse(txtSumAssured.Text);
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

                            insuranceVo.PremiumPaymentDate = Int16.Parse(ddlEPPrPayDate.Text);
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
                        else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INOT")
                        {
                            if (txtOTpremiumAmount.Text.Trim() != "")
                                insuranceVo.PremiumAmount = float.Parse(txtOTpremiumAmount.Text.Trim());
                            insuranceVo.PremiumFrequencyCode = ddlOTPremiumFrequencyCode.SelectedValue.ToString().Trim();
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtOTFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtOTLastPremiumDate.Text.ToString());

                            insuranceVo.PremiumPaymentDate = Int16.Parse(ddlOTPrPayDate.SelectedValue);
                            if (txtOTGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtOTGracePeriod.Text);
                            if (txtOTPremiumAccumulated.Text.Trim() != "")
                                insuranceVo.PremiumAccumalated = float.Parse(txtOTPremiumAccumulated.Text);
                            if (txtOTBonusAccumulated.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtOTBonusAccumulated.Text);
                            if (txtOTSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtOTSurrenderValue.Text);
                            if (txtOTMaturityValue.Text.Trim() != "")
                                insuranceVo.MaturityValue = float.Parse(txtOTMaturityValue.Text);
                            insuranceVo.Remarks = txtOTRemarks.Text.Trim();

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

                            insuranceVo.PremiumPaymentDate = Int16.Parse(ddlWLPPrPayDate.Text.Trim());
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

                            insuranceVo.PremiumPaymentDate = Int16.Parse(ddlTPPrPayDate.Text.Trim());
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
                            if (txtUlipPremiumInstAmt.Text == "")
                                insuranceVo.PremiumAmount = 0;
                            else
                                insuranceVo.PremiumAmount = double.Parse(txtUlipPremiumInstAmt.Text);
                            if (txtUlipPremiuimPeriod.Text == "")
                                insuranceVo.PremiumDuration = 0;
                            else
                                insuranceVo.PremiumDuration = float.Parse(txtUlipPremiuimPeriod.Text);
                            insuranceVo.FirstPremiumDate = DateTime.Parse(txtULIPFirstPremiumDate.Text.ToString());
                            insuranceVo.LastPremiumDate = DateTime.Parse(txtULIPLastPremiumDate.Text.ToString());
                            insuranceVo.PremiumFrequencyCode = ddlULIPPremiumFrequencyCode.SelectedValue;

                            insuranceVo.PremiumAccumalated = 0;
                            insuranceVo.BonusAccumalated = 0;

                            if (txtULIPGracePeriod.Text.Trim() != "")
                                insuranceVo.GracePeriod = float.Parse(txtULIPGracePeriod.Text);
                            if (ddlULIPPrPayDate.Text.Trim() != "")
                                insuranceVo.PremiumPaymentDate = Int16.Parse(ddlULIPPrPayDate.Text.Trim());
                            if (txtULIPMaturityValue.Text.Trim() != "")
                                insuranceVo.BonusAccumalated = float.Parse(txtULIPMaturityValue.Text);
                            if (txtULIPSurrenderValue.Text.Trim() != "")
                                insuranceVo.SurrenderValue = float.Parse(txtULIPSurrenderValue.Text);
                            if (txtULIPCharges.Text.Trim() != "")
                                insuranceVo.ULIPCharges = float.Parse(txtULIPCharges.Text);
                            insuranceVo.Remarks = txtULIPRemarks.Text.Trim();

                            #region dont needed anymore
                            //DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedValue.ToString()));
                            //int count = ds.Tables[0].Rows.Count;
                            subPlanList = new List<float>();
                            insuranceUlipList = new List<InsuranceULIPVo>();
                            //float tot = 0;
                            //int txt = 0;


                            //// Calcuating the total Asset Allocation value
                            //for (int i = 0; i < count; i++)
                            //{
                            //    string temp = (((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString());
                            //    if (temp == "")
                            //        txt = 0;
                            //    else
                            //        txt = int.Parse(temp.ToString());

                            //    tot = tot + (float)txt;
                            //    subPlanList.Add(txt);
                            //}
                            #endregion


                            #region new code implemented by bhoopendra

                            DataTable dtSchedule = new DataTable();
                            dtSchedule = (DataTable)Session["ULIPSubPlanSchedule"];
                                //(DataTable)Session["SubmitAllocationSchedule"]
                            float Percentage = 0;
                            float totalPercentage = 0;

                            if (dtSchedule.Rows.Count > 0)
                            {
                                foreach (DataRow drSchedule in dtSchedule.Rows)
                                {
                                    Percentage = float.Parse(drSchedule["CINPUD_AllocationPer"].ToString());
                                    totalPercentage = Percentage + totalPercentage;
                                }

                                if (totalPercentage == 100)
                                {
                                    foreach (DataRow drSchedule in dtSchedule.Rows)
                                    {
                                        insuranceUlipVo = new InsuranceULIPVo();
                                        string allocPer = drSchedule["CINPUD_AllocationPer"].ToString();
                                        string investedCost = drSchedule["CINPUD_InvestedCost"].ToString();
                                        string currentValue = drSchedule["CINPUD_CurrentValue"].ToString();
                                        string unit = drSchedule["CINPUD_Unit"].ToString();
                                        //string purchaseDate = drSchedule["CINPUD_PurchaseDate"].ToString();
                                        string absoluteReturn = drSchedule["CINPUD_AbsoluteReturn"].ToString();
                                        string fundName = drSchedule["IF_FundName"].ToString();

                                        insuranceUlipVo.IssuerCode = ddlInsuranceIssuerCode.SelectedValue;
                                        insuranceUlipVo.WUP_ULIPSubPlaCode = drSchedule["ISF_SchemeFundId"].ToString();
                                        insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlAssetPerticular.SelectedValue.ToString());

                                        if (allocPer == null)
                                        {
                                            insuranceUlipVo.CIUP_AllocationPer = 0;
                                            insuranceUlipVo.CIUP_PurchasePrice = 0;
                                            insuranceUlipVo.CIUP_Unit = 0;
                                            insuranceUlipVo.CIUP_PurchaseDate = DateTime.MinValue;

                                            insuranceUlipVo.CIUP_InvestedCost = 0;
                                            insuranceUlipVo.CIUP_CurrentValue = 0;
                                            insuranceUlipVo.CIUP_AllocationPer = 0;
                                        }
                                        else
                                        {
                                            if (allocPer != string.Empty)
                                                insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocPer.ToString());

                                            if (fundName != string.Empty)
                                                insuranceUlipVo.WUP_ULIPSubPlaName = fundName.Trim().ToString();
                                            if (unit != string.Empty)
                                                insuranceUlipVo.CIUP_Unit = float.Parse(unit.ToString());
                                            //if (purchaseDate != string.Empty)
                                            //    insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());

                                            if (investedCost != string.Empty)
                                                insuranceUlipVo.CIUP_InvestedCost = float.Parse(investedCost.ToString());
                                            if (currentValue != string.Empty)
                                                insuranceUlipVo.CIUP_CurrentValue = float.Parse(currentValue.ToString());
                                            if (absoluteReturn != string.Empty)
                                                insuranceUlipVo.CIUP_AbsoluteReturn = float.Parse(absoluteReturn.ToString());
                                        }
                                        insuranceUlipList.Add(insuranceUlipVo);
                                    }
                            #endregion


                                    #region dont needed anymore
                                    //Check the total asset Allocation and assign Unit, Purchase price and Allocation percentage 
                                    //if (tot == 100)
                                    //{
                                    //    // lblError.Text = "Hundred";
                                    //    for (int i = 0; i < count; i++)
                                    //    {
                                    //        insuranceUlipVo = new InsuranceULIPVo();
                                    //        string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                                    //        string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                                    //        string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                                    //        string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                                    //        string investedCost = ((TextBox)PlaceHolder1.FindControl("txtInvestedCost" + i.ToString())).Text.ToString();
                                    //        string currentValue = ((TextBox)PlaceHolder1.FindControl("txtCurrentValue" + i.ToString())).Text.ToString();
                                    //        string absoluteReturn = ((TextBox)PlaceHolder1.FindControl("txtAbsoluteReturn" + i.ToString())).Text.ToString();

                                    //        insuranceUlipVo.WUP_ULIPSubPlaCode = ds.Tables[0].Rows[i][0].ToString();
                                    //        insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlUlipPlans.SelectedValue.ToString());

                                    //        if (allocationPer == "")
                                    //        {
                                    //            insuranceUlipVo.CIUP_AllocationPer = 0;
                                    //            insuranceUlipVo.CIUP_PurchasePrice = 0;
                                    //            insuranceUlipVo.CIUP_Unit = 0;
                                    //            insuranceUlipVo.CIUP_PurchaseDate = DateTime.MinValue;

                                    //            insuranceUlipVo.CIUP_InvestedCost = 0;
                                    //            insuranceUlipVo.CIUP_CurrentValue = 0;
                                    //            insuranceUlipVo.CIUP_AllocationPer = 0;
                                    //        }
                                    //        else
                                    //        {
                                    //            if (allocationPer != string.Empty)
                                    //                insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                                    //            if (purchasePrice != string.Empty)
                                    //                insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                                    //            if (units != string.Empty)
                                    //                insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                                    //            if (purchaseDate != string.Empty)
                                    //                insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());

                                    //            if (investedCost != string.Empty)
                                    //                insuranceUlipVo.CIUP_InvestedCost = float.Parse(investedCost.ToString());
                                    //            if (currentValue != string.Empty)
                                    //                insuranceUlipVo.CIUP_CurrentValue = float.Parse(currentValue.ToString());
                                    //            if (absoluteReturn != string.Empty)
                                    //                insuranceUlipVo.CIUP_AbsoluteReturn = float.Parse(absoluteReturn.ToString());
                                    //        }
                                    //        insuranceUlipList.Add(insuranceUlipVo);
                                    //    }
                                    #endregion


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
                                    Session.Remove("ULIPSubPlanSchedule");
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

                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlMPPrPayDate.Text.Trim());
                        if (txtMPGracePeriod.Text.Trim() != "")
                            insuranceVo.GracePeriod = float.Parse(txtMPGracePeriod.Text.Trim());
                        if (txtMPPremiumDuration.Text.Trim() != "")
                            insuranceVo.PremiumDuration = float.Parse(txtMPPremiumDuration.Text.Trim());
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
                                if (paymentDate != string.Empty)
                                    moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                if (repaidPercent != string.Empty)
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
                    //insuranceVo.Name = txtName.Text;
                    //insuranceVo.Name = ddlAssetPerticular.SelectedItem.ToString();
                    insuranceVo.InsuranceIssuerCode = ddlInsuranceIssuerCode.SelectedValue.ToString();
                    if (ddlAssetPerticular.SelectedValue != "Select")
                        insuranceVo.SchemeId = int.Parse(ddlAssetPerticular.SelectedValue);
                    insuranceVo.PolicyNumber = customerAccountVo.PolicyNum;                    
                    insuranceVo.StartDate = DateTime.Parse(txtPolicyCommencementDate.Text.Trim());
                    insuranceVo.EndDate = DateTime.Parse(txtPolicyMaturity.Text.Trim());
                    insuranceVo.SumAssured = double.Parse(txtSumAssured.Text);
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
                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlEPPrPayDate.SelectedItem.Text.Trim());

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
                    else if (insuranceVo.AssetInstrumentCategoryCode.ToString().Trim() == "INOT")
                    {
                        if (txtOTpremiumAmount.Text.Trim() != "")
                            insuranceVo.PremiumAmount = float.Parse(txtOTpremiumAmount.Text.Trim());
                        insuranceVo.PremiumFrequencyCode = ddlOTPremiumFrequencyCode.SelectedValue.ToString().Trim();
                        insuranceVo.FirstPremiumDate = DateTime.Parse(txtOTFirstPremiumDate.Text.ToString());
                        insuranceVo.LastPremiumDate = DateTime.Parse(txtOTLastPremiumDate.Text.ToString());

                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlOTPrPayDate.Text);
                        if (txtOTGracePeriod.Text.Trim() != "")
                            insuranceVo.GracePeriod = float.Parse(txtOTGracePeriod.Text);
                        if (txtOTPremiumAccumulated.Text.Trim() != "")
                            insuranceVo.PremiumAccumalated = float.Parse(txtOTPremiumAccumulated.Text);
                        if (txtOTBonusAccumulated.Text.Trim() != "")
                            insuranceVo.BonusAccumalated = float.Parse(txtOTBonusAccumulated.Text);
                        if (txtOTSurrenderValue.Text.Trim() != "")
                            insuranceVo.SurrenderValue = float.Parse(txtOTSurrenderValue.Text);
                        if (txtOTMaturityValue.Text.Trim() != "")
                            insuranceVo.MaturityValue = float.Parse(txtOTMaturityValue.Text);
                        insuranceVo.Remarks = txtOTRemarks.Text.Trim();

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

                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlWLPPrPayDate.SelectedItem.Text.Trim());
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

                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlTPPrPayDate.SelectedItem.Text.Trim());
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
                        List<InsuranceULIPVo> insuranceUlipListUpdate;
                        InsuranceULIPVo insuranceUlipVo;

                        //int PrevUlipSubPlanCode = 0;
                        //int PrevUlipPlanCode = 0;

                        insuranceVo.PremiumAmount = 0;
                        insuranceVo.PremiumDuration = 0;
                        if (txtUlipPremiumInstAmt.Text == "")
                            insuranceVo.PremiumAmount = 0;
                        else
                            insuranceVo.PremiumAmount = double.Parse(txtUlipPremiumInstAmt.Text);
                        if (txtUlipPremiuimPeriod.Text == "")
                            insuranceVo.PremiumDuration = 0;
                        else
                            insuranceVo.PremiumDuration = float.Parse(txtUlipPremiuimPeriod.Text);

                        insuranceVo.FirstPremiumDate = DateTime.Parse(txtULIPFirstPremiumDate.Text.ToString());
                        insuranceVo.LastPremiumDate = DateTime.Parse(txtULIPLastPremiumDate.Text.ToString());

                        insuranceVo.PremiumAccumalated = 0;
                        insuranceVo.BonusAccumalated = 0;

                        if (txtULIPGracePeriod.Text.Trim() != "")
                            insuranceVo.GracePeriod = float.Parse(txtULIPGracePeriod.Text);
                        if (ddlULIPPrPayDate.Text.Trim() != "")
                            insuranceVo.PremiumPaymentDate = Int16.Parse(ddlULIPPrPayDate.SelectedItem.Text.Trim());
                        if (txtULIPMaturityValue.Text.Trim() != "")
                            insuranceVo.BonusAccumalated = float.Parse(txtULIPMaturityValue.Text);
                        if (txtULIPSurrenderValue.Text.Trim() != "")
                            insuranceVo.SurrenderValue = float.Parse(txtULIPSurrenderValue.Text);
                        if (txtULIPCharges.Text.Trim() != "")
                            insuranceVo.ULIPCharges = float.Parse(txtULIPCharges.Text);
                        insuranceVo.Remarks = txtULIPRemarks.Text.Trim();

                        //DataSet prevUlipSubPlansDS = assetBo.GetPrevULIPSubPlans(insuranceVo.CustInsInvId);
                        //PrevUlipSubPlanCode = Int32.Parse(prevUlipSubPlansDS.Tables[0].Rows[0]["ISF_SchemeFundId"].ToString().Trim());
                        //DataSet prevUlipPlanCodeDS = assetBo.GetPrevUlipPlanCode(insuranceVo.SchemeId);
                        //PrevUlipPlanCode = Int32.Parse(prevUlipPlanCodeDS.Tables[0].Rows[0]["ISF_SchemeFundId"].ToString().Trim());

                        //DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlAssetPerticular.SelectedValue.ToString()));
                        //int count = ds.Tables[0].Rows.Count;

                        //// Calcuating the total Asset Allocation value
                        DataTable dtSchedule = new DataTable();
                        dtSchedule = (DataTable)Session["ULIPSubPlanSchedule"];
                        //dtSchedule = (DataTable)Session["SubmitAllocationSchedule"];
                        float totalPercentage = 0;

                        if (dtSchedule.Rows.Count > 0)
                        {
                            foreach (DataRow drSchedule in dtSchedule.Rows)
                            {
                                totalPercentage += float.Parse(drSchedule["CINPUD_AllocationPer"].ToString());
                            }

                            if (totalPercentage == 100)
                            {
                                // Do an Update Here
                                insuranceUlipListUpdate = new List<InsuranceULIPVo>();
                                insuranceUlipList = (List<InsuranceULIPVo>)Session["insuranceULIPList"];                                
                                foreach (DataRow drSchedule in dtSchedule.Rows)
                                {
                                    insuranceUlipVo = new InsuranceULIPVo();
                                    string allocPer = drSchedule["CINPUD_AllocationPer"].ToString();
                                    string investedCost = drSchedule["CINPUD_InvestedCost"].ToString();
                                    string currentValue = drSchedule["CINPUD_CurrentValue"].ToString();
                                    string unit = drSchedule["CINPUD_Unit"].ToString();
                                    //string purchaseDate = drSchedule["CINPUD_PurchaseDate"].ToString();
                                    string absoluteReturn = drSchedule["CINPUD_AbsoluteReturn"].ToString();
                                    string fundName = drSchedule["IF_FundName"].ToString();
                                    string ulipPlanId = drSchedule["ISF_SchemeFundId"].ToString();

                                    if (allocPer == null)
                                    {
                                        insuranceUlipVo.CIUP_AllocationPer = 0;
                                        insuranceUlipVo.CIUP_PurchasePrice = 0;
                                        insuranceUlipVo.CIUP_Unit = 0;
                                        insuranceUlipVo.CIUP_PurchaseDate = DateTime.MinValue;
                                        insuranceUlipVo.CIUP_InvestedCost = 0;
                                        insuranceUlipVo.CIUP_CurrentValue = 0;
                                        insuranceUlipVo.CIUP_AllocationPer = 0;
                                    }
                                    else
                                    {
                                        if (allocPer != string.Empty)
                                            insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocPer.ToString());

                                        if (fundName != string.Empty)
                                            insuranceUlipVo.WUP_ULIPSubPlaName = fundName.Trim().ToString();

                                        if (ulipPlanId != string.Empty)
                                            insuranceUlipVo.CIUP_ULIPPlanId = Convert.ToInt32(ulipPlanId.ToString());

                                        if (unit != string.Empty)
                                            insuranceUlipVo.CIUP_Unit = float.Parse(unit.ToString());
                                        //if (purchaseDate != string.Empty)
                                        //    insuranceUlipVo.CIUP_PurchaseDate = Convert.ToInt32(ulipPlanId.ToString());

                                        if (investedCost != string.Empty)
                                            insuranceUlipVo.CIUP_InvestedCost = float.Parse(investedCost.ToString());
                                        if (currentValue != string.Empty)
                                            insuranceUlipVo.CIUP_CurrentValue = float.Parse(currentValue.ToString());
                                        if (absoluteReturn != string.Empty)
                                            insuranceUlipVo.CIUP_AbsoluteReturn = float.Parse(absoluteReturn.ToString());
                                    }
                                    insuranceUlipListUpdate.Add(insuranceUlipVo);
                                }
                                if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                                {
                                    for (int i = 0; i < insuranceUlipListUpdate.Count; i++)
                                    {
                                        insuranceUlipVo = new InsuranceULIPVo();
                                        insuranceUlipVo = insuranceUlipListUpdate[i];
                                        insuranceUlipVo.CIP_CustInsInvId = insuranceVo.CustInsInvId;
                                        insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                                        insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                                        insuranceBo.UpdateInsuranceULIPPlan(insuranceUlipVo);
                                    }
                                    Session.Remove("ULIPSubPlanSchedule");
                                    Session.Remove("table");
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                                }
                            }
                            else
                            {
                                lblError.Text = "Check the Allocation";
                                lblError.CssClass = "Error";
                                trULIPError.Visible = true;
                            }
                        }
                        #region dont needed any more
                        // Check the total asset Allocation and assign Unit, Purchase price and Allocation percentage 
                        //if (tot == 100)
                        //{
                        //    // If the Drop Down Selection has changed then 
                        //    // Delete the previous records saved 
                        //    // Update the new records

                        //    if (int.Parse(ddlUlipPlans.SelectedValue.ToString()) == PrevUlipPlanCode)
                        //    {
                        //        // Do an Update Here
                        //        insuranceUlipList = new List<InsuranceULIPVo>();
                        //        insuranceUlipList = (List<InsuranceULIPVo>)Session["insuranceULIPList"];

                        //        for (int i = 0; i < count; i++)
                        //        {
                        //            //insuranceUlipVo = insuranceUlipList[i];
                        //            string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                        //            string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                        //            string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                        //            string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                        //            string investedCost = ((TextBox)PlaceHolder1.FindControl("txtInvestedCost" + i.ToString())).Text.ToString();
                        //            string currentValue = ((TextBox)PlaceHolder1.FindControl("txtCurrentValue" + i.ToString())).Text.ToString();
                        //            string absoluteReturn = ((TextBox)PlaceHolder1.FindControl("txtAbsoluteReturn" + i.ToString())).Text.ToString();

                        //            if (allocationPer == "")
                        //            {
                        //                insuranceUlipList[i].CIUP_AllocationPer = 0;
                        //                insuranceUlipList[i].CIUP_PurchasePrice = 0;
                        //                insuranceUlipList[i].CIUP_Unit = 0;
                        //                insuranceUlipList[i].CIUP_PurchaseDate = DateTime.Parse("1/1/1900");
                        //            }
                        //            else
                        //            {
                        //                if (allocationPer != string.Empty)
                        //                    insuranceUlipList[i].CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                        //                if (purchasePrice != string.Empty)
                        //                    insuranceUlipList[i].CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                        //                if (units != string.Empty)
                        //                    insuranceUlipList[i].CIUP_Unit = float.Parse(units.ToString());
                        //                if (purchaseDate != string.Empty)
                        //                    insuranceUlipList[i].CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());
                        //            }
                        //            //insuranceUlipList.Add(insuranceUlipVo);
                        //        }

                        //        if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                        //        {
                        //            for (int i = 0; i < insuranceUlipList.Count; i++)
                        //            {
                        //                insuranceUlipVo = new InsuranceULIPVo();
                        //                insuranceUlipVo = insuranceUlipList[i];
                        //                insuranceUlipVo.CIP_CustInsInvId = insuranceVo.CustInsInvId;
                        //                insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                        //                insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                        //                insuranceBo.UpdateInsuranceULIPPlan(insuranceUlipVo);
                        //            }

                        //            Session.Remove("table");
                        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        // Delete the old sub-plans
                        //        // --
                        //        if (insuranceBo.DeleteInsuranceUlipPlans(insuranceVo.CustInsInvId))
                        //        {
                        //            insuranceUlipList = new List<InsuranceULIPVo>();

                        //            // Add New Ones Here
                        //            // --
                        //            for (int i = 0; i < count; i++)
                        //            {
                        //                insuranceUlipVo = new InsuranceULIPVo();
                        //                string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                        //                string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                        //                string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                        //                string purchaseDate = ((TextBox)PlaceHolder1.FindControl("txtPurchaseDateId" + i.ToString())).Text.ToString();

                        //                string investedCost = ((TextBox)PlaceHolder1.FindControl("txtInvestedCost" + i.ToString())).Text.ToString();
                        //                string currentValue = ((TextBox)PlaceHolder1.FindControl("txtCurrentValue" + i.ToString())).Text.ToString();
                        //                string absoluteReturn = ((TextBox)PlaceHolder1.FindControl("txtAbsoluteReturn" + i.ToString())).Text.ToString();

                        //                insuranceUlipVo.WUP_ULIPSubPlaCode = ds.Tables[0].Rows[i][0].ToString();
                        //                insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlUlipPlans.SelectedValue.ToString());

                        //                if (allocationPer == "")
                        //                {
                        //                    insuranceUlipVo.CIUP_AllocationPer = 0;
                        //                    insuranceUlipVo.CIUP_PurchasePrice = 0;
                        //                    insuranceUlipVo.CIUP_Unit = 0;
                        //                    insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse("1/1/1900");
                        //                }
                        //                else
                        //                {
                        //                    insuranceUlipVo.CIUP_AllocationPer = float.Parse(allocationPer.ToString());
                        //                    insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                        //                    insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                        //                    insuranceUlipVo.CIUP_PurchaseDate = DateTime.Parse(purchaseDate.ToString());
                        //                }
                        //                insuranceUlipList.Add(insuranceUlipVo);
                        //            }

                        //            if (insuranceBo.UpdateInsurancePortfolio(insuranceVo, userVo.UserId))
                        //            {
                        //                for (int i = 0; i < insuranceUlipList.Count; i++)
                        //                {
                        //                    insuranceUlipVo = new InsuranceULIPVo();
                        //                    insuranceUlipVo = insuranceUlipList[i];
                        //                    insuranceUlipVo.CIP_CustInsInvId = insuranceVo.CustInsInvId;
                        //                    insuranceUlipVo.CIUP_CreatedBy = userVo.UserId;
                        //                    insuranceUlipVo.CIUP_ModifiedBy = userVo.UserId;
                        //                    insuranceBo.CreateInsuranceULIPPlan(insuranceUlipVo);
                        //                }
                        //                Session.Remove("table");
                        //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','login');", true);
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    lblError.Text = "Check the Allocation";
                        //    lblError.CssClass = "Error";
                        //    trULIPError.Visible = true;
                        //}
                        #endregion
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
                        insuranceVo.PremiumPaymentDate = Int16.Parse(ddlMPPrPayDate.SelectedItem.Text.Trim());
                        if (txtMPPremiumDuration.Text.Trim() != "")
                            insuranceVo.PremiumDuration = float.Parse(txtMPPremiumDuration.Text.Trim());
                        else
                            insuranceVo.PremiumDuration = 0.0F;
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
                                    if (paymentDate != string.Empty && paymentDate != null)
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                    else
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.MinValue;
                                    string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                    if (repaidPercent != string.Empty && repaidPercent != null)
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                    else
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse("0.00");
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
                                    if (paymentDate != string.Empty && paymentDate != null)
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                    else
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.MinValue;
                                    string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                    if (repaidPercent != string.Empty && repaidPercent != null)
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(repaidPercent);
                                    else
                                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse("0.00");
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
                                    if (paymentDate != string.Empty && paymentDate != null)
                                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(paymentDate);
                                    string repaidPercent = (((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString())).Text.ToString());
                                    if (repaidPercent != string.Empty && repaidPercent != null)
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

                if (txtMoneyBackEpisode.Text != "")
                {
                    count = int.Parse(txtMoneyBackEpisode.Text);
                    if (count != 0)
                    {
                        trMPDetails.Visible = true;
                        ShowMoneyBackContent(count);
                    }
                    else
                    {
                        pnlMoneyBackEpisode.Visible = false;
                        trMPDetails.Visible = false;
                    }
                }
                else
                {
                    trMPDetails.Visible = false;
                    pnlMoneyBackEpisode.Visible = false;
                }

                //Commented by MP (Not Required
                //**********************************************************************************************************

                // OnPostBack it should load the existing values
                //if (Session["moneyBackEpisodeList"] != null)
                //{
                //    moneyBackEpisodeList = (List<MoneyBackEpisodeVo>)Session["moneyBackEpisodeList"];

                //    // Do a Count Check Here Too
                //    if (count < moneyBackEpisodeList.Count)
                //    {// Special Case
                //        for (int i = 0; i < count; i++)
                //        {
                //            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                //            moneyBackEpisodeVo = moneyBackEpisodeList[i];

                //            TextBox txtBox1 = new TextBox();
                //            txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString()));
                //            txtBox1.Text = moneyBackEpisodeVo.CIMBE_RepaymentDate.ToShortDateString();

                //            TextBox txtBox2 = new TextBox();
                //            txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString()));
                //            txtBox2.Text = moneyBackEpisodeVo.CIMBE_RepaidPer.ToString();
                //        }
                //    }
                //    else
                //    {
                //        for (int i = 0; i < moneyBackEpisodeList.Count; i++)
                //        {
                //            moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                //            moneyBackEpisodeVo = moneyBackEpisodeList[i];

                //            TextBox txtBox1 = new TextBox();
                //            txtBox1 = ((TextBox)PlaceHolder2.FindControl("txtPaymentDate" + i.ToString()));
                //            txtBox1.Text = moneyBackEpisodeVo.CIMBE_RepaymentDate.ToShortDateString();

                //            TextBox txtBox2 = new TextBox();
                //            txtBox2 = ((TextBox)PlaceHolder2.FindControl("txtRepaidPer" + i.ToString()));
                //            txtBox2.Text = moneyBackEpisodeVo.CIMBE_RepaidPer.ToString();
                //        }
                //    }
                //}
                //***********************************************************************************************************
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
                    CalendarExtender calExtender = new CalendarExtender();
                    calExtender.ID = calExtender + i.ToString();
                    calExtender.TargetControlID = txtBox1.ID;
                    calExtender.Format = "dd/MM/yyyy";
                    calExtender.EnableViewState = true;
                    TextBoxWatermarkExtender waterMarkExtender = new TextBoxWatermarkExtender();
                    waterMarkExtender.ID = waterMarkExtender + i.ToString();
                    waterMarkExtender.WatermarkText = "dd/mm/yyyy";
                    waterMarkExtender.TargetControlID = txtBox1.ID;
                    waterMarkExtender.EnableViewState = true;
                    CompareValidator compareValidator = new CompareValidator();
                    compareValidator.ID = compareValidator + i.ToString() + i.ToString();
                    compareValidator.ControlToValidate = txtBox1.ID;
                    compareValidator.Type = ValidationDataType.Date;
                    compareValidator.ErrorMessage = "The date format should be dd/mm/yyyy";
                    compareValidator.Operator = ValidationCompareOperator.DataTypeCheck;
                    compareValidator.CssClass = "cvPCG";
                    compareValidator.Display = ValidatorDisplay.Dynamic;
                    compareValidator.EnableViewState = true;
                    tc.Controls.Add(txtBox1);
                    tc.Controls.Add(calExtender);
                    tc.Controls.Add(waterMarkExtender);
                    tc.Controls.Add(compareValidator);
                    tc.ColumnSpan = 2;
                    tr.Cells.Add(tc);

                    // Middle Empty Cells Used for Alignment
                    for (int j = 0; j < 35; j++)
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
                    CompareValidator compareValidator1 = new CompareValidator();
                    compareValidator1.ID = compareValidator1 + i.ToString() + (i + 1).ToString();
                    compareValidator1.ControlToValidate = txtBox2.ID;
                    compareValidator1.Type = ValidationDataType.Double;
                    compareValidator1.ErrorMessage = "Please enter a numeric value";
                    compareValidator1.Operator = ValidationCompareOperator.DataTypeCheck;
                    compareValidator1.CssClass = "cvPCG";
                    compareValidator1.Display = ValidatorDisplay.Dynamic;
                    compareValidator1.EnableViewState = true;
                    tc.Controls.Add(txtBox2);
                    tc.Controls.Add(compareValidator1);
                    tr.Cells.Add(tc);

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
            lblIssuarCode.Text = ddlInsuranceIssuerCode.SelectedItem.Text;
            BindAssetParticular(ddlInsuranceIssuerCode.SelectedValue);
        }

        protected void ddlUlipPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customerAccountVo.AssetCategory.Trim() == "INUP")
            {
                Session.Remove("ULIPSubPlanSchedule");
                BindRadGridULIPSubPlanSchedule(ddlAssetPerticular.SelectedValue.ToString().Trim());
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Manage = "edit";
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
            if (txtFirstPremiumDate.Text != "dd/mm/yyyy" && txtFirstPremiumDate.Text != string.Empty && txtLastPremiumDate.Text != "dd/mm/yyyy" && txtLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtEPPremiumDuration.Text = NoOfMonths.ToString("f2");
                // txtOTPremiumDuration.Text = NoOfMonths.ToString("f2");
            }
        }
        protected void txtOTLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            if (txtOTFirstPremiumDate.Text != "dd/mm/yyyy" && txtOTFirstPremiumDate.Text != string.Empty && txtOTLastPremiumDate.Text != "dd/mm/yyyy" && txtOTLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtOTFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtOTLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtOTPremiumDuration.Text = NoOfMonths.ToString("f2");
            }
        }

        protected void txtWLPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            if (txtWLPFirstPremiumDate.Text != "dd/mm/yyyy" && txtWLPFirstPremiumDate.Text != string.Empty && txtWLPLastPremiumDate.Text != "dd/mm/yyyy" && txtWLPLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtWLPFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtWLPLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtWLPPremiumDuration.Text = NoOfMonths.ToString("f2");
            }
        }

        protected void txtMPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            if (txtMPFirstPremiumDate.Text != "dd/mm/yyyy" && txtMPFirstPremiumDate.Text != string.Empty && txtMPLastPremiumDate.Text != "dd/mm/yyyy" && txtMPLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtMPFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtMPLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtMPPremiumDuration.Text = NoOfMonths.ToString("f2");
            }
        }

        protected void txtULIPFirstPremiumDate_TextChanged(object sender, EventArgs e)
        {
            if (txtULIPLastPremiumDate.Text != "dd/mm/yyyy" && txtULIPFirstPremiumDate.Text != string.Empty && txtULIPFirstPremiumDate.Text != "dd/mm/yyyy" && txtULIPLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtULIPFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtULIPLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtUlipPremiuimPeriod.Text = NoOfMonths.ToString("f2");
            }
        }

        protected void txtTPLastPremiumDate_TextChanged(object sender, EventArgs e)
        {
            if (txtTPFirstPremiumDate.Text != "dd/mm/yyyy" && txtTPFirstPremiumDate.Text != string.Empty && txtTPLastPremiumDate.Text != "dd/mm/yyyy" && txtTPLastPremiumDate.Text != string.Empty)
            {
                DateTime dtFrom = DateTime.Parse(txtTPFirstPremiumDate.Text);
                DateTime dtTo = DateTime.Parse(txtTPLastPremiumDate.Text);
                DateBo dtBo = new DateBo();

                float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                txtTPPremiumDuration.Text = NoOfMonths.ToString("f2");
            }
        }

        protected void txtPolicyMaturity_TextChanged(object sender, EventArgs e)
        {
            if (customerAccountVo.AssetCategory.Trim() == "INMP")
            {
                if (txtPolicyCommencementDate.Text != "dd/mm/yyyy" && txtPolicyCommencementDate.Text != string.Empty && txtPolicyMaturity.Text != "dd/mm/yyyy" && txtPolicyMaturity.Text != string.Empty)
                {
                    DateTime dtFrom = DateTime.Parse(txtPolicyCommencementDate.Text);
                    DateTime dtTo = DateTime.Parse(txtPolicyMaturity.Text);
                    DateBo dtBo = new DateBo();

                    float NoOfMonths = dtBo.GetDateRangeNumMonths(dtFrom, dtTo);
                    txtMPPolicyTerm.Text = NoOfMonths.ToString("f2");
                }
            }
        }

        public void LoadNominees()
        {
            DataSet dsCustomerAssociates = new DataSet();
            DataTable dtAssociates = new DataTable();
            DataTable dtCustomerAssociates = new DataTable();
            DataRow drCustomerAssociates;
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtAssociates = dsCustomerAssociates.Tables[0];

                dtCustomerAssociates.Columns.Add("MemberCustomerId");
                dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociates.Columns.Add("Name");
                dtCustomerAssociates.Columns.Add("Relationship");

                foreach (DataRow dr in dtAssociates.Rows)
                {

                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gvNominee.DataSource = dtCustomerAssociates;
                    gvNominee.DataBind();
                    gvNominee.Visible = true;

                    trNoNominee.Visible = false;
                    trNominees.Visible = true;
                }
                else
                {
                    trNoNominee.Visible = true;
                    trNominees.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnInsertNewScheme_Click(object sender, EventArgs e)
        {
            OrderBo orderbo = new OrderBo();
            if (txtAsset.Text.Trim() != "")
            {
                if (ddlInsuranceIssuerCode.SelectedIndex != 0)
                    orderbo.InsertAssetParticularScheme(txtAsset.Text.Trim(), ddlInsuranceIssuerCode.SelectedValue);
            }
            BindAssetParticular(ddlInsuranceIssuerCode.SelectedValue);
        }

        public void BindAssetParticular(string issuerCode)
        {
            DataSet ds = assetBo.GetULIPPlans(issuerCode);
            DataTable dtSchemePlan = ds.Tables[0];
            if (dtSchemePlan.Rows.Count > 0)
            {
                ddlAssetPerticular.DataSource = dtSchemePlan;
                ddlAssetPerticular.DataValueField = dtSchemePlan.Columns["IS_SchemeId"].ToString();
                ddlAssetPerticular.DataTextField = dtSchemePlan.Columns["IS_SchemeName"].ToString();
                ddlAssetPerticular.DataBind();
            }
            ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "Select"));
        }

        public void BindRadGridULIPSubPlanSchedule(string ulipPlan)
        {            
            trULIPAllocation.Visible = true;
            rgULIPSubPlanSchedule.Visible = true;
            pnlGridView.Visible = true;

            dtULIPSubPlanSchedule.Columns.Add("ISF_SchemeFundId");
            dtULIPSubPlanSchedule.Columns.Add("IF_FundName");
            dtULIPSubPlanSchedule.Columns.Add("CINPUD_InvestedCost");
            dtULIPSubPlanSchedule.Columns.Add("CINPUD_Unit");
            //dtULIPSubPlanSchedule.Columns.Add("CINPUD_PurchaseDate", typeof(DateTime));
            dtULIPSubPlanSchedule.Columns.Add("CINPUD_CurrentValue");
            dtULIPSubPlanSchedule.Columns.Add("CINPUD_AbsoluteReturn");
            dtULIPSubPlanSchedule.Columns.Add("CINPUD_AllocationPer");
            dtULIPSubPlanSchedule.Columns.Add("Flag");
            if (ddlAssetPerticular.SelectedValue != "0" && ddlAssetPerticular.SelectedValue != "")
            {
                DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ulipPlan));
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    DataRow drULIPSubPlan;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        drULIPSubPlan = dtULIPSubPlanSchedule.NewRow();
                        drULIPSubPlan["ISF_SchemeFundId"] = dr["ISF_SchemeFundId"];
                        drULIPSubPlan["IF_FundName"] = dr["IF_FundName"];
                        drULIPSubPlan["CINPUD_InvestedCost"] = dr["CINPUD_InvestedCost"];
                        drULIPSubPlan["CINPUD_Unit"] = dr["CINPUD_Unit"];
                        //drULIPSubPlan["CINPUD_PurchaseDate"] = DateTime.Now;
                        drULIPSubPlan["CINPUD_CurrentValue"] = dr["CINPUD_CurrentValue"];
                        drULIPSubPlan["CINPUD_AbsoluteReturn"] = dr["CINPUD_AbsoluteReturn"];
                        drULIPSubPlan["CINPUD_AllocationPer"] = dr["CINPUD_AllocationPer"];
                        drULIPSubPlan["Flag"] = "D";
                        dtULIPSubPlanSchedule.Rows.Add(drULIPSubPlan);
                    }
                    Session["ULIPSubPlanSchedule"] = dtULIPSubPlanSchedule;
                }
                else
                {
                    //trULIPAllocation.Visible = false;
                    //Session.Remove("ULIPSubPlanSchedule");
                    rgULIPSubPlanSchedule.DataSource = dtULIPSubPlanSchedule;
                    Session["ULIPSubPlanSchedule"] = dtULIPSubPlanSchedule;
                    rgULIPSubPlanSchedule.DataSourceID = String.Empty;
                    rgULIPSubPlanSchedule.DataBind();
                }
            }
        }

        protected void rgULIPSubPlanSchedule_ItemCommand(object source, GridCommandEventArgs e)
        {
            InsuranceBo insuranceBo = new InsuranceBo();

            trULIPAllocation.Visible = true;
            GridBoundColumn SubPlanName = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("IF_FundName") as GridBoundColumn;
            GridBoundColumn InvestedCost = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_InvestedCost") as GridBoundColumn;
            GridBoundColumn Unit = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_Unit") as GridBoundColumn;
            //GridDateTimeColumn PurchaseDate = (GridDateTimeColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_PurchaseDate") as GridDateTimeColumn;
            GridBoundColumn CurrentValue = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_CurrentValue") as GridBoundColumn;
            GridBoundColumn AbsoluteReturn = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_AbsoluteReturn") as GridBoundColumn;
            GridBoundColumn AllocationPer = (GridBoundColumn)e.Item.OwnerTableView.GetColumnSafe("CINPUD_AllocationPer") as GridBoundColumn;           

            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                e.Canceled = true;
                if (e.Item.OwnerTableView.EditMode == GridEditMode.PopUp)
                {
                    InvestedCost.ReadOnly = true;
                    Unit.ReadOnly = true;
                    //PurchaseDate.ReadOnly = true;
                    CurrentValue.ReadOnly = true;
                    AbsoluteReturn.ReadOnly = true;
                    AllocationPer.ReadOnly = true;
                }
                SubPlanName.ReadOnly = false;
                e.Item.OwnerTableView.InsertItem();
                //GridEditableItem insertedItem = rgULIPSubPlanSchedule.MasterTableView.GetInsertItem();                
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
                SubPlanName.ReadOnly = true;
                InvestedCost.ReadOnly = false;
                Unit.ReadOnly = false;
                //PurchaseDate.ReadOnly = false;
                CurrentValue.ReadOnly = false;
                AbsoluteReturn.ReadOnly = true;
                AllocationPer.ReadOnly = false;                
            }

            if (e.CommandName == RadGrid.PerformInsertCommandName) //"Add new" button clicked
            {
                try
                {
                    GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;
                    string ulipSchemeFundName = (insertedItem["IF_FundName"].Controls[0] as TextBox).Text.Trim();

                    if (ulipSchemeFundName != "")
                    {
                        if (ddlAssetPerticular.SelectedIndex != 0)
                        {
                            DataTable dt = (DataTable)Session["ULIPSubPlanSchedule"];
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["IF_FundName"] = ulipSchemeFundName;
                            dr["CINPUD_InvestedCost"] = "0";
                            dr["CINPUD_Unit"] = "0";
                            dr["CINPUD_CurrentValue"] = "0";
                            dr["CINPUD_AbsoluteReturn"] = "0";
                            dr["CINPUD_AllocationPer"] = "0";
                            dr["Flag"] = "UI";
                            dt.Rows.Add(dr);

                            Session["ULIPSubPlanSchedule"] = dt;
                            rgULIPSubPlanSchedule.DataSource = dt;
                            rgULIPSubPlanSchedule.DataBind();

                            //insuranceBo.InsertULIPInsuranceSchemeFund(ulipSchemeFundName, int.Parse(ddlAssetPerticular.SelectedValue), ddlInsuranceIssuerCode.SelectedValue, userVo.UserId);
                        }                        
                        //else if ()
                        //{
                        //    rgULIPSubPlanSchedule.Controls.Add(new LiteralControl("Allocation Percentage must be 100% "));
                        //    e.Canceled = true;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    rgULIPSubPlanSchedule.Controls.Add(new LiteralControl("Unable to insert into Sub Plan. Reason: " + ex.Message));
                    e.Canceled = true;
                }
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
            }
            else if (e.CommandName == RadGrid.CancelCommandName)
            {
            }
        }

        protected void rgULIPSubPlanSchedule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Session["ULIPSubPlanSchedule"] != null)
            this.rgULIPSubPlanSchedule.DataSource = Session["ULIPSubPlanSchedule"];
        }

        protected void rgULIPSubPlanSchedule_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;             
            
            //InsuranceULIPVo insuranceULIPvo = new InsuranceULIPVo();
            //insuranceULIPvo.CIUP_ULIPPlanId = Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["ISF_SchemeFundId"].ToString());
                //int.Parse(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ISF_SchemeFundId"].ToString());

            
            double investedCost = double.Parse((editedItem["CINPUD_InvestedCost"].Controls[0] as TextBox).Text.Trim());
            double currentValue = double.Parse((editedItem["CINPUD_CurrentValue"].Controls[0] as TextBox).Text.Trim());
            double absoluteReturn = ((currentValue - investedCost) / investedCost)*100;
            double allocationPer = Convert.ToDouble((editedItem["CINPUD_AllocationPer"].Controls[0] as TextBox).Text.Trim());
            double unit = Convert.ToDouble((editedItem["CINPUD_Unit"].Controls[0] as TextBox).Text.Trim());

            //insuranceULIPvo.CIUP_InvestedCost = investedCost;
            //insuranceULIPvo.CIUP_CurrentValue = currentValue;
            //insuranceULIPvo.CIUP_AbsoluteReturn = absoluteReturn;
            //insuranceULIPvo.CIUP_AllocationPer = float.Parse((editedItem["CINPUD_AllocationPer"].Controls[0] as TextBox).Text.Trim());
            //insuranceULIPvo.CIUP_Unit = float.Parse((editedItem["CINPUD_Unit"].Controls[0] as TextBox).Text.Trim());

            DataTable dtUpdate = (DataTable)Session["ULIPSubPlanSchedule"];
            DataRow drUpdate = dtUpdate.Rows[e.Item.ItemIndex];
            drUpdate["CINPUD_InvestedCost"] = investedCost;
            drUpdate["CINPUD_Unit"] = unit;
            drUpdate["CINPUD_CurrentValue"] = currentValue;
            drUpdate["CINPUD_AbsoluteReturn"] = absoluteReturn;
            drUpdate["CINPUD_AllocationPer"] = allocationPer;
            drUpdate["Flag"] = "UI";
            dtUpdate.AcceptChanges();

            Session["ULIPSubPlanSchedule"] = dtUpdate;
            rgULIPSubPlanSchedule.DataSource = dtUpdate;
            rgULIPSubPlanSchedule.DataBind();

            //UpdateULIPSchemeFund = insuranceBo.UpdateULIPInsuranceSchemeFund(insuranceULIPvo);

            //    rgULIPSubPlanSchedule.Controls.Add(new LiteralControl("<strong>Unable to update value</strong>" ));
            //    e.Canceled = true; 
            
            //rgULIPSubPlanSchedule.MasterTableView.ClearEditItems();
            //Session["SubmitAllocationSchedule"] = dt;

            //try
            //{
            //    DataTable dt4 = new DataTable();
            //    dt4 = (DataTable)Session["ULIPSubPlanSchedule"];
            //    int subPlanCode = Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["ISF_SchemeFundId"].ToString());                           
            //    DataRow[] changedRows = dt.Select("ISF_SchemeFundId = " + subPlanCode + "");
            //    changedRows[0][column.UniqueName] = editorValue;
            //    dt.AcceptChanges();
            //    rgULIPSubPlanSchedule.DataSource = dt;
            //    Session["SubmitAllocationSchedule"] = dt;
            //}
            //catch (Exception ex)
            //{
            //    rgULIPSubPlanSchedule.Controls.Add(new LiteralControl("<strong>Unable to set value of column '" + column.UniqueName + "'</strong> - " + ex.Message));
            //    e.Canceled = true;
            //}            
        }

        protected void txtPolicyTerms_TextChanged(object sender, EventArgs e)
        {
            txtPolicyMaturity.Text = CalcEndDate(int.Parse(txtPolicyTerms.Text.ToString()), DateTime.Parse(txtPolicyCommencementDate.Text.ToString())).ToShortDateString();
            txtPolicyMaturity.Enabled = false;
        }

        protected void txtPolicyCommencementDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPolicyTerms.Text.ToString().Trim()))
            {
                txtPolicyMaturity.Text = CalcEndDate(int.Parse(txtPolicyTerms.Text.ToString()), DateTime.Parse(txtPolicyCommencementDate.Text.ToString())).ToShortDateString();
                txtPolicyMaturity.Enabled = false;
            }
        }

        private DateTime CalcEndDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();
            if (ddlPeriodSelection.SelectedItem.Value == "DA")
            {
                endDate = startDate.AddDays(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "MN")
            {
                endDate = startDate.AddMonths(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "YR")
            {
                endDate = startDate.AddYears(period);
            }
            return endDate;
        }

        protected void ddlPeriodSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPolicyMaturity.Text = CalcEndDate(int.Parse(txtPolicyTerms.Text.ToString()), DateTime.Parse(txtPolicyCommencementDate.Text.ToString())).ToShortDateString();
            txtPolicyMaturity.Enabled = false;
        }
    }
}
