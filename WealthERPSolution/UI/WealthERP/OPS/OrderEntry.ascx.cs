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
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();

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
                //trfutureDate.Visible = false;
                //trFutureTrigger.Visible = false;
                //BindBranchDropDown();
                //BindRMDropDown();
                //BindPortfolioDropdown();
                //BindFolionumberDropdown(portfolioId);
                //SchemeDropdown();
                ShowHideFields(0);
                

            }
            if (ViewState["ActionEditViewMode"] == null)
            {
                ViewState["ActionEditViewMode"] = "View";
            }

            if (ViewState["ActionEditViewMode"].ToString() == "View")
            {
                ////SetEditViewMode(true);
            }
            else if (ViewState["ActionEditViewMode"].ToString() == "Edit")
            {
                //SetEditViewMode(false);
            }

        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            customerId = int.Parse(txtCustomerId.Value);
        }
        //private void BindFolionumberDropdown(int portfolioId)
        //{
        //    DataSet dsCustomerAccounts = new DataSet();
        //    DataTable dtCustomerAccounts;

        //    if (schemePlanCode != 0)
        //    {
        //        portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
        //        dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
        //    }

        //    if (dsCustomerAccounts.Tables.Count > 0)
        //    {
        //        dtCustomerAccounts = dsCustomerAccounts.Tables[0];

        //        ddlFolioNumber.DataSource = dtCustomerAccounts;
        //        ddlFolioNumber.DataTextField = "CMFA_FolioNum";
        //        ddlFolioNumber.DataValueField = "CMFA_AccountId";
        //        ddlFolioNumber.DataBind();
        //    }
        //    ddlFolioNumber.Items.Insert(0, "Select a Folio");
        //}

        //private void BindPortfolioDropdown()
        //{
        //    DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
        //    ddlPortfolio.DataSource = ds;
        //    ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
        //    ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
        //    ddlPortfolio.DataBind();
        //}

        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {
            //trfutureDate.Visible = false;
            //trFutureTrigger.Visible = false;
            trSectionTwo10.Visible = false;

        }

        protected void rbtnFuture_CheckedChanged(object sender, EventArgs e)
        {
            //trfutureDate.Visible = true;
            //trFutureTrigger.Visible = true;
            trSectionTwo10.Visible = true;
        }
        //private void BindRMDropDown()
        //{
        //    AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        //    DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlRM.DataSource = dt;
        //        ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
        //        ddlRM.DataTextField = dt.Columns["RMName"].ToString();
        //        ddlRM.DataBind();
        //    }
        //    ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
        //}
        //private void BindBranchDropDown()
        //{

        //    RMVo rmVo = new RMVo();
        //    rmVo = (RMVo)Session[SessionContents.RmVo];
        //    int bmID = rmVo.RMId;

        //    UploadCommonBo uploadsCommonDao = new UploadCommonBo();
        //    DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
        //    if (ds != null)
        //    {
        //        ddlBranch.DataSource = ds;
        //        ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
        //        ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
        //        ddlBranch.DataBind();
        //    }
        //    ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));

        //}

        //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int bmID = rmVo.RMId;
        //    if (ddlBranch.SelectedIndex == 0)
        //    {
        //        BindRMforBranchDropdown(0, bmID);
        //    }
        //    else
        //    {
        //        BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
        //    }
        //}
        //private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        //{

        //    DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
        //    if (ds != null)
        //    {
        //        ddlRM.DataSource = ds.Tables[0]; ;
        //        ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
        //        ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
        //        ddlRM.DataBind();
        //    }
        //    ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        //}

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

                //txtOrederNumber.Enabled = false;
                //txtOrderDate.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlRM.Enabled = false;
                txtCustomerName.Enabled = false;
                btnAddCustomer.Enabled = false;
                ddlTransactionType.Enabled = false;
                //ddlPortfolio.Enabled = false;
                ddlFolioNumber.Enabled = false;
                btnAddFolio.Enabled = false;
                //txtSchemeName.Enabled = false;
                //txtTransactionDate.Enabled = false;
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
                //rbtnPending.Enabled = false;
                //rbtnExecuted.Enabled = false;
                //rbtnCancelled.Enabled = false;
                //rbtnReject.Enabled = false;
                ddlOrderPendingReason.Enabled = false;
                btnSubmit.Enabled = false;
                ddltransType.Enabled = false;

            }
            else
            {
                //txtOrederNumber.Enabled = true;
                //txtOrderDate.Enabled = true;
                ////ddlBranch.Enabled = true;
                //ddlRM.Enabled = true;
                txtCustomerName.Enabled = true;
                btnAddCustomer.Enabled = true;
                ddlTransactionType.Enabled = true;
                //ddlPortfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
                btnAddFolio.Enabled = true;
                //txtSchemeName.Enabled = true;
                //txtTransactionDate.Enabled = true;
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
                //rbtnPending.Enabled = true;
                //rbtnExecuted.Enabled = true;
                //rbtnCancelled.Enabled = true;
                //rbtnReject.Enabled = true;
                ddlOrderPendingReason.Enabled = true;
                btnSubmit.Enabled = true;
                ddltransType.Enabled = true;

            }
        
        
        }

        //public void SchemeDropdown()
        //{
        //    DataSet dsSystematicMIS;
            
        //    try
        //    {
        //        dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(19.ToString());

        //        if (dsSystematicMIS.Tables.Count>1)
        //        {
        //            ddlAmcScheme.DataSource = dsSystematicMIS.Tables[1];
        //            ddlAmcSchemeList.DataValueField = dsSystematicMIS.Tables[1].Columns["PA_AMCCode"].ToString();
        //            ddlAmcSchemeList.DataTextField = dsSystematicMIS.Tables[1].Columns["PASP_SchemePlanName"].ToString();
        //            ddlAmcSchemeList.DataBind();
        //        }
        //        ddlAmcSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "Select"));

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

        //        object[] objects = new object[3];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
     

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int result = 1234;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderMIS','?result=" + result + "');", true);
        }

        protected void ddltransType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddltransType.SelectedValue == "New_Purchase" || ddltransType.SelectedValue == "Additional_Purchase" || ddltransType.SelectedValue == "SIP")
            {
              
                ShowTransactionType(1);

            }
            else if (ddltransType.SelectedValue == "Sell" || ddltransType.SelectedValue == "STP" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "Switch")
            {
                ShowTransactionType(2);
                if (ddltransType.SelectedValue == "Switch")
                {
                    trSell3.Visible = true;
                }
                else
                {
                    trSell3.Visible = false;
 
                }
 
            }
            else if (ddltransType.SelectedValue == "Change_Of_Address_Form")
            {
                ShowTransactionType(3);  

            }
        }


        protected void ShowTransactionType(int type)
        {
            if (type == 0)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = false;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = false;
 
            }
            else if (type == 1)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = true;
                trPurchase1.Visible = true;
                trPurchase2.Visible = true;
                trPurchase3.Visible = true;
                trPurchase4.Visible = true;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = true;
 
            }
            else if (type == 2)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = true;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = true;
                trSell2.Visible = true;
                trSell3.Visible = true;
                trSell4.Visible = true;

                trBtnSubmit.Visible = true;
 
            }
            if (type == 3)
            {
                trAddress1.Visible = true;
                trAddress2.Visible = true;
                trAddress3.Visible = true;
                trAddress4.Visible = true;
                trAddress5.Visible = true;
                trAddress6.Visible = true;
                trAddress7.Visible = true;
                trAddress8.Visible = true;
                trAddress9.Visible = true;
                trAddress10.Visible = true;

                trPurchase.Visible = true;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = true;
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedIndex != 0)
            {
                ShowHideFields(1);
            }
            else if (DropDownList2.SelectedIndex == 0)
            {
                ShowHideFields(0);
                ddltransType.SelectedIndex = 0;
                rbtnImmediate.Checked = true;
                trSell3.Visible = false;
                trBtnSubmit.Visible = false;
            }

        }

        protected void ShowHideFields(int flag)
        {
            if (flag == 0)
            {
                trSectionTwo1.Visible = false;
                trSectionTwo2.Visible = false;
                trSectionTwo3.Visible = false;
                trSectionTwo4.Visible = false;
                trSectionTwo5.Visible = false;
                trSectionTwo6.Visible = false;
                trSectionTwo7.Visible = false;
                trSectionTwo8.Visible = false;
                trSectionTwo9.Visible = false;
                trSectionTwo10.Visible = false;
                ShowTransactionType(0);
            }
            else if (flag == 1)
            {
                trSectionTwo1.Visible = true;
                trSectionTwo2.Visible = true;
                trSectionTwo3.Visible = true;
                trSectionTwo4.Visible = true;
                trSectionTwo5.Visible = true;
                trSectionTwo6.Visible = true;
                trSectionTwo7.Visible = true;
                trSectionTwo8.Visible = true;
                trSectionTwo9.Visible = true;
                
 
            }
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','login');", true);
        }

    }
}