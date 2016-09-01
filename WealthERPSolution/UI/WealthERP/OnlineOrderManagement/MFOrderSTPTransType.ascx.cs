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
    public partial class MFOrderSTPTransType : System.Web.UI.UserControl
    {
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineMFOrderBo onlineMforderBo = new OnlineMFOrderBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo = new CustomerVo();
        DataTable dtGetAllSIPDataForOrder;
        UserVo userVo;
        string path;
        int OrderId;
        string clientMFAccessCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            Session["OrderId"] = OrderId;
            if (!IsPostBack)
            {
                clientMFAccessCode = onlineMforderBo.GetClientMFAccessStatus(customerVo.CustomerId);
                if (clientMFAccessCode == "FA")
                {
                    ShowAvailableLimits();
                    AmcBind();
                    CategoryBind();
                    trJointHolder.Visible = false;
                    trNominee.Visible = false;
                    lblOption.Visible = false;
                    lblDividendType.Visible = false;
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
        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            DataSet ds = new DataSet();
            ds = onlineMforderBo.GetCustomerHoldingAMCList(customerVo.CustomerId, 'R');
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAmc.DataSource = ds.Tables[0];
                ddlAmc.DataValueField = ds.Tables[0].Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = ds.Tables[0].Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));
                BindFolioNumber(int.Parse(ddlAmc.SelectedValue), "SO");

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
            SchemeBind(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId, 'R');
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue), "SO");
        }
        public void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != -1 && ddlCategory.SelectedIndex != -1)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                string category = ddlCategory.SelectedValue.ToString();
                SchemeBind(amcCode, category, customerVo.CustomerId, 'R');
            }

        }

        protected void ddlScheme_onSelectedChanged(object sender, EventArgs e)
        {

        }
        protected void ddlFolio_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedIndex != -1)
            {

                GetControlDetails(int.Parse(ddlScheme.SelectedValue), ddlFolio.SelectedValue, "SO");

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
            lblNavDisplay.Text = "";
            ddlAmc.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


        }
        protected void GetControlDetails(int scheme, string folio, String SchemeType)
        {
            DataSet ds = new DataSet();
            double finalamt;
            double finalunits;
            ds = onlineMforderBo.GetControlDetails(scheme, folio,1);
            DataTable dt = ds.Tables[0];
            if (SchemeType == "SO")
            {
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
                    }
                    DataSet dsNav = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));
                    if (dsNav.Tables[0].Rows.Count > 0)
                    {
                        string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                        lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
                    }
                    CalculateCurrentholding(ds, out finalunits, out finalamt, dsNav.Tables[0].Rows[0][1].ToString());
                    lblUnitsVale.Text = Math.Round(finalunits, 2).ToString();
                    lblAmtVale.Text = Math.Round(finalamt, 2).ToString();
                    txtAmtVale.Text = Math.Round(finalamt, 2).ToString();
                    lblFolioNo.Text = ddlFolio.SelectedItem.Text;

                }
            }
            else
            {
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
                            lblMinAmntVale.Text = dr["MinAmt"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString()))
                        {
                            lblSqnAmtVale.Text = dr["MultiAmt"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString()))
                        {
                            lblCuffTimeVale.Text = dr["CutOffTime"].ToString();
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
                    DataSet dsNav = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));
                    if (dsNav.Tables[0].Rows.Count > 0)
                    {
                        string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                        lblNavVale.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
                    }
                }

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
        private void BindFolioNumber(int amcCode, string schemeType)
        {

            try
            {


                DataTable dtGetAmcFolioNo = new DataTable();
                dtGetAmcFolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId,0);
                if (schemeType == "SO")
                {
                    if (dtGetAmcFolioNo.Rows.Count > 0)
                    {
                        ddlFolio.Items.Clear();
                        ddlFolio.DataSource = dtGetAmcFolioNo;
                        ddlFolio.DataTextField = dtGetAmcFolioNo.Columns["CMFA_FolioNum"].ToString();
                        ddlFolio.DataValueField = dtGetAmcFolioNo.Columns["CMFA_AccountId"].ToString();
                        ddlFolio.DataBind();
                        ddlFolio.Items.Insert(0, new ListItem("Select", "0"));


                    }
                }
                else
                {
                    ddlSwitchFolio.DataSource = dtGetAmcFolioNo;
                    ddlSwitchFolio.DataTextField = dtGetAmcFolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlSwitchFolio.DataValueField = dtGetAmcFolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlSwitchFolio.DataBind();
                    ddlSwitchFolio.Items.Insert(0, new ListItem("Select", "0"));


                }




            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        protected void SetControlDetails()
        {

        }
        protected void ddlSchemeName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeName.SelectedIndex != -1)
            {
                BindSchemeDividendTypes(int.Parse(ddlSchemeName.SelectedValue));
                //GetControlDetails(int.Parse(ddlSchemeName.SelectedValue), null, "SI");
                BindSipUiOnSchemeSelection(int.Parse(ddlSchemeName.SelectedValue));
            }
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
                btnSubmit.Enabled = true;
                ddlAmc.Enabled = true;
                ddlCategory.Enabled = true;
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
            //onlinemforderVo = onlineMforderBo.GetOrderDetails(ID);


        }
        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnkFactSheet.PostBackUrl))
                Response.Write(@"<script language='javascript'>alert('The URL is not valid');</script>");
        }
        protected void SwitchSchemeCategoryBind()
        {


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
            ddlSwtchShmCat.Items.Clear();
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlSwtchShmCat.DataSource = dsCategory.Tables[0];
                ddlSwtchShmCat.DataValueField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlSwtchShmCat.DataTextField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlSwtchShmCat.DataBind();
                ddlSwtchShmCat.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void BindSchemeDividendTypes(int schemeId)
        {
            DataTable dtSchemeDividendOption = commonLookupBo.GetMFSchemeDividentType(schemeId);
            ddlSwitchDvdnType.Items.Clear();
            if (dtSchemeDividendOption.Rows.Count > 0)
            {
                ddlSwitchDvdnType.DataSource = dtSchemeDividendOption;
                ddlSwitchDvdnType.DataValueField = dtSchemeDividendOption.Columns["PSLV_LookupValueCode"].ToString();
                ddlSwitchDvdnType.DataTextField = dtSchemeDividendOption.Columns["PSLV_LookupValue"].ToString();
                ddlSwitchDvdnType.DataBind();
                ddlSwitchDvdnType.Items.Insert(0, new ListItem("--SELECT--", "0"));

            }

        }

        protected void SchemeBind(int amccode, string category, int customerId, char schemeType)
        {
            ddlScheme.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerId, schemeType);
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
            CreatePurchaseOrderType();
        }

        private void CreatePurchaseOrderType()
        {
            OnlineMFOrderVo[] onlinemforderVo = new OnlineMFOrderVo[2];
            List<OnlineMFOrderVo> lsonlinemforder = new List<OnlineMFOrderVo>();
            onlinemforderVo[0] = new OnlineMFOrderVo();
            onlinemforderVo[0].SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            onlinemforderVo[0].AccountId = Int32.Parse(ddlFolio.SelectedValue.ToString());

            List<int> OrderIds = new List<int>();

            if (!string.IsNullOrEmpty(lblAmtVale.Text.ToString()))
            {
                onlinemforderVo[0].Amount = double.Parse(txtSwitchAmnt.Text.ToString());
            }
            else
            {
                onlinemforderVo[0].Amount = 0.0;
            }
            onlinemforderVo[0].TransactionType = "SO";
            lsonlinemforder.Add(onlinemforderVo[0]);
            onlinemforderVo[1] = new OnlineMFOrderVo();
            onlinemforderVo[1].AccountId = int.Parse(ddlSwitchFolio.SelectedValue.ToString());
            onlinemforderVo[1].SchemePlanCode = int.Parse(ddlSchemeName.SelectedValue.ToString());
            onlinemforderVo[1].Amount = int.Parse(txtSwitchAmnt.Text.ToString());
            onlinemforderVo[1].DivOption = ddlSwitchDvdnType.SelectedValue.ToString();
            onlinemforderVo[1].TransactionType = "SI";
            string message = string.Empty;
            lsonlinemforder.Add(onlinemforderVo[1]);
            OrderIds = onlineMforderBo.CreateOnlineMFSwitchOrderDetails(lsonlinemforder, userVo.UserId, customerVo.CustomerId);
            OrderId = int.Parse(OrderIds[0].ToString());
            char msgType = 's';
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


        protected void ddlDivType_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlSchemeType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeType.SelectedIndex != 0)
            {
                if (ddlSchemeType.SelectedValue.ToString() == "N")
                {
                    BindSwitchScheme(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId, char.Parse(ddlSchemeType.SelectedValue), 'P', 0);

                }
                else
                {
                    BindSwitchScheme(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId, char.Parse(ddlSchemeType.SelectedValue), 'P', int.Parse(ddlScheme.SelectedValue));
                }

                BindFolioNumber(int.Parse(ddlAmc.SelectedValue), "SI");


            }

        }
        protected void BindSwitchScheme(int amcCode, string category, int customerId, char switchType, char txnType, int schemePlanCode)
        {
            ddlSchemeName.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetSwitchAmcSchemeList(amcCode, category, customerId, switchType, txnType, schemePlanCode);
            if (dtScheme.Rows.Count > 0)
            {
                ddlSchemeName.DataSource = dtScheme;
                ddlSchemeName.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlSchemeName.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlSchemeName.DataBind();
                ddlSchemeName.Items.Insert(0, new ListItem("Select", "0"));
            }
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
                lblAvailableLimits.Text = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderPurchaseTransType')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderPurchaseTransType')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }

        protected void BindFrequency()
        {
            ddlFrequency.Items.Clear();
            if (dtGetAllSIPDataForOrder == null) return;

            foreach (DataRow row in dtGetAllSIPDataForOrder.Rows)
            {
                if (row["PASP_SchemePlanCode"].ToString() == ddlSchemeName.SelectedValue.ToString())
                {
                    ddlFrequency.Items.Add(new ListItem(row["XF_Frequency"].ToString(), row["XF_FrequencyCode"].ToString()));
                }
            }

            ddlFrequency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));

            //ddlFrequency.Items.Insert(0, new ListItem(""));

            ddlFrequency.SelectedIndex = 0;
        }

        protected void BindSipUiOnSchemeSelection(int schemeCode)
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(schemeCode, ddlFrequency.SelectedValue.ToString(), "STP",1);
            SetLatestNav(schemeCode);
            BindFrequency();
            BindAllControlsWithSIPData();

        }

        protected void SetLatestNav(int schemeCode)
        {
            float latNav = 0;

            DataSet ds = commonLookupBo.GetLatestNav(schemeCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                latNav = float.Parse(ds.Tables[0].Rows[0][1].ToString());
                string strDateForNAV = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                lblNavVale.Text = latNav + " " + "As On " + strDateForNAV;
            }

        }

        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0)
            {
                lblMinAmntVale.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString()), 2).ToString();
                lblSqnAmtVale.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString()), 2).ToString();
                lblCuffTimeVale.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                //ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
                lblEndDateDisplay.Text = string.Empty;

                //if (!string.IsNullOrEmpty(dtGetAllSIPDataForOrder.Rows[0]["AVSD_ExpiryDtae"].ToString()) && Convert.ToDateTime(dtGetAllSIPDataForOrder.Rows[0]["AVSD_ExpiryDtae"].ToString()) > DateTime.Now && !string.IsNullOrEmpty(dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString()))
                //{
                //    trSchemeRating.Visible = true;
                //    imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString() + ".png";
                //    //imgSchemeRating.ImageUrl = @"../Images/msgUnRead.png";
                //    imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + dtGetAllSIPDataForOrder.Rows[0]["PMFRD_RatingOverall"].ToString() + ".png";
                //}
                //else
                //{
                //    trSchemeRating.Visible = false;

                //}

            }
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dpendent controls
            ddlStartDate.SelectedIndex = 0;
            ddlTotalInstallments.SelectedIndex = 0;

            if (ddlFrequency.SelectedIndex == 0) return;

            BindStartDates(int.Parse(ddlScheme.SelectedValue.ToString()), ddlFrequency.SelectedValue.ToString());
            BindTotalInstallments();
            ShowHideControlsForDivAndGrowth();
            //BindSipDetOnFreqSel(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }

        protected void BindStartDates(int schemePlanCode, string frequency)
        {
            ddlStartDate.Items.Clear();

            DateTime[] dtStartdates;

            dtStartdates = onlineMforderBo.GetSipStartDates(schemePlanCode, frequency);

            foreach (DateTime d in dtStartdates) ddlStartDate.Items.Add(new ListItem(d.ToString("dd-MMM-yyyy")));
            ddlStartDate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlStartDate.Items.Insert(0, new ListItem("--SELECT--"));
            ddlStartDate.SelectedIndex = 0;
        }
        protected void BindTotalInstallments()
        {
            ddlTotalInstallments.Items.Clear();

            if (dtGetAllSIPDataForOrder == null) dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlSchemeName.SelectedValue), ddlFrequency.SelectedValue.ToString(), "STP",1);
            if (dtGetAllSIPDataForOrder == null) return;

            int minDues;
            int maxDues;
            minDues = Convert.ToInt16(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinDues"].ToString());
            maxDues = Convert.ToInt16(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MaxDues"].ToString());

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

        protected void ShowHideControlsForDivAndGrowth()
        {
            DataView dvFilterDivNGrowth = new DataView(dtGetAllSIPDataForOrder, "PASP_SchemePlanCode='" + ddlSchemeName.SelectedValue + "'", "PSLV_LookupValueCodeForSchemeOption", DataViewRowState.CurrentRows);

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
        protected void ddlTotalInstallments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTotalInstallments.SelectedIndex == 0 || ddlStartDate.SelectedIndex == 0 || ddlFrequency.SelectedIndex == 0) return;

            DateTime dtEndDate = onlineMforderBo.GetSipEndDate(Convert.ToDateTime(ddlStartDate.SelectedValue), ddlFrequency.SelectedValue, Convert.ToInt32(ddlTotalInstallments.SelectedValue) - 1);
            lblEndDateDisplay.Text = dtEndDate.ToString("dd-MMM-yyyy");
        }

    }
}