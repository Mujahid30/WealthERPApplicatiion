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
//using BoProductMaster;

namespace WealthERP.Admin
{
    public partial class PriceList : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        DataSet dsCategoryList = new DataSet();
        DataTable dtGetMFfund = new DataTable();
        string assetType = "";
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
            compDateValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            cvChkFutureDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            if (Request.QueryString["AssetId"] != null)
            {
                hdnassetType.Value = Request.QueryString["AssetId"].ToString();
            }
            if (hdnassetType.Value == "MF")
            {
                RadTabStrip1.Tabs[1].Visible = true;
                RadTabStrip1.Tabs[2].Visible = true;
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                trgvEquityView.Visible = false;
                trgrMfView.Visible = false;
                trPageCount.Visible = false;
                trPager.Visible = false;
                trMfPagecount.Visible = false;
                lblheader.Text = "MF Data Query";
                trSelectMutualFund.Visible = true;
                trSelectSchemeNAV.Visible = true;


                if (!IsPostBack)
                {
                    lblIllegal.Visible = false;
                    //trFromDate.Style.Add("display", "none");
                    //trToDate.Style.Add("display", "none");
                    trFromDate.Visible = false;
                    trToDate.Visible = false;

                    trbtnSubmit.Visible = false;
                    trSelectMutualFund.Visible = false;
                    BindMutualFundDropDowns();
                    BindSelectAMCDropdown();
                    trSelectMutualFund.Visible = false;
                    trSelectSchemeNAV.Visible = false;
                }
            }
            else if (hdnassetType.Value == "Equity")
            {
                lblIllegal.Visible = false;
                trbtnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
                //RadTabStrip1.Tabs[0].Visible = false;
                RadTabStrip1.Tabs[1].Visible = false;
                RadTabStrip1.Tabs[2].Visible = false;
                lblheader.Text = "Equity Data Query";
                pnlSchemeComparison.Visible = false;
                rbtnCurrent.Visible = true;
                rbtnHistorical.Visible = true;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
                trToDate.Visible = false;
                trFromDate.Visible = false;
            }
        }

        protected void rbtnCurrent_CheckedChanged(object sender, EventArgs e)
        {
            //trFromDate.Style.Add("display", "none");
            //trToDate.Style.Add("display", "none");
            trFromDate.Visible = false;
            trToDate.Visible = false;
            trbtnSubmit.Visible = true;
            trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            //if (IsPostBack)
            //{
            //    ddlSelectMutualFund.SelectedIndex = 0;
            //    ddlSelectSchemeNAV.SelectedIndex = 0;
            //}
            txtToDate.Text = "";
            txtFromDate.Text = "";
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
            trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            //if (IsPostBack)
            //{
            //    ddlSelectMutualFund.SelectedIndex = 0;
            //    ddlSelectSchemeNAV.SelectedIndex = 0;
            //}
            txtToDate.Text = "";
            txtFromDate.Text = "";

            if (rbtnHistorical.Checked)
            {
                //trFromDate.Style.Add("display", "block");
                //trToDate.Style.Add("display", "block");
                trFromDate.Visible = true;
                trToDate.Visible = true;
                trbtnSubmit.Visible = true;
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
            trToDate.Visible = false;
            trFromDate.Visible = false;
            trgrMfView.Visible = false;
            trgvEquityView.Visible = false;
            trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;
            if (hdnassetType.Value == "MF")
            {
                lblIllegal.Visible = false;
                trbtnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;
            }
            else if (hdnassetType.Value == "Equity")
            {
                trSelectSchemeNAV.Visible = false;
                trSelectMutualFund.Visible = false;
                lblIllegal.Visible = false;
                trbtnSubmit.Visible = false;
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
            ddlSelectMutualFund.Items.Insert(0, new ListItem("All AMC", "Select AMC Code"));
        }

        protected void ddlSelectMutualFund_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSchemeNAV();
        }
        public void LoadAllSchemeNAV()
        {
            if (ddlSelectMutualFund.SelectedIndex != 0)
            {
                PriceBo priceBo = new PriceBo();
                DataTable dtLoadAllSchemeNAV = new DataTable();
                dtLoadAllSchemeNAV = priceBo.GetAllScehmeList(int.Parse(ddlSelectMutualFund.SelectedValue));
                ddlSelectSchemeNAV.DataSource = dtLoadAllSchemeNAV;
                ddlSelectSchemeNAV.DataTextField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanName"].ToString();
                ddlSelectSchemeNAV.DataValueField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanCode"].ToString();
                ddlSelectSchemeNAV.DataBind();
                ddlSelectSchemeNAV.Items.Insert(0, new ListItem("All Scheme", "0"));
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
            trgrMfView.Visible = true;
            trPageCount.Visible = true;
            trPager.Visible = true;
            trMfPagecount.Visible = true;
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
                    hdnEquityCount.Value = PriceObj.GetEquityCountSnapshot("C", Search, mypager.CurrentPage).ToString();
                    lblTotalRows.Text = hdnEquityCount.Value;
                    GetPageCount_Equity();
                    ds = PriceObj.GetEquitySnapshot("D", Search, mypager.CurrentPage);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvEquityRecord.DataSource = ds;
                        gvEquityRecord.DataBind();
                        DivEquity.Style.Add("display", "visible");
                        DivPager.Style.Add("display", "visible");
                        DivMF.Style.Add("display", "none");
                        Search = null;
                        hdnCompanySearch.Value = null;
                        //trToDate.Visible = true;
                        //trFromDate.Visible = true;
                        trbtnSubmit.Visible = true;
                    }
                    else
                    {
                        trgvEquityView.Visible = false;
                        trgrMfView.Visible = false;
                        trPageCount.Visible = false;
                        trPager.Visible = false;
                        trMfPagecount.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

                    }
                    trbtnSubmit.Visible = true;
                }

                else
                {
                    int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                    int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
                    int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);

                    string Search = hdnSchemeSearch.Value;
                    hdnMFCount.Value = PriceObj.GetAMFICountSnapshot("C", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
                    lblMFTotalRows.Text = hdnMFCount.Value;
                    GetPageCount_MF();
                    ds = PriceObj.GetAMFISnapshot("D", Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvMFRecord.DataSource = ds.Tables[0]; ;
                        gvMFRecord.DataBind();
                        DivMF.Style.Add("display", "visible");
                        DivEquity.Style.Add("display", "none");
                        DivPager.Style.Add("display", "visible");
                        Search = null;
                        hdnSchemeSearch.Value = null;
                    }
                    else
                    {
                        trgvEquityView.Visible = false;
                        trgrMfView.Visible = false;
                        trPageCount.Visible = false;
                        trPager.Visible = false;
                        trMfPagecount.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);
                    }
                }
            }
            else if (rbtnHistorical.Checked)
            {
                DateTime StartDate = DateTime.Parse(txtFromDate.Text.ToString());
                DateTime EndDate = DateTime.Parse(txtToDate.Text.ToString());
                hdnFromDate.Value = StartDate.ToString();
                hdnToDate.Value = EndDate.ToString();

                if (hdnassetType.Value == "Equity")
                {
                    string Search = hdnCompanySearch.Value;
                    //Search = null;
                    hdnEquityCount.Value = PriceObj.GetEquityCount("C", StartDate, EndDate, Search, mypager.CurrentPage).ToString();
                    lblTotalRows.Text = hdnEquityCount.Value;
                    GetPageCount_Equity();
                    ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search, mypager.CurrentPage);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvEquityRecord.DataSource = ds;
                        gvEquityRecord.DataBind();
                        DivEquity.Style.Add("display", "visible");
                        DivMF.Style.Add("display", "none");
                        DivPager.Style.Add("display", "visible");
                        trToDate.Visible = true;
                        trFromDate.Visible = true;
                        trbtnSubmit.Visible = true;
                    }

                    else
                    {
                        trgvEquityView.Visible = false;
                        trgrMfView.Visible = false;
                        trPageCount.Visible = false;
                        trPager.Visible = false;
                        trMfPagecount.Visible = false;
                        trFromDate.Visible = true;
                        trToDate.Visible = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records');", true);

                    }
                    trbtnSubmit.Visible = true;
                    //hdnCompanySearch.Value = null;
                }
                else if (hdnassetType.Value == "MF")
                {
                    int amfiCode = int.Parse(ddlSelectMutualFund.SelectedValue);
                    int selectAllCode = ddlSelectSchemeNAV.SelectedIndex;
                    int schemeCode = int.Parse(ddlSelectSchemeNAV.SelectedValue);
                    string Search = hdnSchemeSearch.Value;
                    hdnMFCount.Value = PriceObj.GetAMFICount("C", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode).ToString();
                    lblMFTotalRows.Text = hdnMFCount.Value;
                    ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, mypager.CurrentPage, amfiCode, schemeCode, selectAllCode);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvMFRecord.DataSource = ds.Tables[0];
                        gvMFRecord.DataBind();
                        GetPageCount_MF();
                        DivPager.Style.Add("display", "visible");
                        DivMF.Style.Add("display", "visible");
                        DivEquity.Style.Add("display", "none");
                    }
                    else
                    {
                        trgvEquityView.Visible = false;
                        trgrMfView.Visible = false;
                        trPageCount.Visible = false;
                        trPager.Visible = false;
                        trMfPagecount.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('This Scheme Has No Records For This Period Of Time');", true);
                    }
                    //Search = null;
                    //hdnSchemeSearch.Value = null;
                }
            }
        }

        private string SchemeName()
        {
            string txt = string.Empty;
            TextBox txt1 = new TextBox();
            if ((TextBox)gvMFRecord.HeaderRow.FindControl("txtSchemeSearch") != null)
            {
                txt1 = (TextBox)gvMFRecord.HeaderRow.FindControl("txtSchemeSearch");
                txt = txt1.Text;
            }
            return txt;
        }

        private string CompanyName()
        {
            string txt3 = string.Empty;
            TextBox txt2 = new TextBox();
            if ((TextBox)gvEquityRecord.HeaderRow.FindControl("txtCompanySearch") != null)
            {
                txt2 = (TextBox)gvEquityRecord.HeaderRow.FindControl("txtCompanySearch");
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

            string categoryCode = ddlCategory.SelectedValue;
            dsCategoryList = priceBo.BindddlMFSubCategory();
            if (ddlCategory.SelectedIndex != 0)
            {
                if (categoryCode == "MFCO")
                {
                    ddlSubCategory.DataSource = dsCategoryList.Tables[0];
                    ddlSubCategory.DataTextField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    ddlSubCategory.DataValueField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    ddlSubCategory.DataBind();
                   // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
                }
                if (categoryCode == "MFDT")
                {
                    ddlSubCategory.DataSource = dsCategoryList.Tables[1];
                    ddlSubCategory.DataTextField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    ddlSubCategory.DataValueField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    ddlSubCategory.DataBind();
                    //ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
                }
                if (categoryCode == "MFEQ")
                {
                    ddlSubCategory.DataSource = dsCategoryList.Tables[2];
                    ddlSubCategory.DataTextField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    ddlSubCategory.DataValueField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    ddlSubCategory.DataBind();
                   // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
                }
                if (categoryCode == "MFHY")
                {
                    ddlSubCategory.DataSource = dsCategoryList.Tables[3];
                    ddlSubCategory.DataTextField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    ddlSubCategory.DataValueField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    ddlSubCategory.DataBind();
                   // ddlSubCategory.Items.Insert(0, new ListItem("All Sub Category", "0"));
                }
            }

        }

        //protected void ddlReturn_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int returnPeriod = int.Parse(ddlReturn.SelectedValue);
        //    if (ddlReturn.SelectedIndex != 0)
        //    {
        //        if (returnPeriod == 1)
        //        {
        //        }
        //        if (returnPeriod == 2)
        //        {
        //        }
        //        if (returnPeriod == 3)
        //        {
        //        }
        //        if (returnPeriod == 4)
        //        {
        //        }
        //        if (returnPeriod == 5)
        //        {
        //        }
        //        if (returnPeriod == 6)
        //        {
        //        }
        //        if (returnPeriod == 7)
        //        {
        //        }
        //        if (returnPeriod == 8)
        //        {
        //        }
        //        if (returnPeriod == 9)
        //        {
        //        }
        //    }

        //}

        //protected void ddlCondition_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string expression = string.Empty;


        //    string condition = ddlCondition.SelectedValue;
        //    int returnPeriod = int.Parse(ddlReturn.SelectedValue);
        //    if (ddlCondition.SelectedIndex != 0)
        //    {
        //         if (returnPeriod == 1)
        //        {
        //               expression = "OneWeekReturn" + condition;

        //        }
                  
                    
        //        }
        //        //if (condition == 2)
        //        //{
        //        //}
        //        //if (condition == 3)
        //        //{
        //        //}
        //        //if (condition == 4)
        //        //{
        //        //}
        //        //if (condition == 5)
        //        //{
        //        //}
        //        //if (condition == 6)
        //        //{
        //        //}
        //        //if (condition == 7)
        //        //{
        //        //}
        //        //if (condition == 8)
        //        //{
        //        //}
        //        //if (condition == 9)
        //        //{
        //        //}
        //        //if (condition == 10)
        //        //{
        //        //}
        //    }
        //}

        protected void OnClick_btnViewFactsheet(object sender, EventArgs e)
        {
            int amcCode = int.Parse(ddlSelectAMC.SelectedValue);
            //int selectSchemeCode = ddlSelectScheme.SelectedIndex;
            //int schemeCode = int.Parse(ddlSelectScheme.SelectedValue);
            string subCategory = ddlSubCategory.SelectedValue;
            int returnPeriod = int.Parse(ddlReturn.SelectedValue);
            string condition = ddlCondition.SelectedValue;
            string expression = string.Empty;
            dtGetMFfund = priceBo.GetMFFundPerformance(amcCode, subCategory);
              if (returnPeriod == 1)
              {
                   expression = "OneWeekReturn" + condition;
                   dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 2)
              {
                  expression = "OneMonthReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 3)
              {
                  expression = "ThreeMonthReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 4)
              {
                  expression = "SixMonthReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 5)
              {
                  expression = "OneYearReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 6)
              {
                  expression = "TwoYearReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 7)
              {
                  expression = "ThreeYearReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 8)
              {
                  expression = "FiveYearReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;
              }
              if (returnPeriod == 9)
              {
                  expression = "InceptionReturn" + condition;
                  dtGetMFfund.DefaultView.RowFilter = expression;

              }     
              gvMFFundPerformance.DataSource = dtGetMFfund.DefaultView;
              gvMFFundPerformance.DataBind();
            //  DataTable dt = dtGetMFfund.DefaultView.Table.Clone();
            //BindMFFundPerformance(dt);
        }

        //private void BindMFFundPerformance(DataTable dt)
        //{
        //    DataTable dtMFFundPerformance = new DataTable();
        //    DataRow drMFFundPerformance;
        //    try
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            dtMFFundPerformance.Columns.Add("SchemeName");
        //            //dtMFFundPerformance.Columns.Add("AUM");
        //            dtMFFundPerformance.Columns.Add("LaunchDate");
        //            dtMFFundPerformance.Columns.Add("NAV");
        //            //dtMFFundPerformance.Columns.Add("HighestNAV");
        //            //dtMFFundPerformance.Columns.Add("LowestNAV");
        //            //dtMFFundPerformance.Columns.Add("YTD");
        //            dtMFFundPerformance.Columns.Add("OneWeekReturn");
        //            dtMFFundPerformance.Columns.Add("OneMonthReturn");
        //            dtMFFundPerformance.Columns.Add("ThreeMonthReturn");
        //            dtMFFundPerformance.Columns.Add("SixMonthReturn");
        //            dtMFFundPerformance.Columns.Add("OneYearReturn");
        //            dtMFFundPerformance.Columns.Add("TwoYearReturn");
        //            dtMFFundPerformance.Columns.Add("ThreeYearReturn");
        //            dtMFFundPerformance.Columns.Add("FiveYearReturn");
        //            dtMFFundPerformance.Columns.Add("InceptionReturn");
        //            dtMFFundPerformance.Columns.Add("PE");
        //            dtMFFundPerformance.Columns.Add("PB");
        //            //dtMFFundPerformance.Columns.Add("Cash");
        //            dtMFFundPerformance.Columns.Add("Sharpe");
        //            dtMFFundPerformance.Columns.Add("SD");
        //            //dtMFFundPerformance.Columns.Add("Top 5 Holdings");

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                drMFFundPerformance = dtMFFundPerformance.NewRow();
        //                drMFFundPerformance["SchemeName"] = dr["SchemeName"].ToString();
        //                //drMFFundPerformance["AUM"] = dr["AUM"].ToString();
        //                drMFFundPerformance["LaunchDate"] = DateTime.Parse(dr["LaunchDate"].ToString()).ToShortDateString();
        //                drMFFundPerformance["NAV"] = dr["NAV"].ToString();
        //                //drMFFundPerformance["HighestNAV"] = dr["HighestNAV"].ToString();
        //                //drMFFundPerformance["LowestNAV"] = dr["LowestNAV"].ToString();
        //                //drMFFundPerformance["YTD"] = DateTime.Parse(dr["YTD"].ToString()).ToShortDateString();
        //                drMFFundPerformance["OneWeekReturn"] = dr["OneWeekReturn"].ToString();
        //                drMFFundPerformance["OneMonthReturn"] = dr["OneMonthReturn"].ToString();
        //                drMFFundPerformance["ThreeMonthReturn"] = dr["ThreeMonthReturn"].ToString();
        //                drMFFundPerformance["SixMonthReturn"] = dr["SixMonthReturn"].ToString();
        //                drMFFundPerformance["OneYearReturn"] = dr["OneYearReturn"].ToString();
        //                drMFFundPerformance["TwoYearReturn"] = dr["TwoYearReturn"].ToString();
        //                drMFFundPerformance["ThreeYearReturn"] = dr["ThreeYearReturn"].ToString();
        //                drMFFundPerformance["FiveYearReturn"] = dr["FiveYearReturn"].ToString();
        //                drMFFundPerformance["InceptionReturn"] = dr["InceptionReturn"].ToString();
        //                drMFFundPerformance["PE"] = dr["PE"].ToString();
        //                drMFFundPerformance["PB"] = dr["PB"].ToString();
        //                //drMFFundPerformance["Cash"] = dr["Cash"].ToString();
        //                drMFFundPerformance["Sharpe"] = dr["Sharpe"].ToString();
        //                drMFFundPerformance["SD"] = dr["SD"].ToString();
        //                //drMFFundPerformance["Top5Holdings"] = dr["Top5Holdings"].ToString();

        //                dtMFFundPerformance.Rows.Add(drMFFundPerformance);
        //            }
        //            gvMFFundPerformance.DataSource = dtMFFundPerformance;
        //            gvMFFundPerformance.DataBind();
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PriceList.ascx:BindgvMFFundPerformance()");
        //        object[] objects = new object[1];
        //        objects[0] = Session["FP_UserID"];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
    }
}