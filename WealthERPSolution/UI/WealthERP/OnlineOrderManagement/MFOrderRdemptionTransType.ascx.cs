using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using BoOnlineOrderManagement;
using System.Configuration;
using VoUser;
using VoOnlineOrderManagemnet;
using DaoReports;

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderRdemptionTransType : System.Web.UI.UserControl
    {
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineMFOrderBo onlineMforderBo = new OnlineMFOrderBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo = new CustomerVo();
        OnlineMFOrderVo onlinemforderVo = new OnlineMFOrderVo();
        UserVo userVo;
        string path;
        DataSet dsCustomerAssociates = new DataSet();
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        int accountId;
        int OrderId;
        DataTable dtgetfolioNo;
        int retVal;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                AmcBind();
                CategoryBind();
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(0, customerVo.CustomerId);

                txtRedeemTypeValue.Visible = false;
                lblOption.Visible = false;
                lblDividendType.Visible = false;
                if (Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null)
                {
                    int accountId = 0;
                    int schemeCode = 0;
                    int amcCode = 0;
                    string category = string.Empty;
                    accountId = int.Parse(Request.QueryString["accountId"].ToString());
                    schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                    commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);
                    SetSelectedDisplay(accountId, schemeCode, amcCode, category);
                }
            }


        }
        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = onlineMforderBo.GetRedeemAmcDetails(customerVo.CustomerId);
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));
                BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

            }
            else
            {
                PurchaseOrderControlsEnable(false);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No existing Investment found');", true); return;
            }
        }

        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryBind();
            SchemeBind(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId);
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

        }

        public void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != -1 && ddlCategory.SelectedIndex != -1)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                string category = ddlCategory.SelectedValue.ToString();
                SchemeBind(amcCode, category, customerVo.CustomerId);
            }

        }

        protected void ddlScheme_onSelectedChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedIndex != -1)
            {
                //ResetControlDetails(sender, e);
                GetControlDetails(int.Parse(ddlScheme.SelectedValue), ddlFolio.SelectedValue.ToString());
                SetControlDetails();
            }
        }

        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";

            lbltime.Text = "";
            lbldftext.Text = "";
            txtRedeemTypeValue.Text = "";
            lblNavDisplay.Text = "";
            ddlAmc.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


        }
        protected void CalculateCurrentholding(DataSet dscurrent, out double units, out double amt)
        {
            DataTable dt = new DataTable();
            double holdingUnits;
            double holdingAmt;
            double ValuatedUnits;
            double ValuatedAmt;
            double finalUnits;
            double finalAmt;

            if (dscurrent.Tables[1].Rows.Count > 0)
            {
                DataTable dtUnit = dscurrent.Tables[1];
                if (dscurrent.Tables[2].Rows.Count > 0 && (!string.IsNullOrEmpty(dscurrent.Tables[2].Rows[0][0].ToString()) || dscurrent.Tables[2].Rows.Count == 2))
                {

                    DataTable dtvaluated = dscurrent.Tables[2];

                    if (!string.IsNullOrEmpty((dscurrent.Tables[1].Rows[0][0]).ToString()))
                    {
                        holdingUnits = double.Parse((dscurrent.Tables[1].Rows[0][0]).ToString());
                    }
                    else holdingUnits = 0.0;

                    if (!string.IsNullOrEmpty(dscurrent.Tables[2].Rows[1][0].ToString()))
                    {
                        ValuatedUnits = double.Parse(dscurrent.Tables[2].Rows[1][0].ToString());
                    }
                    else ValuatedUnits = 0.0;
                    finalUnits = holdingUnits - ValuatedUnits;
                    if (!string.IsNullOrEmpty((dscurrent.Tables[1].Rows[0][1]).ToString()))
                    {
                        holdingAmt = double.Parse((dscurrent.Tables[1].Rows[0][1]).ToString());
                    }
                    else holdingAmt = 0.0;
                    if (!string.IsNullOrEmpty(dscurrent.Tables[2].Rows[1][1].ToString()))
                    {
                        ValuatedAmt = double.Parse(dscurrent.Tables[2].Rows[1][1].ToString());
                    }
                    else ValuatedAmt = 0.0;
                    finalAmt = holdingAmt - ValuatedAmt;
                }
                else
                {
                    finalUnits = double.Parse((dscurrent.Tables[1].Rows[0][0]).ToString());
                    finalAmt = double.Parse((dscurrent.Tables[1].Rows[0][1]).ToString());
                }

            }
            else
            {
                finalAmt = 0.0;
                finalUnits = 0.0;
            }
            units = finalUnits;
            amt = finalAmt;
        }
        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();
            double finalamt;
            double finalunits;
            ds = onlineMforderBo.GetControlDetails(scheme, folio);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }


                    if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString()))
                    {
                        lbltime.Text = dr["CutOffTime"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    {
                        lbldftext.Text = dr["divFrequency"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["url"].ToString()))
                    {
                        lnkFactSheet.PostBackUrl = dr["url"].ToString();
                    }
                }
            }
            DataSet dsNav = commonLookupBo.GetLatestNav(scheme);
            if (dsNav.Tables[0].Rows.Count > 0)
            {
                string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
            }
            CalculateCurrentholding(ds, out finalunits, out finalamt);
            lblUnitsheldDisplay.Text = finalunits.ToString();
            lblCurrentValueDisplay.Text = finalamt.ToString();

        }
        protected void BindAmcForDrillDown()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        protected void SetSelectedDisplay(int Accountid, int SchemeCode, int Amccode, string Category)
        {
            BindAmcForDrillDown();
            ddlAmc.SelectedValue = Amccode.ToString();
            ddlCategory.SelectedValue = Category;
            SchemeBind(Amccode, Category, 0);
            BindFolioNumber(Amccode);
            ddlFolio.SelectedValue = Accountid.ToString();
            ddlScheme.SelectedValue = SchemeCode.ToString();

            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlFolio.Enabled = false;
            ddlScheme.Enabled = false;
            GetControlDetails(SchemeCode, Accountid.ToString());


        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;


            lblDivType.Visible = true;
            lblCurrentValueDisplay.Visible = true;
            lblUnitsheldDisplay.Visible = true;
            if (lblDividendType.Text == "Growth")
            {
                lblDividendFrequency.Visible = false;
                lbldftext.Visible = false;
                lblDivType.Visible = false;
                ddlDivType.Visible = false;
                RequiredFieldValidator3.Enabled = false;

            }
            else
            {
                //lblDividendFrequency.Visible = true;
                //lbldftext.Visible = true;
                lblDivType.Visible = true;
                ddlDivType.Visible = true;
                RequiredFieldValidator3.Enabled = true;

            }
        }
        protected void CategoryBind()
        {
            ddlCategory.Items.Clear();
            DataSet dsCategory = new DataSet();
            dsCategory = commonLookupBo.GetAllCategoryList();

            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataValueField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlCategory.DataTextField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void SchemeBind(int amccode, string category, int customerid)
        {
            ddlScheme.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerid);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        private void GetNetpositionValues(int folio, int scheme)
        {

        }
        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
            try
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        protected void ddlRedeem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRedeem.SelectedValue == "1")
            {
                lblRedeemType.Text = "Units:";
                txtRedeemTypeValue.Text = null;
                txtRedeemTypeValue.Enabled = true;
            }
            else if (ddlRedeem.SelectedValue == "2")
            {
                lblRedeemType.Text = "Amount (Rs):";
                txtRedeemTypeValue.Text = null;
                txtRedeemTypeValue.Enabled = true;

            }
            else if (ddlRedeem.SelectedValue == "3")
            {
                lblRedeemType.Text = "All Units:";
                txtRedeemTypeValue.Text = lblUnitsheldDisplay.Text;
                txtRedeemTypeValue.Enabled = false;
            }
            txtRedeemTypeValue.Visible = true;
        }
        protected void BindNomineeAndJointHolders()
        {
            MFReportsDao MFReportsDao = new MFReportsDao();
            DataSet dsNomineeAndJointHolders;
            dsNomineeAndJointHolders = MFReportsDao.GetARNNoAndJointHoldings(customerVo.CustomerId, 0, ddlFolio.SelectedItem.ToString());
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[1].Rows)
            {
                strbJointHolder.Append(dr["JointHolderName"].ToString() + ",");
                strbNominee.Append(dr["JointHolderName"].ToString() + ",");
            }

            lblNomineeDisplay.Text = strbNominee.ToString();
            lblHolderDisplay.Text = strbJointHolder.ToString();
        }
        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {
                ddlAmc.Enabled = false;
                ddlCategory.Enabled = false;
                ddlScheme.Enabled = false;
                ddlFolio.Enabled = false;
                txtRedeemTypeValue.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                btnSubmit.Enabled = false;
                ddlRedeem.Enabled = false;
                txtRedeemTypeValue.Enabled = false;

            }
            else
            {
                ddlAmc.Enabled = true;
                ddlCategory.Enabled = true;
                ddlScheme.Enabled = true;
                ddlFolio.Enabled = true;
                txtRedeemTypeValue.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;
                btnSubmit.Enabled = true;
                ddlRedeem.Enabled = true;


            }

        }
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            confirmMessage.Text = "I/We here by confirm that this is an execution-only transaction without any iteraction or advice by the employee/relationship manager/sales person of the above distributor or notwithstanding the advice of in-appropriateness, if any, provided by the employee/relationship manager/sales person of the distributor and the distributor has not chargedany advisory fees on this transaction";
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            CreateRedemptionOrderType();
        }

        private void CreateRedemptionOrderType()
        {
            List<int> OrderIds = new List<int>();
            DateTime dtCutOfffTime;
            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            bool isCutOffTimeOver = false;
            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "Sel";
            dtCutOfffTime = DateTime.Parse(lbltime.Text);

            if (DateTime.Now.TimeOfDay > dtCutOfffTime.TimeOfDay && dtCutOfffTime.TimeOfDay < Convert.ToDateTime("24:00:00.000").TimeOfDay)
            {
                isCutOffTimeOver = true;
            }

            if (ddlRedeem.SelectedValue == "1")
            {
                if (!string.IsNullOrEmpty(txtRedeemTypeValue.Text))
                    onlinemforderVo.Redeemunits = double.Parse(txtRedeemTypeValue.Text);
                else
                    onlinemforderVo.Redeemunits = 0;

                float RedeemUnits = float.Parse(string.IsNullOrEmpty(txtRedeemTypeValue.Text) ? "0" : txtRedeemTypeValue.Text);
                float AvailableUnits = float.Parse(string.IsNullOrEmpty(lblUnitsheldDisplay.Text) ? "0" : lblUnitsheldDisplay.Text);
                if ((ddlRedeem.SelectedValue == "1" && (RedeemUnits > AvailableUnits)))
                {
                    retVal = 1;
                }
            }
            else if (ddlRedeem.SelectedValue == "2")
            {
                if (!string.IsNullOrEmpty(txtRedeemTypeValue.Text))
                    onlinemforderVo.Amount = double.Parse(txtRedeemTypeValue.Text);
                else
                    onlinemforderVo.Amount = 0;
                float RedeemAmt = float.Parse(string.IsNullOrEmpty(txtRedeemTypeValue.Text) ? "0" : txtRedeemTypeValue.Text);
                float AvailableAmt = float.Parse(string.IsNullOrEmpty(lblCurrentValueDisplay.Text) ? "0" : lblCurrentValueDisplay.Text);

                if ((ddlRedeem.SelectedValue == "2" && (RedeemAmt > AvailableAmt)))
                {

                    retVal = -1;
                }

            }
            else if (ddlRedeem.SelectedValue == "3")
            { 
            onlinemforderVo.IsAllUnits=true;

            }

            if (retVal != 0)
            {
                if (retVal == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please enter a valid amount');", true); return;
                }
                else if (retVal == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please enter a valid Units');", true); return;
                }
            }
            OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
            OrderId = int.Parse(OrderIds[0].ToString());


            string message = CreateUserMessage(OrderId, isCutOffTimeOver);
            ShowMessage(message);

            PurchaseOrderControlsEnable(false);

        }

        private string CreateUserMessage(int orderId, bool isCutOffTimeOver)
        {
            string userMessage = string.Empty;
            if (orderId != 0)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            return userMessage;

        }

        protected void lnkTermsCondition_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = true;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = false;
            chkTermsCondition.Checked = true;
        }

        public void TermsConditionCheckBox(object o, ServerValidateEventArgs e)
        {
            if (chkTermsCondition.Checked)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                lblAvailableLimits.Text = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }


    }
}




















