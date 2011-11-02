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

namespace WealthERP.SuperAdmin
{
    public partial class PriceListMonitor : System.Web.UI.UserControl
    {
        TextBox txtDateSearch;
        SuperAdminOpsBo saProductGoldPriceBo = new SuperAdminOpsBo();
        SuperAdminOpsVo saProductGoldPriceVo = new SuperAdminOpsVo();
        SuperAdminOpsDao saProductGoldPriceDao = new SuperAdminOpsDao();
        int ProductGoldPriceID;
        string goldId;

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

        private void BindProductGoldPriceGrid(int ProductGoldPriceID)
        {
            DataSet dsGoldPriceBetweenDates = new DataSet();
            
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }
           
            int Count;

            dsGoldPriceBetweenDates = saProductGoldPriceBo.GetDataBetweenDatesForGoldPrice(saProductGoldPriceVo, ProductGoldPriceID, mypager.CurrentPage, out Count);
           
            this.GetPageCount();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            cmpValidatorFutureDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            cmpCompareValidatorToDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");

            mypager.CurrentPage = 1;
            if (!IsPostBack)
            {
                mypager.Visible = false;
                this.BindProductGoldPriceGrid(ProductGoldPriceID);
                //this.BindGoldGridView();
            }
            ProductGoldPriceID = 0;
            //string strFromDate;
            //string strToDate;
            //strFromDate = "dd/mm/yyyy";
            //strToDate = "dd/mm/yyyy";
            //txtFromDate.Text = strFromDate.ToString();
            //txtToDate.Text = strToDate.ToString();
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if(!IsPostBack)
                hideControls();
            msgRecordStatus.Visible = false;

        }

        //private void BindGrid()
        //{
        //    try
        //    {

        //        if (hdnCurrentPage.Value.ToString() != "")
        //        {
        //            mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
        //            hdnCurrentPage.Value = "";
        //        }
                
        //        GetPageCount();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }           
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            int result = 0;
            saProductGoldPriceVo.PG_Date = DateTime.Parse(txtDate.Text);
            saProductGoldPriceVo.PG_Price = txtPrice.Text;
            try
            {

                result = saProductGoldPriceBo.InsertAndUpdateGoldPrice(saProductGoldPriceVo);

                if (result > 0)
                {
                    if (hdnButtonClick.Value == "addClick")
                    {
                        msgRecordStatusForSubmit.Visible = true;
                        msgRecordStatusForEdit.Visible = false;
                        //lblMessage.Visible = true;
                        //lblMessage.Text = "Price Submitted successfully";
                    }
                    else if (hdnButtonClick.Value == "editClick")
                    {
                        msgRecordStatusForEdit.Visible = true;
                        msgRecordStatusForSubmit.Visible = false;
                        //lblMessage.Visible = true;
                        //lblMessage.Text = "Price Updated successfully";
                    }
                    saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
                    saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
                    BindGoldGridView();
                    HideControlsForBtnAdd();
                }
                //mypager.Visible = true;
                //lblMsg.Visible = false;
                tblErrorMassage.Visible = false;
                
                

            }
            catch (Exception ee)
            {
                ee.StackTrace.ToString();
            }
            clearTextFields();
        }

        public void clearTextFields()
        {
            txtDate.Text = "";
            txtPrice.Text = "";
        }

        protected void btnShowBetweendates_Click(object sender, EventArgs e)
        {
            hideSuccessLabels();
            saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
            saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
            BindGoldGridView();
            btnAdd.Visible = true;
            hideControlsForGoClick();           
        }

        public void hideSuccessLabels()
        {
            msgRecordStatusForEdit.Visible = false;
            msgRecordStatusForSubmit.Visible = false;
        }


        public void hideControlsForGoClick()
        {
            lblDate.Visible = false;
            lblPrice.Visible = false;
            txtDate.Visible = false;
            txtPrice.Visible = false;
            btnSubmit.Visible = false;
            btnReset.Visible = false;
            lnkClickHereForPrice.Visible = false;
            lblMessage.Visible = false;
            msgRecordStatus.Visible = false;
        }

        public void hideControls()
        {
            btnReset.Visible = false;
            btnSubmit.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            lblDate.Visible = false;
            lblPrice.Visible = false;
            txtDate.Visible = false;
            txtPrice.Visible = false;
            lnkClickHereForPrice.Visible = false;
            //mypager.Visible = false;
            //lblCurrentPage.Visible = false;
            //lblTotalRows.Visible = false;
            lblMessage.Visible = false;
        }

        public void BindGoldGridView()
        {
            int count;
            DataSet ds = saProductGoldPriceBo.GetDataBetweenDatesForGoldPrice(saProductGoldPriceVo, ProductGoldPriceID, mypager.CurrentPage, out count);
           
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnDateFilter.Value = "";
                   // lblMsg.Visible = false;
                    tblErrorMassage.Visible = false;
                    GridViewDetails.DataSource = ds;
                    GridViewDetails.DataBind();
                    mypager.Visible = true;
                    hdnRecordCount.Value = count.ToString();
                    GetPageCount();
                    btnDelete.Visible = true;
                    btnEdit.Visible = true;
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
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnReset.Visible = false;
                }
            

            clearTextFields();
 
        }

        protected void txtDateSearch_TextChanged(object sender, EventArgs e)
        {
           
            txtDateSearch = (TextBox)GridViewDetails.HeaderRow.FindControl("txtDateSearch");
            
            if (txtDateSearch.Text=="")           
            {
                saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
                saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
                BindGoldGridView();
            }
            else
            {
                DateTime txtDate = DateTime.Parse(txtDateSearch.Text);
                DataSet ds = saProductGoldPriceBo.GetGoldPriceAccordingToDate(txtDate);
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    tblErrorMassage.Visible = false;
                    hdnDateFilter.Value = txtDateSearch.Text.Trim();
                    GridViewDetails.DataSource = ds;
                    GridViewDetails.DataBind();
                    mypager.Visible = true;
                    lblCurrentPage.Visible = true;
                    lblTotalRows.Visible = true;
                }
                else
                {
                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerText = "No Records found..";
                    hdnDateFilter.Value = txtDateSearch.Text.Trim();
                    GridViewDetails.DataSource = ds;
                    GridViewDetails.DataBind();
                    mypager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnAdd.Visible = false;
                }           
            }                        
        }

        

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            hideSuccessLabels();
            saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
            saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
          
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            for (int i = 0; i < GridViewDetails.Rows.Count; i++)
            {
                GridViewRow gridViewRow = GridViewDetails.Rows[i];
                CheckBox cb = (CheckBox)GridViewDetails.Rows[i].Cells[0].FindControl("chkSelect");
                {
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                             //string val = Convert.ToString(hdnMsgValue.Value);
                             //   if (val == "1")
                             //        {
                            hdnGoalId.Value = GridViewDetails.DataKeys[gridViewRow.RowIndex].Values["PG_ID"].ToString();
                                       
                                     //}
                            //if (saProductGoldPriceBo.deleteGoldPriceDetails(id)< 0)
                            //{
                            //    tblErrorMassage.Visible = true;
                            //    ErrorMessage.InnerHtml = "No Records found..";
                            //}

                            
                            
                            //tblErrorMassage.Visible = true;
                            break;
                        }
                    }
                }

            }
           
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            goldId = hdnGoalId.Value;
            if (val == "1")
            {
                saProductGoldPriceBo.deleteGoldPriceDetails(int.Parse(goldId));
                msgRecordStatus.Visible = true;
                saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
                saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);

                int count;
                DataSet ds = saProductGoldPriceBo.GetDataBetweenDatesForGoldPrice(saProductGoldPriceVo, ProductGoldPriceID, mypager.CurrentPage, out count);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnDateFilter.Value = "";
                    // lblMsg.Visible = false;
                    tblErrorMassage.Visible = false;
                    GridViewDetails.DataSource = ds;
                    GridViewDetails.DataBind();
                    mypager.Visible = true;
                    hdnRecordCount.Value = count.ToString();
                    GetPageCount();
                    btnDelete.Visible = true;
                    btnEdit.Visible = true;
                    lblCurrentPage.Visible = true;
                    lblTotalRows.Visible = true;
                }
                else
                {
                    GridViewDetails.DataSource = ds;
                    GridViewDetails.DataBind();
                    mypager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnReset.Visible = false;
                }
                clearTextFields();
            }
        }


        public void gridViewBindForProductGoldPrice()
        {
            DataSet allPGPDetails = saProductGoldPriceBo.GetAllGoldPriceDetails();
            GridViewDetails.DataSource = allPGPDetails;
            GridViewDetails.DataBind(); 
        }

        public void unHideControlsForBtnEdit()
        {
            lblPrice.Visible = true;
            lblDate.Visible = true;
            txtPrice.Visible = true;
            txtDate.Visible = true;
            lnkClickHereForPrice.Visible = true;
            btnReset.Visible = true;
            btnSubmit.Visible = true;
            mypager.Visible = true;
            msgRecordStatusForSubmit.Visible = false;
            msgRecordStatusForEdit.Visible = false;            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strEdit="editClick";
            hdnButtonClick.Value = strEdit.ToString();

            int id;

            for (int i = 0; i < GridViewDetails.Rows.Count; i++)
            {
                GridViewRow gridViewRow = GridViewDetails.Rows[i];
                CheckBox cb = (CheckBox)GridViewDetails.Rows[i].Cells[0].FindControl("chkSelect");
                {
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            
                            id = int.Parse(GridViewDetails.DataKeys[gridViewRow.RowIndex].Values["PG_ID"].ToString());
                            DataSet ds = saProductGoldPriceBo.GetGoldPriceAccordingToID(id);
                            DateTime pgDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PG_Date"].ToString());
                            string pgpDate = pgDate.ToShortDateString();
                            txtDate.Text = pgpDate;
                            txtPrice.Text = ds.Tables[0].Rows[0]["PG_Price"].ToString();
                            unHideControlsForBtnEdit();
                            break;
                        }
                        
                    }

                }

            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strAdd = "addClick";
            hdnButtonClick.Value = strAdd.ToString();
            
            unHideControlsForBtnAdd();
            
        }

        public void unHideControlsForBtnAdd()
        {
            btnSubmit.Visible = true;
            lblDate.Visible = true;
            lblPrice.Visible = true;
            txtDate.Visible = true;
            txtPrice.Visible = true;
            lnkClickHereForPrice.Visible = true;
            msgRecordStatusForSubmit.Visible = false;
            msgRecordStatusForEdit.Visible = false;
            //if (GridViewDetails.Visible == true)
            //{
            //    mypager.Visible = true;
            //}
            //else 
            //{
            //    mypager.Visible = false;
            //}
            //lblCurrentPage.Visible = false;
            //lblTotalRows.Visible = false;
            lblMessage.Visible = false;
            btnAdd.Visible = false;
           // lblMsg.Visible = false;
            tblErrorMassage.Visible = false;
        }
        public void HideControlsForBtnAdd()
        {
            btnReset.Visible = false;
            btnSubmit.Visible = false;
            lblDate.Visible = false;
            lblPrice.Visible = false;
            txtDate.Visible = false;
            txtPrice.Visible = false;
            lnkClickHereForPrice.Visible = false;
            //mypager.Visible = false;
            //lblCurrentPage.Visible = true;
            //lblTotalRows.Visible = true;
            lblMessage.Visible = true;
            btnAdd.Visible = true;
            //lblMsg.Visible = true;
            tblErrorMassage.Visible = true;
            ErrorMessage.InnerHtml = "No Records found..";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearTextFields();
            saProductGoldPriceVo.Pg_fromDate = DateTime.Parse(txtFromDate.Text);
            saProductGoldPriceVo.Pg_toDate = DateTime.Parse(txtToDate.Text);
            BindGoldGridView();
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
        //protected void GridViewDetails_PreRender(object sender, EventArgs e)
        //{
        //    GridView gv = (GridView)sender;
        //    GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        //    if (gvr != null)
        //    {
        //        gvr.Visible = true;
        //    }
        //}

        //protected void GridViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        //{   
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[0].Text = "Page " + (GridViewDetails.PageIndex + 1) + " of " + GridViewDetails.PageCount;
        //    }   
        //}
                
    }
}
