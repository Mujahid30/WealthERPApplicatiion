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
using Telerik.Web.UI;
using System.Data.SqlClient;

namespace WealthERP.Uploads
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
        int filetypeId;

        string configPath;


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
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            if (!IsPostBack)
            {
                //mypager.CurrentPage = 1;
                hdnProcessIdFilter.Value = ProcessId.ToString();
                // Bind Grid
                BindEquityTransactionGrid(ProcessId);
            }
        }

        private void BindEquityTransactionGrid(int ProcessId)
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            rejectedRecordsBo = new RejectedRecordsBo();

            dsRejectedRecords = rejectedRecordsBo.GetRejectedMFTransactionStaging(adviserVo.advisorId, int.Parse(hdnProcessIdFilter.Value));

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                trMessage.Visible = false;
                trReprocess.Visible = true;
                //gvWERPTrans_Sort.DataSource = dsRejectedRecords.Tables[0];
                gvWERPTrans.DataSource = dsRejectedRecords;
                gvWERPTrans.DataBind();


                if (Cache["MFTransactionDetails" + adviserVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("MFTransactionDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("MFTransactionDetails" + adviserVo.advisorId.ToString());
                    Cache.Insert("MFTransactionDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }
            }
            else
            {
                //hdnRecordCount.Value = "0";
                gvWERPTrans.DataSource = null;
                gvWERPTrans.DataBind();
                trMessage.Visible = true;
                trReprocess.Visible = false;
            }
            //this.GetPageCount();
        }

        protected void reprocess()
        {
            //System.Threading.Thread.Sleep(5000);
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            string error = "";
            int processIdReprocessAll = 0;

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;


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

                DataSet ds = uploadsCommonBo.GetUploadDistinctProcessIdForAdviser(adviserVo.advisorId);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    processIdReprocessAll = int.Parse(dr["ProcessId"].ToString());
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

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            reprocess();
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
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, ProcessId, packagePath, configPath, "CA", "CAMS");
                }
            }
            else if (fileTypeId == 25)
            {

                bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(ProcessId);
                if (camsDatatranslationCheckResult)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, ProcessId, packagePath, configPath, "SU", "Sundaram");
                }
            }
            //***reprocess for folioandTrnx
            else if (fileTypeId == 6)
            {
                standardProfileUploadBo = new StandardProfileUploadBo();
                string stdPackagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                CommonStdTransChecks = standardProfileUploadBo.StdCommonProfileChecks(ProcessId, adviserVo.advisorId, stdPackagePath, configPath);


            }

            else if (fileTypeId == 3)
            {

                bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(ProcessId);
                if (karvyDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, ProcessId, packagePath, configPath, "KA", "Karvy");
                }
            }
            else if (fileTypeId == 15)
            {
                bool TempletonDataTranslationCheck = uploadsCommonBo.UploadsTempletonDataTranslationForReprocess(ProcessId);
                if (TempletonDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, ProcessId, packagePath, configPath, "TN", "Templeton");
                }
            }
            else if (fileTypeId == 17)
            {
                bool DeutscheDataTranslationCheck = uploadsCommonBo.UploadsDeutscheDataTranslationForReprocess(ProcessId);
                if (DeutscheDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserVo.advisorId, ProcessId, packagePath, configPath, "DT", "Deutsche");
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

            foreach (GridDataItem gvr in this.gvWERPTrans.Items)
            //foreach (GridViewRow gvr in this.gvWERPTrans.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {                   
                    StagingID += Convert.ToString(gvWERPTrans.MasterTableView.DataKeyValues[i]["CMFTSId"]) + "~";
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
                rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
                if (hdnProcessIdFilter.Value != "")
                {
                    ProcessId = int.Parse(hdnProcessIdFilter.Value);
                }
                BindEquityTransactionGrid(ProcessId);
            }
        }





        protected void btnProbableInsert_Click(object sender, EventArgs e)
        {
            bool result = true;
            bool blResult = true;
            string gvStagingIds = "";
            uploadsCommonBo = new UploadCommonBo();
            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            int fileTypeId = processlogVo.FileTypeId;
            rejectedRecordsBo = new RejectedRecordsBo();
            foreach (GridDataItem gvRow in gvWERPTrans.MasterTableView.Items)
            //foreach (GridViewRow gvRow in gvWERPTrans.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkId");
                if (ChkBxItem.Checked)
                {
                    gvStagingIds += gvWERPTrans.MasterTableView.DataKeyValues[gvRow.RowIndex]["CMFTSId"].ToString() + ",";
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
            foreach (GridDataItem gvRow in gvWERPTrans.MasterTableView.Items)
            //foreach (GridViewRow gvRow in gvWERPTrans.Rows)
            {
                CheckBox ChkBxItem = (CheckBox)gvRow.FindControl("chkId");
                if (ChkBxItem.Checked)
                {
                    gvStagingIds += gvWERPTrans.MasterTableView.DataKeyValues[gvRow.RowIndex]["CMFTSId"].ToString() + ",";
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

        protected void gvWERPTrans_PreRender(object sender, EventArgs e)
        {
            TableCell tc = new TableCell();

            foreach (GridFilteringItem filterItem in gvWERPTrans.MasterTableView.GetItems(GridItemType.FilteringItem))
            {

                RadComboBox dropdown = (RadComboBox)filterItem.FindControl("rcbRejectReasonFilter");
                //if (Session["slectedValue"] != null)
                //    dropdown.SelectedIndex = Convert.ToInt32(Session["slectedValue"]);
            }
            //DataSet ds=(DataSet)gvWERPTrans.MasterTableView.DataSource;
        }
        protected void rcbRejectReasonFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtering logic  
            //RadComboBox dropdown = (RadComboBox)sender;
            //Session["slectedValue"] = dropdown.SelectedIndex; // Saving the selected index in session variable  
            ((GridFilteringItem)(((RadComboBox)sender).Parent.Parent)).FireCommandEvent("Filter", new Pair());
            //DataSet ds = (DataSet)gvWERPTrans.MasterTableView.DataSource;

        }

        //private void rcbRejectReasonFilter_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    ((GridFilteringItem)(((RadComboBox)sender).Parent.Parent)).FireCommandEvent("Filter", new Pair());
        //}

        protected void gvWERPTrans_ItemCreated(object sender, GridItemEventArgs e)
        {
            string strALL;
            if (e.Item is GridFilteringItem && dsRejectedRecords != null)
            {
                BoundColumn bc = new BoundColumn();

                //GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox rcb = new RadComboBox();
                RadComboBoxItem rcbi = new RadComboBoxItem("Select Reject Reason", "0");

                rcb = (RadComboBox)e.Item.FindControl("rcbRejectReasonFilter");

                rcb.DataSource = dsRejectedRecords.Tables[1];
                rcb.DataTextField = "RejectReason";
                rcb.DataValueField = "RejectReasonCode";

                rcb.DataBind();


                rcb.Items.Insert(0, rcbi);
            }
        }

        protected void gvWERPTrans_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["MFTransactionDetails" + adviserVo.advisorId.ToString()];
            gvWERPTrans.DataSource = dtProcessLogDetails;
            if (gvWERPTrans.DataSource != null)
                gvWERPTrans.Visible = true;
            else
                gvWERPTrans.Visible = false;
        }

    }

}
