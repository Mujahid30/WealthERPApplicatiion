﻿using System;
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

namespace WealthERP.SuperAdmin
{
    public partial class RejectedMFTransactionStaging : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;

        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;
        StandardProfileUploadBo standardProfileUploadBo;

        DataSet dsRejectedRecords;

        int ProcessId;
        int adviserId;
        int filetypeId;

        string configPath;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            ProcessId = 0;
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            GetPageCount();
            this.BindEquityTransactionGrid(ProcessId);
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
            SessionBo.CheckSession();
            btnReprocess.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

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

            // Get Advisor Vo from Session
            // adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                hdnProcessIdFilter.Value = ProcessId.ToString();
                // Bind Grid
                BindEquityTransactionGrid(ProcessId);
            }
        }

        private void BindEquityTransactionGrid(int ProcessId)
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count;

            rejectedRecordsBo = new RejectedRecordsBo();

            dsRejectedRecords = rejectedRecordsBo.GetSuperAdminUploadRejectsMFTransactionStaging(mypager.CurrentPage, out Count, hdnSort.Value, int.Parse(hdnProcessIdFilter.Value), hdnRejectReasonFilter.Value, hdnFileNameFilter.Value, hdnFolioFilter.Value, hdnTransactionTypeFilter.Value, hdnInvNameFilter.Value, hdnSourceTypeFilter.Value, hdnSchemeNameFilter.Value, hdnAdviserFilter.Value);

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                trMessage.Visible = false;
                trReprocess.Visible = true;
                gvWERPTrans.DataSource = dsRejectedRecords.Tables[0];
                gvWERPTrans.DataBind();


                if (dsRejectedRecords.Tables[2].Rows.Count > 0)
                {
                    // Get the Reject Reason Codes Available into Generic Dictionary
                    foreach (DataRow dr in dsRejectedRecords.Tables[2].Rows)
                    {
                        if (dr["RejectReasonCode"].ToString() != "7")
                        {
                            genDictRejectReason.Add(dr["RejectReason"].ToString(), dr["RejectReasonCode"].ToString());
                        }
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
                }

                BindProcessId(dsRejectedRecords.Tables[3]);
                BindInvName(dsRejectedRecords.Tables[6]);
                BindFileName(dsRejectedRecords.Tables[4]);
                BindSourceType(dsRejectedRecords.Tables[5]);
                BindFolioNumber(dsRejectedRecords.Tables[7]);
                BindSchemeName(dsRejectedRecords.Tables[8]);
                BindTransactionType(dsRejectedRecords.Tables[9]);
                BindAdviserName(dsRejectedRecords.Tables[10]);

            }
            else
            {
                hdnRecordCount.Value = "0";
                gvWERPTrans.DataSource = null;
                gvWERPTrans.DataBind();
                trMessage.Visible = true;
                trReprocess.Visible = false;
            }
            this.GetPageCount();
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

        #region BindDropdown Filters

        //Methods with respect to ProcessIdFilter
        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcessId = GetProcessIdDDL();

            if (ddlProcessId != null)
            {
                if (ddlProcessId.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "0";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindProcessId(DataTable dtProcessId)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtProcessId.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtProcessId.Rows)
                {
                    genDictPanNum.Add(dr["ProcessId"].ToString(), dr["ProcessId"].ToString());
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
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlProcessId");
            }
            return ddl;
        }

        //Methods with respect to InverstorName filter
        protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlInvName = GetInvNameDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlInvName != null)
            {
                if (ddlInvName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnInvNameFilter.Value = ddlInvName.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnInvNameFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindInvName(DataTable dtInvName)
        {
            Dictionary<string, string> genDictInvName = new Dictionary<string, string>();
            if (dtInvName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtInvName.Rows)
                {
                    genDictInvName.Add(dr["InvestorName"].ToString(), dr["InvestorName"].ToString());
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
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlInvName") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlInvName");
            }
            return ddl;
        }

        //Methods with respect to FileName Filter
        protected void ddlFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFileName = GetFileNameDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlFileName != null)
            {
                if (ddlFileName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnFileNameFilter.Value = ddlFileName.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnFileNameFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindFileName(DataTable dtFileName)
        {
            Dictionary<string, string> genDictFileName = new Dictionary<string, string>();
            if (dtFileName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFileName.Rows)
                {
                    genDictFileName.Add(dr["ExternalFileName"].ToString(), dr["ExternalFileName"].ToString());
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
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlFileName") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlFileName");
            }
            return ddl;
        }

        //Methods with respect to SourceType Filter
        protected void ddlSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSourceType = GetSourceTypeDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlSourceType != null)
            {
                if (ddlSourceType.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnSourceTypeFilter.Value = ddlSourceType.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnSourceTypeFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindSourceType(DataTable dtSourceType)
        {
            Dictionary<string, string> genDictSourceType = new Dictionary<string, string>();
            if (dtSourceType.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtSourceType.Rows)
                {
                    genDictSourceType.Add(dr["SourceType"].ToString(), dr["SourceType"].ToString());
                }

                DropDownList ddlSourceType = GetSourceTypeDDL();
                if (ddlSourceType != null)
                {
                    ddlSourceType.DataSource = genDictSourceType;
                    ddlSourceType.DataTextField = "Key";
                    ddlSourceType.DataValueField = "Value";
                    ddlSourceType.DataBind();
                    ddlSourceType.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnSourceTypeFilter.Value != "")
                {
                    ddlSourceType.SelectedValue = hdnSourceTypeFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetSourceTypeDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlSourceType") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlSourceType");
            }
            return ddl;
        }

        //Methods with respect to FolioNumber Filter
        protected void ddlFolioNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlFolioNumber = GetFolioNumberDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlFolioNumber != null)
            {
                if (ddlFolioNumber.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnFolioFilter.Value = ddlFolioNumber.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnFolioFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindFolioNumber(DataTable dtFolioNumber)
        {
            Dictionary<string, string> genDictFolioNumber = new Dictionary<string, string>();
            if (dtFolioNumber.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFolioNumber.Rows)
                {
                    genDictFolioNumber.Add(dr["FolioNumber"].ToString(), dr["FolioNumber"].ToString());
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

        private DropDownList GetFolioNumberDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlFolioNumber") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlFolioNumber");
            }
            return ddl;
        }

        //Methods with respect to Scheme Name Filter
        protected void ddlSchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSchemeName = GetSchemeNameDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlSchemeName != null)
            {
                if (ddlSchemeName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnSchemeNameFilter.Value = ddlSchemeName.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnSchemeNameFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindSchemeName(DataTable dtSchemeName)
        {
            Dictionary<string, string> genDictSchemeName = new Dictionary<string, string>();
            if (dtSchemeName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtSchemeName.Rows)
                {
                    genDictSchemeName.Add(dr["SchemeName"].ToString(), dr["SchemeName"].ToString());
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
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlSchemeName") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlSchemeName");
            }
            return ddl;
        }

        //Methods with respect to Transaction Type Filter
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTransactionType = GetTransactionTypeDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlTransactionType != null)
            {
                if (ddlTransactionType.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnTransactionTypeFilter.Value = ddlTransactionType.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnTransactionTypeFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private void BindTransactionType(DataTable dtTransactionType)
        {
            Dictionary<string, string> genDictTransactionType = new Dictionary<string, string>();
            if (dtTransactionType.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtTransactionType.Rows)
                {
                    genDictTransactionType.Add(dr["TransactionType"].ToString(), dr["TransactionType"].ToString());
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
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType");
            }
            return ddl;
        }

        #endregion



        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvWERPTrans_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindEquityTransactionGrid(ProcessId);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindEquityTransactionGrid(ProcessId);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedWERPTransaction.ascx.cs:gvWERPTrans_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            string error = "";
            int processIdReprocessAll = 0;

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;
            string strErrorDesc = "Please a select a processId";
            DropDownList ddl = (DropDownList)gvWERPTrans.HeaderRow.Cells[0].FindControl("ddlProcessId");
            string val = ddl.SelectedValue;
            if (val == "Select")
                Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + strErrorDesc + " .');</script>");
            else
            {
                ProcessId = Convert.ToInt32(val);
                if (Request.QueryString["processId"] != null)
                {
                    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                    processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
                    if (processlogVo.FileTypeId == 1 || processlogVo.FileTypeId == 6 || processlogVo.FileTypeId == 3 || processlogVo.FileTypeId == 15 || processlogVo.FileTypeId == 17 || processlogVo.FileTypeId == 25)
                    {
                        blResult = MFWERPTransactionWERPInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords, processlogVo.FileTypeId);
                    }
                }
                else
                {
                    DataSet ds = uploadsCommonBo.GetSuperAdminUploadDistinctDetailsForProcessId(ProcessId);
                   // DataSet ds = uploadsCommonBo.GetSuperAdminUploadDistinctProcessIdForAdviser();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                    adviserId = int.Parse(dr["A_AdviserId"].ToString());
                    //processIdReprocessAll = ProcessId;
                    processIdReprocessAll = ProcessId;
                    processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);
                    if (processlogVo.FileTypeId == 1 || processlogVo.FileTypeId == 3 || processlogVo.FileTypeId == 15 || processlogVo.FileTypeId == 17 || processlogVo.FileTypeId == 25)
                    {
                        blResult = MFWERPTransactionWERPInsertion(processIdReprocessAll, out countTransactionsInserted, out countRejectedRecords, processlogVo.FileTypeId);
                    }
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

                BindEquityTransactionGrid(ProcessId);
                //used to display alert msg after completion of reprocessing
            }

        }

        private bool MFWERPTransactionWERPInsertion(int ProcessId, out int countTransactionsInserted, out int countRejectedRecords, int fileTypeId)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            werpUploadBo = new WerpUploadsBo();

            countTransactionsInserted = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            //CAMS and KARVY Reprocess 
            string packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
            bool CommonStdTransChecks = false;
            bool CommonTransChecks = false;
            if (fileTypeId == 1)
            {

                bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(ProcessId);
                if (camsDatatranslationCheckResult)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(processlogVo.AdviserId, ProcessId, packagePath, configPath, "CA", "CAMS");
                }
            }
            else if (fileTypeId == 25)
            {

                bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(ProcessId);
                if (camsDatatranslationCheckResult)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(processlogVo.AdviserId, ProcessId, packagePath, configPath, "SU", "Sundaram");
                }
            }
            //***reprocess for folioandTrnx
            else if (fileTypeId == 6)
            {
                standardProfileUploadBo = new StandardProfileUploadBo();
                string stdPackagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                CommonStdTransChecks = standardProfileUploadBo.StdCommonProfileChecks(ProcessId, processlogVo.AdviserId, stdPackagePath, configPath);


            }

            else if (fileTypeId == 3)
            {

                bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(ProcessId);
                if (karvyDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(processlogVo.AdviserId, ProcessId, packagePath, configPath, "KA", "Karvy");
                }
            }
            else if (fileTypeId == 15)
            {
                bool TempletonDataTranslationCheck = uploadsCommonBo.UploadsTempletonDataTranslationForReprocess(ProcessId);
                if (TempletonDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(processlogVo.AdviserId, ProcessId, packagePath, configPath, "TN", "Templeton");
                }
            }
            else if (fileTypeId == 17)
            {
                bool DeutscheDataTranslationCheck = uploadsCommonBo.UploadsDeutscheDataTranslationForReprocess(ProcessId);
                if (DeutscheDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(processlogVo.AdviserId, ProcessId, packagePath, configPath, "DT", "Deutsche");
                }
            }


            if (CommonTransChecks)
            {

                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                bool insertTransWERP = uploadsCommonBo.InsertTransToWERP(ProcessId, packagePath, configPath);
                if (insertTransWERP)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WPMF");

                    processlogVo.EndTime = DateTime.Now;

                    if (fileTypeId == 1)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeCAMS);
                    else if (fileTypeId == 3)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeKarvy);
                    else if (fileTypeId == 15)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeTemp);
                    else if (fileTypeId == 17)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeDeutsche);
                    else if (filetypeId == 25)
                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, "SU");
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                }
            }
            else if (CommonStdTransChecks)
            {
                processlogVo.IsInsertionToWERPComplete = 1;
                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WP");
                processlogVo.EndTime = DateTime.Now;

                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeStandard);

                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

            }
            return blResult;
        }

        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReject = GetRejectReasonDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRejectReasonFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private TextBox GetFolioTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtFolioSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtFolioSearch");
            }
            return txt;
        }

        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlRejectReason");
            }
            return ddl;
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            TextBox txtPanNumber = GetPanNumberTextBox();
            TextBox txtFolio = GetFolioNumberTextBox();
            TextBox txtTransactionType = GetTransactionTypeTextBox();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            if (txtPanNumber != null)
                hdnProcessIdFilter.Value = txtPanNumber.Text.Trim();
            if (txtFolio != null)
                hdnFolioFilter.Value = txtFolio.Text.Trim();

            if (txtTransactionType != null)
                hdnTransactionTypeFilter.Value = txtTransactionType.Text.Trim();

            BindEquityTransactionGrid(ProcessId);
        }

        private TextBox GetTransactionTypeTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtTransactionTypeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtTransactionTypeSearch");
            }
            return txt;
        }

        private TextBox GetFolioNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtFolioSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtFolioSearch");
            }
            return txt;
        }

        private TextBox GetPanNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtPanNumberSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtPanNumberSearch");
            }
            return txt;
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }



        //protected void btnMapFolios_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedFoliosUploads','login');", true);
        //}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            string StagingID = string.Empty;

            foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                    StagingID += Convert.ToString(gvWERPTrans.DataKeys[gvr.RowIndex].Value) + "~";
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                rejectedRecordsBo = new RejectedRecordsBo();
                rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
                if (hdnProcessIdFilter.Value != "")
                {
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                }
                BindEquityTransactionGrid(ProcessId);
            }
        }



        private void CustomerTransactionDelete()
        {
            #region unused
            //foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            //{
            //    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
            //    {
            //        rejectedRecordsBo = new RejectedRecordsBo();

            //        int StagingID = int.Parse(gvWERPTrans.DataKeys[gvr.RowIndex].Values["CMFTSId"].ToString());

            //        rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
            //        if (hdnProcessIdFilter.Value != "")
            //        {
            //            ProcessId = int.Parse(hdnProcessIdFilter.Value);
            //        }

            //        BindEquityTransactionGrid(ProcessId);


            //        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFTransactionStaging','login');", true);



            //    }
            //}
            #endregion
        }

        protected void btnProbableInsert_Click(object sender, EventArgs e)
        {
            bool result = true;
            bool blResult = true;
            string gvStagingIds = "";
            uploadsCommonBo = new UploadCommonBo();
            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            int fileTypeId = processlogVo.FileTypeId;
            //foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            //{
            //    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
            //    {
            //        rejectedRecordsBo = new RejectedRecordsBo();

            //        int stagingID = int.Parse(gvWERPTrans.DataKeys[gvr.RowIndex].Values["CMFTSId"].ToString());

            //        result = rejectedRecordsBo.InsertProbableDuplicatesRejectedTransaction(stagingID);

            //    }

            //}
            rejectedRecordsBo = new RejectedRecordsBo();
            foreach (GridViewRow gvRow in gvWERPTrans.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkId");
                if (ChkBxItem.Checked)
                {
                    gvStagingIds += gvWERPTrans.DataKeys[gvRow.RowIndex].Values["CMFTSId"].ToString() + ",";
                }
            }
            result = rejectedRecordsBo.InsertProbableDuplicatesRejectedTransaction(gvStagingIds);
            if (result)
            {
                // Success Message
                processlogVo.IsInsertionToWERPComplete = 1;
                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WPMF");

                processlogVo.EndTime = DateTime.Now;

                if (fileTypeId == 1)
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeCAMS);
                else if (fileTypeId == 3)
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeKarvy);
                else if (fileTypeId == 15)
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeTemp);
                else if (fileTypeId == 17)
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, Contants.UploadExternalTypeDeutsche);
                else if (filetypeId == 25)
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, "SU");
                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Records Inserted Successfully');", true);

            }
            else
            {
                // Failure Message
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Probable Duplicate Records');", true);
            }

            BindEquityTransactionGrid(ProcessId);

        }

        protected void btnProbableDelete_Click(object sender, EventArgs e)
        {
            bool result = true;
            bool blResult = true;
            string gvStagingIds = "";
            rejectedRecordsBo = new RejectedRecordsBo();
            foreach (GridViewRow gvRow in gvWERPTrans.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkId");
                if (ChkBxItem.Checked)
                {
                    gvStagingIds += gvWERPTrans.DataKeys[gvRow.RowIndex].Values["CMFTSId"].ToString() + ",";
                }
            }
            result = rejectedRecordsBo.DeleteProbableDuplicatesRejectedTransaction(gvStagingIds);
            if (result)
            {
                // Success Message
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Records Deleted Successfully');", true);

            }
            else
            {
                // Failure Message
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Probable Duplicate Records');", true);

            }

            BindEquityTransactionGrid(ProcessId);
        }

        protected void ddlAdviserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAdviserName = GetOrganization();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            DropDownList ddlProcessId = GetProcessIdDDL();
            if (ddlProcessId.SelectedIndex != 0)
            {
                hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                ProcessId = int.Parse(hdnProcessIdFilter.Value);
            }
            else
                ProcessId = 0;

            if (ddlAdviserName != null)
            {
                if (ddlAdviserName.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnAdviserFilter.Value = ddlAdviserName.SelectedItem.Text;
                    BindEquityTransactionGrid(ProcessId);
                    // ddlAdviserName.SelectedItem.Text = hdnAdviserNameAUMFilter.Value;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnAdviserFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        private DropDownList GetOrganization()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlAdviserName") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlAdviserName");
            }
            return ddl;
        }

    }

}
