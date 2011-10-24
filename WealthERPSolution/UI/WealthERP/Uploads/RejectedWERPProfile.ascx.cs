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
    public partial class RejectedWERPProfile : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;
        RMVo rmVo;

        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpMFUploadsBo werpMFUploadsBo;

        DataSet dsRejectedRecords;

        int adviserId;
        int ProcessId;
        string configPath;
        string source;
        string xmlPath;
        int filetypeId;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            //ProcessId = 0;
            //if (Request.QueryString["processId"] != null)
            //    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            //if(Request.QueryString["filetypeid"] != null)
            //    filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
            //ProcessId = ddlProcessId.SelectedValue;

            GetPageCount();
            ProcessId = int.Parse(hdnProcessIdFilter.Value.ToString());
            this.BindWerpProfileGrid(ProcessId);
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
            //ProcessId = 0;
            uploadsCommonBo = new UploadCommonBo();
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());            

            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

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
            adviserId = adviserVo.advisorId;
            rmVo = (RMVo)Session[SessionContents.RmVo];
            //.GetProcessLogInfo(ProcessId);

            ManageFolioLink(ProcessId);
            if (!IsPostBack)
            {
                mypager.CurrentPage = 1;
                hdnProcessIdFilter.Value = ProcessId.ToString();
                ProcessId = int.Parse(hdnProcessIdFilter.Value);
                // Bind Grid
                BindWerpProfileGrid(ProcessId);
            }
        }

        private void ManageFolioLink(int processId)
        {
            processlogVo = uploadsCommonBo.GetProcessLogInfo(processId);
            //TODO:Change FileName to external type.
            if (processlogVo.ExtractTypeCode == "PAF")
            {
                lnkProfile.Visible = true;
                lnkProfile.Text = "View Folio Rejects";
            }

        }

        private void BindWerpProfileGrid(int ProcessId)
        {
            //Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            // Dictionary<string, string> genDictIsCustomerExisting = new Dictionary<string, string>();

            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }

            int Count;

            rejectedRecordsBo = new RejectedRecordsBo();

            if (ProcessId == 0)
            {   // Bind All Processes
                dsRejectedRecords = rejectedRecordsBo.getWERPRejectedProfile(adviserVo.advisorId, ProcessId, mypager.CurrentPage, out Count, hdnSort.Value, hdnPANFilter.Value, hdnRejectReasonFilter.Value, hdnBrokerCodeFilter.Value, hdnCustomerNameFilter.Value);
                //   PANFilter, RejectReasonFilter, BrokerFilter, CustomerNameFilter);
            }
            else
            {   // Bind Grid for the specific Process Id
                dsRejectedRecords = rejectedRecordsBo.getWERPRejectedProfile(adviserVo.advisorId, ProcessId, mypager.CurrentPage, out Count, hdnSort.Value, hdnPANFilter.Value, hdnRejectReasonFilter.Value, hdnBrokerCodeFilter.Value, hdnCustomerNameFilter.Value);
            }

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                trMessage.Visible = false;
                trReprocess.Visible = true;
                gvWERPProfileReject.DataSource = dsRejectedRecords.Tables[0];
                gvWERPProfileReject.DataBind();

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
                gvWERPProfileReject.DataSource = null;
                gvWERPProfileReject.DataBind();
                trMessage.Visible = true;
                trReprocess.Visible = false;
            }
            this.GetPageCount();
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
                    BindWerpProfileGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "0";
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                    BindWerpProfileGrid(ProcessId);
                }
            }
        }
        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlProcessId");
            }
            return ddl;
        }
        
        /*************To delete the selected records ****************/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridViewRow gvr in this.gvWERPProfileReject.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkBxWerp")).Checked == true)
                    i = i + 1;
            }

            if (i == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            else
                CustomerTransactionDelete();
        }
        
        private void CustomerTransactionDelete()
        {
            foreach (GridViewRow gvr in this.gvWERPProfileReject.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkBxWerp")).Checked == true)
                {
                    rejectedRecordsBo = new RejectedRecordsBo();
                    int StagingID = int.Parse(gvWERPProfileReject.DataKeys[gvr.RowIndex].Values["WERPProfileStagingId"].ToString());
                    rejectedRecordsBo.DeleteWERPRejectedProfile(StagingID);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedWERPProfile','login');", true);
                }
            }
        }

        //************** Code End  ***********************//


        protected void ddlPanNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPanNum = GetPanNumDDL();

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
            if (ddlPanNum != null)
            {
                if (ddlPanNum.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnPANFilter.Value = ddlPanNum.SelectedValue;
                    BindWerpProfileGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnPANFilter.Value = "";
                    BindWerpProfileGrid(ProcessId);
                }
            }
        }

        /// <summary>
        /// Fill the PAN Number filter dropdown with PAN Numbers
        /// </summary>
        /// <param name="dtPanNumber"></param>
        private void BindPanNumber(DataTable dtPanNumber)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtPanNumber.Rows.Count > 0)
            {
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

                if (hdnPANFilter.Value != "")
                {
                    ddlPanNum.SelectedValue = hdnPANFilter.Value.ToString().Trim();
                }
            }
        }
        /// <summary>
        /// Get the PAN Number filter dropdown 
        /// </summary>
        /// <returns></returns>
        private DropDownList GetPanNumDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlPanNumber") != null)
            {
                ddl = (DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlPanNumber");
            }
            return ddl;
        }

        protected void btnEditSelectedWerp_Click(object sender, EventArgs e)
        {
            string newPan = string.Empty;
            string newBroker = string.Empty;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();

            // Gets the footer row directly Cool right! 
            GridViewRow footerRow = gvWERPProfileReject.FooterRow;

            string pan = ((TextBox)footerRow.FindControl("txtPanMultiple")).Text;
            string broker = ((TextBox)footerRow.FindControl("txtBrokerMultiple")).Text;

            foreach (GridViewRow dr in gvWERPProfileReject.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBxWerp");
                if (checkBox.Checked)
                {
                    if (pan != "" || broker != "")
                    {
                        newPan = pan;
                        newBroker = broker;
                    }
                    else
                    {
                        newPan = ((TextBox)dr.FindControl("txtPanWerp")).Text;
                        newBroker = ((TextBox)dr.FindControl("txtBroker")).Text;
                    }

                    int WERPStagingId = Convert.ToInt32(gvWERPProfileReject.DataKeys[dr.RowIndex].Value);
                    blResult = rejectedRecordsBo.UpdateWERPProfileStaging(WERPStagingId, newPan, newBroker);
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
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
            BindWerpProfileGrid(ProcessId);
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

        protected void gvWERPProfileReject_Sort(object sender, GridViewSortEventArgs e)
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
                    this.BindWerpProfileGrid(ProcessId);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindWerpProfileGrid(ProcessId);
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

                FunctionInfo.Add("Method", "RejectedWERPTransaction.ascx.cs:gvWERPProfileReject_Sort()");

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
            xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            string error = "";
            int countCustCreated = 0;
            int countFolioCreated = 0;
            int countRejectedRecords = 0;
            int processIdReprocessAll = 0;

            //processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            //if (processlogVo.FileTypeId == 7)
            //    source = "WP";
            //else if (processlogVo.FileTypeId == 4)
            //    source = "KA";
            //else if (processlogVo.FileTypeId == 2)
            //    source = "CA";
            //else if (processlogVo.FileTypeId == 16)
            //    source = "TN";
            //else if (processlogVo.FileTypeId == 18)
            //    source = "DT";

            // BindGrid
            //if (Request.QueryString["processId"] != null)
            //{
            //    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            //}
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
                if (processlogVo.FileTypeId == 7)
                    source = "WP";
                else if (processlogVo.FileTypeId == 4)
                    source = "KA";
                else if (processlogVo.FileTypeId == 2)
                    source = "CA";
                else if (processlogVo.FileTypeId == 16)
                    source = "TN";
                else if (processlogVo.FileTypeId == 18)
                    source = "DT";
                else if (processlogVo.FileTypeId == 21)
                    source = "SU";
                if (processlogVo.FileTypeId == 7 || processlogVo.FileTypeId == 4 || processlogVo.FileTypeId == 2 || processlogVo.FileTypeId == 16 || processlogVo.FileTypeId == 18 || processlogVo.FileTypeId==21)
                {
                    StandardProfileUploadBo standardProfileUploadBo = new StandardProfileUploadBo();
                    //Checks in Profile Staging
                    string packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                    bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(ProcessId, adviserVo.advisorId, packagePath, configPath);
                    if (karvyProCommonChecksResult)
                    {
                        // Insert Customer Details into WERP Tables

                        bool stdProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, ProcessId, rmVo.RMId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                        if (stdProCreateCustomerResult)
                        {
                            //Create new Bank Accounts
                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                            bool stdProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(ProcessId, packagePath, configPath);
                            if (stdProCreateBankAccountResult)
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(ProcessId, source);
                                processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, source);
                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (blResult)
                                {
                                    bool stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(ProcessId);
                                }
                            }
                        }
                    }
                }
            }

            else
            {

                DataSet ds = uploadsCommonBo.GetWERPUploadProcessIdForAdviser(adviserVo.advisorId);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    processIdReprocessAll = int.Parse(dr["ProcessId"].ToString());
                    processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);
                    if (processlogVo.FileTypeId == 7)
                        source = "WP";
                    else if (processlogVo.FileTypeId == 4)
                        source = "KA";
                    else if (processlogVo.FileTypeId == 2)
                        source = "CA";
                    else if (processlogVo.FileTypeId == 16)
                        source = "TN";
                    else if (processlogVo.FileTypeId == 18)
                        source = "DT";
                    else if (processlogVo.FileTypeId == 21)
                        source = "SU";

                    if (processlogVo.FileTypeId == 7 || processlogVo.FileTypeId == 4 || processlogVo.FileTypeId == 2 || processlogVo.FileTypeId == 16 || processlogVo.FileTypeId == 18 || processlogVo.FileTypeId==21)
                    {
                        StandardProfileUploadBo standardProfileUploadBo = new StandardProfileUploadBo();
                        //Checks in Profile Staging
                        string packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                        bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processIdReprocessAll, adviserVo.advisorId, packagePath, configPath);
                        if (karvyProCommonChecksResult)
                        {
                            // Insert Customer Details into WERP Tables

                            bool stdProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserVo.advisorId, processIdReprocessAll, rmVo.RMId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                            if (stdProCreateCustomerResult)
                            {
                                //Create new Bank Accounts
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                bool stdProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processIdReprocessAll, packagePath, configPath);
                                if (stdProCreateBankAccountResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processIdReprocessAll, source);
                                    processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                    processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processIdReprocessAll, source);
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (blResult)
                                    {
                                        bool stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processIdReprocessAll);
                                    }
                                }
                            }
                        }
                    } 
                   
                }
            }

            if (blResult == false)
            {
                error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
            }
            if (blResult)
            {
                // Success Message
                //trErrorMessage.Visible = true;
                //lblError.Text = "Reprocess Done Successfully!";
                msgReprocessComplete.Visible = true;
            }
            else
            {
                // Failure Message                
                msgReprocessincomplete.Visible = true;
            }

            BindWerpProfileGrid(ProcessId);
        }

        private bool MFWERPProfileWERPInsertion(int ProcessId, out int countCustCreated, out int countFolioCreated, out int countRejectedRecords)
        {
            bool blResult = false;

            werpMFUploadsBo = new WerpMFUploadsBo();
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();

            countCustCreated = 0;
            countFolioCreated = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            // Doing a check on data in Staging and marking IsRejected flag
            string packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadChecksWerpMFProfileStaging.dtsx");
            bool werpMFProStagingCheckResult = werpMFUploadsBo.WerpMFProcessDataInStagingProfile(ProcessId, adviserVo.advisorId, packagePath, configPath);

            // Insert Customer Details into Customer Tables
            bool werpMFProCreateCustomerResult = werpMFUploadsBo.WerpMFInsertCustomerDetails(adviserVo.advisorId, ProcessId, rmVo.RMId, out countCustCreated, out countFolioCreated);
            bool werpMFProCreateBankAccountResult = false;
            if (werpMFProCreateCustomerResult)
            {
                processlogVo.NoOfCustomerInserted += countCustCreated;
                processlogVo.NoOfAccountsInserted += countFolioCreated;
                //countRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, Contants.UploadExternalTypeWerp);
                processlogVo.NoOfRejectedRecords = countRejectedRecords;

                // Insert Bank Account Details
                packagePath = Server.MapPath("\\UploadPackages\\WerpMFProfileUploadPackageNew\\WerpMFProfileUploadPackageNew\\UploadWerpMFProfileNewBankAccountCreation.dtsx");
                werpMFProCreateBankAccountResult = werpMFUploadsBo.WerpMFCreationOfNewBankAccounts(ProcessId, packagePath, configPath);
                if (werpMFProCreateBankAccountResult)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.IsInsertionToXtrnlComplete = 2;
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

            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindWerpProfileGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRejectReasonFilter.Value = "";
                    BindWerpProfileGrid(ProcessId);
                }
            }
        }


        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvWERPProfileReject.HeaderRow.FindControl("ddlRejectReason");
            }
            return ddl;
        }

        private TextBox GetPanTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtPanSearch") != null)
            {
                txt = (TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtPanSearch");
            }
            return txt;
        }

        private TextBox GetBrokerCodeTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtBrokerSearch") != null)
            {
                txt = (TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtBrokerSearch");
            }
            return txt;
        }

        private TextBox GetCustomerNameTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtNameSearch") != null)
            {
                txt = (TextBox)gvWERPProfileReject.HeaderRow.FindControl("txtNameSearch");
            }
            return txt;
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            TextBox txtCustomerName = GetCustomerNameTextBox();
            TextBox txtBrokerCode = GetBrokerCodeTextBox();
            TextBox txtPan = GetPanTextBox();

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

            if (txtCustomerName != null)
                hdnCustomerNameFilter.Value = txtCustomerName.Text.Trim();
            if (txtPan != null)
                hdnPANFilter.Value = txtPan.Text.Trim();
            if (txtBrokerCode != null)
                hdnBrokerCodeFilter.Value = txtBrokerCode.Text.Trim();

            BindWerpProfileGrid(ProcessId);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }

        protected void lnkProfile_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedMFFolio','processId=" + processlogVo.ProcessId + "&filetypeId="+ filetypeId+"');", true);
        }

        protected void LinkInputRejects_Click(object sender, EventArgs e)
        {
            
            
            if(filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CAMSProfileFolioInputRejects','processId=" + processlogVo.ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('KarvyProfileFolioInputRejects','processId=" + processlogVo.ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TempletonProfileFolioInputRejects','processId=" + processlogVo.ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('DeutscheProfileFolioInputRejects','processId=" + processlogVo.ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('StandardProfileInputRejects','processId=" + processlogVo.ProcessId + "');", true);
           
        }

    }
}