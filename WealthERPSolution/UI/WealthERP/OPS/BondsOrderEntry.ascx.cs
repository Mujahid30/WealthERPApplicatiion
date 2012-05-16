using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using BoCommon;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using BoOps;
using BoProductMaster;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOps;
using VoUser;
using WealthERP.Base;


namespace WealthERP.OPS
{
    public partial class BondsOrderEntry : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo = new OperationVo();
        AssetBo assetBo = new AssetBo();

        BondOrderVo bondOrderVo;
        OrderBo orderbo = new OrderBo();

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
        DataTable dtBankName = new DataTable();
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates = new DataTable();
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataRow drCustomerAssociates;
        BoDematAccount boDematAccount;

        DataTable dtFrequency;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            orderNumber = operationBo.GetOrderNumber();
            orderNumber = orderNumber + 1;
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Request.QueryString["action"] != null)
            {
                ViewForm = Request.QueryString["action"].ToString();
                operationVo = (OperationVo)Session["operationVo"];
            }
            if (Session["operationVo"] != null)
            {
                operationVo = (OperationVo)Session["operationVo"];
            }
            rmVo = (RMVo)Session[SessionContents.RmVo];

            int bmID = rmVo.RMId;

            if (!IsPostBack)
            {
                rbtnNo.Checked = true;
                bindModeOfHolding();
                if (Request.QueryString["CustomerId"] != null)
                {
                    customerId = Convert.ToInt32(Request.QueryString["CustomerId"]);
                    SetCustomerDetails();
                }

                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }
                if (ddlForm.SelectedValue == "0")
                {
                    HideDematDetails();
                }
                rbtnNo_Load();
            }

            //BindAssetParticularForBonds();
            LoadBondIssuerName();
            BindFrequencyDropdown();
            BindPaymentMode();
        }

        private void SetCustomerDetails()
        {
            if (customerId != 0)
            {
                customerVo = customerBo.GetCustomer(customerId);
                hdnCustomerId.Value = customerVo.CustomerId.ToString();
                txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
            }
        }

        protected void btnAddBank_OnClick(object sender, EventArgs e)
        {
            //Response.Redirect("TestPage.aspx?PageId=CustomerType");

        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','page=Entry');", true);
        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                BindNomineeGrid();
                LoadBondIssuerName();

                BindBankAccountDetails();
                BindOrderStatus();
                BindAssetParticularForBonds();
                if (rbtnNo.Checked == true)
                {
                }
            }
        }

        protected void btnAddCustomer_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','page=Entry');", true);
        }
        protected void OnClick_AddBank(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('AddBankDetails','page=Entry');", true);
        }

        public void BindNomineeGrid()
        {
            DataSet dsCustomerAssociation = new DataSet();
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            boDematAccount = new BoDematAccount();
            dsCustomerAssociation = boDematAccount.GetCustomerAccociation(customerVo);
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

        protected void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (rbtnYes.Checked)
            {
                ddlModeOfHolding.SelectedIndex = 0;
                lblPickJointHolder.Visible = false;
                gvPickJointHolder.Visible = false;
                if (gvPickNominee.Rows.Count >= 1)
                {
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
            else if(rbtnNo.Checked)
            {
                ddlModeOfHolding.SelectedIndex = 9;
                ddlModeOfHolding.Enabled = false;
                gvPickNominee.Visible = false;
                lblPickNominee.Visible = false;

                lblPickJointHolder.Visible = true;
                gvPickJointHolder.Visible = true;
            }
        }
        protected void rbtnNo_Load()
        {
            if (rbtnNo.Checked)
            {
                ddlModeOfHolding.SelectedIndex = 8;
                ddlModeOfHolding.Enabled = false;
                gvPickJointHolder.Visible = false;
                gvPickNominee.Visible = true;
                lblPickJointHolder.Visible = false;
                lblPickNominee.Visible = true;
            }
        }

        public void bindModeOfHolding()
        {
            DataSet dsModeOfHolding = new DataSet();
            boDematAccount = new BoDematAccount();
            dsModeOfHolding = boDematAccount.GetXmlModeOfHolding();
            ddlModeOfHolding.DataSource = dsModeOfHolding;
            ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
            ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "Select"));
        }

        public void LoadBondIssuerName()
        {
            try
            {
                DataTable dt = orderbo.GetBondsIssuerName();
                ddlBondsIssuer.DataSource = dt;
                ddlBondsIssuer.DataTextField = dt.Columns["PBI_BondIssuer"].ToString();
                ddlBondsIssuer.DataValueField = dt.Columns["PBI_BondIssuerid"].ToString();
                ddlBondsIssuer.DataBind();
                ddlBondsIssuer.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:LoadBondIssuerName()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void ShowDematDetails()
        {
            DataTable dtDemateDetails = new DataTable();
            lblDematName.Visible = true;
            txtDematName.Visible = true;
            lblDPID.Visible = true;
            txtDPID.Visible = true;
            lblBenificiaryAccountNumber.Visible = true;
            txtBenificiaryAccountNumber.Visible = true;

            txtDematName.Enabled = false;
            txtDPID.Enabled = false;
            txtBenificiaryAccountNumber.Enabled = false;

            dtDemateDetails = orderbo.GetCustomerDemateAccountDetails(int.Parse(hdnCustomerId.Value));
            if (dtDemateDetails.Rows.Count > 0)
            {
                hdnDemateAccountId.Value = dtDemateDetails.Rows[0]["CEDA_DematAccountId"].ToString();
                txtDematName.Text = dtDemateDetails.Rows[0]["CEDA_DPName"].ToString();
                txtDPID.Text = dtDemateDetails.Rows[0]["CEDA_DPId"].ToString();
                txtBenificiaryAccountNumber.Text = dtDemateDetails.Rows[0]["CEDA_BeneficiaryAccountNum"].ToString();
            }
        }

        public void HideDematDetails()
        {
            lblDematName.Visible = false;
            txtDematName.Visible = false;
            lblDPID.Visible = false;
            txtDPID.Visible = false;
            lblBenificiaryAccountNumber.Visible = false;
            txtBenificiaryAccountNumber.Visible = false;
        }



        public void ddlForm_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlForm.SelectedValue == "Dematerialised")
            {
                ShowDematDetails();
            }
            else if (ddlForm.SelectedValue == "Physical")
            {
                HideDematDetails();
            }
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

        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ddlOrderStatus.SelectedItem.Selected == true)
            {
                string statuscode = "";
                statuscode = ddlOrderStatus.SelectedValue;
                if (ddlOrderStatus.SelectedValue == "OMPD" || ddlOrderStatus.SelectedValue == "OMRJ" || ddlOrderStatus.SelectedValue == "OMCN")
                {
                    lblReason.Visible = true;
                    ddlReason.Visible = true;
                    BindRejectStatus(statuscode);
                }
                else
                {
                    ddlReason.Visible = false;
                    lblReason.Visible = false;
                }
            }
        }

        private void BindRejectStatus(string statusCode)
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
            ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
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

        public void BindAssetParticularForBonds()
        {
            DataTable dtAssetParticular;
            int bondIssuerId = int.Parse(ddlBondsIssuer.SelectedValue);
            dtAssetParticular = orderbo.GetAssetParticularForBonds(bondIssuerId);
            if (dtAssetParticular.Rows.Count > 0)
            {
                ddlAssetPerticular.DataSource = dtAssetParticular;
                ddlAssetPerticular.DataValueField = dtAssetParticular.Columns["PBS_BondSchemeid"].ToString();
                ddlAssetPerticular.DataTextField = dtAssetParticular.Columns["PBS_BondScheme"].ToString();
                ddlAssetPerticular.DataBind();
                ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "0"));
            }
            //ddlAssetPerticular.Items.Insert(0, new ListItem("Select", "Select"));
        }      

        protected void ddlBondsIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIssuerName.Text = ddlBondsIssuer.SelectedItem.Text;
            BindAssetParticularForBonds();
        }

        protected void btnInsertBondParticular_Click(object sender, EventArgs e)
        {
            int bondIssuerId = int.Parse(ddlBondsIssuer.SelectedValue);
            if (txtAssetParticular.Text.Trim() != "")
                if (ddlBondsIssuer.SelectedIndex != 0)
                    orderbo.InsertAssetParticularForBonds(txtAssetParticular.Text.Trim(), bondIssuerId);

            BindAssetParticularForBonds();
        }

        protected void btnInsertBondIssuerName_Click(object sender, EventArgs e)
        {
            if (txtAsset.Text.Trim() != "")                
                orderbo.InsertBondIssuerName(txtAsset.Text.Trim());

            LoadBondIssuerName();
        }

        protected void btnAllSubmit_Click(object sender, EventArgs e)
        {
            bondOrderVo = new BondOrderVo();

            bondOrderVo.BondSchemeId = 2;
                //int.Parse(ddlAssetPerticular.SelectedValue);
            bondOrderVo.BondIssuerid = int.Parse(ddlBondsIssuer.SelectedValue);
            bondOrderVo.ModeOfHolding = ddlModeOfHolding.SelectedValue;
            bondOrderVo.DepositDate = txtDepositDate.SelectedDate.Value;
            bondOrderVo.MaturityDate = txtMaturityDate.SelectedDate.Value;
            bondOrderVo.FaceValue = int.Parse(txtFaceValue.Text);
            bondOrderVo.IsBuyBackFacility = int.Parse(ddlBuyBackFacility.SelectedValue);
            bondOrderVo.BuyBackDate = txtBuyBackDate.SelectedDate.Value;
            bondOrderVo.Frequency = ddlEPPremiumFrequencyCode.SelectedValue;
            bondOrderVo.BuyBackAmount = int.Parse(txtBuyBackAmount.Text);
            bondOrderVo.Amount = double.Parse(txtAmount.Text);

            if (ddlForm.SelectedValue == "Dematerialised")
                bondOrderVo.IsFormOfHoldingPhysical = 0;
            else if (ddlForm.SelectedValue == "Physical")
                bondOrderVo.IsFormOfHoldingPhysical = 1;

            if (rbtnNo.Checked)            
                bondOrderVo.IsJointlyHeld = 0;            
            if (rbtnYes.Checked)
                bondOrderVo.IsJointlyHeld = 1;

            bondOrderVo.AssetCategory = "FI";
            bondOrderVo.OrderDate = txtOrderDate.SelectedDate.Value;
            bondOrderVo.CustomerId = int.Parse(hdnCustomerId.Value);
            
            bondOrderVo.ApplicationNumber = txtApplicationNo.Text;
            bondOrderVo.ApplicationReceivedDate = txtApplicationDate.SelectedDate.Value;
            bondOrderVo.PaymentMode = ddlPaymentMode.SelectedValue;
            bondOrderVo.ChequeNumber = txtPaymentInstrNo.Text;
            bondOrderVo.PaymentDate = txtInstrumentDate.SelectedDate.Value;
            bondOrderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            bondOrderVo.AccountId = int.Parse(hdnDemateAccountId.Value);

            bondOrderVo.AssociationId = 24823;
            bondOrderVo.AssociationType = "Joint Holder";           
            
            bondOrderVo.OrderStepCode = "CA";
            bondOrderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            bondOrderVo.ReasonCode = ddlReason.SelectedValue;
            bondOrderVo.ApprovedBy = int.Parse(hdnCustomerId.Value);
            
            //bondOrderVo.OrderNumber = int.Parse(txtOrderNumber.Text);
            bondOrderVo.OrderNumber = 9988;
            bondOrderVo.SourceCode = "ME";
            if(chkCustomerApproval.Checked)
                bondOrderVo.IsCustomerApprovalApplicable = 1;
            else
                bondOrderVo.IsCustomerApprovalApplicable = 0;

            //if (this.gvPickJointHolder.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvr in this.gvPickJointHolder.Rows)
            //    {
            //        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
            //        {
            //            i++;
            //            bondOrderVo.AssociationId = int.Parse(gvPickJointHolder.DataKeys[gvr.RowIndex].Values[1].ToString());
            //            bondOrderVo.AssociationType = "Joint Holder";
            //            customerAccountBo.CreatePensionGratuitiesAccountAssociation(customerAccountAssociationVo, userVo.UserId);
            //        }
            //    }
            //}
            //else
            //{
            //    i = -1;
            //}

            //if (rbtnYes.Checked)
            //{   
            //    if (i == 0)
            //    {
            //        trError.Visible = true;
            //        lblError.Text = "Please select a Joint Holder";
            //        blResult = false;
            //    }
            //    else
            //    {
            //        trError.Visible = false;
            //    }
            //}


            orderbo.AddBondOrderDetails(bondOrderVo);
        }
    }
}