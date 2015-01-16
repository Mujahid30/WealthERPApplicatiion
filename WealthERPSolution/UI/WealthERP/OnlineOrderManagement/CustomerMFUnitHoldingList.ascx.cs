using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Globalization;
using VoCustomerPortfolio;
using System.Drawing;
namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerMFUnitHoldingList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
        List<MFPortfolioNetPositionVo> OnlineMFHoldingList = null;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        string userType;
        int customerId = 0;
        int portfolioId = 0;
        int accountId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            userVo = (UserVo)Session["userVo"];
            customerId = customerVO.CustomerId;
            BindFolioAccount();
            BindLink();
            if (!IsPostBack)
                Cache.Remove("UnitHolding" + userVo.UserId);
        }
        protected void BindLink()
        {
            if (Request.QueryString["folionum"] != null && Request.QueryString["SchemePlanCode"] != null && Request.QueryString["accountddl"] != null)
            {
                int accountId = int.Parse(Request.QueryString["folionum"].ToString());
                int SchemePlanCode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());
                int accountddl = int.Parse(Request.QueryString["accountddl"].ToString());
                hdnAccount.Value = accountddl.ToString();
                BindUnitHolding();
                ViewState["SchemePlanCode"] = SchemePlanCode;
                ddlPortfolio.SelectedValue = accountddl.ToString();
            }
        }


        private void SetParameter()
        {
            if (ddlPortfolio.SelectedIndex != 0)
            {
                hdnAccount.Value = ddlPortfolio.SelectedValue;
                ViewState["AccountDropDown"] = hdnAccount.Value;
            }
            else
            {
                hdnAccount.Value = "0";
            }
        }
        private void BindPortfolioDropDown()
        {
            //DataSet ds = portfolioBo.GetCustomerPortfolio(customerVO.CustomerId);
            //ddlPortfolio.DataSource = ds;
            //ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            //ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            //ddlPortfolio.DataBind();
            //ddlPortfolio.SelectedValue = portfolioId.ToString();
        }
        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
            DataSet dsFolioAccount;
            DataTable dtFolioAccount;
            dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
            dtFolioAccount = dsFolioAccount.Tables[0];
            if (dtFolioAccount.Rows.Count > 0)
            {
                ddlPortfolio.DataSource = dsFolioAccount.Tables[0];
                ddlPortfolio.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
                ddlPortfolio.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
                ddlPortfolio.DataBind();
                // ViewState["Account"] = ddlPortfolio.SelectedValue;
            }
            ddlPortfolio.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void btnUnitHolding_Click(object sender, EventArgs e)
        {
            //portfolioId = Convert.ToInt32(ddlPortfolio.SelectedValue);
            SetParameter();
            BindUnitHolding();

        }
        public DataTable CreateUnitHoldingListTable()
        {
            DataTable dtMFUnitHolding = new DataTable();
            dtMFUnitHolding.Columns.Add("MFNPId");
            dtMFUnitHolding.Columns.Add("AccountId");
            dtMFUnitHolding.Columns.Add("Category");
            dtMFUnitHolding.Columns.Add("Scheme");
            dtMFUnitHolding.Columns.Add("FolioNum");
            dtMFUnitHolding.Columns.Add("PurchasedUnits", typeof(double));
            dtMFUnitHolding.Columns.Add("DVRUnits", typeof(double));
            dtMFUnitHolding.Columns.Add("OpenUnits", typeof(double));
            dtMFUnitHolding.Columns.Add("Price");
            dtMFUnitHolding.Columns.Add("InvestedCost", typeof(double));
            dtMFUnitHolding.Columns.Add("NAV", typeof(double));
            dtMFUnitHolding.Columns.Add("CurrentValue", typeof(double));
            dtMFUnitHolding.Columns.Add("UnitsSold", typeof(double));
            dtMFUnitHolding.Columns.Add("RedeemedAmount", typeof(double));
            dtMFUnitHolding.Columns.Add("DVP", typeof(double));
            dtMFUnitHolding.Columns.Add("TotalPL", typeof(double));
            dtMFUnitHolding.Columns.Add("AbsoluteReturn", typeof(double));
            dtMFUnitHolding.Columns.Add("DVR", typeof(double));
            dtMFUnitHolding.Columns.Add("XIRR", typeof(double));
            dtMFUnitHolding.Columns.Add("TotalDividends", typeof(double));
            dtMFUnitHolding.Columns.Add("AMCCode");
            dtMFUnitHolding.Columns.Add("SchemeCode");
            dtMFUnitHolding.Columns.Add("AmcName");
            dtMFUnitHolding.Columns.Add("SubCategoryName");
            dtMFUnitHolding.Columns.Add("FolioStartDate");
            dtMFUnitHolding.Columns.Add("InvestmentStartDate");
            dtMFUnitHolding.Columns.Add("CMFNP_NAVDate");
            dtMFUnitHolding.Columns.Add("CMFNP_ValuationDate");
            dtMFUnitHolding.Columns.Add("RealizesdGain");
            dtMFUnitHolding.Columns.Add("IsSchemeSIPType");
            dtMFUnitHolding.Columns.Add("IsSchemePurchege");
            dtMFUnitHolding.Columns.Add("IsSchemeRedeem");
       

            dtMFUnitHolding.Columns.Add("SchemeRating3Year");
            dtMFUnitHolding.Columns.Add("SchemeRating5Year");
            dtMFUnitHolding.Columns.Add("SchemeRating10Year");

            dtMFUnitHolding.Columns.Add("SchemeReturn3Year");
            dtMFUnitHolding.Columns.Add("SchemeReturn5Year");
            dtMFUnitHolding.Columns.Add("SchemeReturn10Year");

            dtMFUnitHolding.Columns.Add("SchemeRisk3Year");
            dtMFUnitHolding.Columns.Add("SchemeRisk5Year");
            dtMFUnitHolding.Columns.Add("SchemeRisk10Year");

            dtMFUnitHolding.Columns.Add("SchemeRatingOverall");
            dtMFUnitHolding.Columns.Add("SchemeRatingSubscriptionExpiryDtae");

            return dtMFUnitHolding;
        }

        /// <summary>
        /// Get Unit Holding Data for Customer
        /// </summary>
        protected void BindUnitHolding()
        {
            DataTable dt = new DataTable();
            //hdnAccount.Value = accountId.ToString();
            OnlineMFHoldingList = customerPortfolioBo.GetOnlineUnitHolding(customerId, int.Parse(hdnAccount.Value));
            if (OnlineMFHoldingList != null)
            {
                DataTable dtMFUnitHoplding = CreateUnitHoldingListTable();                

                DataRow drMFUnitHoplding;
                for (int i = 0; i < OnlineMFHoldingList.Count; i++)
                {
                    drMFUnitHoplding = dtMFUnitHoplding.NewRow();
                    MFPortfolioNetPositionVo mfPortfolioVo;
                    mfPortfolioVo = OnlineMFHoldingList[i];
                    drMFUnitHoplding["MFNPId"] = mfPortfolioVo.MFPortfolioId;
                    drMFUnitHoplding["AccountId"] = mfPortfolioVo.AccountId;
                    drMFUnitHoplding["Category"] = mfPortfolioVo.AssetInstrumentCategoryName;
                    drMFUnitHoplding["Scheme"] = mfPortfolioVo.SchemePlan;
                    drMFUnitHoplding["FolioNum"] = mfPortfolioVo.FolioNumber;
                    if (mfPortfolioVo.ReturnsHoldPurchaseUnit != 0)
                        drMFUnitHoplding["PurchasedUnits"] = mfPortfolioVo.ReturnsHoldPurchaseUnit.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["PurchasedUnits"] = "0.00";
                    if (mfPortfolioVo.ReturnsHoldDVRUnits != 0)
                        drMFUnitHoplding["DVRUnits"] = mfPortfolioVo.ReturnsHoldDVRUnits.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["DVRUnits"] = "0.00";
                    if (mfPortfolioVo.NetHoldings != 0)
                        drMFUnitHoplding["OpenUnits"] = mfPortfolioVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["OpenUnits"] = "0.00";
                    if (mfPortfolioVo.ReturnsAllPrice != 0)
                        drMFUnitHoplding["Price"] = mfPortfolioVo.ReturnsAllPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["Price"] = "N/A";
                    if (mfPortfolioVo.InvestedCost != 0)
                        drMFUnitHoplding["InvestedCost"] = mfPortfolioVo.InvestedCost.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["InvestedCost"] = "0.00";
                    if (mfPortfolioVo.MarketPrice != 0)
                        drMFUnitHoplding["NAV"] = mfPortfolioVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["NAV"] = "0.00";
                    if (mfPortfolioVo.CurrentValue != 0)
                        drMFUnitHoplding["CurrentValue"] = mfPortfolioVo.CurrentValue.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["CurrentValue"] = "0.00";
                    if (mfPortfolioVo.SalesQuantity != 0)
                        drMFUnitHoplding["UnitsSold"] = mfPortfolioVo.SalesQuantity.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["UnitsSold"] = "0.00";
                    if (mfPortfolioVo.RedeemedAmount != 0)
                        drMFUnitHoplding["RedeemedAmount"] = mfPortfolioVo.RedeemedAmount.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["RedeemedAmount"] = "0.00";
                    if (mfPortfolioVo.ReturnsAllDVPAmt != 0)
                        drMFUnitHoplding["DVP"] = mfPortfolioVo.ReturnsAllDVPAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["DVP"] = "0.00";
                    if (mfPortfolioVo.ReturnsHoldTotalPL != 0)
                        drMFUnitHoplding["TotalPL"] = mfPortfolioVo.ReturnsHoldTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["TotalPL"] = "0.00";
                    if (mfPortfolioVo.ReturnsHoldAbsReturn != 0)
                        drMFUnitHoplding["AbsoluteReturn"] = mfPortfolioVo.ReturnsHoldAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["AbsoluteReturn"] = "0.00";
                    if (mfPortfolioVo.ReturnsAllDVRAmt != 0)
                        drMFUnitHoplding["DVR"] = mfPortfolioVo.ReturnsAllDVRAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["DVR"] = "0";
                    if (mfPortfolioVo.ReturnsAllTotalXIRR != 0)
                        drMFUnitHoplding["XIRR"] = mfPortfolioVo.ReturnsAllTotalXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["XIRR"] = "0.00";
                    if (mfPortfolioVo.ReturnsAllTotalDividends != 0)
                        drMFUnitHoplding["TotalDividends"] = mfPortfolioVo.ReturnsAllTotalDividends.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["TotalDividends"] = "0.00";
                    drMFUnitHoplding["AMCCode"] = mfPortfolioVo.AMCCode;
                    drMFUnitHoplding["AmcName"] = mfPortfolioVo.AmcName;
                    drMFUnitHoplding["SchemeCode"] = mfPortfolioVo.SchemePlanCode;
                    drMFUnitHoplding["SubCategoryName"] = mfPortfolioVo.AssetInstrumentSubCategoryName;
                    if (mfPortfolioVo.FolioStartDate == DateTime.MinValue)
                        drMFUnitHoplding["FolioStartDate"] = "N/A";
                    else
                        drMFUnitHoplding["FolioStartDate"] = mfPortfolioVo.FolioStartDate.ToShortDateString();
                    if (mfPortfolioVo.InvestmentStartDate == DateTime.MinValue)
                        drMFUnitHoplding["InvestmentStartDate"] = "N/A";
                    else
                        drMFUnitHoplding["InvestmentStartDate"] = mfPortfolioVo.InvestmentStartDate.ToShortDateString();
                    if (mfPortfolioVo.NavDate == DateTime.MinValue)
                        drMFUnitHoplding["CMFNP_NAVDate"] = "N/A";
                    else
                        drMFUnitHoplding["CMFNP_NAVDate"] = mfPortfolioVo.NavDate.ToShortDateString();
                    drMFUnitHoplding["CMFNP_ValuationDate"] = mfPortfolioVo.ValuationDate.ToShortDateString();
                    if (mfPortfolioVo.ReturnsRealizedTotalPL != 0)
                        drMFUnitHoplding["RealizesdGain"] = mfPortfolioVo.ReturnsRealizedTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["RealizesdGain"] = "0";

                    drMFUnitHoplding["IsSchemeSIPType"] = mfPortfolioVo.IsSchemeSIPType;
                    drMFUnitHoplding["IsSchemePurchege"] = mfPortfolioVo.IsSchemePurchege;
                    drMFUnitHoplding["IsSchemeRedeem"] = mfPortfolioVo.IsSchemeRedeem;

                    drMFUnitHoplding["SchemeRating3Year"] = mfPortfolioVo.SchemeRating3Year;
                    drMFUnitHoplding["SchemeRating5Year"] = mfPortfolioVo.SchemeRating5Year;
                    drMFUnitHoplding["SchemeRating10Year"] = mfPortfolioVo.SchemeRating10Year;

                    drMFUnitHoplding["SchemeReturn3Year"] = mfPortfolioVo.SchemeReturn3Year;
                    drMFUnitHoplding["SchemeReturn5Year"] = mfPortfolioVo.SchemeReturn5Year;
                    drMFUnitHoplding["SchemeReturn10Year"] = mfPortfolioVo.SchemeReturn10Year;

                    drMFUnitHoplding["SchemeRisk3Year"] = mfPortfolioVo.SchemeRisk3Year;
                    drMFUnitHoplding["SchemeRisk5Year"] = mfPortfolioVo.SchemeRisk5Year;
                    drMFUnitHoplding["SchemeRisk10Year"] = mfPortfolioVo.SchemeRisk10Year;

                    drMFUnitHoplding["SchemeRatingOverall"] = mfPortfolioVo.SchemeRatingOverall;
                    drMFUnitHoplding["SchemeRatingSubscriptionExpiryDtae"] = mfPortfolioVo.SchemeRatingSubscriptionExpiryDtae;


                    dtMFUnitHoplding.Rows.Add(drMFUnitHoplding);
                }
                if (dtMFUnitHoplding.Rows.Count > 0)
                {
                    if (Cache["UnitHolding" + userVo.UserId] == null)
                    {
                        Cache.Insert("UnitHolding" + userVo.UserId, dtMFUnitHoplding);
                    }
                    else
                    {
                        Cache.Remove("UnitHolding" + userVo.UserId);
                        Cache.Insert("UnitHolding" + userVo.UserId, dtMFUnitHoplding);
                    }
                    rgUnitHolding.DataSource = dtMFUnitHoplding;
                    rgUnitHolding.DataBind();
                    rgUnitHolding.Visible = true;
                    pnlMFUnitHolding.Visible = true;
                    btnExport.Visible = true;
                    trNoRecords.Visible = false;
                }
                else
                {
                    rgUnitHolding.DataSource = dtMFUnitHoplding;
                    rgUnitHolding.DataBind();
                    //rgUnitHolding.Visible = false;
                    pnlMFUnitHolding.Visible = true;
                    btnExport.Visible = false;
                    trNoRecords.Visible = false;

                }

            }
            else
            {
                DataTable dtUnitNoRecord = CreateUnitHoldingListTable();
                rgUnitHolding.DataSource = dtUnitNoRecord;
                rgUnitHolding.DataBind();
                //rgUnitHolding.Visible = false;
                pnlMFUnitHolding.Visible = true;
                btnExport.Visible = false;
                trNoRecords.Visible = false;
                //lblNoRecords.Text = "No Records Found";

            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;
            string accountId = rgUnitHolding.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString();
            string schemePlanCode = rgUnitHolding.MasterTableView.DataKeyValues[selectedRow - 1]["SchemeCode"].ToString();

            if (ddlAction.SelectedItem.Value.ToString() == "ABY")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderAdditionalPurchase','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
            }
            else if (ddlAction.SelectedItem.Value.ToString() == "SIP")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderRdemptionTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
            }

        }
        protected void rgUnitHolding_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtUnitHolding = new DataTable();
            dtUnitHolding = (DataTable)Cache["UnitHolding" + userVo.UserId.ToString()];
            if (dtUnitHolding != null)
            {
                rgUnitHolding.DataSource = dtUnitHolding;
                rgUnitHolding.Visible = true;
            }
        }
        protected void rgUnitHolding_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            // GridDataItem gvr = (GridDataItem)e.Item;
            string accountId = string.Empty;
            string schemePlanCode = string.Empty;

            if (e.CommandName.ToString() != "Filter")
            {
                if (e.CommandName.ToString() != "Sort")
                {
                    if (e.CommandName.ToString() != "Page")
                    {
                        if (e.CommandName.ToString() != "ChangePageSize")
                        { 
                            GridDataItem gvr = (GridDataItem)e.Item;
                            int selectedRow = gvr.ItemIndex + 1;
                            int folio = int.Parse(gvr.GetDataKeyValue("AccountId").ToString());
                            int SchemePlanCode = int.Parse(gvr.GetDataKeyValue("SchemeCode").ToString());
                            int accountddl = Convert.ToInt32(ViewState["AccountDropDown"]);
                            int amcCode = int.Parse(gvr.GetDataKeyValue("AMCCode").ToString());
                            if (e.CommandName == "SelectTransaction")
                            {
                                if (Session["PageDefaultSetting"] != null)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerTransactionBookList','?folionum=" + folio + "&SchemePlanCode=" + SchemePlanCode + "&accountddl=" + accountddl + "&AMCCode=" + amcCode + "');", true);
                                }
                                else
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=CustomerTransactionBookList&folionum=" + folio + "&SchemePlanCode=" + SchemePlanCode + "&accountddl=" + accountddl + "&AMCCode=" + amcCode + "", false);
                                }
                            }

                        }
                    }
                }
            }
            if (e.CommandName == "Buy" || e.CommandName == "SIP" || e.CommandName == "Sell")
            {
                accountId = rgUnitHolding.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AccountId"].ToString();
                schemePlanCode = rgUnitHolding.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SchemeCode"].ToString();
            }
            if (e.CommandName == "Buy")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderAdditionalPurchase','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }

            }
            if (e.CommandName == "SIP")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }
            }
            if (e.CommandName == "Sell")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderRdemptionTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderRdemptionTransType','&accountId=" + accountId + "&SchemeCode=" + schemePlanCode + "')", true);
                }
            }
        }

        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            rgUnitHolding.ExportSettings.OpenInNewWindow = true;
            rgUnitHolding.ExportSettings.IgnorePaging = true;
            rgUnitHolding.ExportSettings.HideStructureColumns = true;
            rgUnitHolding.ExportSettings.ExportOnlyData = true;
            rgUnitHolding.ExportSettings.FileName = "Unit Holding Details";
            rgUnitHolding.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgUnitHolding.MasterTableView.ExportToExcel();
        }

        public void rgUnitHolding_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Label lblISRedeemFlag = (Label)e.Item.FindControl("lblISRedeemFlag");

                Label lblSIPSchemeFlag = (Label)e.Item.FindControl("lblSIPSchemeFlag");
                Label lblIsPurcheseFlag = (Label)e.Item.FindControl("lblIsPurcheseFlag");
                Label lblSchemeRating = (Label)e.Item.FindControl("lblSchemeRating");

                Label lblRating3Year = (Label)e.Item.FindControl("lblRating3Year");
                Label lblRating5Year = (Label)e.Item.FindControl("lblRating5Year");
                Label lblRating10Year = (Label)e.Item.FindControl("lblRating10Year");

                System.Web.UI.WebControls.Image imgSchemeRating = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSchemeRating");

                System.Web.UI.WebControls.Image imgRating3Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating3yr");
                System.Web.UI.WebControls.Image imgRating5Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating5yr");
                System.Web.UI.WebControls.Image imgRating10Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating10yr");
                System.Web.UI.WebControls.Image imgRatingOvelAll = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRatingOvelAll");

                if (lblSIPSchemeFlag.Text.Trim().ToLower() == "false")
                {
                    ImageButton imgSIP = (ImageButton)e.Item.FindControl("imgSip");
                    imgSIP.Visible=false;
                   
                }
                if (lblIsPurcheseFlag.Text.Trim().ToLower() == "false")
                {
                    ImageButton imgBuy = (ImageButton)e.Item.FindControl("imgBuy");
                    imgBuy.Visible = false;
                   
                }
                if (lblISRedeemFlag.Text.Trim().ToLower() == "false")
                {
                    ImageButton imgSell = (ImageButton)e.Item.FindControl("imgSell");
                    imgSell.Visible = false;
                  
                }
                lblISRedeemFlag.Visible = false;
                lblIsPurcheseFlag.Visible = false;
                lblSIPSchemeFlag.Visible = false;


                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblSchemeRating.Text.Trim() + ".png";

                imgRating3Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating3Year.Text.Trim() + ".png";
                imgRating5Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating5Year.Text.Trim() + ".png";
                imgRating10Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating10Year.Text.Trim() + ".png";

                imgRatingOvelAll.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";

                 //imgSchemeRating.ImageUrl = @"../Images/msgUnRead.png";
                ////imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";
             

            }

        }
    }
}
