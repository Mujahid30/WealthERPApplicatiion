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

using BoAdvisorProfiling;

namespace WealthERP.Uploads
{
    public partial class RejectedMFFolio : System.Web.UI.UserControl
    {


        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
        int ProcessId;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            //ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            BindddlRejectReason();
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (Request.QueryString["filetypeid"] != null)
            {
                filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());
                LinkInputRejects.Visible = true;
            }
            else
                LinkInputRejects.Visible = false;
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
                if (ProcessId != 0)
                {
                    divConditional.Visible = false;
                    BindGrid(ProcessId);
                }
                else
                {
                    divGvCAMSProfileReject.Visible = false;
                }
            }
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
            bool blResult = false;
            int StagingId = 0;
            int MainStagingId = 0;
            rejectedRecordsBo = new RejectedRecordsBo();
            GridFooterItem footerRow = (GridFooterItem)gvCAMSProfileReject.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvCAMSProfileReject.Items)
            {
                if (((TextBox)footerRow.FindControl("txtPanMultiple")).Text.Trim() == "" && ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text.Trim() == "")
                {
                    newPan = ((TextBox)dr.FindControl("txtPan")).Text;
                    newFolio = ((TextBox)dr.FindControl("txtFolio")).Text;
                }
                else
                {
                    newPan = ((TextBox)footerRow.FindControl("txtPanMultiple")).Text;
                    newFolio = ((TextBox)footerRow.FindControl("txtFolioMultiple")).Text;
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

                    blResult = rejectedRecordsBo.UpdateMFFolioStaging(StagingId, MainStagingId, newPan, newFolio);
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
            BindGrid(ProcessId);
        }

        private void BindGrid(int ProcessId)
        {         
            try
            {
                if (ProcessId == null || ProcessId == 0)
                {
                    fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
                    toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
                    rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
                }


                Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
                Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
                Dictionary<string, string> genDictIsCustomerExisting = new Dictionary<string, string>();

                rejectedRecordsBo = new RejectedRecordsBo();
                dsRejectedRecords = rejectedRecordsBo.getMFRejectedFolios(adviserVo.advisorId, ProcessId, fromDate,toDate,rejectReasonCode);
         
                if (dsRejectedRecords.Tables[0].Rows.Count > 0)
                {   // If Records found, then bind them to grid
                    divProfileMessage.Visible = false;
                    divGvCAMSProfileReject.Visible = true;
                    imgBtnrgHoldings.Visible = true;
                    divBtnActionSection.Visible = true;

                    gvCAMSProfileReject.DataSource = dsRejectedRecords.Tables[0];
                    gvCAMSProfileReject.DataBind();

                    if (Cache["RejectedMFFolioDetails" + adviserVo.advisorId.ToString()] == null)
                    {
                        Cache.Insert("RejectedMFFolioDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords.Tables[0]);
                    }
                    else
                    {
                        Cache.Remove("RejectedMFFolioDetails" + adviserVo.advisorId.ToString());
                        Cache.Insert("RejectedMFFolioDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords.Tables[0]);
                    }
                }
                else
                {                    
                    divProfileMessage.Visible = true;
                    divBtnActionSection.Visible = false;
                    divGvCAMSProfileReject.Visible = false;
                    imgBtnrgHoldings.Visible = false;
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
                    StagingID += Convert.ToString(gvCAMSProfileReject.MasterTableView.DataKeyValues[i]["MFFolioStagingId"]) + "~";
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedMFFolio','login');", true);
            }
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            reprocessMFFolio();
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
                bool camsFolioStagingChkResult = standardFolioUploadBo.MFFolioStagingCheck(packagePath, adviserVo.advisorId, ProcessId, configPath);
                if (camsFolioStagingChkResult)
                {
                    //Folio Chks in Std Folio Staging 
                    //packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadStdFolioFromFolioStagingToWerpTable.dtsx");
                    packagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\MoveDataFromStagingToMutufundAccount.dtsx");
                    bool camsFolioWerpInsertionResult = standardFolioUploadBo.MfFolioExceptionFinalTableInsertion(packagePath, adviserVo.advisorId, ProcessId, configPath);
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

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedWERPProfile','processId=" + ProcessId + "');", true);

        }

        protected void LinkInputRejects_Click(object sender, EventArgs e)
        {
            if (filetypeId == (int)Contants.UploadTypes.CAMSProfile)
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
            DataTable dtRejectedMFFolioDetails = new DataTable();

            dtRejectedMFFolioDetails = (DataTable)Cache["RejectedMFFolioDetails" + adviserVo.advisorId.ToString()];
            gvCAMSProfileReject.DataSource = dtRejectedMFFolioDetails;

        }

        protected void btnViewTran_Click(object sender, EventArgs e)
        {
            BindGrid(ProcessId);
        }

        protected void btnAddLob_Click(object sender, EventArgs e)
        {
           string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            int advisorId = adviserVo.advisorId;
            string adviserOrgName = adviserVo.OrganizationName;
            int userId = userVo.UserId;
            string segment = "";
            string assetClass = "MF";
            string category = "INT";
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

                }
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.IdentifierTypeCode = "ARN";
                advisorLOBVo.OrganizationName = adviserOrgName;
                advisorLOBVo.Identifier = strLob;
                advisorLOBVo.ValidityDate = DateTime.Now.AddYears(2);
                advisorLOBVo.LicenseNumber = "";
                advisorLOBBo.AddLOBFromUploadScreen(advisorLOBVo, advisorId, userId);                
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
    }
}
