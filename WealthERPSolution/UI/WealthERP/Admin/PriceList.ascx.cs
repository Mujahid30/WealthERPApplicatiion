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

namespace WealthERP.Admin
{
    public partial class PriceList : System.Web.UI.UserControl
    {

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
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            trgvEquityView.Visible = false;
            trgrMfView.Visible = false;
            trPageCount.Visible = false;
            trPager.Visible = false;
            trMfPagecount.Visible = false;

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
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;

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
            if (ddlAssetGroup.SelectedValue == Contants.Source.MF.ToString())
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
            if (ddlAssetGroup.SelectedValue == Contants.Source.MF.ToString())
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
            if (ddlAssetGroup.SelectedValue == "MF")
            {
                lblIllegal.Visible = false;
                trbtnSubmit.Visible = false;
                trSelectMutualFund.Visible = false;
                trSelectSchemeNAV.Visible = false;

            }
            else if (ddlAssetGroup.SelectedValue == "Equity")
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
            ddlSelectMutualFund.Items.Insert(0, new ListItem("Select AMC Code", "Select AMC Code"));

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




            if (ddlAssetGroup.SelectedValue == "0")
            {
                //lblIllegal.Visible = true;
                //lblIllegal.Text = "Select Asset";
                //trSelectMutualFund.Visible = false;
                //trSelectSchemeNAV.Visible = false;
            }
            else if (rbtnCurrent.Checked)
            {


                if (ddlAssetGroup.SelectedValue == Contants.Source.Equity.ToString())
                {
                    string Search = hdnCompanySearch.Value;
                    hdnEquityCount.Value = PriceObj.GetEquityCountSnapshot("C", Search, mypager.CurrentPage).ToString();
                    lblTotalRows.Text = hdnEquityCount.Value;
                    GetPageCount_Equity();
                    ds = PriceObj.GetEquitySnapshot("D", Search, mypager.CurrentPage);
                    gvEquityRecord.DataSource = ds;
                    gvEquityRecord.DataBind();
                    DivEquity.Style.Add("display", "visible");
                    DivPager.Style.Add("display", "visible");
                    DivMF.Style.Add("display", "none");
                    Search = null;
                    hdnCompanySearch.Value = null;



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



                if (ddlAssetGroup.SelectedValue == Contants.Source.Equity.ToString())
                {
                    string Search = hdnCompanySearch.Value;
                    //Search = null;
                    hdnEquityCount.Value = PriceObj.GetEquityCount("C", StartDate, EndDate, Search, mypager.CurrentPage).ToString();
                    lblTotalRows.Text = hdnEquityCount.Value;
                    GetPageCount_Equity();
                    ds = PriceObj.GetEquityRecord("D", StartDate, EndDate, Search, mypager.CurrentPage);
                    gvEquityRecord.DataSource = ds;
                    gvEquityRecord.DataBind();
                    DivEquity.Style.Add("display", "visible");
                    DivMF.Style.Add("display", "none");
                    DivPager.Style.Add("display", "visible");

                    //hdnCompanySearch.Value = null;

                }
                else if (ddlAssetGroup.SelectedValue == Contants.Source.MF.ToString())
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


            if (ddlAssetGroup.SelectedValue == Contants.Source.Equity.ToString())
            {
                string txtCompanyName = CompanyName();
                if (txtCompanyName != null)
                    hdnCompanySearch.Value = txtCompanyName;
            }

            else if (ddlAssetGroup.SelectedValue == Contants.Source.MF.ToString())
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


    }

}