using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.SqlTypes;
using BoWerpAdmin;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Numeric;
using Telerik.Web.UI;
using BoCustomerPortfolio;
using VoUser;
using System.Reflection;
//using BoProductMaster;

namespace WealthERP.Admin
{
    public partial class PriceList : System.Web.UI.UserControl
    {
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PriceBo priceBo = new PriceBo();
        DataSet dsCategoryList = new DataSet();
        DataTable dtGetMFfund = new DataTable();
        string assetType = "";
        string categoryCode;
        int amcCode = 0;
        string subCategory = "All";
        AdvisorVo advisorVo = new AdvisorVo();

        //List<GoalProfileSetupVo> MutualFundList = new List<PriceVo>();
        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }
        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            OnClick_Submit(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            compDateValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            cvChkFutureDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            //trMFFundPerformance.Visible = false;
            trExportFilteredMFRecord.Visible = false;
            gvEquityRecord.Visible = false;
            imgBtnrgHoldings.Visible = false;
            //DateTime StartDate = DateTime.Now;
            //txtFrom.SelectedDate = StartDate;
            //txtTo.SelectedDate = DateTime.Now;
            if (Request.QueryString["AssetId"] != null)
            {
                hdnassetType.Value = Request.QueryString["AssetId"].ToString();
            }
            if (hdnassetType.Value == "MF")
            {
                //RadTabStrip1.Tabs[1].Visible = true;
                //RadTabStrip1.Tabs[2].Visible = true;
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                trgvEquityView.Visible = false;
                lblheader.Text = "MF Data Query";
                trSelectMutualFund.Visible = true;
                trSelectSchemeNAV.Visible = true;
                trNavCategory.Visible = true;
                gvMFRecord.Visible = false;
                tblFactSheet.Visible = false;
                BindYear();
                BindMonth();
                if (!IsPostBack)
                {
                    lblIllegal.Visible = false;
                    trgvEquityView.Visible = false;
                    gvEquityRecord.Visible = false;
                    tdFromDate.Visible = false;
                    tdToDate.Visible = false;
                    btnSubmit.Visible = false;
                    trSelectMutualFund.Visible = false;
                    BindMutualFundDropDowns();
                    BindSelectAMCDropdown();
                    BindNAVCategory();
                    BindSchemeCategory();
                    trSelectMutualFund.Visible = false;
                    trSelectSchemeNAV.Visible = false;
                    trNavCategory.Visible = false;
                }
            }
            else if (hdnassetType.Value == "Equity")
            {
                lblIllegal.Visible = false;
                btnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
              //  RadTabStrip1.Tabs[1].Visible = false;
               // RadTabStrip1.Tabs[2].Visible = false;
                lblheader.Text = "Equity Data Query";
                pnlSchemeComparison.Visible = false;
                rbtnCurrent.Visible = true;
                rbtnHistorical.Visible = true;
                trgvEquityView.Visible = false;
                gvEquityRecord.Visible = true;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
                trNavCategory.Visible = false;
                tdFromDate.Visible = false;
                tdToDate.Visible = false;
              //  RadTabStrip1.Tabs[0].Text = "Price";
            }
            if (!IsPostBack)
            {
                if ((Request.QueryString["SchemeCode"] != null) && (Request.QueryString["AMCCode"] != null))
                {
                    int month = int.Parse(Request.QueryString["Month"].ToString());
                    int amcCode = int.Parse(Request.QueryString["AMCCode"].ToString());
                    int schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                    BindYear();
                    BindMutualFundDropDowns();
                    ddlAmcCode.SelectedValue = amcCode.ToString();
                    BindNAVCategory();
                    LoadAllSchemeList(amcCode);
                    ddlSchemeList.SelectedValue = schemeCode.ToString();
                    BindMonth();
                    ddMonth.SelectedValue = month.ToString();
                    ShowFactSheet();
                    RadTabStrip1.Tabs[2].Selected = true;
                    FactsheetMultiPage.PageViews[2].Selected = true;
                }
            }
            BindSelectAMCDropdown();
        }
        private void BindNAVCategory()
        {
            DataSet dsNavCategory;
            DataTable dtNavCategory;
            dsNavCategory = priceBo.GetNavOverAllCategoryList();
            dtNavCategory = dsNavCategory.Tables[0];
            if (dtNavCategory.Rows.Count > 0)
            {

                ddlNAVCategory.DataSource = dtNavCategory;
                ddlNAVCategory.DataValueField = dtNavCategory.Columns["Category_Code"].ToString();
                ddlNAVCategory.DataTextField = dtNavCategory.Columns["Category_Name"].ToString();
                ddlNAVCategory.DataBind();
                ddlNAVCategory.Items.Insert(0, new ListItem("All", "All"));

                //--------------------------------------------FactSheet Category Binding------------------


                ddlFactCategory.DataSource = dtNavCategory;
                ddlFactCategory.DataValueField = dtNavCategory.Columns["Category_Code"].ToString();
                ddlFactCategory.DataTextField = dtNavCategory.Columns["Category_Name"].ToString();
                ddlFactCategory.DataBind();
                ddlFactCategory.Items.Insert(0, new ListItem("All", "All"));
            }
        }
        private void BindSchemeCategory()
        {
            DataSet dsSchemeCategory;
            DataTable dtSchemeCategory;
            dsSchemeCategory = priceBo.GetNavOverAllCategoryList();
            //----------------------------------------------Scheme Category Binding------------------------
            if (dsSchemeCategory.Tables.Count > 0)
            {
                dtSchemeCategory = dsSchemeCategory.Tables[0];
                ddlCategory.DataSource = dtSchemeCategory;
                ddlCategory.DataValueField = dtSchemeCategory.Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dtSchemeCategory.Columns["Category_Name"].ToString();
                ddlCategory.DataBind();

            }
            ddlCategory.Items.Insert(0, new ListItem("All", "All"));
        }



        private void BindMonth()
        {
            DataSet ds = customerPortfolioBo.PopulateEQTradeMonth(int.Parse(ddYear.SelectedItem.Value.ToString()));
            ddMonth.DataSource = ds;
            ddMonth.DataTextField = ds.Tables[0].Columns["TradeMonth"].ToString();
            ddMonth.DataValueField = ds.Tables[0].Columns["WTD_Month"].ToString();
            ddMonth.DataBind();
            ddMonth.SelectedValue = DateTime.Now.Month.ToString();

        }

        private void BindYear()
        {
            DataSet ds = customerPortfolioBo.PopulateEQTradeYear();
            ddYear.DataSource = ds;
            ddYear.DataTextField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddYear.DataValueField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddYear.DataBind();
            //ddYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void rbtnCurrent_CheckedChanged(object sender, EventArgs e)
        {
            //trFromDate.Style.Add("display", "none");
            //trToDate.Style.Add("display", "none");
            // trFromDate.Visible = false;
            trExportFilteredMFRecord.Visible = false;
            btnSubmit.Visible = true;
            tdFromDate.Visible = false;
            tdToDate.Visible = false;
            //trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            // trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            //if (IsPostBack)
            //{
            //    ddlSelectMutualFund.SelectedIndex = 0;
            //    ddlSelectSchemeNAV.SelectedIndex = 0;
            //}
            if (hdnassetType.Value == "MF")
            {
                trSelectSchemeNAV.Visible = true;
                trSelectMutualFund.Visible = true;
            }
            else
            {
                trSelectSchemeNAV.Visible = false;
                trSelectMutualFund.Visible = false;
            }
        }
        protected void rbtnHistorical_CheckedChanged(object sender, EventArgs e)
        {
            hdnSchemeSearch.Value = null;
            hdnCompanySearch.Value = null;
            //trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            //trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            trExportFilteredMFRecord.Visible = false;

            //if (IsPostBack)
            //{
            //    ddlSelectMutualFund.SelectedIndex = 0;
            //    ddlSelectSchemeNAV.SelectedIndex = 0;
            //}
            if (rbtnHistorical.Checked || rbtnMissingNAV.Checked)
            {
                //trFromDate.Style.Add("display", "block");
                //trToDate.Style.Add("display", "block");
                tdFromDate.Visible = true;
                tdToDate.Visible = true;
                btnSubmit.Visible = true;
            }
            if (rbtnMissingNAV.Checked)
            {
                Panel1.Visible = false;
            }
            if (hdnassetType.Value == "MF")
            {
                trSelectSchemeNAV.Visible = true;
                trSelectMutualFund.Visible = true;
            }
            else
            {
                trSelectSchemeNAV.Visible = false;
                trSelectMutualFund.Visible = false;
            }
        }
        protected void ddlAssetGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnHistorical.Checked = false;
            rbtnCurrent.Checked = false;
            //trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            //trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            if (hdnassetType.Value == "MF")
            {
                lblIllegal.Visible = false;
                btnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
            }
            else if (hdnassetType.Value == "Equity")
            {
                trSelectSchemeNAV.Visible = false;
                trSelectMutualFund.Visible = false;
                lblIllegal.Visible = false;
                btnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
            }
        }
        public void BindMutualFundDropDowns()
        {
            PriceBo priceBo = new PriceBo();
            DataTable dtGetMutualFundList = new DataTable();
            dtGetMutualFundList = priceBo.GetMutualFundList();
            ddlSelectMutualFund.DataSource = dtGetMutualFundList;
            ddlSelectMutualFund.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlSelectMutualFund.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlSelectMutualFund.DataBind();
            ddlSelectMutualFund.Items.Insert(0, new ListItem("Select AMC", "Select AMC Code"));
            //-----------------------------------------------------------------------------------

            ddlAmcCode.DataSource = dtGetMutualFundList;
            ddlAmcCode.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlAmcCode.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlAmcCode.DataBind();
            ddlAmcCode.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void ddlSelectMutualFund_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSchemeNAV();
            gvMFRecord.DataSource = null;
            gvMFRecord.DataBind();
        }
        public void LoadAllSchemeNAV()
        {
            if (ddlSelectMutualFund.SelectedIndex != 0)
            {
                PriceBo priceBo = new PriceBo();
                DataSet dsLoadAllSchemeNAV;
                DataTable dtLoadAllSchemeNAV = new DataTable();
                if (ddlSelectMutualFund.SelectedIndex != 0 && ddlNAVCategory.SelectedIndex == 0)
                {
                    amcCode = int.Parse(ddlSelectMutualFund.SelectedValue.ToString());
                    categoryCode = ddlNAVCategory.SelectedValue;
                    //subCategory = "All";
                    dsLoadAllSchemeNAV = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                    dtLoadAllSchemeNAV = dsLoadAllSchemeNAV.Tables[0];

                    //dtLoadAllSchemeNAV = priceBo.GetAllScehmeList(int.Parse(ddlAmcCode.SelectedValue));
                }
                //if (ddlSelectMutualFund.SelectedIndex != 0 && ddlNAVCategory.SelectedIndex != 0 && ddlNAVSubCategory.SelectedIndex == 0)
                //{
                //    amcCode = int.Parse(ddlSelectMutualFund.SelectedValue.ToString());
                //    categoryCode = ddlNAVCategory.SelectedValue;
                //    subCategory = ddlNAVSubCategory.SelectedValue;
                //    dsLoadAllSchemeNAV = priceBo.GetSchemeListCategorySubCategory(amcCode, categoryCode, subCategory);
                //    dtLoadAllSchemeNAV = dsLoadAllSchemeNAV.Tables[0];
                //}
                if (ddlSelectMutualFund.SelectedIndex != 0 && ddlNAVCategory.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlSelectMutualFund.SelectedValue.ToString());
                    categoryCode = ddlNAVCategory.SelectedValue;
                    //subCategory = ddlNAVSubCategory.SelectedValue;
                    dsLoadAllSchemeNAV = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                    dtLoadAllSchemeNAV = dsLoadAllSchemeNAV.Tables[0];
                }
                if (dtLoadAllSchemeNAV.Rows.Count > 0)
                {
                    ddlSelectSchemeNAV.DataSource = dtLoadAllSchemeNAV;
                    ddlSelectSchemeNAV.DataTextField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanName"].ToString();
                    ddlSelectSchemeNAV.DataValueField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanCode"].ToString();
                    ddlSelectSchemeNAV.DataBind();
                    ddlSelectSchemeNAV.Items.Insert(0, new ListItem("All Scheme", "0"));
                }
                else
                {
                    ddlSelectSchemeNAV.Items.Clear();
                    ddlSelectSchemeNAV.DataSource = null;
                    ddlSelectSchemeNAV.DataBind();
                    ddlSelectSchemeNAV.Items.Insert(0, new ListItem("Select", "Select"));
                }
                // ddlSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
            }
        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            PriceBo PriceObj = new PriceBo();
            DataSet ds;
            lblIllegal.Visible = false;
            trgvEquityView.Visible = true;
            pnlMissingNAV.Visible = false;
            //trgrMfView.Visible = true;
            //trPageCount.Visible = true;
            // trPager.Visible = true;
            //trMfPagecount.Visible = true;
            //hdnSchemeSearch.Value = null;
            //hdnCompanySearch.Value = null;

            //if (ddlAssetGroup.SelectedValue == "0")
            //{
            //    //lblIllegal.Visible = true;
            //    //lblIllegal.Text = "Select Asset";
            //    //trSelectMutualFund.Visible = false;
            //    //trSelectSchemeNAV.Visible = false;
            //}
            //else 
            if (rbtnCurrent.Checked)
            {
                if (hdnassetType.Value == "Equity")
                {
                    string Search = hdnCompanySearch.Value;
                    //hdnEquityCount.Value = PriceObj.GetEquityCountSnapshot("C", Search, mypager.CurrentPage).ToString();
                    //lblTotalRows.Text = hdnEquityCount.Value;
                    //GetPageCount_Equity();
                    ds = PriceObj.GetEquitySnapshot("D", Search);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvEquityRecord.DataSource = ds;
                        gvEquityRecord.DataBind();
                        gvEquityRecord.Visible = true;
                        imgBtnrgHoldings.Visible = false;
                        // DivEquity.Style.Add("display", "visible");
                        DivPager.Style.Add("display", "visible");
                        DivMF.Style.Add("display", "none");
                        Search = null;
                        hdnCompanySearch.Value = null;
                        //trToDate.Visible = true;
                        //trFromDate.Visible = true;
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        imgBtnrgHoldings.Visible = false;
                        trgvEquityView.Visible = false;
                        gvEquityRecord.Visible = false;
                        //trgrMfView.Visible = false;
                        // trPageCount.Visible = false;
                        trPager.Visible = false;
                        trMfPagecount.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
                        gvMFRecord.DataSource = null;
                        gvMFRecord.DataBind();

                    }
                    btnSubmit.Visible = true;
                }

                else
                {
                    int All = 0;
                    string categoryCode = null;
                    int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                    if (ddlNAVCategory.SelectedIndex != 0)
                    {
                        All = 1;
                        categoryCode = ddlNAVCategory.SelectedValue;
                    }
                    int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;

                    int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);

                    string Search = hdnSchemeSearch.Value;
                    //hdnMFCount.Value = PriceObj.GetAMFICountSnapshot("C", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
                    //lblMFTotalRows.Text = hdnMFCount.Value;
                    //GetPageCount_MF();
                    //ds = PriceObj.GetAMFISnapshot("D", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
                    ds = PriceObj.GetAMFISnapshot("D", Search, amfiCode, schemeCode, selectAllCode, All, categoryCode);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        trExportFilteredMFRecord.Visible = true;
                        gvMFRecord.Visible = true;
                        Panel1.Visible = true;
                        //gvMFRecord.CurrentPageIndex = 0;
                        gvMFRecord.DataSource = ds.Tables[0]; ;
                        gvMFRecord.DataBind();
                        DivMF.Style.Add("display", "visible");
                        //  DivEquity.Style.Add("display", "none");
                        DivPager.Style.Add("display", "visible");
                        //Search = null;
                        hdnSchemeSearch.Value = null;
                        //gvMFRecord.Visible = true;
                        //gvNAVPriceList.DataSource = ds.Tables[0];
                        //gvNAVPriceList.DataBind();

                    }
                    else
                    {
                        // trgvEquityView.Visible = false;
                        //trgrMfView.Visible = false;
                        //trPageCount.Visible = false;
                        // trPager.Visible = false;
                        // trMfPagecount.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
                        gvMFRecord.DataSource = null;
                        gvMFRecord.DataBind();
                        Panel1.Visible = false;
                    }
                }
            }
            else if (rbtnHistorical.Checked)
            {
                if (txtTo.SelectedDate.ToString() == "" || txtFrom.SelectedDate.ToString() == "")
                {
                }
                else
                {
                    DateTime StartDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
                    DateTime EndDate = DateTime.Parse(txtTo.SelectedDate.ToString());
                    //DateTime StartDate = DateTime.Parse(txtFromDate.Text.ToString());
                    //DateTime EndDate = DateTime.Parse(txtToDate.Text.ToString());
                    hdnFromDate.Value = StartDate.ToString();
                    hdnToDate.Value = EndDate.ToString();

                    if (hdnassetType.Value == "Equity")
                    {
                        string Search = hdnCompanySearch.Value;
                        //Search = null;
                        //hdnEquityCount.Value = PriceObj.GetEquityCount("C", StartDate, EndDate, Search, mypager.CurrentPage).ToString();
                        //lblTotalRows.Text = hdnEquityCount.Value;
                        //GetPageCount_Equity();
                        //ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search, mypager.CurrentPage);
                        ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvEquityRecord.DataSource = ds;
                            gvEquityRecord.DataBind();
                            gvEquityRecord.Visible = true;
                            imgBtnrgHoldings.Visible = false;
                            //  DivEquity.Style.Add("display", "visible");
                            DivMF.Style.Add("display", "none");
                            DivPager.Style.Add("display", "visible");
                            tdToDate.Visible = true;
                            tdFromDate.Visible = true;
                            btnSubmit.Visible = true;

                            if (ds != null)
                                imgBtnrgHoldings.Visible = false;
                            if (Cache["gvEquityRecord" + advisorVo.advisorId] == null)
                            {
                                Cache.Insert("gvEquityRecord" + advisorVo.advisorId, ds);
                            }
                            else
                            {
                                Cache.Remove("gvEquityRecord" + advisorVo.advisorId);
                                Cache.Insert("gvEquityRecord" + advisorVo.advisorId, ds);
                            }
                        }

                        else
                        {
                            imgBtnrgHoldings.Visible = false;
                            trgvEquityView.Visible = false;
                            gvEquityRecord.Visible = false;
                            //trgrMfView.Visible = false;
                            trPager.Visible = false;
                            trMfPagecount.Visible = false;
                            tdToDate.Visible = true;
                            tdFromDate.Visible = true;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

                        }
                        btnSubmit.Visible = true;
                        //hdnCompanySearch.Value = null;
                    }
                    else if (hdnassetType.Value == "MF")
                    {
                        //int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                        //int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
                        //int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
                        //string Search = hdnSchemeSearch.Value;
                        int All = 0;
                        string categoryCode = "";
                        int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                        if (ddlNAVCategory.SelectedIndex != 0)
                        {
                            All = 1;
                            categoryCode = ddlNAVCategory.SelectedValue;
                        }
                        int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;

                        int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
                        string Search = hdnSchemeSearch.Value;
                        //hdnMFCount.Value = PriceObj.GetAMFICount("C", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
                        //lblMFTotalRows.Text = hdnMFCount.Value;
                        //ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
                        ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, amfiCode, schemeCode, selectAllCode, All, categoryCode);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            trExportFilteredMFRecord.Visible = true;
                            gvMFRecord.DataSource = ds.Tables[0];
                            gvMFRecord.DataBind();
                            gvMFRecord.Visible = true;
                            //GetPageCount_MF();
                            DivPager.Style.Add("display", "visible");
                            DivMF.Style.Add("display", "visible");
                            // DivEquity.Style.Add("display", "none");
                            Panel1.Visible = true;

                            if (ds.Tables[0] != null)
                                imgBtnrgHoldings.Visible = false;
                            if (Cache["gvEquityRecord" + advisorVo.advisorId] == null)
                            {
                                Cache.Insert("gvEquityRecord" + advisorVo.advisorId, ds.Tables[0]);
                            }
                            else
                            {
                                Cache.Remove("gvEquityRecord" + advisorVo.advisorId);
                                Cache.Insert("gvEquityRecord" + advisorVo.advisorId, ds.Tables[0]);
                            }
                        }
                        else
                        {
                            trgvEquityView.Visible = false;
                            //trgrMfView.Visible = false;
                            // trPageCount.Visible = false;
                            //trPager.Visible = false;
                            // trMfPagecount.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records For This Period Of Time');", true);
                            gvMFRecord.DataSource = null;
                            gvMFRecord.DataBind();
                            Panel1.Visible = false;
                        }
                        //Search = null;
                        //hdnSchemeSearch.Value = null;
                    }
                }
            }
            else if (rbtnMissingNAV.Checked)
            {
                DateTime StartDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
                DateTime EndDate = DateTime.Parse(txtTo.SelectedDate.ToString());
                DataTable dt = PriceObj.GetMissingNAVSchemeList(int.Parse(ddlSelectMutualFund.SelectedValue), ddlNAVCategory.SelectedValue, int.Parse(ddlSelectSchemeNAV.SelectedValue), StartDate, EndDate);
                rdMFMissingNAV.DataSource = dt;
                rdMFMissingNAV.DataBind();
                pnlMissingNAV.Visible = true;
                DivMF.Style.Add("display", "visible");
                if (Cache["MFMissingNAV" + advisorVo.advisorId] != null)
                {
                    Cache.Remove("MFMissingNAV" + advisorVo.advisorId);
                }
                Cache.Insert("MFMissingNAV" + advisorVo.advisorId, dt);
                
            }
        }
        public void PagingTelerikGrid()
        {
            //    PriceBo PriceObj = new PriceBo();
            //    DataSet ds;
            //    lblIllegal.Visible = false;
            //    trgvEquityView.Visible = true;
            //    //trgrMfView.Visible = true;
            //    //trPageCount.Visible = true;
            //    // trPager.Visible = true;
            //    //trMfPagecount.Visible = true;
            //    //hdnSchemeSearch.Value = null;
            //    //hdnCompanySearch.Value = null;

            //    //if (ddlAssetGroup.SelectedValue == "0")
            //    //{
            //    //    //lblIllegal.Visible = true;
            //    //    //lblIllegal.Text = "Select Asset";
            //    //    //trSelectMutualFund.Visible = false;
            //    //    //trSelectSchemeNAV.Visible = false;
            //    //}
            //    //else 
            //    if (rbtnCurrent.Checked)
            //    {
            //        if (hdnassetType.Value == "Equity")
            //        {
            //            string Search = hdnCompanySearch.Value;
            //            //hdnEquityCount.Value = PriceObj.GetEquityCountSnapshot("C", Search, mypager.CurrentPage).ToString();
            //            //lblTotalRows.Text = hdnEquityCount.Value;
            //            //GetPageCount_Equity();
            //            ds = PriceObj.GetEquitySnapshot("D", Search);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gvEquityRecord.DataSource = ds;
            //                gvEquityRecord.DataBind();
            //                DivEquity.Style.Add("display", "visible");
            //                DivPager.Style.Add("display", "visible");
            //                DivMF.Style.Add("display", "none");
            //                Search = null;
            //                hdnCompanySearch.Value = null;
            //                //trToDate.Visible = true;
            //                //trFromDate.Visible = true;
            //                trbtnSubmit.Visible = true;
            //            }
            //            else
            //            {
            //                trgvEquityView.Visible = false;
            //                //trgrMfView.Visible = false;
            //                trPageCount.Visible = false;
            //                trPager.Visible = false;
            //                trMfPagecount.Visible = false;
            //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

            //            }
            //            trbtnSubmit.Visible = true;
            //        }

            //        else
            //        {
            //            int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
            //            int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
            //            int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);

            //            string Search = hdnSchemeSearch.Value;
            //            //hdnMFCount.Value = PriceObj.GetAMFICountSnapshot("C", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
            //            //lblMFTotalRows.Text = hdnMFCount.Value;
            //            //GetPageCount_MF();
            //            //ds = PriceObj.GetAMFISnapshot("D", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
            //            ds = PriceObj.GetAMFISnapshot("D", Search, amfiCode, schemeCode, selectAllCode);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gvMFRecord.DataSource = ds.Tables[0]; ;
            //                gvMFRecord.DataBind();
            //                DivMF.Style.Add("display", "visible");
            //                DivEquity.Style.Add("display", "none");
            //                DivPager.Style.Add("display", "visible");
            //                Search = null;
            //                hdnSchemeSearch.Value = null;

            //                //gvNAVPriceList.DataSource = ds.Tables[0];
            //                //gvNAVPriceList.DataBind();
            //            }
            //            else
            //            {
            //                // trgvEquityView.Visible = false;
            //                //trgrMfView.Visible = false;
            //                //trPageCount.Visible = false;
            //                // trPager.Visible = false;
            //                // trMfPagecount.Visible = false;
            //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
            //            }
            //        }
            //    }
            //    else if (rbtnHistorical.Checked)
            //    {
            //        DateTime StartDate = DateTime.Parse(txtFromDate.Text.ToString());
            //        DateTime EndDate = DateTime.Parse(txtToDate.Text.ToString());
            //        hdnFromDate.Value = StartDate.ToString();
            //        hdnToDate.Value = EndDate.ToString();

            //        if (hdnassetType.Value == "Equity")
            //        {
            //            string Search = hdnCompanySearch.Value;
            //            //Search = null;
            //            //hdnEquityCount.Value = PriceObj.GetEquityCount("C", StartDate, EndDate, Search, mypager.CurrentPage).ToString();
            //            //lblTotalRows.Text = hdnEquityCount.Value;
            //            //GetPageCount_Equity();
            //            //ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search, mypager.CurrentPage);
            //            ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gvEquityRecord.DataSource = ds;
            //                gvEquityRecord.DataBind();
            //                DivEquity.Style.Add("display", "visible");
            //                DivMF.Style.Add("display", "none");
            //                DivPager.Style.Add("display", "visible");
            //                trToDate.Visible = true;
            //                trFromDate.Visible = true;
            //                trbtnSubmit.Visible = true;
            //            }

            //            else
            //            {
            //                trgvEquityView.Visible = false;
            //                //trgrMfView.Visible = false;
            //                trPageCount.Visible = false;
            //                trPager.Visible = false;
            //                trMfPagecount.Visible = false;
            //                trFromDate.Visible = true;
            //                trToDate.Visible = true;
            //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

            //            }
            //            trbtnSubmit.Visible = true;
            //            //hdnCompanySearch.Value = null;
            //        }
            //        else if (hdnassetType.Value == "MF")
            //        {
            //            int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
            //            int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
            //            int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
            //            string Search = hdnSchemeSearch.Value;
            //            //hdnMFCount.Value = PriceObj.GetAMFICount("C", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
            //            //lblMFTotalRows.Text = hdnMFCount.Value;
            //            //ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
            //            ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, amfiCode, schemeCode, selectAllCode);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gvMFRecord.DataSource = ds.Tables[0];
            //                gvMFRecord.DataBind();
            //                //GetPageCount_MF();
            //                //DivPager.Style.Add("display", "visible");
            //                //DivMF.Style.Add("display", "visible");
            //                //DivEquity.Style.Add("display", "none");
            //            }
            //            else
            //            {
            //                trgvEquityView.Visible = false;
            //                //trgrMfView.Visible = false;
            //                // trPageCount.Visible = false;
            //                //trPager.Visible = false;
            //                // trMfPagecount.Visible = false;
            //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records For This Period Of Time');", true);
            //            }
            //            //Search = null;
            //            //hdnSchemeSearch.Value = null;
            //        }
            //    }
        }



        private string SchemeName()
        {
            string txt = string.Empty;
            TextBox txt1 = new TextBox();
            if ((TextBox)gvMFRecord.FindControl("txtSchemeSearch") != null)
            {
                txt1 = (TextBox)gvMFRecord.FindControl("txtSchemeSearch");
                txt = txt1.Text;
            }
            return txt;
        }

        private string CompanyName()
        {
            string txt3 = string.Empty;
            TextBox txt2 = new TextBox();
            if ((TextBox)gvEquityRecord.FindControl("txtCompanySearch") != null)
            {
                txt2 = (TextBox)gvEquityRecord.FindControl("txtCompanySearch");
                txt3 = txt2.Text;
            }
            return txt3;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (hdnassetType.Value == "Equity")
            {
                string txtCompanyName = CompanyName();
                if (txtCompanyName != null)
                    hdnCompanySearch.Value = txtCompanyName;
            }

            else if (hdnassetType.Value == "MF")
            {
                string txtSchemeName = SchemeName();

                if (txtSchemeName != null)
                    hdnSchemeSearch.Value = txtSchemeName;
            }
            OnClick_Submit(sender, e);
        }

        private void GetPageCount_Equity()
        {
            string upperlimit;
            int rowCount = Convert.ToInt32(hdnEquityCount.Value);
            int ratio = rowCount / 10;
            mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            string lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
            upperlimit = (mypager.CurrentPage * 10).ToString();
            if (mypager.CurrentPage == mypager.PageCount)
                upperlimit = hdnEquityCount.Value;
            string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            lblCurrentPage.Text = PageRecords;
            hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        }

        private void GetPageCount_MF()
        {
            int rowCount = 0;
            string upperlimit;
            rowCount = Convert.ToInt32(hdnMFCount.Value);
            int ratio = rowCount / 15;
            mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            string lowerlimit = (((mypager.CurrentPage - 1) * 15) + 1).ToString();
            if (rowCount < 16)
                upperlimit = rowCount.ToString();
            else
                upperlimit = (mypager.CurrentPage * 15).ToString();
            if (mypager.CurrentPage == mypager.PageCount)
                upperlimit = hdnMFCount.Value;
            string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            lblMFCurrentPage.Text = PageRecords;
            hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        }


        /*******************************************************************************************************/
        /// <summary>
        /// Code Started for Factsheet By Bhoopendra Prakash Sahoo(date:17/10/2011)
        /// </summary>
        /*******************************************************************************************************/

        public void BindSelectAMCDropdown()
        {
            PriceBo priceBo = new PriceBo();
            //ProductMFBo productMFBo = new ProductMFBo();
            DataTable dtGetAMCList = new DataTable();
            dtGetAMCList = priceBo.GetMutualFundList();
            ddlSelectAMC.DataSource = dtGetAMCList;
            ddlSelectAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlSelectAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlSelectAMC.DataBind();
            ddlSelectAMC.Items.Insert(0, new ListItem("All AMC", "0"));
        }

        //protected void ddlSelectAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadAllNAVScheme();
        //}

        //public void LoadAllNAVScheme()
        //{
        //    if (ddlSelectAMC.SelectedIndex != 0)
        //    {
        //        PriceBo priceBo = new PriceBo();
        //        DataTable dtLoadAllSchemeNAV = new DataTable();
        //        dtLoadAllSchemeNAV = priceBo.GetAllScehmeList(int.Parse(ddlSelectAMC.SelectedValue));
        //        ddlSelectScheme.DataSource = dtLoadAllSchemeNAV;
        //        ddlSelectScheme.DataTextField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanName"].ToString();
        //        ddlSelectScheme.DataValueField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanCode"].ToString();
        //        ddlSelectScheme.DataBind();
        //        ddlSelectScheme.Items.Insert(0, new ListItem("Select Scheme", "0"));
        //    }
        //    else
        //    {
        //    }
        //}

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            //string categoryCode = ddlCategory.SelectedValue;
            //dsCategoryList = priceBo.BindddlMFSubCategory();
            //if (categoryCode == "0")
            //{
            //    divSubCategory.Visible = false;
            //    hdnSubCategory.Value = "";

            //}
            //if (categoryCode == "MFCO")
            //{
            //    divSubCategory.Visible = true;
            //    ddlSubCategory.DataSource = dsCategoryList.Tables[0];
            //    ddlSubCategory.DataTextField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            //    ddlSubCategory.DataValueField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            //    ddlSubCategory.DataBind();
            //    // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
            //}
            //if (categoryCode == "MFDT")
            //{
            //    divSubCategory.Visible = true;
            //    ddlSubCategory.DataSource = dsCategoryList.Tables[1];
            //    ddlSubCategory.DataTextField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            //    ddlSubCategory.DataValueField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            //    ddlSubCategory.DataBind();
            //    //ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
            //}
            //if (categoryCode == "MFEQ")
            //{
            //    divSubCategory.Visible = true;
            //    ddlSubCategory.DataSource = dsCategoryList.Tables[2];
            //    ddlSubCategory.DataTextField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            //    ddlSubCategory.DataValueField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            //    ddlSubCategory.DataBind();
            //    // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
            //}
            //if (categoryCode == "MFHY")
            //{
            //    divSubCategory.Visible = true;
            //    ddlSubCategory.DataSource = dsCategoryList.Tables[3];
            //    ddlSubCategory.DataTextField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            //    ddlSubCategory.DataValueField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            //    ddlSubCategory.DataBind();
            //    // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
            //}
        }

        protected void OnClick_btnGo(object sender, EventArgs e)
        {
            ViewState["AmcCode"] = null;
            ViewState["SubCategory"] = null;
            ViewState["ReturnPeriod"] = null;
            ViewState["Condition"] = null;

            hdnAmcCode.Value = ddlSelectAMC.SelectedValue;
            ViewState["AmcCode"] = hdnAmcCode.Value;

            if (ddlCategory.SelectedIndex != 0)
                hdnSubCategory.Value = ddlCategory.SelectedValue;
            else
                hdnSubCategory.Value = "";
            ViewState["SubCategory"] = hdnSubCategory.Value;

            hdnReturnPeriod.Value = ddlReturn.SelectedValue;
            ViewState["ReturnPeriod"] = hdnReturnPeriod.Value;

            hdnCondition.Value = ddlCondition.SelectedValue;
            ViewState["Condition"] = hdnCondition.Value;

            BindMFFundPerformance();
        }

        private void BindMFFundPerformance()
        {
            string expression = string.Empty;
            if (hdnSubCategory.Value == "")
            {
                if (ViewState["SubCategory"] == null)
                {
                    hdnSubCategory.Value = "0";
                }
                else
                {
                    hdnSubCategory.Value = ViewState["SubCategory"].ToString();
                }
            }

            if (hdnAmcCode.Value == "")
            {
                if (ViewState["AmcCode"] == null)
                {
                    hdnAmcCode.Value = "0";
                }
                else
                {
                    hdnAmcCode.Value = ViewState["AmcCode"].ToString();
                }
            }

            if (hdnReturnPeriod.Value == "")
            {
                if (ViewState["ReturnPeriod"] == null)
                {
                    hdnReturnPeriod.Value = "0";
                }
                else
                {
                    hdnReturnPeriod.Value = ViewState["ReturnPeriod"].ToString();
                }
            }

            if (hdnCondition.Value == "")
            {
                if (ViewState["Condition"] == null)
                {
                    hdnCondition.Value = "0";
                }
                else
                {
                    hdnCondition.Value = ViewState["Condition"].ToString();
                }
            }

            dtGetMFfund = priceBo.GetMFFundPerformance(int.Parse(hdnAmcCode.Value), hdnSubCategory.Value);

            if (int.Parse(hdnReturnPeriod.Value) == 1)
            {
                expression = "OneWeekReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 2)
            {
                expression = "OneMonthReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 3)
            {
                expression = "ThreeMonthReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 4)
            {
                expression = "SixMonthReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 5)
            {
                expression = "OneYearReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 6)
            {
                expression = "TwoYearReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 7)
            {
                expression = "ThreeYearReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 8)
            {
                expression = "FiveYearReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;
            }
            if (int.Parse(hdnReturnPeriod.Value) == 9)
            {
                expression = "InceptionReturn" + hdnCondition.Value;
                dtGetMFfund.DefaultView.RowFilter = expression;

            }
            //if (dtGetMFfund.DefaultView.Count > 0)
            //{
            gvMFFundPerformance.DataSource = dtGetMFfund.DefaultView;
            gvMFFundPerformance.DataBind();
            if (dtGetMFfund.DefaultView.Table.Rows.Count > 0)
            {
                trMFFundPerformance.Visible = true;
            }
            else if (dtGetMFfund.DefaultView.Table.Rows.Count == 0)
            {
                trMFFundPerformance.Visible = false;
            }

            if (Cache["FundPerformanceDetails" + advisorVo.advisorId] == null)
            {
                Cache.Insert("FundPerformanceDetails" + advisorVo.advisorId, dtGetMFfund.DefaultView);
            }
            else
            {
                Cache.Remove("FundPerformanceDetails" + advisorVo.advisorId);
                Cache.Insert("FundPerformanceDetails" + advisorVo.advisorId, dtGetMFfund.DefaultView);
            }

            //    ErrorMessage.Visible = false;
            //}
            //else
            //{
            //    ErrorMessage.Visible = true;
            //    //gvMFFundPerformance.Visible = false;
            //}
            Panel2.Visible = true;
            gvMFFundPerformance.Visible = true;
            //trMFFundPerformance.Visible = true;
        }

        //protected void gvMFFundPerformance_ItemDataBound(object sender, GridItemEventArgs e)
        //{            
        //    if (e.Item is GridCommandItem)
        //    {
        //        GridCommandItem cmditm = (GridCommandItem)e.Item;
        //        //to hide AddNewRecord button
        //        //cmditm.FindControl("InitInsertButton").Visible = false;//hide the text
        //        //cmditm.FindControl("AddNewRecordButton").Visible = false;//hide the image

        //        //to hide Refresh button
        //        //cmditm.FindControl("RefreshButton").Visible = false;//hide the text
        //        //cmditm.FindControl("RebindGridButton").Visible = false;//hide the image
        //    }
        //}

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                gvMFFundPerformance.ExportSettings.ExportOnlyData = true;
            }
        }

        protected void btnViewFactsheet_Click(object sender, EventArgs e)
        {
            ShowFactSheet();
        }

        public void ShowFactSheet()
        {
            tblFactSheet.Visible = true;
            DateTime factSheetDate;
            string monthName;
            //factSheetDate = Convert.ToDateTime(txtFactSheetDate.Text);
            PriceBo priceBo = new PriceBo();
            DataSet dsFactsheetschemeDetails = new DataSet();
            int schemePlanId = 0, month = 0, year = 0;

            // Get Month from Portfolio MF Page
            if (Request.QueryString["Month"] != null)
                month = int.Parse(Request.QueryString["Month"].ToString());
            else
                month = int.Parse(ddMonth.SelectedValue.ToString());

            // Get Year from Portfolio MF Page
            if (Request.QueryString["Year"] != null)
                year = int.Parse(Request.QueryString["Year"].ToString());
            else
                year = int.Parse(ddYear.SelectedValue.ToString());
            BindYear();
            ddYear.SelectedValue = year.ToString();


            monthName = GetMonthName(month);

            // Get SchemeCode from Portfolio MF Page
            if (Request.QueryString["SchemeCode"] != null)
                schemePlanId = int.Parse(Request.QueryString["SchemeCode"].ToString());
            else
                schemePlanId = int.Parse(ddlSchemeList.SelectedValue.ToString());

            //Get Scheme Name from Portfolio MF Page
            if (Request.QueryString["SchemeName"] != null)
                lblGetScheme.Text = Request.QueryString["SchemeName"].ToString();
            else
                lblGetScheme.Text = ddlSchemeList.SelectedItem.Text;


            lblGetMonth.Text = monthName;
            //month=RadDatePicker1
            dsFactsheetschemeDetails = priceBo.GetFactSheetSchemeDetails(schemePlanId, month, year);
            if (dsFactsheetschemeDetails.Tables[3].Rows.Count > 0)
            {
                tblFundHouseDetails.Visible = false;
                tblFactFundHouseDetails.Visible = true;
                lblgetWebsite.Text = dsFactsheetschemeDetails.Tables[3].Rows[0][3].ToString();
                lblgetAddress.Text = dsFactsheetschemeDetails.Tables[3].Rows[0]["Address"].ToString();
                lblgetAMC.Text = dsFactsheetschemeDetails.Tables[3].Rows[0]["PA_FundName"].ToString();
            }
            else
            {
                tblFundHouseDetails.Visible = true;
                tblFactFundHouseDetails.Visible = false;
                //lblgetWebsite.Text = "";
                //lblgetAddress.Text = "";
                //lblgetAMC.Text = "";
            }
            if (dsFactsheetschemeDetails.Tables[0].Rows.Count > 0)
            {
                tblFundObject.Visible = false;
                tblFactFundObject.Visible = true;
                lblObjPara.Text = dsFactsheetschemeDetails.Tables[0].Rows[0]["PAS_SchemeObjective"].ToString();
            }
            else
            {
                //lblObjPara.Text = "";
                tblFactFundObject.Visible = false;
                tblFundObject.Visible = true;
            }
            if (dsFactsheetschemeDetails.Tables[1].Rows.Count > 0)
            {
                tblInvInformation.Visible = false;
                tblFactInvInformation.Visible = true;
                lblgetSchemeType.Text = dsFactsheetschemeDetails.Tables[1].Rows[0]["SchemeType"].ToString();
                lblgetlunchDate.Text = dsFactsheetschemeDetails.Tables[1].Rows[0]["LaunchDate"].ToString();
                lblGetFundMgr.Text = dsFactsheetschemeDetails.Tables[1].Rows[0]["FundManager"].ToString();
                lblgetBenchMark.Text = dsFactsheetschemeDetails.Tables[1].Rows[0]["BenchMark"].ToString();
            }
            else
            {
                tblInvInformation.Visible = true;
                tblFactInvInformation.Visible = false;
                //lblgetSchemeType.Text = "";
                //lblgetlunchDate.Text = "";
                //lblGetFundMgr.Text = "";
                //lblgetBenchMark.Text = "";
            }
            if (dsFactsheetschemeDetails.Tables[2].Rows.Count > 0)
            {
                tblFundStructure.Visible = false;
                tblFactFundStructure.Visible = true;
                lblgetPERatio.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[2].Rows[0]["PASPEBR_PE"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetPBRatio.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[2].Rows[0]["PASPEBR_PB"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetAvgMkt.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[2].Rows[0]["PASPEBR_MarketCap"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
            else
            {
                tblFundStructure.Visible = true;
                tblFactFundStructure.Visible = false;
                //lblgetPERatio.Text = "";
                //lblgetPBRatio.Text = "";
                //lblgetAvgMkt.Text = "";
            }
            if (dsFactsheetschemeDetails.Tables[4].Rows.Count > 0)
            {
                tblFinancialDetails.Visible = false;
                tblFactFinancialDetails.Visible = true;
                lblgetNAV.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[4].Rows[0]["PSP_NetAssetValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetNAV52high.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[4].Rows[0]["PASPHL_52WeekHigh"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetNAV52Low.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[4].Rows[0]["PASPHL_52WeekLow"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetMinInvestment.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[4].Rows[0]["PASP_MinInvestment"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
            else
            {
                tblFinancialDetails.Visible = true;
                tblFactFinancialDetails.Visible = false;
                //lblgetNAV.Text = "";
                //lblgetNAV52high.Text = "";
                //lblgetNAV52Low.Text = "";
                //lblgetMinInvestment.Text = "";
            }
            if (dsFactsheetschemeDetails.Tables[5].Rows.Count > 0)
            {
                tblVolatality.Visible = false;
                tblFactVolatality.Visible = true;
                lblgetFama.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[5].Rows[0]["Fama"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetstdDev.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[5].Rows[0]["Std_Dev"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetBeta.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[5].Rows[0]["Beta"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblgetSharpe.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[5].Rows[0]["Sharpe"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
            else
            {
                tblVolatality.Visible = true;
                tblFactVolatality.Visible = false;
                //lblgetFama.Text = "";
                //lblgetstdDev.Text = "";
                //lblgetBeta.Text = "";
                //lblgetSharpe.Text = "";
            }
            if (dsFactsheetschemeDetails.Tables[8].Rows.Count > 0)
            {
                tblFactSchemePerformance.Visible = true;
                tblmsgSchemePerformance.Visible = false;
                lblGet3Month.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[8].Rows[0]["PASPABR_3MonthReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblGet6Month.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[8].Rows[0]["PASPABR_6MonthReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblGet1year.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[8].Rows[0]["PASPABR_1YearReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblGet3Years.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[9].Rows[0]["PASPAR_3YearReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblGet5Years.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[9].Rows[0]["PASPAR_5YearReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblGetSinceInception.Text = String.Format("{0:n2}", decimal.Parse(dsFactsheetschemeDetails.Tables[9].Rows[0]["PASPAR_InceptionReturn"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
            else
            {
                tblFactSchemePerformance.Visible = false;
                tblmsgSchemePerformance.Visible = true;
            }
            BindSectorWiseGrid(dsFactsheetschemeDetails);
            BindTop10Company(dsFactsheetschemeDetails);

            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove
            Request.QueryString.Remove("SchemeCode");
            Request.QueryString.Remove("SchemeName");
        }


        private string GetMonthName(int month)
        {
            string monthName = null;
            int i = month;
            switch (i)
            {
                case 1: monthName = "January";
                    break;
                case 2: monthName = "February";
                    break;
                case 3: monthName = "March";
                    break;
                case 4: monthName = "April";
                    break;
                case 5: monthName = "May";
                    break;
                case 6: monthName = "June";
                    break;
                case 7: monthName = "July";
                    break;
                case 8: monthName = "August";
                    break;
                case 9: monthName = "September";
                    break;
                case 10: monthName = "October";
                    break;
                case 11: monthName = "November";
                    break;
                case 12: monthName = "December";
                    break;
                default: monthName = "Invalid Month"; break;
            }
            return monthName;
        }

        private void BindSectorWiseGrid(DataSet dsFactsheetschemeDetails)
        {
            DataTable dtSectorWiseDetails = dsFactsheetschemeDetails.Tables[6];
            if (dtSectorWiseDetails.Rows.Count > 0)
            {
                gvSectorWiseHolding.DataSource = dtSectorWiseDetails;
                gvSectorWiseHolding.DataBind();
                gvSectorWiseHolding.Visible = true;
                trSector.Visible = false;
            }
            else
            {
                gvSectorWiseHolding.Visible = false;
                trSector.Visible = true;
            }
        }
        private void BindTop10Company(DataSet dsFactsheetschemeDetails)
        {

            DataTable dtTop10Company = dsFactsheetschemeDetails.Tables[7];
            if (dtTop10Company.Rows.Count > 0)
            {
                gvTop10Companies.DataSource = dtTop10Company;
                gvTop10Companies.DataBind();
                gvTop10Companies.Visible = true;
                trTop10Company.Visible = false;
            }
            else
            {
                gvTop10Companies.Visible = false;
                trTop10Company.Visible = true;
            }
        }

        protected void ddlAmcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmcCode.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlAmcCode.SelectedValue);
                if (ddlAmcCode.SelectedIndex != 0)
                {
                    LoadAllSchemeList(amcCode);
                }
                else
                    ddlSchemeList.SelectedItem.Text = "";
            }
        }
        protected void ddlNAVCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //BindNavSubCategory();
            LoadAllSchemeNAV();
            gvMFRecord.DataSource = null;
            gvMFRecord.DataBind();
        }
        private void LoadAllSchemeList(int amcCode)
        {
            PriceBo priceBo = new PriceBo();
            DataSet dsLoadAllScheme = new DataSet();
            DataTable dtLoadAllScheme = new DataTable();
            if (ddlAmcCode.SelectedIndex != 0 && ddlFactCategory.SelectedIndex == 0)
            {
                amcCode = int.Parse(ddlAmcCode.SelectedValue.ToString());
                categoryCode = ddlFactCategory.SelectedValue;
                //dtLoadAllScheme = priceBo.GetAllScehmeList(amcCode);
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }
            if (ddlAmcCode.SelectedIndex != 0 && ddlFactCategory.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlAmcCode.SelectedValue.ToString());
                categoryCode = ddlFactCategory.SelectedValue;
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }
            if (dtLoadAllScheme.Rows.Count > 0)
            {
                ddlSchemeList.DataSource = dtLoadAllScheme;
                ddlSchemeList.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlSchemeList.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlSchemeList.Items.Clear();
                ddlSchemeList.DataSource = null;
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
            }

        }
        protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMonth();
        }
         protected void rdMFMissingNAV_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMFMissingNAV = new DataTable();
            dtMFMissingNAV = (DataTable)Cache["MFMissingNAV" + advisorVo.advisorId.ToString()];
            rdMFMissingNAV.DataSource = dtMFMissingNAV;
        }
        
        protected void gvMFFundPerformance_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataView dtFundPerformanceDetailsDetails = new DataView();
            dtFundPerformanceDetailsDetails = (DataView)Cache["FundPerformanceDetails" + advisorVo.advisorId.ToString()];
            gvMFFundPerformance.DataSource = dtFundPerformanceDetailsDetails;
        }

        protected void gvEquityRecord_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //DataSet dtGvSchemeDetails = new DataSet();
            DataTable dtGvSchemeDetails = new DataTable();
            dtGvSchemeDetails = (DataTable)Cache["gvEquityRecord" + advisorVo.advisorId];
            gvEquityRecord.DataSource = dtGvSchemeDetails;

            gvEquityRecord.ExportSettings.OpenInNewWindow = true;
            gvEquityRecord.ExportSettings.IgnorePaging = true;
            gvEquityRecord.ExportSettings.HideStructureColumns = true;
            gvEquityRecord.ExportSettings.ExportOnlyData = true;
            gvEquityRecord.ExportSettings.FileName = "Equity Details";
            gvEquityRecord.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //gvEquityRecord.MasterTableView.ExportToExcel();
        }

        protected void gvMFRecord_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            {
                PriceBo PriceObj = new PriceBo();
                DataSet ds;
                lblIllegal.Visible = false;
                trgvEquityView.Visible = true;
                trExportFilteredMFRecord.Visible = true;

                if (rbtnCurrent.Checked)
                {
                    if (hdnassetType.Value == "Equity")
                    {
                        string Search = hdnCompanySearch.Value;

                        ds = PriceObj.GetEquitySnapshot("D", Search);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvEquityRecord.DataSource = ds;
                            gvEquityRecord.DataBind();
                            gvEquityRecord.Visible = true;
                            // DivEquity.Style.Add("display", "visible");
                            DivPager.Style.Add("display", "visible");
                            DivMF.Style.Add("display", "none");
                            Search = null;
                            hdnCompanySearch.Value = null;

                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            trgvEquityView.Visible = false;
                            gvEquityRecord.Visible = false;
                            //  trPageCount.Visible = false;
                            trPager.Visible = false;
                            trMfPagecount.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

                        }
                        btnSubmit.Visible = true;
                    }

                    else
                    {
                        int amfiCode = 0;
                        int selectAllCode = 0;
                        int schemeCode = 0;
                        int All = 0;
                        if (ddlSelectMutualFund.SelectedValue == "Select AMC Code" || ddlSelectSchemeNAV.SelectedIndex == -1 || ddlSelectSchemeNAV.SelectedValue == "")
                        {

                        }
                        else
                        {

                            amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                            if (ddlNAVCategory.SelectedIndex != 0)
                                All = 1;
                            selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
                            string CategoryCode = ddlNAVCategory.SelectedValue;
                            schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
                            string Search = hdnSchemeSearch.Value;
                            ds = PriceObj.GetAMFISnapshot("D", Search, amfiCode, schemeCode, selectAllCode, All, CategoryCode);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvMFRecord.DataSource = ds.Tables[0];
                                gvMFRecord.Visible = true;
                                Panel1.Visible = true;
                                DivMF.Style.Add("display", "visible");
                                // DivEquity.Style.Add("display", "none");
                                DivPager.Style.Add("display", "visible");

                                //DivMF.Style.Add("display", "visible");
                                //DivEquity.Style.Add("display", "none");
                                //DivPager.Style.Add("display", "visible");
                                Search = null;
                                hdnSchemeSearch.Value = null;
                                trExportFilteredMFRecord.Visible = true;

                                gvMFRecord.VirtualItemCount = ds.Tables[0].Rows.Count;
                                gvMFRecord.DataSource = ds;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
                            }
                        }
                    }
                }
                else if (rbtnHistorical.Checked)
                {
                    //if (txtFromDate.Text == "" || txtToDate.Text=="")
                    if (txtTo.SelectedDate.ToString() == "" || txtFrom.SelectedDate.ToString() == "")
                    {
                    }
                    else
                    {
                        DateTime StartDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
                        DateTime EndDate = DateTime.Parse(txtTo.SelectedDate.ToString());
                        hdnFromDate.Value = StartDate.ToString();
                        hdnToDate.Value = EndDate.ToString();

                        if (hdnassetType.Value == "Equity")
                        {
                            string Search = hdnCompanySearch.Value;

                            ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvEquityRecord.DataSource = ds;
                                gvEquityRecord.DataBind();
                                gvEquityRecord.Visible = true;
                                // DivEquity.Style.Add("display", "visible");
                                DivMF.Style.Add("display", "none");
                                DivPager.Style.Add("display", "visible");
                                tdToDate.Visible = true;
                                tdFromDate.Visible = true;
                                btnSubmit.Visible = true;
                            }

                            else
                            {
                                trgvEquityView.Visible = false;
                                gvEquityRecord.Visible = false;
                                // trPageCount.Visible = false;
                                trPager.Visible = false;
                                trMfPagecount.Visible = false;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
                            }
                            btnSubmit.Visible = true;
                        }
                        else if (hdnassetType.Value == "MF")
                        {
                            int All = 0;
                            int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                            int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
                            int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
                            string Search = hdnSchemeSearch.Value;
                            if (ddlNAVCategory.SelectedIndex != 0)
                                All = 1;
                            string CategoryCode = ddlNAVCategory.SelectedValue;
                            ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, amfiCode, schemeCode, selectAllCode, All, CategoryCode);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvMFRecord.VirtualItemCount = ds.Tables[0].Rows.Count;
                                gvMFRecord.DataSource = ds;
                                gvMFRecord.Visible = true;
                                Panel1.Visible = true;
                                DivMF.Style.Add("display", "visible");
                                // DivEquity.Style.Add("display", "none");
                                DivPager.Style.Add("display", "visible");
                                trExportFilteredMFRecord.Visible = true;

                            }
                            else
                            {
                                trgvEquityView.Visible = false;

                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records For This Period Of Time');", true);
                            }
                        }
                    }
                }
            }
        }
        
        protected void ddlFactCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmcCode.SelectedIndex != 0)
            {
                if (ddlFactCategory.SelectedIndex != 0)
                {
                    LoadAllSchemeList(amcCode);
                }

            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select AMC first');", true);
        }
        protected void btnExportFilteredMFRecord_OnClick(object sender, ImageClickEventArgs e)
        {
            gvMFRecord.ExportSettings.OpenInNewWindow = true;
            gvMFRecord.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMFRecord.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMFRecord.MasterTableView.ExportToExcel();
        }
        protected void btnExportMFFundPerformance_OnClick(object sender, ImageClickEventArgs e)
        {
            gvMFFundPerformance.ExportSettings.OpenInNewWindow = true;
            gvMFFundPerformance.ExportSettings.IgnorePaging = true;
            gvMFFundPerformance.ExportSettings.HideStructureColumns = true;
            gvMFFundPerformance.ExportSettings.ExportOnlyData = true;
            gvMFFundPerformance.ExportSettings.FileName = "Scheme Details";
            gvMFFundPerformance.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMFFundPerformance.MasterTableView.ExportToExcel();
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityRecord.ExportSettings.OpenInNewWindow = true;
            gvEquityRecord.ExportSettings.IgnorePaging = true;
            gvEquityRecord.ExportSettings.HideStructureColumns = true;
            gvEquityRecord.ExportSettings.ExportOnlyData = true;
            gvEquityRecord.ExportSettings.FileName = "Query Equity Data";
            gvEquityRecord.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityRecord.MasterTableView.ExportToExcel();
        }
        public void imgMissingNavExprt_OnClick(object sender, ImageClickEventArgs e)
        {
            rdMFMissingNAV.ExportSettings.OpenInNewWindow = true;
            rdMFMissingNAV.ExportSettings.IgnorePaging = true;
            rdMFMissingNAV.ExportSettings.HideStructureColumns = true;
            rdMFMissingNAV.ExportSettings.ExportOnlyData = true;
            rdMFMissingNAV.ExportSettings.FileName = "Missing NAV";
            rdMFMissingNAV.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdMFMissingNAV.MasterTableView.ExportToExcel();
        }      


    }
}
