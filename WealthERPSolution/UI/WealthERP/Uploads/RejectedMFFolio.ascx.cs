using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoUploads;
using System.Data;
using VoUploads;
using VoUser;
using WealthERP.Base;
using System.Configuration;
using BoCommon;
using System.Collections;
using Telerik.Web.UI;
using System.Web.Services;
using VoAdvisorProfiling;
using DaoUploads;
using BoSuperAdmin;
using BoAdvisorProfiling;

namespace WealthERP.Uploads
{
    public partial class RejectedMFFolio : System.Web.UI.UserControl
    {

        RejectedRecordsDao rejectedRecordsDao;
        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
        int ProcessId=0;
        int filetypeId;
        RejectedRecordsBo rejectedRecordsBo;
        DataSet dsRejectedRecords;
        string configPath;
        AdvisorVo adviserVo = new AdvisorVo();
        RMVo rmVo;
        UserVo userVo;
        UploadProcessLogVo processlogVo;
        public delegate void btnClick(object sender, EventArgs e);
        public event btnClick btnReprocessOnClick;
        DateTime fromDate;
        DateTime toDate;
        int rejectReasonCode;
        AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
        KarvyUploadsBo karvyUploadsBo = new KarvyUploadsBo();
        StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
        TempletonUploadsBo templetonUploadsBo = new TempletonUploadsBo();
        DeutscheUploadsBo deutscheUploadsBo = new DeutscheUploadsBo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();
        StandardFolioUploadBo standardFolioUploadBo = new StandardFolioUploadBo();
        WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
        StandardProfileUploadBo standardProfileUploadBo = new StandardProfileUploadBo();

        int adviserId;
        int rmId;

        string XMLConversionStatus = string.Empty;
        string InputInsertionStatus = string.Empty;
        string FirstStagingStatus = string.Empty;
        string SecondStagingStatus = string.Empty;
        string WERPInsertionStatus = string.Empty;
        string ExternalInsertionStatus = string.Empty;
        string packagePath = string.Empty;
        string xmlPath;
        string xmlFileName = string.Empty;
        string extracttype;

        DataTable dtgvWERPMF = new DataTable();
        // DataSet dsRejectedRecords = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            //ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            adviserId = adviserVo.advisorId;
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["adviserId"] != null)
            {
                adviserId = Int32.Parse(Request.QueryString["adviserId"].ToString());
                if (rmVo == null)
                {
                    DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);

                    if (dsAdviserRmDetails.Tables[0].Rows.Count > 0)
                    {
                        rmId = int.Parse(dsAdviserRmDetails.Tables[0].Rows[0]["ar_rmid"].ToString());
                        hfRmId.Value = rmId.ToString();
                    }

                }
            }
            


            if (adviserId == 1000)
            {
                tdLblAdviser.Visible = true;
                tdDdlAdviser.Visible = true;
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                {
                    adviserId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    if (hfRmId.Value != "")
                    {
                        rmId = Convert.ToInt32(hfRmId.Value);
                    }
                    Session["adviserId_Upload"] = adviserId;
                }
                else
                {
                    // Panel2.Visible = false;
                    adviserId = 1000;
                }
            }
            else
            {
                Session["adviserId_Upload"] = adviserId;
                // divConditional.Visible = false;
                tdLblAdviser.Visible = false;
                tdDdlAdviser.Visible = false;
                if (adviserVo.advisorId != 1000)
                {
                    adviserId = adviserVo.advisorId;
                }
                if(rmVo != null)
                rmId = rmVo.RMId;
            }
            if (Session["userVo"] != null)
            {

            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);
            }
            if (!IsPostBack)
            {
                gvCAMSProfileReject.Visible = false;
                divBtnActionSection.Visible = false;
                //    if (ProcessId != 0)
                //    {

                //    }
                txtFromTran.SelectedDate = DateTime.Now.AddMonths(-1).Date;
                txtToTran.SelectedDate = DateTime.Now;
                //DateTime fromDate = DateTime.Now.AddDays(-30);
                //txtFromTran.SelectedDate = fromDate.Date;
                //txtToTran.SelectedDate = DateTime.Now;
                LinkInputRejects.Visible = false;
                if (adviserId != 1000)
                {
                    if (ProcessId != 0)
                    {
                        divConditional.Visible = false;
                        tblProcessIdDetails.Visible = true;
                        lblProcessIdValue.Text = ProcessId.ToString();
                        if (Request.QueryString["extractType"] != null)
                            lblExtractTypeValue.Text = Request.QueryString["extractType"];
                        else
                            lblExtractTypeValue.Text = "";
                        if (Request.QueryString["uploadDate"] != null)
                            lblGetUploadDate.Text = Request.QueryString["uploadDate"];
                        else
                            lblGetUploadDate.Text = "";
                        if (Request.QueryString["RTName"] != null)
                            lblGetRTName.Text = Request.QueryString["RTName"];
                        else
                            lblGetRTName.Text = "";
                    }
                    else
                    {
                        divConditional.Visible = true;
                        tblProcessIdDetails.Visible = false;
                    }
                    BindddlRejectReason();
                    BindGrid(ProcessId);
                }
                else
                {
                    if (ProcessId != 0)
                    {
                        divConditional.Visible = false;
                        tblProcessIdDetails.Visible = false;
                        BindGrid(ProcessId);
                    }
                    else
                    {
                        divGvCAMSProfileReject.Visible = false;
                        BindAdviserDropDownList();
                        tdBtnViewRejetcs.Visible = false;
                        tdTxtToDate.Visible = false;
                        tdlblToDate.Visible = false;
                        tdTxtFromDate.Visible = false;
                        tdlblFromDate.Visible = false;
                        tdlblRejectReason.Visible = false;
                        tdDDLRejectReason.Visible = false;
                        tblProcessIdDetails.Visible = false;
                    }
                }

                //if (ProcessId != 0)
                //{
                //    divConditional.Visible = false;
                //    BindGrid(ProcessId);
                //}
                //else
                //{
                //    divGvCAMSProfileReject.Visible = false;
                //}
            }
            //gvCAMSProfileReject.Visible = true;
            trNote.Visible = false;

            if (Request.QueryString["filetypeid"] != null)
            {
                string uploadType = string.Empty;
                rejectedRecordsBo = new RejectedRecordsBo();
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
                if (filetypeId == 2)
                    uploadType = "CA";
                else if (filetypeId == 4)
                    uploadType = "KA";
                else if (filetypeId == 16)
                    uploadType = "TN";
                else if (filetypeId == 21)
                    uploadType = "SU";
                dsRejectedRecords = rejectedRecordsBo.GetProfileFolioInputRejects(ProcessId, uploadType);
                if (dsRejectedRecords.Tables[0].Rows.Count != 0)
                    LinkInputRejects.Visible = true;
                else
                    LinkInputRejects.Visible = false;
            }

        }

        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        public void BindddlRejectReason()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();
            processlogVo = new UploadProcessLogVo();
            rejectedRecordsBo = new RejectedRecordsBo();
            DataSet ds = rejectedRecordsBo.GetRejectReasonList(1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    genDictPriority.Add(dr["WRR_RejectReasonDescription"].ToString(), dr["WRR_RejectReasonCode"].ToString());
                }

                if (ddlRejectReason != null)
                {
                    ddlRejectReason.DataSource = genDictPriority;
                    ddlRejectReason.DataTextField = "Key";
                    ddlRejectReason.DataValueField = "Value";
                    ddlRejectReason.DataBind();
                }
            }

            ddlRejectReason.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string newPan = string.Empty;
            string newFolio = string.Empty;
            string newbroker = string.Empty;
            int StagingId = 0;
            // int CPS_Id = 0;
            int MainStagingId = 0;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();
            GridFooterItem footerRow = (GridFooterItem)gvCAMSProfileReject.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvCAMSProfileReject.Items)
            {
                if (((TextBox)footerRow.FindControl("txtPanMultiple")).Text.Trim() == "" && ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text.Trim() == "" && ((TextBox)footerRow.FindControl("txtBrokerMultiple")).Text.Trim() == "")
                {
                    newPan = ((TextBox)dr.FindControl("txtPan")).Text;
                    newFolio = ((TextBox)dr.FindControl("txtFolio")).Text;
                    newbroker = ((TextBox)dr.FindControl("txtBroker")).Text;
                }
                else
                {
                    newPan = ((TextBox)footerRow.FindControl("txtPanMultiple")).Text;
                    newFolio = ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text;
                    newbroker = ((TextBox)footerRow.FindControl("txtBrokerMultiple")).Text;
                }

                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                if (checkBox.Checked)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    StagingId = int.Parse((gvCAMSProfileReject.MasterTableView.DataKeyValues[selectedRow - 1]["CMFFS_Id"].ToString()));
                    MainStagingId = int.Parse((gvCAMSProfileReject.MasterTableView.DataKeyValues[selectedRow - 1]["CMFFS_MainStagingId"].ToString()));
                    // CPS_Id = int.Parse((gvCAMSProfileReject.MasterTableView.DataKeyValues[selectedRow - 1]["CPS_Id"].ToString()));
                    blResult = rejectedRecordsBo.UpdateMFFolioStaging(StagingId, MainStagingId, newPan, newFolio, newbroker);
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
            BindGrid(ProcessId);
            //NeedSource();
            //gvCAMSProfileReject.Rebind(); 

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
            //BindGrid(ProcessId);
            //gvCAMSProfileReject.MasterTableView.Rebind();
        }


        private void BindGrid(int ProcessId)
        {
            try
            {
                int selectedPage = 0;
                //if (ProcessId != 0)
                //{
                //    dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserId, ProcessId, fromDate, toDate, rejectReasonCode);
                //}
                //else
                //{
                //    rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
                //    fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
                //    toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
                //    dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserId, ProcessId, fromDate, toDate, rejectReasonCode);
                //}
                if (ProcessId == null || ProcessId == 0)
                {

                    rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
                    fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
                    toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
                    selectedPage = gvCAMSProfileReject.CurrentPageIndex;
                    // dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserId, ProcessId, fromDate, toDate, rejectReasonCode);
                }


                Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
                Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
                Dictionary<string, string> genDictIsCustomerExisting = new Dictionary<string, string>();
                rejectedRecordsBo = new RejectedRecordsBo();
                dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserId, ProcessId, fromDate, toDate, rejectReasonCode);

                if (dsRejectedRecords.Tables[0].Rows.Count > 0)
                {   // If Records found, then bind them to grid
                    divProfileMessage.Visible = false;
                    divGvCAMSProfileReject.Visible = true;
                    imgBtnrgHoldings.Visible = true;
                    divBtnActionSection.Visible = true;

                    if (Cache["RejectedMFFolioDetails" + adviserId.ToString()] == null)
                    {
                        Cache.Insert("RejectedMFFolioDetails" + adviserId.ToString(), dsRejectedRecords.Tables[0]);
                    }
                    else
                    {
                        Cache.Remove("RejectedMFFolioDetails" + adviserId.ToString());
                        Cache.Insert("RejectedMFFolioDetails" + adviserId.ToString(), dsRejectedRecords.Tables[0]);
                    }

                    gvCAMSProfileReject.CurrentPageIndex = selectedPage;
                    gvCAMSProfileReject.DataSource = dsRejectedRecords.Tables[0];
                    gvCAMSProfileReject.DataBind();
                    gvCAMSProfileReject.Visible = true;
                    trNote.Visible = true;


                }
                else
                {
                    divProfileMessage.Visible = true;
                    divBtnActionSection.Visible = false;
                    divGvCAMSProfileReject.Visible = false;
                    imgBtnrgHoldings.Visible = false;
                    gvCAMSProfileReject.Visible = false;
                    trNote.Visible = false;
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
                FunctionInfo.Add("Method", "RejectedCAMSProfile.ascx:BindGrid()");
                object[] objects = new object[1];
                objects[0] = ProcessId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /*************To delete the selected records ****************/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            string StagingID = string.Empty;
            foreach (GridDataItem item in this.gvCAMSProfileReject.Items)
            {
                if (((CheckBox)item.FindControl("chkBx")).Checked == true)
                {
                    StagingID += Convert.ToString(gvCAMSProfileReject.MasterTableView.DataKeyValues[item.ItemIndex]["CMFFS_Id"]) + "~";
                    i = i + 1;
                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                rejectedRecordsBo = new RejectedRecordsBo();
                rejectedRecordsBo.DeleteMFRejectedFolios(StagingID);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Selected item deleted !!');", true);

            }
            BindGrid(ProcessId);
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            string StagingID = string.Empty;

            ViewState.Remove("RejectReason");
            foreach (GridDataItem item in this.gvCAMSProfileReject.Items)
            {
                if (((CheckBox)item.FindControl("chkBx")).Checked == true)
                {
                    ProcessId = int.Parse(gvCAMSProfileReject.MasterTableView.DataKeyValues[item.ItemIndex]["ADUL_ProcessId"].ToString());
                    filetypeId = Int32.Parse(gvCAMSProfileReject.MasterTableView.DataKeyValues[item.ItemIndex]["WUXFT_XMLFileTypeId"].ToString());
                    extracttype = gvCAMSProfileReject.MasterTableView.DataKeyValues[item.ItemIndex]["XUET_ExtractTypeCode"].ToString();
                    break;

                }
            }

            UploadProcessLogVo processlogVo1 = new UploadProcessLogVo();
            processlogVo1 = uploadsCommonBo.GetProcessLogInfo(ProcessId);


            uploadsCommonBo = new UploadCommonBo();
            processlogVo1 = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            if (processlogVo1.IsInsertionToInputComplete == 1)
                InputInsertionStatus = "Y";
            else
                InputInsertionStatus = "N";
            if (processlogVo1.IsInsertionToFirstStagingComplete == 1)
                FirstStagingStatus = "Y";
            else
                FirstStagingStatus = "N";
            if (processlogVo1.IsInsertionToSecondStagingComplete == 1)
                SecondStagingStatus = "Y";
            else
                SecondStagingStatus = "N";
            if (processlogVo1.IsInsertionToWERPComplete == 1)
                WERPInsertionStatus = "Y";
            else
                WERPInsertionStatus = "N";

            Reprocess(ProcessId, filetypeId, InputInsertionStatus, FirstStagingStatus, SecondStagingStatus, WERPInsertionStatus);
        }


        public void reprocessMFFolio()
        {
            bool blResult = false;
            UploadCommonBo uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            UploadProcessLogVo TempprocesslogVo = new UploadProcessLogVo();
            StandardProfileUploadBo standardProfileUploadBo = new StandardProfileUploadBo();
            StandardFolioUploadBo standardFolioUploadBo = new StandardFolioUploadBo();
            CamsUploadsBo camsUploadsBo = new CamsUploadsBo();

            // BindGrid

            foreach (GridDataItem gdi in gvCAMSProfileReject.Items)
            {
                int selectedRow = 0;
                CheckBox cb = new CheckBox();
                cb = (CheckBox)gdi.FindControl("chkBx");
                if (cb.Checked)
                {
                    selectedRow = gdi.ItemIndex + 1;
                    ProcessId = int.Parse((gvCAMSProfileReject.MasterTableView.DataKeyValues[selectedRow - 1]["ProcessID"].ToString()));

                    break;
                }
            }

            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);
            if (uploadsCommonBo.ResetRejectedFlagByProcess(ProcessId, 9))
            {
                //Folio Chks in Std Folio Staging 
                //string packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                string packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\CheckFolioStaging.dtsx");
                bool camsFolioStagingChkResult = standardFolioUploadBo.MFFolioStagingCheck(packagePath, adviserId, ProcessId, configPath);
                if (camsFolioStagingChkResult)
                {
                    //Folio Chks in Std Folio Staging 
                    //packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\MoveDataFromStagingToMutufundAccount.dtsx");
                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.MfFolioExceptionFinalTableInsertion(packagePath, adviserId, ProcessId, configPath);
                    if (camsFolioWerpInsertionResult)
                    {
                        processlogVo.IsInsertionToWERPComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        if (processlogVo.FileTypeId == 2)
                        {
                            processlogVo.IsInsertionToWERPComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "CA");
                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "CA");
                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                        }
                        else if (processlogVo.FileTypeId == 4)
                        {
                            processlogVo.IsInsertionToWERPComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "KA");
                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "KA");
                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                        }
                        else if (processlogVo.FileTypeId == 16)
                        {
                            processlogVo.IsInsertionToWERPComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "TN");
                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "TN");
                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        }
                        else if (processlogVo.FileTypeId == 18)
                        {
                            processlogVo.IsInsertionToWERPComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(ProcessId, "DT");
                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(ProcessId, "WPMF");
                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(ProcessId, "DT");
                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        }
                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (blResult)
                        {
                            bool stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(ProcessId);
                        }
                    }
                }

            }

            if (blResult)
            {
                // Success Message
                msgReprocessComplete.Visible = true;
            }
            else
            {
                // Failure Message
                msgReprocessincomplete.Visible = true;
            }
            BindGrid(ProcessId);
        }


        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            if (Request.QueryString["filetypeid"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog');", true);

        }

        protected void LinkInputRejects_Click(object sender, EventArgs e)
        {
            if (filetypeId == (int)Contants.UploadTypes.CAMSProfile || filetypeId ==21)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CAMSProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('KarvyProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TempletonProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('DeutscheProfileFolioInputRejects','processId=" + ProcessId + "');", true);
            else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('StandardProfileInputRejects','processId=" + ProcessId + "');", true);

        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCAMSProfileReject.ExportSettings.OpenInNewWindow = true;
            gvCAMSProfileReject.ExportSettings.IgnorePaging = true;
            gvCAMSProfileReject.ExportSettings.HideStructureColumns = true;
            gvCAMSProfileReject.ExportSettings.ExportOnlyData = true;
            gvCAMSProfileReject.ExportSettings.FileName = "Rejected MF Folio Details";
            gvCAMSProfileReject.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCAMSProfileReject.MasterTableView.ExportToExcel();
        }

        protected void gvCAMSProfileReject_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            // trMessage.Visible = false;
            // DataSet dtRejectedMFFolioDetails = new DataSet();
            DataTable dtrr = new DataTable();
            dtrr = (DataTable)Cache["RejectedMFFolioDetails" + adviserId.ToString()];
            // dtrr = dtRejectedMFFolioDetails.Tables[0];
            if (ViewState["RejectReason"] != null)
                rcbType = ViewState["RejectReason"].ToString();
            if (!string.IsNullOrEmpty(rcbType))
            {
                DataView dvStaffList = new DataView(dtrr, "RejectReason = '" + rcbType + "'", "ADUL_ProcessId,CMFFS_INV_NAME,CMFSFS_PANNum,CMFSFS_FolioNum,PA_AMCName,CMFSS_BrokerCode,CMFFS_BANK_NAME,CMGCXP_ADDRESS1,CMGCXP_CITY,CMGCXP_PINCODE,CMGCXP_PHONE_OFF,CMGCXP_PHONE_RES,CMGCXP_DOB", DataViewRowState.CurrentRows);
                // DataView dvStaffList = dtMIS.DefaultView;
                gvCAMSProfileReject.DataSource = dvStaffList.ToTable();
                gvCAMSProfileReject.Visible = true;
                trNote.Visible = true;
            }
            else
            {
                gvCAMSProfileReject.DataSource = dtrr;
                gvCAMSProfileReject.Visible = true;
                trNote.Visible = true;

            }

        }

        protected void btnViewTran_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFromTran.SelectedDate.ToString()))
                if (txtFromTran.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
            if (txtToTran.SelectedDate != null)
                toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
            gvCAMSProfileReject.CurrentPageIndex = 0;
            BindGrid(ProcessId);
            ViewState.Remove("RejectReason");
            divLobAdded.Visible = false;
        }

        public void Reprocess(int processID, int filetypeId, string InputInsertionStatus, string FirstStagingStatus, string SecondStagingStatus, string WERPInsertionStatus)
        {

            camsUploadsBo = new CamsUploadsBo();
            karvyUploadsBo = new KarvyUploadsBo();
            uploadsCommonBo = new UploadCommonBo();
            processlogVo = new UploadProcessLogVo();
            werpEQUploadsBo = new WerpEQUploadsBo();
            templetonUploadsBo = new TempletonUploadsBo();
            standardFolioUploadBo = new StandardFolioUploadBo();
            standardProfileUploadBo = new StandardProfileUploadBo();
            deutscheUploadsBo = new DeutscheUploadsBo();

            int NoOfCustomersUploaded = 0;
            int NoOfFoliosUploaded = 0;
            int NoOfTransactionsUploaded = 0;
            int NoOfRejectedRecords = 0;
            int countCustCreated = 0;
            int countFolioCreated = 0;

            bool werpEQTradeInputResult = false;
            bool werpEQFirstStagingResult = false;
            bool werpEQFirstStagingCheckResult = false;
            bool werpEQSecondStagingResult = false;
            bool WERPEQSecondStagingCheckResult = false;
            bool WERPEQTradeWerpResult = false;
            bool stdFolioCommonDeleteResult = false;
            bool stdProCommonDeleteResult = false;

            xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);
            string extracttype = processlogVo.ExtractTypeCode;

            bool blResult = false;

            //tblReprocess.Visible = true;

            if (InputInsertionStatus == "Y")
            {
                if (FirstStagingStatus == "Y")
                {
                    if (SecondStagingStatus == "Y")
                    {
                        // WERP Insertion Logic

                        #region CAMS Profile
                        if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blCamsAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "CAMS");
                            }
                            // MF CAMS Profile Upload
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);  //StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (stdProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (camsProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts newly added 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool CreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);

                                        if (CreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (camsFolioStagingChkResult)
                                {
                                    //Folio Staging to WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (camsFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                        }
                        #endregion CAMS Profile

                        #region Sundaram Profile
                        if (filetypeId == 21)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blCamsAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Sundaram");
                            }
                            // MF Sundaram Profile Upload
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);  //StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (stdProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (camsProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts newly added 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool CreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);

                                        if (CreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (camsFolioStagingChkResult)
                                {
                                    //Folio Staging to WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (camsFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                        }
                        #endregion Sundaram Profile

                        #region Standard Equity Trade Account WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                        {
                            // Standard Equity Trade Account WERP Insertion
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                if (WERPEQTradeWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        #endregion Standard Equity Trade Account WERP Insertion

                        #region CAMS MF TRansaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                        {

                            bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(processID);
                            if (camsDatatranslationCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion CAMS MF TRansaction WERP Insertion

                        #region Sundaram MF TRansaction WERP Insertion
                        else if (filetypeId == 25)
                        {

                            bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(processID);
                            if (camsDatatranslationCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "SU");
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion Sundaram MF TRansaction WERP Insertion
                        #region WERP Equity Transation Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                        {
                            // WERP Equity Transation Insertion
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 8);
                                if (WERPEQTranWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        else if (filetypeId == (int)Contants.UploadTypes.IIFLTransaction)
                        {
                            // WERP Equity Transation Insertion
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                            if (WERPEQSecondStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 19);
                                if (WERPEQTranWerpResult)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                    processlogVo.EndTime = DateTime.Now;
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }
                        #endregion WERP Equity Transation Insertion

                        #region KARVY MF Transaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                        {


                            bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(processID);
                            if (karvyDataTranslationCheck)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                if (CommonTransChecks)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (karvyTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }
                        #endregion KARVY MF Transaction WERP Insertion

                        #region KARVY Profile + Folio WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Karvy");
                            }
                            //KARVY Profile + Folio WERP Insertion
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //Checks in Profile Staging
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (karvyProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (karvyProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (karvyProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }

                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging                            
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool karvyFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (karvyFolioStagingChkResult)
                                {
                                    //Inserting Folio into WERP Tables
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (blResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }

                        }
                        #endregion KARVY Profile + Folio WERP Insertion

                        #region Standard Profile WERP INsertion
                        else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                        {
                            //Standard Profile WERP INsertion
                            StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();

                            bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                            if (stdProCreateCustomerResult)
                            {
                                //Create new Bank Accounts
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                if (stdProCreateBankAccountResult)
                                {

                                    processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (blResult)
                                    {
                                        stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "WP");
                                    }
                                }
                            }
                        }
                        #endregion Standard Profile WERP INsertion

                        #region Templeton Transaction WERP Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                        {
                            // Update AMC - Sujith's Query
                            bool blTempAMCUpdated = uploadsCommonBo.UpdateAMCForTransactionReprocess(processID, "Templeton");

                            if (blTempAMCUpdated)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");
                                if (CommonTransChecks)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (camsTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Templeton Profile + Folio WERP Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blTempletonAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Templeton");
                            }

                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //common profile checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (templetonProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (templetonProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (templetonProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (templetonFolioStagingChkResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                    }
                                }
                            }
                            else if (filetypeId == (int)Contants.UploadTypes.ODINTransaction)
                            {
                                // WERP Equity Transation Insertion
                                werpEQUploadsBo = new WerpEQUploadsBo();
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                if (WERPEQSecondStagingCheckResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                    bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 19);
                                    if (WERPEQTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Deutsche Transaction WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                        {
                            // Update AMC - Sujith's Query
                            bool blDeutAMCUpdated = uploadsCommonBo.UpdateAMCForTransactionReprocess(processID, "Deutsche");

                            if (blDeutAMCUpdated)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                if (CommonTransChecks)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                    bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                    if (deutscheTranWerpResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                        processlogVo.EndTime = DateTime.Now;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    }
                                }
                            }


                        }
                        #endregion

                        #region Deutsche Profile + Folio WERP Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                        {
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                bool blDeutcheAMCUpdated = uploadsCommonBo.UpdateAMCForFolioReprocess(processID, "Deutsche");
                            }

                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                //common profile checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                if (deutscheProCommonChecksResult)
                                {
                                    // Insert Customer Details into WERP Tables
                                    bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                    if (deutscheProCreateCustomerResult)
                                    {
                                        //Create new Bank Accounts
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                        bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                        if (deutscheProCreateBankAccountResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                            processlogVo.NoOfCustomerInserted = processlogVo.NoOfCustomerInserted + countCustCreated;
                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                //Folio Chks in Std Folio Staging 
                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                if (deutscheFolioStagingChkResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                    bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioWerpInsertionResult)
                                    {
                                        processlogVo.IsInsertionToWERPComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (blResult)
                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                    }
                                }
                            }
                        }
                        #endregion

                        //****gobinda's MF STANDARD TRNX AND FOLIO****\\

                        #region Standard Transaction + Folio Insertion
                        else if (filetypeId == 6)
                        {

                            bool CommonStdTransChecks;
                            standardProfileUploadBo = new StandardProfileUploadBo();
                            string stdPackagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                            CommonStdTransChecks = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, stdPackagePath, configPath);

                            if (CommonStdTransChecks)
                            {
                                processlogVo.IsInsertionToWERPComplete = 1;
                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WP");
                                processlogVo.EndTime = DateTime.Now;

                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeStandard);

                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            }
                        }

                        #endregion

                        //-----------gobinda's SYSTEMATIC STANDARD-------------\\

                        #region STANDARD SYSTEMATIC
                        else if (filetypeId == 26)
                        {
                            bool standardSIPCommonStagingChk = false;
                            bool standardSIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            standardSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(processID, packagePath, configPath, "WPT");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "WPT");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (standardSIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                                standardSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (standardSIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "WPT");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }
                        }

                        #endregion



                        #region CAMS SYSTEMATIC
                        //CAMS SYSTEMATIC 
                        if (filetypeId == 20)
                        {
                            bool camsSIPCommonStagingChk = false;
                            bool camsSIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            camsSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(processID, packagePath, configPath, "CA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "CA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (camsSIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                                camsSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (camsSIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "CA");


                                    // processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "CA");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                }
                            }
                        }
                        #endregion

                        //-----------gobinda's KARVY STANDARD-------------\\
                        #region KARVY SYSTEMATIC
                        else if (filetypeId == 27)
                        {
                            bool karvySIPCommonStagingChk = false;
                            bool karvySIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            karvySIPCommonStagingChk = camsUploadsBo.KarvySIPCommonStagingChk(processID, packagePath, configPath, "KA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "KA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (karvySIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                karvySIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (karvySIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "KA");
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                        //-----------gobinda's Trail Commission Templeton-------------\\
                        #region Trail Commission
                        else if (filetypeId == 28)
                        {
                            AdvisorVo advisorVo = new AdvisorVo();
                            uploadsCommonBo = new UploadCommonBo();
                            //CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
                            bool TempletonTrailCommonStagingChk = false;
                            bool TempletonTrailCommonStagingToSetUp = false;
                            bool updateProcessLog = false;
                            string packagePath;


                            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);

                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\callOnReprocess.dtsx");

                            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(adviserId, processID, packagePath, configPath);
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "TN");

                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (TempletonTrailCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(processID, packagePath, configPath, "TN",adviserId);

                                if (TempletonTrailCommonStagingToSetUp)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "TN");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                        //-----------gobinda's Trail Commission CAMS-------------\\
                        #region Trail Commission
                        else if (filetypeId == 29)
                        {
                            AdvisorVo advisorVo = new AdvisorVo();
                            uploadsCommonBo = new UploadCommonBo();
                            //CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
                            bool TempletonTrailCommonStagingChk = false;
                            bool TempletonTrailCommonStagingToSetUp = false;
                            bool updateProcessLog = false;
                            string packagePath;


                            processlogVo = uploadsCommonBo.GetProcessLogInfo(processID);

                            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\callOnReprocess.dtsx");

                            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(adviserId, processID, packagePath, configPath);
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "TN");

                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (TempletonTrailCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(processID, packagePath, configPath, "CA" , adviserId);

                                if (TempletonTrailCommonStagingToSetUp)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "CA");
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion

                    }


                    else
                    {
                        #region CAMS Profile
                        // Call WERP Insertion Logic
                        if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                        {
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                if (camsProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {

                                        //common profile checks

                                        // Final step
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (stdProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (camsProCreateCustomerResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (camsFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (camsFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (camsFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                        }
                                    }
                                }
                            }


                        }
                        #endregion

                        //-----------gobinda's KARVY STANDARD-------------\\
                        #region KARVY SYSTEMATIC
                        else if (filetypeId == 27)
                        {
                            bool karvySIPCommonStagingChk = false;
                            bool karvySIPCommonStagingToWERP = false;
                            bool updateProcessLog = false;
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");
                            karvySIPCommonStagingChk = camsUploadsBo.KarvySIPCommonStagingChk(processID, packagePath, configPath, "KA");
                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(processID, "KA");
                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (karvySIPCommonStagingChk)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SipKarvyUploads\\SipKarvyUploads\\SipKarvyUploads\\UploadStandardTransactionCommonStagingToWERP.dtsx");
                                karvySIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(processID, packagePath, configPath);

                                if (karvySIPCommonStagingToWERP)
                                {
                                    processlogVo.IsInsertionToWERPComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(processID, "KA");
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                }
                            }

                        }

                        #endregion


                        #region Sundaram Profile
                        // Call WERP Insertion Logic
                        if (filetypeId == 21)
                        {
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                if (camsProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {

                                        //common profile checks

                                        // Final step
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (stdProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (camsProCreateCustomerResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (camsFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (camsFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (camsFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                        }
                                    }
                                }
                            }


                        }
                        #endregion



                        #region Standard Equity Trade Account Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                        {
                            // WERP Equity Insert To 2nd Staging Trade Account
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                            werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                            if (werpEQSecondStagingResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog3)
                                {

                                    // WERP Insertion
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                    WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                    if (WERPEQSecondStagingCheckResult)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                        WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                        if (WERPEQTradeWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region CAMS MF TRansaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                            bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (camsTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");

                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (camsTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Sundaram MF TRansaction Common Staging Insertion
                        else if (filetypeId == 25)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                            bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (camsTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");

                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (camsTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "Su");
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region WERP Equity Transation Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                        {
                            werpEQUploadsBo = new WerpEQUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                            werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                            if (werpEQFirstStagingCheckResult)
                            {
                                // WERP Equity Insert To 2nd Staging Transaction
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                if (werpEQSecondStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                    if (updateProcessLog3)
                                    {

                                        // Fourth step
                                        // WERP Insertion
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                        WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                        if (WERPEQSecondStagingCheckResult)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                            bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath); // EQ Trans XML File Type Id = 8
                                            if (WERPEQTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region KARVY MF Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                        {
                            KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                            bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                            if (karvyTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                    if (CommonTransChecks)
                                    {

                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (karvyTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region KARVY Profile + Folio Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                        {   // MF Karvy Profile Upload

                            // Data Translation checks in Karvy Staging
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                            bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                            if (karvyProStagingCheckResult)
                            {
                                if (extracttype == "PO" || extracttype == "PAF")
                                {
                                    //Inserting Data from Karvy Staging to Profile Staging
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                    bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                    if (karvyStagingToProfileStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //Making Chks in Profile Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (karvyProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                if (karvyProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (karvyProCreateBankAccountResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (extracttype == "FO" || extracttype == "PAF")
                                {
                                    if (karvyProStagingCheckResult)
                                    {
                                        // Inserting Karvy Staging Data to Folio Staging
                                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                        bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                        if (karvyStagingToFolioStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool karvyFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (karvyFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (blResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        #endregion

                        #region Standard Profile Common Staging INsertion
                        else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                        {
                            StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                            bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                            if (stdProCommonStagingResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog3)
                                {
                                    //common profile checks
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                    bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                    if (stdProCommonChecksResult)
                                    {
                                        // Insert Customer Details into WERP Tables
                                        bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                        if (stdProCreateCustomerResult)
                                        {
                                            //Create new Bank Accounts
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                            bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                            if (stdProCreateBankAccountResult)
                                            {

                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                {
                                                    stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Templeton Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                            bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                            if (templeTranStagingCheckResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                if (templeTranSecondStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                        if (CommonTransChecks)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (templeTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                //updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Templeton Profile + Folio Common Staging Insertion

                        else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                        {
                            // Insertion to common staging
                            if (extracttype == "PO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                if (templetonProCommonStagingResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog3)
                                    {
                                        //common profile checks
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                        bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                        if (templetonProCommonChecksResult)
                                        {
                                            // Insert Customer Details into WERP Tables
                                            bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                            if (templetonProCreateCustomerResult)
                                            {
                                                //Create new Bank Accounts
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                if (templetonProCreateBankAccountResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                    processlogVo.NoOfCustomerInserted = countCustCreated;
                                                    processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (templetonFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Deutsche Transaction Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                            bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                            if (deutscheTranStagingCheckResult)
                            {
                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                    bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                    if (CommonTransChecks)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                        bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                        if (deutscheTranWerpResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                            processlogVo.EndTime = DateTime.Now;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Deutsche Profile + Folio Common Staging Insertion
                        else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                        {
                            // Deutsche Insert To Staging Profile

                            // Doing a check on data translation
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                            bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                            if (deutscheProStagingCheckResult)
                            {
                                if (extracttype == "PO" || extracttype == "PAF")
                                {
                                    // Insertion to common staging
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                    bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                    if (deutscheProCommonStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //common profile checks
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (deutscheProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                if (deutscheProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (deutscheProCreateBankAccountResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {

                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (deutscheFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                else if (FirstStagingStatus == "N")
                {
                    #region CAMS Profile

                    if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                    {   // MF CAMS Profile Upload
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                        if (camsProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (camsProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging

                                        // Thirs step fails
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                        if (camsProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks

                                                // Final step

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {

                                                    // Insert Customer Details into WERP Tables
                                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (camsProCreateCustomerResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "FO" || extracttype == "PAF")
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                        if (camsFolioCommonStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (camsFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (camsFolioWerpInsertionResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }


                    #endregion

                    #region Sundaram Profile

                    if (filetypeId == 21)
                    {   // MF Sundaram Profile Upload
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                        if (camsProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (camsProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging

                                        // Thirs step fails
                                        packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                        bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                        if (camsProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks

                                                // Final step

                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {

                                                    // Insert Customer Details into WERP Tables
                                                    bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (camsProCreateCustomerResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "FO" || extracttype == "PAF")
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                        bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                        if (camsFolioCommonStagingResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                            bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                            if (camsFolioStagingChkResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                if (camsFolioWerpInsertionResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.EndTime = DateTime.Now;
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                                    processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    if (blResult)
                                                        stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);

                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }


                    #endregion


                    #region Standard Equity Trade Account First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                    {
                        // WERP Equity Insert To 1st Staging Trade Account
                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeAccInputToEQStdTradeAccStaging.dtsx");
                        werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTradeAccount(processID, packagePath, configPath);
                        if (werpEQFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog2)
                            {
                                // Doing a check on data in First Staging and marking IsRejected flag
                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQStdTradeAccStaging.dtsx");
                                werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                if (werpEQFirstStagingCheckResult)
                                {
                                    //Step 3:
                                    // WERP Equity Insert To 2nd Staging Trade Account
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                                    werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                    if (werpEQSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog3)
                                        {
                                            //Step 4:
                                            // WERP Insertion
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                            if (WERPEQSecondStagingCheckResult)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                                WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                                if (WERPEQTradeWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                    #endregion

                    #region CAMS MF TRansaction First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                    {
                        CamsUploadsBo camsUploadBo = new CamsUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                        bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);

                        if (camsTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                                bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (camsTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (camsTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Sundaram MF TRansaction First Staging Insertion

                    else if (filetypeId == 25)
                    {
                        CamsUploadsBo camsUploadBo = new CamsUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                        bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);

                        if (camsTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                                bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (camsTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (camsTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "SU");
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region WERP Equity Transation First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                    {
                        werpEQUploadsBo = new WerpEQUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTransactionInputToEQStdTranStaging.dtsx");
                        werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTransaction(processID, packagePath, configPath);
                        if (werpEQFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog2)
                            {
                                // Doing a check on data in First Staging and marking IsRejected flag
                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                                werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                                if (werpEQFirstStagingCheckResult)
                                {
                                    // WERP Equity Insert To 2nd Staging Transaction
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                    werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                    if (werpEQSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog3)
                                        {
                                            // WERP Insertion
                                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                            WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                            if (WERPEQSecondStagingCheckResult)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath);
                                                if (WERPEQTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region KARVY MF Transaction First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                    {
                        KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();
                        packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadKarvyXtrnlTransactionInputToKarvyXtrnlTransactionStaging.dtsx");
                        bool karvyTranStagingResult = karvyUploadBo.KarvyInsertToStagingTrans(processID, packagePath, configPath);
                        if (karvyTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog)
                            {

                                packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                                bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                                if (karvyTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                        if (CommonTransChecks)
                                        {

                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (karvyTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region KARVY Profile + Folio First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                    {   // MF Karvy Profile Upload

                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileInputToKarvyXtrnlProfileStaging .dtsx");
                        bool karvyProStagingResult = karvyUploadsBo.KARVYInsertToStagingProfile(processID, packagePath, configPath);
                        if (karvyProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Data Translation checks in Karvy Staging
                                packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                                bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                                if (karvyProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Inserting Karvy Staging Data to Profile Staging
                                        packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                        bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                        if (karvyStagingToProfileStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //Making Chks in Profile Staging
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (karvyProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (karvyProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (karvyProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        if (karvyProStagingCheckResult)
                                        {
                                            // Inserting Karvy Staging Data to Folio Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                            bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                            if (karvyStagingToFolioStagingResult)
                                            {

                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool stdFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (stdFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (blResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Standard Profile First Staging INsertion

                    else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                    {
                        StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileInputDataToFirstStaging.dtsx");
                        bool stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(processID, packagePath, configPath);
                        if (stdProFirstStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Data translation checks
                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsDataTranslationChecksInStdProfileMainStaging.dtsx");
                                bool stdProTranslationCheckStagingResult = StandardProfileUploadBo.StdDataTranslationCheckInFirstStaging(processID, packagePath, configPath);
                                if (stdProTranslationCheckStagingResult)
                                {
                                    // Insertion to common staging
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                                    bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                                    if (stdProCommonStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog3)
                                        {
                                            //common profile checks
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                            bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                            if (stdProCommonChecksResult)
                                            {
                                                // Insert Customer Details into WERP Tables
                                                bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                                if (stdProCreateCustomerResult)
                                                {
                                                    //Create new Bank Accounts
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                    bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                    if (stdProCreateBankAccountResult)
                                                    {

                                                        processlogVo.NoOfCustomerInserted = countCustCreated;
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                        {
                                                            stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Templeton Transaction First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                    {
                        packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransToStagingTable.dtsx");
                        bool templeTranStagingResult = templetonUploadsBo.TempletonInsertToStagingTrans(processID, packagePath, configPath);
                        if (templeTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                                bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                                if (templeTranStagingCheckResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                    bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                    if (templeTranSecondStagingResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (templeTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Templeton Profile + Folio First Staging Insertion

                    else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                    {
                        // Templeton Insert To Staging Profile
                        packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool templetonProStagingResult = templetonUploadsBo.TempInsertToStagingProfile(processID, packagePath, configPath);
                        if (templetonProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempDataTranslationChecksFirstStaging.dtsx");
                                bool templetonProStagingCheckResult = templetonUploadsBo.TempProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (templetonProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                        bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                        if (templetonProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (templetonProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (templetonProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (templetonProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (extracttype == "FO" || extracttype == "PAF")
                        {
                            if (templetonProStagingResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (templetonFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (templetonFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Deutsche Transaction First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                    {
                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionInputToFirstStaging.dtsx");
                        bool deutscheTranStagingResult = deutscheUploadsBo.DeutscheInsertToStagingTrans(processID, packagePath, configPath);
                        if (deutscheTranStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                            if (updateProcessLog)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                                bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                if (deutscheTranStagingCheckResult)
                                {
                                    processlogVo.IsInsertionToSecondStagingComplete = 1;
                                    processlogVo.EndTime = DateTime.Now;
                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                    if (updateProcessLog)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                        bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                        if (CommonTransChecks)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                            bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                            if (deutscheTranWerpResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                                processlogVo.EndTime = DateTime.Now;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Deutsche Profile + Folio First Staging Insertion
                    else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                    {
                        // Deutsche Insert To Staging Profile
                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                        bool deutscheProStagingResult = deutscheUploadsBo.DeutscheInsertToStagingProfile(processID, packagePath, configPath);
                        if (deutscheProStagingResult)
                        {
                            processlogVo.IsInsertionToFirstStagingComplete = 1;
                            processlogVo.EndTime = DateTime.Now;
                            bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                            if (updateProcessLog2)
                            {
                                // Doing a check on data translation
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                                bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                if (deutscheProStagingCheckResult)
                                {
                                    if (extracttype == "PO" || extracttype == "PAF")
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                        bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                        if (deutscheProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (deutscheProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                    if (deutscheProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (deutscheProCreateBankAccountResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (extracttype == "PO" || extracttype == "PAF")
                        {
                            if (deutscheProStagingResult)
                            {
                                packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                if (deutscheFolioCommonStagingResult)
                                {
                                    //Folio Chks in Std Folio Staging 
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                    bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                    if (deutscheFolioStagingChkResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                        bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioWerpInsertionResult)
                                        {
                                            processlogVo.IsInsertionToWERPComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (blResult)
                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    else
                    {
                        // Invalid Number
                    }
                }
            }
            else if (InputInsertionStatus == "N")
            {
                xmlFileName = Server.MapPath("\\UploadFiles\\" + processID + ".xml");

                #region CAMS Profile

                if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
                {   // MF CAMS Profile Upload
                    // CAMS Insert To Input Profile
                    // First step fails
                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSFileToXtrnlProfileInput.dtsx");
                    bool camsProInputResult = camsUploadsBo.CAMSInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (camsProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // CAMS Insert To Staging Profile

                            // Second step fails
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                            if (camsProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                    bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (camsProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging

                                            // Thirs step fails
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsProCommonStagingResult = camsUploadsBo.CAMSInsertToCommonStaging(processID, packagePath, configPath);
                                            if (camsProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks

                                                    // Final step

                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (stdProCommonChecksResult)
                                                    {

                                                        // Insert Customer Details into WERP Tables
                                                        bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (camsProCreateCustomerResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "CA");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadFolioDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsFolioCommonStagingResult = camsUploadsBo.CAMSInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                            if (camsFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (camsFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (camsFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "CA");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "CA");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }

                #endregion


                #region Sundaram Profile

                if (filetypeId == 21)
                {   // MF CAMS Profile Upload
                    // CAMS Insert To Input Profile
                    // First step fails
                    // Sundaram Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\SundramFileToInput.dtsx");
                    bool camsProInputResult = camsUploadsBo.SundramInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (camsProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // CAMS Insert To Staging Profile

                            // Second step fails
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool camsProStagingResult = camsUploadsBo.CAMSInsertToStagingProfile(processID, packagePath, configPath);
                            if (camsProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadDataTranslationChecksFirstStaging.dtsx");
                                    bool camsProStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (camsProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging

                                            // Thirs step fails
                                            packagePath = Server.MapPath("\\UploadPackages\\CAMSProfileUploadPackageNew\\CAMSProfileUploadPackageNew\\UploadProfileDataFromCAMSStagingToCommonStaging.dtsx");
                                            bool camsProCommonStagingResult = camsUploadsBo.SundaramInsertToCommonStaging(processID, packagePath, configPath);
                                            if (camsProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks

                                                    // Final step

                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool stdProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (stdProCommonChecksResult)
                                                    {

                                                        // Insert Customer Details into WERP Tables
                                                        bool camsProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (camsProCreateCustomerResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "SU");
                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadFolioDataFromSundaramStagingToCommonStaging.dtsx");
                                            bool camsFolioCommonStagingResult = camsUploadsBo.SundaramInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                            if (camsFolioCommonStagingResult)
                                            {
                                                //Folio Chks in Std Folio Staging 
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                bool camsFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                if (camsFolioStagingChkResult)
                                                {
                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                    if (camsFolioWerpInsertionResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.EndTime = DateTime.Now;
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "SU");
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "SU");
                                                        processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                        if (blResult)
                                                            stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                    }
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }

                #endregion
                #region Standard Equity Trade Account Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTradeAccount)
                {
                    // WERP Equity Insert To Input Profile
                    //Step 1:
                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadTradeAccDataFromEQStdFileToEQStdTradeAccInput.dtsx");
                    werpEQTradeInputResult = werpEQUploadsBo.WerpEQInsertToInputTradeAccount(packagePath, xmlFileName, configPath);
                    if (werpEQTradeInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 2;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            //Step2:
                            // WERP Equity Insert To 1st Staging Trade Account
                            packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeAccInputToEQStdTradeAccStaging.dtsx");
                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTradeAccount(processID, packagePath, configPath);
                            if (werpEQFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog2)
                                {
                                    // Doing a check on data in First Staging and marking IsRejected flag
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQStdTradeAccStaging.dtsx");
                                    werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                    if (werpEQFirstStagingCheckResult)
                                    {
                                        //Step 3:
                                        // WERP Equity Insert To 2nd Staging Trade Account
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQStdTradeStagingToEQTradeStaging.dtsx");
                                        werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTradeAccount(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                        if (werpEQSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog3)
                                            {
                                                //Step 4:
                                                // WERP Insertion
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadChecksOnEQTradeStaging.dtsx");
                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTradeAccount(processID, packagePath, configPath, adviserId);

                                                if (WERPEQSecondStagingCheckResult)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\EQTradeAccountUploadPackage\\UploadEQTradeStagingToWerp.dtsx");
                                                    WERPEQTradeWerpResult = werpEQUploadsBo.WERPEQInsertTradeAccountDetails(processID, packagePath, configPath);
                                                    if (WERPEQTradeWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPEQ");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetAccountsUploadRejectCount(processID, "WPEQ");
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region CAMS MF TRansaction Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.CAMSTransaction)
                {
                    CamsUploadsBo camsUploadBo = new CamsUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                    bool camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (camsTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                            bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);
                            if (camsTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadChecksCAMSTransactionStaging.dtsx");
                                    bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (camsTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "CA", "CAMS");
                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (camsTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeCAMS);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion


                #region Sundaram MF TRansaction Input Insertion

                else if (filetypeId == 25)
                {
                    CamsUploadsBo camsUploadBo = new CamsUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadTransactionDataFromCAMSFileToXtrnlTransactionInput.dtsx");
                    bool camsTranInputResult = camsUploadsBo.CAMSInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (camsTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\CAMSTransactionUploadPackageNew\\CAMSTransactionUploadPackageNew\\UploadXtrnlTransactionInputToXtrnlTransactionStaging.dtsx");
                            bool camsTranStagingResult = camsUploadsBo.CAMSInsertToStagingTrans(processID, packagePath, configPath);
                            if (camsTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\SundramProfileUploadNew\\SundramProfileUploadNew\\UploadChkSundaramTransactionStaging.dtsx");
                                    bool camsTranStagingCheckResult = camsUploadsBo.CAMSProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (camsTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "SU", "Sundaram");
                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool camsTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (camsTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "");
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region WERP Equity Transation Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.EquityStandardTransaction)
                {
                    werpEQUploadsBo = new WerpEQUploadsBo();
                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadTransactionDataFromEQStdFileToEQStdTranInput.dtsx");
                    bool werpEQTranInputResult = werpEQUploadsBo.WerpEQInsertToInputTransaction(packagePath, xmlFileName, configPath);
                    if (werpEQTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 2;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            // WERP Equity Insert To 1st Staging Transaction
                            packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTransactionInputToEQStdTranStaging.dtsx");
                            werpEQFirstStagingResult = werpEQUploadsBo.WerpEQInsertToFirstStagingTransaction(processID, packagePath, configPath);
                            if (werpEQFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog2)
                                {
                                    // Doing a check on data in First Staging and marking IsRejected flag
                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQStdTranStaging.dtsx");
                                    werpEQFirstStagingCheckResult = werpEQUploadsBo.WerpEQProcessDataInFirstStagingTrans(processID, packagePath, configPath, adviserId);

                                    if (werpEQFirstStagingCheckResult)
                                    {
                                        // WERP Equity Insert To 2nd Staging Transaction
                                        packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQStdTranStagingToEQTranStaging.dtsx");
                                        werpEQSecondStagingResult = werpEQUploadsBo.WerpEQInsertToSecondStagingTransaction(processID, packagePath, configPath, 8); // EQ Trans File Type Id = 8

                                        if (werpEQSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog3)
                                            {
                                                // WERP Insertion
                                                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
                                                WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(processID, packagePath, configPath, adviserId);

                                                if (WERPEQSecondStagingCheckResult)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                                                    bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(processID, packagePath, configPath);
                                                    if (WERPEQTranWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPEQ");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, "WPEQ");
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region KARVY MF Transaction Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.KarvyTransaction)
                {
                    KarvyUploadsBo karvyUploadBo = new KarvyUploadsBo();

                    packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadTransactionDataFromKarvyFileToXtrnlTransactionInput.dtsx");
                    bool karvyTranInputResult = karvyUploadBo.KarvyInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (karvyTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog)
                        {

                            packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadKarvyXtrnlTransactionInputToKarvyXtrnlTransactionStaging.dtsx");
                            bool karvyTranStagingResult = karvyUploadBo.KarvyInsertToStagingTrans(processID, packagePath, configPath);
                            if (karvyTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog)
                                {

                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyTransactionUploadPackageNew\\KarvyTransactionUploadPackageNew\\UploadChecksKarvyTransactionStaging.dtsx");
                                    bool karvyTranStagingCheckResult = karvyUploadBo.KarvyProcessDataInStagingTrans(processID, packagePath, configPath);
                                    if (karvyTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "KA", "Karvy");
                                            if (CommonTransChecks)
                                            {

                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool karvyTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (karvyTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeKarvy);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region KARVY Profile + Folio Input Insertion

                else if (filetypeId == (int)Contants.UploadTypes.KarvyProfile)
                {   // MF Karvy Profile Upload

                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadProfileDataFromKarvyProfileFileToXtrnlProfileInput.dtsx");
                    bool karvyProInputResult = karvyUploadsBo.KARVYInsertToInputProfile(packagePath, processID, xmlFileName, configPath);
                    if (karvyProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog1)
                        {
                            // Karvy Insert To Staging Profile
                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileInputToKarvyXtrnlProfileStaging .dtsx");
                            bool karvyProStagingResult = karvyUploadsBo.KARVYInsertToStagingProfile(processID, packagePath, configPath);
                            if (karvyProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Data Translation checks in Karvy Staging
                                    packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadDataTranslationKarvyProfileStaging.dtsx");
                                    bool karvyProStagingCheckResult = karvyUploadsBo.KARVYStagingDataTranslationCheck(processID, packagePath, configPath);
                                    if (karvyProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Inserting Karvy Staging Data to Profile Staging
                                            packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToProfileStaging.dtsx");
                                            bool karvyStagingToProfileStagingResult = karvyUploadsBo.KARVYStagingInsertToProfileStaging(processID, packagePath, configPath);
                                            if (karvyStagingToProfileStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //Making Chks in Profile Staging
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool karvyProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (karvyProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool karvyProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (karvyProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool karvyProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (karvyProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "KA");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (extracttype == "FO" || extracttype == "PAF")
                                        {
                                            if (karvyProStagingCheckResult)
                                            {
                                                // Inserting Karvy Staging Data to Folio Staging
                                                packagePath = Server.MapPath("\\UploadPackages\\KarvyProfileUploadPackageNew\\KarvyProfileUploadPackageNew\\UploadKarvyXtrnlProfileStagingToFolioStaging.dtsx");
                                                bool karvyStagingToFolioStagingResult = karvyUploadsBo.KARVYStagingInsertToFolioStaging(processID, packagePath, configPath);
                                                if (karvyStagingToFolioStagingResult)
                                                {

                                                    //Folio Chks in Std Folio Staging 
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                                    bool stdFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                                    if (stdFolioStagingChkResult)
                                                    {
                                                        //Folio Chks in Std Folio Staging 
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                                        blResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                                        if (blResult)
                                                        {
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "KA");
                                                            processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "KA");
                                                            processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                                stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Standard Profile Input INsertion

                else if (filetypeId == (int)Contants.UploadTypes.StandardProfile)
                {  // Std Insert To Input Profile
                    StandardProfileUploadBo StandardProfileUploadBo = new StandardProfileUploadBo();
                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileDataFromFileToInput.dtsx");
                    bool stdProInputResult = StandardProfileUploadBo.StdInsertToInputProfile(packagePath, xmlFileName, configPath);
                    if (stdProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // Std Insert To First Staging Profile
                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileInputDataToFirstStaging.dtsx");
                            bool stdProFirstStagingResult = StandardProfileUploadBo.StdInsertToFirstStaging(processID, packagePath, configPath);
                            if (stdProFirstStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Data translation checks
                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsDataTranslationChecksInStdProfileMainStaging.dtsx");
                                    bool stdProTranslationCheckStagingResult = StandardProfileUploadBo.StdDataTranslationCheckInFirstStaging(processID, packagePath, configPath);
                                    if (stdProTranslationCheckStagingResult)
                                    {
                                        // Insertion to common staging
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadStdProfileFirstStagingToCommonStaging.dtsx");
                                        bool stdProCommonStagingResult = StandardProfileUploadBo.StdInsertToCommonStaging(processID, packagePath, configPath);
                                        if (stdProCommonStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                            if (updateProcessLog3)
                                            {
                                                //common profile checks
                                                packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                bool stdProCommonChecksResult = StandardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                if (stdProCommonChecksResult)
                                                {
                                                    // Insert Customer Details into WERP Tables
                                                    bool stdProCreateCustomerResult = StandardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out  countCustCreated);
                                                    if (stdProCreateCustomerResult)
                                                    {
                                                        //Create new Bank Accounts
                                                        packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                        bool stdProCreateBankAccountResult = StandardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                        if (stdProCreateBankAccountResult)
                                                        {

                                                            processlogVo.NoOfCustomerInserted = countCustCreated;
                                                            processlogVo.IsInsertionToWERPComplete = 1;
                                                            processlogVo.EndTime = DateTime.Now;
                                                            processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "WP");
                                                            blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                            if (blResult)
                                                            {
                                                                stdProCommonDeleteResult = StandardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "WP");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Templeton Transaction Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.TempletonTransaction)
                {
                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTransactionFromXmlFileToInputTable.dtsx");
                    bool templeTranInputResult = templetonUploadsBo.TempletonInsertToInputTrans(processID, packagePath, xmlFileName, configPath);
                    if (templeTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransToStagingTable.dtsx");
                            bool templeTranStagingResult = templetonUploadsBo.TempletonInsertToStagingTrans(processID, packagePath, configPath);
                            if (templeTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadChecksOnTempletonStaging.dtsx");
                                    bool templeTranStagingCheckResult = templetonUploadsBo.TempletonProcessDataInStagingTrans(processID, packagePath, configPath);
                                    if (templeTranStagingCheckResult)
                                    {
                                        packagePath = Server.MapPath("\\UploadPackages\\TempletonTransactionUploadPackage\\TempletonTransactionUploadPackage\\UploadTempletonTransStagingToTransStaging.dtsx");
                                        bool templeTranSecondStagingResult = templetonUploadsBo.TempletonInsertFromTempStagingTransToCommonStaging(processID, packagePath, configPath);
                                        if (templeTranSecondStagingResult)
                                        {
                                            processlogVo.IsInsertionToSecondStagingComplete = 1;
                                            processlogVo.EndTime = DateTime.Now;
                                            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                            if (updateProcessLog)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                                bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "TN", "Templeton");

                                                if (CommonTransChecks)
                                                {
                                                    packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                    bool templeTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                    if (templeTranWerpResult)
                                                    {
                                                        processlogVo.IsInsertionToWERPComplete = 1;
                                                        processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                        processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeTemp);
                                                        processlogVo.EndTime = DateTime.Now;
                                                        blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Templeton Profile + Folio Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.TempletonProfile)
                {
                    // Templeton Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempletonProfileDataFromFileToInput.dtsx");
                    bool templetonProInputResult = templetonUploadsBo.TempInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (templetonProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";

                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // Templeton Insert To Staging Profile
                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool templetonProStagingResult = templetonUploadsBo.TempInsertToStagingProfile(processID, packagePath, configPath);
                            if (templetonProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadTempDataTranslationChecksFirstStaging.dtsx");
                                    bool templetonProStagingCheckResult = templetonUploadsBo.TempProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (templetonProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging
                                            packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadProfileDataFromTempStagingToCommonStaging.dtsx");
                                            bool templetonProCommonStagingResult = templetonUploadsBo.TempInsertToCommonStaging(processID, packagePath, configPath);
                                            if (templetonProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool templetonProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (templetonProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool templetonProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (templetonProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool templetonProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (templetonProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "TN");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                if (templetonProStagingResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\TempletonProfileUploadPackageNew\\TempletonProfileUploadPackageNew\\UploadFolioDataFromTempStagingToCommonStaging.dtsx");
                                    bool templetonFolioCommonStagingResult = templetonUploadsBo.TempInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                    if (templetonFolioCommonStagingResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                        bool templetonFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                        if (templetonFolioStagingChkResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                            bool templetonFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                            if (templetonFolioWerpInsertionResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "TN");
                                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "TN");
                                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Deutsche Transaction Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.DeutscheTransaction)
                {
                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionXMLFileToInputTable.dtsx");
                    bool deutscheTranInputResult = deutscheUploadsBo.DeutscheInsertToInputTrans(processID, packagePath, xmlFileName, configPath);

                    if (deutscheTranInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                        if (updateProcessLog)
                        {
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionInputToFirstStaging.dtsx");
                            bool deutscheTranStagingResult = deutscheUploadsBo.DeutscheInsertToStagingTrans(processID, packagePath, configPath);
                            if (deutscheTranStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                if (updateProcessLog)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheTransactionUploadPackage\\DeutscheTransactionUploadPackage\\UploadDeutscheTransactionFirstStagingtoSecondStaging.dtsx");
                                    bool deutscheTranStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingTrans(processID, adviserId, packagePath, configPath);
                                    if (deutscheTranStagingCheckResult)
                                    {
                                        processlogVo.IsInsertionToSecondStagingComplete = 1;
                                        processlogVo.EndTime = DateTime.Now;
                                        updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);

                                        if (updateProcessLog)
                                        {
                                            packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\ChecksCommonUploadPackage.dtsx");
                                            bool CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, processID, packagePath, configPath, "DT", "Deutsche");

                                            if (CommonTransChecks)
                                            {
                                                packagePath = Server.MapPath("\\UploadPackages\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\MFTransactionCommonUploadPackage\\InsertTransactionIntoWERP.dtsx");
                                                bool deutscheTranWerpResult = uploadsCommonBo.InsertTransToWERP(processID, packagePath, configPath);
                                                if (deutscheTranWerpResult)
                                                {
                                                    processlogVo.IsInsertionToWERPComplete = 1;
                                                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(processID, "WPMF");
                                                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(processID, Contants.UploadExternalTypeDeutsche);
                                                    processlogVo.EndTime = DateTime.Now;
                                                    updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Deutsche Profile + Folio Input Insertion
                else if (filetypeId == (int)Contants.UploadTypes.DeutscheProfile)
                {
                    // Deutsche Insert To Input Profile
                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheProfileDataFromFileToInput.dtsx");
                    bool deutscheProInputResult = deutscheUploadsBo.DeutscheInsertToInputProfile(processID, packagePath, xmlFileName, configPath);
                    if (deutscheProInputResult)
                    {
                        processlogVo.IsInsertionToInputComplete = 1;
                        processlogVo.IsInsertionToXtrnlComplete = 1;
                        processlogVo.EndTime = DateTime.Now;
                        processlogVo.XMLFileName = processlogVo.ProcessId.ToString() + ".xml";
                        bool updateProcessLog1 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                        if (updateProcessLog1)
                        {
                            // Deutsche Insert To Staging Profile
                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheXtrnlProfileInputToXtrnlProfileStaging.dtsx");
                            bool deutscheProStagingResult = deutscheUploadsBo.DeutscheInsertToStagingProfile(processID, packagePath, configPath);
                            if (deutscheProStagingResult)
                            {
                                processlogVo.IsInsertionToFirstStagingComplete = 1;
                                processlogVo.EndTime = DateTime.Now;
                                bool updateProcessLog2 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                if (updateProcessLog2)
                                {
                                    // Doing a check on data translation
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadDeutscheDataTranslationChecksFirstStaging.dtsx");
                                    bool deutscheProStagingCheckResult = deutscheUploadsBo.DeutscheProcessDataInStagingProfile(processID, adviserId, packagePath, configPath);
                                    if (deutscheProStagingCheckResult)
                                    {
                                        if (extracttype == "PO" || extracttype == "PAF")
                                        {
                                            // Insertion to common staging
                                            packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadProfileDataFromDeutscheStagingToCommonStaging.dtsx");
                                            bool deutscheProCommonStagingResult = deutscheUploadsBo.DeutscheInsertToCommonStaging(processID, packagePath, configPath);
                                            if (deutscheProCommonStagingResult)
                                            {
                                                processlogVo.IsInsertionToSecondStagingComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                bool updateProcessLog3 = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (updateProcessLog3)
                                                {
                                                    //common profile checks
                                                    packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadsCommonProfileChecksInProfileStaging.dtsx");
                                                    bool deutscheProCommonChecksResult = standardProfileUploadBo.StdCommonProfileChecks(processID, adviserId, packagePath, configPath);
                                                    if (deutscheProCommonChecksResult)
                                                    {
                                                        // Insert Customer Details into WERP Tables
                                                        bool deutscheProCreateCustomerResult = standardProfileUploadBo.StdInsertCustomerDetails(adviserId, processID, rmId, processlogVo.BranchId, xmlPath, out countCustCreated);
                                                        if (deutscheProCreateCustomerResult)
                                                        {
                                                            //Create new Bank Accounts
                                                            packagePath = Server.MapPath("\\UploadPackages\\StandardProfileUploadPackageNew\\StandardProfileUploadPackageNew\\UploadCreateNewBankAccount.dtsx");
                                                            bool deutscheProCreateBankAccountResult = standardProfileUploadBo.StdCreationOfNewBankAccounts(processID, packagePath, configPath, adviserId);
                                                            if (deutscheProCreateBankAccountResult)
                                                            {
                                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                                processlogVo.EndTime = DateTime.Now;
                                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadProfileRejectCount(processID, "DT");
                                                                processlogVo.NoOfCustomerInserted = countCustCreated;
                                                                processlogVo.NoOfCustomerDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfCustomerInserted - processlogVo.NoOfRejectedRecords;
                                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                                if (blResult)
                                                                    stdProCommonDeleteResult = standardProfileUploadBo.StdDeleteCommonStaging(processID);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (extracttype == "FO" || extracttype == "PAF")
                            {
                                if (deutscheProStagingResult)
                                {
                                    packagePath = Server.MapPath("\\UploadPackages\\DeutscheProfileUploadPackageNew\\DeutscheProfileUploadPackageNew\\UploadFolioDataFromDeutscheStagingToCommonStaging.dtsx");
                                    bool deutscheFolioCommonStagingResult = deutscheUploadsBo.DeutscheInsertFolioDataToFolioCommonStaging(processID, packagePath, configPath);
                                    if (deutscheFolioCommonStagingResult)
                                    {
                                        //Folio Chks in Std Folio Staging 
                                        packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsCommonFolioChecksInFolioStaging.dtsx");
                                        bool deutscheFolioStagingChkResult = standardFolioUploadBo.StdFolioChksInFolioStaging(packagePath, adviserId, processID, configPath);
                                        if (deutscheFolioStagingChkResult)
                                        {
                                            //Folio Chks in Std Folio Staging 
                                            packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                                            bool deutscheFolioWerpInsertionResult = standardFolioUploadBo.StdCustomerFolioCreation(packagePath, adviserId, processID, configPath);
                                            if (deutscheFolioWerpInsertionResult)
                                            {
                                                processlogVo.IsInsertionToWERPComplete = 1;
                                                processlogVo.EndTime = DateTime.Now;
                                                processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetProfileUploadRejectCount(processID, "DT");
                                                processlogVo.NoOfAccountsInserted = uploadsCommonBo.GetAccountsUploadCount(processID, "WPMF");
                                                processlogVo.NoOfInputRejects = uploadsCommonBo.GetUploadProfileInputRejectCount(processID, "DT");
                                                processlogVo.NoOfAccountDuplicates = processlogVo.NoOfTotalRecords - processlogVo.NoOfAccountsInserted - processlogVo.NoOfRejectedRecords;
                                                blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                                                if (blResult)
                                                    stdFolioCommonDeleteResult = standardFolioUploadBo.StdDeleteCommonStaging(processID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

            }
            else
            {
                // Invalid Number
            }

            if (blResult)
            {
                // Display Success Message
                msgReprocessComplete.Visible = true;

                if (extracttype == "PO" || extracttype == "PAF")
                {   // Check if Profile Upload

                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = false;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "FO")
                {   // Check if Profile Upload

                    //trUploadedCustomers.Visible = false;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = false;
                    //trRejectedRecords.Visible = true;

                    ////txtUploadedCustomers.Text = NoOfCustomersUploaded.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "MFT" || extracttype == "ET")
                {   // Check if Transaction Upload

                    //trUploadedCustomers.Visible = false;
                    //trUploadedFolios.Visible = false;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "")
                {   // Check if Combination Upload

                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                else if (extracttype == "SS")
                {   // Check if Combination Upload

                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                else if (extracttype == "TRAIL")
                {
                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }

                if (Request.QueryString["processId"] != null)
                    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                else
                {
                    ProcessId = 0;
                }
                BindGrid(ProcessId);
            }
            else
            {
                // Display Failure
                msgReprocessincomplete.Visible = true;

                if (extracttype == "PO" || extracttype == "PAF")
                {   // Check if Profile Upload

                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = false;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                if (extracttype == "FO")
                {   // Check if Profile Upload

                    //trUploadedCustomers.Visible = false;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = false;
                    //trRejectedRecords.Visible = true;

                    ////txtUploadedCustomers.Text = NoOfCustomersUploaded.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "MFT" || extracttype == "ET")
                {   // Check if Transaction Upload

                    //trUploadedCustomers.Visible = false;
                    //trUploadedFolios.Visible = false;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();

                }
                else if (extracttype == "")
                {   // Check if Combination Upload

                    //trUploadedCustomers.Visible = true;
                    //trUploadedFolios.Visible = true;
                    //trUploadedTransactions.Visible = true;
                    //trRejectedRecords.Visible = true;

                    //txtUploadedCustomers.Text = processlogVo.NoOfCustomerInserted.ToString();
                    //txtUploadedFolios.Text = processlogVo.NoOfAccountsInserted.ToString();
                    //txtUploadedTransactions.Text = processlogVo.NoOfTransactionInserted.ToString();
                    //txtRejectedRecords.Text = processlogVo.NoOfRejectedRecords.ToString();
                }
                else
                {
                    //trUploadedCustomers.Visible = false;
                    //trUploadedFolios.Visible = false;
                    //trUploadedTransactions.Visible = false;
                    //trRejectedRecords.Visible = false;
                }

                BindGrid(ProcessId);
            }
        }


        protected void gvCAMSProfileReject_PreRender(object sender, EventArgs e)
        {
            if (gvCAMSProfileReject.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {
            dtgvWERPMF = (DataTable)Cache["RejectedMFFolioDetails" + adviserId.ToString()];
            // dtgvWERPMF = dsRejectedRecords.Tables[0];
            DataView view = new DataView(dtgvWERPMF);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvCAMSProfileReject.MasterTableView.FilterExpression.ToString());
            gvCAMSProfileReject.MasterTableView.Rebind();
        }


        protected void gvCAMSProfileReject_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dtgvWERPMF = (DataTable)Cache["RejectedMFFolioDetails" + adviserId.ToString()];

                Session["dt"] = dtgvWERPMF;
                DataTable dtcustMIS = new DataTable();
                dtcustMIS.Columns.Add("RejectReason");
                //dtcustMIS.Columns.Add("RejectReason");
                // dtcustMIS.Columns.Add("SystematicTransactionType");
                DataRow drcustMIS;
                foreach (DataRow dr in dtgvWERPMF.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["RejectReason"] = dr["RejectReason"].ToString();

                    dtcustMIS.Rows.Add(drcustMIS);
                }
                //combo.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("ALL", "0"));
                DataView view = new DataView(dtgvWERPMF);
                DataTable distinctValues = view.ToTable(true, "RejectReason");
                RadComboBoxIN.DataSource = distinctValues;
                RadComboBoxIN.DataValueField = dtcustMIS.Columns["RejectReason"].ToString();
                RadComboBoxIN.DataTextField = dtcustMIS.Columns["RejectReason"].ToString();
                //RadComboBoxIN.ClearSelection();
                RadComboBoxIN.DataBind();

            }

        }
        protected void RadComboBoxRR_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReason"] = dropdown.SelectedValue.ToString();
            if (ViewState["RejectReason"] != "")
            {
                //    gvWERPTrans.MasterTableView.FilterExpression = "([RejectReason]= '" + dropdown.SelectedValue + "')";
                GridColumn column = gvCAMSProfileReject.MasterTableView.GetColumnSafe("RejectReason");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCAMSProfileReject.CurrentPageIndex = 0;
                gvCAMSProfileReject.MasterTableView.Rebind();
                //   // column.CurrentFilterValue = dropdown.SelectedValue.ToString();



                //    //+ Combo.SelectedValue +
            }
            else
            {
                //gvCAMSProfileReject.MasterTableView.FilterExpression = "";
                GridColumn column = gvCAMSProfileReject.MasterTableView.GetColumnSafe("RejectReason");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvCAMSProfileReject.CurrentPageIndex = 0;
                gvCAMSProfileReject.MasterTableView.Rebind();
                //  //  column.CurrentFilterValue = dropdown.SelectedValue.ToString();
                //    // dropdown.SelectedValue = ViewState["RejectReason"].ToString();

            }

        }


        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["RejectReason"] != null)
            {

                Combo.SelectedValue = ViewState["RejectReason"].ToString();
            }

        }




        protected void btnAddLob_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            int advisorId = adviserId;
            string adviserOrgName = adviserVo.OrganizationName;
            int userId = userVo.UserId;
            string segment = "";
            string assetClass = "MF";
            string category = "INT";
            bool lobAdded = false;

            try
            {
                int i = 0;
                string strLob = string.Empty;
                foreach (GridDataItem item in this.gvCAMSProfileReject.Items)
                {
                    if (((CheckBox)item.FindControl("chkBx")).Checked == true)
                    {
                        strLob = Convert.ToString(gvCAMSProfileReject.MasterTableView.DataKeyValues[i]["CMFSS_BrokerCode"]);
                        break;
                    }
                    i++;
                }
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.IdentifierTypeCode = "ARN";
                advisorLOBVo.OrganizationName = adviserOrgName;
                advisorLOBVo.Identifier = strLob;
                advisorLOBVo.ValidityDate = DateTime.Now.AddYears(2);
                advisorLOBVo.LicenseNumber = "";
                lobAdded = advisorLOBBo.AddLOBFromUploadScreen(advisorLOBVo, advisorId, userId);
                if (lobAdded == true)
                {
                    divLobAdded.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('LOB already exists !!');", true);
                    divLobAdded.Visible = false;
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
                FunctionInfo.Add("Method", "EditLOB.ascx:btnMFSubmit_Click()");
                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
            {
                adviserId = int.Parse(ddlAdviser.SelectedValue);
                DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);
                Session["adviserId_Upload"] = adviserId;
                if (dsAdviserRmDetails.Tables[0].Rows.Count > 0)
                {
                    rmId = int.Parse(dsAdviserRmDetails.Tables[0].Rows[0]["ar_rmid"].ToString());
                    hfRmId.Value = rmId.ToString();
                }

                tdBtnViewRejetcs.Visible = true;
                tdTxtToDate.Visible = true;
                tdlblToDate.Visible = true;
                tdTxtFromDate.Visible = true;

                tdlblFromDate.Visible = true;
                tdlblRejectReason.Visible = true;
                tdDDLRejectReason.Visible = true;
                divConditional.Visible = false;
                BindddlRejectReason();

                BindGrid(ProcessId);
                gvCAMSProfileReject.Visible = true;
                trNote.Visible = true;
            }
            else
            {
                tdBtnViewRejetcs.Visible = false;
                tdTxtToDate.Visible = false;
                tdlblToDate.Visible = false;
                tdTxtFromDate.Visible = false;

                tdlblFromDate.Visible = false;
                tdlblRejectReason.Visible = false;
                tdDDLRejectReason.Visible = false;

                divGvCAMSProfileReject.Visible = false;
                trNote.Visible = false;
            }

        }
    }
}
