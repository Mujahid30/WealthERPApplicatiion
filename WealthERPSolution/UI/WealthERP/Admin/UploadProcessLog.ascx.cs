using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using BoCommon;

namespace WealthERP.Admin
{
    public partial class UploadProcessLog1 : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                BindGrid();
            }
            
        }
        private void BindGrid()
        {
            int Count;
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            ProductPriceUploadLogBo productPriceUploadLogBo = new ProductPriceUploadLogBo();
            gvProcessLog.DataSource = productPriceUploadLogBo.GetProcessLog(mypager.CurrentPage,out Count);
            gvProcessLog.DataBind();

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");
            GetPageCount();
        }
        private void GetPageCount()
        {
            string upperlimit;
            string lowerlimit;
            int rowCount = 0;
            if (hdnRecordCount.Value != "")
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
            if (rowCount > 0)
            {
                int ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }
        protected void gvProcessLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int processId = int.Parse(gvProcessLog.DataKeys[selectedRow].Values["ProcessId"].ToString());
                string assetType =  gvProcessLog.DataKeys[selectedRow].Values["assetClass"].ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdminUploadReject','?processId=" + processId + "&assetGroup="+assetType+"');", true);

            }
            catch (Exception ex)
            {
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {

            GetPageCount();
            BindGrid();
        }
    }
}