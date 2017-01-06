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
        string clientMFAccessCode = string.Empty;
        string subcategory = string.Empty;
        int amcCode = 0;
        string category = string.Empty;
        string categoryname = string.Empty;
        string schemeName = string.Empty;
        string amcName = string.Empty;
        int scheme;
        string schemeDividendOption;
        string exchangeType = string.Empty;
        int IsRedeemAvaliable = 0;
        int IspurchaseAvaliable = 0;
        int IsSIPAvaliable = 0;
              int BackOfficeUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            RadInformation.VisibleOnPageLoad = false;
            rwTermsCondition.VisibleOnPageLoad = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (Session["BackOfficeUserId"].ToString() != null)
            {
                BackOfficeUserId = Convert.ToInt32(Session["BackOfficeUserId"]);
            }
            else
            {
                BackOfficeUserId = 0;
            }
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
                if (clientMFAccessCode == "FA" || clientMFAccessCode == "PA")
                {
                    AmcBind();
                    //CategoryBind();
                    BindNomineeAndJointHolders();
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(0, customerVo.CustomerId, exchangeType == "Online" ? 0 : 1);
                    lnkOfferDoc.Visible = false;
                    lnkFactSheet.Visible = false;
                    lnkExitLoad.Visible = false;
                    //txtRedeemTypeValue.Visible = false;
                    //lblOption.Visible = false;
                    //lblDividendType.Visible = false;
                    trDividendOption.Visible = false;
                    trRedeemType.Visible = true;
                    if ((Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null) || Session["MFSchemePlan"] != null)
                    {
                        int accountId = 0;
                        int schemeCode = 0;
                        int amcCode = 0;
                        string category = string.Empty;
                        if (Request.QueryString["accountId"] != null)
                        {
                            schemeCode = int.Parse(Session["MFSchemePlan"].ToString());
                            accountId = int.Parse(Request.QueryString["accountId"].ToString());
                            //commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);
                            commonLookupBo.GetSchemeAMCSchemeCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category, out categoryname, out amcName, out schemeName, out  IsSIPAvaliable, out  IspurchaseAvaliable, out  IsRedeemAvaliable, exchangeType == "Online" ? 1 : 0);
                            if (IsRedeemAvaliable != 1)
                            {
                                ShowMessage("Redeem is not available", 'I'); return;
                            }
                          

                            lblAmc.Text = amcName;
                            lblCategory.Text = categoryname;
                            lblScheme.Text = schemeName;
                            dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(amcCode, customerVo.CustomerId, exchangeType == "Online" ? 0 : 1);
                            SetSelectedDisplay(accountId, int.Parse(Session["MFSchemePlan"].ToString()), amcCode, category);
                        }
                        else
                        {

                            commonLookupBo.GetSchemeAMCSchemeCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category, out categoryname, out amcName, out schemeName, out  IsSIPAvaliable, out  IspurchaseAvaliable, out  IsRedeemAvaliable, exchangeType == "Online" ? 1 : 0);
                            if (IsRedeemAvaliable != 1)
                            {
                                ShowMessage("Redeem is not available", 'I'); return;

                            }

                            lblAmc.Text = amcName;
                            lblCategory.Text = categoryname;
                            lblScheme.Text = schemeName;

                            SetSelectedDisplay(0, int.Parse(Session["MFSchemePlan"].ToString()), amcCode, category);


                        }
                    }
                }
                else
                {
                    ShowMessage(onlineMforderBo.GetOnlineOrderUserMessage(clientMFAccessCode),'F');
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
                BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

            }
            else
            {
                PurchaseOrderControlsEnable(false);

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No existing Investment found');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('No existing Investment found','" + "I" + "');", true);

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptv2ewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + Session["MFSchemePlan"].ToString() + "')", true); 
                return;
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

        protected void ddlFolio_onSelectedChanged(object sender, EventArgs e)
        {
            if (ddlFolio.SelectedIndex != -1)
            {
                GetControlDetails(int.Parse(ddlScheme.SelectedValue), ddlFolio.SelectedValue.ToString());
            }

        }

        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";

            //lblMintxt.Text = "";
            //lblMulti.Text = "";
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
        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();
            double finalamt;
            double finalunits;
            ds = onlineMforderBo.GetControlDetails(scheme, folio, exchangeType == "Online" ? 1 : 0);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["PSLV_DividendType"].ToString()))
                    ddlDivType.SelectedValue = dr["PSLV_DividendType"].ToString();
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


                    if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    {
                        lbldftext.Text = dr["divFrequency"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["url"].ToString()))
                    {
                        lnkFactSheet.PostBackUrl = dr["url"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["PAISC_AssetInstrumentSubCategoryCode"].ToString()) && lblUnitsheldDisplay.Text != null)
                    {
                        subcategory = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
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
            if(exchangeType == "Demat")
                ddlRedeem.Items[2].Enabled = false;
            if (ds.Tables[4].Rows.Count > 0)
            {

                ViewState["BseCode"] = ds.Tables[4].Rows[0][0].ToString();
            }
            DataSet dsNav = commonLookupBo.GetLatestNav(scheme);
            if (dsNav.Tables[0].Rows.Count > 0)
            {
                string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
            }
            CalculateCurrentholding(ds, out finalunits, out finalamt, dsNav.Tables[0].Rows.Count > 0 ? Convert.ToString(dsNav.Tables[0].Rows[0][1]) : "0");
            lblUnitsheldDisplay.Text = Math.Round(finalunits, 2).ToString();
            lblCurrentValueDisplay.Text = Math.Round(finalamt, 2).ToString();

            if (subcategory == "MFEQTP")
            {
                ddlRedeem.Items[3].Enabled = false;
            }
            if ((double.Parse(lblUnitsheldDisplay.Text) <= 0) && (subcategory == "MFEQTP"))
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
                txtRedeemTypeValue.Enabled = true;
                chkTermsCondition.Enabled = false;
                lnkTermsCondition.Enabled = true;
                btnSubmit.Enabled = true;
                lblMsg.Visible = false;

            }
            else
            {
                ddlRedeem.Enabled = false;
                txtRedeemTypeValue.Enabled = false;
                chkTermsCondition.Enabled = false;
                lnkTermsCondition.Enabled = false;
                btnSubmit.Enabled = false;
                lblMsg.Visible = true;
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
            BindFolioNumber(SchemeCode);
            ddlFolio.SelectedValue = Accountid.ToString();
            ddlScheme.SelectedValue = SchemeCode.ToString();

            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlScheme.Enabled = false;
            GetControlDetails(SchemeCode, ddlFolio.SelectedValue);


        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;


            lblCurrentValueDisplay.Visible = true;
            lblUnitsheldDisplay.Visible = true;
            if (lblDividendType.Text == "Growth")
            {
                //lblDividendFrequency.Visible = false;
                //lbldftext.Visible = false;
                trDivfeq.Visible = false;
                //lblDivType.Visible = false;
                //ddlDivType.Visible = false;
                trDivtype.Visible = false;
                RequiredFieldValidator3.Enabled = false;


            }
            else
            {
                //lblDividendFrequency.Visible = true;
                //lbldftext.Visible = true;
                //lblDivType.Visible = true;
                //ddlDivType.Visible = true;
                //trDivfeq.Visible = true;
                // trDivtype.Visible = true;
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
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerid, 'R');
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
        private void GetNetpositionValues(int folio, int scheme)
        {

        }
        private void BindFolioNumber(int amcCode)
        {
            ddlFolio.Items.Clear();
            DataTable dtScheme = new DataTable();
            try
            {
                dtScheme = onlineMforderBo.GetCustomerFolioSchemeWise(customerVo.CustomerId, amcCode, exchangeType == "Online" ? 1 : 0);
                //commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);

                if (dtScheme.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtScheme;
                    ddlFolio.DataTextField = dtScheme.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtScheme.Columns["CMFA_AccountId"].ToString();
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
                cmpMinAmountUnits.ValueToCompare = lblMinUnitValue.Text;
                cmpMinAmountUnits.ErrorMessage = "Redemption Units entered should be greater than Minimum Redemption Units. " + lblMinUnitValue.Text;
            }
            else if (ddlRedeem.SelectedValue == "2")
            {
                lblRedeemType.Text = "Amount (Rs):";
                txtRedeemTypeValue.Text = null;
                txtRedeemTypeValue.Enabled = true;
                cmpMinAmountUnits.ValueToCompare = lblMinAmountValue.Text;
                cmpMinAmountUnits.ErrorMessage = "Redemption Amount entered should be greater than Minimum Redemption Amount (Rs): " + lblMinAmountValue.Text;

            }
            else if (ddlRedeem.SelectedValue == "3")
            {
                lblRedeemType.Text = "All Units:";
                txtRedeemTypeValue.Text = lblUnitsheldDisplay.Text;
                txtRedeemTypeValue.Enabled = false;
                cmpMinAmountUnits.IsValid = true;
            }
            //txtRedeemTypeValue.Visible = true;
            trRedeemType.Visible = true;
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
                ddlRedeem.Enabled = false;
                txtRedeemTypeValue.Enabled = false;

                btnSubmit.Visible = false;

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
                ddlRedeem.Enabled = true;

                btnSubmit.Visible = true;


            }

        }
        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            confirmMessage.Text = onlineMforderBo.GetOnlineOrderUserMessage("EUIN");
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            commonLookupBo.GetSchemeAMCSchemeCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category, out categoryname, out amcName, out schemeName, out  IsSIPAvaliable, out  IspurchaseAvaliable, out  IsRedeemAvaliable, exchangeType == "Online" ? 1 : 0);
            if (IsRedeemAvaliable == 1)
                CreateRedemptionOrderType();
               
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Redeem is not avaliable');", true); return;
            }
        }

        private void CreateRedemptionOrderType()
        {
            List<int> OrderIds = new List<int>();
            DateTime dtCutOfffTime;
            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            bool isCutOffTimeOver = false;
            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "SEL";
            dtCutOfffTime = DateTime.Parse(lbltime.Text);

            if (DateTime.Now.TimeOfDay > dtCutOfffTime.TimeOfDay && dtCutOfffTime.TimeOfDay < System.TimeSpan.Parse("23:59:59"))
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
                if (!string.IsNullOrEmpty(txtRedeemTypeValue.Text))
                    onlinemforderVo.Redeemunits = double.Parse(txtRedeemTypeValue.Text);
                else
                    onlinemforderVo.Redeemunits = 0;
                onlinemforderVo.IsAllUnits = true;
                //onlinemforderVo.Redeemunits = float.Parse(txtRedeemTypeValue.Text);

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
            
            string message = string.Empty;
            char msgType = 'F';
            if (exchangeType == "Online")
            {
                onlinemforderVo.OrderType = 1;
                OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, (BackOfficeUserId != 0) ? BackOfficeUserId : userVo.UserId, customerVo.CustomerId);
                OrderId = int.Parse(OrderIds[0].ToString());
                message=CreateUserMessage(OrderId, isCutOffTimeOver,out msgType);
               
            }
            else if (exchangeType == "Demat")
            {
                onlinemforderVo.OrderType = 0;
                DematAccountVo dematevo = new DematAccountVo();
                BoDematAccount bo = new BoDematAccount();
                dematevo = bo.GetCustomerActiveDematAccount(customerVo.CustomerId);
                onlinemforderVo.BSESchemeCode = ViewState["BseCode"].ToString(); 
                OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
                message = OnlineMFOrderBo.BSEorderEntryParam(userVo.UserId, customerVo.CustCode, onlinemforderVo, customerVo.CustomerId, dematevo.DepositoryName, out msgType);
            }
           
            PurchaseOrderControlsEnable(false);
            ShowMessage(message, msgType);

        }

        private string CreateUserMessage(int orderId, bool isCutOffTimeOver,out char msgType)
        {
            string userMessage = string.Empty;
            msgType = 'S';
            if (orderId != 0)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            else
                msgType = 'F';
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

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderRdemptionTransType')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderRdemptionTransType')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation.VisibleOnPageLoad = true;

        }

    }
}




















