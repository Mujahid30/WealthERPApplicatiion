using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUploads;
using System.Data;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUploads;
using System.Configuration;
using BoCommon;
using System.Collections;

namespace WealthERP.Uploads
{
    public partial class RejectedFoliosTransactionUpload : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadCommonBo uploadsCommonBo;
        RejectedRecordsBo rejectedRecordsBo = new RejectedRecordsBo();
        int adviserid;

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

                int ratio = rowCount / 30;
                mypager.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = ((mypager.CurrentPage - 1) * 30).ToString();
                upperlimit = (mypager.CurrentPage * 30).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get Advisor Vo from Session
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            adviserid = adviserVo.advisorId;

            SessionBo.CheckSession();
            if(!IsPostBack)
            BindRejectedRecordsGrid();
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
                dsRejectedRecords = rejectedRecordsBo.GetUploadMFRejectsDistinctFolios(adviserid, mypager.CurrentPage, out Count);
                
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CAMSTransInputRejects.ascx:BindRejectedRecordsGrid()");

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
                gvRejectedFolios.DataSource = dsRejectedRecords.Tables[0];
                gvRejectedFolios.DataBind();
            }
            else
            {
                trInputNullMessage.Visible = true;
                gvRejectedFolios.DataSource = null;
                gvRejectedFolios.DataBind();
            }

            this.GetPageCount();

            
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }

        protected void btnMapToCustomer_Click(object sender, EventArgs e)
        {
            ArrayList Stagingtableid = new ArrayList();
            ArrayList ProcessId = new ArrayList();
            int i = 0;
            const int FOLIONUM_INDEX = 2;
            HiddenField hdnStagingTableid;
            HiddenField hdnAMCCode;
            string folionum;
            foreach (GridViewRow dr in gvRejectedFolios.Rows)
            {
                
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBxSelFolio");
                if (checkBox.Checked)
                {
                    hdnStagingTableid = (HiddenField)dr.FindControl("hdnBxSelStagingTabId");
                    Stagingtableid.Add(hdnStagingTableid.Value.ToString());

                    hdnAMCCode = (HiddenField)dr.FindControl("hdnBxSelAMCCode");
                    folionum = dr.Cells[FOLIONUM_INDEX].Text;
                    //Get all processid for selected folionum and amc code
                    DataSet dsProcessIds = rejectedRecordsBo.GetUploadProcessIDForSelectedFoliosANDAMC(adviserid, Convert.ToInt32(hdnAMCCode.Value.ToString()), folionum);

                    foreach (DataRow drprocessids in dsProcessIds.Tables[0].Rows)
                    {
                        ProcessId.Add(drprocessids["ProcessId"].ToString());
                    }

                    //ProcessId.Add(hdnAMCCode.Value.ToString());
                    i++;
                }
               
                    
            }

            //use a hashtable to create a unique list
            Hashtable ht = new Hashtable();

            foreach (string item in ProcessId)
            {
                //set a key in the hashtable for our arraylist value - leaving the hashtable value empty
                ht[item] = null;
                
            }

            //now grab the keys from that hashtable into another arraylist
            ArrayList distincProcessIds = new ArrayList(ht.Keys);

            
            //int selectedcount = ProcessId.Count;
            //string[] processids = new string[selectedcount];
            //for(i=0; i<selectedcount; i++)
            
            

            Session["Stagingtableid"] = Stagingtableid;
            Session["distincProcessIds"] = distincProcessIds;
            Response.Write("<script type='text/javascript'>detailedresults=window.open('Uploads/MapToCustomers.aspx','mywindow', 'width=700,height=450,scrollbars=yes,location=no');</script>");
        }

        

    }
}