﻿using System;
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

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerMFUnitHoldingList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        MFPortfolioVo mfPortfolioVo=new MFPortfolioVo();
        List<MFPortfolioNetPositionVo> OnlineMFHoldingList = null;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        string userType;
        int customerId = 0;
        int portfolioId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            customerId = customerVO.CustomerId;
            BindFolioAccount();
           
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
            }
            ddlPortfolio.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnUnitHolding_Click(object sender, EventArgs e)
        {
            //portfolioId = Convert.ToInt32(ddlPortfolio.SelectedValue);
            SetParameter();
            BindUnitHolding();
           
        }
                   
        /// <summary>
        /// Get Unit Holding Data for Customer
        /// </summary>
        protected void BindUnitHolding()
        {
            OnlineMFHoldingList = customerPortfolioBo.GetOnlineUnitHolding(customerId, int.Parse(hdnAccount.Value));
            if (OnlineMFHoldingList.Count != 0)
            {
                DataTable dtMFUnitHoplding = new DataTable();
                dtMFUnitHoplding.Columns.Add("MFNPId");
                dtMFUnitHoplding.Columns.Add("AccountId");
                dtMFUnitHoplding.Columns.Add("Category");
                dtMFUnitHoplding.Columns.Add("Scheme");
                dtMFUnitHoplding.Columns.Add("FolioNum");
                dtMFUnitHoplding.Columns.Add("PurchasedUnits", typeof(double));
                dtMFUnitHoplding.Columns.Add("DVRUnits", typeof(double));
                dtMFUnitHoplding.Columns.Add("OpenUnits", typeof(double));
                dtMFUnitHoplding.Columns.Add("Price");
                dtMFUnitHoplding.Columns.Add("InvestedCost", typeof(double));
                dtMFUnitHoplding.Columns.Add("NAV", typeof(double));
                dtMFUnitHoplding.Columns.Add("CurrentValue", typeof(double));
                dtMFUnitHoplding.Columns.Add("UnitsSold", typeof(double));
                dtMFUnitHoplding.Columns.Add("RedeemedAmount", typeof(double));
                dtMFUnitHoplding.Columns.Add("DVP", typeof(double));
                dtMFUnitHoplding.Columns.Add("TotalPL", typeof(double));
                dtMFUnitHoplding.Columns.Add("AbsoluteReturn", typeof(double));
                dtMFUnitHoplding.Columns.Add("DVR", typeof(double));
                dtMFUnitHoplding.Columns.Add("XIRR", typeof(double));
                dtMFUnitHoplding.Columns.Add("TotalDividends", typeof(double));
                dtMFUnitHoplding.Columns.Add("AMCCode");
                dtMFUnitHoplding.Columns.Add("SchemeCode");
                dtMFUnitHoplding.Columns.Add("AmcName");
                dtMFUnitHoplding.Columns.Add("SubCategoryName");
                dtMFUnitHoplding.Columns.Add("FolioStartDate");
                dtMFUnitHoplding.Columns.Add("InvestmentStartDate");
                dtMFUnitHoplding.Columns.Add("CMFNP_NAVDate");
                dtMFUnitHoplding.Columns.Add("CMFNP_ValuationDate");
                dtMFUnitHoplding.Columns.Add("RealizesdGain");

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
                        drMFUnitHoplding["RealizesdGain"] = mfPortfolioVo.ReturnsRealizedTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                      else
                        drMFUnitHoplding["RealizesdGain"] = "0.00";
                    dtMFUnitHoplding.Rows.Add(drMFUnitHoplding);
               }
                if (dtMFUnitHoplding.Rows.Count > 0)
                  {
                    if (Cache["UnitHolding" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("UnitHolding" + advisorVo.advisorId, dtMFUnitHoplding);
                    }
                    else
                    {
                        Cache.Remove("UnitHolding" + advisorVo.advisorId);
                        Cache.Insert("UnitHolding" + advisorVo.advisorId, dtMFUnitHoplding);
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
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;           
            string AccountId = rgUnitHolding.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString();
            if(ddlAction.SelectedItem.Value.ToString() == "ABY")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderAdditionalPurchase','&accountId=" + AccountId + "')", true); 
            }
            else if (ddlAction.SelectedItem.Value.ToString() == "SIP")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','&accountId=" + AccountId + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderRdemptionTransType','&accountId=" + AccountId + "')", true);
            }

        }
        protected void rgUnitHolding_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable  dtUnitHolding = new DataTable();
            dtUnitHolding = (DataTable)Cache["UnitHolding" + advisorVo.advisorId.ToString()];
            if (dtUnitHolding != null)
            {
                rgUnitHolding.DataSource = dtUnitHolding;
                rgUnitHolding.Visible = true;
            }
        }
        protected void rgUnitHolding_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
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
                            if (e.CommandName == "SelectTransaction")
                            {
                                Response.Redirect("ControlHost.aspx?pageid=CustomerTransactionBookList&folionum=" + folio + "&SchemePlanCode=" + SchemePlanCode + "", false);
                            }

                        }
                    }
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
    }
}