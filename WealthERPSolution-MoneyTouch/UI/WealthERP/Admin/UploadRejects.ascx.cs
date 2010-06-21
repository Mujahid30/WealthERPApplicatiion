using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using VoWerpAdmin;
using VoUser;
using BoCommon;

namespace WealthERP.Admin
{
    public partial class UploadReject : System.Web.UI.UserControl
    {
        int processId = 0;
        UserVo userVo = null;
        string assetGroupStr = string.Empty;
        AssetGroupType assetGroupType = new AssetGroupType();
        public int updatedSnapshots =0 ;
        public int updatedHistory = 0;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            SetParameters();

            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                BindGrid();
            }
            
        }
        private void SetParameters()
        {
            if (Request.QueryString["assetGroup"] != null)
            {
                assetGroupStr = Request.QueryString["assetGroup"].Trim();
                if (assetGroupStr == "MF")
                    assetGroupType = AssetGroupType.MF;
                else if (assetGroupStr == "Equity")
                    assetGroupType = AssetGroupType.Equity;

                lblHeader.Text = assetGroupStr + " " + "Price Rejects";

            }
            if (Request.QueryString["processId"] != null)
                processId = Convert.ToInt32(Request.QueryString["processId"]);
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdminUploadProcessLog','?assetGroup=" + Request.QueryString["assetGroup"] + "');", true);
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            UploadRejectsBo uploadRejectsBo = new UploadRejectsBo();
            uploadRejectsBo.Reprocess(processId, UploadType.Price, assetGroupType, userVo.UserId,out updatedSnapshots,out updatedHistory);
            DisplayRejectsStatusMessage();
            BindGrid();
        }
        private void DisplayRejectsStatusMessage()
        {
            string strStatusMessage = "<div>Total Snapshots Updated :" + updatedSnapshots + " </div>" +
                "<div><br/>Total History Records Updated :"+ updatedHistory +"</div>";

            divStatus.Visible = true;
            divStatus.InnerHtml = strStatusMessage;
            

        }
        private void BindGrid()
        {
            int Count;
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }


            UploadReject uploadReject = new UploadReject();
            UploadRejectsBo uploadRejectsBo = new UploadRejectsBo();
            gvUploadReject.DataSource = uploadRejectsBo.GetRejectedRecords(processId, mypager.CurrentPage, out Count, UploadType.Price, assetGroupType);
            gvUploadReject.DataBind();

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



        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {

            SetParameters();
            GetPageCount();
            BindGrid();
        }

    }
}