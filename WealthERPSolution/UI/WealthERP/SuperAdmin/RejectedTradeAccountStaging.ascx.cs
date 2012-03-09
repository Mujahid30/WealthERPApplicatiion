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

namespace WealthERP.SuperAdmin
{
    public partial class RejectedTradeAccountStaging : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;

        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;

        DataSet dsRejectedRecords;

        int ProcessId;
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

            GetPageCount();
            this.BindRejectedUploadsGrid(ProcessId);
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
            ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

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
            //adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                // Bind Grid
                hdnProcessIdFilter.Value = ProcessId.ToString();
                BindRejectedUploadsGrid(ProcessId);
            }
        }

        private void BindRejectedUploadsGrid(int ProcessId)
        {
            // Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count;

            rejectedRecordsBo = new RejectedRecordsBo();

            if (ProcessId == 0) //TODO : This IF ELSE can be removed.
            {   // Bind All Processes
                dsRejectedRecords = rejectedRecordsBo.GetSuperAdminRejectedTradeAccountStaging(ProcessId, mypager.CurrentPage, out Count, hdnSort.Value, hdnTradeAccountNumFilter.Value, hdnRejectReasonFilter.Value, hdnPanFilter.Value);
            }
            else
            {   // Bind Grid for the specific Process Id
                dsRejectedRecords = rejectedRecordsBo.GetSuperAdminRejectedTradeAccountStaging(ProcessId, mypager.CurrentPage, out Count, hdnSort.Value, hdnTradeAccountNumFilter.Value, hdnRejectReasonFilter.Value, hdnPanFilter.Value);
            }

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
                        genDictRejectReason.Add(dr["RejectReason"].ToString(), dr["RejectReasonCode"].ToString());
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
                BindPanNumber(dsRejectedRecords.Tables[3]);
                BindProcessId(dsRejectedRecords.Tables[4]);
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

        protected void ddlPanNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPanNum = GetPanNumDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (ddlPanNum != null)
            {
                if (ddlPanNum.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnPanFilter.Value = ddlPanNum.SelectedValue;
                    BindRejectedUploadsGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnPanFilter.Value = "";
                    BindRejectedUploadsGrid(ProcessId);
                }
            }
        }

        private void BindPanNumber(DataTable dtPanNumber)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtPanNumber.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtPanNumber.Rows)
                {
                    genDictPanNum.Add(dr["PANNumber"].ToString(), dr["PANNumber"].ToString());
                }

                DropDownList ddlPanNum = GetPanNumDDL();
                if (ddlPanNum != null)
                {
                    ddlPanNum.DataSource = genDictPanNum;
                    ddlPanNum.DataTextField = "Key";
                    ddlPanNum.DataValueField = "Value";
                    ddlPanNum.DataBind();
                    ddlPanNum.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnPanFilter.Value != "")
                {
                    ddlPanNum.SelectedValue = hdnPanFilter.Value.ToString().Trim();
                }
            }
        }

        private DropDownList GetPanNumDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlPanNumber") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlPanNumber");
            }
            return ddl;
        }

        //********** Code implented by bhoopendra for adding a dropdown filter of process id.*************//
        //********** Code Starts *************//
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

        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcessId = GetProcessIdDDL();

            if (ddlProcessId != null)
            {
                if (ddlProcessId.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindRejectedUploadsGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "0";
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindRejectedUploadsGrid(ProcessId);
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

        /*************To delete the selected records ****************/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
                    i = i + 1;
            }

            if (i == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            else
                CustomerTransactionDelete();
        }

        private void CustomerTransactionDelete()
        {
            foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
                {
                    rejectedRecordsBo = new RejectedRecordsBo();
                    int StagingID = int.Parse(gvWERPTrans.DataKeys[gvr.RowIndex].Values["WERPTransactionId"].ToString());
                    rejectedRecordsBo.DeleteRejectsEquityTradeAccountStaging(StagingID);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedTradeAccountStaging','login');", true);
                }
            }
        }
        //************** Code End  ***********************//

        protected void btnEditSelectedWPTrans_Click(object sender, EventArgs e)
        {
            string newTradeAccountNum = string.Empty;
            string newPanNum = string.Empty;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();

            // Gets the footer row directly Cool right! 
            GridViewRow footerRow = gvWERPTrans.FooterRow;

            string tradeAccountNum = ((TextBox)footerRow.FindControl("txtTradeAccountNumberMultiple")).Text;
            string panNum = ((TextBox)footerRow.FindControl("txtPanNumberMultiple")).Text;

            foreach (GridViewRow dr in gvWERPTrans.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBxWPTrans");
                if (checkBox.Checked)
                {
                    if (tradeAccountNum != "" || panNum != "")
                    {
                        newTradeAccountNum = tradeAccountNum;
                        newPanNum = panNum;
                    }
                    else
                    {
                        newTradeAccountNum = ((TextBox)dr.FindControl("txtTradeAccountNumber")).Text;
                        newPanNum = ((TextBox)dr.FindControl("txtPanNumber")).Text;
                    }

                    int Id = Convert.ToInt32(gvWERPTrans.DataKeys[dr.RowIndex].Value);
                    blResult = rejectedRecordsBo.UpdateRejectedTradeAccountStaging(Id, newTradeAccountNum, newPanNum);
                }
            }

            if (blResult)
            {
                // Success Message
            }
            else
            {
                // Failure Message
            }

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            BindRejectedUploadsGrid(ProcessId);
        }

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
                    this.BindRejectedUploadsGrid(ProcessId);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindRejectedUploadsGrid(ProcessId);
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
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;
            string error = "";
            int processIdReprocessAll = 0;

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                blResult = TradeAccountStagingInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords);
            }
            else
            {
                DataSet ds = uploadsCommonBo.GetSuperAdminEquityTradeAccountStagingProcessId();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    processIdReprocessAll = int.Parse(dr["ProcessId"].ToString());
                    blResult = TradeAccountStagingInsertion(processIdReprocessAll, out countTransactionsInserted, out countRejectedRecords);
                }
            }
            if (blResult == false)
            {
                error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
            }

            if (blResult)
            {
                // Success Message
                trErrorMessage.Visible = true;
                lblError.Text = "Reprocess Done Successfully!";
            }
            else
            {
                // Failure Message
                trErrorMessage.Visible = true;
                lblError.Text = "Reprocess Failure!";
            }
            BindRejectedUploadsGrid(ProcessId);
        }

        private bool TradeAccountStagingInsertion(int ProcessId, out int countTransactionsInserted, out int countRejectedRecords)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            werpUploadBo = new WerpUploadsBo();

            countTransactionsInserted = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
            // WERP Insertion
            string packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
            bool WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(ProcessId, packagePath, configPath, processlogVo.AdviserId);

            if (WERPEQSecondStagingCheckResult)
            {
                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                bool WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(ProcessId, packagePath, configPath);

                if (WERPEQTradeWerpResult)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPEQ");
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(ProcessId, "WPEQ");
                    processlogVo.EndTime = DateTime.Now;
                    bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                    if (updateProcessLog)
                        blResult = true;
                }
            }

            return blResult;
        }


        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReject = GetRejectReasonDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindRejectedUploadsGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRejectReasonFilter.Value = "";
                    BindRejectedUploadsGrid(ProcessId);
                }
            }
        }

        private TextBox GetTradeAccountNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtTradeAccountNumberSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtTradeAccountNumberSearch");
            }
            return txt;
        }

        private TextBox GetPANNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtPanNumberSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtPanNumberSearch");
            }
            return txt;
        }


        private DropDownList GetIsRejectedDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlIsRejected") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlIsRejected");
            }
            return ddl;
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
            TextBox txtTradeAccountNumber = GetTradeAccountNumberTextBox();
            TextBox txtPan = GetPANNumberTextBox();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());



            if (txtTradeAccountNumber != null)
                hdnTradeAccountNumFilter.Value = txtTradeAccountNumber.Text.Trim();
            if (txtPan != null)
                hdnPanFilter.Value = txtPan.Text.Trim();
            BindRejectedUploadsGrid(ProcessId);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
    }
}