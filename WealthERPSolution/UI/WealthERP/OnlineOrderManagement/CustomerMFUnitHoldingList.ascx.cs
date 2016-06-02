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
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerMFUnitHoldingList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
        List<MFPortfolioNetPositionVo> OnlineMFHoldingList = null;
        List<MFTransactionVo> mfTransactionList = null;
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        string userType;
        int customerId = 0;
        int portfolioId = 0;
        int accountId = 0;
        string exchangeType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            userVo = (UserVo)Session["userVo"];
            customerId = customerVO.CustomerId;
            if (Session["ExchangeMode"] != null)
                exchangeType = Session["ExchangeMode"].ToString();
            if (!IsPostBack)
            {
                Cache.Remove("UnitHolding" + userVo.UserId);
                BindFolioAccount();
                BindLink();
            }
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
            dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId, exchangeType == "Online" ? 1 : 0);
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
            dtMFUnitHolding.Columns.Add("Sold Value", typeof(double));
            dtMFUnitHolding.Columns.Add("DVP", typeof(double));
            dtMFUnitHolding.Columns.Add("Unrealised Gain/Loss", typeof(double));
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
            dtMFUnitHolding.Columns.Add("Realised Gain/Loss", typeof(double));
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
            dtMFUnitHolding.Columns.Add("SchemeRatingDate");
            dtMFUnitHolding.Columns.Add("Status");
            return dtMFUnitHolding;
        }

        /// <summary>
        /// Get Unit Holding Data for Customer
        /// </summary>
        protected void BindUnitHolding()
        {
            DataTable dt = new DataTable();
            //hdnAccount.Value = accountId.ToString();
            OnlineMFHoldingList = customerPortfolioBo.GetOnlineUnitHolding(customerId, int.Parse(hdnAccount.Value), exchangeType == "Online" ? 1 : 0);
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
                        drMFUnitHoplding["InvestedCost"] = mfPortfolioVo.InvestedCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["InvestedCost"] = "0.00";
                    if (mfPortfolioVo.MarketPrice != 0)
                        drMFUnitHoplding["NAV"] = mfPortfolioVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["NAV"] = "0.00";
                    if (mfPortfolioVo.CurrentValue != 0)
                        drMFUnitHoplding["CurrentValue"] = mfPortfolioVo.CurrentValue.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["CurrentValue"] = "0.00";
                    if (mfPortfolioVo.SalesQuantity != 0)
                        drMFUnitHoplding["UnitsSold"] = mfPortfolioVo.SalesQuantity.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["UnitsSold"] = "0.00";
                    if (mfPortfolioVo.RedeemedAmount != 0)
                        drMFUnitHoplding["Sold Value"] = mfPortfolioVo.RedeemedAmount.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["Sold Value"] = "0.00";
                    if (mfPortfolioVo.ReturnsAllDVPAmt != 0)
                        drMFUnitHoplding["DVP"] = mfPortfolioVo.ReturnsAllDVPAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["DVP"] = "0.00";
                    if (mfPortfolioVo.ReturnsHoldTotalPL != 0)
                        drMFUnitHoplding["Unrealised Gain/Loss"] = mfPortfolioVo.ReturnsHoldTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["Unrealised Gain/Loss"] = "0.00";
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
                        drMFUnitHoplding["Realised Gain/Loss"] = mfPortfolioVo.ReturnsRealizedTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    else
                        drMFUnitHoplding["Realised Gain/Loss"] = "0.00";

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
                    if (DateTime.Parse(mfPortfolioVo.SchemeRatingDate.ToString()) != DateTime.Parse("01/01/1900 00:00:00"))
                        drMFUnitHoplding["SchemeRatingDate"] = DateTime.Parse(mfPortfolioVo.SchemeRatingDate.ToString()).ToString("dd/MM/yyyy");
                    drMFUnitHoplding["Status"] = mfPortfolioVo.status;
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
                    var page = 0;
                    rgUnitHolding.CurrentPageIndex = page;
                    rgUnitHolding.DataSource = dtMFUnitHoplding;
                    rgUnitHolding.DataBind();
                    rgUnitHolding.Visible = true;
                    Div1.Visible = true;
                    btnExport.Visible = true;
                    trNoRecords.Visible = false;
                }
                else
                {
                    rgUnitHolding.DataSource = dtMFUnitHoplding;
                    rgUnitHolding.DataBind();
                    //rgUnitHolding.Visible = false;
                    Div1.Visible = true;
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
                Div1.Visible = true;
                btnExport.Visible = false;
                trNoRecords.Visible = false;
                //lblNoRecords.Text = "No Records Found";

            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            //RadGrid itm = (RadGrid)ddlAction.NamingContainer;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;
            string accountId = rgUnitHolding.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString();
            string schemePlanCode = rgUnitHolding.MasterTableView.DataKeyValues[selectedRow - 1]["SchemeCode"].ToString();
            for (int i = 0; i < rgUnitHolding.Items.Count; i++)
            {
                DropDownList DDLValue = rgUnitHolding.Items[i].FindControl("ddlAction") as DropDownList;
                string schemePlanCode1 = rgUnitHolding.MasterTableView.DataKeyValues[i]["SchemeCode"].ToString();
                if (schemePlanCode.Trim() != schemePlanCode1.Trim())
                {
                    DDLValue.SelectedValue = "0";
                }
            }
            switch (ddlAction.SelectedValue)
            {
                case "SIP":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
                    break;
                case "BUY":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
                    break;
                case "SEL":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderRdemptionTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
                    break;
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
                Div1.Visible = true;
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
                            RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
                            int selectedRow = gvr.ItemIndex + 1;
                            int folio = int.Parse(gvr.GetDataKeyValue("AccountId").ToString());
                            int SchemePlanCode = int.Parse(gvr.GetDataKeyValue("SchemeCode").ToString());
                            int accountddl = Convert.ToInt32(ViewState["AccountDropDown"]);
                            int amcCode = int.Parse(gvr.GetDataKeyValue("AMCCode").ToString());
                            if (e.CommandName == "SelectTransaction")
                            {
                                BindTransactionDetails(folio, SchemePlanCode, gvChildDetails);

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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
            }
            if (e.CommandName == "SIP")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
            }
            if (e.CommandName == "Sell")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderRdemptionTransType&accountId=" + accountId + "','" + schemePlanCode + "')", true);
            }
        }

        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {

            DataTable dtUnitHolding = new DataTable();
            dtUnitHolding = (DataTable)Cache["UnitHolding" + userVo.UserId.ToString()];
            System.Data.DataView view = new System.Data.DataView(dtUnitHolding);
            System.Data.DataTable selected =
                    view.ToTable("Selected", false, "Scheme", "FolioNum", "PurchasedUnits", "InvestedCost", "NAV", "Unrealised Gain/Loss", "CurrentValue", "UnitsSold", "Sold Value", "Realised Gain/Loss");
            if (selected.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerHoldings.xls"));
                Response.ContentType = "application/ms-excel";

                string str = string.Empty;

                foreach (DataColumn dtcol in selected.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";

                }
                Response.Write("\n");
                foreach (DataRow dr in selected.Rows)
                {
                    str = "";
                    for (int j = 0; j < selected.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

            }
            //if (selected.Rows.Count > 0)
            //{

            //    string data = null;
            //    int i = 0;
            //    int j = 0;

            //    Excel.Workbook xlWorkBook;
            //    Excel.Worksheet xlWorkSheet;
            //    object misValue = System.Reflection.Missing.Value;

            //    Excel.Application xlApp = new Excel.Application();
            //    xlWorkBook = xlApp.Workbooks.Add(misValue);
            //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //    int z = 0;
            //    foreach (DataColumn dtcol in selected.Columns)
            //    {
                    
            //        data = dtcol.ColumnName;
            //        xlWorkSheet.Cells[1, z + 1] = data; z++;
            //    }

            //    for (i = 1; i <= selected.Rows.Count; i++)
            //    {
            //        for (j = 0; j <= selected.Columns.Count - 1; j++)
            //        {
            //            data = selected.Rows[i - 1].ItemArray[j].ToString();
            //            xlWorkSheet.Cells[i + 1, j + 1] = data;
            //        }
            //    }
            //    Random asa = new Random();
            //    string filename = "CustomerHolding" + asa.Next() + ".xls";
            //    xlWorkBook.SaveAs(Server.MapPath("~/UploadFiles/"+ filename), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //    xlWorkBook.Close(true, misValue, misValue);
            //    xlApp.Quit();
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "setFormat('" + filename + "');", true);
            //}
           

        }

        public void rgUnitHolding_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                MFPortfolioNetPositionVo mfPortfolioVo = new MFPortfolioNetPositionVo();
                Label lblISRedeemFlag = (Label)e.Item.FindControl("lblISRedeemFlag");
                DropDownList ddlAction = (DropDownList)e.Item.FindControl("ddlAction");
                Label lblSIPSchemeFlag = (Label)e.Item.FindControl("lblSIPSchemeFlag");
                Label lblIsPurcheseFlag = (Label)e.Item.FindControl("lblIsPurcheseFlag");
                Label lblSchemeRating = (Label)e.Item.FindControl("lblSchemeRating");

                Label lblRating3Year = (Label)e.Item.FindControl("lblRating3Year");
                Label lblRating5Year = (Label)e.Item.FindControl("lblRating5Year");
                Label lblRating10Year = (Label)e.Item.FindControl("lblRating10Year");
                Label lblRatingAsOnPopUp = (Label)e.Item.FindControl("lblRatingAsOnPopUp");

                System.Web.UI.WebControls.Image imgSchemeRating = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSchemeRating");

                System.Web.UI.WebControls.Image imgRating3Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating3yr");
                System.Web.UI.WebControls.Image imgRating5Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating5yr");
                System.Web.UI.WebControls.Image imgRating10Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating10yr");
                System.Web.UI.WebControls.Image imgRatingOvelAll = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRatingOvelAll");

                string status = rgUnitHolding.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Status"].ToString();
                if (lblSIPSchemeFlag.Text.Trim().ToLower() == "true" && status=="Active")
                {
                //    ddlAction.Items[1].Enabled = false;
                //}
                //else
                //{
                    ddlAction.Items.FindByText("SIP").Enabled = true;

                    //ddlAction.Items[1].Enabled = true;
                }
                if (lblIsPurcheseFlag.Text.Trim().ToLower() == "true" && status == "Active")
                {
                //    ddlAction.Items[2].Enabled = false;
                //}
                //else
                //{
                    ddlAction.Items.FindByText("Purchase").Enabled = true;
                    //ddlAction.Items[2].Enabled = true;
                }
                if (lblISRedeemFlag.Text.Trim().ToLower() == "true" )
                {
                //    ddlAction.Items[3].Enabled = false;
                //}
                //else
                //{
                    ddlAction.Items.FindByText("Redeem").Enabled = true;

                    //ddlAction.Items[3].Enabled = true;
                }
                lblISRedeemFlag.Visible = false;
                lblIsPurcheseFlag.Visible = false;
                lblSIPSchemeFlag.Visible = false;


                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblSchemeRating.Text.Trim() + ".png";

                imgRating3Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating3Year.Text.Trim() + ".png";
                imgRating5Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating5Year.Text.Trim() + ".png";
                imgRating10Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating10Year.Text.Trim() + ".png";

                imgRatingOvelAll.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";
                //if (!string.IsNullOrEmpty(mfPortfolioVo.SchemeRatingDate))
                //    {
                //        lblRatingAsOnPopUp.Text =  "As On " + mfPortfolioVo.SchemeRatingDate;
                //    }

                //imgSchemeRating.ImageUrl = @"../Images/msgUnRead.png";
                ////imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";


            }
        }
        protected void BindTransactionDetails(int AccountId, int schemePlanCode, RadGrid gvChildDetails)
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            mfTransactionList = customerTransactionBo.GetCustomerTransactionsBook(advisorVo.advisorId, customerId, Convert.ToDateTime("01-01-1990"), DateTime.Now, 1, 0, AccountId, schemePlanCode);
            if (mfTransactionList.Count != 0)
            {


                DataTable dtMFTransactions = new DataTable();
                dtMFTransactions.Columns.Add("TransactionId");
                dtMFTransactions.Columns.Add("Customer Name");
                dtMFTransactions.Columns.Add("Folio Number");
                dtMFTransactions.Columns.Add("Scheme Name");
                dtMFTransactions.Columns.Add("Transaction Type");
                dtMFTransactions.Columns.Add("Transaction Date", typeof(DateTime));
                dtMFTransactions.Columns.Add("Price", typeof(double));
                dtMFTransactions.Columns.Add("Units", typeof(double));
                dtMFTransactions.Columns.Add("Amount", typeof(double));
                dtMFTransactions.Columns.Add("STT", typeof(double));
                dtMFTransactions.Columns.Add("Portfolio Name");
                dtMFTransactions.Columns.Add("TransactionStatus");
                dtMFTransactions.Columns.Add("Category");
                dtMFTransactions.Columns.Add("AMC");
                dtMFTransactions.Columns.Add("ADUL_ProcessId");
                dtMFTransactions.Columns.Add("CMFT_SubBrokerCode");
                dtMFTransactions.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                dtMFTransactions.Columns.Add("CreatedOn");
                dtMFTransactions.Columns.Add("CMFT_ExternalBrokerageAmount", typeof(double));
                dtMFTransactions.Columns.Add("CMFT_Area");
                dtMFTransactions.Columns.Add("CMFT_EUIN");
                dtMFTransactions.Columns.Add("CurrentNAV");
                dtMFTransactions.Columns.Add("DivReinvestment");
                dtMFTransactions.Columns.Add("DivFrequency");
                dtMFTransactions.Columns.Add("Channel");
                dtMFTransactions.Columns.Add("OrderNo");
                //dtMFTransactions.Columns.Add("TransactionNumber");
                dtMFTransactions.Columns.Add("CO_OrderDate");
                dtMFTransactions.Columns.Add("ELSSMaturityDate", typeof(DateTime));
                DataRow drMFTransaction;
                for (int i = 0; i < mfTransactionList.Count; i++)
                {
                    drMFTransaction = dtMFTransactions.NewRow();
                    mfTransactionVo = new MFTransactionVo();
                    mfTransactionVo = mfTransactionList[i];
                    drMFTransaction[0] = mfTransactionVo.TransactionId.ToString();
                    drMFTransaction[1] = mfTransactionVo.CustomerName.ToString();
                    drMFTransaction[2] = mfTransactionVo.Folio.ToString();
                    drMFTransaction[3] = mfTransactionVo.SchemePlan.ToString();
                    drMFTransaction[4] = mfTransactionVo.TransactionType.ToString();
                    drMFTransaction[5] = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                    drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                    //totalUnits = totalUnits + mfTransactionVo.Units;
                    drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    //totalAmount = totalAmount + mfTransactionVo.Amount;
                    drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    drMFTransaction[10] = mfTransactionVo.PortfolioName.ToString();
                    drMFTransaction[11] = mfTransactionVo.TransactionStatus.ToString();
                    drMFTransaction[12] = mfTransactionVo.Category;
                    drMFTransaction[13] = mfTransactionVo.AMCName;
                    if (mfTransactionVo.ProcessId == 0)
                        drMFTransaction[14] = "N/A";
                    else
                        drMFTransaction[14] = int.Parse(mfTransactionVo.ProcessId.ToString());
                    drMFTransaction[15] = mfTransactionVo.SubBrokerCode;
                    drMFTransaction[16] = mfTransactionVo.SubCategoryName;
                    drMFTransaction[17] = mfTransactionVo.CreatedOn;
                    drMFTransaction[18] = decimal.Parse(mfTransactionVo.BrokerageAmount.ToString());
                    drMFTransaction["CMFT_Area"] = mfTransactionVo.Area.ToString();
                    drMFTransaction["CMFT_EUIN"] = mfTransactionVo.EUIN.ToString();
                    drMFTransaction["DivReinvestment"] = mfTransactionVo.DivReinvestmen.ToString(); ;
                    drMFTransaction["DivFrequency"] = mfTransactionVo.Divfrequency.ToString(); ;
                    drMFTransaction["Channel"] = mfTransactionVo.channel.ToString();
                    drMFTransaction["OrderNo"] = mfTransactionVo.orderNo;
                    drMFTransaction["CurrentNav"] = mfTransactionVo.latestNav;
                    //drMFTransaction["TransactionNumber"] = mfTransactionVo.TrxnNo.ToString();
                    if (!string.IsNullOrEmpty(mfTransactionVo.OrdDate.ToString()) && (mfTransactionVo.OrdDate) != DateTime.MinValue)
                    {
                        drMFTransaction["CO_OrderDate"] = mfTransactionVo.OrdDate;
                    }
                    else
                    {
                        drMFTransaction["CO_OrderDate"] = "";
                    }
                    if (!string.IsNullOrEmpty(mfTransactionVo.ELSSMaturityDate.ToString()) && (mfTransactionVo.ELSSMaturityDate) != DateTime.MinValue)
                    {
                        drMFTransaction["ELSSMaturityDate"] = mfTransactionVo.ELSSMaturityDate;
                    }
                    //else
                    //{
                    //    drMFTransaction["ELSSMaturityDate"] = DateTime.MinValue;
                    //}

                    dtMFTransactions.Rows.Add(drMFTransaction);
                }
                if (gvChildDetails.Visible == false)
                {
                    gvChildDetails.Visible = true;
                    //btnDetails.Text = "-";
                }
                else if (gvChildDetails.Visible == true)
                {
                    gvChildDetails.Visible = false;
                    //buttonlink.Text = "+";
                }
                if (Cache["ViewTransaction" + userVo.UserId] == null)
                {
                    Cache.Insert("ViewTransaction" + userVo.UserId, dtMFTransactions);
                }
                else
                {
                    Cache.Remove("ViewTransaction" + userVo.UserId);
                    Cache.Insert("ViewTransaction" + userVo.UserId, dtMFTransactions);
                }
                gvChildDetails.CurrentPageIndex = 0;
                gvChildDetails.DataSource = dtMFTransactions;
                //Session["gvMFTransactions"] = dtMFTransactions;
                gvChildDetails.DataBind();
            }

        }
    }
}
