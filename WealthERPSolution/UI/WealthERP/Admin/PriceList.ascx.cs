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
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            if (!IsPostBack)
            {  
                
                lblIllegal.Visible = false;
                trFromDate.Style.Add("display", "none");
                trToDate.Style.Add("display", "none");
            }

        }



        protected void rbtnCurrent_CheckedChanged(object sender, EventArgs e)
        {
            trFromDate.Style.Add("display", "none");
            trToDate.Style.Add("display", "none");
        }
       


        protected void rbtnHistorical_CheckedChanged(object sender, EventArgs e)
        {

            hdnSchemeSearch.Value = null;
            hdnCompanySearch.Value = null;
            if (rbtnHistorical.Checked)
            {
                trFromDate.Style.Add("display", "block");
                trToDate.Style.Add("display", "block");
            }

        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            PriceBo PriceObj = new PriceBo();
            DataSet ds;
            lblIllegal.Visible = false;
            //hdnSchemeSearch.Value = null;
            //hdnCompanySearch.Value = null;



           
            if (ddlAssetGroup.SelectedValue == "0")
            {
                lblIllegal.Visible = true;
                lblIllegal.Text = "Select Asset";

             }
            else if (rbtnCurrent.Checked  )
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

                       
                        string Search = hdnSchemeSearch.Value;
                        hdnMFCount.Value = PriceObj.GetAMFICountSnapshot("C", Search, mypager.CurrentPage).ToString();
                        lblMFTotalRows.Text = hdnMFCount.Value;
                        GetPageCount_MF();
                        ds = PriceObj.GetAMFISnapshot("D", Search, mypager.CurrentPage);
                        gvMFRecord.DataSource = ds.Tables[0]; ;
                        gvMFRecord.DataBind();
                        DivMF.Style.Add("display", "visible");
                        DivEquity.Style.Add("display", "none");
                        DivPager.Style.Add("display", "visible");
                        Search = null;
                        hdnSchemeSearch.Value = null;
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
                   

                    string Search = hdnSchemeSearch.Value;
                    hdnMFCount.Value = PriceObj.GetAMFICount("C", StartDate, EndDate, Search, mypager.CurrentPage).ToString();
                    lblMFTotalRows.Text = hdnMFCount.Value;
                    ds = PriceObj.GetAMFIRecord("D", StartDate, EndDate, Search, mypager.CurrentPage);
                    gvMFRecord.DataSource = ds.Tables[0];
                    gvMFRecord.DataBind();
                    GetPageCount_MF();
                    DivPager.Style.Add("display", "visible");
                    DivMF.Style.Add("display", "visible");
                    DivEquity.Style.Add("display", "none");
                    //Search = null;
                    //hdnSchemeSearch.Value = null;
                    
                }
            }

        }

        private string  SchemeName()
        {
            string txt = string.Empty;
            TextBox txt1 = new TextBox();
            if ((TextBox)gvMFRecord.HeaderRow.FindControl("txtSchemeSearch") != null)
            {
                txt1 =(TextBox)gvMFRecord.HeaderRow.FindControl("txtSchemeSearch");
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
                string  txtSchemeName = SchemeName();

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
            string lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
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
            int ratio = rowCount / 10;
            mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            string lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
            upperlimit = (mypager.CurrentPage * 10).ToString();
            if (mypager.CurrentPage == mypager.PageCount)
                upperlimit = hdnMFCount.Value;
            string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            lblMFCurrentPage.Text = PageRecords;
            hdnCurrentPage.Value = mypager.CurrentPage.ToString();

        }


    }

}