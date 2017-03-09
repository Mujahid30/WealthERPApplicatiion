using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using VoWerpAdmin;
using BoWerpAdmin;
using WealthERP.Base;
using BoCommon;


namespace WealthERP.Admin
{
    public partial class Maintenance : System.Web.UI.UserControl
    {

        protected override void OnInit(EventArgs e)
        {

            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {

            if (ddlProductMaster.SelectedValue == Contants.ProductMaster.ScripMaster.ToString())
            {
                GetPageCount_Equity();
                DataSet ds = new DataSet();
                MaintenanceBo obj = new MaintenanceBo();
                ds = obj.GetScripMasterRecords(mypager.CurrentPage, "N");
                gvEquityMaintenance.DataSource = ds.Tables[0];
                gvEquityMaintenance.DataBind();
                gvEquityMaintenance.PageIndex = mypager.CurrentPage;
                DivPager.Style.Add("display", "visible");
                DivEquity.Style.Add("display", "visible");
                DivMF.Style.Add("display", "none");
            }

            if (ddlProductMaster.SelectedValue == Contants.ProductMaster.SchemeMaster.ToString())
            {
                GetPageCount_MF();
                DataSet ds = new DataSet();
                MaintenanceBo obj = new MaintenanceBo();
                ds = obj.GetSchemeMasterRecord(mypager.CurrentPage, "N");
                gvMFMaintenance.DataSource = ds.Tables[0];
                gvMFMaintenance.DataBind();
                gvMFMaintenance.PageIndex = mypager.CurrentPage;
                DivPager.Style.Add("display", "visible");
                DivMF.Style.Add("display", "visible");
                DivEquity.Style.Add("display", "none");
            }

        }

        private void GetPageCount_Equity()
        {
            string upperlimit;
            int rowCount = Convert.ToInt32(hdnEquityCount.Value);
            int ratio = rowCount / 10;
            mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            string lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
            upperlimit = (mypager.CurrentPage * 10).ToString();
            if (mypager.CurrentPage == mypager.PageCount)
                upperlimit = hdnEquityCount.Value;
            string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            lblgvEquityMaintenanceCurrentPage.Text = PageRecords;
        }

        private void GetPageCount_MF()
        {
            string upperlimit;
            int rowCount = Convert.ToInt32(hdnMFCount.Value);
            int ratio = rowCount / 10;
            mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
            string lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
            upperlimit = (mypager.CurrentPage * 10).ToString();
            if (mypager.CurrentPage == mypager.PageCount)
                upperlimit = hdnMFCount.Value;
            string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
            lblgvMFMaintenanceCurrentPage.Text = PageRecords;
        }

        protected void ProductMaster_SelectedIndexedChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MaintenanceBo obj = new MaintenanceBo();
            DivPager.Style.Add("display", "visible");
            if (ddlProductMaster.SelectedValue == Contants.ProductMaster.ScripMaster.ToString())
            {
                hdnEquityCount.Value = obj.GetScripMasterRecords("C").ToString();
                lblgvEquityMaintenanceTotalRows.Text = hdnEquityCount.Value;
                ds = obj.GetScripMasterRecords(1, "N");
                gvEquityMaintenance.DataSource = ds.Tables[0];
                gvEquityMaintenance.DataBind();
                DivEquity.Style.Add("display", "visible");
                DivMF.Style.Add("display", "none");
                mypager.CurrentPage = ds.Tables[0].Rows.Count > 0 ? 1 : 0;
                this.GetPageCount_Equity();


            }
            else if (ddlProductMaster.SelectedValue == Contants.ProductMaster.SchemeMaster.ToString())
            {
                hdnMFCount.Value = obj.GetSchemeMasterRecord("C").ToString();
                lblgvMFMaintenanceTotalRows.Text = hdnMFCount.Value;
                ds = obj.GetSchemeMasterRecord(1, "N");
                gvMFMaintenance.DataSource = ds.Tables[0];
                gvMFMaintenance.DataBind();
                DivEquity.Style.Add("display", "none");
                DivMF.Style.Add("display", "visible");
                mypager.CurrentPage = ds.Tables[0].Rows.Count > 0 ? 1 : 0;
                this.GetPageCount_MF();
            }

        }

        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
            int selectedRow = gvr.RowIndex;
            int WERPCODE = Convert.ToInt32(gvEquityMaintenance.Rows[selectedRow].Cells[0].Text);
            string RecordName = gvEquityMaintenance.Rows[selectedRow].Cells[1].Text;
            string Url = String.Empty;
            if (ddlAction.SelectedItem.Value.ToString() == Contants.Mode.Edit.ToString())
            {
                Url = String.Format("?RecordName={0}&WERPCODE={1}&Mode={2}&AssetGroup={3}&From={4}", RecordName, WERPCODE, Contants.Mode.Edit.ToString(), Contants.Source.Equity.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);

            }
            else if (ddlAction.SelectedItem.Value.ToString() == Contants.Mode.View.ToString())
            {
                Url = String.Format("?RecordName={0}&WERPCODE={1}&Mode={2}&AssetGroup={3}&From={4}", RecordName, WERPCODE, Contants.Mode.View.ToString(), Contants.Source.Equity.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);
            }

        }

        protected void ddlMFSelect_SelectedIndexChanged(Object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
            int selectedRow = gvr.RowIndex;
            int WERPCODE = Convert.ToInt32(gvMFMaintenance.Rows[selectedRow].Cells[0].Text);
            string RecordName = gvMFMaintenance.Rows[selectedRow].Cells[1].Text;
            string Url = String.Empty;
            if (ddlAction.SelectedItem.Value.ToString() == Contants.Mode.Edit.ToString())
            {
                Url = String.Format("?RecordName={0}&WERPCODE={1}&Mode={2}&AssetGroup={3}&From={4}", RecordName, WERPCODE, Contants.Mode.Edit.ToString(), Contants.Source.MF.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);

            }
            else if (ddlAction.SelectedItem.Value.ToString() == Contants.Mode.View.ToString())
            {
                Url = String.Format("?RecordName={0}&WERPCODE={1}&Mode={2}&AssetGroup={3}&From={4}", RecordName, WERPCODE, Contants.Mode.View.ToString(), Contants.Source.MF.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);
            }


        }

        protected void OnClick_AddNew(object sender, EventArgs e)
        {
            string Url = string.Empty;
            if (ddlProductMaster.SelectedValue == "0")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Validation", "Select at least one option");

            if (ddlProductMaster.SelectedValue == Contants.ProductMaster.ScripMaster.ToString())
            {
                Url = String.Format("?Mode={0}&AssetGroup={1}&From={2}", Contants.Mode.New.ToString(), Contants.Source.Equity.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);

            }
            else if (ddlProductMaster.SelectedValue == Contants.ProductMaster.SchemeMaster.ToString())
            {
                Url = String.Format("?Mode={0}&AssetGroup={1}&From={2}", Contants.Mode.New.ToString(), Contants.Source.MF.ToString(), "Main");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminMapping','" + Url + "');", true);
            }

        }

        protected void gvEquityMaintenance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataSet ds = new DataSet();
            //MaintenanceBo obj = new MaintenanceBo();
            //ds = obj.GetScripMasterRecords();
            //gvEquityMaintenance.DataSource = ds.Tables[0];
            //gvEquityMaintenance.PageIndex = e.NewPageIndex;
            //gvEquityMaintenance.DataBind();
            //gvEquityMaintenance.Visible = true;
            //DivEquity.Style.Add("display", "visible");
        }

        protected void gvMFMaintenance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataSet ds = new DataSet();
            //MaintenanceBo obj = new MaintenanceBo();
            //ds = obj.GetSchemeMasterRecord();
            //gvMFMaintenance.DataSource = ds.Tables[0];
            //gvMFMaintenance.PageIndex = e.NewPageIndex;
            //gvMFMaintenance.DataBind();
            //gvMFMaintenance.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }


    }
}