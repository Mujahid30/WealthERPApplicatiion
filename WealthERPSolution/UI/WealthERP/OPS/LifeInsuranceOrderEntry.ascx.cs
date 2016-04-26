using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using System.Configuration;
using VoOps;
using VOAssociates;

namespace WealthERP.OPS
{
    public partial class LifeInsuranceOrderEntry : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        OrderBo orderbo = new OrderBo();
        AdvisorVo advisorVo;
        OrderVo ordervo;
        UserVo userVo;
        LifeInsuranceOrderVo lifeInsuranceOrdervo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo = new OperationVo();
        AssetBo assetBo = new AssetBo();
       
        BoCustomerPortfolio.BoDematAccount bodemataccount = new BoDematAccount();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        int amcCode;
        int accountId;
        string categoryCode;
        int portfolioId;
        int schemePlanCode;
        int customerId;
        string path;
        int flag = 0;
        int Sflag = 0;
        int Fflag = 0;
        int orderNumber = 0;
        string ViewForm = string.Empty;
        int orderId;

        string group = "IN";
        DataTable dtBankName = new DataTable();

        string orderStatusCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            userVo = (UserVo)Session["userVo"];

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;

            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];

            if (!IsPostBack)
            {
                pnlOrderSteps.Visible = false;
                btnUpdate.Visible = false;
                lnkBtnEdit.Visible = false;
                lnkBack.Visible = false;
                lnkbtnDelete.Visible = false;
                SetControls(true);

                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                   
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    txtAssociateSearch.Text = associateuserheirarchyVo.AgentCode;
                    GetAgentName(associateuserheirarchyVo.AdviserAgentId);
                    txtAgentId.Value = associateuserheirarchyVo.AdviserAgentId.ToString();
                    AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode + "/" + advisorVo.advisorId.ToString() + "/" + associateuserheirarchyVo.IsBranchOps;
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";


                }
                if (!string.IsNullOrEmpty(Request.QueryString["strOrderId"]))
                {
                    customerId = Convert.ToInt32(Request.QueryString["strCustomerId"]);
                    orderId = Convert.ToInt32(Request.QueryString["strOrderId"]);
                    string action = Request.QueryString["strAction"].Trim();

                    lifeInsuranceOrdervo = orderbo.GetLifeInsuranceOrderDetails(orderId);

                    customerVo = customerBo.GetCustomer(customerId);
                    hdnCustomerId.Value = customerVo.CustomerId.ToString();
                    txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                    lblGetBranch.Text = customerVo.BranchName;
                    lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;

                    if (action == "Edit")
                    {
                        SetControls(true);
                        btnUpdate.Enabled = true;
                        btnUpdate.Visible = true;
                        btnSubmit.Visible = false;
                        lnkBtnEdit.Visible = false;
                        lnkbtnDelete.Visible = true;
                    }
                    else if (action == "View")
                    {
                        SetControls(false);
                        lnkBtnEdit.Visible = true;
                        lnkBack.Visible = true;
                        lnkbtnDelete.Visible = true;
                        btnUpdate.Enabled = false;
                        btnSubmit.Visible = false;
                    }
                    pnlOrderSteps.Visible = true;
                    SetValuesToControls(lifeInsuranceOrdervo);
                }
                BindAssetParticular();
                BindFrequencyDropdown();
                BindPaymentMode();
                BindOrderStepsGrid();
            }
        }
        protected void txtAgentId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgentId.Value.ToString().Trim()))
            {
                GetAgentName(int.Parse(txtAgentId.Value));
            }
            txtAssociateSearch.Focus();
        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(txtCustomerId.Value);
                LoadCustomerDetails(customerId);
                imgBtnAddBank.Visible=true;
                imgBtnRefereshBank.Visible= true;
            }
        }

        public void LoadCustomerDetails(int customerId)
        {
            customerVo = customerBo.GetCustomer(customerId);
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            lblGetRM.Text = customerVo.RMName;
            lblgetPan.Text = customerVo.PANNum;
            if (txtCustomerId.Value != null && txtCustomerId.Value != "")
                hdnCustomerId.Value = txtCustomerId.Value;
            txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
            //customerId = int.Parse(txtCustomerId.Value);
            BindBankAccountDetails();
            //BindOrderStatus();
            if (txtCustomerId.Value != "")
                BindNomineeGrid();

            lblPickNominee.Visible = true;
            gvPickNominee.Visible = true;
        }

        public void LoadCategory(string productType)
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory(productType); //Change to the respective GroupCode
                ddlInstrumentCategory.DataSource = ds.Tables[0];
                ddlInstrumentCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlInstrumentCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlInstrumentCategory.DataBind();
                ddlInstrumentCategory.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadCategory()");
                object[] objects = new object[1];
                objects[0] = group;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadInsuranceIssuerCode()
        {
            try
            {
                DataTable dt = XMLBo.GetInsuranceIssuer(path);
                ddlPolicyIssuer.DataSource = dt;
                ddlPolicyIssuer.DataTextField = dt.Columns["InsuranceIsserName"].ToString();
                ddlPolicyIssuer.DataValueField = dt.Columns["InsuranceIssuerCode"].ToString();
                ddlPolicyIssuer.DataBind();
                ddlPolicyIssuer.Items.Insert(0, new ListItem("Select", "Select"));
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

        public void BindBankAccountDetails()
        {
            DataTable dt = orderbo.GetBankAccountDetails(customerVo.CustomerId);
            ddlBankName.DataSource = dt;
            ddlBankName.DataTextField = dt.Columns["WERPBM_BankName"].ToString();
            ddlBankName.DataValueField = dt.Columns["CB_CustBankAccId"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBankName.SelectedValue.ToString() != "Select")
            {
                DataTable dt = orderbo.GetBankBranch(int.Parse(ddlBankName.SelectedValue.ToString()));
                lblBranchName.Text = dt.Rows[0][1].ToString();
            }
            else
            {
                lblBranchName.Text = "";
            }
        }

        protected void imgBtnRefereshBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBankAccountDetails();
        }

        private void BindFrequencyDropdown()
        {
            DataTable dt = XMLBo.GetFrequency(path);
            ddlEPPremiumFrequencyCode.DataSource = dt;
            ddlEPPremiumFrequencyCode.DataTextField = dt.Columns["Frequency"].ToString();
            ddlEPPremiumFrequencyCode.DataValueField = dt.Columns["FrequencyCode"].ToString();
            ddlEPPremiumFrequencyCode.DataBind();
            ddlEPPremiumFrequencyCode.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void BindPaymentMode()
        {
            DataTable dtPaymentMode;
            dtPaymentMode = orderbo.GetPaymentMode();
            if (dtPaymentMode.Rows.Count > 0)
            {
                ddlPaymentMode.DataSource = dtPaymentMode;
                ddlPaymentMode.DataValueField = dtPaymentMode.Columns["XPM_PaymentModeCode"].ToString();
                ddlPaymentMode.DataTextField = dtPaymentMode.Columns["XPM_PaymentMode"].ToString();
                ddlPaymentMode.DataBind();
            }
            ddlPaymentMode.Items.Insert(0, new ListItem("Select", "Select"));
        }

        public void BindNomineeGrid()
        {
            DataSet dsCustomerAssociation = new DataSet();
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customerVo.CustomerId);
            //Populating  Pick nominee
            if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
            {
                lblPickNominee.Text = "You do not have any associates";
            }
            else
            {
                lblPickNominee.Text = "Pick Nominee:";
                gvPickNominee.DataSource = dsCustomerAssociation.Tables[0];
                gvPickNominee.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAgentId.Value.ToString().Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select Agent Name.');", true);
                btnSubmit.Enabled = true;

                return;
            }
            bool bresult = false;
            string nomineeAssociationIds = "";

            lifeInsuranceOrdervo = new LifeInsuranceOrderVo();

            if (hdnCustomerId.Value != null && hdnCustomerId.Value != "")
                lifeInsuranceOrdervo.CustomerId = int.Parse(hdnCustomerId.Value);
            lifeInsuranceOrdervo.ApplicationNumber = txtApplicationNo.Text;
            if (txtApplicationDate.SelectedDate != null)
                lifeInsuranceOrdervo.ApplicationReceivedDate = txtApplicationDate.SelectedDate.Value;

            lifeInsuranceOrdervo.AssetGroup = ddlProductType.SelectedValue;
            if (ddlInstrumentCategory.SelectedValue != null && ddlInstrumentCategory.SelectedValue != string.Empty)
                lifeInsuranceOrdervo.AssetCategory = ddlInstrumentCategory.SelectedValue;

            lifeInsuranceOrdervo.ChequeNumber = txtPaymentInstrNo.Text;
            lifeInsuranceOrdervo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            lifeInsuranceOrdervo.FrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue;
            lifeInsuranceOrdervo.GIIssuerCode = "0";            
            
            lifeInsuranceOrdervo.IsJointlyHeld = 0;
            lifeInsuranceOrdervo.InsuranceIssuerCode = ddlPolicyIssuer.SelectedValue;
            if (ddlAssetPerticular.SelectedValue != null && ddlAssetPerticular.SelectedValue != "Select")
                lifeInsuranceOrdervo.InsuranceSchemeId = Convert.ToInt32(ddlAssetPerticular.SelectedValue);
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";

            if (hdnCustomerId.Value != null && hdnCustomerId.Value != "")
                lifeInsuranceOrdervo.ApprovedBy = int.Parse(hdnCustomerId.Value);
            
            if (txtMaturityDate.SelectedDate != null)
                lifeInsuranceOrdervo.MaturityDate = txtMaturityDate.SelectedDate.Value;
            if (txtOrderDate.SelectedDate != null)
                lifeInsuranceOrdervo.OrderDate = txtOrderDate.SelectedDate.Value;
            
            if (txtPaymentInstruDate.SelectedDate != null)
                lifeInsuranceOrdervo.PaymentDate = txtPaymentInstruDate.SelectedDate.Value;
            lifeInsuranceOrdervo.PaymentMode = ddlPaymentMode.SelectedValue;
            lifeInsuranceOrdervo.SourceCode = "ME";
            lifeInsuranceOrdervo.SumAssured = double.Parse(txtSumAssured.Text);
            lifeInsuranceOrdervo.AgentCode = txtAssociateSearch.Text;
            lifeInsuranceOrdervo.AgentId = int.Parse(txtAgentId.Value);
            
            if (this.gvPickNominee.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPickNominee.Rows)
                {
                    if (((CheckBox)gvr.FindControl("PNCheckBox")).Checked == true)
                    {
                        nomineeAssociationIds += gvPickNominee.DataKeys[gvr.RowIndex].Value + "~";
                        //gvPickNominee.DataKeys[gvr.RowIndex].Values[1].ToString() + "~";
                    }
                }
            }

            bresult = orderbo.AddLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, out orderId, userVo.UserId);
            if (bresult == true)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderMIS','?result=" + result + "');", true);
                pnlOrderSteps.Visible = true;
                rgvOrderSteps.Visible = true;
                SetEnableDisableControls("Edit");
                btnSubmit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);
                BindOrderStepsGrid();

            }
            else
            {
                pnlOrderSteps.Visible = false;
            }
        }

        public void SetEnableDisableControls(string action)
        {
            if (action == "Entry")
            {
                SetControls(true);
            }
            else if (action == "Edit")
            {
                SetControls(false);
            }
        }

        public void SetControls(bool bentry)
        {
            txtCustomerName.Enabled = bentry;
            btnImgAddCustomer.Enabled = bentry;
            ddlInstrumentCategory.Enabled = bentry;
            ddlPolicyIssuer.Enabled = bentry;
            ddlAssetPerticular.Enabled = bentry;
            imgBtnOpenPopup.Enabled = bentry;  
            gvPickNominee.Enabled = bentry;

            ddlPaymentMode.Enabled = bentry;
            txtPaymentInstruDate.Enabled = bentry;
            ddlBankName.Enabled = bentry;
            ddlProductType.Enabled = bentry;
            ddlAssetSubCategory.Enabled = bentry;
            imgBtnAddBank.Enabled = bentry;
            txtPaymentInstrNo.Enabled = bentry;

            txtApplicationNo.Enabled = bentry;
            txtApplicationDate.Enabled = bentry;
            txtMaturityDate.Enabled = bentry;
            txtOrderDate.Enabled = bentry;
            txtSumAssured.Enabled = bentry;
            ddlEPPremiumFrequencyCode.Enabled = bentry;
        }

        public void SetEditableControls(bool editable)
        {
            ddlPaymentMode.Enabled = editable;
            txtPaymentInstruDate.Enabled = editable;
            ddlBankName.Enabled = editable;
            imgBtnAddBank.Enabled = editable;
            txtPaymentInstrNo.Enabled = editable;

            txtApplicationNo.Enabled = editable;
            txtApplicationDate.Enabled = editable;
            txtMaturityDate.Enabled = editable;
            txtOrderDate.Enabled = editable;
            txtSumAssured.Enabled = editable;
            ddlEPPremiumFrequencyCode.Enabled = editable;
        }

        public void bindNomineeGridInViewMode()
        {
            DataTable dtCustomerAssociation = new DataTable();
            //customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            dtCustomerAssociation = orderbo.GetCustomerOrderAssociates(orderId);
            //Populating  Pick nominee
            if (dtCustomerAssociation.Rows.Count == 0)
            {
                lblPickNominee.Text = "You did not assign Nominee";
            }
            else
            {
                lblPickNominee.Text = "Associated Nominee:";
                gvPickNominee.DataSource = dtCustomerAssociation;
                gvPickNominee.DataBind();
            }
        }

        public void SetValuesToControls(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        {
            hdnCustomerId.Value = lifeInsuranceOrdervo.CustomerId.ToString();
            LoadCustomerDetails(int.Parse(hdnCustomerId.Value));
            txtApplicationNo.Text = lifeInsuranceOrdervo.ApplicationNumber;
            txtApplicationDate.SelectedDate = lifeInsuranceOrdervo.ApplicationReceivedDate;

            if (lifeInsuranceOrdervo.AssetCategory != null && lifeInsuranceOrdervo.AssetCategory != string.Empty)
                ddlInstrumentCategory.SelectedValue = lifeInsuranceOrdervo.AssetCategory;

            txtPaymentInstrNo.Text = lifeInsuranceOrdervo.ChequeNumber;
            ddlBankName.SelectedValue = lifeInsuranceOrdervo.CustBankAccId.ToString();
            lblBranchName.Text = lifeInsuranceOrdervo.BankBranchName;
            ddlEPPremiumFrequencyCode.SelectedValue = lifeInsuranceOrdervo.FrequencyCode;

            bindNomineeGridInViewMode();

            ddlPolicyIssuer.SelectedValue = lifeInsuranceOrdervo.InsuranceIssuerCode;
            if (lifeInsuranceOrdervo.InsuranceSchemeId != null)
                ddlAssetPerticular.SelectedValue = lifeInsuranceOrdervo.InsuranceSchemeId.ToString();
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";

            txtMaturityDate.SelectedDate = lifeInsuranceOrdervo.MaturityDate;
            txtOrderDate.SelectedDate = lifeInsuranceOrdervo.OrderDate;            
            txtPaymentInstruDate.SelectedDate = lifeInsuranceOrdervo.PaymentDate;
            ddlPaymentMode.SelectedValue = lifeInsuranceOrdervo.PaymentMode;
            lifeInsuranceOrdervo.SourceCode = "ME";
            txtSumAssured.Text = lifeInsuranceOrdervo.SumAssured.ToString();            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (txtAsset.Text.Trim() != "" && ddlPolicyIssuer.SelectedIndex != 0 && ddlInstrumentCategory.SelectedIndex != 0)
            {
                orderbo.InsertAssetParticularScheme(txtAsset.Text.Trim(), ddlPolicyIssuer.SelectedValue, ddlProductType.SelectedValue == "LI"?ddlInstrumentCategory.SelectedValue: ddlAssetSubCategory.SelectedValue);
                if (ddlProductType.SelectedValue == "LI")
                { BindAssetParticular(); }
                else
                {
                    BindGIAssetParticular(ddlPolicyIssuer.SelectedValue);
                }
            }
            radwindowPopup.VisibleOnPageLoad = false;
        }

        public void BindAssetParticular()
        {
            DataTable dtAssetParticular;
            dtAssetParticular = orderbo.GetAssetParticular(ddlPolicyIssuer.SelectedValue);
            if (dtAssetParticular.Rows.Count > 0)
            {
                ddlAssetPerticular.DataSource = dtAssetParticular;
                ddlAssetPerticular.DataValueField = dtAssetParticular.Columns["IS_SchemeId"].ToString();
                ddlAssetPerticular.DataTextField = dtAssetParticular.Columns["IS_SchemeName"].ToString();
                ddlAssetPerticular.DataBind();
            }
            ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlPolicyIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProductType.SelectedValue == "LI")
            {
                lblIssuerCode.Text = ddlPolicyIssuer.SelectedItem.Text;
                BindAssetParticular();
            }
            else
            {
                BindGIAssetParticular(ddlPolicyIssuer.SelectedValue);
            }
        }
                
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool bresult = false;
            string nomineeAssociationIds = "";

            lifeInsuranceOrdervo = new LifeInsuranceOrderVo();
            //if (!string.IsNullOrEmpty(hdnCustomerId.Value))
            if (!string.IsNullOrEmpty(Request.QueryString["strOrderId"]))           
                lifeInsuranceOrdervo.OrderId = Convert.ToInt32(Request.QueryString["strOrderId"]);            
            lifeInsuranceOrdervo.CustomerId = int.Parse(hdnCustomerId.Value);
            lifeInsuranceOrdervo.ApplicationNumber = txtApplicationNo.Text;
            if (txtApplicationDate.SelectedDate != null)
                lifeInsuranceOrdervo.ApplicationReceivedDate = txtApplicationDate.SelectedDate.Value;

            lifeInsuranceOrdervo.AssetGroup = "IN";
            if (ddlInstrumentCategory.SelectedValue != null && ddlInstrumentCategory.SelectedValue != string.Empty)
                lifeInsuranceOrdervo.AssetCategory = ddlInstrumentCategory.SelectedValue;

            lifeInsuranceOrdervo.ChequeNumber = txtPaymentInstrNo.Text;
            lifeInsuranceOrdervo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            lifeInsuranceOrdervo.FrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue;
            lifeInsuranceOrdervo.GIIssuerCode = "0";            

            lifeInsuranceOrdervo.InsuranceIssuerCode = ddlPolicyIssuer.SelectedValue;
            if (ddlAssetPerticular.SelectedValue != null && ddlAssetPerticular.SelectedValue != string.Empty)
                lifeInsuranceOrdervo.InsuranceSchemeId = Convert.ToInt32(ddlAssetPerticular.SelectedValue);
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";
            
            lifeInsuranceOrdervo.ApprovedBy = int.Parse(hdnCustomerId.Value);           
            if (txtMaturityDate.SelectedDate != null)
                lifeInsuranceOrdervo.MaturityDate = txtMaturityDate.SelectedDate.Value;
            if (txtOrderDate.SelectedDate != null)
                lifeInsuranceOrdervo.OrderDate = txtOrderDate.SelectedDate.Value;
            if (txtPaymentInstruDate.SelectedDate != null)
                lifeInsuranceOrdervo.PaymentDate = txtPaymentInstruDate.SelectedDate.Value;
            lifeInsuranceOrdervo.PaymentMode = ddlPaymentMode.SelectedValue;
            lifeInsuranceOrdervo.SourceCode = "ME";
            lifeInsuranceOrdervo.SumAssured = double.Parse(txtSumAssured.Text);
                        
            if (this.gvPickNominee.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPickNominee.Rows)
                {
                    if (((CheckBox)gvr.FindControl("PNCheckBox")).Checked == true)
                    {
                        nomineeAssociationIds += gvPickNominee.DataKeys[gvr.RowIndex].Value + "~";
                    }
                }
            }

            bresult = orderbo.UpdateLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds);
            if (bresult == true)
            {
                pnlOrderSteps.Visible = true;
                BindOrderStepsGrid();
            }
            else
            {
                pnlOrderSteps.Visible = false;
            }
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            bool editable = true;
            SetEditableControls(editable);
            //BindNomineeGrid();
            btnUpdate.Visible = true;
            btnUpdate.Enabled = true;
            btnSubmit.Visible = false;
            lnkBtnEdit.Visible = false;
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["strOrderId"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','none');", true);
            }
        }
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                bool bresult = false;
                bresult = orderbo.DeleteLifeInsuranceOrder(orderId);
                if(bresult == true)
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LifeInsuranceOrderEntry','none');", true);
            }
        }

        public void BindOrderStepsGrid()
        {
            DataSet dsOrderSteps = new DataSet();
            DataTable dtOrderDetails;
            //SetControls(false);
            dsOrderSteps = orderbo.GetOrderStepsDetails(orderId);
            dtOrderDetails = dsOrderSteps.Tables[0];
            lbOrderBook.Visible = true;
            if (dtOrderDetails.Rows.Count == 0)
            {
                //lblPickNominee.Text = "You have not placed any Order";
            }
            else
            {
                //lblPickNominee.Text = "Order Steps";
                rgvOrderSteps.DataSource = dtOrderDetails;
                rgvOrderSteps.DataBind();
                Session["OrderDetails"] = dtOrderDetails;
            }
        }

        protected void rgvOrderSteps_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                DataTable dt = (DataTable)Session["OrderDetails"];

                GridEditableItem editItem = e.Item as GridEditableItem;
                RadComboBox comboOrderStatus = (RadComboBox)editItem.FindControl("ddlCustomerOrderStatus");
                RadComboBox comboOrderStatusReason = (RadComboBox)editItem.FindControl("ddlCustomerOrderStatusReason");
                string orderstepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();

                comboOrderStatus.DataSource = orderbo.GetCustomerOrderStepStatus(orderstepCode);
                comboOrderStatus.DataTextField = "XS_Status";
                comboOrderStatus.DataValueField = "XS_StatusCode";

                comboOrderStatusReason.DataSource = orderbo.GetCustomerOrderStepStatusRejectReason(orderstepCode);
                comboOrderStatusReason.DataTextField = "XSR_StatusReason";
                comboOrderStatusReason.DataValueField = "XSR_StatusReasonCode";
            }
        }

        protected void BindRadComboBoxStatus(RadComboBox rcStatus, string orderCode, int orderid)
        {
            DataTable dtOrderStatus = orderbo.GetOrderStatus(orderCode, orderid);
            if (dtOrderStatus.Rows.Count > 0)
            {
                rcStatus.DataSource = dtOrderStatus;
                rcStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                rcStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                rcStatus.DataBind();
                rcStatus.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select Status", "0"));
            }
        }

        protected void rgvOrderSteps_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DataTable dt = (DataTable)Session["OrderDetails"];
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                //((Literal)dataItem["DropDownColumnStatus"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();
                //((Literal)dataItem["DropDownColumnStatusReason"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();

                LinkButton editButton = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
                string editColumn = dataItem["COS_IsEditable"].Text;
                if (editColumn == "1")
                {
                    editButton.Enabled = true;
                }
                else
                {
                    editButton.Enabled = false;
                }
            }
        }

        protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgvOrderSteps.DataSource = (DataTable)Session["OrderDetails"];
        }

        protected void rgvOrderSteps_ItemCommand(object source, GridCommandEventArgs e)
        {
            bool bResult = false;
            if (e.CommandName == "Update")
            {
                GridEditableItem edititem = e.Item as GridEditableItem;
                GridEditFormItem editform = (GridEditFormItem)e.Item;

                RadComboBox rcStatus = edititem.FindControl("ddlCustomerOrderStatus") as RadComboBox;
                RadComboBox rcPendingReason = edititem.FindControl("ddlCustomerOrderStatusReason") as RadComboBox;

                DataTable dt = new DataTable();
                dt = (DataTable)Session["OrderDetails"];

                string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
                orderId = int.Parse(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
                string updatedStatus = rcStatus.SelectedValue;
                string updatedReason = rcPendingReason.SelectedValue;

                bResult = orderbo.UpdateOrderStep(updatedStatus, updatedReason, orderId, orderStepCode);
                if (bResult == true)
                {
                    rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Successfully Updated</strong>"));
                }
                else
                {
                    rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Unable to update value</strong>"));
                    e.Canceled = true;
                }
                BindOrderStepsGrid();
            }
        }

        protected void rcStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcStatus = (RadComboBox)o;
            GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
            RadComboBox rcPendingReason = editedItem.FindControl("ddlCustomerOrderStatus") as RadComboBox;
            RadComboBox ddlCustomerOrderStatusReason = editedItem.FindControl("ddlCustomerOrderStatusReason") as RadComboBox;
            string statusOrderCode = rcPendingReason.SelectedValue;
            BindRadComboBoxPendingReason(ddlCustomerOrderStatusReason, statusOrderCode);
        }

        protected void BindRadComboBoxPendingReason(RadComboBox rcPendingReason, string statusOrderCode)
        {
            DataTable dtReason = orderbo.GetOrderStatusPendingReason(statusOrderCode);
            if (dtReason.Rows.Count > 0)
            {
                rcPendingReason.DataSource = dtReason;
                rcPendingReason.DataValueField = dtReason.Columns["XSR_StatusReasonCode"].ToString();
                rcPendingReason.DataTextField = dtReason.Columns["XSR_StatusReason"].ToString();
                rcPendingReason.DataBind();
                if (rcPendingReason.SelectedIndex == 0)
                {
                    rcPendingReason.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select Reason", "0"));
                }
            }
        }

        protected void rcbPendingReason_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcStatus = (RadComboBox)o;
            GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
            RadComboBox rcPendingReason = editedItem.FindControl("rcbPendingReason") as RadComboBox;
            string statusOrderCode = rcStatus.SelectedValue;
            BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
        }

        protected void btnOpenPopup_Click(object sender, ImageClickEventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }
        protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(ddlProductType.SelectedValue == "LI" ? "IN" : ddlProductType.SelectedValue);
            
            if (ddlProductType.SelectedValue == "GI")
            {
                tdddlAssetSubCategory.Visible = true;
                tdlblAssetSubCategory.Visible = true;
                BindGIPolicyIssuerDropDown();
            }
            else
            {
                tdddlAssetSubCategory.Visible = false;
                tdlblAssetSubCategory.Visible = false;
                LoadInsuranceIssuerCode();
            }
        }
        protected void ddlInstrumentCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubCategory("GI", ddlInstrumentCategory.SelectedValue);
        }
        private void loadSubCategory(string productType, string InsturmentCategory)
        {
            try
            {
                //if (ddlAssetCategory.SelectedIndex != 0)
                //{
                DataSet ds = assetBo.GetAssetInstrumentSubCategory(productType, InsturmentCategory);
                ddlAssetSubCategory.DataSource = ds;
                ddlAssetSubCategory.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlAssetSubCategory.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlAssetSubCategory.DataBind();
                ddlAssetSubCategory.Items.Insert(0, new ListItem("Select Asset Sub-Category", "Select Asset Sub-Category"));
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LifeInsuranceOrderEntry.ascx.cs:ddlInstrumentCategory_OnSelectedIndexChanged()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void BindGIPolicyIssuerDropDown()
        {
            try
            {
                 InsuranceBo  InsuranceBo = new  InsuranceBo();
                DataSet ds = InsuranceBo.GetGIIssuerList();
                if (ds != null)
                {
                    ddlPolicyIssuer.DataSource = ds;
                    ddlPolicyIssuer.DataValueField = ds.Tables[0].Columns["XGII_GIIssuerCode"].ToString();
                    ddlPolicyIssuer.DataTextField = ds.Tables[0].Columns["XGII_GeneralinsuranceCompany"].ToString();
                    ddlPolicyIssuer.DataBind();
                }
                ddlPolicyIssuer.Items.Insert(0, new ListItem("Select", "Select"));

                //ddlPolicyIssuer.Attributes.Add("onChange", "");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LifeInsuranceOrderEntry.ascx.cs:BindPolicyIssuerDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void BindGIAssetParticular(string issuerCode)
        {
            AssetBo assetBo = new AssetBo();
            DataSet ds = assetBo.GetGIPlans(issuerCode);
            DataTable dtSchemePlan = ds.Tables[0];
            ddlAssetPerticular.Items.Clear();
            if (dtSchemePlan.Rows.Count > 0)
            {
                ddlAssetPerticular.DataSource = dtSchemePlan;
                ddlAssetPerticular.DataValueField = dtSchemePlan.Columns["PGISP_SchemePlanCode"].ToString();
                ddlAssetPerticular.DataTextField = dtSchemePlan.Columns["PGISP_SchemePlanName"].ToString();
                ddlAssetPerticular.DataBind();
                ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {

                ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "Select"));
            }

        }
        protected void lbOrderBook_OnClick(object sender, EventArgs e)
        {
            Session["UserType"] = "adviser";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ProductOrderDetailsMF','login');", true);
        }
        private void GetAgentName(int agentId)
        {
            // Admin after selecting agent code and sales login default
            DataTable Agentname;
            if (agentId == 0)
            {
                return;
            }
            lblAssociate.Visible = true;
            lblAssociateReport.Visible = true;
            Agentname = customerBo.GetSubBrokerName(agentId);
            if (Agentname.Rows.Count > 0)
            {
                lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                lblAssociateReportTo.Text = Agentname.Rows[0][4].ToString();
                
            }

        }
    }
}
