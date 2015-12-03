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
    public partial class MFOrderSIPTransType : System.Web.UI.UserControl
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

        protected void Page_Init(object sender, EventArgs e)
        {


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            custPortVo = (CustomerPortfolioVo)Session["CustomerPortfolioVo"];
            RadInformation.VisibleOnPageLoad = false;
            int TOcpmaretime = int.Parse(DateTime.Now.ToShortTimeString().Split(':')[0]);
            if (TOcpmaretime >= int.Parse(ConfigurationSettings.AppSettings["START_TIME"]) && TOcpmaretime < int.Parse(ConfigurationSettings.AppSettings["END_TIME"]))
            {
               
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadTransactPanel('MFOnlineSchemeManager')", true);
                    return;
            }
            if (custPortVo == null)
            {
                custPortVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
            }
            divValidationError.Visible = false;

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
                    //if (Request.QueryString["strAction"] != null && Request.QueryString["orderId"] != null && Request.QueryString["customerId"] != null)
                    //{
                    //    strAction = Request.QueryString["strAction"].ToString();
                    //    orderIdForEdit = Convert.ToInt32(Request.QueryString["orderId"].ToString());
                    //    customerIdforEdit = Convert.ToInt32(Request.QueryString["customerId"].ToString());
                    //}
                    //else if ((Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null) || Session["MFSchemePlan"] != null)
                    //{
                    int amcCode = 0;
                    string category = string.Empty;
                    //    if (Request.QueryString["accountId"] != null)
                    //    {
                    //        schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                    //        accountId = int.Parse(Request.QueryString["accountId"].ToString());
                    //        commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);
                    //        OnDrillDownBindControlValue(amcCode, category, accountId, schemeCode);
                    //        DataViewOnEdit();
                    //    }
                    //    else
                    if (Session["MFSchemePlan"] != null)
                        {
                            //amcCode = int.Parse(Request.QueryString["Amc"].ToString());
                            //ddlAmc.SelectedValue = amcCode.ToString();
                            //ddlCategory.SelectedValue = Request.QueryString["category"].ToString();
                            //BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
                            commonLookupBo.GetSchemeAMCCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category);
                            OnDrillDownBindControlValue(amcCode, category, 0, int.Parse(Session["MFSchemePlan"].ToString()));
                            BindSchemeDividendTypes(int.Parse(Session["MFSchemePlan"].ToString()));
                            //BindSchemes(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);

                            //SchemeBind(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId);
                            //ddlScheme.SelectedValue = schemeCode.ToString();
                            //BindFrequency();
                            //BindAllControlsWithSIPData();
                            //BindFolioNumber(int.Parse(ddlAmc.SelectedValue)); 
                            //SetControlDetails(schemeCode);
                            //SetSelectedDisplay(int.Parse(ddlFolio.SelectedValue), schemeCode, amcCode, category);
                            //BindNomineeAndJointHolders();
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
                //lblAvailableLimits.Text = boOnlineOrder.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void OnDrillDownBindControlValue(int amcCode, string category, int accountId, int schemeCode)
        {
            ddlAmc.SelectedValue = amcCode.ToString();
            lblAmc.Text = ddlAmc.SelectedItem.Text;
            ddlCategory.SelectedValue = category;
            lblCategory.Text = ddlCategory.SelectedItem.Text;
            BindAMCSchemes(amcCode, category);
            ddlScheme.SelectedValue = schemeCode.ToString();
            lblScheme.Text = ddlScheme.SelectedItem.Text;
            BindSipUiOnSchemeSelection(schemeCode);
            //ddlFolio.SelectedValue = accountId.ToString();

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

            DataTable dtAmc = commonLookupBo.GetProdAmc(0, true);
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
            BindSchemes(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
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

            dtScheme = commonLookupBo.GetAmcSipSchemeList(amc, category);
            if (dtScheme == null) return;
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            //ddlScheme.Items.Insert(0, new ListItem("--SELECT--"));
            ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            ddlScheme.SelectedIndex = 0;
        }

        protected void BindAMCSchemes(int amc, string category)
        {
            DataTable dtScheme;
            dtScheme = commonLookupBo.GetAmcSipSchemeList(amc, category);
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
            onlineMFOrderVo.SystematicTypeCode = "SIP";
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
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            rgvAmount.MinimumValue = string.IsNullOrEmpty(lblMinAmountrequiredDisplay.Text) == true ? "0" : lblMinAmountrequiredDisplay.Text;
            rgvAmount.MaximumValue = ((int)99999999).ToString();
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
                int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
                if (retVal != 0 && retVal != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please enter amount greater than or equal to minimum SIP amount & in mutiple of subsequent amount');", true);
                    return;
                }

                IDictionary<string, string> sipOrderIds = new Dictionary<string, string>();
                decimal availableBalance = boOnlineOrder.GetUserRMSAccountBalance(customerVo.AccountId);
                string message = string.Empty;
                int OrderId = 0;
                int sipId = 0;
                if (availableBalance >= Convert.ToDecimal(onlineMFOrderVo.Amount))
                {
                    SaveOrderDetails();
                    sipOrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);
                    OrderId = int.Parse(sipOrderIds["OrderId"].ToString());
                    sipId = int.Parse(sipOrderIds["SIPId"].ToString());

                    if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                    {
                        accountDebitStatus = boOnlineOrder.DebitRMSUserAccountBalance(customerVo.AccountId, -onlineMFOrderVo.Amount, OrderId);
                        ShowAvailableLimits();
                    }
                }
                char msgType;
                message = CreateUserMessage(OrderId, sipId, accountDebitStatus, retVal == 1 ? true : false, out msgType);
                ShowMessage(message, msgType);
            }
            else
            {
                onlineMFOrderVo.Action = "Update";
                int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
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
                userMessage = "SIP Requested successfully, SIP reference no is " + sipId.ToString();
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
            DataView dvFilterDivNGrowth = new DataView(dtGetAllSIPDataForOrder, "PASP_SchemePlanCode='" + ddlScheme.SelectedValue + "'", "PSLV_LookupValueCodeForSchemeOption", DataViewRowState.CurrentRows);

            dtGetAllSIPDataForOrder = dvFilterDivNGrowth.ToTable();
            if (dtGetAllSIPDataForOrder.Rows[0]["PSLV_LookupValueCodeForSchemeOption"].ToString() == "DV")
            {
                //trDividendOption.Visible = true;
                if (ddlScheme.SelectedIndex == 0) return;
                BindSchemeDividendTypes(Convert.ToInt32(ddlScheme.SelectedValue.ToString()));
            }
            else
            {
                trDividendType.Visible = false;
                trDividendFrequency.Visible = false;
                //trDividendOption.Visible = false;
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
        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();

            ds = boOnlineOrder.GetControlDetails(scheme, ddlFolio.SelectedValue);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString())) lblDividendType.Text = dr["PSLV_LookupValue"].ToString();

                    if (!string.IsNullOrEmpty(dr["MinAmt"].ToString())) txtMinAmtDisplay.Text = dr["MinAmt"].ToString();

                    if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString())) lblMutiplesThereAfterDisplay.Text = dr["MultiAmt"].ToString();

                    if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString())) lblCutOffTimeDisplay.Text = dr["CutOffTime"].ToString();

                    //if (!string.IsNullOrEmpty(dr["divFrequency"].ToString())) lblFrequency.Text = dr["divFrequency"].ToString();
                }
            }
        }

        protected void BindStartDates()
        {
            ddlStartDate.Items.Clear();
            DateTime[] dtStartdates;
            if (strAction != "Edit") dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue);
            else dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(onlineMFOrderVo.SchemePlanCode), onlineMFOrderVo.FrequencyCode);
            DateTime currentDate = DateTime.Now;
            foreach (DateTime d in dtStartdates)
                if (d != currentDate)
                ddlStartDate.Items.Add(new ListItem(d.ToString("dd-MMM-yyyy")));
            ddlStartDate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlStartDate.Items.Insert(0, new ListItem("--SELECT--"));
            ddlStartDate.SelectedIndex = 0;
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dpendent controls
            ddlStartDate.SelectedIndex = 0;
            ddlTotalInstallments.SelectedIndex = 0;

            if (ddlFrequency.SelectedIndex == 0) return;

            BindStartDates();
            BindTotalInstallments();
            //ShowHideControlsForDivAndGrowth();
            BindSipDetOnFreqSel(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }
        protected void BindSipDetOnFreqSel(string schemeId, string freq)
        {
            DataSet dsSipDetails = boOnlineOrder.GetSipDetails(int.Parse(schemeId), freq);

            if (dsSipDetails == null || dsSipDetails.Tables[0].Rows.Count == 0) return;
            DataRow dtSipDet = dsSipDetails.Tables[0].Rows[0];
            lblMinAmountrequiredDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            txtMinAmtDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MultipleAmount"].ToString()), 2).ToString();
            lblCutOffTimeDisplay.Text = dtSipDet["PASPD_CutOffTime"].ToString();
            lblUnitHeldDisplay.Text = "0.00";

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
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(schemeCode, ddlFrequency.SelectedValue.ToString(), "SIP");

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
                lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString()), 2).ToString();
                lblCutOffTimeDisplay.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
                lblEndDateDisplay.Text = string.Empty;

                if (!string.IsNullOrEmpty(dtGetAllSIPDataForOrder.Rows[0]["AVSD_ExpiryDtae"].ToString()) && Convert.ToDateTime(dtGetAllSIPDataForOrder.Rows[0]["AVSD_ExpiryDtae"].ToString()) > DateTime.Now && Convert.ToInt16(dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString()) > 0)
                {
                    trSchemeRating.Visible = true;
                    imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString() + ".png";
                    //imgSchemeRating.ImageUrl = @"../Images/msgUnRead.png";

                    //Rating Overall
                    imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString() + ".png";

                    //Rating yearwise
                    imgRating3yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Rating3Year"].ToString() + ".png";
                    imgRating5yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Rating5Year"].ToString() + ".png";
                    imgRating10yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Rating10Year"].ToString() + ".png";

                    lblSchemeRetrun3yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Return3Year"].ToString();
                    lblSchemeRetrun5yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Return5Year"].ToString();
                    lblSchemeRetrun10yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Return10Year"].ToString();

                    lblSchemeRisk3yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Risk3Year"].ToString();
                    lblSchemeRisk5yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Risk5Year"].ToString();
                    lblSchemeRisk10yr.Text = dtGetAllSIPDataForOrder.Rows[0]["PMFRD_Risk10Year"].ToString();
                    if (!string.IsNullOrEmpty(dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingDate"].ToString()))
                    {
                        lblSchemeRatingAsOn.Text = "As On " + Convert.ToDateTime(dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingDate"].ToString()).ToShortDateString();
                        lblRatingAsOnPopUp.Text = lblSchemeRatingAsOn.Text;
                    }

                }
                else
                {
                    lblSchemeRatingAsOn.Visible = false;
                    imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/5.png";
                }

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
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);
                else
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(onlineMFOrderVo.AssetGroup), customerVo.CustomerId);

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
                ddlFolio.Items.Insert(1, new ListItem("New", "1"));
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

            if (dtGetAllSIPDataForOrder == null) dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue.ToString(), "SIP");
            if (dtGetAllSIPDataForOrder == null) return;

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

            btnSubmit.Visible = false;
            //trNewOrder.Visible = true;
        }

        protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolio.SelectedIndex < 1) return;

            if (ddlFolio.SelectedItem.ToString() != "New")
            {
                BindNomineeAndJointHolders();
                GetControlDetails(Convert.ToInt32(ddlScheme.SelectedValue), ddlFolio.SelectedValue);
                //trNominee.Visible = true;
                //trJointHolder.Visible = true;
                ddlFrequency.SelectedIndex = 0;
            }
            else
            {
                //trNominee.Visible = false;
                //trJointHolder.Visible = false;
            }
        }

        protected void lnkExitLoad_Click(object sender, EventArgs e)
        {
            lblExitLoad.Text = "";

            if (dtGetAllSIPDataForOrder == null) dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue.ToString(), "SIP");
            if (dtGetAllSIPDataForOrder == null) return;

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

        protected void BindSchemeDividendTypes(int schemeId)
        {
            DataTable dtSchemeDividendOption = commonLookupBo.GetMFSchemeDividentType(schemeId);
            ddlDividendFreq.Items.Clear();
            if (dtSchemeDividendOption.Rows.Count > 0)
            {
                ddlDividendFreq.DataSource = dtSchemeDividendOption;
                ddlDividendFreq.DataValueField = dtSchemeDividendOption.Columns["PSLV_LookupValueCode"].ToString();
                ddlDividendFreq.DataTextField = dtSchemeDividendOption.Columns["PSLV_LookupValue"].ToString();
                ddlDividendFreq.DataBind();
                ddlDividendFreq.Items.Insert(0, new ListItem("--SELECT--", "0"));

            }

        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation.VisibleOnPageLoad = true;

        }
    }
}
