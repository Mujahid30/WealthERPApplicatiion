using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Admin
{
    public partial class WERPAdminDownloadProcessLog : System.Web.UI.UserControl
    {
        DataSet getProcessLogDs;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            GetPageCount();
            this.BindProcessGrid();
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

                int ratio = rowCount / 15;
                mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = ((mypager.CurrentPage - 1) * 15).ToString();
                upperlimit = (mypager.CurrentPage * 15).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["userVo"] != null)
            {

            }
            else
            {
                Session.Clear();
                Session.Abandon();

                // If User Sessions are empty, load the login control 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);
            }

            if (!IsPostBack)
            {
               

                trError.Visible = false;
                trTransactionMessage.Visible = false;
                BindProcessGrid();
            }
            

        }
        private void BindProcessGrid()
        {
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count;
            ProductPriceDownloadLogBo productPriceDownloadLogBo = new ProductPriceDownloadLogBo();

            try
            {
                getProcessLogDs = productPriceDownloadLogBo.GetProcessLog(mypager.CurrentPage, out Count);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewUploadProcessLog.ascx:BindProcessHistoryGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");

            if (getProcessLogDs.Tables[0].Rows.Count > 0)
            {
                trTransactionMessage.Visible = false;
                gvProcessLog.DataSource = getProcessLogDs.Tables[0];
                gvProcessLog.DataBind();
            }
            else
            {
                trTransactionMessage.Visible = true;
                gvProcessLog.DataSource = null;
                gvProcessLog.DataBind();
            }

            this.GetPageCount();


            //ProductPriceDownloadLogBo productPriceDownloadLogBo = new ProductPriceDownloadLogBo();
            //DataSet ds = null;
            //ds = productPriceDownloadLogBo.GetProcessLog();
            //gvProcessLog.DataSource = ds;
            //gvProcessLog.DataBind();
        }
    }
}