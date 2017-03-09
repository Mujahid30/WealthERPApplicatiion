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
using BoSuperAdmin;


namespace WealthERP.Uploads
{
    public partial class RejectedMFTransactionStaging : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;
        //  DataTable dtRejectReason;
        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        StandardProfileUploadBo standardProfileUploadBo;
        RMVo rmVo = new RMVo();
        DataSet dsRejectedRecords = new DataSet();
        DataTable dtgvWERPTrans1 = new DataTable();
        DataTable dtgvWERPTrans2 = new DataTable();


        int ProcessId;
        int filetypeId;
        int adviserId;
        int rmId;
        string configPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            btnReprocess.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            ProcessId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            btnMapFolios.Visible = false;

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["filetypeId"] != null)
                filetypeId = Int32.Parse(Request.QueryString["filetypeId"].ToString());

            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            if (adviserVo.advisorId == 1000)
            {
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                {
                    adviserId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    //if (hfRmId.Value != "")
                    //{
                    //    rmId = Convert.ToInt32(hfRmId.Value);
                    //}
                    trGridView.Visible = true;

                    Panel2.ScrollBars = ScrollBars.Horizontal;
                    Session["adviserId_Upload"] = adviserId;
                }
                else
                {
                    trReprocess.Visible = false;
                    trGridView.Visible = false;
                    Panel2.ScrollBars = ScrollBars.None;
                    // Panel2.Visible = false;
                    adviserId = 1000;
                }
            }
            else
            {
                imgBtnrgHoldings.Visible = true;
                trReprocess.Visible = true;
                trGridView.Visible = true;
                Panel2.ScrollBars = ScrollBars.Horizontal;
                //Panel2.Visible = true;
                trAdviserSelection.Visible = false;
                adviserId = adviserVo.advisorId;
            }
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


            if (!IsPostBack)
            {
                //mypager.CurrentPage = 1;
                if (adviserId != 1000)
                {
                    hdnProcessIdFilter.Value = ProcessId.ToString();
                    // Bind Grid
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {
                    imgBtnrgHoldings.Visible = false;
                    BindAdviserDropDownList();
                }
            }


        }

        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                //  RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");

                // DataSet dtProcessLogDetails = new DataSet();
                dsRejectedRecords = (DataSet)Cache["MFTransactionDetails" + adviserId.ToString()];
                dtgvWERPTrans1 = dsRejectedRecords.Tables[0];
                Session["dt"] = dtgvWERPTrans1;
                DataTable dtcustMIS = new DataTable();
                dtcustMIS.Columns.Add("RejectReason");
                //dtcustMIS.Columns.Add("RejectReason");
                // dtcustMIS.Columns.Add("SystematicTransactionType");
                DataRow drcustMIS;
                foreach (DataRow dr in dtgvWERPTrans1.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["RejectReason"] = dr["RejectReason"].ToString();
                    //drcustMIS["RejectReason"] = dr["RejectReason"].ToString();
                    //drcustMIS["SystematicTransactionType"] = dr["TypeCode"].ToString();
                    dtcustMIS.Rows.Add(drcustMIS);
                }
                //combo.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("ALL", "0"));
                DataView view = new DataView(dtgvWERPTrans1);
                DataTable distinctValues = view.ToTable(true, "RejectReason");
                RadComboBoxIN.DataSource = distinctValues;
                RadComboBoxIN.DataValueField = dtcustMIS.Columns["RejectReason"].ToString();
                RadComboBoxIN.DataTextField = dtcustMIS.Columns["RejectReason"].ToString();
                //RadComboBoxIN.ClearSelection();
                RadComboBoxIN.DataBind();

                //RadComboBoxRR.DataSource = dtcustMIS;
                //RadComboBoxRR.DataValueField = dtcustMIS.Columns["RejectReason"].ToString();
                //RadComboBoxRR.DataTextField = dtcustMIS.Columns["RejectReason"].ToString();
                //RadComboBoxRR.ClearSelection();
                //RadComboBoxRR.DataBind();


            }

            // }

        }

        private void BindEquityTransactionGrid(int ProcessId)
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            rejectedRecordsBo = new RejectedRecordsBo();

            dsRejectedRecords = rejectedRecordsBo.GetRejectedMFTransactionStaging(adviserId, int.Parse(hdnProcessIdFilter.Value));

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                trMessage.Visible = false;
                trReprocess.Visible = true;
                trGridView.Visible = true;
                //gvWERPTrans_Sort.DataSource = dsRejectedRecords.Tables[0];


                if (Cache["MFTransactionDetails" + adviserId.ToString()] == null)
                {
                    Cache.Insert("MFTransactionDetails" + adviserId.ToString(), dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("MFTransactionDetails" + adviserId.ToString());
                    Cache.Insert("MFTransactionDetails" + adviserId.ToString(), dsRejectedRecords);
                }

                gvWERPTrans.DataSource = dsRejectedRecords;
                gvWERPTrans.DataBind();
                Panel2.ScrollBars = ScrollBars.Horizontal;
                trGridView.Visible = true;
                imgBtnrgHoldings.Visible = true;
            }
            else
            {
                //hdnRecordCount.Value = "0";
                Panel2.ScrollBars = ScrollBars.None;
                trGridView.Visible = false;
                gvWERPTrans.DataSource = null;
                gvWERPTrans.DataBind();
                trMessage.Visible = true;
                trReprocess.Visible = false;
                imgBtnrgHoldings.Visible = false;
            }
            //this.GetPageCount();
        }

        protected void RadComboBoxRR_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReason"] = dropdown.SelectedValue.ToString();
            if (ViewState["RejectReason"] != "")
            {
                //    gvWERPTrans.MasterTableView.FilterExpression = "([RejectReason]= '" + dropdown.SelectedValue + "')";
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReason");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.MasterTableView.Rebind();

            }
            else
            {
                // gvWERPTrans.MasterTableView.FilterExpression = "0";
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReason");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.MasterTableView.Rebind();


            }

        }




        //protected void rcbContinents_PreRender(object sender, EventArgs e)
        //{
        //    //persist the combo selected value  
        //    if (ViewState["RejectReason"] != null)
        //    {
        //        RadComboBox Combo = sender as RadComboBox;
        //        Combo.SelectedValue = ViewState["RejectReason"].ToString();
        //    }
        //}



        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["RejectReason"] != null)
            {

                Combo.SelectedValue = ViewState["RejectReason"].ToString();
            }

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

                DataSet ds = uploadsCommonBo.GetUploadDistinctProcessIdForAdviser(adviserId);

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
            NeedSource();
            gvWERPTrans.MasterTableView.Rebind();
           // BindEquityTransactionGrid(ProcessId);
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
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, ProcessId, packagePath, configPath, "CA", "CAMS");
                }
            }
            else if (fileTypeId == 25)
            {

                bool camsDatatranslationCheckResult = uploadsCommonBo.UploadsCAMSDataTranslationForReprocess(ProcessId);
                if (camsDatatranslationCheckResult)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, ProcessId, packagePath, configPath, "SU", "Sundaram");
                }
            }
            //***reprocess for folioandTrnx
            else if (fileTypeId == 6)
            {
                standardProfileUploadBo = new StandardProfileUploadBo();
                string stdPackagePath = Server.MapPath("\\UploadPackages\\StandardFolioUploadPackageNew\\StandardFolioUploadPackageNew\\UploadsStandardMFTrxnStagingChk.dtsx");
                CommonStdTransChecks = standardProfileUploadBo.StdCommonProfileChecks(ProcessId, adviserId, stdPackagePath, configPath);


            }

            else if (fileTypeId == 3)
            {

                bool karvyDataTranslationCheck = uploadsCommonBo.UploadsKarvyDataTranslationForReprocess(ProcessId);
                if (karvyDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, ProcessId, packagePath, configPath, "KA", "Karvy");
                }
            }
            else if (fileTypeId == 15)
            {
                bool TempletonDataTranslationCheck = uploadsCommonBo.UploadsTempletonDataTranslationForReprocess(ProcessId);
                if (TempletonDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, ProcessId, packagePath, configPath, "TN", "Templeton");
                }
            }
            else if (fileTypeId == 17)
            {
                bool DeutscheDataTranslationCheck = uploadsCommonBo.UploadsDeutscheDataTranslationForReprocess(ProcessId);
                if (DeutscheDataTranslationCheck)
                {
                    CommonTransChecks = uploadsCommonBo.TransCommonChecks(adviserId, ProcessId, packagePath, configPath, "DT", "Deutsche");
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

            foreach (GridDataItem gvr in this.gvWERPTrans.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                CustomerTransactionDelete();
                NeedSource();
                gvWERPTrans.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                //    rejectedRecordsBo = new RejectedRecordsBo();
                //    rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
                //    if (hdnProcessIdFilter.Value != "")
                //    {
                //        ProcessId = int.Parse(hdnProcessIdFilter.Value);
                //    }
                //    BindEquityTransactionGrid(ProcessId);
            }
        }
        private void CustomerTransactionDelete()
        {
            string StagingID = string.Empty;
            foreach (GridDataItem gvr in this.gvWERPTrans.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    rejectedRecordsBo = new RejectedRecordsBo();
                    StagingID += Convert.ToString(gvWERPTrans.MasterTableView.DataKeyValues[gvr.ItemIndex]["CMFTSId"]) + "~";
                }

            }
            rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
            BindEquityTransactionGrid(ProcessId);

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
        protected void btnSchemeMapping_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ControlHost.aspx?pageid=OnlineSchemeMIS", false);
        }
        protected void btnDataTranslationMapping_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlHost.aspx?pageid=AddSchemeMapping", false);

        }

        protected void gvWERPTrans_PreRender(object sender, EventArgs e)
        {
            if (gvWERPTrans.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }



        //}
        //gvWERPTrans.MasterTableView.FilterExpression = string.Empty;

        //            gvWERPTrans.MasterTableView.Rebind();   
        //    TableCell tc = new TableCell();

        //    foreach (GridFilteringItem filterItem in gvWERPTrans.MasterTableView.GetItems(GridItemType.FilteringItem))
        //    {

        //        RadComboBox dropdown = (RadComboBox)filterItem.FindControl("RadComboBoxRR");

        //        ViewState["RejectReason"] = dropdown.SelectedValue.ToString();


        //        if (ViewState["RejectReason"] != "")
        //        {
        //            if (ViewState["RejectReason"] != null)
        //                dropdown.SelectedValue = ViewState["RejectReason"].ToString();

        //            gvWERPTrans.MasterTableView.FilterExpression = "([RejectReason]= '" + dropdown.SelectedValue + "')";
        //            GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReason");
        //            column.CurrentFilterFunction = GridKnownFunction.EqualTo;
        //            gvWERPTrans.MasterTableView.Rebind();

        //            //+ Combo.SelectedValue +

        //        }
        //        else
        //        {
        //            gvWERPTrans.MasterTableView.FilterExpression = "";
        //            GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReason");
        //            column.CurrentFilterFunction = GridKnownFunction.EqualTo;
        //            gvWERPTrans.MasterTableView.Rebind();

        //        }

        //    }


        protected void RefreshCombos()
        {
            dsRejectedRecords = (DataSet)Cache["MFTransactionDetails" + adviserId.ToString()];
            dtgvWERPTrans1 = dsRejectedRecords.Tables[0];
            DataView view = new DataView(dtgvWERPTrans1);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvWERPTrans.MasterTableView.FilterExpression.ToString());
            gvWERPTrans.MasterTableView.Rebind();
        }

        protected void rcbRejectReasonFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtering logic  
            //RadComboBox dropdown = (RadComboBox)sender;
            //Session["slectedValue"] = dropdown.SelectedIndex; // Saving the selected index in session variable  
            // ((GridFilteringItem)(((RadComboBox)sender).Parent.Parent)).FireCommandEvent("Filter", new Pair());
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
        protected void NeedSource()
        {
            string rcbType = string.Empty;
            trMessage.Visible = false;
            DataSet dtProcessLogDetails = new DataSet();
            DataTable dtrr = new DataTable();
            dtProcessLogDetails = (DataSet)Cache["MFTransactionDetails" + adviserId.ToString()];
            if (dtProcessLogDetails != null)
            {
                dtrr = dtProcessLogDetails.Tables[0];
                if (ViewState["RejectReason"] != null)
                    rcbType = ViewState["RejectReason"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "RejectReason = '" + rcbType + "'", "InvestorName,CMFTS_PANNum,ProcessId,FolioNumber,Scheme,SchemeName,TransactionType,ExternalFileName,SourceType", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvWERPTrans.DataSource = dtProcessLogDetails;

                }
            }
        }
        protected void gvWERPTrans_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            trMessage.Visible = false;
            DataSet dtProcessLogDetails = new DataSet();
            DataTable dtrr = new DataTable();
            dtProcessLogDetails = (DataSet)Cache["MFTransactionDetails" + adviserId.ToString()];
            if (dtProcessLogDetails != null)
            {
                dtrr = dtProcessLogDetails.Tables[0];
                if (ViewState["RejectReason"] != null)
                    rcbType = ViewState["RejectReason"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "RejectReason = '" + rcbType + "'", "InvestorName,CMFTS_PANNum,ProcessId,FolioNumber,Scheme,SchemeName,TransactionType,ExternalFileName,SourceType", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvWERPTrans.DataSource = dtProcessLogDetails;

                }
            }
            //DataSet dtProcessLogDetails = new DataSet();
            //dtProcessLogDetails = (DataSet)Cache["MFTransactionDetails" + adviserVo.advisorId.ToString()];
            //gvWERPTrans.DataSource = dtProcessLogDetails;
            //if (gvWERPTrans.DataSource != null)
            //    gvWERPTrans.Visible = true;
            //else
            //    gvWERPTrans.Visible = false;
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvWERPTrans.ExportSettings.OpenInNewWindow = true;
           // gvWERPTrans.MasterTableView.Caption = "Adviser:" + adviserVo.OrganizationName+' '+"RM:"+ rmVo.FirstName;            
            gvWERPTrans.ExportSettings.IgnorePaging = true;
            gvWERPTrans.ExportSettings.HideStructureColumns = true;
            gvWERPTrans.ExportSettings.ExportOnlyData = true;
            gvWERPTrans.ExportSettings.FileName = "Rejected MF Transaction Details";
            gvWERPTrans.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvWERPTrans.MasterTableView.ExportToExcel();
        }
        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
            {
                adviserId = int.Parse(ddlAdviser.SelectedValue);
                DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);

                hdnProcessIdFilter.Value = ProcessId.ToString();
                // Bind Grid
                trReprocess.Visible = false;
                BindEquityTransactionGrid(ProcessId);
                //imgBtnrgHoldings.Visible = true;
                Session["adviserId_Upload"] = adviserId;
            }
            else
            {
                trReprocess.Visible = true;
                trGridView.Visible = true;
                imgBtnrgHoldings.Visible = false;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string newScheme = string.Empty;
            string PanNum = string.Empty;
            string TransactionType = string.Empty;
            string FolioNumber = string.Empty;
            double Price = 0;
            double Units = 0;
            double Amount = 0;
            int UserTransactionNo = 0;
            int CMFTSId = 0;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();
            GridFooterItem footerRow = (GridFooterItem)gvWERPTrans.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvWERPTrans.Items)
            {
                if (((TextBox)footerRow.FindControl("txtSchemeFooter")).Text.Trim() == "")
                {
                    newScheme = ((TextBox)dr.FindControl("txtScheme")).Text;
                }
                else
                {
                    newScheme = ((TextBox)footerRow.FindControl("txtSchemeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPanFooter")).Text.Trim() == "")
                {
                    PanNum = ((TextBox)dr.FindControl("txtPanNum")).Text;
                }
                else
                {
                    PanNum = ((TextBox)footerRow.FindControl("txtPanFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtTransactionFooter")).Text.Trim() == "")
                {
                    TransactionType = ((TextBox)dr.FindControl("txtTransaction")).Text;
                }
                else
                {
                    TransactionType = ((TextBox)footerRow.FindControl("txtTransactionFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtFolioNumberFooter")).Text.Trim() == "")
                {
                    FolioNumber = ((TextBox)dr.FindControl("txtFolioNumber")).Text;
                }
                else
                {
                    FolioNumber = ((TextBox)footerRow.FindControl("txtFolioNumberFooter")).Text;
                }

                if (((TextBox)footerRow.FindControl("txtPriceFooter")).Text.Trim() =="")
                {
                    Price = Convert.ToDouble(((TextBox)dr.FindControl("txtPrice")).Text);
                }
                else
                {
                    Price = Convert.ToDouble(((TextBox)footerRow.FindControl("txtPriceFooter")).Text);
                }
                if (((TextBox)footerRow.FindControl("txtUnitsFooter")).Text.Trim() == "")
                {
                    Units = Convert.ToDouble(((TextBox)dr.FindControl("txtUnits")).Text);
                }
                else
                {
                    Units = Convert.ToDouble(((TextBox)footerRow.FindControl("txtUnitsFooter")).Text);
                }
                if (((TextBox)footerRow.FindControl("txtAmountFooter")).Text.Trim() == "")
                {
                    Amount = Convert.ToDouble(((TextBox)dr.FindControl("txtAmount")).Text);
                }
                else
                {
                    Amount = Convert.ToDouble(((TextBox)footerRow.FindControl("txtAmountFooter")).Text);
                }
                if (((TextBox)footerRow.FindControl("txtUsertransactionFooter")).Text.Trim() == "")
                {
                    UserTransactionNo = Convert.ToInt32(((TextBox)dr.FindControl("txtUsertransaction")).Text);
                }
                else
                {
                    UserTransactionNo = Convert.ToInt32(((TextBox)footerRow.FindControl("txtUsertransactionFooter")).Text);
                }


                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    CMFTSId = int.Parse((gvWERPTrans.MasterTableView.DataKeyValues[selectedRow - 1]["CMFTSId"].ToString()));
                    blResult = rejectedRecordsBo.UpdateMFTrasactionStaging(CMFTSId, PanNum, newScheme, TransactionType, FolioNumber, Price, Units, Amount, UserTransactionNo);
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
            //BindGrid(ProcessId);
            BindEquityTransactionGrid(ProcessId);
        }
    }

}
