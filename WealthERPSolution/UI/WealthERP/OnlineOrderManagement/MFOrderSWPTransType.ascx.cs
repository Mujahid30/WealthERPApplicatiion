using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using VoUser;
using BoCustomerPortfolio;
using System.Configuration;
using BoProductMaster;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using DaoReports;
using VoCustomerPortfolio;
using BoCustomerPortfolio;


namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderSWTTransType : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        PortfolioBo portfolioBo = new PortfolioBo();

        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataTable dtCustomerAssociates;
        DataRow drCustomerAssociates;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        string path;
        DataTable dtgetfolioNo;
        DataTable dtModeOfHolding;
        DataTable dtGetAllSIPDataForOrder;
        DataTable dtGetAllSIPDataForOrderEdit;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ProductMFBo productMfBo = new ProductMFBo();
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();
        CustomerPortfolioVo custPortVo;
        string strAction;
        string[] AllSipDates;
        int orderIdForEdit;
        int customerIdforEdit;
        List<OnlineMFOrderVo> SipDataForOrderEditList = new List<OnlineMFOrderVo>();
        DataTable dtFrequency;
        string clientMFAccessCode = string.Empty;
        string subcategory = string.Empty;
        int BackOfficeUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            custPortVo = (CustomerPortfolioVo)Session["CustomerPortfolioVo"];
            if (custPortVo == null)
            {
                custPortVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
            }
            divValidationError.Visible = false;
            if (Session["BackOfficeUserId"].ToString() != null)
            {
                BackOfficeUserId = Convert.ToInt32(Session["BackOfficeUserId"]);
            }
            else
            {
                BackOfficeUserId = 0;
            }
            if (!IsPostBack)
            {
                clientMFAccessCode = boOnlineOrder.GetClientMFAccessStatus(customerVo.CustomerId);
                if (clientMFAccessCode == "FA")
                {
                    AmcBind();
                    BindCategory();
                    ShowAvailableLimits();
                    //lnkOfferDoc.Visible = false;
                    //lnkFactSheet.Visible = false;
                    //lnkExitLoad.Visible = false;
                    if (Request.QueryString["strAction"] != null && Request.QueryString["orderId"] != null && Request.QueryString["customerId"] != null)
                    {
                        strAction = Request.QueryString["strAction"].ToString();
                        orderIdForEdit = Convert.ToInt32(Request.QueryString["orderId"].ToString());
                        customerIdforEdit = Convert.ToInt32(Request.QueryString["customerId"].ToString());
                    }
                    else if (Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null)
                    {
                        int accountId = 0;
                        int schemeCode = 0;
                        int amcCode = 0;
                        string category = string.Empty;
                        int isSIPAvaliable=0;
                        accountId = int.Parse(Request.QueryString["accountId"].ToString());
                        schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                        commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category, out isSIPAvaliable, 1);
                        OnDrillDownBindControlValue(amcCode, category, accountId, schemeCode);
                        DataViewOnEdit();
                    }

                    btnSubmit.Text = "Submit";

                    if (strAction == "Edit")
                    {
                        BindSipDetailsForEdit();
                        DataViewOnEdit();
                        btnSubmit.Text = "Modify";
                        onlineMFOrderVo.Action = "Edit";
                    }
                }
                else
                {
                    ShowMessage(boOnlineOrder.GetOnlineOrderUserMessage(clientMFAccessCode), 'I');
                    FreezeControls();
                    divControlContainer.Visible = false;
                    divClientAccountBalance.Visible = false;
                }
            }
        }


        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                lblAvailableLimits.Text = boOnlineOrder.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void OnDrillDownBindControlValue(int amcCode, string category, int accountId, int schemeCode)
        {
            ddlAmc.SelectedValue = amcCode.ToString();
            ddlCategory.SelectedValue = category;
            BindAMCSchemes(amcCode, category);
            ddlScheme.SelectedValue = schemeCode.ToString();

            BindSipUiOnSchemeSelection(schemeCode);
            ddlFolio.SelectedValue = accountId.ToString();

        }


        protected void DataViewOnEdit()
        {
            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlScheme.Enabled = false;
            ddlFolio.Enabled = false;
        }

        protected void BindSipDetailsForEdit()
        {
            SipDataForOrderEditList = new List<OnlineMFOrderVo>();
            SipDataForOrderEditList = commonLookupBo.GetAllSIPDataForOrderEdit(orderIdForEdit, customerIdforEdit);
            onlineMFOrderVo = (OnlineMFOrderVo)SipDataForOrderEditList[0];

            BindEachControlForEditWithVo();
        }


        protected void BindEachControlForEditWithVo()
        {
            BindStartDates();
            BindFrequency();
            SetLatestNav();

            BindFolioNumber(Convert.ToInt32(onlineMFOrderVo.AssetGroup));

            BindSchemes(Convert.ToInt32(onlineMFOrderVo.AssetGroup), "ALL");
            ddlAmc.SelectedValue = onlineMFOrderVo.AssetGroup;
            if (!string.IsNullOrEmpty(onlineMFOrderVo.Category))
                ddlCategory.SelectedValue = onlineMFOrderVo.Category;
            ddlFolio.SelectedValue = onlineMFOrderVo.Folio;
            ddlScheme.SelectedValue = onlineMFOrderVo.SchemePlanCode.ToString();
            ddlFrequency.SelectedValue = onlineMFOrderVo.FrequencyCode;
            ddlDividendFreq.SelectedValue = onlineMFOrderVo.FrequencyCode;
            if (!string.IsNullOrEmpty(onlineMFOrderVo.DivOption))
                ddlDividendOption.SelectedValue = onlineMFOrderVo.DivOption;
            txtAmount.Text = onlineMFOrderVo.Amount.ToString();
            onlineMFOrderVo.StartDate.ToString("dd-MMM-yyyy");
            ddlStartDate.SelectedValue = onlineMFOrderVo.StartDate.ToString();
            lblEndDateDisplay.Text = onlineMFOrderVo.EndDate.ToString();
            BindTotalInstallments();
            ddlTotalInstallments.SelectedValue = onlineMFOrderVo.TotalInstallments.ToString();//for the time being take it as reedemed units
        }

        protected void ddlStartDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlTotalInstallments.SelectedIndex = 0;
        }

        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            if (ddlAmc.SelectedIndex == 0) return;

            DataTable dtAmc = boOnlineOrder.GetCustomerHoldingAMCList(customerVo.CustomerId, 'W').Tables[0];
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
            }
            ddlAmc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlAmc.Items.Insert(0, new ListItem("--SELECT--"));
            ddlAmc.SelectedIndex = 0;
        }

        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlFolio.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;

            if (ddlAmc.SelectedIndex == 0) return;

            BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
            //BindSchemes(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
            BindSchemes();
            DataTable dtAmc = commonLookupBo.GetProdAmc(int.Parse(ddlAmc.SelectedValue), true);
            lnkFactSheet.PostBackUrl = dtAmc.Rows[0]["PA_Url"].ToString();
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlScheme.SelectedIndex = 0;

            if (ddlCategory.SelectedIndex == 0) return;

            BindSchemes();
        }

        private void BindCategory()
        {
            ddlCategory.Items.Clear();

            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory = commonLookupBo.GetCategoryList("MF", null);

                if (dtCategory.Rows.Count > 0)
                {
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                //ddlCategory.Items.Insert(0, new ListItem("--SELECT--"));
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                ddlCategory.Items.Insert(1, new ListItem("All"));
                ddlCategory.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void BindSchemes()
        {
            ddlScheme.Items.Clear();
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));

            ddlScheme.SelectedIndex = 0;

            if (ddlAmc.SelectedIndex == 0 || ddlCategory.SelectedIndex == 0) return;

            string category = ddlCategory.SelectedValue.ToLower() == "all" ? null : ddlCategory.SelectedValue.ToLower();
            int amc = int.Parse(ddlAmc.SelectedValue);

            DataTable dtScheme = new DataTable();

            dtScheme = commonLookupBo.GetAmcSchemeList(amc, category, customerVo.CustomerId, 'W');

            if (dtScheme == null) return;
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            //ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlScheme.SelectedIndex = 0;
        }

        protected void BindAMCSchemes(int amc, string category)
        {
            DataTable dtScheme;
            dtScheme = commonLookupBo.GetAmcSchemeList(amc, category, customerVo.CustomerId, 'W');

            if (dtScheme == null) return;
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            ddlScheme.SelectedIndex = 0;

        }

        protected void BindSchemes(int Amc, string Category)
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            ddlScheme.SelectedIndex = 0;

            string category = Category.ToLower() == "all" ? null : Category.ToLower();

            DataTable dtScheme = new DataTable();

            dtScheme = commonLookupBo.GetAmcSipSchemeList(Amc, category);
            if (dtScheme == null) return;
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            ddlScheme.SelectedIndex = 0;
        }

        private void SaveOrderDetails()
        {
            onlineMFOrderVo.SchemePlanCode = int.Parse(ddlScheme.SelectedValue);
            if (ddlFolio.SelectedIndex > 1)
                onlineMFOrderVo.AccountId = Convert.ToInt32(ddlFolio.SelectedValue);
            onlineMFOrderVo.SystematicTypeCode = "SWP";
            onlineMFOrderVo.SystematicDate = DateTime.Parse(ddlStartDate.SelectedValue).Day;
            onlineMFOrderVo.Amount = double.Parse(txtAmount.Text);
            onlineMFOrderVo.SourceCode = "";
            onlineMFOrderVo.FrequencyCode = ddlFrequency.SelectedValue;
            onlineMFOrderVo.CustomerId = customerVo.CustomerId;
            onlineMFOrderVo.StartDate = DateTime.Parse(ddlStartDate.SelectedValue);
            onlineMFOrderVo.EndDate = DateTime.Parse(lblEndDateDisplay.Text);
            onlineMFOrderVo.SystematicDates = "";
            onlineMFOrderVo.TotalInstallments = int.Parse(ddlTotalInstallments.SelectedValue);
            onlineMFOrderVo.PortfolioId = custPortVo.PortfolioId;
            if (ddlDividendFreq.SelectedIndex > -1)
                onlineMFOrderVo.DivOption = ddlDividendFreq.SelectedValue;
            onlineMFOrderVo.SWPRedeemValueType = ddlRedeem.SelectedValue.ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlRedeem.SelectedValue.ToString() == "AM")
            {
                rgvAmount.MinimumValue = string.IsNullOrEmpty(lblMinAmountValue.Text) == true ? "0" : lblMinAmountValue.Text;
                rgvAmount.MaximumValue = ((int)99999999).ToString();
            }
            else if (ddlRedeem.SelectedValue.ToString()== "UN")
            {
                rgvAmount.MinimumValue = string.IsNullOrEmpty(lblMinUnitValue.Text) == true ? "0" : lblMinUnitValue.Text;
                rgvAmount.MaximumValue = ((int)99999999).ToString();
            }
            Page.Validate("btnSubmit");

            confirmMessage.Text = boOnlineOrder.GetOnlineOrderUserMessage("EUIN");
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);


        }

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderSIPTransType')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            CreateSIPOrder();
        }

        private void CreateSIPOrder()
        {
            bool accountDebitStatus = false;

            if (!Page.IsValid)
            {
                divValidationError.Visible = true;
                return;
            }
            if (btnSubmit.Text == "Submit")
            {
                onlineMFOrderVo.Action = "Insert";
                int retVal = 1;
                    //commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
                if (retVal != 0 && retVal != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please enter amount greater than or equal to minimum SIP amount & in mutiple of subsequent amount');", true);
                    return;
                }

                IDictionary<string, string> sipOrderIds = new Dictionary<string, string>();
                //decimal availableBalance = boOnlineOrder.GetUserRMSAccountBalance(customerVo.AccountId);
                string message = string.Empty;
                int OrderId = 0;
                int sipId = 0;
                //if (availableBalance >= Convert.ToDecimal(onlineMFOrderVo.Amount))
                //{
                    SaveOrderDetails();
                    sipOrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, (BackOfficeUserId != 0) ? BackOfficeUserId : userVo.UserId);
                    OrderId = int.Parse(sipOrderIds["OrderId"].ToString());
                    sipId = int.Parse(sipOrderIds["SIPId"].ToString());

                    //if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                    //{
                    //    accountDebitStatus = boOnlineOrder.DebitRMSUserAccountBalance(customerVo.AccountId, -onlineMFOrderVo.Amount, OrderId);
                    //    ShowAvailableLimits();
                    //}
                //}
                char msgType;
                message = CreateUserMessage(OrderId, sipId, accountDebitStatus, retVal == 1 ? true : false, out msgType);
                ShowMessage(message, msgType);
            }
            else
            {
                onlineMFOrderVo.Action = "Update";
                int retVal = 1;
                    //commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
                if (retVal != 0 && retVal != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rules defined were incorrect');", true);
                    return;
                }
                if (retVal == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:DeleteConfirmation();", true);
                }
                List<int> OrderIds = new List<int>();
                SaveOrderDetails();
                //OrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);

            }

            FreezeControls();

        }

        private string CreateUserMessage(int orderId, int sipId, bool accountDebitStatus, bool isCutOffTimeOver, out char msgType)
        {
            string userMessage = string.Empty;
            msgType = 'S';
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();

                msgType = 'S';
            }
            else if (orderId != 0 && accountDebitStatus == false)
            {
                userMessage = "Order placed successfully,Order will not process due to insufficient balance, Order reference no is " + orderId.ToString();
                msgType = 'F';
            }
            else if (orderId == 0 && sipId != 0)
            {
                userMessage = "SWP Requested successfully, SIP reference no is " + sipId.ToString();
                msgType = 'S';
            }
            else if (orderId == 0 && sipId == 0)
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

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlFrequency.SelectedIndex = 0;
            if (ddlScheme.SelectedIndex == 0) return;
            BindSipUiOnSchemeSelection(Convert.ToInt32(ddlScheme.SelectedValue));

        }

        protected void ShowHideControlsForDivAndGrowth()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0)
            {
                DataView dvFilterDivNGrowth = new DataView(dtGetAllSIPDataForOrder, "PASP_SchemePlanCode='" + ddlScheme.SelectedValue + "'", "PSLV_LookupValueCodeForSchemeOption", DataViewRowState.CurrentRows);

                dtGetAllSIPDataForOrder = dvFilterDivNGrowth.ToTable();
                if (dtGetAllSIPDataForOrder.Rows[0]["PSLV_LookupValueCodeForSchemeOption"].ToString() == "DV")
                {
                    trDividendType.Visible = true;
                    trDividendFrequency.Visible = true;
                    //trDividendOption.Visible = true;
                }
                else
                {
                    trDividendType.Visible = false;
                    trDividendFrequency.Visible = false;
                    //trDividendOption.Visible = false;
                }
            }
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
                strbJointHolder.Append(dr["JointHolderName"].ToString());
                strbNominee.Append(dr["JointHolderName"].ToString());
            }

            lblNomineeDisplay.Text = strbNominee.ToString();
            lblHolderDisplay.Text = strbJointHolder.ToString();
        }
        //protected void GetControlDetails(int scheme, string folio)
        //{
        //    DataSet ds = new DataSet();

        //    ds = boOnlineOrder.GetControlDetails(scheme, ddlFolio.SelectedValue);
        //    DataTable dt = ds.Tables[0];
        //    if (dt.Rows.Count > -1)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString())) lblDividendType.Text = dr["PSLV_LookupValue"].ToString();

        //            if (!string.IsNullOrEmpty(dr["MinAmt"].ToString())) txtMinAmtDisplay.Text = dr["MinAmt"].ToString();

        //            if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString())) lblMutiplesThereAfterDisplay.Text = dr["MultiAmt"].ToString();

        //            if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString())) lblCutOffTimeDisplay.Text = dr["CutOffTime"].ToString();

        //            //if (!string.IsNullOrEmpty(dr["divFrequency"].ToString())) lblFrequency.Text = dr["divFrequency"].ToString();
        //        }
        //    }
        //}

        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();
            double finalamt;
            double finalunits;
            ds = boOnlineOrder.GetControlDetails(scheme, folio,1);
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
                        lbltime.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(dr["RedeemMinAmt"].ToString()))
                    {
                        lblMinAmountValue.Text = dr["RedeemMinAmt"].ToString();
                        lblMinAmountValue.Visible = true;
                    }



                    if (!string.IsNullOrEmpty(dr["RedeemMinUnit"].ToString()))
                    {
                        lblMinUnitValue.Text = dr["RedeemMinUnit"].ToString();
                        lblMinUnitValue.Visible = true;
                    }


                    //if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    //{
                    //    lbldftext.Text = dr["divFrequency"].ToString();
                    //}
                    if (!string.IsNullOrEmpty(dr["url"].ToString()))
                    {
                        lnkFactSheet.PostBackUrl = dr["url"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["PAISC_AssetInstrumentSubCategoryCode"].ToString()) && lblUnitHeldDisplay.Text != null)
                    {
                        subcategory = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    }
                }
            }
            DataSet dsNav = commonLookupBo.GetLatestNav(scheme);
            if (dsNav.Tables[0].Rows.Count > 0)
            {
                string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
            }
            CalculateCurrentholding(ds, out finalunits, out finalamt, dsNav.Tables[0].Rows[0][1].ToString());
            lblUnitHeldDisplay.Text = Math.Round(finalunits, 2).ToString();
            lblUnitHeldDisplay.Text = Math.Round(finalamt, 2).ToString();
            if ((double.Parse(lblUnitHeldDisplay.Text) <= 0) && (subcategory == "MFEQTP"))
            {
                SetControlsState(false);
            }
            else
            {
                SetControlsState(true);

            }
        }

        public void SetControlsState(bool isEnable)
        {
            if (isEnable)
            {
                ddlRedeem.Enabled = true;
                lblUnitHeld.Enabled = true;
                chkTermsCondition.Enabled = true;
                lnkTermsCondition.Enabled = true;
                btnSubmit.Enabled = true;
                //lblMsg.Visible = false;

            }
            else
            {
                ddlRedeem.Enabled = false;
                lblUnitHeld.Enabled = false;
                chkTermsCondition.Enabled = false;
                lnkTermsCondition.Enabled = false;
                btnSubmit.Enabled = false;
                //lblMsg.Visible = true;
            }

        }


        protected void CalculateCurrentholding(DataSet dscurrent, out double units, out double amt, string nav)
        {
            DataTable dt = new DataTable();
            double holdingUnits = 0;
            double valuatedUnits = 0;
            double finalUnits;
            double finalAmt;
            double immatureUnits = 0;
            double Nav = double.Parse(nav);
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

                    if (!string.IsNullOrEmpty(dscurrent.Tables[2].Rows[1][0].ToString()))
                    {
                        valuatedUnits = double.Parse(dscurrent.Tables[2].Rows[1][0].ToString());
                    }


                    if (!string.IsNullOrEmpty(dscurrent.Tables[3].Rows[0][0].ToString()))
                        immatureUnits = double.Parse(dscurrent.Tables[3].Rows[0][0].ToString());

                    finalUnits = holdingUnits - (valuatedUnits + immatureUnits);

                    finalAmt = finalUnits * Nav;

                }
                else
                {
                    if (!string.IsNullOrEmpty(dscurrent.Tables[3].Rows[0][0].ToString()))
                        immatureUnits = double.Parse(dscurrent.Tables[3].Rows[0][0].ToString());
                    finalUnits = double.Parse((dscurrent.Tables[1].Rows[0][0]).ToString()) - immatureUnits;
                    finalAmt = finalUnits * Nav;
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

        protected void BindStartDates()
        {
            ddlStartDate.Items.Clear();

            DateTime[] dtStartdates;

            if (strAction != "Edit" && Convert.ToInt32(ddlScheme.SelectedValue) != 0)
            {
                dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue);
                //else dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(onlineMFOrderVo.SchemePlanCode), onlineMFOrderVo.FrequencyCode);

                foreach (DateTime d in dtStartdates) ddlStartDate.Items.Add(new ListItem(d.ToString("dd-MMM-yyyy")));
                ddlStartDate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                //ddlStartDate.Items.Insert(0, new ListItem("--SELECT--"));
                ddlStartDate.SelectedIndex = 0;
            }
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dpendent controls
            ddlStartDate.SelectedIndex = 0;
            ddlTotalInstallments.SelectedIndex = 0;

            if (ddlFrequency.SelectedIndex == 0) return;

            BindStartDates();
            BindTotalInstallments();
            ShowHideControlsForDivAndGrowth();
            BindSipDetOnFreqSel(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }
        protected void BindSipDetOnFreqSel(string schemeId, string freq)
        {
            DataSet dsSipDetails = boOnlineOrder.GetSipDetails(int.Parse(schemeId), freq);

            if (dsSipDetails == null || dsSipDetails.Tables[0].Rows.Count == 0) return;
            DataRow dtSipDet = dsSipDetails.Tables[0].Rows[0];
            //lblMinAmountrequiredDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            txtMinAmtDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            //lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MultipleAmount"].ToString()), 2).ToString();
            //lblCutOffTimeDisplay.Text = dtSipDet["PASPD_CutOffTime"].ToString();
            //lblUnitHeldDisplay.Text = "0.00";
        }
        protected void hidFolioNumber_ValueChanged(object sender, EventArgs e)
        {
        }

        protected string FormatFloat(float num)
        {
            string strFloat = "0.00";
            try
            {
                strFloat = num.ToString("0.00");
            }
            catch (Exception ex)
            {

            }
            return strFloat;
        }

        protected void SetLatestNav()
        {
            float latNav = 0;

            if (onlineMFOrderVo.Action != "Edit")
            {
                if ((!string.IsNullOrEmpty(ddlScheme.SelectedValue)) || ddlScheme.SelectedValue == "0")
                {
                    DataSet ds = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        latNav = float.Parse(ds.Tables[0].Rows[0][1].ToString());
                        string strDateForNAV = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                        lblNavDisplay.Text = latNav + " " + "As On " + strDateForNAV;
                    }
                }
            }
            else
            {
                DataSet ds = commonLookupBo.GetLatestNav(int.Parse(onlineMFOrderVo.SchemePlanCode.ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    latNav = float.Parse(ds.Tables[0].Rows[0][1].ToString());
                    string strDateForNAV = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                    lblNavDisplay.Text = latNav + " " + "As On " + strDateForNAV;
                }
            }
        }

        protected void BindSipUiOnSchemeSelection(int schemeCode)
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(schemeCode, ddlFrequency.SelectedValue.ToString(),"SWP",1);

            SetLatestNav();
            BindFrequency();
            BindAllControlsWithSIPData();
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
        }

        protected void BindFrequency()
        {
            ddlFrequency.Items.Clear();
            if (dtGetAllSIPDataForOrder == null) return;

            foreach (DataRow row in dtGetAllSIPDataForOrder.Rows)
            {
                if (row["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedValue.ToString())
                {
                    ddlFrequency.Items.Add(new ListItem(row["XF_Frequency"].ToString(), row["XF_FrequencyCode"].ToString()));
                }
            }

            ddlFrequency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));

            //ddlFrequency.Items.Insert(0, new ListItem(""));

            ddlFrequency.SelectedIndex = 0;
        }

        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0)
            {
                txtMinAmtDisplay.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString()), 2).ToString();
                //lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString()), 2).ToString();
                lblCutt.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
                lblEndDateDisplay.Text = string.Empty;
            }
        }

        protected void ShowSipDates(string DelimitedDateVals)
        {
            AllSipDates = DelimitedDateVals.Split(';');

            int i = 0;
            foreach (string date in AllSipDates)
            {
                if (string.IsNullOrEmpty(date)) continue;
                CheckBox chk = new CheckBox();
                chk.ID = "chk_Sip_" + i;
                chk.Text = date;
                chk.Visible = true;
                i++;
            }
        }

        private void BindFolioNumber(int amcCode)
        {
            ddlFolio.Items.Clear();
            if (ddlAmc.SelectedIndex == 0) return;

            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                if (strAction != "Edit")
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId,0);
                else
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(onlineMFOrderVo.AssetGroup), customerVo.CustomerId,0);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }
                ddlFolio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                //ddlFolio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
                //ddlFolio.Items.Insert(0, new ListItem("--SELECT--"));
                //ddlFolio.Items.Insert(1, new ListItem("New", "1"));
                ddlFolio.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        protected void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                //dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociatesRaw.Columns.Add("Name");
                //dtCustomerAssociates.Columns.Add("Relationship");


                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    //drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + "  " + dr["C_LastName"].ToString();
                    //drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates != null)
                {

                }
                //else
                //{
                //    trNominees.Visible = false;
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnkFactSheet.PostBackUrl))
                Response.Write(@"<script language='javascript'>alert('The URL is not valid');</script>");
        }

        protected void BindTotalInstallments()
        {
            ddlTotalInstallments.Items.Clear();

            if (dtGetAllSIPDataForOrder == null || dtGetAllSIPDataForOrder.Rows.Count == 0) dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue.ToString(),"SWP",1);
            if (dtGetAllSIPDataForOrder == null || dtGetAllSIPDataForOrder.Rows.Count == 0) return;

            int minDues;
            int maxDues;
            if (strAction != "Edit")
            {
                minDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinDues"]);
                maxDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MaxDues"]);
            }
            else
            {
                minDues = Convert.ToInt32(onlineMFOrderVo.MinDues);
                maxDues = Convert.ToInt32(onlineMFOrderVo.MaxDues);
            }
            StringBuilder strTotalInstallments = new StringBuilder();

            for (int i = minDues; i <= maxDues; i++) strTotalInstallments.Append(i + "~");

            string str = strTotalInstallments.ToString();

            string[] strSplit = str.Split('~');

            foreach (string s in strSplit)
            {
                if (string.IsNullOrEmpty(s.Trim())) continue;
                ddlTotalInstallments.Items.Add(new ListItem(s.ToString()));
            }
            ddlTotalInstallments.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlTotalInstallments.Items.Insert(0, new ListItem("--SELECT--"));
            ddlTotalInstallments.SelectedIndex = 0;
        }

        protected void ddlTotalInstallments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTotalInstallments.SelectedIndex == 0 || ddlStartDate.SelectedIndex == 0 || ddlFrequency.SelectedIndex == 0) return;

            DateTime dtEndDate = boOnlineOrder.GetSipEndDate(Convert.ToDateTime(ddlStartDate.SelectedValue), ddlFrequency.SelectedValue, Convert.ToInt32(ddlTotalInstallments.SelectedValue) - 1);
            lblEndDateDisplay.Text = dtEndDate.ToString("dd-MMM-yyyy");
        }

        protected void FreezeControls()
        {
            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlScheme.Enabled = false;
            ddlFolio.Enabled = false;
            ddlFrequency.Enabled = false;
            ddlStartDate.Enabled = false;
            txtAmount.Enabled = false;
            ddlTotalInstallments.Enabled = false;
            ddlDividendFreq.Enabled = false;
            ddlDividendOption.Enabled = false;
            trTermsCondition.Visible = false;

            btnSubmit.Visible = false;
            trNewOrder.Visible = true;
        }

        protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolio.SelectedIndex < 1) return;

            if (ddlFolio.SelectedItem.ToString() != "New")
            {
                BindNomineeAndJointHolders();
                GetControlDetails(Convert.ToInt32(ddlScheme.SelectedValue), ddlFolio.SelectedValue);
                trNominee.Visible = true;
                trJointHolder.Visible = true;
                ddlFrequency.SelectedIndex = 0;
            }
            else
            {
                trNominee.Visible = false;
                trJointHolder.Visible = false;
            }
        }

        protected void lnkExitLoad_Click(object sender, EventArgs e)
        {
            lblExitLoad.Text = "";

            if (dtGetAllSIPDataForOrder == null || dtGetAllSIPDataForOrder.Rows.Count==0) dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue.ToString(),"SWP",1);
            if (dtGetAllSIPDataForOrder == null || dtGetAllSIPDataForOrder.Rows.Count == 0) return;

            if (dtGetAllSIPDataForOrder.Rows.Count <= 0) return;
            string strExitLoadPerc = "0.00%";
            string strExitLoadRemk = "No remark";

            DataRow drExitLoad = dtGetAllSIPDataForOrder.Rows[0];
            if (!string.IsNullOrEmpty(drExitLoad["PASPD_ExitLoadPercentage"].ToString()))
                strExitLoadPerc = FormatFloat(float.Parse(drExitLoad["PASPD_ExitLoadPercentage"].ToString())) + "%";
            if (!string.IsNullOrEmpty(drExitLoad["PASPD_ExitLoadRemark"].ToString()))
                strExitLoadRemk = drExitLoad["PASPD_ExitLoadRemark"].ToString();
            lblExitLoad.Text = strExitLoadPerc + "(" + strExitLoadRemk + ")";
        }

        void ShowValidationMessage()
        {
            divValidationError.Visible = true;
        }

        protected void BindDivFrequency()
        {

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

        protected void ddlRedeem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRedeem.SelectedValue == "UN")
            {
                lblAmount.Text = "Units:";
                txtAmount.Text = null;
                txtAmount.Enabled = true;
                //cmpMinAmountUnits.ValueToCompare = lblMinUnitValue.Text;
                //cmpMinAmountUnits.ErrorMessage = "Redemption Units entered should be greater than Minimum Redemption Units. " + lblMinUnitValue.Text;
            }
            else if (ddlRedeem.SelectedValue == "AM")
            {
                lblAmount.Text = "Amount (Rs):";
                txtAmount.Text = null;
                txtAmount.Enabled = true;
                //cmpMinAmountUnits.ValueToCompare = lblMinAmountValue.Text;
                //cmpMinAmountUnits.ErrorMessage = "Redemption Amount entered should be greater than Minimum Redemption Amount (Rs): " + lblMinAmountValue.Text;

            }
            //txtRedeemTypeValue.Visible = true;
          
        }
    }
}