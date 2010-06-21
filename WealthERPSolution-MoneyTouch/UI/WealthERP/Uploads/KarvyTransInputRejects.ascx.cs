using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using VoUploads;
using BoUploads;
using BoCommon;
using WealthERP.Base;
using System.Configuration;

namespace WealthERP.Uploads
{
    public partial class KarvyTransInputRejects : System.Web.UI.UserControl
    {
        UserVo userVo;
        RejectedRecordsBo rejectedRecordsBo = new RejectedRecordsBo();
        

        int processID;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            GetPageCount();
            this.BindRejectedRecordsGrid();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];

            SessionBo.CheckSession();

            if (!IsPostBack)
            {
                processID = Int32.Parse(Request.QueryString["processId"].ToString());
                trError.Visible = false;

                BindRejectedRecordsGrid();
            }
        }

        void BindRejectedRecordsGrid()
        {
            DataSet dsRejectedRecords;
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count;

            try
            {
                dsRejectedRecords = rejectedRecordsBo.GetTransInputRejects(processID, "KA", mypager.CurrentPage, out Count);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "KarvyTransInputRejects.ascx:BindRejectedRecordsGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {
                trInputNullMessage.Visible = false;
                gvRejectedRecords.DataSource = dsRejectedRecords.Tables[0];
                gvRejectedRecords.DataBind();
            }
            else
            {
                trInputNullMessage.Visible = true;
                gvRejectedRecords.DataSource = null;
                gvRejectedRecords.DataBind();
            }

            this.GetPageCount();
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
    }
}