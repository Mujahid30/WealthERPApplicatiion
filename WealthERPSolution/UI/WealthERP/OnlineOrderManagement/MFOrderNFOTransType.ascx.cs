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

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderNFOTransType : System.Web.UI.UserControl
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
        string clientMFAccessCode = string.Empty;
        string exchangeType = string.Empty;
        int debitstatus = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            Session["OrderId"] = OrderId;
            RadInformation.VisibleOnPageLoad = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (Session["ExchangeMode"] != null && Session["ExchangeMode"].ToString() == "Demat")
            {
                CommonLookupBo boCommon = new CommonLookupBo();
                if (!boCommon.CheckForBusinessDate(DateTime.Now))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "LoadBottomPanelFromBlocking", "LoadTransactPanel('MFOnlineSchemeManager');", true);
                    return;
                }
                if (!(now >= TimeSpan.Parse(ConfigurationSettings.AppSettings["BSETradeOpTime"]) && now <= TimeSpan.Parse(ConfigurationSettings.AppSettings["BSETradeEnTime"])))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanel('MFOnlineSchemeManager')", true);
                    return;
                }
            }
            int TOcpmaretime = int.Parse(DateTime.Now.ToShortTimeString().Split(':')[0]);
            if (TOcpmaretime >= int.Parse(ConfigurationSettings.AppSettings["START_TIME"]) && TOcpmaretime < int.Parse(ConfigurationSettings.AppSettings["END_TIME"]))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanel('MFOnlineSchemeManager')", true);
                return;
            }
            if (Session["ExchangeMode"] != null)
                exchangeType = Session["ExchangeMode"].ToString();
            else
                exchangeType = "Online";
            if (!IsPostBack)
            {
                clientMFAccessCode = onlineMforderBo.GetClientMFAccessStatus(customerVo.CustomerId);
                if (clientMFAccessCode == "FA")
                {
                    ShowAvailableLimits();
                    BindNFOSchemeList();
                    BindNomineeAndJointHolders();
                    lblOption.Visible = false;
                    lblDividendType.Visible = false;
                    if (Session["MFSchemePlan"] != null)
                    {
                      
                        ddlScheme.SelectedValue = Session["MFSchemePlan"].ToString();
                        if( ddlScheme.SelectedValue !="")
                        lblScheme.Text = ddlScheme.SelectedItem.Text;
                        GetControlDetails(int.Parse(Session["MFSchemePlan"].ToString()), null);
                        SetControlDetails();
                    }

                }
                else
                {
                    ShowMessage(onlineMforderBo.GetOnlineOrderUserMessage(clientMFAccessCode), 'I');
                    PurchaseOrderControlsEnable(false);
                    divControlContainer.Visible = false;
                    divClientAccountBalance.Visible = false;
                }
            }



        }

        protected void BindNFOSchemeList()
        {
            DataTable dtNFOSchemeList;
            dtNFOSchemeList = commonLookupBo.GetOnlineMFNFOSchemeList();
            if (dtNFOSchemeList.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtNFOSchemeList;
                ddlScheme.DataValueField = dtNFOSchemeList.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtNFOSchemeList.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("--SELECT--", "0"));
            }
            else
            {

            }
        }
        protected void ddlScheme_onSelectedChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedIndex != -1)
            {
                GetControlDetails(int.Parse(ddlScheme.SelectedValue), null);
                SetControlDetails();
            }
        }

        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";
            lblMintxt.Text = "";
            lblMulti.Text = "";
            lbltime.Text = "";
            lbldftext.Text = "";
            txtAmt.Text = "";
            //lblNavDisplay.Text = "";
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


        }
        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();
            DataTable dtSchemeAmcCategory;
            ds = onlineMforderBo.GetControlDetails(scheme, folio,1);
            dtSchemeAmcCategory = commonLookupBo.GetMFSchemeAMCCategory(scheme);

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MinAmt"].ToString()))
                    {
                        lblMintxt.Text = dr["MinAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString()))
                    {
                        lblMulti.Text = dr["MultiAmt"].ToString();
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

                    if (!string.IsNullOrEmpty(dr["PASP_NFOStartDate"].ToString()))
                    {
                        NFOStartDate.Text = Convert.ToDateTime(dr["PASP_NFOStartDate"].ToString()).ToShortDateString().ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["PASP_NFOEndDate"].ToString()))
                    {
                        NFOEndDate.Text = Convert.ToDateTime(dr["PASP_NFOEndDate"].ToString()).ToShortDateString().ToString();
                    }

                }

            }
            if (dtSchemeAmcCategory.Rows.Count > 0)
            {
                lblNFOSchemeAMC.Text = dtSchemeAmcCategory.Rows[0]["PA_AMCName"].ToString();
                lblSchemeCategory.Text = dtSchemeAmcCategory.Rows[0]["PAIC_AssetInstrumentCategoryName"].ToString();
                BindFolioNumber(Convert.ToInt32(dtSchemeAmcCategory.Rows[0]["PA_AMCCode"].ToString()));
            }

        }
        private void BindFolioNumber(int amcCode)
        {
            DataTable dtCustomerFolioList;
            try
            {
                ddlFolio.Items.Clear();
                dtCustomerFolioList = commonLookupBo.GetFolioNumberForSIP(amcCode, customerVo.CustomerId,0);
                if (dtCustomerFolioList.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtCustomerFolioList;
                    ddlFolio.DataTextField = dtCustomerFolioList.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtCustomerFolioList.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }
                ddlFolio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                //ddlFolio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                //ddlFolio.Items.Insert(0, new ListItem("--SELECT--"));
                ddlFolio.Items.Insert(1, new ListItem("New", "1"));
                ddlFolio.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;
            lblMulti.Visible = true;
            lblMintxt.Visible = true;
            //lblDivType.Visible = true;

            if (lblDividendType.Text == "Growth")
            {
                lblDividendFrequency.Visible = false;
                lbldftext.Visible = false;

                //lblDivType.Visible = false;
                //ddlDivType.Visible = false;
                RequiredFieldValidator4.Enabled = false;
                divDividentType.Visible = false;


            }
            else
            {
                // lblDividendFrequency.Visible = true;
                //lbldftext.Visible = true;
                //lblDivType.Visible = true;
                //ddlDivType.Visible = true;                
                divDividentType.Visible = true;
                BindSchemeDividendTypes(Convert.ToInt32(ddlScheme.SelectedValue.ToString()));
                RequiredFieldValidator4.Enabled = true;



            }


        }
        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {

                ddlScheme.Enabled = false;
                ddlFolio.Enabled = false;
                txtAmt.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                //trTermsCondition.Visible = false;

                btnSubmit.Visible = false;
                trNewOrder.Visible = true;

            }
            else
            {
                btnSubmit.Enabled = true;
                ddlScheme.Enabled = true;
                ddlFolio.Enabled = true;
                txtAmt.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;
                btnSubmit.Enabled = true;

                btnSubmit.Visible = true;
                trNewOrder.Visible = false;
            }

        }
        protected void LoadOrderDetails()
        {

            int ID = int.Parse(Session["OrderId"].ToString());
            onlinemforderVo = onlineMforderBo.GetOrderDetails(ID);


        }
        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnkFactSheet.PostBackUrl))
                Response.Write(@"<script language='javascript'>alert('The URL is not valid');</script>");
        }


        protected void SchemeBind(int amccode, string category)
        {
            ddlScheme.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, 0, 'P');
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));
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
            if(exchangeType =="Online")
            CreateNFOOrderType();
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('NFO is not avaliable for Exchange');", true); return;

        }

        private void CreateNFOOrderType()
        {
            List<int> OrderIds = new List<int>();
            bool accountDebitStatus = false;
            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtAmt.Text.ToString()))
            {
                onlinemforderVo.Amount = double.Parse(txtAmt.Text.ToString());
            }
            else
                onlinemforderVo.Amount = 0.0;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "BUY";
            if (ddlFolio.SelectedValue.ToString() != "1")
            {
                onlinemforderVo.AccountId = Convert.ToInt32(ddlFolio.SelectedValue.ToString());
                onlinemforderVo.FolioNumber = ddlFolio.SelectedValue.ToString();
            }
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
                if (retVal == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have entered amount less than Minimum Initial amount allowed');", true); return;
                }
                if (retVal == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You should enter the amount in multiples of Subsequent amount ');", true); return;
                }

            }
            decimal availableBalance = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId);
            string message = string.Empty;

            if (availableBalance >= Convert.ToDecimal(onlinemforderVo.Amount))
            {
                OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
                OrderId = int.Parse(OrderIds[0].ToString());

                if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = onlineMforderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -onlinemforderVo.Amount, OrderId, out debitstatus);
                    ShowAvailableLimits();
                }

            }
            char msgType;
            message = CreateUserMessage(OrderId, accountDebitStatus, retVal == 1 ? true : false, out msgType);
            PurchaseOrderControlsEnable(false);
            ShowMessage(message, msgType);

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

        protected void lnkEdit_Click(object sender, EventArgs e)
        { }
        protected void lnkBack_Click(object sender, EventArgs e)
        { }

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
                // lblAvailableLimits.Text = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderBuyTransTypeOffline')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderBuyTransTypeOffline')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }

        protected void BindSchemeDividendTypes(int schemeId)
        {
            DataTable dtSchemeDividendOption = commonLookupBo.GetMFSchemeDividentType(schemeId);
            ddlDivType.Items.Clear();
            if (dtSchemeDividendOption.Rows.Count > 0)
            {
                ddlDivType.DataSource = dtSchemeDividendOption;
                ddlDivType.DataValueField = dtSchemeDividendOption.Columns["PSLV_LookupValueCode"].ToString();
                ddlDivType.DataTextField = dtSchemeDividendOption.Columns["PSLV_LookupValue"].ToString();
                ddlDivType.DataBind();
                ddlDivType.Items.Insert(0, new ListItem("--SELECT--", "0"));

            }

        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation.VisibleOnPageLoad = true;

        }

        protected void BindNomineeAndJointHolders()
        {
            OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
            DataSet dsNomineeAndJointHolders = OnlineBondBo.GetNomineeJointHolder(customerVo.CustomerId);
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[0].Rows)
            {
                //strbJointHolder.Append(dr["CustomerName"].ToString() + ",");
                string r = dr["CEDAA_AssociationType"].ToString();
                if (r != "Joint Holder")
                    strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                else
                    strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                //strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                //strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
            }
            lblNomineeDisplay.Text = strbNominee.ToString().TrimEnd(',');
            lblHolderDisplay.Text = strbJointHolder.ToString().TrimEnd(',');
        }
    }
}