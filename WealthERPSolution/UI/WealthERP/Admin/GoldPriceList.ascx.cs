using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using BoCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Text;
using VoSuperAdmin;
using BoSuperAdmin;
using DaoSuperAdmin;
using System.Globalization;

namespace WealthERP.Admin
{
    public partial class GoldPriceList : System.Web.UI.UserControl
    {     
       
        SuperAdminOpsBo saProductGoldPriceBo = new SuperAdminOpsBo();
        SuperAdminOpsVo saProductGoldPriceVo = new SuperAdminOpsVo();
        SuperAdminOpsDao saProductGoldPriceDao = new SuperAdminOpsDao();
        int ProductGoldPriceID;    


        protected void Page_Load(object sender, EventArgs e)
        {
      
            cmpValidatorFutureDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            cmpCompareValidatorToDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");

            mypager.CurrentPage = 1;
            if (!IsPostBack)
            {
                mypager.Visible = false;
        
            }
            ProductGoldPriceID = 0;
            
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");           
            
        }


        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
            saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
            GetPageCount();
            BindGoldGridView();
        }

        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            if (hdnRecordCount.Value != "")
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
            if (rowCount > 0)
            {
                ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);

                lblCurrentPage.Text = PageRecords;
                lblTotalRows.Text = hdnRecordCount.Value.ToString();
                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        public void BindGoldGridView()
        {
            int count;
            DataSet ds = saProductGoldPriceBo.GetDataBetweenDatesForGoldPrice(saProductGoldPriceVo, ProductGoldPriceID, mypager.CurrentPage, out count);

            if (ds.Tables[0].Rows.Count > 0)
            {
            
         
                tblErrorMassage.Visible = false;
                GridViewDetails.DataSource = ds;
                GridViewDetails.DataBind();
                mypager.Visible = true;
                hdnRecordCount.Value = count.ToString();
                GetPageCount();
               
                lblCurrentPage.Visible = true;
                lblTotalRows.Visible = true;
            }
            else
            {
                tblErrorMassage.Visible = true;
                ErrorMessage.InnerText = "No Records found..";
                GridViewDetails.DataSource = ds;
                GridViewDetails.DataBind();
                mypager.Visible = false;
                lblCurrentPage.Visible = false;
                lblTotalRows.Visible = false;
              
            }    

        }

        protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPGPrice = e.Row.FindControl("lblPGPrice") as Label;

                if (lblPGPrice.Text.ToString() != "")
                {

                    lblPGPrice.Text = Math.Round(double.Parse(lblPGPrice.Text), 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")).ToString();
                }
            }
        }

        protected void btnShowBetweendates_Click(object sender, EventArgs e)
        {
            saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
            saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
            BindGoldGridView();
         
        }
      
    }
}