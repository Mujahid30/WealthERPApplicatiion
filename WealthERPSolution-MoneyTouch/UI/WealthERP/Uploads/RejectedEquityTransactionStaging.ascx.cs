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

namespace WealthERP.Uploads
{

    public partial class RejectedEquityTransactionStaging : System.Web.UI.UserControl
    {
        DataTable dtTransactionTypes = new DataTable();
        DataTable dtFilterTransactionTypes = new DataTable();

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
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
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

            dsRejectedRecords = rejectedRecordsBo.GetRejectedEquityTransactionsStaging(ProcessId, mypager.CurrentPage, out Count, hdnSort.Value, hdnRejectReasonFilter.Value, hdnPanNumberFilter.Value, hdnScripFilter.Value, hdnExchangeFilter.Value, hdnTransactionTypeFilter.Value);
            

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                dtTransactionTypes = dsRejectedRecords.Tables[4]; //All transaction types
                dtFilterTransactionTypes = dsRejectedRecords.Tables[5]; //Transaction types for filter
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
                        ddlRejectReason.Items.Insert(0, new ListItem("Select Reject Reason", "Select Reject Reason"));
                    }

                    if (hdnRejectReasonFilter.Value != "")
                    {
                        ddlRejectReason.SelectedValue = hdnRejectReasonFilter.Value.ToString().Trim();
                    }
                }

                BindPanNumber(dsRejectedRecords.Tables[3]);

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
                    hdnPanNumberFilter.Value = ddlPanNum.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnPanNumberFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTransactionType = GetTransactionTypeDdl();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

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
                    ddlPanNum.Items.Insert(0, new ListItem("Select PAN Number", "Select PAN Number"));
                }

                if (hdnPanNumberFilter.Value != "")
                {
                    ddlPanNum.SelectedValue = hdnPanNumberFilter.Value.ToString().Trim();
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

        private DropDownList GetTransactionTypeDdl()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType") != null)
            {
                ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType");
            }
            return ddl;
        }

        protected void btnEditSelectedWPTrans_Click(object sender, EventArgs e)
        {
            //int Id = 0;
            string panNumber = string.Empty;
            string tradeAccountNumber = string.Empty;
            string scripCode = string.Empty;
            string newScripCode = string.Empty;
            string exchange = string.Empty;
            string price = string.Empty;
            string newPrice = string.Empty;

            string transactionType = string.Empty;
            string newTransactionType = string.Empty;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();

            // Gets the footer row directly Cool right! 
            GridViewRow footerRow = gvWERPTrans.FooterRow;

            string newPanNumber = ((TextBox)footerRow.FindControl("txtPanNumberMultiple")).Text;

            if (((TextBox)footerRow.FindControl("txtScripCodeMultiple")).Text != string.Empty)
                newScripCode = ((TextBox)footerRow.FindControl("txtScripCodeMultiple")).Text;

            string newExchange = ((TextBox)footerRow.FindControl("txtExchangeMultiple")).Text;

            if (((DropDownList)footerRow.FindControl("ddlTransactionType")).SelectedValue != "-1")
                newTransactionType = ((DropDownList)footerRow.FindControl("ddlTransactionType")).SelectedValue;

            if (((TextBox)footerRow.FindControl("txtPriceMultiple")).Text != string.Empty)
                newPrice = ((TextBox)footerRow.FindControl("txtPriceMultiple")).Text;




            foreach (GridViewRow dr in gvWERPTrans.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBxWPTrans");
                if (checkBox.Checked)
                {
                    if (newPanNumber != "" || newScripCode != "" || newExchange != "" || newPrice != "" || newTransactionType != "") //Change this logic
                    {
                        panNumber = newPanNumber;
                        scripCode = newScripCode;
                        exchange = newExchange;
                        transactionType = newTransactionType;
                        price = newPrice;
                    }
                    else
                    {
                        panNumber = ((TextBox)dr.FindControl("txtPanNumber")).Text;
                        if (((TextBox)dr.FindControl("txtScripCode")).Text != string.Empty)
                            scripCode = ((TextBox)dr.FindControl("txtScripCode")).Text;
                        exchange = ((TextBox)dr.FindControl("txtExchange")).Text;
                        transactionType = ((DropDownList)dr.FindControl("ddlTransactionType")).SelectedValue;
                        // price = Convert.ToDouble(((TextBox)dr.FindControl("txtPrice")).Text);
                        price = ((TextBox)dr.FindControl("txtPrice")).Text;
                    }
                    int id = Convert.ToInt32(gvWERPTrans.DataKeys[dr.RowIndex].Value);


                    blResult = rejectedRecordsBo.UpdateRejectedEquityTransactionStaging(id, panNumber, scripCode, exchange, price.ToString(), transactionType);
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
            BindEquityTransactionGrid(ProcessId);
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
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }

            //if (uploadsCommonBo.ResetRejectedFlagByProcess(ProcessId, 8))
            //{
            // start the reprocess from the staging onwards

            // WERP Insertion
            blResult = MFWERPTransactionWERPInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords);
            //  }

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

            BindEquityTransactionGrid(ProcessId);
        }

        private bool MFWERPTransactionWERPInsertion(int ProcessId, out int countTransactionsInserted, out int countRejectedRecords)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            werpUploadBo = new WerpUploadsBo();

            countTransactionsInserted = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            // WERP Insertion
            // WERP Equity Transation Insertion
            WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
            string packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
            bool WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(ProcessId, packagePath, configPath, adviserVo.advisorId);

            if (WERPEQSecondStagingCheckResult)
            {
                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(ProcessId, packagePath, configPath); // EQ Trans XML File Type Id = 8);
                if (WERPEQTranWerpResult)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WPEQ");
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, "WPEQ");
                    processlogVo.EndTime = DateTime.Now;
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
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
            TextBox txtPanNumber = GetPanNumberTextBox();
            TextBox txtTradeAccountNumber = GetTradeAccountNumberTextBox();
            TextBox txtScrip = GetScripTextBox();
            TextBox txtExchange = GetExchangeTextBox();
            TextBox txtTransactionType = GetTransactionTypeTextBox();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (txtPanNumber != null)
                hdnPanNumberFilter.Value = txtPanNumber.Text.Trim();
            if (txtTradeAccountNumber != null)
                hdnTradeAccountNumberFilter.Value = txtTradeAccountNumber.Text.Trim();
            if (txtScrip != null)
                hdnScripFilter.Value = txtScrip.Text.Trim();
            if (txtExchange != null)
                hdnExchangeFilter.Value = txtExchange.Text.Trim();

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

        private TextBox GetExchangeTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtExchangeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtExchangeSearch");
            }
            return txt;
        }

        private TextBox GetScripTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.HeaderRow.FindControl("txtScripCodeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.HeaderRow.FindControl("txtScripCodeSearch");
            }
            return txt;
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

        /// <summary>
        /// This method displays the transaction type drop down in each row  
        /// </summary>
        protected void gvWERPTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            const int CONST_TRANSACTION_TYPE_COLUMN = 9;

            /// Dropdown in each row || Header dropdown(for filtering) ||  Footer dropdown (Multiple updates)
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.Header)
            {
                HiddenField hdnTransactionType = new HiddenField();
                DropDownList ddlTransactionType = (DropDownList)e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("ddlTransactionType");

                if (e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("hdnTransactionType") != null)
                    hdnTransactionType = (HiddenField)e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("hdnTransactionType");

                ddlTransactionType.Items.Add(new ListItem("Select Transaction Type", ""));
                ddlTransactionType.SelectedValue = "";
                ///Add all the Transaction types.
                if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
                {
                    foreach (DataRow dr in dtTransactionTypes.Rows)
                    {
                        ddlTransactionType.Items.Add(new ListItem(dr["WETT_TransactionTYpeName"].ToString(), dr["WETT_TransactionCode"].ToString()));
                    }
                    if (!String.IsNullOrEmpty(hdnTransactionType.Value) && Convert.ToInt32(hdnTransactionType.Value) > 0)
                        ddlTransactionType.SelectedValue = hdnTransactionType.Value;
                }
                else if (e.Row.RowType == DataControlRowType.Header)  //Add transaction types for filtering.
                {
                    foreach (DataRow dr in dtFilterTransactionTypes.Rows)
                    {
                        ddlTransactionType.Items.Add(new ListItem(dr["WETT_TransactionTYpeName"].ToString(), dr["WETT_TransactionCode"].ToString()));
                    }
                    if (!String.IsNullOrEmpty(hdnTransactionTypeFilter.Value) && Convert.ToInt32(hdnTransactionTypeFilter.Value) > 0)
                        ddlTransactionType.SelectedValue = hdnTransactionTypeFilter.Value;
                }
            }

        }


    }
}