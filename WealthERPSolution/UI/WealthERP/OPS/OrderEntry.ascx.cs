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

namespace WealthERP.OPS
{
    public partial class OrderEntry : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();

        int portfolioId;
        int schemePlanCode;
        int customerId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            if (!IsPostBack)
            {

                if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
                trfutureDate.Visible = false;
                trFutureTrigger.Visible = false;
                BindBranchDropDown();
                BindRMDropDown();
                BindPortfolioDropdown();
                BindFolionumberDropdown(portfolioId);

            }
            if (ViewState["ActionEditViewMode"] == null)
            {
                ViewState["ActionEditViewMode"] = "View";
            }

            if (ViewState["ActionEditViewMode"].ToString() == "View")
            {
                SetEditViewMode(true);
            }
            else if (ViewState["ActionEditViewMode"].ToString() == "Edit")
            {
                SetEditViewMode(false);
            }

        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            customerId = int.Parse(txtCustomerId.Value);
        }
        private void BindFolionumberDropdown(int portfolioId)
        {
            DataSet dsCustomerAccounts = new DataSet();
            DataTable dtCustomerAccounts;

            if (schemePlanCode != 0)
            {
                portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
            }

            if (dsCustomerAccounts.Tables.Count > 0)
            {
                dtCustomerAccounts = dsCustomerAccounts.Tables[0];

                ddlFolioNumber.DataSource = dtCustomerAccounts;
                ddlFolioNumber.DataTextField = "CMFA_FolioNum";
                ddlFolioNumber.DataValueField = "CMFA_AccountId";
                ddlFolioNumber.DataBind();
            }
            ddlFolioNumber.Items.Insert(0, "Select a Folio");
        }

        private void BindPortfolioDropdown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
        }

        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {
            trfutureDate.Visible = false;
            trFutureTrigger.Visible = false;

        }

        protected void rbtnFuture_CheckedChanged(object sender, EventArgs e)
        {
            trfutureDate.Visible = true;
            trFutureTrigger.Visible = true;
        }
        private void BindRMDropDown()
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlRM.DataSource = dt;
                ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
        }
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;

            UploadCommonBo uploadsCommonDao = new UploadCommonBo();
            DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            if (ds != null)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                ddlBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));

        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bmID = rmVo.RMId;
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
            if (ds != null)
            {
                ddlRM.DataSource = ds.Tables[0]; ;
                ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        }

        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Edit")
            {
                ViewState["ActionEditViewMode"] = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
                SetEditViewMode(false);
            }
        }


        public void SetEditViewMode(bool Bool)
        {

            if (Bool)
            {

                txtOrederNumber.Enabled = false;
                txtOrderDate.Enabled = false;
                ddlBranch.Enabled = false;
                ddlRM.Enabled = false;
                txtCustomerName.Enabled = false;
                btnAddCustomer.Enabled = false;
                ddlTransactionType.Enabled = false;
                ddlPortfolio.Enabled = false;
                ddlFolioNumber.Enabled = false;
                btnAddFolio.Enabled = false;
                txtSchemeName.Enabled = false;
                txtTransactionDate.Enabled = false;
                txtReceivedDate.Enabled = false;
                rbtnImmediate.Enabled = false;
                rbtnFuture.Enabled = false;
                txtFutureDate.Enabled = false;
                txtFutureTrigger.Enabled = false;
                txtAmount.Enabled = false;
                txtUnits.Enabled = false;
                chkCheque.Enabled = false;
                chkECS.Enabled = false;
                chkDraft.Enabled = false;
                txtPaymentNumber.Enabled = false;
                txtPaymentDetails.Enabled = false;
                txtBankDetails.Enabled = false;
                rbtnPending.Enabled = false;
                rbtnExecuted.Enabled = false;
                rbtnCancelled.Enabled = false;
                rbtnReject.Enabled = false;
                ddlOrderPendingReason.Enabled = false;
                btnSubmit.Enabled = false;
                DropDownList1.Enabled = false;

            }
            else
            {
                txtOrederNumber.Enabled = true;
                txtOrderDate.Enabled = true;
                ddlBranch.Enabled = true;
                ddlRM.Enabled = true;
                txtCustomerName.Enabled = true;
                btnAddCustomer.Enabled = true;
                ddlTransactionType.Enabled = true;
                ddlPortfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
                btnAddFolio.Enabled = true;
                txtSchemeName.Enabled = true;
                txtTransactionDate.Enabled = true;
                txtReceivedDate.Enabled = true;
                rbtnImmediate.Enabled = true;
                rbtnFuture.Enabled = true;
                txtFutureDate.Enabled = true;
                txtFutureTrigger.Enabled = true;
                txtAmount.Enabled = true;
                txtUnits.Enabled = true;
                chkCheque.Enabled = true;
                chkECS.Enabled = true;
                chkDraft.Enabled = true;
                txtPaymentNumber.Enabled = true;
                txtPaymentDetails.Enabled = true;
                txtBankDetails.Enabled = true;
                rbtnPending.Enabled = true;
                rbtnExecuted.Enabled = true;
                rbtnCancelled.Enabled = true;
                rbtnReject.Enabled = true;
                ddlOrderPendingReason.Enabled = true;
                btnSubmit.Enabled = true;
                DropDownList1.Enabled = true;

            }
        
        
        }
     

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int result = 1234;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderMIS','?result=" + result + "');", true);
        } 

    }
}