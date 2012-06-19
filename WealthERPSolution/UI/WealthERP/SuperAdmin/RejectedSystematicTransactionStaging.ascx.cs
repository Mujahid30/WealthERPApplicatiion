using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUploads;
using VoCustomerProfiling;
using VoUser;
using BoUploads;
using BoCustomerProfiling;
using BoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;


namespace WealthERP.SuperAdmin
{
    public partial class RejectedSystematicTransactionStaging : System.Web.UI.UserControl
    {
        KarvyUploadsVo karvyUploadsVo = new KarvyUploadsVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo = new UploadProcessLogVo();
        RMVo rmVo = new RMVo();

        string ValidationProgress = "";

        CustomerBo customerBo = new CustomerBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();
        UploadValidationBo uploadsvalidationBo = new UploadValidationBo();
        UserBo userBo = new UserBo();
        int processId;
        string configPath;
        DataSet dsRejectedSIP = new DataSet();
        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            processId = 0;
            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            GetPageCount();
            this.BindRejectedSIPGrid(processId);
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
                lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
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

            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            processId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (!Page.IsPostBack)
            {
                BindRejectedSIPGrid(processId);

            }


        }

        private void BindRejectedSIPGrid(int processId)
        {
            DataSet dsRejectedSIP = new DataSet();
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }
            hdnProcessIdFilter.Value = processId.ToString();
            int Count;

            dsRejectedSIP = uploadsCommonBo.GetSuperAdminRejectedSIPRecords(mypager.CurrentPage, out Count, int.Parse(hdnProcessIdFilter.Value), hdnRejectReasonFilter.Value, hdnFileNameFilter.Value, hdnFolioFilter.Value, hdnTransactionTypeFilter.Value, hdnInvNameFilter.Value, hdnSchemeNameFilter.Value, hdnAdviserFilter.Value);
            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");


            if (dsRejectedSIP.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                trMessage.Visible = false;
                trReprocess.Visible = true;
                gvSIPReject.DataSource = dsRejectedSIP.Tables[0];
                gvSIPReject.DataBind();



                foreach (DataRow dr in dsRejectedSIP.Tables[1].Rows)
                {
                    genDictRejectReason.Add(dr["WRR_RejectReasonDescription"].ToString(), dr[1].ToString());
                }
                DropDownList ddlRejectReason = GetRejectReasonDDL();
                if (ddlRejectReason != null)
                {
                    ddlRejectReason.DataSource = genDictRejectReason;
                    ddlRejectReason.DataTextField = "Key";
                    ddlRejectReason.DataValueField = "Value";
                    ddlRejectReason.DataBind();
                    ddlRejectReason.Items.Insert(0, new ListItem("Select", "Select"));
                }
                if (hdnRejectReasonFilter.Value != "")
                {
                    ddlRejectReason.SelectedValue = hdnRejectReasonFilter.Value.ToString().Trim();
                }
                BindProcessId(dsRejectedSIP.Tables[2]);
                BindFolioNumber(dsRejectedSIP.Tables[3]);
                BindInvName(dsRejectedSIP.Tables[4]);
                BindSchemeName(dsRejectedSIP.Tables[5]);
                BindTransactionType(dsRejectedSIP.Tables[6]);
                BindFileName(dsRejectedSIP.Tables[7]);
                BindAdviserName(dsRejectedSIP.Tables[9]);

            }
            else
            {
                hdnRecordCount.Value = "0";
                gvSIPReject.DataSource = null;
                gvSIPReject.DataBind();
                trMessage.Visible = true;
                trReprocess.Visible = false;
            }
            this.GetPageCount();
        }



        //protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
        //    {

        //        GridEditFormItem gridEditFormItem = (GridEditFormItem)e.Item;
        //        DropDownList dropDownList = (DropDownList)gridEditFormItem["Container"].FindControl("ddlRejectCode");
        //        dropDownList.DataSourceID = "AccessDataSource1";
        //        dropDownList.DataTextField = "ContactName";
        //        dropDownList.DataValueField = "ContactName";
        //        dropDownList.SelectedIndex = 3;
        //        dropDownList.DataBind();
        //    }
        //} 

        //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{

        //    this.gvSIPReject.DataSource = dsRejectedSIP.Tables[1];
        //}


        protected void lnkBtnBackToUploadLog_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
        }


        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcessId = GetProcessIdDDL();

            if (ddlProcessId != null)
            {
                if (ddlProcessId.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                    processId = int.Parse(hdnProcessIdFilter.Value);
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "0";
                    BindRejectedSIPGrid(processId);
                }
            }
        }

        protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlInvName = GetInvNameDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (ddlInvName != null)
            {
                if (ddlInvName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnInvNameFilter.Value = ddlInvName.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnInvNameFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }

        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReject = GetRejectReasonDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRejectReasonFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }


        protected void ddlFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFileName = GetFileNameDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (ddlFileName != null)
            {
                if (ddlFileName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnFileNameFilter.Value = ddlFileName.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnFileNameFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }

        protected void ddlFolioNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFolioNumber = GetFolioNumberDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (ddlFolioNumber != null)
            {
                if (ddlFolioNumber.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnFolioFilter.Value = ddlFolioNumber.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnFolioFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }

        protected void ddlSchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSchemeName = GetSchemeNameDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());


            if (ddlSchemeName != null)
            {
                if (ddlSchemeName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnSchemeNameFilter.Value = ddlSchemeName.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnSchemeNameFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }
        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlRejectReason");
            }
            return ddl;
        }


        private void BindProcessId(DataTable dtProcessId)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtProcessId.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtProcessId.Rows)
                {
                    genDictPanNum.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlProcessId = GetProcessIdDDL();
                if (ddlProcessId != null)
                {
                    ddlProcessId.DataSource = genDictPanNum;
                    ddlProcessId.DataTextField = "Key";
                    ddlProcessId.DataValueField = "Value";
                    ddlProcessId.DataBind();
                    ddlProcessId.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnProcessIdFilter.Value != "")
                {
                    ddlProcessId.SelectedValue = hdnProcessIdFilter.Value.ToString().Trim();
                }
            }
        }

        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlProcessId");
            }
            return ddl;
        }

        private void BindFolioNumber(DataTable dtFolioNumber)
        {
            Dictionary<string, string> genDictFolioNumber = new Dictionary<string, string>();
            if (dtFolioNumber.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFolioNumber.Rows)
                {
                    genDictFolioNumber.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlFolioNumber = GetFolioNumberDDL();
                if (ddlFolioNumber != null)
                {
                    ddlFolioNumber.DataSource = genDictFolioNumber;
                    ddlFolioNumber.DataTextField = "Key";
                    ddlFolioNumber.DataValueField = "Value";
                    ddlFolioNumber.DataBind();
                    ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnFolioFilter.Value != "")
                {
                    ddlFolioNumber.SelectedValue = hdnFolioFilter.Value.ToString();
                }
            }
        }

        //Methods with respect to Transaction Type Filter
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTransactionType = GetTransactionTypeDDL();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (ddlTransactionType != null)
            {
                if (ddlTransactionType.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnTransactionTypeFilter.Value = ddlTransactionType.SelectedValue;
                    BindRejectedSIPGrid(processId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTransactionTypeFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }


        private DropDownList GetFolioNumberDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlFolioNumber") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlFolioNumber");
            }
            return ddl;
        }

        private void BindInvName(DataTable dtInvName)
        {
            Dictionary<string, string> genDictInvName = new Dictionary<string, string>();
            if (dtInvName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtInvName.Rows)
                {
                    genDictInvName.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlInvName = GetInvNameDDL();
                if (ddlInvName != null)
                {
                    ddlInvName.DataSource = genDictInvName;
                    ddlInvName.DataTextField = "Key";
                    ddlInvName.DataValueField = "Value";
                    ddlInvName.DataBind();
                    ddlInvName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnInvNameFilter.Value != "")
                {
                    ddlInvName.SelectedValue = hdnInvNameFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetInvNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlInvName") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlInvName");
            }
            return ddl;
        }


        private void BindSchemeName(DataTable dtSchemeName)
        {
            Dictionary<string, string> genDictSchemeName = new Dictionary<string, string>();
            if (dtSchemeName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtSchemeName.Rows)
                {
                    genDictSchemeName.Add(dr[0].ToString(), dr[1].ToString());
                }

                DropDownList ddlSchemeName = GetSchemeNameDDL();
                if (ddlSchemeName != null)
                {
                    ddlSchemeName.DataSource = genDictSchemeName;
                    ddlSchemeName.DataTextField = "Key";
                    ddlSchemeName.DataValueField = "Value";
                    ddlSchemeName.DataBind();
                    ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnSchemeNameFilter.Value != "")
                {
                    ddlSchemeName.SelectedValue = hdnSchemeNameFilter.Value.ToString();
                }
            }
        }


        private DropDownList GetSchemeNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlSchemeName") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlSchemeName");
            }
            return ddl;
        }


        private void BindTransactionType(DataTable dtTransactionType)
        {
            Dictionary<string, string> genDictTransactionType = new Dictionary<string, string>();
            if (dtTransactionType.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtTransactionType.Rows)
                {
                    genDictTransactionType.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlTransationType = GetTransactionTypeDDL();
                if (ddlTransationType != null)
                {
                    ddlTransationType.DataSource = genDictTransactionType;
                    ddlTransationType.DataTextField = "Key";
                    ddlTransationType.DataValueField = "Value";
                    ddlTransationType.DataBind();
                    ddlTransationType.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnTransactionTypeFilter.Value != "")
                {
                    ddlTransationType.SelectedValue = hdnTransactionTypeFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetTransactionTypeDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlTransactionType") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlTransactionType");
            }
            return ddl;
        }

        private void BindFileName(DataTable dtFileName)
        {
            Dictionary<string, string> genDictFileName = new Dictionary<string, string>();
            if (dtFileName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFileName.Rows)
                {
                    genDictFileName.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlFileName = GetFileNameDDL();
                if (ddlFileName != null)
                {
                    ddlFileName.DataSource = genDictFileName;
                    ddlFileName.DataTextField = "Key";
                    ddlFileName.DataValueField = "Value";
                    ddlFileName.DataBind();
                    ddlFileName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnFileNameFilter.Value != "")
                {
                    ddlFileName.SelectedValue = hdnFileNameFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetFileNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlFileName") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlFileName");
            }
            return ddl;
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            string error = "";
            int processIdReprocessAll = 0;


            if (Request.QueryString["processId"] != null)
            {
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
                processlogVo = uploadsCommonBo.GetProcessLogInfo(processId);

                blResult = MFWERPSIPTransactionInsertion(processId);

            }

            else
            {
                DataSet ds = uploadsCommonBo.GetSuperAdminSIPUploadRejectDistinctProcessIdForAdviser();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    processIdReprocessAll = int.Parse(dr["WUPL_ProcessId"].ToString());
                    processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);

                    blResult = MFWERPSIPTransactionInsertion(processIdReprocessAll);

                    if (blResult == false)
                    {
                        error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
                    }
                }

            }

            if (error == "")
            {
                // Success Message
                //trErrorMessage.Visible = true;
                //lblError.Text = "Reprocess Done Successfully!";
                msgReprocessComplete.Visible = true;

            }
            else
            {
                // Failure Message
                trErrorMessage.Visible = true;
                msgReprocessincomplete.Visible = true;
                lblError.Text = "ErrorStatus:" + error;
            }

            BindRejectedSIPGrid(processId);
        }


        private bool MFWERPSIPTransactionInsertion(int UploadProcessId)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
            bool camsSIPCommonStagingChk = false;
            bool camsSIPCommonStagingToWERP = false;
            bool updateProcessLog = false;
            string packagePath;


            processlogVo = uploadsCommonBo.GetProcessLogInfo(UploadProcessId);

            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");

            camsSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(UploadProcessId, packagePath, configPath, "CA");
            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "CA");

            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
            if (camsSIPCommonStagingChk)
            {
                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                camsSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                if (camsSIPCommonStagingToWERP)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.EndTime = DateTime.Now;
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "CA");
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                }
            }
            return blResult;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;


            foreach (GridViewRow gvr in this.gvSIPReject.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
                msgDelete.Visible = false;
            }
            else
            {
                msgDelete.Visible = true;
                CustomerSIPTransactionDelete();
                msgDelete.Visible = true;
            }

        }


        private void CustomerSIPTransactionDelete()
        {
            foreach (GridViewRow gvr in this.gvSIPReject.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    int StagingID = int.Parse(gvSIPReject.DataKeys[gvr.RowIndex].Values["CMFSCS_ID"].ToString());

                    uploadsCommonBo.DeleteMFSIPTransactionStaging(StagingID);

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
                    BindRejectedSIPGrid(processId);


                }
            }

        }

        private void BindAdviserName(DataTable dtAdviser)
        {
            Dictionary<string, string> genOrganizationData = new Dictionary<string, string>();
            if (dtAdviser.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAdviser.Rows)
                {
                    genOrganizationData.Add(dr[0].ToString(), dr[0].ToString());
                }
                DropDownList ddlAdviserNameDate = GetOrganization();
                if (ddlAdviserNameDate != null)
                {
                    ddlAdviserNameDate.DataSource = genOrganizationData;
                    ddlAdviserNameDate.DataTextField = "Key";
                    ddlAdviserNameDate.DataValueField = "Value";
                    ddlAdviserNameDate.DataBind();
                    ddlAdviserNameDate.Items.Insert(0, new ListItem("Select", "Select"));
                }
                if (hdnAdviserFilter.Value != "")
                {
                    ddlAdviserNameDate.SelectedValue = hdnAdviserFilter.Value;
                }

            }
        }

        protected void ddlAdviserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAdviserName = GetOrganization();

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (ddlAdviserName != null)
            {
                if (ddlAdviserName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values

                    hdnAdviserFilter.Value = ddlAdviserName.SelectedItem.Text;
                    BindRejectedSIPGrid(processId);
                    // ddlAdviserName.SelectedItem.Text = hdnAdviserNameAUMFilter.Value;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnAdviserFilter.Value = "";
                    BindRejectedSIPGrid(processId);
                }
            }
        }

        private DropDownList GetOrganization()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.HeaderRow.FindControl("ddlAdviserName") != null)
            {
                ddl = (DropDownList)gvSIPReject.HeaderRow.FindControl("ddlAdviserName");
            }
            return ddl;
        }

    }
}