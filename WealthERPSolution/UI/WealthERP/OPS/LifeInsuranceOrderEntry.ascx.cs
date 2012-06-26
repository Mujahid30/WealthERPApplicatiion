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

            if (!IsPostBack)
            {
                pnlOrderSteps.Visible = false;
                btnUpdate.Visible = false;
                lnkBtnEdit.Visible = false;
                lnlBack.Visible = false;

                if (!string.IsNullOrEmpty(Request.QueryString["strCustomerId"]))
                {
                    customerId = Convert.ToInt32(Request.QueryString["CustomerId"]);
                    customerVo = customerBo.GetCustomer(customerId);
                    hdnCustomerId.Value = customerVo.CustomerId.ToString();
                    txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                    lblGetBranch.Text = customerVo.BranchName;
                    lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;
                }

                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }

                if (!string.IsNullOrEmpty(Request.QueryString["strOrderId"]))
                {
                    customerId = Convert.ToInt32(Request.QueryString["strCustomerId"]);
                    orderId = Convert.ToInt32(Request.QueryString["strOrderId"]);
                    lifeInsuranceOrdervo = orderbo.GetLifeInsuranceOrderDetails(orderId);
                    SetControls(false);
                    lnkBtnEdit.Visible = true;
                    lnlBack.Visible = true;
                    btnUpdate.Enabled = true;
                    btnSubmit.Visible = false;
                    pnlOrderSteps.Visible = true;
                    SetValuesToControls(lifeInsuranceOrdervo);
                }

                if (rbtnNo.Checked == true)
                {
                    ddlModeOfHolding.SelectedIndex = 8;
                    gvPickNominee.Visible = false;
                    lblPickJointHolder.Visible = false;
                    lblPickNominee.Visible = false;
                }
            }

            LoadCategory();
            bindModeOfHolding();
            LoadInsuranceIssuerCode();
            BindFrequencyDropdown();
            BindPaymentMode();
            BindOrderStepsGrid();
        }
        
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(txtCustomerId.Value);
                LoadCustomerDetails(customerId);
            }
        }

        public void LoadCustomerDetails(int customerId)
        {
            customerVo = customerBo.GetCustomer(customerId);
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            lblGetRM.Text = customerVo.RMName;
            lblgetPan.Text = customerVo.PANNum;
            //hdnCustomerId.Value = txtCustomerId.Value;
            txtCustomerName.Text = customerVo.FirstName+customerVo.MiddleName+customerVo.LastName;
            //customerId = int.Parse(txtCustomerId.Value);
            BindBankAccountDetails();
            BindOrderStatus();
            if (txtCustomerId.Value != "")
            BindNomineeGrid();

            lblPickNominee.Visible = true;
            gvPickNominee.Visible = true;
            
            lblPickJointHolder.Visible = false;
            gvPickJointHolder.Visible = false;
        }

        public void LoadCategory()
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory(group); //Change to the respective GroupCode
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

        public void bindModeOfHolding()
        {
            DataSet dsModeOfHolding = new DataSet();

            dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
            ddlModeOfHolding.DataSource = dsModeOfHolding;
            ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
            ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "Select"));
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
            DataTable dt = orderbo.GetBankAccountDetails(customerId);
            ddlBankName.DataSource = dt;
            ddlBankName.DataTextField = dt.Columns["CB_BankName"].ToString();
            ddlBankName.DataValueField = dt.Columns["CB_CustBankAccId"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = orderbo.GetBankBranch(int.Parse(ddlBankName.SelectedValue.ToString()));
            lblBranchName.Text = dt.Rows[0][1].ToString();
        }

        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.GetOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                ddlOrderStatus.DataBind();
            }
            //ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
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
            dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customerVo);
            //Populating  Pick nominee
            if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
            {
                lblPickNominee.Text = "You have no associates";
            }
            else
            {
                lblPickNominee.Text = "Pick Nominee";
                gvPickNominee.DataSource = dsCustomerAssociation.Tables[0];
                gvPickNominee.DataBind();
            }

            //Populating  Joint Holder nominee
            if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
            {
                lblPickJointHolder.Text = "You have no associates";
            }
            else
            {
                lblPickJointHolder.Text = "Pick Joint Holder";
                gvPickJointHolder.DataSource = dsCustomerAssociation;
                gvPickJointHolder.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool bresult = false;
            string nomineeAssociationIds = "";
            string jointHoldingAssociationIds = "";

            lifeInsuranceOrdervo = new LifeInsuranceOrderVo();

            lifeInsuranceOrdervo.CustomerId = int.Parse(hdnCustomerId.Value);
            lifeInsuranceOrdervo.ApplicationNumber = txtApplicationNo.Text;
            if (txtApplicationDate.SelectedDate != null)
                lifeInsuranceOrdervo.ApplicationReceivedDate = txtApplicationDate.SelectedDate.Value;
            
            lifeInsuranceOrdervo.AssetCategory = "IN";
            lifeInsuranceOrdervo.ChequeNumber = txtPaymentInstrNo.Text;
            lifeInsuranceOrdervo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            lifeInsuranceOrdervo.FrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue;
            lifeInsuranceOrdervo.GIIssuerCode = "0";
            if (rbtnYes.Checked)
            {
                lifeInsuranceOrdervo.IsJointlyHeld = 1;
                if (ddlModeOfHolding.SelectedValue != "Select")
                    lifeInsuranceOrdervo.HoldingMode = ddlModeOfHolding.SelectedValue;
            }
            else if (rbtnNo.Checked)
            {
                lifeInsuranceOrdervo.HoldingMode = "Singly";
                lifeInsuranceOrdervo.IsJointlyHeld = 0;
            }

            lifeInsuranceOrdervo.InsuranceIssuerCode = ddlPolicyIssuer.SelectedValue;
            lifeInsuranceOrdervo.InsuranceSchemeId = 1;
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";
            lifeInsuranceOrdervo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            lifeInsuranceOrdervo.ReasonCode = ddlReason.SelectedValue;
            lifeInsuranceOrdervo.ApprovedBy = int.Parse(hdnCustomerId.Value);
            //lifeInsuranceOrdervo.AssociationId = 0;
            //lifeInsuranceOrdervo.AssociationType = "Joint Holder";
            if (txtMaturityDate.SelectedDate != null)
                lifeInsuranceOrdervo.MaturityDate = txtMaturityDate.SelectedDate.Value;
            if (txtOrderDate.SelectedDate != null)
                lifeInsuranceOrdervo.OrderDate = txtOrderDate.SelectedDate.Value;
            //lifeInsuranceOrdervo.OrderId = ;
            //lifeInsuranceOrdervo.OrderNumber = int.Parse(txtPaymentInstrNo.Text);
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
                        //gvPickNominee.DataKeys[gvr.RowIndex].Values[1].ToString() + "~";
                    }
                }
            }
            if (this.gvPickJointHolder.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPickJointHolder.Rows)
                {
                    if (((CheckBox)gvr.FindControl("PJHCheckBox")).Checked == true)
                    {
                        jointHoldingAssociationIds = gvPickJointHolder.DataKeys[gvr.RowIndex].Value + "~";
                    }
                }
            }
            bresult = orderbo.AddLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, jointHoldingAssociationIds);
            if (bresult == true)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderMIS','?result=" + result + "');", true);
                pnlOrderSteps.Visible = true;
                SetEnableDisableControls("Entry");
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
            btnAddCustomer.Enabled = bentry;
            ddlInstrumentCategory.Enabled = bentry;
            ddlPolicyIssuer.Enabled = bentry;
            ddlAssetPerticular.Enabled = bentry;
            btnAddNew.Enabled = bentry;
            pnlHolding.Enabled = bentry;
            rbtnYes.Enabled = bentry;
            rbtnNo.Enabled = bentry;
            ddlModeOfHolding.Enabled = bentry;
            gvPickNominee.Enabled = bentry;
            gvPickJointHolder.Enabled = bentry;

            ddlPaymentMode.Enabled = bentry;
            txtPaymentInstruDate.Enabled = bentry;
            ddlBankName.Enabled = bentry;
            btnAddBank.Enabled = bentry;
            txtPaymentInstrNo.Enabled = bentry;

            txtApplicationNo.Enabled = bentry;
            txtApplicationDate.Enabled = bentry;
            txtMaturityDate.Enabled = bentry;
            txtOrderDate.Enabled = bentry;
            txtSumAssured.Enabled = bentry;
            ddlEPPremiumFrequencyCode.Enabled = bentry;
            ddlOrderStatus.Enabled = bentry;
            ddlReason.Enabled = bentry;
            chkCA.Enabled = bentry;
        }

        public void SetEditableControls(bool editable)
        {
            ddlPaymentMode.Enabled = editable;
            txtPaymentInstruDate.Enabled = editable;
            ddlBankName.Enabled = editable;
            btnAddBank.Enabled = editable;
            txtPaymentInstrNo.Enabled = editable;

            txtApplicationNo.Enabled = editable;
            txtApplicationDate.Enabled = editable;
            txtMaturityDate.Enabled = editable;
            txtOrderDate.Enabled = editable;
            txtSumAssured.Enabled = editable;
            ddlEPPremiumFrequencyCode.Enabled = editable;
            ddlOrderStatus.Enabled = editable;
            ddlReason.Enabled = editable;
            chkCA.Enabled = editable;
        }

        public void SetValuesToControls(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        {
            hdnCustomerId.Value = lifeInsuranceOrdervo.CustomerId.ToString();
            LoadCustomerDetails(int.Parse(hdnCustomerId.Value));
            txtApplicationNo.Text = lifeInsuranceOrdervo.ApplicationNumber;
            txtApplicationDate.SelectedDate = lifeInsuranceOrdervo.ApplicationReceivedDate;
            lifeInsuranceOrdervo.AssetCategory = "IN";
            txtPaymentInstrNo.Text = lifeInsuranceOrdervo.ChequeNumber;
            ddlBankName.SelectedValue = lifeInsuranceOrdervo.CustBankAccId.ToString();
            ddlEPPremiumFrequencyCode.SelectedValue = lifeInsuranceOrdervo.FrequencyCode;

            lifeInsuranceOrdervo.GIIssuerCode = "0";
            //if (lifeInsuranceOrdervo.AssociationType == "Joint Holder")
            //{

            //}

            if (rbtnYes.Checked)
            {
                lifeInsuranceOrdervo.IsJointlyHeld = 1;
                ddlModeOfHolding.SelectedValue = lifeInsuranceOrdervo.HoldingMode;
            }
            else if (rbtnNo.Checked)
            {
                lifeInsuranceOrdervo.HoldingMode = "Singly";
                lifeInsuranceOrdervo.IsJointlyHeld = 0;
            }

            ddlPolicyIssuer.SelectedValue = lifeInsuranceOrdervo.InsuranceIssuerCode;
            lifeInsuranceOrdervo.InsuranceSchemeId = 1;
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";
            ddlOrderStatus.SelectedValue = lifeInsuranceOrdervo.OrderStatusCode;
            ddlReason.SelectedValue = lifeInsuranceOrdervo.ReasonCode;
            //lifeInsuranceOrdervo.ApprovedBy = int.Parse(hdnCustomerId.Value);

            txtMaturityDate.SelectedDate = lifeInsuranceOrdervo.MaturityDate;
            txtOrderDate.SelectedDate = lifeInsuranceOrdervo.OrderDate;
            //lifeInsuranceOrdervo.OrderId = ;
            //lifeInsuranceOrdervo.OrderNumber = int.Parse(txtPaymentInstrNo.Text);
            txtPaymentInstruDate.SelectedDate = lifeInsuranceOrdervo.PaymentDate;
            ddlPaymentMode.SelectedValue = lifeInsuranceOrdervo.PaymentMode;
            lifeInsuranceOrdervo.SourceCode = "ME";
            txtSumAssured.Text = lifeInsuranceOrdervo.SumAssured.ToString();

            //if (this.gvPickNominee.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvr in this.gvPickNominee.Rows)
            //    {
            //        if (((CheckBox)gvr.FindControl("PNCheckBox")).Checked == true)
            //        {
            //            nomineeAssociationIds += gvPickNominee.DataKeys[gvr.RowIndex].Value + "~";            //            
            //        }
            //    }
            //}
            //if (this.gvPickJointHolder.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvr in this.gvPickJointHolder.Rows)
            //    {
            //        if (((CheckBox)gvr.FindControl("PJHCheckBox")).Checked == true)
            //        {
            //            jointHoldingAssociationIds = gvPickJointHolder.DataKeys[gvr.RowIndex].Value + "~";
            //        }
            //    }
            //}
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //if(txtAsset.Text.Trim() != "")
            //orderbo.InsertAssetParticularScheme(txtAsset.Text.Trim(), ddlPolicyIssuer.SelectedValue);
            if (txtAsset.Text.Trim() != "")
                if (ddlPolicyIssuer.SelectedIndex != 0)
                    orderbo.InsertAssetParticularScheme(txtAsset.Text.Trim(), ddlPolicyIssuer.SelectedValue);

            BindAssetParticular();
        }

        protected void btnInsertAsset_Click(object sender, EventArgs e)
        {
            if (txtAsset.Text.Trim() != "")
                if (ddlPolicyIssuer.SelectedIndex != 0)
                    orderbo.InsertAssetParticularScheme(txtAsset.Text.Trim(), ddlPolicyIssuer.SelectedValue);

            BindAssetParticular();
        }

        protected void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (rbtnYes.Checked)
            {
                ddlModeOfHolding.SelectedIndex = 0;
                if (gvPickNominee.Rows.Count >= 1)
                {
                    lblPickJointHolder.Visible = true;
                    gvPickJointHolder.Visible = true;
                    gvPickNominee.Visible = true;
                    lblPickNominee.Visible = true;
                }
                else
                {
                    gvPickNominee.Visible = false;
                    lblPickNominee.Visible = false;
                }
                ddlModeOfHolding.Enabled = true;
            }
            else if (rbtnNo.Checked)
            {
                ddlModeOfHolding.SelectedIndex = 9;
                ddlModeOfHolding.Enabled = false;
                gvPickNominee.Visible = false;
                lblPickNominee.Visible = false;

                lblPickJointHolder.Visible = true;
                gvPickJointHolder.Visible = true;
            }
        }
        protected void rbtnNo_Load(object sender, EventArgs e)
        {
            if (rbtnNo.Checked)
            {
                ddlModeOfHolding.Enabled = false;
            }
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
            lblIssuarCode.Text = ddlPolicyIssuer.SelectedItem.Text;

            BindAssetParticular();
        }

        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOrderStatusReason();
        }

        private void BindOrderStatusReason()
        {
            DataSet dsStausReason;
            DataTable dtStatusReason;
            dsStausReason = operationBo.GetRejectStatus(ddlOrderStatus.SelectedValue);
            dtStatusReason = dsStausReason.Tables[0];
            if (dtStatusReason.Rows.Count > 0)
            {
                ddlReason.DataSource = dtStatusReason;
                ddlReason.DataValueField = dtStatusReason.Columns["XSR_StatusReasonCode"].ToString();
                ddlReason.DataTextField = dtStatusReason.Columns["XSR_StatusReason"].ToString();
                ddlReason.DataBind();
            }
            //ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsReason;
            DataTable dtReason;
            dsReason = operationBo.GetRejectStatus(ddlOrderStatus.SelectedValue);
            dtReason = dsReason.Tables[0];
            if (dtReason.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtReason;
                ddlOrderStatus.DataValueField = dtReason.Columns["XSR_StatusReasonCode"].ToString();
                ddlOrderStatus.DataTextField = dtReason.Columns["XSR_StatusReason"].ToString();
                ddlOrderStatus.DataBind();
            }
        }

        protected void chkCA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCA.Checked == true)
                ddlOrderStatus.Enabled = true;
            else
            {
                ddlOrderStatus.Enabled = false;
                ddlOrderStatus.SelectedValue = "OMIP";
                //ddlOrderPendingReason.Visible = false;
                //lblOrderPendingReason.Visible = false;

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool bresult = false;
            string nomineeAssociationIds = "";
            string jointHoldingAssociationIds = "";

            lifeInsuranceOrdervo = new LifeInsuranceOrderVo();
            //if (!string.IsNullOrEmpty(hdnCustomerId.Value))
            lifeInsuranceOrdervo.CustomerId = int.Parse(hdnCustomerId.Value);
            lifeInsuranceOrdervo.ApplicationNumber = txtApplicationNo.Text;
            if (txtApplicationDate.SelectedDate != null)
            lifeInsuranceOrdervo.ApplicationReceivedDate = txtApplicationDate.SelectedDate.Value;
            lifeInsuranceOrdervo.AssetCategory = "IN";
            lifeInsuranceOrdervo.ChequeNumber = txtPaymentInstrNo.Text;
            lifeInsuranceOrdervo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            lifeInsuranceOrdervo.FrequencyCode = ddlEPPremiumFrequencyCode.SelectedValue;
            lifeInsuranceOrdervo.GIIssuerCode = "0";
            if (rbtnYes.Checked)
            {
                if (ddlModeOfHolding.SelectedValue != "Select")
                    lifeInsuranceOrdervo.HoldingMode = ddlModeOfHolding.SelectedValue;
            }
            else if (rbtnNo.Checked)
            {
                lifeInsuranceOrdervo.HoldingMode = "Singly";
            }

            lifeInsuranceOrdervo.InsuranceIssuerCode = ddlPolicyIssuer.SelectedValue;
            lifeInsuranceOrdervo.InsuranceSchemeId = 1;
            lifeInsuranceOrdervo.SourceCode = "AL";
            lifeInsuranceOrdervo.OrderStepCode = "AL";
            lifeInsuranceOrdervo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            lifeInsuranceOrdervo.ReasonCode = ddlReason.SelectedValue;
            lifeInsuranceOrdervo.ApprovedBy = int.Parse(hdnCustomerId.Value);
            //lifeInsuranceOrdervo.AssociationId = 0;
            //lifeInsuranceOrdervo.AssociationType = "Joint Holder";
            if (txtMaturityDate.SelectedDate != null)
            lifeInsuranceOrdervo.MaturityDate = txtMaturityDate.SelectedDate.Value;
            if (txtOrderDate.SelectedDate != null)
            lifeInsuranceOrdervo.OrderDate = txtOrderDate.SelectedDate.Value;
            //lifeInsuranceOrdervo.OrderId = ;
            //lifeInsuranceOrdervo.OrderNumber = int.Parse(txtPaymentInstrNo.Text);
            if (txtPaymentInstruDate.SelectedDate != null)
            lifeInsuranceOrdervo.PaymentDate = txtPaymentInstruDate.SelectedDate.Value;
            lifeInsuranceOrdervo.PaymentMode = ddlPaymentMode.SelectedValue;
            lifeInsuranceOrdervo.SourceCode = "ME";
            lifeInsuranceOrdervo.SumAssured = double.Parse(txtSumAssured.Text);

            if (rbtnNo.Checked)
            {
                lifeInsuranceOrdervo.IsJointlyHeld = 0;
            }
            if (rbtnYes.Checked)
            {
                lifeInsuranceOrdervo.IsJointlyHeld = 1;
            }
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
            if (this.gvPickJointHolder.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPickJointHolder.Rows)
                {
                    if (((CheckBox)gvr.FindControl("PJHCheckBox")).Checked == true)
                    {
                        jointHoldingAssociationIds = gvPickJointHolder.DataKeys[gvr.RowIndex].Value + "~";
                    }
                }
            }
            bresult = orderbo.UpdateLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, jointHoldingAssociationIds);
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
            btnUpdate.Visible = true;
            btnSubmit.Visible = false;
            lnkBtnEdit.Visible = false;            
        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["strOrderId"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','none');", true);
            }
        }

        public void BindOrderStepsGrid()
        {
            DataSet dsOrderSteps = new DataSet();
            DataTable dtOrderDetails;
            SetControls(false);
            dsOrderSteps = orderbo.GetOrderStepsDetails(orderId);
            dtOrderDetails = dsOrderSteps.Tables[0];
            if (dtOrderDetails.Rows.Count == 0)
            {
                lblPickNominee.Text = "You have not placed any Order";
            }
            else
            {
                lblPickNominee.Text = "Order Steps";
                rgvOrderSteps.DataSource = dtOrderDetails;
                rgvOrderSteps.DataBind();
                Session["OrderDetails"] = dtOrderDetails;
            }
        }

        protected void rgvOrderSteps_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //DataTable dt = (DataTable)Session["OrderDetails"];
                //RadComboBox rcStatus = (RadComboBox)e.Item.FindControl("rcbStatus");
                //RadComboBox rcPendingReason = (RadComboBox)e.Item.FindControl("rcbPendingReason");
                //rcStatus.AutoPostBack = true;
                //rcStatus.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(this.rcStatus_SelectedIndexChanged);                
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
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem edititem = (GridEditableItem)e.Item;
                RadComboBox rcStatus = (RadComboBox)edititem.FindControl("rcbStatus");
                RadComboBox rcPendingReason = (RadComboBox)edititem.FindControl("rcbPendingReason");
                string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
                int orderid = Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
                BindRadComboBoxStatus(rcStatus, orderStepCode, orderid);
                rcPendingReason.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select Reason", "0"));
                //orderStatusCode = rcStatus.SelectedValue;                
            }

            //if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            //{
            //    GridEditableItem edititem = (GridEditableItem)e.Item;
            //    RadComboBox rcStatus = (RadComboBox)edititem.FindControl("rcbStatus");
            //    RadComboBox rcPendingReason = (RadComboBox)edititem.FindControl("rcbPendingReason");
            //    string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
            //    int orderid = Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
            //    BindRadComboBoxStatus(rcStatus, orderStepCode, orderid);
            //    rcPendingReason.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select Reason", "0"));
            //    //orderStatusCode = rcStatus.SelectedValue;                
            //}
        }

        protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgvOrderSteps.DataSource = Session["OrderDetails"];            
        }

        protected void rgvOrderSteps_ItemCommand(object source, GridCommandEventArgs e)
        {
            bool bResult = false;
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                //string schemeName = dataItem["PASP_SchemePlanName"].Text;

                LinkButton button = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
            }
            else if (e.CommandName == "Update")
            {
                GridEditableItem edititem = e.Item as GridEditableItem;
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                RadComboBox rcStatus = (RadComboBox)editform.FindControl("rcbStatus");
                RadComboBox rcPendingReason = (RadComboBox)editform.FindControl("rcbPendingReason");

                DataTable dt = new DataTable();
                dt = (DataTable)Session["OrderDetails"];

                string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
                int orderId = int.Parse(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
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
            }
            //else
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)rgvOrderSteps.MasterTableView.GetColumn("EditCommandColumn");
            //    if (!editColumn.Visible)
            //        editColumn.Visible = true;
            //    BindOrderStepsGrid();
            //}
        }

        protected void rcStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcStatus = (RadComboBox)o;
            GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
            RadComboBox rcPendingReason = editedItem.FindControl("rcbPendingReason") as RadComboBox;
            string statusOrderCode = rcStatus.SelectedValue;
            BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
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
    }
}