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
    public partial class MFOrderAdditionalPurchase : System.Web.UI.UserControl
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
        int OrderId;

        string clientMFAccessCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            RadInformation.VisibleOnPageLoad = false;
            if (!IsPostBack)
            {
                clientMFAccessCode = onlineMforderBo.GetClientMFAccessStatus(customerVo.CustomerId);
                if (clientMFAccessCode == "FA")
                {
                    ShowAvailableLimits();
                    AmcBind();
                    CategoryBind();

                    if ((Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null) || Request.QueryString["Amc"] != null)
                    {
                        int accountId = 0;
                        int schemeCode = 0;
                        int amcCode = 0;
                        string category = string.Empty;
                        schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                        if (Request.QueryString["accountId"] != null)
                        {
                            accountId = int.Parse(Request.QueryString["accountId"].ToString());
                            commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);
                            SetSelectedDisplay(accountId, schemeCode, amcCode, category);
                            SetControlVisisbility();
                        }
                        else
                        {
                            amcCode = int.Parse(Request.QueryString["Amc"].ToString());
                            ddlAmc.SelectedValue = amcCode.ToString();
                            ddlCategory.SelectedValue = Request.QueryString["category"].ToString();
                            SchemeBind(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId);
                            ddlScheme.SelectedValue = schemeCode.ToString();
                            SetControlDetails(schemeCode);
                            if (ddlFolio.SelectedValue != "")
                            {
                                SetSelectedDisplay(int.Parse(ddlFolio.SelectedValue), schemeCode, amcCode, ddlCategory.SelectedValue);
                                BindNomineeAndJointHolders();
                            }
                        }
                    }
                }
                else
                {
                    ShowMessage(onlineMforderBo.GetOnlineOrderUserMessage(clientMFAccessCode), 'I');
                    divControlContainer.Visible = false;
                    divClientAccountBalance.Visible = false;
                }
            }


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
            //BindFolioNumber(Amccode);
            ddlFolio.SelectedValue = Accountid.ToString();
            ddlScheme.SelectedValue = SchemeCode.ToString();

            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlFolio.Enabled = false;
            ddlScheme.Enabled = false;
            //GetControlDetails(SchemeCode, Accountid.ToString());
            SetControlDetails(SchemeCode);


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
        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = onlineMforderBo.GetCustomerHoldingAMCList(customerVo.CustomerId, 'R');
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));

                //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
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
            //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
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
                // ResetControlDetails(sender,e);
                SetControlDetails(int.Parse(ddlScheme.SelectedValue));
                SetControlVisisbility();
            }
        }


        protected void SetControlDetails(int schemeId)
        {
            DataSet ds = new DataSet();
            string schemeDividendOption;
            ds = onlineMforderBo.GetCustomerSchemeFolioHoldings(customerVo.CustomerId, schemeId, out schemeDividendOption);
            DataTable dt = ds.Tables[0];
            //SCHEME DETAILS SET--1
            if (dt.Rows.Count > -1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AdditionalMinAmt"].ToString()))
                    {
                        lblMintxt.Text = dr["AdditionalMinAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AdditionalMultiAmt"].ToString()))
                    {
                        lblMulti.Text = dr["AdditionalMultiAmt"].ToString();
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

                    if (!string.IsNullOrEmpty(dr["AVSD_ExpiryDtae"].ToString()) && Convert.ToDateTime(dr["AVSD_ExpiryDtae"].ToString()) > DateTime.Now && Convert.ToInt16(dr["PMFRD_RatingOverall"].ToString()) > 0)
                    {
                        trSchemeRating.Visible = true;
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_RatingOverall"].ToString() + ".png";

                        //Rating Overall
                        imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + dr["PMFRD_RatingOverall"].ToString() + ".png";

                        //Rating yearwise
                        imgRating3yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating3Year"].ToString() + ".png";
                        imgRating5yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating5Year"].ToString() + ".png";
                        imgRating10yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating10Year"].ToString() + ".png";

                        lblSchemeRetrun3yr.Text = dr["PMFRD_Return3Year"].ToString();
                        lblSchemeRetrun5yr.Text = dr["PMFRD_Return5Year"].ToString();
                        lblSchemeRetrun10yr.Text = dr["PMFRD_Return10Year"].ToString();

                        lblSchemeRisk3yr.Text = dr["PMFRD_Risk3Year"].ToString();
                        lblSchemeRisk5yr.Text = dr["PMFRD_Risk5Year"].ToString();
                        lblSchemeRisk10yr.Text = dr["PMFRD_Risk10Year"].ToString();
                        if (!string.IsNullOrEmpty(dr["PMFRD_RatingDate"].ToString()))
                        {
                            lblSchemeRatingAsOn.Text = "As On " + Convert.ToDateTime(dr["PMFRD_RatingDate"].ToString()).ToShortDateString();
                            lblRatingAsOnPopUp.Text = lblSchemeRatingAsOn.Text;
                        }
                    }
                    else
                    {
                        trSchemeRating.Visible = false;
                        lblSchemeRatingAsOn.Visible = false;
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                    }
                }
            }
            if (lblDividendType.Text == "Dividend" & !string.IsNullOrEmpty(schemeDividendOption))
            {
                ddlDivType.SelectedValue = schemeDividendOption;
            }
            //HOLDINGS SET ---2
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtUnit = ds.Tables[1];
                foreach (DataRow drunits in dtUnit.Rows)
                {
                    if (!string.IsNullOrEmpty(drunits["CMFNP_NetHoldings"].ToString()))
                    {
                        lblUnitsheldDisplay.Text = drunits["CMFNP_NetHoldings"].ToString();
                    }
                }
            }

            //NAV SET----3

            if (ds.Tables[2].Rows.Count > 0)
            {
                string date = Convert.ToDateTime(ds.Tables[2].Rows[0][0]).ToString("dd-MMM-yyyy");
                lblNavDisplay.Text = ds.Tables[2].Rows[0][1] + " " + "As On " + " " + date;
            }

            //FOLIO SET--4
            DataTable dtCustomerMFAccount = ds.Tables[3];
            if (dtCustomerMFAccount.Rows.Count > 0)
                BindFolioNumber(dtCustomerMFAccount);


        }
        protected void SetControlVisisbility()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;
            lblMulti.Visible = true;
            lblMintxt.Visible = true;
            lblDivType.Visible = true;
            lblUnitsheldDisplay.Visible = true;
            if (lblDividendType.Text == "Growth")
            {

                trDivfeq.Visible = false;
                trDivtype.Visible = false;
                trDividendType.Visible = false;
                RequiredFieldValidator4.Enabled = false;

            }
            else
            {
                trDivtype.Visible = true;
                trDividendType.Visible = true;
                RequiredFieldValidator4.Enabled = true;
                ddlDivType.Enabled = false;


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
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerid, 'P');
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("--SELECT--", "0"));
            }
            else
                ddlScheme.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";
            lblMintxt.Text = "";
            lblMulti.Text = "";
            lbltime.Text = "";
            lbldftext.Text = "";
            txtAmt.Text = "";
            lblNavDisplay.Text = "";
            ddlAmc.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


        }

        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {
                ddlAmc.Enabled = false;
                ddlCategory.Enabled = false;
                ddlScheme.Enabled = false;
                ddlFolio.Enabled = false;
                txtAmt.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                trTermsCondition.Visible = false;

                btnSubmit.Visible = false;
                trNewOrder.Visible = true;
            }
            else
            {
                ddlAmc.Enabled = true;
                ddlCategory.Enabled = true;
                ddlScheme.Enabled = true;
                ddlFolio.Enabled = true;
                txtAmt.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;

                btnSubmit.Visible = true;
                trNewOrder.Visible = false;

            }

        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            confirmMessage.Text = onlineMforderBo.GetOnlineOrderUserMessage("EUIN");
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            CreateAdditionalPurchaseOrderType();
        }

        private void CreateAdditionalPurchaseOrderType()
        {
            string message = string.Empty;
            List<int> OrderIds = new List<int>();
            bool accountDebitStatus = false;
            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtAmt.Text.ToString()))
            {
                onlinemforderVo.Amount = double.Parse(txtAmt.Text.ToString());
            }
            else
                onlinemforderVo.Amount = 0.0;
            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "ABY";
            float amt;
            float minAmt;
            float multiAmt;
            DateTime Dt;

            if (string.IsNullOrEmpty(txtAmt.Text))
            {
                amt = 0;

            }
            else
            {
                amt = float.Parse(txtAmt.Text);
            }
            if (string.IsNullOrEmpty(lblMintxt.Text) && string.IsNullOrEmpty(lblMulti.Text) && string.IsNullOrEmpty(lbltime.Text))
            {
                minAmt = 0; multiAmt = 0; Dt = DateTime.MinValue;
            }
            else
            {

                minAmt = float.Parse(lblMintxt.Text);
                multiAmt = float.Parse(lblMulti.Text);
                Dt = DateTime.Parse(lbltime.Text);
            }
            int retVal = commonLookupBo.IsRuleCorrect(amt, minAmt, amt, multiAmt, Dt);
            if (retVal != 0)
            {

                if (retVal == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You should enter the amount in multiples of Subsequent amount');", true); return;
                }
                if (retVal == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have entered amount less than Minimum Initial amount allowed');", true); return;
                }
            }
            decimal availableBalance = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId);
            if (availableBalance >= Convert.ToDecimal(onlinemforderVo.Amount))
            {
                OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
                OrderId = int.Parse(OrderIds[0].ToString());

                if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = onlineMforderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -onlinemforderVo.Amount, OrderId);
                    ShowAvailableLimits();
                }

            }
            char msgType;
            message = CreateUserMessage(OrderId, accountDebitStatus, retVal == 1 ? true : false, out msgType);
            ShowMessage(message, msgType);
            PurchaseOrderControlsEnable(false);

        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver, out char msgType)
        {
            string userMessage = string.Empty;
            msgType = 'S';
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            else if (orderId != 0 && accountDebitStatus == false)
            {
                userMessage = "Order placed successfully,Order will not process due to insufficient balance, Order reference no is " + orderId.ToString();
                msgType = 'F';
            }
            else if (orderId == 0)
            {
                userMessage = "Order cannot be processed. Insufficient balance";
                msgType = 'F';
            }


            return userMessage;

        }

        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
        }

        private void BindFolioNumber(DataTable dtMFAccountNo)
        {
            try
            {

                if (dtMFAccountNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtMFAccountNo;
                    ddlFolio.DataTextField = dtMFAccountNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtMFAccountNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }
                ddlFolio.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
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

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderAdditionalPurchase')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation.VisibleOnPageLoad = true;

        }




    }
}
